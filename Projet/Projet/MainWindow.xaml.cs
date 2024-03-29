﻿using System;
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

        // Attributs
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
        /// <summary>
        /// methode permettant d'ouvrir un fichier de sauvergarde de la librairie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// methode permettant de sauvergarder un fichier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// methode permettant de quitter l'application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuFileExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// methode qui serialise des objets items dans une observable collection qui est serialisée dans un fichier
        /// </summary>
        /// <param name="fileName"></param>
        public void toSerialize(string fileName)
        {
            FileStream stream = File.Create(fileName);
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, lstMovies);
            stream.Close();
        }

        /// <summary>
        /// methode deserialize des objets items dans une observable collection qui est serialisée dans un fichier
        /// </summary>
        /// <param name="fileName"></param>
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

        /// <summary>
        /// methode qui permet de faire un test de population dans la librairie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// outil de debug qui permet de vider la bibliotheque
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteDataGrid_Click(object sender, RoutedEventArgs e)
        {
            lstMovies.Clear();
        }

        /// <summary>
        /// methode qui permet d'ajouter un item en appelant la methode add
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuAddMovie_Click(object sender, RoutedEventArgs e)
        {
            add();
        }

        /// <summary>
        /// methode qui ajoute un nouvel item en appelant une nouvelle fenetre
        /// </summary>
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
        }

        /// <summary>
        /// methode qui appele une methode delete 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeleteMovie_Click(object sender, RoutedEventArgs e)
        {
            delete();

        }
         /// <summary>
         /// methode qui permet de supprimer un item dans la bibliotheque
         /// </summary>
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

        /// <summary>
        /// methode qui permet d'appeler la méthode edit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditMovie_Click(object sender, RoutedEventArgs e)
        {
            edit();
        }

        /// <summary>
        /// methode qui permet d'éditer un item dans la bibliotheque en appelant une nouvelle fenetre
        /// </summary>
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
            }
            else // Pas d'item selectionner dans la grid
            {
                MessageBox.Show("Erreur: Pas d'éléments sélectionné");
            }
        }

        /// <summary>
        /// methode qui permet de clear la selection des items lorsqu'on change la valeur de filtre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGridList.SelectedItems.Clear();
        }

        bool flagctrl = false;

        /// <summary>
        /// methode permettant de gerer les inputs clavier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// methoque qui permet d'ouvrir la fenetre d'aide
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuHelpShow_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Développé par : \n– Victor Hachard \n– Gilles Van Acker \n– Arthur Draye \n– Bastien Weber \n– Stacey Cnudde", "Autheurs", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// methode qui permet de faire une recherche des item de la datagrid dès qu'un mot est entré
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxFind_TextChanged(object sender, TextChangedEventArgs e)
        {
          
            DataGridList.SelectedItems.Clear();
            string choise = ComboBoxFilter.SelectionBoxItem.ToString();
            int count = 0;
            List<Item> lstDevant = new List<Item>();

            foreach (Item item in DataGridList.Items)
            {
                List<string> lstItems = new List<string>();
                int i = 0;
                switch (choise)
                {
                    case "Titre":
                        lstItems.AddRange(item.Title.Split(' ').ToList());
                        break;
                    case "Année":
                        lstItems.Add(item.Year.ToString());
                        break;
                    case "Genre":
                        lstItems.AddRange(item.Gender.Split(' ').ToList());
                        break;
                    case "Note":
                        lstItems.Add(item.Rating.ToString());
                        break;
                }
                foreach (string strFind in lstItems)
                {
                    foreach (string strBox in TextBoxFind.Text.Split(' '))
                    {
                        if (strBox.ToLower() == strFind.ToLower())
                        {
                            lstDevant.Add(item);
                        }
                    }
                }
            }
            foreach (Item tt in lstDevant)
            {
                count++;
                lstMovies.Move(lstMovies.IndexOf(tt), 0);

                DataGridList.SelectedItems.Add(tt);
            }

        }
    }
}
