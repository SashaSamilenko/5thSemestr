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
    public class EmployeeController : ApiController
    {
        ITaskService taskService { get; set; }
        IEmployeeService employeeService { get; set; }
        IStatusHistoryService statusHistoryService { get; set; }
        IMapper imapper { get; set; }
        public EmployeeController(ITaskService taskS, IEmployeeService employeeS, IStatusHistoryService statusHistoryS)
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
        public IEnumerable<EmployeeViewModel> Get()
        {
            IEnumerable<EmployeeDTO> employeeDTOs = employeeService.GetEmployees();
            return imapper.Map<IEnumerable<EmployeeDTO>, List<EmployeeViewModel>>(employeeDTOs);
        }

        // GET api/<controller>/id
        //[System.Web.Http.Route("~/api/GetTask/id")]
        //[HttpGet]
        public IHttpActionResult Get(Int32 id)
        {
            try
            {
                EmployeeDTO employeeDTO = employeeService.GetEmployee(id);

                return Ok(imapper.Map<EmployeeDTO, EmployeeViewModel>(employeeDTO));
            }
            catch (NotExistEmployeeWithIdException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
