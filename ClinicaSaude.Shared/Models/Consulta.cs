using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaSaude.Shared.Models
{
    public class Consulta
    {
        [Key]
        public int ConsultaId { get; set; }

        [Required]
        [StringLength(200)]
        [Column("Especialidade")] // corresponde à coluna do banco
        public string Descricao { get; set; } = string.Empty;

        [Required]
        [Column("DataHora")] // corresponde à coluna do banco
        public DateTime Data { get; set; }

        [Required]
        public int PacienteId { get; set; }

        public Paciente? Paciente { get; set; }
    }
}
