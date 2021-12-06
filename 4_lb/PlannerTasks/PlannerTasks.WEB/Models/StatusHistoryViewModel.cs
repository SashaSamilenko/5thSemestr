using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlannerTasks.DAL.Entities;
using Task = System.Threading.Tasks.Task;

namespace PlannerTasks.BLL.DTO
{
    public class StatusHistoryViewModel
    {
        public Int32 StatusHistoryId { get; set; }
        public Int32 Status { get; set; }
        public DateTime DateAppearOfStatus { get; set; }
        public Int32 TaskId { get; set; }
    }
}
