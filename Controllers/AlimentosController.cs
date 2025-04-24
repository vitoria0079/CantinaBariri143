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
    public class AlimentosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlimentosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Alimentos
        public async Task<IActionResult> Index()
        {
            return _context.Alimentos != null ?
                        View(await _context.Alimentos.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Alimentos'  is null.");
        }

        // GET: Alimentos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Alimentos == null)
            {
                return NotFound();
            }

            var alimentos = await _context.Alimentos
                .FirstOrDefaultAsync(m => m.AlimentosId == id);
            if (alimentos == null)
            {
                return NotFound();
            }

            return View(alimentos);
        }

        // GET: Alimentos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Alimentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AlimentosId,Descricao,PrecoUnitario,Validade,QtdEstoque,Restricoes")] Alimentos alimentos)
        {
            if (ModelState.IsValid)
            {
                alimentos.AlimentosId = Guid.NewGuid();
                _context.Add(alimentos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(alimentos);
        }

        // GET: Alimentos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Alimentos == null)
            {
                return NotFound();
            }

            var alimentos = await _context.Alimentos.FindAsync(id);
            if (alimentos == null)
            {
                return NotFound();
            }
            return View(alimentos);
        }

        // POST: Alimentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AlimentosId,Descricao,PrecoUnitario,Validade,QtdEstoque,Restricoes")] Alimentos alimentos)
        {
            if (id != alimentos.AlimentosId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alimentos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlimentosExists(alimentos.AlimentosId))
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
            return View(alimentos);
        }

        // GET: Alimentos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Alimentos == null)
            {
                return NotFound();
            }

            var alimentos = await _context.Alimentos
                .FirstOrDefaultAsync(m => m.AlimentosId == id);
            if (alimentos == null)
            {
                return NotFound();
            }

            return View(alimentos);
        }

        // POST: Alimentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Alimentos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Alimentos'  is null.");
            }
            var alimentos = await _context.Alimentos.FindAsync(id);
            if (alimentos != null)
            {
                _context.Alimentos.Remove(alimentos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlimentosExists(Guid id)
        {
            return (_context.Alimentos?.Any(e => e.AlimentosId == id)).GetValueOrDefault();
        }

        // GET: Alimentos/Search
        public async Task<IActionResult> Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return View("Index", await _context.Alimentos.ToListAsync());
            }

            var alimentos = await _context.Alimentos
                .Where(a => a.Descricao.Contains(searchTerm) || a.Restricoes.Contains(searchTerm))
                .ToListAsync();

            return View("Index", alimentos);
        }

    }
}

