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
        /// <summary>
        /// Identifier of employee
        /// </summary>
        public Int32 EmployeeId { get; set; }

        /// <summary>
        /// First name of employee
        /// </summary>
        public String FirstName { get; set; }

        /// <summary>
        /// Second name of employee
        /// </summary>
        public String SecondName { get; set; }

        /// <summary>
        /// Birth date of employee
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Phone number of employee
        /// </summary>
        public String PhoneNumber { get; set; }

        /// <summary>
        /// List of tasks.
        /// That property implements of relation one-to-many for Task
        /// </summary>
        public virtual List<Task> Tasks { get; set; } // using 'virtual' for realization lazy loading
    }
}
