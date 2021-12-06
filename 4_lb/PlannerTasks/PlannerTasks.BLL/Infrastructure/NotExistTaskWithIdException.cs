using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerTasks.BLL.Infrastructure
{
    /// <summary>
    /// Exception about not existing task with given Id
    /// </summary>
    public class NotExistTaskWithIdException: Exception
    {
        /// <summary>
        /// Constructor without parameters
        /// </summary>
        public NotExistTaskWithIdException()
        {
        }

        /// <summary>
        /// Constructor with one parameter
        /// </summary>
        /// <param name="message">Message about exception</param>
        public NotExistTaskWithIdException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Constructor with two parameters
        /// </summary>
        /// <param name="message">Message about exception</param>
        /// <param name="inner">Inner exception</param>
        public NotExistTaskWithIdException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
