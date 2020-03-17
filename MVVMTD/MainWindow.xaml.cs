using Lib1.Entities;
using Lib1.Services;
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

namespace MvvmTD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Connect connect = new Connect();
            List < Personne > listePersonne = connect.Load();
            
            listPerson.ItemsSource = listePersonne;
        }

        public void onNameClicked()
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
