namespace Projet
{
    public enum EnumGenre
    {
        None,
        Action,
        Drame,
        Comedie,
        Animation,
        Fantastique,
        Horreur,
        Crime

    }

    public class MyConvert
    {
        public static EnumGenre stringToGender(string e)
        {
            EnumGenre res = EnumGenre.None;
            switch (e)
            {
                case "Action":
                    res = EnumGenre.Action;
                    break;
                case "Drame":
                    res = EnumGenre.Drame;
                    break;
                case "Comedie":
                    res = EnumGenre.Comedie;
                    break;
                case "Animation":
                    res = EnumGenre.Animation;
                    break;
                case "Fantastique":
                    res = EnumGenre.Fantastique;
                    break;
                case "Horreur":
                    res = EnumGenre.Horreur;
                    break;
                case "Crime":
                    res = EnumGenre.Crime;
                    break;
            }

            return res;
        }
    }
}