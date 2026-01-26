using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperRH.Models
{
    [Table("Cargos")]
    public class Cargo
    {
        [Key]
        public int idCargo { get; set; }

        [Required]
        public string NomeCargo { get; set; } = null!;

        public string Area { get; set; } = null!;

        public string? Nivel { get; set; }

        public bool Status { get; set; } = true;
    }
}
