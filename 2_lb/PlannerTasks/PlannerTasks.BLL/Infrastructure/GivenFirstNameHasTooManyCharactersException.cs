using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerTasks.BLL.Infrastructure
{
    /// <summary>
    /// Exception about consisting of first name from too many characters
    /// </summary>
    public class GivenFirstNameHasTooManyCharactersException: Exception
    {
        /// <summary>
        /// Constructor without parameters
        /// </summary>
        public GivenFirstNameHasTooManyCharactersException()
        {
        }

        /// <summary>
        /// Constructor with one parameter
        /// </summary>
        /// <param name="message">Message about exception</param>
        public GivenFirstNameHasTooManyCharactersException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Constructor with two parameters
        /// </summary>
        /// <param name="message">Message about exception</param>
        /// <param name="inner">Inner exception</param>
        public GivenFirstNameHasTooManyCharactersException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
