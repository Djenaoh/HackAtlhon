namespace HackathonCisco
{
    class Interfaces
    {
        private TypeOfInterfaces typeOfInterfaces;
        private int preInterfaces;
        private int postInterfaces;
        private bool active = false;
        private string description;
        private IP ip;
        private IP mask;

        public TypeOfInterfaces TypeOfInterfaces { get => typeOfInterfaces; set => typeOfInterfaces = value; }
        public int PreInterfaces { get => preInterfaces; set => preInterfaces = value; }
        public int PostInterfaces { get => postInterfaces; set => postInterfaces = value; }
        public bool Active { get => active; set => active = value; }
        public string Description { get => description; set => description = value; }
        public IP Ip { get => ip; set => ip = value; }
        public IP Mask { get => mask; set => mask = value; }

        public Interfaces() { }

        public Interfaces(TypeOfInterfaces typeOfInterfaces, int preInterfaces, int postInterfaces, bool active, string description, IP ip, IP mask)
        {
            TypeOfInterfaces = typeOfInterfaces;
            PreInterfaces = preInterfaces;
            PostInterfaces = postInterfaces;
            Active = active;
            Description = description;
            Ip = ip;
            Mask = mask;
        }

        public Interfaces(TypeOfInterfaces typeOfInterfaces, int preInterfaces, int postInterfaces, bool active, string description, string ip, string mask)
        {
            TypeOfInterfaces = typeOfInterfaces;
            PreInterfaces = preInterfaces;
            PostInterfaces = postInterfaces;
            Active = active;
            Description = description;
            Ip = new IP(ip);
            Mask = new IP(mask);
        }

        public Interfaces AddPreInterfaces(int preInterfaces)
        {
            this.PreInterfaces = preInterfaces;
            return this;
        }
        public Interfaces AddPostInterfaces(int postInterfaces)
        {
            this.PostInterfaces = postInterfaces;
            return this;
        }
        public Interfaces AddTypeOfInterfaces(TypeOfInterfaces typeOfInterfaces)
        {
            this.TypeOfInterfaces = typeOfInterfaces;
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

        private string GetActiveString()
        {
            return this.Active ? "no shutdown" : "shutdown";
        }

        public new string ToString()
        {
            string res = "";
            res += "interface " + this.TypeOfInterfaces + " " + this.PreInterfaces + "/" + this.PostInterfaces + "\n";
            res += "ip address " + this.Ip.ToString() + " " + this.Mask.ToString() + "\n";
            res += "description " + this.Description + "\n";
            res += this.GetActiveString() + "\n";
            return res;
        }
    }
}