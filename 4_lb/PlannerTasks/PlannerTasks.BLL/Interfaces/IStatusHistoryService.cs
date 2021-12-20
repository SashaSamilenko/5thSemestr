using PlannerTasks.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerTasks.BLL.Interfaces
{
    public interface IStatusHistoryService
    {
        IEnumerable<StatusHistoryDTO> GetAllStatusHistoryForGivenTask(int id);
        IEnumerable<StatusHistoryDTO> GetAllStatusHistory();
        void SetOnExecutionStatus(int id);
        void SetOnTestingStatus(int id);
        void SetExpiredStatus(int id);
        void SetDoneStatus(int id);
        void Dispose();
    }
}
