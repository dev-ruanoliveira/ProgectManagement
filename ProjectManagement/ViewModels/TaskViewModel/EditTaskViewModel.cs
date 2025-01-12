namespace ProjectManagement.ViewModels.TaskViewModel
{
    public class EditTaskViewModel
    {
        public EditTaskViewModel()
        {
            IsAtive = true;
        }
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsAtive { get; set; }
    }
}
