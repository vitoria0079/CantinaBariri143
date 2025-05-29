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
    public class ComprasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComprasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Compras
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Compras.Include(c => c.Alimentos).Include(c => c.Fornecedores);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Compras/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Compras == null)
            {
                return NotFound();
            }

            var compras = await _context.Compras
                .Include(c => c.Alimentos)
                .Include(c => c.Fornecedores)
                .FirstOrDefaultAsync(m => m.ComprasId == id);
            if (compras == null)
            {
                return NotFound();
            }

            return View(compras);
        }

        // GET: Compras/Create
        public IActionResult Create()
        {
            ViewData["FornecedoresId"] = new SelectList(_context.Fornecedores, "FornecedoresId", "RazaoSocial");
            ViewData["AlimentosId"] = new SelectList(_context.Alimentos, "AlimentosId", "Descricao");
            ViewBag.AlimentosValores = _context.Alimentos
                .ToDictionary(a => a.AlimentosId.ToString(), a => a.PrecoUnitario);
            return View();
        }

        // POST: Compras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComprasId,FornecedoresId,AlimentosId,PrecoUnitario,DataCompra,QtdUnitaria")] Compras compras)
        {
            if (ModelState.IsValid)
            {
                compras.ComprasId = Guid.NewGuid();
                _context.Add(compras);
                await _context.SaveChangesAsync();

                // Atualiza o estoque do alimento comprado
                var alimento = await _context.Alimentos.FindAsync(compras.AlimentosId);
                if (alimento != null)
                {
                    alimento.QtdEstoque += compras.QtdUnitaria;
                    _context.Alimentos.Update(alimento);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["FornecedoresId"] = new SelectList(_context.Fornecedores, "FornecedoresId", "RazaoSocial", compras.FornecedoresId);
            ViewData["AlimentosId"] = new SelectList(_context.Alimentos, "AlimentosId", "Descricao", compras.AlimentosId);
            ViewBag.AlimentosValores = _context.Alimentos
                .ToDictionary(a => a.AlimentosId.ToString(), a => a.PrecoUnitario);
            return View(compras);
        }


        // GET: Compras/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Compras == null)
            {
                return NotFound();
            }

            var compras = await _context.Compras.FindAsync(id);
            if (compras == null)
            {
                return NotFound();
            }
            ViewData["AlimentosId"] = new SelectList(_context.Alimentos, "AlimentosId", "Descricao", compras.AlimentosId);
            ViewData["FornecedoresId"] = new SelectList(_context.Fornecedores, "FornecedoresId", "RazaoSocial", compras.FornecedoresId);
            return View(compras);
        }

        // POST: Compras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ComprasId,FornecedoresId,AlimentosId,PrecoUnitario,DataCompra,QtdUnitaria")] Compras compras)
        {
            if (id != compras.ComprasId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(compras);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComprasExists(compras.ComprasId))
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
            ViewData["AlimentosId"] = new SelectList(_context.Alimentos, "AlimentosId", "Descricao", compras.AlimentosId);
            ViewData["FornecedoresId"] = new SelectList(_context.Fornecedores, "FornecedoresId", "RazaoSocial", compras.FornecedoresId);
            return View(compras);
        }

        // GET: Compras/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Compras == null)
            {
                return NotFound();
            }

            var compras = await _context.Compras
                .Include(c => c.Alimentos)
                .Include(c => c.Fornecedores)
                .FirstOrDefaultAsync(m => m.ComprasId == id);
            if (compras == null)
            {
                return NotFound();
            }

            return View(compras);
        }

        // POST: Compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var compra = await _context.Compras.FindAsync(id);
            if (compra != null)
            {
                // Atualiza o estoque do alimento
                var alimento = await _context.Alimentos.FindAsync(compra.AlimentosId);
                if (alimento != null)
                {
                    alimento.QtdEstoque -= compra.QtdUnitaria;
                    if (alimento.QtdEstoque < 0) alimento.QtdEstoque = 0; // Evita estoque negativo
                    _context.Update(alimento);
                }

                _context.Compras.Remove(compra);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }


        private bool ComprasExists(Guid id)
        {
            return (_context.Compras?.Any(e => e.ComprasId == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return View("Index", await _context.Compras.Include(a => a.Alimentos).Include(f => f.Fornecedores).ToListAsync());
            }

            var compras = await _context.Compras
                .Where(a => a.Fornecedores.RazaoSocial.Contains(searchTerm) || a.Alimentos.Descricao.Contains(searchTerm)).Include(a => a.Alimentos).Include(f => f.Fornecedores)
                .ToListAsync();

            return View("Index", compras);
        }

    }
}
