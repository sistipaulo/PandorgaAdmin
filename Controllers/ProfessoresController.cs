using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pandorga_Admin.Data;
using Pandorga_Admin.Models;
using static Pandorga_Admin.Models.Professor;

namespace Pandorga_Admin.Controllers
{
    public class ProfessoresController : Controller
    {
        private readonly Pandorga_AdminContext _context;

        public ProfessoresController(Pandorga_AdminContext context)
        {
            _context = context;
        }

        // GET: Professores
        public async Task<IActionResult> Index()
        {
              return _context.Professor != null ? 
                          View(await _context.Professor.ToListAsync()) :
                          Problem("Entity set 'Pandorga_AdminContext.Professor'  is null.");
        }

        // GET: Professores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Professor == null)
            {
                return NotFound();
            }

            var professor = await _context.Professor
                .FirstOrDefaultAsync(m => m.ID == id);
            if (professor == null)
            {
                return NotFound();
            }

            return View(professor);
        }

        // GET: Professores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Professores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nome,Email,Telefone,Especializacao")] Professor professor)
        {

                _context.Add(professor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }

        // GET: Professores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Professor == null)
            {
                return NotFound();
            }

            var professor = await _context.Professor.FindAsync(id);
            if (professor == null)
            {
                return NotFound();
            }
            return View(professor);
        }

        // POST: Professores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nome,Email,Telefone,Especializacao")] Professor professor)
        {
            if (id != professor.ID)
            {
                return NotFound();
            }


                try
                {
                    _context.Update(professor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfessorExists(professor.ID))
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

        // GET: Professores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Professor == null)
            {
                return NotFound();
            }

            var professor = await _context.Professor
                .FirstOrDefaultAsync(m => m.ID == id);
            if (professor == null)
            {
                return NotFound();
            }

            return View(professor);
        }

        // POST: Professores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Professor == null)
            {
                return Problem("Entity set 'Pandorga_AdminContext.Professor'  is null.");
            }
            var professor = await _context.Professor.FindAsync(id);
            if (professor != null)
            {
                _context.Professor.Remove(professor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfessorExists(int id)
        {
          return (_context.Professor?.Any(e => e.ID == id)).GetValueOrDefault();
        }

        public IActionResult Relatorio()
        {
            var professores = _context.Professor.ToList();

            ViewData["TotalProfessores"] = professores.Count();


            var cargosFuncionarios = new Dictionary<string, int>
            {
                { "Professores", professores.Count(p => p.Especializacao == CargoEnum.Professor) },
                { "Diretores", professores.Count(p => p.Especializacao == CargoEnum.Diretor) },
                { "Coordenadores", professores.Count(p => p.Especializacao == CargoEnum.Coordenador) },
                { "Assistentes", professores.Count(p => p.Especializacao == CargoEnum.Assistente) },
                { "Secretária", professores.Count(p => p.Especializacao == CargoEnum.Secretario) }
            };

            ViewData["Cargos"] = cargosFuncionarios;

            return View(professores);
        }

    }
}
