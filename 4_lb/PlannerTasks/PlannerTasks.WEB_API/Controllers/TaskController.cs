using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

using PlannerTasks.BLL.Interfaces;
using PlannerTasks.BLL.DTO;

using PlannerTasks.WEB_API.App_Start;
using PlannerTasks.WEB_API.Models;

using AutoMapper;

using PlannerTasks.BLL.Services;
using PlannerTasks.BLL.Infrastructure;
using Ninject;
using Ninject.Modules;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;

namespace PlannerTasks.WEB_API.Controllers
{
    public class TaskController : ApiController
    {
        ITaskService taskService { get; set; }
        IEmployeeService employeeService { get; set; }
        IStatusHistoryService statusHistoryService { get; set; }
        IMapper imapper { get; set; }
        public TaskController(ITaskService taskS, IEmployeeService employeeS, IStatusHistoryService statusHistoryS)
        {
            /*taskService = taskS;
            employeeService = employeeS;
            statusHistoryService = statusHistoryS;*/
            imapper = AutoMapperConfiguration.GetMapper();
            var modules = new INinjectModule[] { new ServiceModule("PlannerDB") };
            var kernel = new StandardKernel(modules);

            kernel.Bind<ITaskService>().To<TaskService>();
            kernel.Bind<IEmployeeService>().To<EmployeeService>();
            kernel.Bind<IStatusHistoryService>().To<StatusHistoryService>();

            taskService = kernel.Get<ITaskService>();
            employeeService = kernel.Get<IEmployeeService>();
            statusHistoryService = kernel.Get<IStatusHistoryService>();
        }
        // GET api/<controller>
        [System.Web.Http.Route("~/api/GetAllTasks")]
        [HttpGet]
        public IEnumerable<TaskViewModel> GetAllTasks()
        {
            IEnumerable<TaskDTO> taskDTOs = null;
            foreach(EmployeeDTO employeeDto in employeeService.GetEmployees())
            {
                IEnumerable<TaskDTO> tempTasks = employeeService.GetAllTaskForGivenEmployee(employeeDto.EmployeeId);
                if(tempTasks != null)
                {
                    foreach(TaskDTO taskDto in tempTasks)
                    {
                        taskDTOs.Append<TaskDTO>(taskDto);
                    }   
                }
            }
            return imapper.Map<IEnumerable<TaskDTO>, List<TaskViewModel>>(taskDTOs);
        }
    }
}