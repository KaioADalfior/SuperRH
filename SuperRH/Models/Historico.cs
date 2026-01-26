using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperRH.Models
{
    [Table("Historico")]
    public class Historico
    {
        [Key]
        public int idHistorico { get; set; }

        [Required]
        public int idColaborador { get; set; }

        [Required(ErrorMessage = "A categoria é obrigatória.")]
        [StringLength(50)]
        public string Categoria { get; set; } = string.Empty;

        [Required(ErrorMessage = "O título da ocorrência é obrigatório.")]
        [StringLength(100)]
        [Display(Name = "Ocorrência")]
        public string Titulo { get; set; } = string.Empty;

        [DataType(DataType.MultilineText)]
        public string? Descricao { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataEvento { get; set; } = DateTime.Now;

        public DateTime DataRegistro { get; set; } = DateTime.Now;

        public string? UsuarioResponsavel { get; set; }

        // 🔗 RELACIONAMENTO CORRETO
        [ForeignKey(nameof(idColaborador))]
        public virtual Colaborador? Colaborador { get; set; }
    }
}
