using ContactModele.Entities;
using ContactModele.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Data;
using System.Windows.Input;

namespace MvvmTD.ViewModels
{
    class ContactsVueModele : ViewModelBase
    {
        private readonly ObservableCollection<ContactVueModele> listeContacts;
        private readonly ICollectionView collectionView;

        private RelayCommand newCustomerCommand;
        private RelayCommand newFriendCommand;
        private RelayCommand commandeSuivant;
        private RelayCommand commandePrecedent;
        private RelayCommand commandeTrier;

        public ContactsVueModele()
        {
            List<Personne> personnes = Connect.Instance.Load();

            listeContacts = new ObservableCollection<ContactVueModele>();

            foreach (Personne personne in personnes)
            {
                listeContacts.Add(new ContactVueModele(personne));
            }

            collectionView = CollectionViewSource.GetDefaultView(listeContacts);
            collectionView.CurrentChanged += OnCollectionViewCurrentChanged;
            collectionView.MoveCurrentToFirst();

        }

        public void OnCollectionViewCurrentChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("ContactSelected");
        }

        public ObservableCollection<ContactVueModele> ListeContacts
        {
            get { return listeContacts; }
        }

        public ContactVueModele ContactSelected
        {
            get
            {
                if (null == collectionView.CurrentItem) return null;
                return (ContactVueModele)collectionView.CurrentItem;
            }
        }
        public ICommand NewCustomer
        {
            get
            {
                if (null == newCustomerCommand) newCustomerCommand = new RelayCommand(AjoutClient);
                return newCustomerCommand;
            }
        }
        private void AjoutClient()
        {
            System.Windows.MessageBox.Show("Ajout d'un client");
        }

        public ICommand NewFriend
        {
            get
            {
                if (null == newFriendCommand) newFriendCommand = new RelayCommand(AjoutAmi);
                return newFriendCommand;
            }
        }

        private void AjoutAmi()
        {
            System.Windows.MessageBox.Show("Ajout d'un ami");
        }

        public ICommand CommandeSuivant
        {
            get
            {
                if (commandeSuivant == null)
                {
                    commandeSuivant = new RelayCommand(GoSuivant, CanGoSuivant);
                }
                return commandeSuivant;
            }
        }

        public void GoSuivant()
        {
            collectionView.MoveCurrentToNext();
        }
        public bool CanGoSuivant()
        {
            if (collectionView.CurrentPosition < ((System.Windows.Data.ListCollectionView)collectionView).Count - 1)
                return true;
            return false;
        }

        public ICommand CommandePrecedent
        {
            get
            {
                if (commandePrecedent == null)
                {
                    commandePrecedent = new RelayCommand(GoPrecedent, CanGoPrecedent);
                }
                return commandePrecedent;
            }
        }

        public void GoPrecedent()
        {
            collectionView.MoveCurrentToPrevious();
        }
        public bool CanGoPrecedent()
        {
            return collectionView.CurrentPosition != 0;
        }

        public ICommand CommandeTrier
        {
            get
            {
                if (commandeTrier == null)
                {
                    commandeTrier = new RelayCommand(new Action<object>(TrierLaListe));
                }
                return commandeTrier;
            }
        }

        // Tri de la CollectionView :
        // l'utilisation de la méthode DeferRefresh permet d'effectuer le tri qu'à la sortie de l'instruction.
        // sinon, la collection serait réarrangée à chaque fois, au clear puis à chaque Add  !!
        public void TrierLaListe(object pNomProprieteDeTri)
        {
            if (null == pNomProprieteDeTri)
            {
                if (pNomProprieteDeTri == null)
                {
                    //tri par défaut
                    using (collectionView.DeferRefresh())
                    {
                        collectionView.SortDescriptions.Clear();
                        collectionView.SortDescriptions.Add(new SortDescription("Nom", ListSortDirection.Ascending));
                        collectionView.SortDescriptions.Add(new SortDescription("Prenom", ListSortDirection.Ascending));
                    }
                }
            }
            else
            {
                using (collectionView.DeferRefresh())
                {
                    collectionView.SortDescriptions.Clear();
                    collectionView.SortDescriptions.Add(new SortDescription(pNomProprieteDeTri.ToString(), ListSortDirection.Ascending));
                }
            }
        }

        public string TexteRechercher
        {
            set
            {
                collectionView.Filter = item => IsMatch(item, value);
                OnPropertyChanged("TexteRechercherNoMatch");
            }
        }

        // Méthode appellée pour chaque élément de la collection
        // pour déterminer si l'élément correspond ou non à la recherche.
        private bool IsMatch(object item, string value)
        {
            Console.WriteLine(item);
            if (!(item is ContactVueModele)) return false;

            value = value.ToUpper();

            ContactVueModele p = (ContactVueModele)item;

            Personne test = p.Contact;
            Console.WriteLine(test.GetType());
            //if (p.Contact.GetType == typeof(Client))
            //{
            //    return p.Contact.Societe.ToUpper().Contains(value);
            //}

            return (p.Contact.Nom.ToUpper().Contains(value) || p.Contact.Prenom.ToUpper().Contains(value));
        }
        public bool TexteRechercherNoMatch
        {
            get { return collectionView.IsEmpty; }
        }
    }
}
