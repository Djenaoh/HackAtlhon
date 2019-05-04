using System;

namespace HackathonCisco
{
    class IP
    {

        // Attribut
        private int first;
        private int seconde;
        private int third;
        private int fourth;

        // Setter/Getter
        public int First { get => first; set => first = value; }
        public int Seconde { get => seconde; set => seconde = value; }
        public int Third { get => third; set => third = value; }
        public int Fourth { get => fourth; set => fourth = value; }

        // Contructor
        public IP(int first, int seconde, int third, int fourth)
        {
            this.First = first;
            this.Seconde = seconde;
            this.Third = third;
            this.Fourth = fourth;
        }
        public IP(string ip)
        {
            string[] lstIp = ip.Split('.');
            this.First = Convert.ToInt32(lstIp[0]);
            this.Seconde = Convert.ToInt32(lstIp[1]);
            this.Third = Convert.ToInt32(lstIp[2]);
            this.Fourth = Convert.ToInt32(lstIp[3]);
        }

        // ToString Methode
        public new string ToString()
        {
            return this.First.ToString() + "." + this.Seconde.ToString() + "." + this.Third.ToString() + "." + this.Fourth.ToString();
        }
    }
}