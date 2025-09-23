namespace ClinicaSaude.Shared.Models
{
    using System.ComponentModel.DataAnnotations;
    using System;

    public class Consulta
    {
        [Key]
        public int ConsultaId { get; set; }

        public DateTime Data { get; set; }

        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; } = null!;
    }
}
