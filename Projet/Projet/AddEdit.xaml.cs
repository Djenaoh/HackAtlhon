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

        public Item ReturnData()
        {
            Item tmp = new Item()
                .addTitle(Convert.ToString(TextBoxTitle.Text))
                .addYear(Convert.ToInt32(Convert.ToString(TextBoxYear.Text)));
            return tmp;
        }

        private void BtnValider_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void BtnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
