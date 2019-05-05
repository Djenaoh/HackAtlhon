using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackathonCisco
{
    public class Cisco
    {

        // Attribut
        private string hostname;
        private string banner;
        private string securePriviledgeMode;
        private string secureConsoleMode;
        private bool enableSecureConsoleMode = false;
        private bool noIpDomaineLookup = false;
        private List<Interfaces> interfaces;

        // Setter/Getter
        public string Hostname { get => hostname; set => hostname = value; }
        public List<Interfaces> Interfaces { get => interfaces; set => interfaces = value; }
        public bool NoIpDomaineLookup { get => noIpDomaineLookup; set => noIpDomaineLookup = value; }
        public string SecurePriviledgeMode { get => securePriviledgeMode; set => securePriviledgeMode = value; }
        public string SecureConsoleMode { get => secureConsoleMode; set => secureConsoleMode = value; }
        public string Banner { get => banner; set => banner = value; }
        public bool EnableSecureConsoleMode { get => enableSecureConsoleMode; set => enableSecureConsoleMode = value; }

        // Contructor
        public Cisco() {}
        public Cisco(string hostname){this.Hostname = hostname;}

        // Methode to contruct the ToString
        private string GetEnableSecureConsoleMode()
        {
            return this.EnableSecureConsoleMode ? "login\n" : "";
        }
        private string GetSecureConsoleMode()
        {
            if (this.SecureConsoleMode != null)
            {
                string res = "line console 0\n";
                res += "password " + this.secureConsoleMode + "\n";
                res += this.GetEnableSecureConsoleMode();
                res += "exit\n";
                return res;
            } return "";
        }

        // Methode to add
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
        public Cisco AddSecureConsoleMode(string secureConsoleMode, bool enableSecureConsoleMode = false)
        {
            this.SecureConsoleMode = secureConsoleMode;
            this.EnableSecureConsoleMode = enableSecureConsoleMode;
            return this;
        }
        public Cisco AddEnableSecureConsoleMode(bool enableSecureConsoleMode)
        {
            this.EnableSecureConsoleMode = enableSecureConsoleMode;
            return this;
        }
        public Cisco AddNoIpDomaineLookup(bool noIpDomaineLookup)
        {
            this.NoIpDomaineLookup = noIpDomaineLookup;
            return this;
        }

        // Methode to remove
        public void RemoveInterfaces()
        {

        }

        // ToString Methode
        public new string ToString()
        {
            string res = "enable\n";
            res += "configure terminal\n";
            res += "hostname " + hostname + "\n";
            res += this.SecurePriviledgeMode != null ? "enable secret " + this.SecurePriviledgeMode + "\n" : "";
            res += this.GetSecureConsoleMode();
            res += this.Banner != null ? "banner motd #" + this.Banner + "#\n" : "";
            res += this.NoIpDomaineLookup ? "no ip domain-lookup\n" : "";
            if (this.Interfaces != null)
            {
                foreach (Interfaces inte in this.Interfaces)
                {
                    res += inte.ToString() + "exit\n";
                }
            }
            return res;
        }
    }
}
