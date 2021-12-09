using System;
using System.Collections.Generic;
using System.Linq;
using Thread = System.Threading;
using ThreadTasks = System.Threading.Tasks;
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
            Thread.TimerCallback tm = new Thread.TimerCallback(CheckTimeExcecutionOfTasksAsync);
            // создаем таймер
            //Thread.Timer timer = new Thread.Timer(tm, uow, 5000, 600000);
        }

        /// <summary>
        /// Method for making task
        /// </summary>
        /// <param name="taskDto">Task data transfer object</param>
        /// <param name="priorityValue"></param>
        public void MakeTask(TaskDTO taskDto)
        {
            Employee employee = Database.Employees.Get(taskDto.EmployeeId);
            Console.WriteLine("!!! Entered employee ID: {0}", taskDto.EmployeeId);
            if (employee == null)
                throw new NotExistEmployeeWithIdException("Employee did not find.");

            Database.Tasks.Create(new Task()
            {
                Description = taskDto.Description,
                TimeExecution = taskDto.TimeExecution,
                StartTime = DateTime.Now,
                Status = Status.NotStarted,
                CurrentPriority = (Priority)taskDto.CurrentPriority,
                EmployeeId = employee.EmployeeId
            });
            Database.Save();

            Database.StatusHistories.Create(new StatusHistory()
            {
                DateAppearOfStatus = DateTime.Now,
                Status = Status.NotStarted,
                TaskId = Database.Tasks.GetAll().Last().TaskId
            });
            Database.Save();
        }
        public void DeleteTask(int id)
        {
            Task task = Database.Tasks.Get(id);
            if (task == null)
                throw new NotExistTaskWithIdException("Task did not find with given id.");

            Database.Tasks.Delete(id);
            Database.Save();
        }
        public TaskDTO GetTask(int id)
        {
            Task task = Database.Tasks.Get(id);
            if (task == null)
                throw new NotExistTaskWithIdException("Task did not find with given id.");

            MapperConfiguration config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Task, TaskDTO>();
            });
            IMapper mapper = config.CreateMapper();
            return mapper.Map<Task, TaskDTO>(task);
        }
        public void CheckTimeExcecutionOfTasks(Object uow)
        {
            DateTime timeNow = DateTime.Now;
            IUnitOfWork unitOfWork = (IUnitOfWork)uow;
            IList<Task> tasks = unitOfWork.Tasks.GetAll().ToList();
            foreach(Task task in tasks)
            {
                Console.WriteLine("Status of task = {0}", task.Status);
                if (task.Status == Status.OnExecution || task.Status == Status.OnTesting)
                {
                    TimeSpan subTime = timeNow.Subtract(task.StartTime);
                    Double hours = subTime.TotalHours;
                    Double executionHours = task.TimeExecution.TotalHours;
                    if (hours > executionHours)
                    {
                        task.Status = Status.Expired;
                        Database.Tasks.Update(task);

                        Database.StatusHistories.Create(new StatusHistory()
                        {
                            DateAppearOfStatus = DateTime.Now,
                            Status = Status.Expired,
                            TaskId = task.TaskId
                        });

                        Database.Save();
                    }
                }
            }
        }
        public async void CheckTimeExcecutionOfTasksAsync(Object uow)
        {
            await ThreadTasks.Task.Run(() => CheckTimeExcecutionOfTasks(uow));
        }
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
