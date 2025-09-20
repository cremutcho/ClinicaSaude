using System;
using System.ComponentModel.DataAnnotations;
using ClinicaSaude.Shared.Models; // necessário se houver referências internas
using System.Collections.Generic;

namespace ClinicaSaude.Shared.Models
{
    public class Consulta
    {
        [Key]
        public int ConsultaId { get; set; }

        [Required]
        public required string Especialidade { get; set; } // obrigatório

        [Required]
        public DateTime DataHora { get; set; }

        // Chave estrangeira
        [Required]
        public int PacienteId { get; set; }

        // Navegação
        public required Paciente Paciente { get; set; }
    }
}
