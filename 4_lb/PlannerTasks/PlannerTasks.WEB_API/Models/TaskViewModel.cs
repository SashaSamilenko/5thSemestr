using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerTasks.WEB_API.Models
{
    public class TaskViewModel
    {
        public Int32 TaskId { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "Мінімальна довжина опису становить 10 символів")]
        public String Description { get; set; }

        [Required]
        [Range(1, 24, ErrorMessage = "Значення може приймати бути від 1 до 24")]
        [Display(Name = "Очікуваний час виконання")]
        public Int32 TimeExecution { get; set; }
        public Int32 Status { get; set; }

        [Required]
        [Range(1, 3, ErrorMessage = "Значення може приймати бути від 1 до 3")]
        [Display(Name = "Пріорітет задачі(від 1 до 3)")]
        public Int32 CurrentPriority { get; set; }
        public DateTime StartTime { get; set; }
        public Int32 EmployeeId { get; set; }
    }
}
