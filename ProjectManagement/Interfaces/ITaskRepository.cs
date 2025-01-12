using ProjectManagement.Models;

namespace ProjectManagement.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<TaskManagement>> GetAllTasksActive();
        Task<List<TaskManagement>> GetAllTasksInactive();
        Task<TaskManagement> GetTaskById(int id);
        Task RegisterTask(TaskManagement model);
        Task EditTask(TaskManagement model);
        Task DeleteTask(TaskManagement model);
    }
}
