namespace ClinicaSaude.Shared.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Paciente
    {
        [Key]
        public int PacienteId { get; set; }

        [Required, StringLength(100)]
        public required string Nome { get; set; }

        [Required, StringLength(100)]
        public required string CPF { get; set; }

        [StringLength(15)]
        public string? Telefone { get; set; }

        public string? UserId { get; set; }

        public ICollection<Consulta> Consultas { get; set; } = new List<Consulta>();
    }
}
