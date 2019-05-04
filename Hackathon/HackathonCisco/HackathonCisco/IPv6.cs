using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackathonCisco
{
    class IPv6
    {

        // Attribut
        private string first;
        private string seconde;
        private string third;
        private string fourth;
        private string fifth;
        private string sixth;
        private string seventh;
        private string eighth;
        private int mask = -1;

        // Setter/Getter
        public string First { get => first; set => first = value; }
        public string Seconde { get => seconde; set => seconde = value; }
        public string Third { get => third; set => third = value; }
        public string Fourth { get => fourth; set => fourth = value; }
        public string Fifth { get => fifth; set => fifth = value; }
        public string Sixth { get => sixth; set => sixth = value; }
        public string Seventh { get => seventh; set => seventh = value; }
        public string Eighth { get => eighth; set => eighth = value; }
        public int Mask { get => mask; set => mask = value; }

        // Contructor
        public IPv6(string first, string seconde, string third, string fourth, string fifth, string sixth, string seventh, string eighth, int mask = -1)
        {
            this.First = first;
            this.Seconde = seconde;
            this.Third = third;
            this.Fourth = fourth;
            this.Fifth = fifth;
            this.Sixth = sixth;
            this.Seventh = seventh;
            this.Eighth = eighth;
            this.Mask = mask;
        }
        public IPv6(string ipv6, int mask = -1)
        {
            string[] lstIp = ipv6.Split(':');
            this.First = lstIp[0];
            this.Seconde = lstIp[1];
            this.Third = lstIp[2];
            this.Fourth = lstIp[3];
            this.Fifth = lstIp[4];
            this.Sixth = lstIp[5];
            this.Seventh = lstIp[6];
            this.Eighth = lstIp[7];
            this.Mask = mask;
        }

        // ToString Methode
        public new string ToString()
        {
            string res = this.First + ":" + this.Seconde + ":" + this.Third + ":" + this.Fourth + ":" + this.Fifth + ":" + this.Sixth + ":" + this.Seventh + ":" + this.Eighth;
            res += this.Mask != -1 ? "/" + this.Mask : "";
            return res;
        }
    }
}