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
    public class EventosController : Controller
    {
        private readonly Pandorga_AdminContext _context;

        public EventosController(Pandorga_AdminContext context)
        {
            _context = context;
        }

        // GET: Eventos
        public async Task<IActionResult> Index()
        {
            var pandorga_AdminContext = _context.Evento.Include(e => e.Sala).Include(e => e.Turma);
            return View(await pandorga_AdminContext.ToListAsync());
        }

        // GET: Eventos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Evento == null)
            {
                return NotFound();
            }

            var evento = await _context.Evento
                .Include(e => e.Sala)
                .Include(e => e.Turma)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // GET: Eventos/Create
        public IActionResult Create()
        {
            ViewData["SalaID"] = new SelectList(_context.Sala, "ID", "Nome");
            ViewData["TurmaID"] = new SelectList(_context.Turma, "ID", "NomeTurma");
            return View();
        }

        // POST: Eventos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,NomeEvento,Data,Descricao,Local,TurmaID,SalaID")] Evento evento)
        {

                _context.Add(evento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

        }

        // GET: Eventos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Evento == null)
            {
                return NotFound();
            }

            var evento = await _context.Evento.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }
            ViewData["SalaID"] = new SelectList(_context.Sala, "ID", "Nome", evento.SalaID);
            ViewData["TurmaID"] = new SelectList(_context.Turma, "ID", "NomeTurma", evento.TurmaID);
            return View(evento);
        }

        // POST: Eventos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,NomeEvento,Data,Descricao,Local,TurmaID,SalaID")] Evento evento)
        {
            if (id != evento.ID)
            {
                return NotFound();
            }


                try
                {
                    _context.Update(evento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventoExists(evento.ID))
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

        // GET: Eventos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Evento == null)
            {
                return NotFound();
            }

            var evento = await _context.Evento
                .Include(e => e.Sala)
                .Include(e => e.Turma)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // POST: Eventos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Evento == null)
            {
                return Problem("Entity set 'Pandorga_AdminContext.Evento'  is null.");
            }
            var evento = await _context.Evento.FindAsync(id);
            if (evento != null)
            {
                _context.Evento.Remove(evento);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventoExists(int id)
        {
          return (_context.Evento?.Any(e => e.ID == id)).GetValueOrDefault();
        }

        public IActionResult Relatorio()
        {
            var eventos = _context.Evento.Include(e => e.Turma).Include(e => e.Sala).ToList();

            ViewData["TotalEventos"] = eventos.Count();

            var eventosPorTurma = eventos.Where(e => e.TurmaID.HasValue)
                                          .GroupBy(e => e.Turma.NomeTurma)
                                          .ToDictionary(g => g.Key, g => g.Count());
            ViewData["EventosPorTurma"] = eventosPorTurma;

            var eventosPorSala = eventos.Where(e => e.SalaID.HasValue)
                                         .GroupBy(e => e.Sala.Nome)
                                         .ToDictionary(g => g.Key, g => g.Count());
            ViewData["EventosPorSala"] = eventosPorSala;

            var proximosEventos = eventos.Where(e => e.Data >= DateTime.Now)
                                         .OrderBy(e => e.Data)
                                         .Take(5)
                                         .ToList();
            ViewData["ProximosEventos"] = proximosEventos;

            return View(eventos);
        }

    }
}
