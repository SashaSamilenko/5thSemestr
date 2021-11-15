using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerTasks.DAL.Entities
{
    /// <summary>
    /// Enum Status consists status of execution of task
    /// </summary>
    public enum Status
    {
        NotStarted,
        OnExecution,
        OnTesting,
        Done
    }

    /// <summary>
    /// Enum Priority consists priority of task`s execution
    /// </summary>
    public enum Priority
    {
        Low,
        Medium,
        High
    }

    /// <summary>
    /// Class Task
    /// Is entity for task of employee 
    /// </summary>
    public class Task
    {
        public Int32 TaskId { get; set; }
        public String Description { get; set; }
        public TimeSpan TimeExecution { get; set; }
        public DateTime? StartTime { get; set; }
        public Status Status { get; set; }
        public Priority CurrentPriority { get; set; }
        public Int32 EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public virtual List<StatusHistory> HistoryStatus { get; set; }
    }
}
