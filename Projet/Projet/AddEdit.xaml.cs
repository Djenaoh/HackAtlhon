using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Projet
{
    /// <summary>
    /// Logique d'interaction pour AddEdit.xaml
    /// </summary>
    public partial class AddEdit : Window
    {
        public static string PATH = System.AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// constructeur appellé pour ajouter un élément
        /// </summary>
        public AddEdit()
        {
            InitializeComponent();
            this.Title = "Ajouter";
            ImageImage.Source = new BitmapImage(new Uri(MainWindow.PATH + MainWindow.RES + "template.jpg"));
            load();
            TextChanger();
        }
        void TextChanger()
        {
            TextBoxTitle.Tag = "Titre";
            TextBoxWriters.Tag = "Réalisateur";
            TextBoxYear.Tag = "XXXX";
            TextBoxDirector.Tag = "Directeur";
            TextBoxStars.Tag = "Acteur";
            TextBoxDescription.Tag = "Ajoutez une description/Synopsis";

            TextBoxTitle.Foreground = Brushes.Gray;
            TextBoxWriters.Foreground = Brushes.Gray;
            TextBoxYear.Foreground = Brushes.Gray;
            TextBoxDirector.Foreground = Brushes.Gray;
            TextBoxDescription.Foreground = Brushes.Gray;
            TextBoxStars.Foreground = Brushes.Gray;


            TextBoxTitle.Text = (string)TextBoxTitle.Tag;
            TextBoxWriters.Text = (string)TextBoxWriters.Tag;
            TextBoxYear.Text = (string)TextBoxYear.Tag;
            TextBoxDirector.Text = (string)TextBoxDirector.Tag;
            TextBoxDescription.Text = (string)TextBoxDescription.Tag;
            TextBoxStars.Text = (string)TextBoxStars.Tag;


            TextBoxTitle.LostFocus += new RoutedEventHandler(OnEmptyText);
            TextBoxWriters.LostFocus += new RoutedEventHandler(OnEmptyText);
            TextBoxYear.LostFocus += new RoutedEventHandler(OnEmptyText);
            TextBoxDirector.LostFocus += new RoutedEventHandler(OnEmptyText);
            TextBoxDescription.LostFocus += new RoutedEventHandler(OnEmptyText);
            TextBoxStars.LostFocus += new RoutedEventHandler(OnEmptyText);

            TextBoxTitle.GotFocus += new RoutedEventHandler(OnFillText);
            TextBoxWriters.GotFocus += new RoutedEventHandler(OnFillText);
            TextBoxYear.GotFocus += new RoutedEventHandler(OnFillText);
            TextBoxDirector.GotFocus += new RoutedEventHandler(OnFillText);
            TextBoxDescription.GotFocus += new RoutedEventHandler(OnFillText);
            TextBoxStars.GotFocus += new RoutedEventHandler(OnFillText);
        }
        void OnEmptyText(object sender, EventArgs e)
        {
            var textbox = (TextBox)sender;

            if (string.IsNullOrEmpty(textbox.Text))
            {
                TextBoxTitle.Text = (string)TextBoxTitle.Tag;
                TextBoxWriters.Text = (string)TextBoxWriters.Tag;
                TextBoxYear.Text = (string)TextBoxYear.Tag;
                TextBoxDirector.Text = (string)TextBoxDirector.Tag;
                TextBoxDescription.Text = (string)TextBoxDescription.Tag;
                TextBoxStars.Text = (string)TextBoxStars.Tag;

                textbox.Foreground = Brushes.Gray;
            }

        }
        void OnFillText(object sender, EventArgs e)
        {
            var textbox = (TextBox)sender;

            if (!string.IsNullOrEmpty(textbox.Text))
            {
                textbox.Text = "";
                textbox.Foreground = Brushes.Black;

            }
        }

        private void load()
        {
            img_icon_val.Source = new BitmapImage(new Uri(MainWindow.PATH + MainWindow.RES + "icon_val.png"));
            img_icon_anul.Source = new BitmapImage(new Uri(MainWindow.PATH + MainWindow.RES + "icon_anul.png"));
            img_icon_browse.Source = new BitmapImage(new Uri(MainWindow.PATH + MainWindow.RES + "icon_browse.png"));
        }
        
        /// <summary>
        /// constructeur appellé pour appeler un item
        /// </summary>
        /// <param name="movie"></param>
        public AddEdit(Item movie)
        {
            InitializeComponent();
            this.Title = "Editer";
            load();
            TextBoxTitle.Text = movie.Title;
            TextBoxYear.Text = movie.Year.ToString();
            TextBoxDescription.Text = movie.Description;
            TextBoxDirector.Text = stringListToString(movie.LstDirectors);
            TextBoxStars.Text = stringListToString(movie.LstStars);
            TextBoxWriters.Text = stringListToString(movie.LstWriters);
            ComboRating.SelectedIndex = movie.Rating;
            ComboGender.SelectedIndex = movie.LstGenders.Key;
            pathImage = movie.Image == "" ? MainWindow.PATH + MainWindow.RES + "template.jpg" : movie.Image ;
            ImageImage.Source = new BitmapImage(new Uri(pathImage));
        }

        private string stringListToString(List<string> lstE)
        {
            string res = "";
            foreach(string e in lstE)
            {
                res += e + ",";
            }
            return res.Substring(0, res.Length - 1);
        }

        Item tmp;
        string pathImage = "";

        public Item ReturnData()
        {
            tmp.addImage(pathImage);
            return tmp;
        }
        
        /// <summary>
        /// methode qui prend le chemin de l'image ajouté, la copie dans un dossier propre au projet et ajoute le chemin créé dans l'objet item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.jpg)|*.jpg";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                if (!Directory.Exists(PATH + "Image"))
                {
                    System.IO.Directory.CreateDirectory(PATH + "Image");
                }
                pathImage = openFileDialog.FileName;
                BitmapImage source = new BitmapImage(new Uri(pathImage));
                System.IO.File.Copy(pathImage, PATH + "Image\\" + openFileDialog.SafeFileName, true);
                pathImage = MainWindow.PATH + "Image\\" + openFileDialog.SafeFileName;
                ImageImage.Source = source;
            }
        }

        /// <summary>
        /// methode permettant de faire les controles de saisie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnValider_Click(object sender, RoutedEventArgs e)
        {
            string res = "Erreur(s):\n\n";

            int year = 0;
            string gender = "None";
            string title = "";
            int rating = 0;

            title = Convert.ToString(TextBoxTitle.Text);
            rating = ComboRating.SelectedIndex;
            gender = ComboGender.SelectionBoxItem.ToString();
            
            bool boolYear = Int32.TryParse(Convert.ToString(TextBoxYear.Text), out year);

            if (TextBoxTitle.Text == "")
            {
                res += "-Le titre est obligatoire\n";
            }
            if (TextBoxYear.Text == "")
            {
                res += "-L'année est obligatoire\n";
            }
            else if (!boolYear)
            {
                res += "-Le texte entré dans la partie Année  \"" + TextBoxYear.Text + "\" n'est pas correct. Veuillez entrer une année en nombre entier\n";
                TextBoxYear.Clear();
            }
            else if (!(year > 1700 && year < 2500))
            {
                res += "-Le texte entré dans la partie Année  \"" + TextBoxYear.Text + "\" n'est pas correct. Veuillez entrer une année compris entre 1700 et 2500\n";
                TextBoxYear.Clear();
            }

            if (res != "Erreur(s):\n\n")
            {
                MessageBox.Show(res);
                return;        
            }
            tmp = new Item()
                 .addTitle(title)
                 .addDescription(TextBoxDescription.Text)
                 .addRating(rating)
                 .addGenres(EnumGender.stringToGender(gender))
                 .addDirectors(stringTolistString(TextBoxDirector.Text))
                 .addStars(stringTolistString(TextBoxStars.Text))
                 .addWriters(stringTolistString(TextBoxWriters.Text))
                 .addYear(year)
                 .addImage(MainWindow.PATH + MainWindow.RES + "template.jpg");
            this.DialogResult = true;
        }

        private string[] stringTolistString(string e)
        {
            return e.Split(',');
        }

        /// <summary>
        /// methode pour annuler et revenir à la mainwindow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
