using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
            img_icon_val.Source = new BitmapImage(new Uri(PATH + "/Image/interface/icon_val.png"));
            //img_icon_anul.Source = new BitmapImage(new Uri(PATH + "/Image/interface/icon_anul.png"));
            img_icon_anul.Source = new BitmapImage(new Uri(PATH + "/Image/interface/icon_anul.png"));
            img_icon_browse.Source = new BitmapImage(new Uri(PATH + "/Image/interface/icon_browse.png"));
        }
        
        public AddEdit(Item movie)
        {
            InitializeComponent();
            this.Title = "Editer";
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
                pathImage = openFileDialog.FileName;
                BitmapImage source = new BitmapImage(new Uri(pathImage));
                System.IO.File.Copy(pathImage, PATH + "Image\\" + openFileDialog.SafeFileName, true);
                pathImage = "Image\\" + openFileDialog.SafeFileName;
                ImageImage.Source = source;
            }
        }

        private void BtnValider_Click(object sender, RoutedEventArgs e)
        {
            string res = "Erreur(s):\n\n";

            int year = 0;
            EnumGenre gender = EnumGenre.None;
            string title = "";
            string description = "";
            int rating = 0;

            title = Convert.ToString(TextBoxTitle.Text);
            rating = ComboRating.SelectedIndex;
            gender = MyConvert.stringToGender(ComboGender.SelectedItem.ToString());
            
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
                 .addGenres(gender)
                 .addDirectors(stringTolistString(TextBoxDirector.Text))
                 .addStars(stringTolistString(TextBoxStars.Text))
                 .addWriters(stringTolistString(TextBoxWriters.Text))
                 .addYear(year);

            this.DialogResult = true;
        }

        private string[] stringTolistString(string e)
        {
            string[] res = e.Split(',');
            return res;
        }

        private bool isValidString(string e)
        {
            return true;
            Regex regexItem = new Regex("^[a-zA-Z0-9 ]*$");
            if (regexItem.IsMatch(e))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private void BtnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
