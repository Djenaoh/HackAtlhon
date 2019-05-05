namespace HackathonCisco
{
    public class Interfaces
    {

        // Attribut
        private Ports port;
        private bool active = false;
        private string description;
        private IP ip;
        private IP mask;

        // Setter/Getter
        public bool Active { get => active; set => active = value; }
        public string Description { get => description; set => description = value; }
        public IP Ip { get => ip; set => ip = value; }
        public IP Mask { get => mask; set => mask = value; }
        public Ports Port { get => port; set => port = value; }

        // Contructor
        public Interfaces() {}
        public Interfaces(Ports port, bool active, string description, IP ip, IP mask)
        {
            this.Port = port;
            Active = active;
            Description = description;
            Ip = ip;
            Mask = mask;
        }

        // Methode to add
        public Interfaces AddPort(Ports port)
        {
            this.Port = port;
            return this;
        }
        public Interfaces AddPort(TypeOfInterfaces typeOfPort, int prePort, int postPort)
        {
            this.Port = new Ports(typeOfPort, prePort, postPort);
            return this;
        }
        public Interfaces AddActive(bool active)
        {
            this.Active = active;
            return this;
        }
        public Interfaces AddDescription(string description)
        {
            this.Description = description;
            return this;
        }
        public Interfaces AddIp(IP ip)
        {
            this.Ip = ip;
            return this;
        }
        public Interfaces AddMask(IP mask)
        {
            this.Mask = mask;
            return this;
        }
        public Interfaces AddIp(string ip)
        {
            this.Ip = new IP(ip);
            return this;
        }
        public Interfaces AddMask(string mask)
        {
            this.Mask = new IP(mask);
            return this;
        }

        // ToString Methode
        public new string ToString()
        {
            string res = "";
            res += "interface " + this.Port.ToString() + "\n";
            res += "ip address " + this.Ip.ToString() + " " + this.Mask.ToString() + "\n";
            res += "description " + this.Description + "\n";
            res += this.Active ? "no shutdown\n" : "shutdown\n";
            return res;
        }
    }
}