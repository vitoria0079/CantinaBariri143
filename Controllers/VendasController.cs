using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CantinaBariri143.Data;
using CantinaBariri143.Models;

namespace CantinaBariri143.Controllers
{
    public class VendasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VendasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vendas
        public async Task<IActionResult> Index()
        {
            // Carrega todos os pedidos ordenados por DataPedido
            var pedidosOrdenados = await _context.Pedidos
                .OrderBy(p => p.DataPedido)
                .Select(p => p.PedidosId)
                .ToListAsync();

            // Cria o dicionário: PedidosId => NumeroPedido (D5)
            var numeroPedidoDict = pedidosOrdenados
                .Select((id, idx) => new { id, numero = (idx + 1).ToString("D5") })
                .ToDictionary(x => x.id, x => x.numero);

            // Carrega as vendas com os pedidos relacionados
            var vendas = await _context.Vendas
                .Include(v => v.Clientes)
                .Include(v => v.Pedidos)
                .ToListAsync();

            ViewBag.NumeroPedidoDict = numeroPedidoDict;

            return View(vendas);
        }

        // GET: Vendas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {

            var venda = await _context.Vendas
                .Include(v => v.Clientes)
                .Include(v => v.Pedidos)
                .FirstOrDefaultAsync(m => m.VendasId == id);

            if (venda == null)
            {
                return NotFound();
            }

            return View(venda);
        }


        // GET: Vendas/Create
        public IActionResult Create()
        {
            ViewData["ClientesId"] = new SelectList(_context.Clientes, "ClientesId", "Nome");
            ViewBag.Pedidos = _context.Pedidos
            .OrderBy(p => p.DataPedido)
            .ToList() // materializa a lista na memória
            .Select((p, index) => new
            {
                p.PedidosId,
                NumeroPedido = (index + 1).ToString("D5"),
                p.Total
            })
            .ToList();

            return View();
        }


        // POST: Vendas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Vendas vendas, List<Guid> PedidosIds)
        {
            if (ModelState.IsValid)
            {
                var cliente = await _context.Clientes.FindAsync(vendas.ClientesId);

                // Adicione esta linha antes de usar pedidosSelecionados:
                var pedidosSelecionados = _context.Pedidos
                    .Include(p => p.Alimentos)
                    .Where(p => PedidosIds.Contains(p.PedidosId))
                    .ToList();

                var pedidos = _context.Pedidos
                .OrderBy(p => p.DataPedido)
                .ToList()
                .Select((p, index) => new
                {
                    p.PedidosId,
                    NumeroPedido = (index + 1).ToString("D5"),
                    p.AlimentosId,
                    p.Qtd,
                    p.DataPedido,
                    p.Total
                    // adicione outros campos que quiser exibir
                })
                .ToList();

                ViewBag.Pedidos = pedidos;

                // Agora pedidosSelecionados está disponível para uso
                var restricoesCliente = (cliente?.Restricao ?? "").Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                var conflitos = new List<string>();

                foreach (var pedido in pedidosSelecionados)
                {
                    var restricoesAlimento = (pedido.Alimentos?.Restricoes ?? "").Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                    var conflito = restricoesCliente.Intersect(restricoesAlimento, StringComparer.OrdinalIgnoreCase).ToList();
                    if (conflito.Any())
                    {
                        conflitos.Add($"Atenção: O cliente '{cliente.Nome}' possui restrição alimentar '{cliente.Restricao}' e o alimento '{pedido.Alimentos.Descricao}' contém essa restrição. Venda pode ser perigosa!");
                    }
                }

                if (conflitos.Any())
                {
                    ViewBag.AlertaRestricao = string.Join("<br>", conflitos);
                    ViewData["ClientesId"] = new SelectList(_context.Clientes, "ClientesId", "Nome", vendas.ClientesId);
                    ViewBag.Pedidos = _context.Pedidos
                        .OrderBy(p => p.DataPedido)
                        .ToList()
                        .Select((p, index) => new
                        {
                            p.PedidosId,
                            NumeroPedido = (index + 1).ToString("D5"),
                            p.Total
                        })
                        .ToList();
                    return View(vendas); // NÃO SALVA, apenas exibe o alerta
                }

                // Se não houver conflito, salva normalmente
                vendas.VendasId = Guid.NewGuid();
                if (PedidosIds != null && PedidosIds.Any())
                    vendas.PedidosId = PedidosIds.First(); // Preenche o campo obrigatório

                vendas.Total = pedidosSelecionados.Sum(p => Convert.ToDecimal(p.Total)).ToString("F2");
                vendas.DataVenda = DateTime.Now;

                foreach (var pedido in pedidosSelecionados)
                {
                    if (pedido.Alimentos != null)
                    {
                        pedido.Alimentos.QtdEstoque -= pedido.Qtd;
                        _context.Alimentos.Update(pedido.Alimentos);
                    }
                }

                _context.Add(vendas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ClientesId"] = new SelectList(_context.Clientes, "ClientesId", "Nome", vendas.ClientesId);
            ViewBag.Pedidos = _context.Pedidos.Select(p => new { p.PedidosId, p.Total }).ToList();
            return View(vendas);
        }



        // GET: Vendas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Vendas == null)
            {
                return NotFound();
            }

            var vendas = await _context.Vendas.FindAsync(id);
            if (vendas == null)
            {
                return NotFound();
            }
            ViewData["ClientesId"] = new SelectList(_context.Clientes, "ClientesId", "Nome", vendas.ClientesId);
            ViewData["PedidosId"] = new SelectList(_context.Pedidos, "PedidosId", "AlimentosId", vendas.PedidosId);
            return View(vendas);
        }

        // POST: Vendas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("VendasId,ClientesId,PedidosId,DataVenda,Total")] Vendas vendas)
        {
            if (id != vendas.VendasId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendasExists(vendas.VendasId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientesId"] = new SelectList(_context.Clientes, "ClientesId", "Nome", vendas.ClientesId);
            ViewData["PedidosId"] = new SelectList(_context.Pedidos, "PedidosId", "AlimentosId", vendas.PedidosId);
            return View(vendas);
        }

        // GET: Vendas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Vendas == null)
            {
                return NotFound();
            }

            var vendas = await _context.Vendas
                .Include(v => v.Clientes)
                .Include(v => v.Pedidos)
                .FirstOrDefaultAsync(m => m.VendasId == id);
            if (vendas == null)
            {
                return NotFound();
            }

            return View(vendas);
        }

        // POST: Vendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Vendas == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Vendas'  is null.");
            }
            var vendas = await _context.Vendas
                .Include(v => v.Pedidos)
                .ThenInclude(p => p.Alimentos)
                .FirstOrDefaultAsync(v => v.VendasId == id);

            if (vendas != null)
            {
                // Devolve a quantidade ao estoque
                if (vendas.Pedidos?.Alimentos != null)
                {
                    vendas.Pedidos.Alimentos.QtdEstoque += vendas.Pedidos.Qtd;
                    _context.Alimentos.Update(vendas.Pedidos.Alimentos);
                }

                _context.Vendas.Remove(vendas);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool VendasExists(Guid id)
        {
            return (_context.Vendas?.Any(e => e.VendasId == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return View("Index", await _context.Vendas.Include(a => a.Clientes).ToListAsync());
            }

            var vendas = await _context.Vendas
                .Where(a => a.Clientes.Nome.Contains(searchTerm) || a.Pedidos.Alimentos.Descricao.Contains(searchTerm)).Include(a => a.Clientes)
                .ToListAsync();

            return View("Index", vendas);
        }

    }
}
