namespace ProjectManagement.ViewModels.TaskViewModel
{
    public class RegisterTaskViewModel
    {
        public RegisterTaskViewModel()
        {
            IsAtive = true;
        }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsAtive { get; set; }
    }
}
