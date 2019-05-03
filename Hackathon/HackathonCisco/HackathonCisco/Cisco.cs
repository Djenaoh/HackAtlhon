using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackathonCisco
{
    class Cisco
    {

        private string hostname;
        private List<Interfaces> interfaces;

        public string Hostname { get => hostname; set => hostname = value; }
        public List<Interfaces> Interfaces { get => interfaces; set => interfaces = value; }

        public void AddInterfaces(Interfaces inte)
        {
            if (this.interfaces == null)
            {
                interfaces = new List<Interfaces>();
            }
            this.Interfaces.Add(inte);
        }

        public void RemoveInterfaces()
        {

        }

        public string SaveToConf()
        {
            string res = "hostname " + hostname + "\n";
            foreach(Interfaces inte in this.Interfaces)
            {
                res += inte.ToString() + "\nexit\n";
            }

            return res;
        }
    }
}
