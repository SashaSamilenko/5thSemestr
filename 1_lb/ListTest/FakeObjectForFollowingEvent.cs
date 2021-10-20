using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CircularList;

namespace TestProject
{
    internal class FakeObjectForFollowingEvent
    {
        public CircularList<Int32> CircularList { get; private set; }
        public CircleEventArgs eventArgs { get; private set; }
        public FakeObjectForFollowingEvent()
        {
            CircularList = null;
            eventArgs = null;
        }
        public void FakeFollowingMethod(Object CircularList, CircleEventArgs eventArgs)
        {
            this.CircularList = (CircularList<Int32>)CircularList;
            this.eventArgs = eventArgs;
        }
    }
}
