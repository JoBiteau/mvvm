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
            List<Personne> personnes = ContactEFService.Instance.Load();
            //List<Personne> personnes = ContactService.Instance.Load();

            listeContacts = new ObservableCollection<ContactVueModele>();

            foreach (Personne personne in personnes)
            {
                ContactVueModele cvm = new ContactVueModele(personne);
                cvm.delFromList += DelFromList;
                listeContacts.Add(cvm);
            }

            collectionView = CollectionViewSource.GetDefaultView(listeContacts);
            collectionView.CurrentChanged += OnCollectionViewCurrentChanged;
            collectionView.MoveCurrentToFirst();

        }

        public void DelFromList(object sender, EventArgs e)
        {
            ListeContacts.Remove(sender as ContactVueModele);
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
            ListeContacts.Add(new ContactVueModele(new Client { Nom = "Nouveau client" }));
            
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
            ListeContacts.Add(new ContactVueModele(new Ami { Nom = "Nouvel ami" }));

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

        private bool IsMatch(object item, string value)
        {
            bool societeMatch = false;
            if (!(item is ContactVueModele)) return false;

            value = value.ToUpper();

            ContactVueModele p = (ContactVueModele)item;

            Personne person = p.Contact;
            if (person.GetType() == typeof(Client))
            {
                Client client = (Client)person;
                if (null != client.Societe)
                {
                    societeMatch = client.Societe.ToUpper().Contains(value);
                }
            }

            return (p.Contact.Nom.ToUpper().Contains(value) || p.Contact.Prenom.ToUpper().Contains(value) || societeMatch);
        }
        public bool TexteRechercherNoMatch
        {
            get { return collectionView.IsEmpty; }
        }
    }
}
