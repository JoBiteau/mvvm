using ContactModele.Entities;
using ContactModele.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Data;

namespace MvvmTD.ViewModels
{
    class ContactsVueModele : ViewModelBase
    {
        private readonly ObservableCollection<ContactVueModele> listeContacts;
        private readonly ICollectionView collectionView;

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
            Console.WriteLine("Current change on collection view");
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
    }
}
