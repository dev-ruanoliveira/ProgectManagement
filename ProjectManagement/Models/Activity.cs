using ProjectManagement.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public int Registration { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório.")]
        [StringLength(100, ErrorMessage = "É permitido no máximo {1} caracteres.")]
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public Status Status { get; set; }
        public int TaskId { get; set; }

        public TaskManagement? Task { get; set; }
    }
}
