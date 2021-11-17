using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlannerTasks.DAL.Entities;
using Task = System.Threading.Tasks.Task;

namespace PlannerTasks.BLL.DTO
{
    public class StatusHistoryDTO
    {
        public Int32 StatusHistoryId { get; set; }
        public Status Status { get; set; }
        public Int32 TaskId { get; set; }
        public Task Task { get; set; }
    }
}
