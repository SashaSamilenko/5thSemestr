using AutoMapper;
using PlannerTasks.BLL.DTO;
using PlannerTasks.BLL.Infrastructure;
using PlannerTasks.BLL.Interfaces;
using PlannerTasks.DAL.Entities;
using PlannerTasks.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Thread = System.Threading;
using ThreadTasks = System.Threading.Tasks;

namespace PlannerTasks.BLL.Services
{
    public class EmployeeService: IEmployeeService
    {
        /// <summary>
        /// Property presents of interface IUnitOfWork
        /// </summary>
        IUnitOfWork Database { get; set; }

        /// <summary>
        /// Property presents of mapper of Task to TaskDTO
        /// </summary>
        IMapper mapperToTaskDTO { get; set; }

        /// <summary>
        /// Property presents of mapper of Employee to EmployeeDTO
        /// </summary>
        IMapper mapperToEmployeeDTO { get; set; }

        /// <summary>
        /// Constructor with one parameter
        /// </summary>
        /// <param name="uow"></param>
        public EmployeeService(IUnitOfWork uow)
        {
            Database = uow;
            MapperConfiguration configToTaskDTOTask = new MapperConfiguration(cfg => cfg.CreateMap<Task, TaskDTO>());
            MapperConfiguration configEmployee = new MapperConfiguration(cfg => cfg.CreateMap<Employee, EmployeeDTO>());

            mapperToTaskDTO = configToTaskDTOTask.CreateMapper();
            mapperToEmployeeDTO = configEmployee.CreateMapper();
        }
        public IEnumerable<EmployeeDTO> GetEmployees()
        {
            return mapperToEmployeeDTO.Map<IEnumerable<Employee>, List<EmployeeDTO>>(Database.Employees.GetAll());
        }
        public EmployeeDTO GetEmployee(int id)
        {
            Employee employee = Database.Employees.Get(id);
            if (employee == null)
                throw new NotExistEmployeeWithIdException("Employee did not find with given id.");

            return mapperToEmployeeDTO.Map<Employee, EmployeeDTO>(employee);
        }
        public IEnumerable<TaskDTO> GetAllTaskForGivenEmployee(int id)
        {
            Employee employee = Database.Employees.Get(id);
            if (employee == null)
            {
                throw new NotExistEmployeeWithIdException("Employee with given id is not existing.");
            }

            
            IEnumerable<Task> tasks = Database.Tasks.Find(t => t.EmployeeId == id);
            return mapperToTaskDTO.Map<IEnumerable<Task>, IEnumerable<TaskDTO>>(tasks);
        }
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
