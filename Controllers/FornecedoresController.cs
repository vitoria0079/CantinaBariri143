﻿using System;
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
    public class FornecedoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FornecedoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Fornecedores
        public async Task<IActionResult> Index()
        {
            return _context.Fornecedores != null ?
                        View(await _context.Fornecedores.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Fornecedores'  is null.");
        }

        // GET: Fornecedores/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Fornecedores == null)
            {
                return NotFound();
            }

            var fornecedores = await _context.Fornecedores
                .FirstOrDefaultAsync(m => m.FornecedoresId == id);
            if (fornecedores == null)
            {
                return NotFound();
            }

            return View(fornecedores);
        }

        // GET: Fornecedores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fornecedores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FornecedoresId,RazaoSocial,CNPJ")] Fornecedores fornecedores)
        {
            if (ModelState.IsValid)
            {
                fornecedores.FornecedoresId = Guid.NewGuid();
                _context.Add(fornecedores);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fornecedores);
        }

        // GET: Fornecedores/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Fornecedores == null)
            {
                return NotFound();
            }

            var fornecedores = await _context.Fornecedores.FindAsync(id);
            if (fornecedores == null)
            {
                return NotFound();
            }
            return View(fornecedores);
        }

        // POST: Fornecedores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FornecedoresId,RazaoSocial,CNPJ")] Fornecedores fornecedores)
        {
            if (id != fornecedores.FornecedoresId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fornecedores);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FornecedoresExists(fornecedores.FornecedoresId))
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
            return View(fornecedores);
        }

        // GET: Fornecedores/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Fornecedores == null)
            {
                return NotFound();
            }

            var fornecedores = await _context.Fornecedores
                .FirstOrDefaultAsync(m => m.FornecedoresId == id);
            if (fornecedores == null)
            {
                return NotFound();
            }

            return View(fornecedores);
        }

        // POST: Fornecedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Fornecedores == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Fornecedores'  is null.");
            }
            var fornecedores = await _context.Fornecedores.FindAsync(id);
            if (fornecedores != null)
            {
                _context.Fornecedores.Remove(fornecedores);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FornecedoresExists(Guid id)
        {
            return (_context.Fornecedores?.Any(e => e.FornecedoresId == id)).GetValueOrDefault();
        }

        // GET: Fornecedores/Search
        public async Task<IActionResult> Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return View("Index", await _context.Fornecedores.ToListAsync());
            }

            var fornecedores = await _context.Fornecedores
                .Where(a => a.RazaoSocial.Contains(searchTerm) || a.CNPJ.Contains(searchTerm))
                .ToListAsync();

            return View("Index", fornecedores);
        }

    }
}
