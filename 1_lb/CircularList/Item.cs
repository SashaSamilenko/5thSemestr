using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Threading.Tasks;

namespace CircularList
{
    /// <summary>
    /// Class Item
    /// This class presents node!
    /// This class desribe an node with uncertain type!
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class Item<T>
    {
        /// <summary>
        /// This field is about current data
        /// </summary>
        public T CurrentData { get; set; }

        /// <summary>
        /// This field indecates to the previous elements
        /// </summary>
        public Item<T> Previous { get; set; }

        /// <summary>
        /// This field indecates to the next elements
        /// </summary>
        public Item<T> Next { get; set; }

        /// <summary>
        /// This is constructure
        /// </summary>
        /// <param name="data"></param>
        public Item(T data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            CurrentData = data;
        }
    }
}
