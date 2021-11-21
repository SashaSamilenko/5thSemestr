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
        NotStarted=0,
        OnExecution=1,
        OnTesting=2,
        Expired=-1,
        Done=3
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
        /// <summary>
        /// Identifier of task
        /// </summary>
        public Int32 TaskId { get; set; }

        /// <summary>
        /// Description of task
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// Time execution of task
        /// </summary>
        public TimeSpan TimeExecution { get; set; }

        /// <summary>
        /// Time of starting execute of task
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// Status of task
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// Priority of task
        /// </summary>
        public Priority CurrentPriority { get; set; }

        /// <summary>
        /// Identifier of employee
        /// </summary>
        public Int32 EmployeeId { get; set; }

        /// <summary>
        /// That property implements relation many-to-one for Employee
        /// </summary>
        public Employee Employee { get; set; }

        /// <summary>
        /// That property implements relation one-to-many for HistoryStatus
        /// </summary>
        public virtual List<StatusHistory> HistoryStatuses { get; set; }
    }
}
