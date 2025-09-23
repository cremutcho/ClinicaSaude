using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ClinicaSaude.Shared.Models;

namespace ClinicaSaude.Shared.Data
{
    public class ClinicaSaudeContext : IdentityDbContext
    {
        public ClinicaSaudeContext(DbContextOptions<ClinicaSaudeContext> options)
            : base(options) { }

        public DbSet<Paciente> Pacientes { get; set; } = null!;
        public DbSet<Consulta> Consultas { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Consulta>()
                .HasOne(c => c.Paciente)
                .WithMany(p => p.Consultas)
                .HasForeignKey(c => c.PacienteId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
