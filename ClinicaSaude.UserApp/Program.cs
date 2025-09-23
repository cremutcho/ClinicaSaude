using ClinicaSaude.Shared.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os serviços MVC
builder.Services.AddControllersWithViews();

// Configura o DbContext com SQL Server
builder.Services.AddDbContext<ClinicaSaudeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ClinicaSaudeConnection")));

// Configura o Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ClinicaSaudeContext>()
    .AddDefaultTokenProviders();

// Configuração do cookie de login
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";              // página de login
    options.AccessDeniedPath = "/Account/AccessDenied"; // página de acesso negado
});

var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication(); // Importante: antes de UseAuthorization
app.UseAuthorization();

// Rotas para Areas
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

// Rota padrão
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Inicializa roles automaticamente
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DbInitializer.SeedRolesAsync(services);
}

app.Run();
