using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Pandorga_Admin.Models
{
    public class Evento
    {
        public int ID { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "O nome do evento é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do evento não pode exceder 100 caracteres.")]
        public string NomeEvento { get; set; }

        [DisplayName("Data")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "A data do evento é obrigatória.")]
        public DateTime Data { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "A descrição do evento é obrigatória.")]
        [StringLength(500, ErrorMessage = "A descrição não pode exceder 500 caracteres.")]
        public string Descricao { get; set; }

        [DisplayName("Endereço")]
        [Required(ErrorMessage = "O endereço do evento é obrigatório.")]
        [StringLength(200, ErrorMessage = "O endereço não pode exceder 200 caracteres.")]
        public string Local { get; set; }

        // Chaves Estrangeiras
        [DisplayName("Turma")]
        public int? TurmaID { get; set; }

        [DisplayName("Sala")]
        public int? SalaID { get; set; }

        // Propriedades de Navegação
        public Turma Turma { get; set; }
        public Sala Sala { get; set; }
    }
}
