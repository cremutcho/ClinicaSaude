using ClinicaSaude.Shared.Data;
using ClinicaSaude.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class PacienteController : Controller
{
    private readonly ClinicaSaudeContext _context;

    public PacienteController(ClinicaSaudeContext context)
    {
        _context = context;
    }

    // GET: Paciente
    public async Task<IActionResult> Index()
    {
        var pacientes = await _context.Pacientes.ToListAsync();
        return View(pacientes);
    }

    // GET: Paciente/Create
    public IActionResult Create() => View();

    // POST: Paciente/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Paciente paciente)
    {
        if (ModelState.IsValid)
        {
            _context.Add(paciente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(paciente);
    }

    // GET: Paciente/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var paciente = await _context.Pacientes.FindAsync(id);
        if (paciente == null) return NotFound();
        return View(paciente);
    }

    // POST: Paciente/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Paciente paciente)
    {
        if (id != paciente.PacienteId) return NotFound();
        if (ModelState.IsValid)
        {
            _context.Update(paciente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(paciente);
    }

    // GET: Paciente/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var paciente = await _context.Pacientes.FindAsync(id);
        if (paciente == null) return NotFound();
        return View(paciente);
    }

    // POST: Paciente/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var paciente = await _context.Pacientes.FindAsync(id);
        if (paciente != null)
        {
            _context.Pacientes.Remove(paciente);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
