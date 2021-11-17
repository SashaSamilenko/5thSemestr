using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerTasks.BLL.Infrastructure
{
    /// <summary>
    /// Exception about consisting of phone number from too many characters
    /// </summary>
    public class GivenPhoneNumberHasTooManyCharactersException: Exception
    {
        /// <summary>
        /// Constructor without parameters
        /// </summary>
        public GivenPhoneNumberHasTooManyCharactersException()
        {
        }

        /// <summary>
        /// Constructor with one parameter
        /// </summary>
        /// <param name="message">Message about exception</param>
        public GivenPhoneNumberHasTooManyCharactersException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Constructor with two parameters
        /// </summary>
        /// <param name="message">Message about exception</param>
        /// <param name="inner">Inner exception</param>
        public GivenPhoneNumberHasTooManyCharactersException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
