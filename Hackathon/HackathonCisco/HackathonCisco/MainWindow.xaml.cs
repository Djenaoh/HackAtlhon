using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HackathonCisco
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string PATH = System.AppDomain.CurrentDomain.BaseDirectory;
        public static string FILE_NAME = "scriptTEST.txt";

        public MainWindow()
        {
            InitializeComponent();
            // Interfaces
            Interfaces int01 = new Interfaces(new Ports(TypeOfInterfaces.FastEthernet, 0, 1), true, "Reseau A", new IP ("10.10.10.254"), new IP("255.255.255.0"));
            Interfaces int02 = new Interfaces();
            int02.AddPort(new Ports())
                .AddDescription("Super reseau 2")
                .AddIp("192.168.0.1")
                .AddMask(new IP(255, 255, 0, 0));
            // Routes
            Routes rou01 = new Routes(new IP ("10.10.10.0"), new IP("255.255.255.0"), new Ports(TypeOfInterfaces.FastEthernet, 0, 1));
            Routes rou02 = new Routes(new IP("0.0.0.0"), new IP("0.0.0.0"), new IP("10.10.10.0"));
            Routes rou03 = new Routes(new IP("10.20.20.0"), new IP("255.255.255.0"), new Ports(TypeOfInterfaces.FastEthernet, 0, 1), new IP("10.10.10.0"));
            // Router
            CiscoRouter routeur = new CiscoRouter("routeur001");
            routeur.AddBanner("Super Routeur")
                .AddInterfaces(int01, int02)
                .AddNoIpDomaineLookup(true)
                .AddSecureConsoleMode("cisco", true)
                .AddSecurePriviledgeMode("cisco")
                .AddRoutes(rou01, rou02, rou03);

            MessageBox.Show(routeur.ToString());
            ReadWrite.SaveToTxt(routeur, PATH, FILE_NAME);

            //CiscoRouter routeur2 = new CiscoRouter();
            //routeur2.ReadFromTxt(PATH, FILE_NAME);
            //MessageBox.Show(routeur2.ToString());
        }
    }
}
