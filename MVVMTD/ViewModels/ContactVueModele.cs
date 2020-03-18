using ContactModele.Entities;
using System;

namespace MvvmTD.ViewModels
{
    class ContactVueModele : ViewModelBase
    {
        private Personne contact;

        public ContactVueModele(Personne personne)
        {
            contact = personne ?? throw new NullReferenceException("Personne");
        }

        public Personne Contact
        {
            get{ return this.contact; }
        }
    }
}
