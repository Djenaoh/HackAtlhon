using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackathonCisco
{
    class Cisco
    {

        private string hostname;
        private string banner;
        private string securePriviledgeMode;
        private string secureConsoleMode;
        private bool noIpDomaineLookup = false;
        private List<Interfaces> interfaces;

        public string Hostname { get => hostname; set => hostname = value; }
        public List<Interfaces> Interfaces { get => interfaces; set => interfaces = value; }
        public bool NoIpDomaineLookup { get => noIpDomaineLookup; set => noIpDomaineLookup = value; }
        public string SecurePriviledgeMode { get => securePriviledgeMode; set => securePriviledgeMode = value; }
        public string SecureConsoleMode { get => secureConsoleMode; set => secureConsoleMode = value; }
        public string Banner { get => banner; set => banner = value; }

        public Cisco(string hostname){this.Hostname = hostname;}

        private string GetNoIpDomaineLookup()
        {
            return this.NoIpDomaineLookup ? "no ip domain-lookup\n" : "";
        }
        private string GetSecureConsoleMode()
        {
            if (this.SecureConsoleMode != null)
            {
                string res = "line console 0\n";
                res += "password " + this.secureConsoleMode + "\n";
                res += "login\n";
                res += "exit\n";
                return res;
            } return "";
        }
        private string GetSecurePriviledgeMode()
        {
            if (this.SecurePriviledgeMode != null)
            {
                string res = "enable secret " + this.SecurePriviledgeMode + "\n";
                return res;
            }
            return "";
        }
        private string GetBanner()
        {
            if (this.Banner != null)
            {
                string res = "banner motd #" + this.Banner + "#\n";
                return res;
            }
            return "";
        }

        public Cisco AddInterfaces(params Interfaces[] list)
        {
            if (this.interfaces == null)
            {
                interfaces = new List<Interfaces>();
            }
            for (int i = 0; i < list.Length; i++)
            {
                this.Interfaces.Add(list[i]);
            }
            return this;
        }
        public Cisco AddHostname(string hostname)
        {
            this.Hostname = hostname;
            return this;
        }
        public Cisco AddBanner(string banner)
        {
            this.Banner = banner;
            return this;
        }
        public Cisco AddSecurePriviledgeMode(string securePriviledgeMode)
        {
            this.SecurePriviledgeMode = securePriviledgeMode;
            return this;
        }
        public Cisco AddSecureConsoleMode(string secureConsoleMode)
        {
            this.SecureConsoleMode = secureConsoleMode;
            return this;
        }
        public Cisco AddNoIpDomaineLookup(bool noIpDomaineLookup)
        {
            this.NoIpDomaineLookup = noIpDomaineLookup;
            return this;
        }



        public void RemoveInterfaces()
        {

        }

        public string SaveToConf()
        {
            string res = "enable\n";
            res += "configure terminal \n";
            res += "hostname " + hostname + "\n";
            res += this.GetSecurePriviledgeMode();
            res += this.GetSecureConsoleMode();
            res += this.GetBanner();
            res += this.GetNoIpDomaineLookup();
            foreach (Interfaces inte in this.Interfaces)
            {
                res += inte.ToString() + "exit\n";
            }

            return res;
        }

        public void SaveToTxt()
        {
            StreamWriter wr = new StreamWriter(MainWindow.PATH + MainWindow.FILE_NAME, false);
            wr.WriteLine(SaveToConf());
            wr.Close();
        }
    }
}
