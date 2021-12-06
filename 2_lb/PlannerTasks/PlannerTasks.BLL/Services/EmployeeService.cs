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
        /// Constructor with one parameter
        /// </summary>
        /// <param name="uow"></param>
        public EmployeeService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public IEnumerable<EmployeeDTO> GetEmployees()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Employee, EmployeeDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Employee>, List<EmployeeDTO>>(Database.Employees.GetAll());
        }
        public EmployeeDTO GetEmployee(int id)
        {
            Employee employee = Database.Employees.Get(id);
            if (employee == null)
                throw new NotExistEmployeeWithIdException("Employee did not find with given id.");

            return
                new EmployeeDTO
                {
                    EmployeeId = employee.EmployeeId,
                    FirstName = employee.FirstName,
                    SecondName = employee.SecondName,
                    PhoneNumber = employee.PhoneNumber,
                    BirthDate = employee.BirthDate
                };
        }
        public IEnumerable<TaskDTO> GetAllTaskForGivenEmployee(int id)
        {
            Employee employee = Database.Employees.Get(id);
            if (employee == null)
            {
                throw new NotExistEmployeeWithIdException("Employee with given id is not existing.");
            }

            MapperConfiguration config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Task, TaskDTO>();
            });

            IMapper mapper = config.CreateMapper();
            IEnumerable<Task> source = Database.Tasks.Find(t => t.EmployeeId == id);
            return mapper.Map<IEnumerable<Task>, IEnumerable<TaskDTO>>(source);
        }
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
