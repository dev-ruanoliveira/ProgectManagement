using ProjectManagement.Models;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Data;
using ProjectManagement.Interfaces;

namespace ProjectManagement.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DataContext _context;

        public TaskRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<TaskManagement>> GetAllTasksActive()
        {
            return await _context.Tasks.Where(x => x.IsAtive == true).ToListAsync();
        }

        public async Task<TaskManagement> GetTaskById(int id)
        {
            return await _context.Tasks.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<TaskManagement>> GetAllTasksInactive()
        {
            return await _context.Tasks.Where(x => x.IsAtive == false).ToListAsync();
        }

        public async Task RegisterTask(TaskManagement model)
        {
             _context.Tasks.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task EditTask(TaskManagement model)
        {
            _context.Tasks.Update(model);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTask(TaskManagement model)
        {
            _context.Tasks.Remove(model);
            await _context.SaveChangesAsync();
        }


    }
}
