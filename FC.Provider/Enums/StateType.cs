using System;
using System.Collections.Generic;
using System.Text;

namespace FC.Provider.Enums
{
    [Flags]
    public enum StateType : Int32
    {
        Waiting = 0,
        Accepted = 1,
        Deleted = 2,
        Confirmed = 3,
        Rejected = 4
    }
}
