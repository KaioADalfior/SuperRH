using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperRH.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        public int idUsuario { get; set; }

        [Required(ErrorMessage = "Login obrigatório")]
        [StringLength(50)]
        public string Login { get; set; } = string.Empty;

        [Required(ErrorMessage = "Senha obrigatória")]
        public string SenhaHash { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nome obrigatório")]
        [StringLength(100)]
        public string NomeCompleto { get; set; } = string.Empty;

        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [StringLength(20)]
        public string NivelAcesso { get; set; } = string.Empty;

        /* =========================
           🔗 VÍNCULO COM COLABORADOR
        ========================== */
        [Required]
        public int idColaborador { get; set; }

        [ForeignKey(nameof(idColaborador))]
        public virtual Colaborador? Colaborador { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.Now;

        public bool Status { get; set; } = true;
    }
}
