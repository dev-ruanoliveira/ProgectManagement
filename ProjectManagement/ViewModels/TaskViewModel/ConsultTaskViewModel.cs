namespace ProjectManagement.ViewModels.TaskViewModel
{
    public class ConsultTaskViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsAtive { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
