using Lib1.Entities;
using Lib1.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MvvmTD.Views
{
    /// <summary>
    /// Logique d'interaction pour ucContact.xaml
    /// </summary>
    public partial class ucContact : UserControl
    {
        public ucContact()
        {
            InitializeComponent();
            Connect connect = new Connect();
            listPerson.ItemsSource = connect.Load();
        }

        private void inputNom_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void inputSociete_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

    }
}
