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
using System.Net.Http;
using System.Net;

namespace PlannerTasks.WEB_API.Controllers
{
    public class StatusHistoryController : ApiController
    {
        ITaskService taskService { get; set; }
        IEmployeeService employeeService { get; set; }
        IStatusHistoryService statusHistoryService { get; set; }
        IMapper imapper { get; set; }
        public StatusHistoryController(ITaskService taskS, IEmployeeService employeeS, IStatusHistoryService statusHistoryS)
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

        [System.Web.Http.Route("api/StatusHistories")]
        public IEnumerable<StatusHistoryViewModel> Get()
        {
            IEnumerable<StatusHistoryDTO> statusHistoryDTOs = statusHistoryService.GetAllStatusHistory();
            return imapper.Map<IEnumerable<StatusHistoryDTO>, List<StatusHistoryViewModel>>(statusHistoryDTOs);
        }

        [System.Web.Http.Route("api/StatusHistories/{id}")]
        public IHttpActionResult Get(Int32 id)
        {
            try
            {
                TaskDTO taskDTO = taskService.GetTask(id);

                return Ok(imapper.Map<TaskDTO, TaskViewModel>(taskDTO));
            }
            catch (NotExistTaskWithIdException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [System.Web.Http.Route("api/Employees/{employeeId}/Tasks/{taskId}/StatusHistory")]
        [HttpGet]
        public IHttpActionResult GetStatusHistoryForTaskForEmployee(Int32 employeeId, Int32 taskId)
        {
            try
            {
                IEnumerable<TaskDTO> taskDTOs = employeeService.GetAllTaskForGivenEmployee(employeeId);
            }
            catch(NotExistEmployeeWithIdException ex)
            {
                return BadRequest(ex.Message);
            }
            try
            {
                IEnumerable<StatusHistoryDTO> statusHistoryViewModels = statusHistoryService.GetAllStatusHistoryForGivenTask(taskId);
                return Ok(imapper.Map<IEnumerable<StatusHistoryDTO>, List<StatusHistoryViewModel>>(statusHistoryViewModels));
            }
            catch (NotExistTaskWithIdException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [System.Web.Http.Route("api/Tasks/{taskId}/StatusHistory")]
        [HttpGet]
        public IHttpActionResult GetStatusHistoryForTask(Int32 taskId)
        {
            try
            {
                IEnumerable<StatusHistoryDTO> statusHistoryViewModels = statusHistoryService.GetAllStatusHistoryForGivenTask(taskId);
                return Ok(imapper.Map<IEnumerable<StatusHistoryDTO>, List<StatusHistoryViewModel>>(statusHistoryViewModels));
            }
            catch (NotExistTaskWithIdException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
