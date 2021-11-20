﻿using System;
using System.Collections.Generic;
using System.Linq;
using PlannerTasks.BLL.DTO;
using PlannerTasks.DAL.Entities;
using PlannerTasks.DAL.Interfaces;
using PlannerTasks.BLL.Infrastructure;
using PlannerTasks.BLL.Interfaces;
using AutoMapper;

namespace PlannerTasks.BLL.Services
{
    /// <summary>
    /// Class TaskService
    /// Provides a service to interact
    /// with DLL level and data
    /// </summary>
    public class TaskService: ITaskService
    {
        /// <summary>
        /// Property presents of interface IUnitOfWork
        /// </summary>
        IUnitOfWork Database { get; set; }

        /// <summary>
        /// Constructor with one parameter
        /// </summary>
        /// <param name="uow"></param>
        public TaskService(IUnitOfWork uow)
        {
            Database = uow;
        }

        /// <summary>
        /// Method for making task
        /// </summary>
        /// <param name="taskDto">Task data transfer object</param>
        /// <param name="priorityValue"></param>
        public void MakeTask(TaskDTO taskDto)
        {
            Employee employee = Database.Employees.Get(taskDto.EmployeeId);

            if (employee == null)
                throw new NotExistEmployeeWithIdException("Employee did not find.");

            //decimal sum = new Discount(0.1m).GetDiscountedPrice(phone.Price);

            Task order = new Task
            {
                Description = taskDto.Description,
                TimeExecution = taskDto.TimeExecution,
                StartTime = DateTime.Now,
                Status = taskDto.Status,
                CurrentPriority = taskDto.CurrentPriority,
                EmployeeId = employee.EmployeeId
            };
            Database.Tasks.Create(order);
            /*Database.Save();*/

            Database.StatusHistories.Create(new StatusHistory()
            {
                DateAppearOfStatus = DateTime.Now,
                Status = Status.NotStarted,
                TaskId = Database.Tasks.GetAll().Last().TaskId
            });
            Database.Save();
        }

        public IEnumerable<EmployeeDTO> GetEmployees()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Employee, EmployeeDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Employee>, List<EmployeeDTO>>(Database.Employees.GetAll());
        }

        public EmployeeDTO GetEmployee(int? id)// System.Nullable<Int32>
        {
            if (id == null)
                throw new ValidationException("Have not given id of employee.");
            Employee employee = Database.Employees.Get(id.Value);
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

        public IEnumerable<StatusHistory> GetAllStatusHistoryForGivenTask(int id)
        {
            Task task = Database.Tasks.Get(id);
            if (task == null)
            {
                throw new NotExistTaskWithIdException("Task with given taskId is not existing.");
            }

            return Database.StatusHistories.Find(sh => sh.TaskId == id);
        }

        public void SetOnExecutionStatus(int id)
        {
            Task task = Database.Tasks.Get(id);
            if (task == null)
            {
                throw new NotExistTaskWithIdException("Task with given id is not existing.");
            }

            task.Status = Status.OnExecution;
            Database.Tasks.Update(task);

            Database.StatusHistories.Create(new StatusHistory()
            {
                DateAppearOfStatus = DateTime.Now,
                Status = Status.OnExecution,
                TaskId = id
            });

            Database.Save();
        }

        public void SetOnTestingStatus(int id)
        {
            Task task = Database.Tasks.Get(id);
            if (task == null)
            {
                throw new NotExistTaskWithIdException("Task with given id is not existing.");
            }

            task.Status = Status.OnTesting;
            Database.Tasks.Update(task);

            Database.StatusHistories.Create(new StatusHistory()
            {
                DateAppearOfStatus = DateTime.Now,
                Status = Status.OnTesting,
                TaskId = id
            });

            Database.Save();
        }

        public void SetExpiredStatus(int id)
        {
            Task task = Database.Tasks.Get(id);
            if (task == null)
            {
                throw new NotExistTaskWithIdException("Task with given id is not existing.");
            }

            task.Status = Status.Expired;
            Database.Tasks.Update(task);

            Database.StatusHistories.Create(new StatusHistory()
            {
                DateAppearOfStatus = DateTime.Now,
                Status = Status.Expired,
                TaskId = id
            });

            Database.Save();
        }

        public void SetDoneBeforeExpiredStatus(int id)
        {
            Task task = Database.Tasks.Get(id);
            if (task == null)
            {
                throw new NotExistTaskWithIdException("Task with given id is not existing.");
            }

            task.Status = Status.DoneBeforeExpired;
            Database.Tasks.Update(task);

            Database.StatusHistories.Create(new StatusHistory()
            {
                DateAppearOfStatus = DateTime.Now,
                Status = Status.DoneBeforeExpired,
                TaskId = id
            });

            Database.Save();
        }

        public void SetDoneAfterExpiredStatus(int id)
        {
            Task task = Database.Tasks.Get(id);
            if (task == null)
            {
                throw new NotExistTaskWithIdException("Task with given id is not existing.");
            }

            task.Status = Status.DoneAfterExpired;
            Database.Tasks.Update(task);

            Database.StatusHistories.Create(new StatusHistory()
            {
                DateAppearOfStatus = DateTime.Now,
                Status = Status.DoneAfterExpired,
                TaskId = id
            });

            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
