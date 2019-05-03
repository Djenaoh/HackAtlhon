using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackathonCisco
{
    class Ports
    {

        // Attribut
        private TypeOfInterfaces typeOfInterfaces;
        private int preInterfaces;
        private int postInterfaces;

        // Setter/Getter
        public int PostInterfaces { get => postInterfaces; set => postInterfaces = value; }
        public int PreInterfaces { get => preInterfaces; set => preInterfaces = value; }
        public TypeOfInterfaces TypeOfInterfaces { get => typeOfInterfaces; set => typeOfInterfaces = value; }

        // Contructor
        public Ports() {}
        public Ports(TypeOfInterfaces typeOfInterfaces, int preInterfaces, int postInterfaces)
        {
            PostInterfaces = postInterfaces;
            PreInterfaces = preInterfaces;
            TypeOfInterfaces = typeOfInterfaces;
        }
        public Ports(TypeOfInterfaces typeOfInterfaces, string interfaces)
        {
            PostInterfaces = Convert.ToInt32(interfaces.Split('/')[1]); ;
            PreInterfaces = Convert.ToInt32(interfaces.Split('/')[0]); ;
            TypeOfInterfaces = typeOfInterfaces;
        }

        // Methode to add
        public Ports AddPreInterfaces(int preInterfaces)
        {
            this.PreInterfaces = preInterfaces;
            return this;
        }
        public Ports AddPostInterfaces(int postInterfaces)
        {
            this.PostInterfaces = postInterfaces;
            return this;
        }
        public Ports AddTypeOfInterfaces(TypeOfInterfaces typeOfInterfaces)
        {
            this.TypeOfInterfaces = typeOfInterfaces;
            return this;
        }

        // ToString Methode
        public new string ToString()
        {
            return this.TypeOfInterfaces + " " + this.PreInterfaces + "/" + this.PostInterfaces;
        }

    }
}
