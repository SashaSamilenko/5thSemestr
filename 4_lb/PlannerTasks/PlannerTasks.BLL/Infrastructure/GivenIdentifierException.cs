using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerTasks.BLL.Infrastructure
{
    /// <summary>
    /// Exception about invalid identifier
    /// </summary>
    public class GivenIdentifierException: Exception
    {
        /// <summary>
        /// Constructor without parameters
        /// </summary>
        public GivenIdentifierException()
        {
        }

        /// <summary>
        /// Constructor with one parameter
        /// </summary>
        /// <param name="message">Message about exception</param>
        public GivenIdentifierException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Constructor with two parameters
        /// </summary>
        /// <param name="message">Message about exception</param>
        /// <param name="inner">Inner exception</param>
        public GivenIdentifierException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
