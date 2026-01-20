using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperRH.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        public int idUsuario { get; set; }

        [Required]
        public required string Login { get; set; }

        [Required]
        public required string SenhaHash { get; set; }

        [Required]
        public required string NomeCompleto { get; set; }

        public string? Email { get; set; }

        [Required]
        public required string NivelAcesso { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.Now;

        public bool Status { get; set; } = true;
    }
}
