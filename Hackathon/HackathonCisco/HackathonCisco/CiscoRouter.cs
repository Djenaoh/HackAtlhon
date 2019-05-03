using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackathonCisco
{
    class CiscoRouter : Cisco
    {

        public CiscoRouter() : base("Routeur") { }

        public CiscoRouter(string hostname) : base(hostname)
        {


        }

        public new string ToString()
        {
            string res = "";

            return base.ToString() + res;
        }

    }
}
