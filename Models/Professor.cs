using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Pandorga_Admin.Models
{
    public class Professor
    {
        public int ID { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        public string Nome { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email não é válido.")]
        [StringLength(100, ErrorMessage = "O email não pode exceder 100 caracteres.")]
        public string Email { get; set; }

        [DisplayName("Telefone")]
        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [RegularExpression(@"\(\d{2}\)\d{5}-\d{4}", ErrorMessage = "O telefone deve estar no formato (51)99999-9999.")]
        public string Telefone { get; set; }

        [DisplayName("Cargo")]
        [Required(ErrorMessage = "o campo cargo é obrigatório.")]
        public CargoEnum Especializacao { get; set; }

        // Propriedade de Navegação
        public ICollection<Turma> Turmas { get; set; }

        public enum CargoEnum
        {
            [Display(Name = "Professor")]
            Professor,

            [Display(Name = "Coordenador")]
            Coordenador,

            [Display(Name = "Diretor")]
            Diretor,

            [Display(Name = "Assistente")]
            Assistente,

            [Display(Name = "Secretária")]
            Secretario
        }
    }
}
