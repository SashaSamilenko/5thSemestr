using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using System.Configuration;

namespace PlannerTasks.WEB_MVC.Controllers
{
    public class HomeController: Controller
    {
        ITaskService taskService;
        IEmployeeService employeeService;
        IStatusHistoryService statusHistoryService;
        public HomeController()
        {
            var modules = new INinjectModule[] { new ServiceModule("PlannerDB"), new TaskModule(), new EmployeeModule(), new StatusHistoryModule() };
            var kernel = new StandardKernel(modules);
            taskService = kernel.Get<ITaskService>();
            employeeService = kernel.Get<IEmployeeService>();
            statusHistoryService = kernel.Get<IStatusHistoryService>();
        }

        // /Home/Index
        public ActionResult Index()
        {
            IEnumerable<EmployeeDTO> employeeDTOs = employeeService.GetEmployees();
            MapperConfiguration config = new MapperConfiguration(cfg => {
                cfg.CreateMap<EmployeeDTO, EmployeeViewModel>();
            });
            IMapper mapper = config.CreateMapper();
            IEnumerable<EmployeeViewModel> employeeViewModels = mapper.Map<IEnumerable<EmployeeDTO>, IEnumerable<EmployeeViewModel>>(employeeDTOs);

            ViewBag.Title = "Планувальник завдань";
            return View(employeeViewModels);
        }


        // Home/MakeTask
        [HttpGet]
        public ActionResult MakeTask()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MakeTask(int id, TaskViewModel task)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MapperConfiguration config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<TaskViewModel, TaskDTO>()
                        .ForMember("TimeExecution", te => te.MapFrom(c => new TimeSpan(c.TimeExecution, 0, 0)));
                    });

                    IMapper mapper = config.CreateMapper();
                    TaskDTO taskDto = mapper.Map<TaskViewModel, TaskDTO>(task);

                    taskDto.Status = 1;
                    taskService.MakeTask(taskDto);

                    return RedirectToAction("Index");
                }
                catch (NotExistEmployeeWithIdException ex)
                {
                    throw ex;
                }
            }
            return View(task);
        }
        public void DeleteTask(Int32 id)
        {
            try
            {
                taskService.DeleteTask(id);
            }
            catch(NotExistTaskWithIdException ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public IEnumerable<EmployeeViewModel> GetEmployees()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<EmployeeDTO, EmployeeViewModel>()).CreateMapper();
            return mapper.Map<IEnumerable<EmployeeDTO>, List<EmployeeViewModel>>(employeeService.GetEmployees());
        }

        [HttpGet]
        public EmployeeViewModel GetEmployee(int id)
        {
            try
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<EmployeeDTO, EmployeeViewModel>()).CreateMapper();
                return mapper.Map<EmployeeDTO, EmployeeViewModel>(employeeService.GetEmployee(id));
            }
            catch (NotExistEmployeeWithIdException e)
            {
                throw e;
            }
        }

        [HttpGet]
        public ActionResult SearchTasks(int id)
        {
            try
            {
                IEnumerable<TaskDTO> taskDtos = employeeService.GetAllTaskForGivenEmployee(id);
                MapperConfiguration config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<TaskDTO, TaskViewModel>()
                    .ForMember("TimeExecution", te => te.MapFrom(c => c.TimeExecution.Hours));
                });
                IMapper mapper = config.CreateMapper();
                return View(mapper.Map<IEnumerable<TaskDTO>, IEnumerable<TaskViewModel>>(taskDtos));
            }
            catch (NotExistEmployeeWithIdException e)
            {
                throw e;
            }
        }

        // Home/Search
        public ActionResult SearchListOfStatusHistory()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SearchHistory(int identifier)
        {
            IEnumerable<StatusHistoryDTO> statusHistoryDtos = statusHistoryService.GetAllStatusHistoryForGivenTask(identifier).ToList();
            MapperConfiguration config = new MapperConfiguration(cfg => {
                cfg.CreateMap<StatusHistoryDTO, StatusHistoryViewModel>();
            });
            IMapper mapper = config.CreateMapper();
            return View(mapper.Map<IEnumerable<StatusHistoryDTO>, IEnumerable<StatusHistoryViewModel>>(statusHistoryDtos));
        }

        [HttpPost]
        public void SetOnExecutionStatus(int id)
        {
            statusHistoryService.SetOnExecutionStatus(id);
        }

        [HttpPost]
        public void SetOnTestingStatus(int id)
        {
            statusHistoryService.SetOnTestingStatus(id);
        }

        [HttpPost]
        public void SetExpiredStatus(int id)
        {
            statusHistoryService.SetExpiredStatus(id);
        }

        [HttpPost]
        public void SetDoneStatus(int id)
        {
            statusHistoryService.SetDoneStatus(id);
        }

        // Home/IndexPaged
        public ActionResult IndexPaged(int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            IEnumerable<EmployeeDTO> employeeDTOs = employeeService.GetEmployees();
            MapperConfiguration config = new MapperConfiguration(cfg => {
                cfg.CreateMap<EmployeeDTO, EmployeeViewModel>();
            });
            IMapper mapper = config.CreateMapper();
            IEnumerable<EmployeeViewModel> employeeViewModels = mapper.Map<IEnumerable<EmployeeDTO>, IEnumerable<EmployeeViewModel>>(employeeDTOs);

            return View(employeeViewModels.ToList().ToPagedList(pageNumber, pageSize));
        }

        protected void Dispose(bool disposing)
        {
            taskService.Dispose();
        }
    }
}
