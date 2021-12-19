using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Thread = System.Threading.Thread;
using System.Web;
using System.Web.Mvc;

using Ninject;
using Ninject.Modules;
using AutoMapper;

using PagedList;
using PagedList.Mvc;

using PlannerTasks.BLL.Services;
using PlannerTasks.BLL.Interfaces;
using PlannerTasks.BLL.DTO;
using PlannerTasks.BLL.Infrastructure;
using PlannerTasks.WEB_MVC.Models;
using PlannerTasks.WEB_MVC.Util;
using PlannerTasks.WEB_MVC.App_Start;
using System.Configuration;

namespace PlannerTasks.WEB_MVC.Controllers
{
    public class HomeController: Controller
    {
        ITaskService taskService { get; set; }
        IEmployeeService employeeService { get; set; }
        IStatusHistoryService statusHistoryService { get; set; }
        /*IMapper mapperToTaskDTO { get; set; }
        IMapper mapperToTaskViewModel { get; set; }
        IMapper mapperToStatusHistoryViewModel { get; set; }
        IMapper mapperToEmployeeViewModel { get; set; }*/
        IMapper imapper { get; set; }
        public HomeController()//(ITaskService taskS, IEmployeeService employeeS, IStatusHistoryService statusHistoryS)
        {
            imapper = AutoMapperConfiguration.GetMapper();
            var modules = new INinjectModule[] { new ServiceModule("PlannerDB") };
            var kernel = new StandardKernel(modules);

            kernel.Bind<ITaskService>().To<TaskService>();
            kernel.Bind<IEmployeeService>().To<EmployeeService>();
            kernel.Bind<IStatusHistoryService>().To<StatusHistoryService>();

            taskService = kernel.Get<ITaskService>();
            employeeService = kernel.Get<IEmployeeService>();
            statusHistoryService = kernel.Get<IStatusHistoryService>();

            /*taskService = taskS;
            employeeService = employeeS;
            statusHistoryService = statusHistoryS;
            imapper = AutoMapperConfiguration.GetMapper();*/
            /*
            var modules = new INinjectModule[] { new ServiceModule("PlannerDB"), new TaskModule(), new EmployeeModule(), new StatusHistoryModule() };
            var kernel = new StandardKernel(modules);
            taskService = kernel.Get<ITaskService>();
            employeeService = kernel.Get<IEmployeeService>();
            statusHistoryService = kernel.Get<IStatusHistoryService>();

            
            MapperConfiguration configToTaskDTO = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TaskViewModel, TaskDTO>()
                .ForMember("TimeExecution", te => te.MapFrom(c => new TimeSpan(c.TimeExecution, 0, 0)));
            });
            mapperToTaskDTO = configToTaskDTO.CreateMapper();

            MapperConfiguration configToTaskViewModel = new MapperConfiguration(cfg => {
                cfg.CreateMap<TaskDTO, TaskViewModel>()
                .ForMember("TimeExecution", te => te.MapFrom(c => c.TimeExecution.Hours));
            });
            mapperToTaskViewModel = configToTaskViewModel.CreateMapper();

            MapperConfiguration configToStatusHitoryViewModel = new MapperConfiguration(cfg => {
                cfg.CreateMap<StatusHistoryDTO, StatusHistoryViewModel>();
            });
            mapperToStatusHistoryViewModel = configToStatusHitoryViewModel.CreateMapper();

            MapperConfiguration configToEmployeeViewModel = new MapperConfiguration(cfg => {
                cfg.CreateMap<EmployeeDTO, EmployeeViewModel>();
            });
            mapperToEmployeeViewModel = configToEmployeeViewModel.CreateMapper();*/
        }

        // /Home/Index
        public ActionResult Index()
        {
            IEnumerable<EmployeeDTO> employeeDTOs = employeeService.GetEmployees();

            IEnumerable<EmployeeViewModel> employeeViewModels = imapper.Map<IEnumerable<EmployeeDTO>, IEnumerable<EmployeeViewModel>>(employeeDTOs);
            ViewBag.Title = "Планувальник завдань";
            return View(employeeViewModels);
        }


        // Home/MakeTask
        [HttpGet]
        public ActionResult MakeTask(int id)
        {
            ViewBag.EmployeeId = id;
            return View();
        }
        [HttpPost]
        public ActionResult MakeTask(TaskViewModel task)
        {
            if (ModelState.IsValidField("Description") && ModelState.IsValidField("TimeExecution") && ModelState.IsValidField("CurrentPriority"))
            {
                try
                {
                    TaskDTO taskDto = imapper.Map<TaskViewModel, TaskDTO>(task);
                    taskDto.Status = 0;
                    taskService.MakeTask(taskDto);
                    return RedirectToAction("Index");
                }
                catch (NotExistTaskWithIdException ex)
                {
                    return RedirectToAction("Index");
                }
            }
            ViewBag.EmployeeId = task.EmployeeId;
            return View(task);
        }

        [HttpGet]
        public ActionResult DeleteTaskOnId(int id)
        {
            try
            {
                taskService.DeleteTask(id);
                return RedirectToAction("Index");
            }
            catch (NotExistTaskWithIdException ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult DeleteTask(int? id)
        {
            if (id == null)
            {
                ViewBag.ErrorMessage = "Ідентифікатор не може приймати nullable-значення.";
                return View();
            }
            try 
            {
                IEnumerable<TaskDTO> taskDtos = employeeService.GetAllTaskForGivenEmployee((int)id);
                ViewBag.EmployeeId = id;

                return View(imapper.Map<IEnumerable<TaskDTO>, IEnumerable<TaskViewModel>>(taskDtos));
            }
            catch(NotExistEmployeeWithIdException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public ActionResult DeleteTaskWithId(int employeeId, int taskId)
        {
            try
            {
                IEnumerable<TaskDTO> taskDTOs = employeeService.GetAllTaskForGivenEmployee(employeeId).ToList();
                TaskDTO tempTaskDto = taskService.GetTask(taskId);
                if (taskDTOs.Where(t => t.TaskId == taskId).ToList().Count() == 0)
                {
                    ViewBag.ErrorMessage = "Співробітник не виконує завдання із даним ідентифікатором. Будь ласка, повторіть ввід.";
                    ViewBag.ExistingFlag = false;
                    ViewBag.TaskId = "N/A";
                    return PartialView();
                }
                ViewBag.TaskId = taskId.ToString();
                ViewBag.ExistingFlag = true;
                taskService.DeleteTask(taskId);

                return PartialView();
            }
            catch(NotExistTaskWithIdException ex)
            {
                ViewBag.TaskId = "N/A";
                ViewBag.ErrorMessage = "Не існує задачі із заданим ідентифікатором. Будь ласка, спробуйте ввести ідентифікатор ще раз.";
                ViewBag.ExistingFlag = false;
                return PartialView();
            }
        }

        [HttpGet]
        public ActionResult ChangeStatus(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            try
            {
                IEnumerable<TaskDTO> taskDtos = employeeService.GetAllTaskForGivenEmployee((int)id);
                ViewBag.EmployeeId = id;
                return View(imapper.Map<IEnumerable<TaskDTO>, IEnumerable<TaskViewModel>>(taskDtos));
            }
            catch (NotExistEmployeeWithIdException ex)
            {
                return HttpNotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult ChangeStatusOfTask(int employeeId, int taskId)
        {
            try
            {
                IEnumerable<TaskDTO> taskDTOs = employeeService.GetAllTaskForGivenEmployee(employeeId).ToList();
                TaskDTO taskDto = taskService.GetTask(taskId);
                if (taskDTOs.Where(t => t.TaskId == taskId).ToList().Count()==0)
                {
                    ViewBag.ErrorMessage = "Співробітник не виконує завдання із даним ідентифікатором. Будь ласка, повторіть ввід.";
                    ViewBag.ExistingFlag = true;
                    return PartialView();
                }

                ViewBag.ExistingFlag = false;

                return PartialView(imapper.Map<TaskDTO,TaskViewModel>(taskDto));
            }
            catch (NotExistTaskWithIdException ex)
            {
                ViewBag.ErrorMessage = "Не знайдено жодного завдання із заданим ідентифікатором.";
                ViewBag.ExistingFlag = true;
                return PartialView();
            }
        }

        [HttpGet]
        public ActionResult ChangeStatusOfTaskWithId(int employeeId, int taskId)
        {
            try
            {
                IEnumerable<TaskDTO> taskDTOs = employeeService.GetAllTaskForGivenEmployee(employeeId).ToList();
                TaskDTO taskDto = taskService.GetTask(taskId);
                if (taskDTOs.Where(t => t.TaskId == taskId).ToList().Count() == 0)
                {
                    ViewBag.ErrorMessage = "Співробітник не виконує завдання із даним ідентифікатором. Будь ласка, повторіть ввід.";
                    ViewBag.ExistingFlag = true;
                    return View();
                }
                ViewBag.ExistingFlag = false;

                return View(imapper.Map<TaskDTO, TaskViewModel>(taskDto));
            }
            catch (NotExistTaskWithIdException ex)
            {
                ViewBag.ErrorMessage = "Не знайдено жодного завдання із заданим ідентифікатором.";
                ViewBag.ExistingFlag = true;
                return View();
            }
        }

        [HttpPost]
        public ActionResult ChangeStatusConfirmation(int taskId, string newStatus)
        {
            switch(newStatus)
            {
                case "onExecution":
                    SetOnExecutionStatus(taskId);
                    break;
                case "onTesting":
                    SetOnTestingStatus(taskId);
                    break;
                case "onExpired":
                    SetExpiredStatus(taskId);
                    break;
                case "onDone":
                    SetDoneStatus(taskId);
                    break;
                default:
                    ViewBag.Message = "Не вдалося змінити статус задачі!";
                    return PartialView();
            }
            ViewBag.Message = "Статус задачі успішно змінений!";
            return PartialView();
        }

        [HttpGet]
        public ActionResult SearchTasks(int id)
        {
            try
            {
                ViewBag.EmployeeId = id;
                IEnumerable<TaskDTO> taskDtos = employeeService.GetAllTaskForGivenEmployee(id);
                return View(imapper.Map<IEnumerable<TaskDTO>, IEnumerable<TaskViewModel>>(taskDtos));
            }
            catch (NotExistEmployeeWithIdException e)
            {
                throw e;
            }
        }

        [HttpGet]
        public ActionResult SearchListOfStatusHistory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SearchHistory(int TaskId)
        {
            try 
            {
                IEnumerable<StatusHistoryDTO> statusHistoryDtos = statusHistoryService.GetAllStatusHistoryForGivenTask(TaskId).ToList();
                return PartialView(imapper.Map<IEnumerable<StatusHistoryDTO>, IEnumerable<StatusHistoryViewModel>>(statusHistoryDtos));
            }
            catch(NotExistTaskWithIdException ex)
            {
                return HttpNotFound();
            }
        }

        private void SetOnExecutionStatus(int id)
        {
            statusHistoryService.SetOnExecutionStatus(id);
        }

        private void SetOnTestingStatus(int id)
        {
            statusHistoryService.SetOnTestingStatus(id);
        }

        private void SetExpiredStatus(int id)
        {
            statusHistoryService.SetExpiredStatus(id);
        }

        private void SetDoneStatus(int id)
        {
            statusHistoryService.SetDoneStatus(id);
        }

        // Home/IndexPaged
        public ActionResult IndexPaged(int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            IEnumerable<EmployeeDTO> employeeDTOs = employeeService.GetEmployees();
            return View(imapper.Map<IEnumerable<EmployeeDTO>, IEnumerable<EmployeeViewModel>>(employeeDTOs).ToList().ToPagedList(pageNumber, pageSize));
        }

        protected void Dispose(bool disposing)
        {
            taskService.Dispose();
        }
    }
}
