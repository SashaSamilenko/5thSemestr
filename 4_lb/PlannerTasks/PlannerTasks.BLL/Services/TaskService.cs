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
        /// Property presents of mapper of TaskDTO to Task
        /// </summary>
        IMapper mapperToTaskEntity { get; set; }
        /// <summary>
        /// Property presents of mapper of Task to TaskDTO
        /// </summary>
        IMapper mapperToTaskDTO { get; set; }

        /// <summary>
        /// Property presents of mapper of StatusHistoryDTO to StatusHistory
        /// </summary>
        IMapper mapperToStatusHistoryEntity { get; set; }

        /// <summary>
        /// Constructor with one parameter
        /// </summary>
        /// <param name="uow"></param>
        public TaskService(IUnitOfWork uow)
        {
            Database = uow;
            MapperConfiguration configTaskToEntity = new MapperConfiguration(cfg => {
                cfg.CreateMap<TaskDTO, Task>()
                .ForMember("StartTime", te => te.MapFrom(c => DateTime.Now))
                .ForMember("Status", te => te.MapFrom(c => Status.NotStarted))
                .ForMember("CurrentPriority", te => te.MapFrom(c => (Priority)c.CurrentPriority));
            });
            MapperConfiguration configTaskToDto = new MapperConfiguration(cfg => {
                cfg.CreateMap<Task, TaskDTO>()
                .ForMember("Status", te => te.MapFrom(c => (int)c.Status))
                .ForMember("CurrentPriority", te => te.MapFrom(c => (int)c.CurrentPriority));
            });
            MapperConfiguration configStatusHistoryToEntity = new MapperConfiguration(cfg => cfg.CreateMap<StatusHistoryDTO, StatusHistory>());

            mapperToStatusHistoryEntity = configStatusHistoryToEntity.CreateMapper();
            mapperToTaskEntity = configTaskToEntity.CreateMapper();
            mapperToTaskDTO = configTaskToDto.CreateMapper();


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
            if (employee == null)
                throw new NotExistEmployeeWithIdException("Employee did not find.");

            
            Database.Tasks.Create(mapperToTaskEntity.Map<TaskDTO, Task>(taskDto));
            Database.Save();

            StatusHistoryDTO statusHistoryDTO = new StatusHistoryDTO() {
                DateAppearOfStatus = DateTime.Now,
                Status = (int)Status.NotStarted,
                TaskId = Database.Tasks.GetAll().Last().TaskId
            };
            
            Database.StatusHistories.Create(mapperToStatusHistoryEntity.Map<StatusHistoryDTO, StatusHistory>(statusHistoryDTO));
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
            return mapperToTaskDTO.Map<Task, TaskDTO>(task);
        }
        public void ChangeTaskStatus(TaskDTO taskDto)
        {
            Task task = Database.Tasks.Get(taskDto.TaskId);
            task.Status = (Status)taskDto.Status;
            Database.Tasks.Update(task);

            StatusHistoryDTO statusHistoryDTO = new StatusHistoryDTO()
            {
                DateAppearOfStatus = DateTime.Now,
                Status = taskDto.Status,
                TaskId = taskDto.TaskId
            };

            Database.StatusHistories.Create(mapperToStatusHistoryEntity.Map<StatusHistoryDTO, StatusHistory>(statusHistoryDTO));

            Database.Save();
        }
        public IEnumerable<TaskDTO> GetAllTask()
        {
            IEnumerable<Task> tasks = Database.Tasks.GetAll().ToList();
            if (tasks == null)
                throw new ValidationException("Tasks did not find.");
            return mapperToTaskDTO.Map<IEnumerable<Task>, IEnumerable<TaskDTO>>(tasks);
        }
        public void CheckTimeExcecutionOfTasks(Object uow)
        {
            DateTime timeNow = DateTime.Now;
            IUnitOfWork unitOfWork = (IUnitOfWork)uow;
            IList<Task> tasks = unitOfWork.Tasks.GetAll().ToList();
            foreach(Task task in tasks)
            {
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
