using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerTasks.WEB_MVC.Models
{
    public class TaskViewModel
    {
        public Int32 TaskId { get; set; }

        [StringLength(50, MinimumLength = 10, ErrorMessage = "Довжина опису має складатися принаймні з 10 символів")]
        public String Description { get; set; }

        public Int32 TimeExecution { get; set; }
        public Int32 Status { get; set; }

        [Range(1, 3)]
        public Int32 CurrentPriority { get; set; }
        public DateTime StartTime { get; set; }
        public Int32 EmployeeId { get; set; }
    }
}
