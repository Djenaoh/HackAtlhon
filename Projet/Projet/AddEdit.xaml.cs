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
            TextBoxTitle.Tag = "Film";
            TextBoxWriters.Tag = "Scénariste(s)";
            TextBoxYear.Tag = "Année xxxx";
            TextBoxDirector.Tag = "Réalisateur(s)";
            TextBoxStars.Tag = "Acteur(s)";
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
            else if (!isValidString(TextBoxTitle.Text))
            {
                res += "-Le texte entré dans la partie Titre \"" + TextBoxTitle.Text + "\" n'est pas correct. Veuillez écrire correctement s'il vous plait (évitez les caractères spéciaux)\n";
                TextBoxTitle.Clear();
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
            if (!isValidString(TextBoxDirector.Text))
            {
                res += "-Le texte entré dans la partie Directeur \"" + TextBoxDirector.Text + "\" n'est pas correct. Veuillez écrire correctement s'il vous plait (évitez les caractères spéciaux)\n";
                TextBoxDirector.Clear();
            }
            if (!isValidString(TextBoxStars.Text))
            {
                res += "-Le texte entré dans la partie Star \"" + TextBoxStars.Text + "\" n'est pas correct. Veuillez écrire correctement s'il vous plait (évitez les caractères spéciaux)\n";
                TextBoxStars.Clear();
            }
            if (!isValidString(TextBoxWriters.Text))
            {
                res += "-Le texte entré dans la partie Scénariste \"" + TextBoxWriters.Text + "\" n'est pas correct. Veuillez écrire correctement s'il vous plait (évitez les caractères spéciaux)\n";
                TextBoxWriters.Clear();
            }
            if (!isValidString(TextBoxDescription.Text))
            {
                res += "-Le texte entré dans la partie Description \"" + TextBoxDescription.Text + "\" n'est pas correct. Veuillez écrire correctement s'il vous plait (évitez les caractères spéciaux)\n";
                TextBoxDescription.Clear();
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

        private bool isValidString(string e)
        {
            return true;
            Regex regexItem = new Regex("^[a-zA-Z0-9.,;:'\"\\-éèàçâêûîôäëüïöùæœ&ÂÊÎÔÛÄËÏÖÜÀÆÇÉÈŒÙ]* $");
            return regexItem.IsMatch(e);

        }

        private void BtnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
