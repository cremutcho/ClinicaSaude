using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ClinicaSaude.Shared.Models;
using ClinicaSaude.Shared.Data;

[Area("User")]
public class ConsultaController : Controller
{
    private readonly ClinicaSaudeContext _context; 
    private readonly UserManager<IdentityUser> _userManager;

    public ConsultaController(ClinicaSaudeContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // GET: User/Consulta
    public async Task<IActionResult> Index()
    {
        var userId = _userManager.GetUserId(User);
        var paciente = await _context.Pacientes
            .Include(p => p.Consultas)
            .FirstOrDefaultAsync(p => p.UserId == userId);

        if (paciente == null)
        {
            return RedirectToAction("Register", "Account");
        }

        return View(paciente);
    }

    // GET: User/Consulta/Create
    public IActionResult Create()
    {
        var consulta = new Consulta
        {
            Data = DateTime.Now // valor padrão inicial
        };
        return View(consulta);
    }

    // POST: User/Consulta/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Consulta consulta)
    {
        if (!ModelState.IsValid)
        {
            return View(consulta);
        }

        var userId = _userManager.GetUserId(User);
        var paciente = await _context.Pacientes.FirstOrDefaultAsync(p => p.UserId == userId);

        if (paciente == null)
        {
            return RedirectToAction("Register", "Account");
        }

        consulta.PacienteId = paciente.PacienteId;

        try
        {
            _context.Consultas.Add(consulta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            // Adiciona a mensagem de erro no ModelState para aparecer na view
            ModelState.AddModelError(string.Empty, "Erro ao salvar a consulta: " + ex.Message);
            return View(consulta);
        }
    }
}
