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
            Clear();
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
            Unlock();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                string firstLine = File.ReadAllLines(openFileDialog.FileName).Skip(0).Take(1).First(); //readFirstLine
                if (firstLine == "! routeur")
                {
                    cRouteur = new CiscoRouter();
                    ReadWrite.ReadFromTxt(cRouteur, System.IO.Path.GetDirectoryName(openFileDialog.FileName), openFileDialog.FileName);
                }
                else if (firstLine == "! switch")
                {
                    cSwitch = new CiscoSwitch();
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
            Clear();
            Unlock();
            cSwitch = new CiscoSwitch();

        }

        private void MenuFileRouteur_Click(object sender, RoutedEventArgs e)
        {
            Clear();
            Unlock();
            cRouteur = new CiscoRouter();

        }

        private void TextBoxHostname_KeyDown(object sender, KeyEventArgs e)
        {
            if (cRouteur != null)
                cRouteur.AddHostname(TextBoxHostname.Text);
            else if (cSwitch != null)
                cSwitch.AddHostname(TextBoxHostname.Text);
        }

        private void MenuFileClose_Click(object sender, RoutedEventArgs e)
        {
            Clear();

        }

        private void Clear()
        {
            cRouteur = null;
            cSwitch  = null;
            foreach (Control ctl in containerCanvas.Children) // Clear , lock all CheckBox and TextBox
            {
                if (ctl.GetType() == typeof(CheckBox))
                {
                    ((CheckBox)ctl).IsChecked = false;
                    ((CheckBox)ctl).IsEnabled = false;
                }
                if (ctl.GetType() == typeof(TextBox))
                {
                    ((TextBox)ctl).IsEnabled = false;
                    ((TextBox)ctl).Text = String.Empty;
                }
            }
        }

        private void Unlock()
        {
            foreach (Control ctl in containerCanvas.Children)
            {
                if (ctl.GetType() == typeof(TextBox))
                    ((TextBox)ctl).IsEnabled = true;
                if (ctl.GetType() == typeof(CheckBox))
                    ((CheckBox)ctl).IsEnabled = true;
                }
        }

        private void CheckBoxPasswordConsole_Checked(object sender, RoutedEventArgs e)
        {
                if (cRouteur != null)
                    cRouteur.AddEnableSecureConsoleMode(Convert.ToBoolean(CheckBoxPasswordConsole.IsChecked));
                else if (cSwitch != null)
                    cSwitch.AddEnableSecureConsoleMode(Convert.ToBoolean(CheckBoxPasswordConsole.IsChecked));
        }

        private void TextBoxPasswordConsole_KeyDown(object sender, KeyEventArgs e)
        {
            if (cRouteur != null)
                cRouteur.AddSecureConsoleMode(TextBoxPasswordConsole.Text);
            else if (cSwitch != null)
                cSwitch.AddSecureConsoleMode(TextBoxPasswordConsole.Text);
        }

        private void TextBoxPasswordPriviledge_KeyDown(object sender, KeyEventArgs e)
        {
            if (cRouteur != null)
                cRouteur.AddSecurePriviledgeMode(TextBoxPasswordPriviledge.Text);
            else if (cSwitch != null)
                cSwitch.AddSecurePriviledgeMode(TextBoxPasswordPriviledge.Text);
        }
    }
}
