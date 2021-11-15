using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerTasks.DAL.Entities
{
    public class StatusHistory
    {
        public Int32 StatusHistoryId { get; set; }
        public Status Status { get; set; }
        public Int32 TaskId { get; set; }
        public Task Task { get; set; }
    }
}
