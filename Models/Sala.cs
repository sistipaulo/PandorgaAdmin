using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Pandorga_Admin.Models
{
    public class Sala
    {
        public int ID { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        public string Nome { get; set; }

        [DisplayName("Capacidade")]
        [Required(ErrorMessage = "A capacidade é obrigatória.")]
        [Range(1, int.MaxValue, ErrorMessage = "A capacidade deve ser um número positivo.")]
        public int Capacidade { get; set; }

        // Chave Estrangeira
        [DisplayName("Turma")]
        public int? TurmaID { get; set; }

        // Propriedade de Navegação
        public Turma Turma { get; set; }
    }
}
