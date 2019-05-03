using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackathonCisco
{
    class ReadWrite
    {
        // Reading/Writing Methode
        public static void SaveToTxt(CiscoSwitch ob, string path, string fileName, bool append = false)
        {
            StreamWriter wr = new StreamWriter(path + fileName, append);
            wr.WriteLine(ob.ToString());
            wr.Close();
        }
        public static void SaveToTxt(CiscoRouter ob, string path, string fileName, bool append = false)
        {
            StreamWriter wr = new StreamWriter(path + fileName, append);
            wr.WriteLine(ob.ToString());
            wr.Close();
        }

        public static void ReadFromTxt(CiscoRouter ob, string path, string fileName)
        {
            if (File.Exists(path + fileName))
            {
                var lstEnum = EnumCiscoWords.GetValues(typeof(EnumCiscoWords));
                StreamReader reader = new StreamReader(path + fileName);
                int flag = 0;
                while (!reader.EndOfStream)
                {
                    string tmp = reader.ReadLine();
                    string[] lstTmp = tmp.Split(' ');
                    if (flag == 1)
                    {
                        if ("password" == Convert.ToString(lstTmp[0]))
                        {
                            ob.AddSecureConsoleMode(Convert.ToString(lstTmp[1]));
                        }
                        else if ("login" == Convert.ToString(lstTmp[0]))
                        {
                            ob.AddEnableSecureConsoleMode(true);
                        }
                        else if ("exit" == Convert.ToString(lstTmp[0]))
                        {
                            flag = 0;
                        }
                    }
                    else if ("hostname" == Convert.ToString(lstTmp[0]))
                    {
                        ob.AddHostname(Convert.ToString(lstTmp[1]));
                    }
                    else if (lstTmp.Length > 1)
                    {
                        if ("enable" == Convert.ToString(lstTmp[0]) && "secret" == Convert.ToString(lstTmp[1]))
                        {
                            ob.AddSecurePriviledgeMode(Convert.ToString(lstTmp[2]));
                        }
                        else if ("line" == Convert.ToString(lstTmp[0]) && "console" == Convert.ToString(lstTmp[1]) && "0" == Convert.ToString(lstTmp[2]))
                        {
                            flag = 1;
                        }
                        else if ("banner" == Convert.ToString(lstTmp[0]) && "motd" == Convert.ToString(lstTmp[1]))
                        {
                            string ban = Convert.ToString(lstTmp[2]);
                        }
                    }



                }
                reader.Close();
            }
        }
    }
}
