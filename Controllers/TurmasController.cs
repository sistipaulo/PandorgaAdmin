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
    public class TurmasController : Controller
    {
        private readonly Pandorga_AdminContext _context;

        public TurmasController(Pandorga_AdminContext context)
        {
            _context = context;
        }

        // GET: Turmas
        public async Task<IActionResult> Index()
        {
            var pandorga_AdminContext = _context.Turma.Include(t => t.Professor);
            return View(await pandorga_AdminContext.ToListAsync());
        }

        // GET: Turmas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Turma == null)
            {
                return NotFound();
            }

            var turma = await _context.Turma
                .Include(t => t.Professor)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (turma == null)
            {
                return NotFound();
            }

            return View(turma);
        }

        // GET: Turmas/Create
        public IActionResult Create()
        {
            ViewData["ProfessorID"] = new SelectList(_context.Professor, "ID", "Nome");
            return View();
        }

        // POST: Turmas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,NomeTurma,Ano,Turno,ProfessorID")] Turma turma)
        {

            
                _context.Add(turma);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
        }

        // GET: Turmas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Turma == null)
            {
                return NotFound();
            }

            var turma = await _context.Turma.FindAsync(id);
            if (turma == null)
            {
                return NotFound();
            }
            ViewData["ProfessorID"] = new SelectList(_context.Professor, "ID", "Nome", turma.ProfessorID);
            return View(turma);
        }

        // POST: Turmas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,NomeTurma,Ano,Turno,ProfessorID")] Turma turma)
        {
            if (id != turma.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(turma);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TurmaExists(turma.ID))
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
            ViewData["ProfessorID"] = new SelectList(_context.Professor, "ID", "Nome", turma.ProfessorID);
            return View(turma);
        }

        // GET: Turmas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Turma == null)
            {
                return NotFound();
            }

            var turma = await _context.Turma
                .Include(t => t.Professor)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (turma == null)
            {
                return NotFound();
            }

            return View(turma);
        }

        // POST: Turmas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Turma == null)
            {
                return Problem("Entity set 'Pandorga_AdminContext.Turma'  is null.");
            }
            var turma = await _context.Turma.FindAsync(id);
            if (turma != null)
            {
                _context.Turma.Remove(turma);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TurmaExists(int id)
        {
          return (_context.Turma?.Any(e => e.ID == id)).GetValueOrDefault();
        }

        public IActionResult Relatorio()
        {
            var turmas = _context.Turma.Include(t => t.Professor)
                                       .Include(t => t.Alunos)
                                       .Include(t => t.Eventos)
                                       .ToList();

            ViewData["TotalTurmas"] = turmas.Count();

            var alunos = turmas.SelectMany(t => t.Alunos).Where(a => a.DataNascimento != default(DateTime)).ToList();
            ViewData["MediaIdade"] = alunos.Count() > 0 ? (int)Math.Round(alunos.Average(a => (DateTime.Now - a.DataNascimento).Days / 365.25)) : 0;

            var faixaEtaria = new Dictionary<string, int>
            {
                { "Menos de 1 ano", alunos.Count(a => (DateTime.Now - a.DataNascimento).TotalDays < 365) },
                { "1 a 3 anos", alunos.Count(a => (DateTime.Now - a.DataNascimento).TotalDays >= 365 && (DateTime.Now - a.DataNascimento).TotalDays < 3 * 365) },
                { "3 a 5 anos", alunos.Count(a => (DateTime.Now - a.DataNascimento).TotalDays >= 3 * 365 && (DateTime.Now - a.DataNascimento).TotalDays < 5 * 365) },
                { "5 a 7 anos", alunos.Count(a => (DateTime.Now - a.DataNascimento).TotalDays >= 5 * 365 && (DateTime.Now - a.DataNascimento).TotalDays < 7 * 365) },
                { "7 anos ou mais", alunos.Count(a => (DateTime.Now - a.DataNascimento).TotalDays >= 7 * 365) }
            };

            ViewData["FaixaEtaria"] = faixaEtaria;

            var totalEventos = turmas.SelectMany(t => t.Eventos).Count();
            ViewData["TotalEventos"] = totalEventos;

            var turmasPorAno = turmas.GroupBy(t => t.Ano).ToDictionary(g => g.Key, g => g.Count());
            ViewData["TurmasPorAno"] = turmasPorAno;

            return View(turmas);
        }


    }
}
