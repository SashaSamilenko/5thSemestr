using System;
using System.Collections.Generic;
using PlannerTasks.BLL.DTO;
using PlannerTasks.DAL.Entities;
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
        public void MakeTask(TaskDTO taskDto, Int32 priorityValue)
        {
            Employee employee = Database.Employees.Get(taskDto.EmployeeId);

            if (employee == null)
                throw new ValidationException("Employee did not find!", "");

            //decimal sum = new Discount(0.1m).GetDiscountedPrice(phone.Price);
            Priority taskPriority;
            switch (priorityValue)
            {
                case 1: taskPriority = Priority.Low;
                    break;
                case 2: taskPriority = Priority.Medium;
                    break;
                case 3: taskPriority = Priority.High;
                    break; 
                default: taskPriority = Priority.Low;
                    break;
            }

            Task order = new Task
            {
                Description = taskDto.Description,
                TimeExecution = taskDto.TimeExecution,
                StartTime = taskDto.StartTime,
                Status = Status.NotStarted,
                CurrentPriority = taskPriority,
                EmployeeId = employee.EmployeeId
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
                        HomePhone = employee.PhoneNumber, 
                        BirthDate = employee.BirthDate//,
                        //TitleOfCourtesy = employee.TitleOfCourtesy,
                        //BusyRate = employee.BusyRate
                    };
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
