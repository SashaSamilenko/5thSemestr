using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerTasks.DAL.Entities
{
    /// <summary>
    /// Class Employee
    /// Is entity of employee
    /// in team
    /// </summary>
    public class Employee
    {
        public Int32 EmployeeId { get; set; }
        public String FirstName { get; set; }
        public String SecondName { get; set; }
        public DateTime BirthDate { get; set; }
        public String HomePhone { get; set; }
        public virtual List<Task> Tasks { get; set; } // using 'virtual' for realization lazy loading
    }
}
