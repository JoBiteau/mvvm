using ContactModele.Entities;
using ContactModele.Services;
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
    public partial class UcContact : UserControl
    {
        public UcContact()
        {
            InitializeComponent();
            Connect connect = new Connect();
            listPerson.ItemsSource = connect.Load();
        }

        private void InputNom_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void InputSociete_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        public void OnNameClicked()
        {
            Personne albert = new Client
            {
                Nom = "Albert",
                Prenom = "Lecinquiememousquetaire",
                Societe = "Spanguerro Spaggeti",
                Email = "ursaf.forever@finance.gouv.fr",
                Num_client = 42,
                Telephone = "0123456789"
            };
        }

        public void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Console.WriteLine("Ok, le texte à changé. A voir si on lui associe une action ou pas");
        }
    }
}
