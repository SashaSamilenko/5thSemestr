using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Threading.Tasks;

namespace List
{
    internal class Item<T>
    {
        public T CurrentData { get; set; }
        public Item<T> Next { get; set; }
        public Item(T data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            CurrentData = data;
        }
        public override string ToString()
        {
            return CurrentData.ToString();
        }
    }
}
