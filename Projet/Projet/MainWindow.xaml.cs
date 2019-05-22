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


using System.IO;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Projet
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        // Attr
        ObservableCollection<Item> lstMovies = new ObservableCollection<Item>();
        public static string PATH = System.AppDomain.CurrentDomain.BaseDirectory;
        string fileName = "database.txt";

        public MainWindow()
        {
            InitializeComponent();
            DataContext = lstMovies;
            load();
        }

        private void MenuFileOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                return;
            }
        }

        private void MenuFileSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (saveFileDialog.ShowDialog() == true)
            {
                return;
            }
        }

        private void MenuFileExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }


        private void load()
        {
            Item test = new Item().addTitle("Kill Bill")
                .addGerne(EnumGenre.Action);
            lstMovies.Add(test);
        }

        public void toSerialize()
        {
            FileStream stream = File.Create(fileName);
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, lstMovies);
            stream.Close();
        }
        public void deSerialize()
        {
            var formatter = new BinaryFormatter();
            FileStream stream = File.OpenRead(fileName);
            Console.WriteLine("Deserializing vector");
            var u = (List<Item>)formatter.Deserialize(stream);
            Console.WriteLine(u);
            stream.Close();
        }

    }
}
