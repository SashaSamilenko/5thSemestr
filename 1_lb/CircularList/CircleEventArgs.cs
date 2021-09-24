using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace List
{
    /// <summary>
    /// Class CircleEventArgs
    /// This class is follower of EventArgs
    /// </summary>
    public class CircleEventArgs: EventArgs
    {
        private int count;
        /// <summary>
        /// This is property of count
        /// </summary>
        public int Count
        { 
            get=>count;
        }
        /// <summary>
        /// This is  constructure with parameter for class "CircleEventArgs"
        /// </summary>
        /// <param name="count"></param>
        public CircleEventArgs(int count)
        {
            this.count = count;
        }
    }
}
