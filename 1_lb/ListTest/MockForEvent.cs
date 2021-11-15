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
        public CircularList<T> Object;
        public CircleEventArgs mess;
        public MockForEvent()
        {
            Object = new CircularList<T>();
        }
        public MockForEvent(T data)
        {
            Object = new CircularList<T>(data);
        }
        public void VerifyMethod(object sender, EventArgs e)
        {
            mess = (CircleEventArgs)e;
        }
    }
}
