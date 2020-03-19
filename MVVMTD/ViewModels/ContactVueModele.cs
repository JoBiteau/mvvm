using ContactModele.Entities;
using ContactModele.Services;
using System;
using System.Windows.Input;

namespace MvvmTD.ViewModels
{
    class ContactVueModele : ViewModelBase
    {
        public EventHandler onRefreshList;
        public EventHandler delFromList;

        private ContactService service;
        private Personne contact;
        private RelayCommand editCommand;
        private RelayCommand delCommand;



        public ContactVueModele(Personne personne)
        {
            service = new ContactService();
            contact = personne ?? throw new NullReferenceException("Personne");
        }

        public Personne Contact
        {
            get{ return this.contact; }
        }

        public ICommand EditCommand
        {
            get
            {
                if (null == editCommand) editCommand = new RelayCommand(EnregistrerContact);
                return editCommand;
            }
        }

        private void EnregistrerContact()
        {
            service.Edit(contact);
        }

        public ICommand DelCommand
        {
            get
            {
                if (null == delCommand) delCommand = new RelayCommand(SupprimerContact);
                return delCommand;
            }
        }

        private void SupprimerContact()
        {
            service.Del(contact);
            delFromList?.Invoke(this, EventArgs.Empty);
        }


        public bool EstClient
        {
            get
            {
                if (contact.GetType() == typeof(Client)) return true;
                return false;
            }
        }

        public bool EstAmi
        {
            get
            {
                if (contact.GetType() == typeof(Ami)) return true;
                return false;
            }
        }
    }
}
