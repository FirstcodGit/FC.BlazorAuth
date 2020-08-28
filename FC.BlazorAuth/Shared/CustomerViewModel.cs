using System;
using System.Collections.Generic;
using System.Text;

namespace FC.BlazorAuth.Shared
{
    public class CustomerViewModel
    {
        public string Token { get; set; }
        public string Expired { get; set; }
        public bool Auth { get; set; }
    }
}
