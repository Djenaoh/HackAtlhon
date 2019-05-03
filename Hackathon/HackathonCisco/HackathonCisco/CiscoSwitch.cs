using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackathonCisco
{
    class CiscoSwitch : Cisco
    {


        public new string SaveToConf()
        {
            string res = "test";

            return base.SaveToConf() + res;
        }
    }
}
