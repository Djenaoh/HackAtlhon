using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackathonCisco
{
    class CiscoSwitch : Cisco
    {

        // Contructor
        public CiscoSwitch() : base() {}
        public CiscoSwitch(string hostname) : base(hostname) {}

        // Base Methode to add
        public new CiscoSwitch AddInterfaces(params Interfaces[] list)
        {
            base.AddInterfaces(list);
            return this;
        }
        public new CiscoSwitch AddHostname(string hostname)
        {
            base.AddHostname(hostname);
            return this;
        }
        public new CiscoSwitch AddBanner(string banner)
        {
            base.AddBanner(banner);
            return this;
        }
        public new CiscoSwitch AddSecurePriviledgeMode(string securePriviledgeMode)
        {
            base.AddSecurePriviledgeMode(securePriviledgeMode);
            return this;
        }
        public new CiscoSwitch AddSecureConsoleMode(string secureConsoleMode, bool enableSecureConsoleMode = false)
        {
            base.AddSecureConsoleMode(secureConsoleMode, enableSecureConsoleMode);
            return this;
        }
        public new CiscoSwitch AddEnableSecureConsoleMode(bool enableSecureConsoleMode)
        {
            this.AddEnableSecureConsoleMode(enableSecureConsoleMode);
            return this;
        }
        public new CiscoSwitch AddNoIpDomaineLookup(bool noIpDomaineLookup)
        {
            base.AddNoIpDomaineLookup(noIpDomaineLookup);
            return this;
        }

        public new string ToString()
        {
            string res = "";
            return base.ToString() + res;
        }
    }
}
