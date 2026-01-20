using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperRH.Models
{
    [Table("Funcionarios")]
    public class Funcionario
    {
        [Key]
        public int idFuncionario { get; set; }

        [Required(ErrorMessage = "O nome completo é obrigatório.")]
        [StringLength(150)]
        [Display(Name = "Nome Completo")]
        public required string NomeCompleto { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve ter 11 dígitos.")]
        [Display(Name = "CPF")]
        public required string CPF { get; set; }

        [Required(ErrorMessage = "O RG é obrigatório.")]
        [StringLength(20)]
        [Display(Name = "RG")]
        public required string RG { get; set; }

        [StringLength(20)]
        [Display(Name = "Órgão Emissor")]
        public string? OrgaoEmissor { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O gênero é obrigatório.")]
        [StringLength(1)]
        public required string Sexo { get; set; } // M, F, O

        [StringLength(20)]
        [Display(Name = "Estado Civil")]
        public string? EstadoCivil { get; set; }

        [StringLength(50)]
        public string? Nacionalidade { get; set; } = "Brasileira";

        [StringLength(50)]
        public string? Naturalidade { get; set; }

        [StringLength(150)]
        [Display(Name = "Nome do Pai")]
        public string? NomePai { get; set; }

        [Required(ErrorMessage = "O nome da mãe é obrigatório.")]
        [StringLength(150)]
        [Display(Name = "Nome da Mãe")]
        public required string NomeMae { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.Now;

        public bool Status { get; set; } = true;
    }
}