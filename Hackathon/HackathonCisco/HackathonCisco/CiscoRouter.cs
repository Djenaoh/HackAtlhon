using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackathonCisco
{
    public class CiscoRouter : Cisco
    {

        // Attribut
        private List<Routes> routes;

        // Setter/Getter
        public List<Routes> Routes { get => routes; set => routes = value; }

        // Contructor
        public CiscoRouter() : base() { }
        public CiscoRouter(string hostname) : base(hostname) {}

        // Base Methode to add
        public new CiscoRouter AddInterfaces(params Interfaces[] list)
        {
            base.AddInterfaces(list);
            return this;
        }
        public new CiscoRouter AddHostname(string hostname)
        {
            base.AddHostname(hostname);
            return this;
        }
        public new CiscoRouter AddBanner(string banner)
        {
            base.AddBanner(banner);
            return this;
        }
        public new CiscoRouter AddSecurePriviledgeMode(string securePriviledgeMode)
        {
            base.AddSecurePriviledgeMode(securePriviledgeMode);
            return this;
        }
        public new CiscoRouter AddSecureConsoleMode(string secureConsoleMode, bool enableSecureConsoleMode = false)
        {
            base.AddSecureConsoleMode(secureConsoleMode, enableSecureConsoleMode);
            return this;
        }
        public new CiscoRouter AddEnableSecureConsoleMode(bool enableSecureConsoleMode)
        {
            this.AddEnableSecureConsoleMode(enableSecureConsoleMode);
            return this;
        }
        public new CiscoRouter AddNoIpDomaineLookup(bool noIpDomaineLookup)
        {
            base.AddNoIpDomaineLookup(noIpDomaineLookup);
            return this;
        }

        // Router Methode
        public CiscoRouter AddRoutes(params Routes[] list)
        {
            if (this.routes == null)
            {
                routes = new List<Routes>();
            }
            for (int i = 0; i < list.Length; i++)
            {
                this.Routes.Add(list[i]);
            }
            return this;
        }
        public void RemoveRoutes()
        {

        }

        // ToString Methode
        public new string ToString()
        {
            string res = "";
            if (this.Routes != null)
            {
                foreach (Routes roue in this.Routes)
                {
                    res += roue.ToString();
                }
            }
            return "! routeur\n" + base.ToString() + res;
        }

    }
}
