using ProjectManagement.Interfaces;
using ProjectManagement.Models;
using ProjectManagement.ViewModels.ActivityViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Threading.Tasks;
using ProjectManagement.Models.Enums;

namespace ProjectManagement.Controllers
{
    public class ActivityController : Controller
    {
        private readonly IActivityRepository _activityRepository;

        public ActivityController(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public async Task<IActionResult> Index(int idTask)
        {
            var listViewModel = new List<ConsultActivityViewModel>();
            try
            {
                var activity = await _activityRepository.GetActivitiesByTask(idTask);
                foreach (var item in activity)
                {
                    listViewModel.Add(
                        new ConsultActivityViewModel()
                        {
                            Title = item.Title,
                            CreatedDate = item.CreatedDate,
                            Description = item.Description,
                            Registration = item.Registration,
                            StatusId = (int)item.Status,
                            Id = item.Id,
                            TaskId = item.TaskId
                        }
                    );


                }

                ViewBag.TaskId = idTask;


            }
            catch (Exception ex)
            {

                throw;
            }
            return View(listViewModel);
        }

        public IActionResult Register(int idTask)
        {
            ViewBag.Status = Enum.GetValues(typeof(Status))
                .Cast<Status>()
                .Select(e => new SelectListItem
                {
                    Value = ((int)e).ToString(),
                    Text = (int)e == 1 ? "Pendente" : (int)e == 2 ? "Em Andamento" : "Finalizado"
                }).ToList();

            return View(new RegisterActivityViewModel() { TaskId = idTask });
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterActivityViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var activity = new Activity()
                    {
                        Description = viewModel.Description,
                        Registration = new Random().Next(1000, 1000000),
                        Title = viewModel.Title,
                        Status = (Status)viewModel.StatusId,
                        CreatedDate = DateTime.Now,
                        TaskId = viewModel.TaskId
                    };

                    await _activityRepository.RegisterActivity(activity);
                    ModelState.Clear();

                    TempData["MessageSuccess"] = "Atividade cadastrada com sucesso!";
                }
                catch (Exception ex)
                {
                    TempData["MessageErro"] = ex.Message;

                }
            }

            ViewBag.Status = Enum.GetValues(typeof(Status))
                .Cast<Status>()
                .Select(e => new SelectListItem
                {
                    Value = ((int)e).ToString(),
                    Text = (int)e == 1 ? "Pendente" : (int)e == 2 ? "Em Andamento" : "Finalizado"
                }).ToList();

            return RedirectToAction("Index", "Activity", new { idTask = viewModel.TaskId });
        }

        public async Task<IActionResult> ChangeCard(int idActivity)
        {
            int taskId = 0;
            try
            {
                var activity = await _activityRepository.GetActivitieById(idActivity);
                taskId = activity.TaskId;

                await _activityRepository.MigrateActivity(idActivity);

            }
            catch (Exception ex)
            {
                TempData["MessageErro"] = ex.Message;
            }

            return RedirectToAction("Index", "Activity", new { idTask = taskId });
        }

        public async Task<IActionResult> Delete(int idActivity)
        {
            int taskId = 0;
            try
            {
                var activity = await _activityRepository.GetActivitieById(idActivity);
                taskId = activity.TaskId;

                await _activityRepository.DeleteActivity(activity);

                TempData["MessageSuccess"] = "Atividade excluída com sucesso!";

            }
            catch (Exception ex)
            {

                TempData["MessageErro"] = ex.Message;
            }

            return RedirectToAction("Index", "Activity", new { idTask = taskId });
        }

    }
}
