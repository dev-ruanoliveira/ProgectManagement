namespace ProjectManagement.ViewModels.ActivityViewModel
{
    public class ConsultActivityViewModel
    {
        public int Id { get; set; }
        public int Registration { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; } 
        public int StatusId { get; set; }
        public int TaskId { get; set; }

    }
}
