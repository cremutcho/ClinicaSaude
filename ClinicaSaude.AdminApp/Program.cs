using ClinicaSaude.Shared.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços do MVC
builder.Services.AddControllersWithViews();

// 🔹 Configuração do DbContext
builder.Services.AddDbContext<ClinicaSaudeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ClinicaSaudeConnection")));

var app = builder.Build();

// Pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Rota padrão
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
