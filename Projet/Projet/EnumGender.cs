using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet
{
    public class EnumGender
    {
        public static KeyValuePair<int, string> None = new KeyValuePair<int, string>(0, "None");
        public static KeyValuePair<int, string> Action = new KeyValuePair<int, string>(1, "Action");
        public static KeyValuePair<int, string> Drame = new KeyValuePair<int, string>(2, "Drame");
        public static KeyValuePair<int, string> Comedie = new KeyValuePair<int, string>(3, "Comedie");
        public static KeyValuePair<int, string> Animation = new KeyValuePair<int, string>(4, "Animation");
        public static KeyValuePair<int, string> Fantastique = new KeyValuePair<int, string>(5, "Fantastique");
        public static KeyValuePair<int, string> Horreur = new KeyValuePair<int, string>(5, "Horreur");

        public static KeyValuePair<int, string> stringToGender(string e)
        {
            KeyValuePair<int, string> res = EnumGender.None;
            switch (e)
            {
                case "Action":
                    res = EnumGender.Action;
                    break;
                case "Drame":
                    res = EnumGender.Drame;
                    break;
                case "Comedie":
                    res = EnumGender.Comedie;
                    break;
                case "Animation":
                    res = EnumGender.Animation;
                    break;
                case "Fantastique":
                    res = EnumGender.Fantastique;
                    break;
                case "Horreur":
                    res = EnumGender.Horreur;
                    break;
            }

            return res;
        }
    }
}
