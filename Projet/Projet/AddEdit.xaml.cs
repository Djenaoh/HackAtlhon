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
        public AddEdit()
        {
            InitializeComponent();
            this.Title = "Ajouter";
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
                ImageImage.Source = source;
            }
        }

        private void BtnValider_Click(object sender, RoutedEventArgs e)
        {
            string res = "Erreur:\n";

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
                res += "Le titre est obligatoire\n";
            }
            else if (!isValidString(TextBoxTitle.Text))
                {
                    res += "Le titre " + TextBoxTitle.Text + " n'est pas correcte cela dois etre un string\n";
                    TextBoxTitle.Clear();
                }
                if (!isValidString(TextBoxDescription.Text))
                {
                    res += "La descirprptiotionoin " + TextBoxDescription.Text + " n'est pas correcte cela dois etre un string\n";
                    TextBoxDescription.Clear();
                }
                if (TextBoxYear.Text == "")
            {
                res += "La date est obligatoire\n";
            }
                else if (!boolYear)
                {
                    res += "La date " + TextBoxYear.Text + " n'est pas correcte cela dois etre un entier\n";
                    TextBoxYear.Clear();
                }

            if (!isValidString(TextBoxDirector.Text))
            {
                res += "Le ou les direteurs " + TextBoxDirector.Text + " n'est pas correcte cela dois etre un string\n";
                TextBoxDirector.Clear();
            }
            if (!isValidString(TextBoxStars.Text))
            {
                res += "Le ou les stars " + TextBoxStars.Text + " n'est pas correcte cela dois etre un string\n";
                TextBoxStars.Clear();
            }
            if (!isValidString(TextBoxWriters.Text))
            {
                res += "Le ou les scenaristes " + TextBoxWriters.Text + " n'est pas correcte cela dois etre un string\n";
                TextBoxWriters.Clear();
            }
            if (!isValidString(TextBoxDescription.Text))
            {
                res += "La descirption " + TextBoxDescription.Text + " n'est pas correcte cela dois etre un string\n";
                TextBoxDescription.Clear();
            }

            if (res != "Erreur:\n")
                {
                    MessageBox.Show(res);
                return;
                    
                }
            tmp = new Item()
                 .addTitle(title)
                 .addDescription(TextBoxDirector.Text)
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
