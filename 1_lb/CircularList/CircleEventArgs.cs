using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircularList
{
    /// <summary>
    /// Class CircleEventArgs.
    /// This class is follower of EventArgs and contains recived information.
    /// </summary>
    public class CircleEventArgs: EventArgs
    {
        /// <summary>
        /// Private field of recived information
        /// </summary>
        private readonly Int32 count;

        /// <summary>
        /// This is property of count
        /// </summary>
        public Int32 Count
        {
            get
            {
                return count;
            }
        }

        /// <summary>
        /// This is  constructure with parameter for class "CircleEventArgs"
        /// </summary>
        /// <param name="count"></param>
        public CircleEventArgs(Int32 count)
        {
            this.count = count;
        }
    }
}
