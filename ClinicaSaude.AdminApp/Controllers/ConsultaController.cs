using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClinicaSaude.Shared.Data;
using ClinicaSaude.Shared.Models;

namespace ClinicaSaude.AdminApp.Controllers
{
    public class ConsultaController : Controller
    {
        private readonly ClinicaSaudeContext _context;

        public ConsultaController(ClinicaSaudeContext context)
        {
            _context = context;
        }

        // GET: Consulta
        public async Task<IActionResult> Index()
        {
            var consultas = await _context.Consultas
                .Include(c => c.Paciente)
                .ToListAsync();
            return View(consultas);
        }

        // GET: Consulta/Create
        public IActionResult Create()
        {
            ViewBag.PacienteId = new SelectList(_context.Pacientes, "PacienteId", "Nome");
            return View();
        }

        // POST: Consulta/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConsultaId,Descricao,Data,PacienteId")] Consulta consulta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consulta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.PacienteId = new SelectList(_context.Pacientes, "PacienteId", "Nome", consulta.PacienteId);
            return View(consulta);
        }

        // GET: Consulta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var consulta = await _context.Consultas.FindAsync(id);
            if (consulta == null) return NotFound();

            ViewBag.PacienteId = new SelectList(_context.Pacientes, "PacienteId", "Nome", consulta.PacienteId);
            return View(consulta);
        }

        // POST: Consulta/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConsultaId,Descricao,Data,PacienteId")] Consulta consulta)
        {
            if (id != consulta.ConsultaId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consulta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Consultas.Any(e => e.ConsultaId == consulta.ConsultaId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.PacienteId = new SelectList(_context.Pacientes, "PacienteId", "Nome", consulta.PacienteId);
            return View(consulta);
        }

        // GET: Consulta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var consulta = await _context.Consultas
                .Include(c => c.Paciente)
                .FirstOrDefaultAsync(m => m.ConsultaId == id);

            if (consulta == null) return NotFound();

            return View(consulta);
        }

        // POST: Consulta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consulta = await _context.Consultas.FindAsync(id);
            if (consulta != null)
            {
                _context.Consultas.Remove(consulta);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
