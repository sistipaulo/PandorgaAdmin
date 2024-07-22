using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Pandorga_Admin.Models
{
    public class Aluno
    {
        public int ID { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        public string Nome { get; set; }

        [DisplayName("Data de Nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        public DateTime DataNascimento { get; set; }

        [DisplayName("Responsável")]
        [Required(ErrorMessage = "O nome do responsável é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do responsável não pode exceder 100 caracteres.")]
        public string NomeResponsavel { get; set; }

        [DisplayName("Contato")]
        [Required(ErrorMessage = "O contato do responsável é obrigatório.")]
        [RegularExpression(@"\(\d{2}\)\d{5}-\d{4}", ErrorMessage = "O telefone deve estar no formato (51)99999-9999.")]
        public string ContatoResponsavel { get; set; }

        [DisplayName("Endereço")]
        [Required(ErrorMessage = "O endereço do responsável é obrigatório.")]
        [StringLength(200, ErrorMessage = "O endereço não pode exceder 200 caracteres.")]
        public string Endereco { get; set; }

        // Chave Estrangeira
        [DisplayName("Turma")]
        public int? TurmaID { get; set; }

        // Propriedade de Navegação
        public Turma Turma { get; set; }
    }
}
