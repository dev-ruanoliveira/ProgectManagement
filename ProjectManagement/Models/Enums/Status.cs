using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Models.Enums
{
    public enum Status
    {
        [Display(Name = "Pendente")]
        Pending = 1,

        [Display(Name = "Em Progresso")]
        InProgress = 2,

        [Display(Name = "Finalizado")]
        Finished = 3
    }
}
