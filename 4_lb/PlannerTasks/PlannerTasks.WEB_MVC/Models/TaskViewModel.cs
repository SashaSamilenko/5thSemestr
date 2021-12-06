using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerTasks.WEB_MVC.Models
{
    public class TaskViewModel
    {
        public Int32? TaskId { get; set; }
        public String Description { get; set; }
        public Int32 TimeExecution { get; set; }
        public Int32? Status { get; set; }
        public Int32 CurrentPriority { get; set; }
        public DateTime? StartTime { get; set; }
        public Int32 EmployeeId { get; set; }
    }
}
