using ProjectManagement.Interfaces;
using ProjectManagement.Models;
using ProjectManagement.ViewModels.TaskViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagement.Controllers
{
    public class TaskManagementController : Controller
    {
        private readonly ITaskRepository _taskRepository;

        public TaskManagementController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<IActionResult> Index()
        {
            var tasksViewModel = new List<ConsultTaskViewModel>();
            try
            {
                var tasks = await _taskRepository.GetAllTasksActive();

                foreach (var item in tasks)
                {
                    tasksViewModel.Add(
                    new ConsultTaskViewModel
                    {
                        Id = item.Id,
                        IsAtive = item.IsAtive,
                        CreatedDate = item.CreateDate.Value,
                        Description = item.Description,
                        Name = item.Name
                    });
                }
            }
            catch (Exception ex)
            {

                TempData["MessageErro"] = ex.Message;
            }

            return View(tasksViewModel);
        }

        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterTaskViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var task = new TaskManagement()
                    {
                        Name = viewModel.Name,
                        Description = viewModel.Description,
                        CreateDate = DateTime.Now,
                        IsAtive = viewModel.IsAtive
                    };

                    await _taskRepository.RegisterTask(task);
                    TempData["MessageSuccess"] = "Tarefa cadastrada com sucesso!";

                }
                catch (Exception ex)
                {

                    TempData["MessageErro"] = ex.Message;
                }
            }

            return RedirectToAction("Index", "TaskManagement");
        }

        public async Task<IActionResult> Edit(int idTask)
        {
            var taskViewModel = new EditTaskViewModel();
            try
            {
                var task = await _taskRepository.GetTaskById(idTask);

                if ((task != null))
                {
                    taskViewModel.Description = task.Description;
                    taskViewModel.Name = task.Name;
                    taskViewModel.IsAtive = task.IsAtive;
                    taskViewModel.Id = idTask;
                }
            }
            catch (Exception ex)
            {
                TempData["MessageErro"] = ex.Message;
            }

            return View(taskViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTaskViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var task = await _taskRepository.GetTaskById(viewModel.Id);

                    if ((task != null))
                    {
                        task.Description = viewModel.Description;
                        task.Name = viewModel.Name;
                        task.IsAtive = viewModel.IsAtive;

                        await _taskRepository.EditTask(task);
                        TempData["MessageSuccess"] = "Tarefa editada com sucesso!";

                    }
                    else
                    {
                        TempData["MessageWarning"] = "Tarefa não encontrada.";

                    }
                }
                catch (Exception ex)
                {

                    TempData["MessageErro"] = ex.Message;
                }
            }
            return RedirectToAction("Index", "TaskManagement");
        }

        public async Task<IActionResult> Delete(int idTask)
        {
            try
            {
                var task = await _taskRepository.GetTaskById(idTask);

                if (task != null)
                {
                    await _taskRepository.DeleteTask(task);
                }
                else
                {
                    TempData["MessageAlert"] = "Tarefa não encontrada.";

                }
            }
            catch (Exception ex)
            {

                TempData["MessageErro"] = ex.Message;
            }

            return RedirectToAction("ArchivedTasks", "TaskManagement");
        }

        public async Task<IActionResult> ArchivedTasks()
        {
            var tasksViewModel = new List<ConsultTaskViewModel>();

            try
            {
                var tasks = await _taskRepository.GetAllTasksInactive();

                foreach (var item in tasks)
                {
                    tasksViewModel.Add(
                    new ConsultTaskViewModel
                    {
                        Id = item.Id,
                        IsAtive = item.IsAtive,
                        CreatedDate = item.CreateDate.Value,
                        Description = item.Description,
                        Name = item.Name
                    });
                }

            }
            catch (Exception)
            {

                throw;
            }
            return View(tasksViewModel);
        }

        public async Task<IActionResult> ActiveTask(int idTask)
        {
            try
            {
                var task = await _taskRepository.GetTaskById(idTask);

                task.IsAtive = true;

                await _taskRepository.EditTask(task);
                TempData["MessageSuccess"] = $"Tarefa {task.Name} ativada com sucesso!";

            }
            catch (Exception ex)
            {
                TempData["MessageErro"] = ex.Message;
            }
            return RedirectToAction("ArchivedTasks", "TaskManagement");
        }

        public async Task<IActionResult> ArchivedTask(int idTask)
        {
            try
            {
                var task = await _taskRepository.GetTaskById(idTask);

                task.IsAtive = false;

                await _taskRepository.EditTask(task);

                TempData["MessageSuccess"] = $"Tarefa {task.Name} arquivada com sucesso!";

            }
            catch (Exception ex)
            {
                TempData["MessageErro"] = ex.Message;
            }
            return RedirectToAction("Index", "TaskManagement");
        }
    }
}
