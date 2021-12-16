using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlannerTasks.BLL.Services;
using PlannerTasks.BLL.Interfaces;
using PlannerTasks.BLL.DTO;
using PlannerTasks.BLL.Infrastructure;
using Ninject;
using Ninject.Modules;
using AutoMapper;

namespace PlannerTasks.Console.Controllers
{
    public class MainController
    {
        ITaskService taskService;
        IEmployeeService employeeService;
        IStatusHistoryService statusHistoryService;
        public MainController(ITaskService tServ, IEmployeeService eServ, IStatusHistoryService shServ)
        {
            taskService = tServ;
            employeeService = eServ;
            statusHistoryService = shServ;
        }
        public void MakeTask(TaskDTO task)
        {
            try
            {
                taskService.MakeTask(task);
            }
            catch (NotExistEmployeeWithIdException ex)
            {
                throw ex;
            }
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
        public IEnumerable<EmployeeDTO> GetEmployees()
        {
            return employeeService.GetEmployees();
        }
        public EmployeeDTO GetEmployee(int id)
        {
            try
            {
                return employeeService.GetEmployee(id);
            }
            catch (NotExistEmployeeWithIdException e)
            {
                throw e;
            }
        }
        public IEnumerable<TaskDTO> GetAllTaskForGivenEmployee(int id)
        {
            try
            {
                return employeeService.GetAllTaskForGivenEmployee(id);
            }
            catch (NotExistEmployeeWithIdException e)
            {
                throw e;
            }
            
        }
        public IEnumerable<StatusHistoryDTO> GetAllStatusHistoryForGivenTask(int id)
        {
            return statusHistoryService.GetAllStatusHistoryForGivenTask(id);
        }

        public void SetOnExecutionStatus(int id)
        {
            statusHistoryService.SetOnExecutionStatus(id);
        }

        public void SetOnTestingStatus(int id)
        {
            statusHistoryService.SetOnTestingStatus(id);
        }

        public void SetExpiredStatus(int id)
        {
            statusHistoryService.SetExpiredStatus(id);
        }

        public void SetDoneStatus(int id)
        {
            statusHistoryService.SetDoneStatus(id);
        }

        protected void Dispose(bool disposing)
        {
            taskService.Dispose();
        }
    }
}
