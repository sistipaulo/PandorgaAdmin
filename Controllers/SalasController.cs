using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pandorga_Admin.Data;
using Pandorga_Admin.Models;

namespace Pandorga_Admin.Controllers
{
    public class SalasController : Controller
    {
        private readonly Pandorga_AdminContext _context;

        public SalasController(Pandorga_AdminContext context)
        {
            _context = context;
        }

        // GET: Salas
        public async Task<IActionResult> Index()
        {
            var pandorga_AdminContext = _context.Sala.Include(s => s.Turma);
            return View(await pandorga_AdminContext.ToListAsync());
        }

        // GET: Salas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sala == null)
            {
                return NotFound();
            }

            var sala = await _context.Sala
                .Include(s => s.Turma)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (sala == null)
            {
                return NotFound();
            }

            return View(sala);
        }

        // GET: Salas/Create
        public IActionResult Create()
        {
            ViewData["TurmaID"] = new SelectList(_context.Set<Turma>(), "ID", "NomeTurma");
            return View();
        }

        // POST: Salas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nome,Capacidade,TurmaID")] Sala sala)
        {

                _context.Add(sala);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

        }

        // GET: Salas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sala == null)
            {
                return NotFound();
            }

            var sala = await _context.Sala.FindAsync(id);
            if (sala == null)
            {
                return NotFound();
            }
            ViewData["TurmaID"] = new SelectList(_context.Set<Turma>(), "ID", "NomeTurma", sala.TurmaID);
            return View(sala);
        }

        // POST: Salas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nome,Capacidade,TurmaID")] Sala sala)
        {
            if (id != sala.ID)
            {
                return NotFound();
            }


                try
                {
                    _context.Update(sala);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalaExists(sala.ID))
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

        // GET: Salas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sala == null)
            {
                return NotFound();
            }

            var sala = await _context.Sala
                .Include(s => s.Turma)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (sala == null)
            {
                return NotFound();
            }

            return View(sala);
        }

        // POST: Salas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sala == null)
            {
                return Problem("Entity set 'Pandorga_AdminContext.Sala'  is null.");
            }
            var sala = await _context.Sala.FindAsync(id);
            if (sala != null)
            {
                _context.Sala.Remove(sala);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalaExists(int id)
        {
          return (_context.Sala?.Any(e => e.ID == id)).GetValueOrDefault();
        }

        public IActionResult Relatorio()
        {
            var salas = _context.Sala.Include(s => s.Turma).ToList();

            ViewData["TotalSalas"] = salas.Count();

            var salasPorTurma = salas.Where(s => s.TurmaID.HasValue)
                                     .GroupBy(s => s.Turma.NomeTurma)
                                     .ToDictionary(g => g.Key, g => g.Count());
            ViewData["SalasPorTurma"] = salasPorTurma;

            var capacidadeTotal = salas.Sum(s => s.Capacidade);
            ViewData["CapacidadeTotal"] = capacidadeTotal;

            var capacidadeMedia = salas.Count() > 0 ? salas.Average(s => s.Capacidade) : 0;
            ViewData["CapacidadeMedia"] = capacidadeMedia;

            return View(salas);
        }

    }
}
