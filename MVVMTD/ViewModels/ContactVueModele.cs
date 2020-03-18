using ContactModele.Entities;
using System;

namespace MvvmTD.ViewModels
{
    class ContactVueModele 
    {
        private Personne contact;

        public ContactVueModele(Personne personne)
            {
            if (null == personne)
            {
                throw new NullReferenceException("Personne");
            }
            contact = personne;
        }

        public Personne Contact
        {
            get{ return this.contact; }
        }

        
    }
}
