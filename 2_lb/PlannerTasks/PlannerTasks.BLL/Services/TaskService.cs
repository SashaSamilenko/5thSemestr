using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlannerTasks.BLL.DTO;
using PlannerTasks.DAL.Entities;
//using PlannerTasks.BLL.BusinessModels;
using PlannerTasks.DAL.Interfaces;
using PlannerTasks.BLL.Infrastructure;
using PlannerTasks.BLL.Interfaces;
using AutoMapper;

namespace PlannerTasks.BLL.Services
{
    public class TaskService: ITaskService
    {
        IUnitOfWork Database { get; set; }

        public TaskService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public void MakeTask(TaskDTO taskDto)
        {
            Employee employee = Database.Employees.Get(taskDto.EmployeeId);

            if (employee == null)
                throw new ValidationException("Employee did not find!", "");

            //decimal sum = new Discount(0.1m).GetDiscountedPrice(phone.Price);

            Task order = new Task
            {
                Description = taskDto.Description,
                TimeExecution = taskDto.TimeExecution,
                Status = taskDto.Status,
                Priority = taskDto.Priority,
                EmployeeId = employee.EmployeeId
                /*Date = DateTime.Now,
                Address = orderDto.Address,
                PhoneId = phone.Id,
                Sum = sum,
                PhoneNumber = orderDto.PhoneNumber*/
            };
            Database.Tasks.Create(order);
            Database.Save();
        }

        public IEnumerable<EmployeeDTO> GetEmployees()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Employee, EmployeeDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Employee>, List<EmployeeDTO>>(Database.Employees.GetAll());
        }

        public EmployeeDTO GetEmployee(int? id)
        {
            if (id == null)
                throw new ValidationException("Does not id of employee!", "");
            var employee = Database.Employees.Get(id.Value);
            if (employee == null)
                throw new ValidationException("Employee did not find!", "");

            return
                new EmployeeDTO
                    { 
                        EmployeeId = employee.EmployeeId,
                        FirstName = employee.FirstName, 
                        SecondName = employee.SecondName, 
                        HomePhone = employee.HomePhone, 
                        BirthDate = employee.BirthDate,
                        TitleOfCourtesy = employee.TitleOfCourtesy,
                        BusyRate = employee.BusyRate

                };
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
