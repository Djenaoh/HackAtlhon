using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackathonCisco
{
    class Routes
    {

        // Attribut
        private Ports port;
        private IP ip;
        private IP mask;
        private IP ipNext;

        // Setter/Getter
        public IP IpNext { get => ipNext; set => ipNext = value; }
        public IP Mask { get => mask; set => mask = value; }
        public IP Ip { get => ip; set => ip = value; }
        public Ports Port { get => port; set => port = value; }

        // Contructor
        public Routes(IP ip, IP mask, Ports port, IP ipNext)
        {
            IpNext = ipNext;
            Mask = mask;
            Ip = ip;
            this.Port = port;
        }
        public Routes(IP ip, IP mask, IP ipNext)
        {
            IpNext = ipNext;
            Mask = mask;
            Ip = ip;
        }
        public Routes(IP ip, IP mask, Ports port)
        {
            Mask = mask;
            Ip = ip;
            this.Port = port;
        }

        // Methode to add
        public Routes AddPort(Ports port)
        {
            this.Port = port;
            return this;
        }
        public Routes AddPort(TypeOfInterfaces typeOfPort, int prePort, int postPort)
        {
            this.Port = new Ports(typeOfPort, prePort, postPort);
            return this;
        }
        public Routes AddIp(IP ip)
        {
            this.Ip = ip;
            return this;
        }
        public Routes AddIpNext(IP ipNext)
        {
            this.IpNext = ipNext;
            return this;
        }
        public Routes AddMask(IP mask)
        {
            this.Mask = mask;
            return this;
        }
        public Routes AddIp(string ip)
        {
            this.Ip = new IP(ip);
            return this;
        }
        public Routes AddIpNext(string ipNext)
        {
            this.IpNext = new IP(ipNext);
            return this;
        }
        public Routes AddMask(string mask)
        {
            this.Mask = new IP(mask);
            return this;
        }

        // ToString Methode
        public new string ToString()
        {
            string res = "";
            res += "ip route " + this.Ip.ToString() + " " + this.Mask.ToString();
            res += this.Port != null ? " " + this.Port.ToString() : "";
            res += this.IpNext != null ? " " + this.IpNext.ToString() : "";
            return res + "\n";
        }
    }
}
