using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackathonCisco
{
    class CiscoSwitch : Cisco
    {

        public CiscoSwitch() : base("Switch") {}

        public CiscoSwitch(string hostname) : base(hostname)
        {
            

        }


        public new string ToString()
        {
            string res = "";

            return base.ToString() + res;
        }
    }
}
