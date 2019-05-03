﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackathonCisco
{
    class Cisco
    {

        private string hostname;
        private string banner;
        private string securePriviledgeMode;
        private string secureConsoleMode;
        private bool enableSecureConsoleMode = false;
        private bool noIpDomaineLookup = false;
        private List<Interfaces> interfaces;

        public string Hostname { get => hostname; set => hostname = value; }
        public List<Interfaces> Interfaces { get => interfaces; set => interfaces = value; }
        public bool NoIpDomaineLookup { get => noIpDomaineLookup; set => noIpDomaineLookup = value; }
        public string SecurePriviledgeMode { get => securePriviledgeMode; set => securePriviledgeMode = value; }
        public string SecureConsoleMode { get => secureConsoleMode; set => secureConsoleMode = value; }
        public string Banner { get => banner; set => banner = value; }
        public bool EnableSecureConsoleMode { get => enableSecureConsoleMode; set => enableSecureConsoleMode = value; }

        public Cisco(string hostname){this.Hostname = hostname;}

        private string GetNoIpDomaineLookup()
        {
            return this.NoIpDomaineLookup ? "no ip domain-lookup\n" : "";
        }
        private string GetEnableSecureConsoleMode()
        {
            return this.EnableSecureConsoleMode ? "login\n" : "";
        }
        private string GetSecureConsoleMode()
        {
            if (this.SecureConsoleMode != null)
            {
                string res = "line console 0\n";
                res += "password " + this.secureConsoleMode + "\n";
                res += this.GetEnableSecureConsoleMode();
                res += "exit\n";
                return res;
            } return "";
        }
        private string GetSecurePriviledgeMode()
        {
            if (this.SecurePriviledgeMode != null)
            {
                string res = "enable secret " + this.SecurePriviledgeMode + "\n";
                return res;
            }
            return "";
        }
        private string GetBanner()
        {
            if (this.Banner != null)
            {
                string res = "banner motd #" + this.Banner + "#\n";
                return res;
            }
            return "";
        }

        public Cisco AddInterfaces(params Interfaces[] list)
        {
            if (this.interfaces == null)
            {
                interfaces = new List<Interfaces>();
            }
            for (int i = 0; i < list.Length; i++)
            {
                this.Interfaces.Add(list[i]);
            }
            return this;
        }
        public Cisco AddHostname(string hostname)
        {
            this.Hostname = hostname;
            return this;
        }
        public Cisco AddBanner(string banner)
        {
            this.Banner = banner;
            return this;
        }
        public Cisco AddSecurePriviledgeMode(string securePriviledgeMode)
        {
            this.SecurePriviledgeMode = securePriviledgeMode;
            return this;
        }
        public Cisco AddSecureConsoleMode(string secureConsoleMode, bool enableSecureConsoleMode = false)
        {
            this.SecureConsoleMode = secureConsoleMode;
            this.EnableSecureConsoleMode = enableSecureConsoleMode;
            return this;
        }
        public Cisco AddEnableSecureConsoleMode(bool enableSecureConsoleMode)
        {
            this.EnableSecureConsoleMode = enableSecureConsoleMode;
            return this;
        }
        public Cisco AddNoIpDomaineLookup(bool noIpDomaineLookup)
        {
            this.NoIpDomaineLookup = noIpDomaineLookup;
            return this;
        }



        public void RemoveInterfaces()
        {

        }

        public new string ToString()
        {
            string res = "enable\n";
            res += "configure terminal\n";
            res += "hostname " + hostname + "\n";
            res += this.GetSecurePriviledgeMode();
            res += this.GetSecureConsoleMode();
            res += this.GetBanner();
            res += this.GetNoIpDomaineLookup();
            if (this.Interfaces != null)
            {
                foreach (Interfaces inte in this.Interfaces)
                {
                    res += inte.ToString() + "exit\n";
                }
            }

            return res;
        }

        public void SaveToTxt(string path, string fileName, bool append = false)
        {
            StreamWriter wr = new StreamWriter(path + fileName, append);
            wr.WriteLine(ToString());
            wr.Close();
        }

        public void ReadFromTxt(string path, string fileName)
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
                            this.AddSecureConsoleMode(Convert.ToString(lstTmp[1]));
                        }
                        else if ("login" == Convert.ToString(lstTmp[0]))
                        {
                            this.AddEnableSecureConsoleMode(true);
                        }
                        else if ("exit" == Convert.ToString(lstTmp[0]))
                        {
                            flag = 0;
                        }
                    }
                    else if ("hostname" == Convert.ToString(lstTmp[0]))
                    {
                        this.AddHostname(Convert.ToString(lstTmp[1]));
                    }
                    else if (lstTmp.Length > 1)
                    {
                        if ("enable" == Convert.ToString(lstTmp[0]) && "secret" == Convert.ToString(lstTmp[1]))
                        {
                            this.AddSecurePriviledgeMode(Convert.ToString(lstTmp[2]));
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
