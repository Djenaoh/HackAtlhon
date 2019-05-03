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
        public MainWindow()
        {
            InitializeComponent();
            Interfaces int01 = new Interfaces(TypeOfInterfaces.FastEthernet, 0, 1, true, "Reseau A", "10.10.10.254", "255.255.255.0");

            CiscoRouter test = new CiscoRouter();
            test.Hostname = "cisco";
            test.AddInterfaces(int01);
            MessageBox.Show(test.SaveToConf());

        }
    }
}
