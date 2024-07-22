using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Pandorga_Admin.Models
{
    public class Turma
    {
        public int ID { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "O nome da turma é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome da turma não pode exceder 100 caracteres.")]
        public string NomeTurma { get; set; }

        [DisplayName("Ano")]
        [Required(ErrorMessage = "O ano é obrigatório.")]
        [Range(2018, 2024, ErrorMessage = "O ano deve estar entre 2018 e 2024.")]
        public int Ano { get; set; }

        [DisplayName("Turno")]
        [Required(ErrorMessage = "O turno é obrigatório.")]
        [StringLength(50, ErrorMessage = "O turno não pode exceder 50 caracteres.")]
        public string Turno { get; set; }

        // Chave Estrangeira
        [DisplayName("Professor")]
        [Required(ErrorMessage = "O professor é obrigatório.")]
        public int ProfessorID { get; set; }

        // Propriedades de Navegação
        public Professor Professor { get; set; }
        public ICollection<Aluno> Alunos { get; set; }
        public Sala Sala { get; set; }
        public ICollection<Evento> Eventos { get; set; }
    }
}
