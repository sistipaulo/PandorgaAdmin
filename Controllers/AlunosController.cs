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
    public class AlunosController : Controller
    {
        private readonly Pandorga_AdminContext _context;

        public AlunosController(Pandorga_AdminContext context)
        {
            _context = context;
        }

        // GET: Alunos
        public async Task<IActionResult> Index()
        {
            var pandorga_AdminContext = _context.Aluno.Include(a => a.Turma);
            return View(await pandorga_AdminContext.ToListAsync());
        }

        // GET: Alunos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Aluno == null)
            {
                return NotFound();
            }

            var aluno = await _context.Aluno
                .Include(a => a.Turma)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        // GET: Alunos/Create
        public IActionResult Create()
        {
            ViewData["TurmaID"] = new SelectList(_context.Turma, "ID", "NomeTurma");
            return View();
        }

        // POST: Alunos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nome,DataNascimento,NomeResponsavel,ContatoResponsavel,Endereco,TurmaID")] Aluno aluno)
        {

                _context.Add(aluno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

        }

        // GET: Alunos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Aluno == null)
            {
                return NotFound();
            }

            var aluno = await _context.Aluno.FindAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }
            ViewData["TurmaID"] = new SelectList(_context.Turma, "ID", "NomeTurma", aluno.TurmaID);
            return View(aluno);
        }

        // POST: Alunos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nome,DataNascimento,NomeResponsavel,ContatoResponsavel,Endereco,TurmaID")] Aluno aluno)
        {
            if (id != aluno.ID)
            {
                return NotFound();
            }

                try
                {
                    _context.Update(aluno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlunoExists(aluno.ID))
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

        // GET: Alunos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Aluno == null)
            {
                return NotFound();
            }

            var aluno = await _context.Aluno
                .Include(a => a.Turma)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        // POST: Alunos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Aluno == null)
            {
                return Problem("Entity set 'Pandorga_AdminContext.Aluno'  is null.");
            }
            var aluno = await _context.Aluno.FindAsync(id);
            if (aluno != null)
            {
                _context.Aluno.Remove(aluno);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlunoExists(int id)
        {
          return (_context.Aluno?.Any(e => e.ID == id)).GetValueOrDefault();
        }

        public IActionResult Relatorio()
        {
            var alunos = _context.Aluno.Include(a => a.Turma).ToList();

            ViewData["TotalAlunos"] = alunos.Count();
            ViewData["MediaIdade"] = alunos.Count() > 0 ? (int)Math.Round(alunos.Average(a => (DateTime.Now - a.DataNascimento).Days / 365.25)) : 0;

            return View();
        }


    }
}
