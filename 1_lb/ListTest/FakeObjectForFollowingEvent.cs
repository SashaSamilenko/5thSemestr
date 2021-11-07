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
        internal Object CircularList { get; private set; }
        internal CircleEventArgs EventArgs { get; private set; }
        internal FakeObjectForFollowingEvent()
        {
            CircularList = null;
            EventArgs = null;
        }
        internal void FakeFollowingMethod(Object circularList, CircleEventArgs eventArgs)
        {
            CircularList = circularList;
            EventArgs = eventArgs;
        }
    }
}