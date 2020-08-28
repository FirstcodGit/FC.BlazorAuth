using System;
using System.Collections.Generic;
using System.Text;

namespace FC.Provider.Enums
{
    [Flags]
    public enum AuthenticationType : Int32
    {
        None = 0,
        GoogleAuthentication = 1,
        PhoneAuthentication = 2,
        EmailAuthentication = 3
    }
}