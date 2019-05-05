using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;

namespace HackathonCisco
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string PATH = System.AppDomain.CurrentDomain.BaseDirectory;
        public static string FILE_NAME = "scriptTEST.txt";
        public CiscoRouter cRouteur;
        public CiscoSwitch cSwitch;

        public MainWindow()
        {
            InitializeComponent();
            // Interfaces
            Interfaces int01 = new Interfaces(new Ports(TypeOfInterfaces.FastEthernet, 0, 1), true, "Reseau A", new IP ("10.10.10.254"), new IP("255.255.255.0"));
            Interfaces int02 = new Interfaces(new Ports(), false, "Super reseau 2", new IP("192.168.0.1"), new IP(255, 255, 0, 0));
            // IPv6
            IPv6 ipv601 = new IPv6("2a02:2788:614:1830:a532:86b0:ad1d:c42b", 64);
            // Routes
            Routes rou01 = new Routes(new IP ("10.10.10.0"), new IP("255.255.255.0"), new Ports(TypeOfInterfaces.FastEthernet, 0, 1));
            Routes rou02 = new Routes(new IP("0.0.0.0"), new IP("0.0.0.0"), new IP("10.10.10.0"));
            Routes rou03 = new Routes(new IP("10.20.20.0"), new IP("255.255.255.0"), new Ports(TypeOfInterfaces.FastEthernet, 0, 1), new IP("10.10.10.0"));
            // Router
            // routeur = new CiscoRouter("routeur001");
            // routeur.AddBanner("Super Routeur")
            //   .AddInterfaces(int01, int02)
            //   .AddNoIpDomaineLookup(true)
            //   .AddSecureConsoleMode("cisco", true)
            //   .AddSecurePriviledgeMode("cisco")
            //   .AddRoutes(rou01, rou02, rou03);
        }

        private void MenuFileOpen_Click(object sender, RoutedEventArgs e)
        {
            cRouteur = new CiscoRouter();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                string firstLine = File.ReadAllLines(openFileDialog.FileName).Skip(0).Take(1).First(); //readFirstLine
                if (firstLine == "! routeur")
                {
                    ReadWrite.ReadFromTxt(cRouteur, System.IO.Path.GetDirectoryName(openFileDialog.FileName), openFileDialog.FileName);
                }
                else if (firstLine == "! switch")
                {
                    ReadWrite.ReadFromTxt(cSwitch, System.IO.Path.GetDirectoryName(openFileDialog.FileName), openFileDialog.FileName);
                }
            }
        }

        private void MenuFileSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, cRouteur == null ? cSwitch.ToString() : cRouteur.ToString());
            }
        }

        private void MenuFileExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void MenuFileNewSwitch_Click(object sender, RoutedEventArgs e)
        {
            cRouteur = null;
            cSwitch = new CiscoSwitch();

        }

        private void MenuFileRouteur_Click(object sender, RoutedEventArgs e)
        {
            cRouteur = new CiscoRouter();
            cSwitch = null;

        }

        private void TextBoxHostname_KeyDown(object sender, KeyEventArgs e)
        {
            if (cRouteur != null)
                cRouteur.AddHostname(TextBoxHostname.Text);
            else
                cSwitch.AddHostname(TextBoxHostname.Text);
        }
    }
}
