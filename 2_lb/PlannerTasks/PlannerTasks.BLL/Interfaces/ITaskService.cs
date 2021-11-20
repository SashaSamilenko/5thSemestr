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
        EmployeeDTO GetEmployee(int id);
        IEnumerable<EmployeeDTO> GetEmployees();
        IEnumerable<TaskDTO> GetAllTaskForGivenEmployee(int id);
        IEnumerable<StatusHistoryDTO> GetAllStatusHistoryForGivenTask(int id);
        void SetOnExecutionStatus(int id);
        void SetOnTestingStatus(int id);
        void SetExpiredStatus(int id);
        void SetDoneStatus(int id);
        void Dispose();
    }
}
