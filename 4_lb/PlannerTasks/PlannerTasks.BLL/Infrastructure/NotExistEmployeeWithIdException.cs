using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerTasks.BLL.Infrastructure
{
    /// <summary>
    /// Exception about not existing employee with given Id
    /// </summary>
    public class NotExistEmployeeWithIdException: Exception
    {
        /// <summary>
        /// Constructor without parameters
        /// </summary>
        public NotExistEmployeeWithIdException()
        {
        }

        /// <summary>
        /// Constructor with one parameter
        /// </summary>
        /// <param name="message">Message about exception</param>
        public NotExistEmployeeWithIdException(string message) 
            :base(message)
        {
        }

        /// <summary>
        /// Constructor with two parameters
        /// </summary>
        /// <param name="message">Message about exception</param>
        /// <param name="inner">Inner exception</param>
        public NotExistEmployeeWithIdException(string message, Exception inner)
            :base(message, inner)
        {
        }
    }
}
