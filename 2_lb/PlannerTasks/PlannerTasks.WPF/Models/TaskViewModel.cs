using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerTasks.WPF.Models
{
    public class TaskViewModel
    {
        public Int32 EmployeeId { get; set; }
        public String Description { get; set; }
        public TimeSpan TimeExecution { get; set; }
        public Int32 Status { get; set; }
        public Int32 Priority { get; set; }
    }
}
