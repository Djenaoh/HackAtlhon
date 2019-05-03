using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackathonCisco
{
    class Routes
    {
        private TypeOfInterfaces typeOfInterfaces;
        private int preInterfaces = -1;
        private int postInterfaces = -1;
        private IP ip;
        private IP mask;
        private IP ipNext;

        public IP IpNext { get => ipNext; set => ipNext = value; }
        public int PostInterfaces { get => postInterfaces; set => postInterfaces = value; }
        public int PreInterfaces { get => preInterfaces; set => preInterfaces = value; }
        public IP Mask { get => mask; set => mask = value; }
        public IP Ip { get => ip; set => ip = value; }
        public TypeOfInterfaces TypeOfInterfaces { get => typeOfInterfaces; set => typeOfInterfaces = value; }

        public Routes(IP ip, IP mask, TypeOfInterfaces typeOfInterfaces, int postInterfaces, int preInterfaces, IP ipNext)
        {
            IpNext = ipNext;
            PostInterfaces = postInterfaces;
            PreInterfaces = preInterfaces;
            Mask = mask;
            Ip = ip;
            TypeOfInterfaces = typeOfInterfaces;
        }

        public Routes(string ip, string mask, TypeOfInterfaces typeOfInterfaces, int postInterfaces, int preInterfaces, string ipNext)
        {
            IpNext = new IP(ipNext);
            PostInterfaces = postInterfaces;
            PreInterfaces = preInterfaces;
            Mask = new IP(mask);
            Ip = new IP(ip);
            TypeOfInterfaces = typeOfInterfaces;
        }

        public Routes(IP ip, IP mask, IP ipNext)
        {
            IpNext = ipNext;
            Mask = mask;
            Ip = ip;
        }

        public Routes(string ip, string mask, string ipNext)
        {
            IpNext = new IP(ipNext);
            Mask = new IP(mask);
            Ip = new IP(ip);
        }

        public Routes(IP ip, IP mask, TypeOfInterfaces typeOfInterfaces, int postInterfaces, int preInterfaces)
        {
            Mask = mask;
            Ip = ip;
            PostInterfaces = postInterfaces;
            PreInterfaces = preInterfaces;
            TypeOfInterfaces = typeOfInterfaces;
        }

        public Routes(string ip, string mask, TypeOfInterfaces typeOfInterfaces, int postInterfaces, int preInterfaces)
        {
            Mask = new IP(mask);
            Ip = new IP(ip);
            PostInterfaces = postInterfaces;
            PreInterfaces = preInterfaces;
            TypeOfInterfaces = typeOfInterfaces;
        }

        public Routes AddPreInterfaces(int preInterfaces)
        {
            this.PreInterfaces = preInterfaces;
            return this;
        }
        public Routes AddPostInterfaces(int postInterfaces)
        {
            this.PostInterfaces = postInterfaces;
            return this;
        }
        public Routes AddTypeOfInterfaces(TypeOfInterfaces typeOfInterfaces)
        {
            this.TypeOfInterfaces = typeOfInterfaces;
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


        public new string ToString()
        {
            string res = "";
            res += "ip route " + this.Ip.ToString() + " " + this.Mask.ToString();
            if (this.PreInterfaces != -1)
            {
                res += " " + this.TypeOfInterfaces + " " + this.PreInterfaces + "/" + this.PostInterfaces;
            }
            if (this.IpNext != null)
            {
                res += " " + this.IpNext.ToString();
            }
            return res + "\n";
        }
    }
}
