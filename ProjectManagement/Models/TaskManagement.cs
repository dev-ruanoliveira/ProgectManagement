namespace ProjectManagement.Models
{
    public class TaskManagement
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool IsAtive { get; set; }

        public List<Activity> Activities { get; set; }
    }
}
