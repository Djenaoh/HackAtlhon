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
        ObservableCollection<Wrapper> lstSeries = new ObservableCollection<Wrapper>();
        public static string PATH = System.AppDomain.CurrentDomain.BaseDirectory;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = lstMovies;
        }

        private void MenuFileOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                deSerialize(openFileDialog.FileName);
            }
        }

        private void MenuFileSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (saveFileDialog.ShowDialog() == true)
            {
                toSerialize(saveFileDialog.FileName);
            }
        }

        private void MenuFileExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        public void toSerialize(string fileName)
        {
            FileStream stream = File.Create(fileName);
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, lstMovies);
            stream.Close();
        }
        public void deSerialize(string fileName)
        {
            var formatter = new BinaryFormatter();
            FileStream stream = File.OpenRead(fileName);
            ObservableCollection<Item> lstTmp = (ObservableCollection<Item>)formatter.Deserialize(stream);
            stream.Close();
            foreach(Item item in lstTmp)
            {
                lstMovies.Add(item);
            }
        }

        private void AddLebowski_Click(object sender, RoutedEventArgs e)
        {
            Item lebowski = new Item().addTitle("The Big Lebowski")
                .addYear(1998)
                .addGenres(EnumGenre.Comedie, EnumGenre.Crime)
                .addRating(5)
                .addImage("/Image/BigLebowski.jpg")
                .addDescription("Jeff \"The Dude\" Lebowski, mistaken for a millionaire of the same name, seeks restitution for his ruined rug and enlists his bowling buddies to help get it.")
                .addReviews(new List<string>(new string[] { "Greatest movie ever made.", "With the combination of the writing of the Coen brothers and the Cinematography of Roger Deakins, they created a film as beautiful as it is funny. The Coen brothers consistently impress me with their ability to write an interesting story with fascinating yet quirky characters. Without resorting to gratuitous sexual scenes like many other writer/directors of R rated films the Coen brothers manage to add the right amount of language and violence that is necessary to the story without it becoming the only reason for watching. 'The Big Lebowski' has so many clever and hilarious lines that you have to watch it over and over again. " }))
                .addDirectors("Joel Coen", "Ethan Coen")
                .addStars("Jeff Bridges", "John Goodman", "Julianne Moore")
                .addWriters("Ethan Coen", "Joel Coen");
            lstMovies.Add(lebowski);
        }

        private void DeleteDataGrid_Click(object sender, RoutedEventArgs e)
        {
            lstMovies.Clear();
        }

        private void MenuFileClose_Click(object sender, RoutedEventArgs e)
        {
            lstMovies.Clear();
        }

        private void MenuAddMovie_Click(object sender, RoutedEventArgs e)
        {
            AddEdit fenetre02 = new AddEdit();
            if (Convert.ToBoolean(fenetre02.ShowDialog())) // Valider
            {
                lstMovies.Add(fenetre02.ReturnData());
            }
            else // Annuler
            {
            }
        }

      /*  private void BtnModifierElement_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridList.SelectedItems.Count == 1)
            {
                Personne per = DataGridList.Items[DataGridList.SelectedIndex] as Personne;
                AjoutEdition fenetre02 = new AjoutEdition(per);
                if (Convert.ToBoolean(fenetre02.ShowDialog())) // Valider
                {
                    listing.Remove(per);
                    listing.Add(fenetre02.RecupDonneesPersonne());
                }
                else // Annuler
                {
                }
            }
            else // Pas d'item selectionner dans la grid
            {
                MessageBox.Show("Plusieur elements selectioner");
            }
        }*/
    }
}
