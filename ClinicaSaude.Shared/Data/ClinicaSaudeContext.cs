using ClinicaSaude.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicaSaude.Shared.Data
{
    public class ClinicaSaudeContext : DbContext
    {
        public ClinicaSaudeContext(DbContextOptions<ClinicaSaudeContext> options)
            : base(options) { }

        // Inicialização para evitar warnings de null
        public DbSet<Paciente> Pacientes { get; set; } = null!;
        public DbSet<Consulta> Consultas { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração do relacionamento 1:N entre Paciente e Consulta
            modelBuilder.Entity<Consulta>()
                .HasOne(c => c.Paciente)
                .WithMany(p => p.Consultas)
                .HasForeignKey(c => c.PacienteId)
                .OnDelete(DeleteBehavior.Cascade); // Deletar consultas ao remover paciente
        }
    }
}
