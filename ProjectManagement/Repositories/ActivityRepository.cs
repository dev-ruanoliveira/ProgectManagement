
using ProjectManagement.Models.Enums;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Data;
using ProjectManagement.Interfaces;
using ProjectManagement.Models;

namespace ProjectManagement.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly DataContext _context;

        public ActivityRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Activity>> GetAllActivities()
        {
             return await _context.Activities.ToListAsync();
        }

        public async Task<Activity> GetActivitieById(int id)
        {
            return await _context.Activities.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Activity>> GetActivitiesByStatus(int id)
        {
            return await _context.Activities.Where(x => x.Status == (Status)id).ToListAsync();
        }

        public async Task RegisterActivity(Activity model)
        {
            _context.Activities.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteActivity(Activity model)
        {
            _context.Remove(model);
            await _context.SaveChangesAsync();

        }

        public async Task MigrateActivity(int id)
        {
            var activity = _context.Activities.FirstOrDefault(x => x.Id == id);
            var status = activity.Status == Status.Pending ? Status.InProgress : Status.Finished;

            activity.Status = status;

            _context.Update(activity);
            await _context.SaveChangesAsync();

        }

        public async Task<List<Activity>> GetActivitiesByTask(int id)
        {
            return await _context.Activities.Include(x => x.Task)
                .Where(x => x.TaskId == id && x.Task.IsAtive == true).ToListAsync();
        }
    }
}
