using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlannerTasks.BLL.DTO;
using Task = System.Threading.Tasks.Task;

namespace PlannerTasks.WEB_MVC.Models
{
    public class StatusHistoryViewModel
    {
        public Int32 StatusHistoryId { get; set; }
        public Int32 Status { get; set; }
        public DateTime DateAppearOfStatus { get; set; }
        public Int32 TaskId { get; set; }
    }
}
