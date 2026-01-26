using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperRH.Models
{
    [Table("Colaboradores")]
    public class Colaborador
    {
        [Key]
        public int idColaborador { get; set; }

        [Required(ErrorMessage = "O nome completo é obrigatório.")]
        [StringLength(150)]
        [Display(Name = "Nome Completo")]
        public string NomeCompleto { get; set; } = null!;

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve ter 11 dígitos.")]
        [Display(Name = "CPF")]
        public string CPF { get; set; } = null!;

        [Required(ErrorMessage = "O RG é obrigatório.")]
        [StringLength(20)]
        [Display(Name = "RG")]
        public string RG { get; set; } = null!;

        [StringLength(20)]
        [Display(Name = "Órgão Emissor")]
        public string? OrgaoEmissor { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O sexo é obrigatório.")]
        [StringLength(1)]
        [Display(Name = "Sexo")]
        public string Sexo { get; set; } = null!; // M, F, O

        [StringLength(20)]
        [Display(Name = "Estado Civil")]
        public string? EstadoCivil { get; set; }

        [StringLength(50)]
        public string Nacionalidade { get; set; } = "Brasileira";

        [StringLength(50)]
        public string? Naturalidade { get; set; }

        [StringLength(150)]
        [Display(Name = "Nome do Pai")]
        public string? NomePai { get; set; }

        [Required(ErrorMessage = "O nome da mãe é obrigatório.")]
        [StringLength(150)]
        [Display(Name = "Nome da Mãe")]
        public string NomeMae { get; set; } = null!;

        // 🔗 CHAVE ESTRANGEIRA - CARGO
        [Required]
        [Display(Name = "Cargo")]
        public int idCargo { get; set; }

        [ForeignKey(nameof(idCargo))]
        public Cargo? Cargo { get; set; }

        [Display(Name = "Data de Cadastro")]
        public DateTime DataCadastro { get; set; } = DateTime.Now;

        public bool Status { get; set; } = true;
    }
}
