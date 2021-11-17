using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerTasks.DAL.Entities
{
    /// <summary>
    /// Class StatusHistory
    /// That implements entity of status history.
    /// </summary>
    public class StatusHistory
    {
        /// <summary>
        /// Identifier of StatusHistory
        /// </summary>
        public Int32 StatusHistoryId { get; set; }

        /// <summary>
        /// Status of task
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// Date appear of task`s status
        /// </summary>
        public DateTime DateAppearOfStatus { get; set; }

        /// <summary>
        /// Identifier of task
        /// </summary>
        public Int32 TaskId { get; set; }

        /// <summary>
        /// Reference to task.
        /// That property implements relation many-to-one for Task
        /// </summary>
        public Task Task { get; set; }
    }
}
