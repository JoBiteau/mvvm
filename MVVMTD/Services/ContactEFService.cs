using ContactModele.Entities;
using MvvmTD.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvvmTD
{
    public class ContactEFService
    {

        private static ContactEFService instance = null;
        //public List<Personne> Contacts { get; set; }


        public static ContactEFService Instance
        {
            get
            {
                if (null == instance)
                {
                    instance = new ContactEFService();
                }
                return instance;
            }
        }

        public List<Personne> Load()
        {
            List<Personne> result = new List<Personne>();

            using (ContactsContext context = new ContactsContext())
            {
                var c = context.Clients.ToList();
                foreach (Client client in c)
                {
                    result.Add(client);
                }

                var a = context.Amis.ToList();
                foreach (Ami ami in a)
                {
                    result.Add(ami);
                }
            }

            return result;
        }

        public bool EnregistrerContact(Personne contact)
        {
            return true;
        }

        public bool SupprimerContact(Personne contact)
        {
            return true;

        }
    }
    
}
