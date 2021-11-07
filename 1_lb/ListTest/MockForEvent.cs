using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CircularList;

namespace TestProject
{
    internal class MockForEvent<T>
    {
        internal CircularList<T> Object;

        internal MockForEvent()
        {
            Object = new CircularList<T>();
        }

        internal MockForEvent(T data)
        {
            Object = new CircularList<T>(data);
        }
    }
}
