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
    }
}
