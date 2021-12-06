using PlannerTasks.BLL.Infrastructure;
using PlannerTasks.BLL.Interfaces;
using PlannerTasks.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThreadTasks = System.Threading.Tasks;
using Thread = System.Threading;
using PlannerTasks.BLL.DTO;
using AutoMapper;
using PlannerTasks.DAL.Interfaces;

namespace PlannerTasks.BLL.Services
{
    public class StatusHistoryService: IStatusHistoryService
    {
        /// <summary>
        /// Property presents of interface IUnitOfWork
        /// </summary>
        IUnitOfWork Database { get; set; }

        /// <summary>
        /// Constructor with one parameter
        /// </summary>
        /// <param name="uow"></param>
        public StatusHistoryService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public IEnumerable<StatusHistoryDTO> GetAllStatusHistoryForGivenTask(int id)
        {
            Task task = Database.Tasks.Get(id);
            if (task == null)
            {
                throw new NotExistTaskWithIdException("Task with given id is not existing.");
            }

            MapperConfiguration config = new MapperConfiguration(cfg => {
                cfg.CreateMap<StatusHistory, StatusHistoryDTO>();
            });

            IMapper mapper = config.CreateMapper();
            IEnumerable<StatusHistory> source = Database.StatusHistories.Find(sh => sh.TaskId == id);
            return mapper.Map<IEnumerable<StatusHistory>, IEnumerable<StatusHistoryDTO>>(source);
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
        public void SetDoneStatus(int id)
        {
            Task task = Database.Tasks.Get(id);
            if (task == null)
            {
                throw new NotExistTaskWithIdException("Task with given id is not existing.");
            }

            task.Status = Status.Done;
            Database.Tasks.Update(task);

            Database.StatusHistories.Create(new StatusHistory()
            {
                DateAppearOfStatus = DateTime.Now,
                Status = Status.Done,
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
