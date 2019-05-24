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
        public static string RES = "/res/interface/";

        public MainWindow()
        {
            InitializeComponent();
            DataContext = lstMovies;
            img_logo.Source = new BitmapImage(new Uri(PATH + RES + "logo.jpg"));
            img_icon_add.Source = new BitmapImage(new Uri(PATH + RES + "icon_add.png"));
            img_icon_delete.Source = new BitmapImage(new Uri(PATH + RES + "icon_delete.png"));
            img_icon_edit.Source = new BitmapImage(new Uri(PATH + RES + "icon_edit.png"));
            BtnEdit.IsEnabled = false;
            BtnDelete.IsEnabled = false;

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
            if (DataGridList.Items.Count != 0)
            {
                BtnEdit.IsEnabled = true;
                BtnDelete.IsEnabled = true;
            }
        }

        private void MenuFileSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt";
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.FileName = "database.txt";
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
            foreach (Item item in lstTmp)
            {
                lstMovies.Add(item);
            }
        }

        private void AddLebowski_Click(object sender, RoutedEventArgs e)
        {
            Item lebowski = new Item().addTitle("The Big Lebowski")
                .addYear(1998)
                .addGenres(EnumGender.Comedie)
                .addRating(5)
                .addImage(PATH + "Image\\BigLebowski.jpg")
                .addDescription("Jeff \"The Dude\" Lebowski, mistaken for a millionaire of the same name, seeks restitution for his ruined rug and enlists his bowling buddies to help get it.")
                .addReviews(new List<string>(new string[] { "Greatest movie ever made.", "With the combination of the writing of the Coen brothers and the Cinematography of Roger Deakins, they created a film as beautiful as it is funny. The Coen brothers consistently impress me with their ability to write an interesting story with fascinating yet quirky characters. Without resorting to gratuitous sexual scenes like many other writer/directors of R rated films the Coen brothers manage to add the right amount of language and violence that is necessary to the story without it becoming the only reason for watching. 'The Big Lebowski' has so many clever and hilarious lines that you have to watch it over and over again. " }))
                .addDirectors("Joel Coen", "Ethan Coen")
                .addStars("Jeff Bridges", "John Goodman", "Julianne Moore")
                .addWriters("Ethan Coen", "Joel Coen");
            Item killBill1 = new Item().addTitle("Kill Bill: Volume I")
                .addYear(2003)
                .addGenres(EnumGender.Action)
                .addRating(5)
                .addImage(PATH + "Image\\kb1.jpg")
                .addDescription("After awakening from a four-year coma, a former assassin wreaks vengeance on the team of assassins who betrayed her.")
                .addDirectors("Quentin Tarantino")
                .addStars("Uma Thurman", "David Carradine", "Daryl Hannah")
                .addWriters("Quentin Tarantino");
            Item killBill2 = new Item().addTitle("Kill Bill: Volume II")
               .addYear(2004)
               .addGenres(EnumGender.Action)
               .addRating(5)
               .addImage(PATH + "Image\\kb2.jpg")
               .addDescription("The Bride continues her quest of vengeance against her former boss and lover Bill, the reclusive bouncer Budd, and the treacherous, one-eyed Elle.")
               .addDirectors("Quentin Tarantino")
               .addStars("Uma Thurman", "David Carradine", "Michael Madsen")
               .addWriters("Quentin Tarantino");
            lstMovies.Add(lebowski);
            lstMovies.Add(killBill1);
            lstMovies.Add(killBill2);
            if (DataGridList.Items.Count != 0)
            {
                BtnEdit.IsEnabled = true;
                BtnDelete.IsEnabled = true;
            }
        }

        private void DeleteDataGrid_Click(object sender, RoutedEventArgs e)
        {
            lstMovies.Clear();
        }

        private void MenuAddMovie_Click(object sender, RoutedEventArgs e)
        {
            add();
        }

        private void add()
        {
            AddEdit fenetre02 = new AddEdit();
            if (Convert.ToBoolean(fenetre02.ShowDialog())) // Valider
            {
                lstMovies.Add(fenetre02.ReturnData());
                if (DataGridList.Items.Count != 0)
                {
                    BtnEdit.IsEnabled = true;
                    BtnDelete.IsEnabled = true;
                }
            }
            else // Annuler
            {
            }
        }

        private void BtnDeleteMovie_Click(object sender, RoutedEventArgs e)
        {
            delete();

        }

        private void delete()
        {
            List<Item> a_effacer = new List<Item>();
            if (DataGridList.SelectedItems.Count > 0)
            {
                if (MessageBoxResult.Yes == MessageBox.Show("Été vous sûr de vouloir supprimer ce(s) éléement(s)", "Validation", MessageBoxButton.YesNo, MessageBoxImage.Hand))
                {
                    foreach (Item xxx in DataGridList.SelectedItems)
                    {
                        a_effacer.Add(xxx);
                    }
                    foreach (Item yyy in a_effacer)
                    {
                        lstMovies.Remove(yyy);
                    }
                }
            }
            else // Pas d'item selectionner dans la grid
            {
                MessageBox.Show("Erreur: Pas d'éléments sélectionné");
            }
        }

        private void BtnEditMovie_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridList.SelectedItems.Count == 1)
            {
                Item movie = DataGridList.Items[DataGridList.SelectedIndex] as Item;
                AddEdit fenetre02 = new AddEdit(movie);
                if (Convert.ToBoolean(fenetre02.ShowDialog())) // Valider
                {
                    lstMovies.Remove(movie);
                    lstMovies.Add(fenetre02.ReturnData());
                }
                else // Annuler
                {
                }
            }
            else // Pas d'item selectionner dans la grid
            {
                MessageBox.Show("Erreur: Pas d'éléments sélectionné");
            }
        }

        private void edit()
        {
            if (DataGridList.SelectedItems.Count == 1)
            {
                Item movie = DataGridList.Items[DataGridList.SelectedIndex] as Item;
                AddEdit fenetre02 = new AddEdit(movie);
                if (Convert.ToBoolean(fenetre02.ShowDialog())) // Valider
                {
                    lstMovies.Remove(movie);
                    lstMovies.Add(fenetre02.ReturnData());
                }
                else // Annuler
                {
                }
            }
            else // Pas d'item selectionner dans la grid
            {
                MessageBox.Show("Erreur: Pas d'éléments sélectionné");
            }
        }

        private void ComboBoxFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGridList.SelectedItems.Clear();
        }

        bool flagctrl = false;
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl)
            {
                flagctrl = true;
                e.Handled = true;
            }
            else if ((e.Key == Key.D) && (flagctrl))
            {
                flagctrl = false;
                delete();
            }
            else if ((e.Key == Key.E ) && (flagctrl))
            {
                edit();
                flagctrl = false;
            }
            else if ((e.Key == Key.N) && (flagctrl))
            {
                add();
                flagctrl = false;
            }
            else
            {
                flagctrl = false;
            }
        }

        private void MenuHelpShow_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("GILLOU TU FERA IC", "SUPER TITRE", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void TextBoxFind_TextChanged(object sender, TextChangedEventArgs e)
        {
          
            DataGridList.SelectedItems.Clear();
            string choise = ComboBoxFilter.SelectionBoxItem.ToString();
            int count = 0;
            int preCount = 0;
            List<Item> lstDevant = new List<Item>();

            foreach (Item item in DataGridList.Items)
            {
                string[] lstItems = item.Title.Split(' ');
                int i = 0;
                switch (choise)
                {
                    case "Year":
                        lstItems[0] = item.Year.ToString();
                        break;
                    case "Gender":
                        lstItems = item.Gender.Split(' ');
                        break;
                    case "Directors":
                        foreach (string str in item.LstDirectors)
                        {
                            lstItems[i] = str;
                            i++;
                        }
                        break;
                    case "Writers":
                        foreach (string str in item.LstWriters)
                        {
                            lstItems[i] = str;
                            i++;
                        }
                        break;
                    case "Stars":
                        foreach (string str in item.LstStars)
                        {
                            lstItems[i] = str;
                            i++;
                        }
                        break;
                    case "Rating":
                        lstItems[0] = item.Rating.ToString();
                        break;
                }
                preCount = 0;
                foreach (string strFind in lstItems)
                {
                    foreach (string strBox in TextBoxFind.Text.Split(' '))
                    {
                        if (strBox.ToLower() == strFind.ToLower())
                        {
                            DataGridList.SelectedItems.Add(item);
                            lstDevant.Add(item);
                            preCount++;
                            if (preCount == 0)
                                count++;
                        }
                    }
                }


            }


            foreach (Item tt in lstDevant)
            {

                lstMovies.Move(lstMovies.IndexOf(tt), 0);
            }

        }
    }
}
