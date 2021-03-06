using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlannerTasks.BLL.DTO;

namespace PlannerTasks.BLL.Interfaces
{
    public interface ITaskService
    {
        void MakeTask(TaskDTO taskDto);
        void DeleteTask(int id);
        TaskDTO GetTask(int id);
        IEnumerable<TaskDTO> GetAllTask();
        void ChangeTaskStatus(TaskDTO taskDto);
        void CheckTimeExcecutionOfTasks(Object uow);
        void CheckTimeExcecutionOfTasksAsync(Object uow);
        void Dispose();
    }
}
