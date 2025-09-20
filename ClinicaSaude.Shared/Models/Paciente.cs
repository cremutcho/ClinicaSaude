using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClinicaSaude.Shared.Models
{
    public class Paciente
    {
        [Key]
        public int PacienteId { get; set; }

        [Required]
        [StringLength(100)]
        public required string Nome { get; set; }  // obrigatório

        [Required]
        [StringLength(100)]
        public required string CPF { get; set; }   // obrigatório

        [StringLength(15)]
        public string? Telefone { get; set; }     // opcional

        public ICollection<Consulta> Consultas { get; set; } = new List<Consulta>(); // inicializada
    }
}
