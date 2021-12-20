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
        //[System.Web.Http.Route("~/api/GetAllTasks")]
        [System.Web.Http.Route("api/Tasks")]
        public IEnumerable<TaskViewModel> Get()
        {
            IEnumerable<TaskDTO> taskDTOs = taskService.GetAllTask();
            return imapper.Map<IEnumerable<TaskDTO>, List<TaskViewModel>>(taskDTOs);
        }

        // GET api/<controller>/id
        //[System.Web.Http.Route("~/api/GetTask/id")]
        //[HttpGet]
        [System.Web.Http.Route("api/Tasks/{id}")]
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

        [System.Web.Http.Route("api/Task")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Post([FromBody] TaskViewModel taskViewModel)
        {
            if (!ModelState.IsValidField("EmployeeId") || !ModelState.IsValidField("Description") || !ModelState.IsValidField("TimeExecution") || !ModelState.IsValidField("CurrentPriority"))
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            TaskDTO taskDto = imapper.Map<TaskViewModel, TaskDTO>(taskViewModel);
            taskDto.Status = 0;
            try
            {
                taskService.MakeTask(taskDto);
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        [System.Web.Http.Route("api/Tasks/{id}")]
        [System.Web.Http.HttpDelete]
        public IHttpActionResult Delete(Int32 id)
        {
            try
            {
                taskService.DeleteTask(id);
                return Ok();
            }
            catch (NotExistTaskWithIdException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [System.Web.Http.Route("api/Tasks/{taskId}")]
        [System.Web.Http.HttpPatch]
        public IHttpActionResult ChangeStatus(Int32 taskId, [FromBody] TaskViewModel taskViewModel)
        {
            if (!ModelState.IsValidField("Status"))
            {
                return BadRequest("Field Status can be more zero and less than 5");
            }
            try
            {
                TaskDTO taskDto = taskService.GetTask(taskId);
                taskDto.Status = taskViewModel.Status;
                taskService.ChangeTaskStatus(taskDto);
                return Ok();
            }
            catch (NotExistTaskWithIdException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [System.Web.Http.Route("api/Employees/{employeeId}/Tasks")]
        [HttpGet]
        public IHttpActionResult GetAllTasksForEmployee(Int32 employeeId)
        {
            try
            {
                IEnumerable<TaskDTO> taskDTOs = employeeService.GetAllTaskForGivenEmployee(employeeId);
                return Ok(imapper.Map<IEnumerable<TaskDTO>, List<TaskViewModel>>(taskDTOs));
            }
            catch(NotExistEmployeeWithIdException ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}