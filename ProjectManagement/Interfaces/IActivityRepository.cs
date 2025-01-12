using ProjectManagement.Models;

namespace ProjectManagement.Interfaces
{
    public interface IActivityRepository
    {
        Task<List<Activity>> GetAllActivities();
        Task<Activity> GetActivitieById(int id);
        Task<List<Activity>> GetActivitiesByStatus(int id);
        Task<List<Activity>> GetActivitiesByTask(int id);
        Task RegisterActivity(Activity dto);
        Task DeleteActivity(Activity model);
        Task MigrateActivity(int id);
    }
}
