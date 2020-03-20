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

        public bool Edit(Personne contact)
        {
            using (ContactsContext context = new ContactsContext())
            {
                if (contact.GetType() == typeof(Client))
                {
                    if (contact.Id > 0)
                    {
                        context.Clients.Attach(contact as Client);
                    }
                    else
                    {
                        context.Clients.Add(contact as Client);
                    }
                } 
                else
                {
                    if (contact.Id > 0)
                    {
                        context.Amis.Attach(contact as Ami);
                    }
                    else
                    {
                        context.Amis.Add(contact as Ami);
                    }
                }
                context.SaveChanges();
            }

             return true;
        }

        public bool Del(Personne contact)
        {
            using (ContactsContext context = new ContactsContext())
            {
                if (0 == contact.Id)
                {
                    return false;
                }

                if (contact.GetType() == typeof(Client))
                {
                    context.Clients.Remove(contact as Client);
                }
                else
                {
                    context.Amis.Remove(contact as Ami);
                }
                context.SaveChanges();
            }

            return true;
        }
    }
}
