using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerTasks.BLL.Infrastructure
{
    /// <summary>
    /// Exception about not valid parameter
    /// </summary>
    public class ValidationException : Exception
    {
        /// <summary>
        /// Constructor without parameters
        /// </summary>
        public ValidationException()
        {
        }

        /// <summary>
        /// Constructor with one parameter
        /// </summary>
        /// <param name="message">Message about exception</param>
        public ValidationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Constructor with two parameters
        /// </summary>
        /// <param name="message">Message about exception</param>
        /// <param name="inner">Inner exception</param>
        public ValidationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
