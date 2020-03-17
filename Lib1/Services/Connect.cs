using Lib1.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lib1.Services
{
    public class Connect
    {
        private static Connect instance = null;
        public List<Personne> Contacts { get; set;}

        public Connect()
        {
            Contacts = new List<Personne>();

            Contacts.Add(new Client { Nom = "Barrack", Prenom = "Haffritt" });
            Contacts.Add(new Client { Nom = "Barbara", Prenom = "Dégout" });
            Contacts.Add(new Client { Nom = "Bart", Prenom = "Haba" });
            Contacts.Add(new Ami { Nom = "Bernadette", Prenom = "Dejeu" });
        }

        public static Connect Instance
        {
            get
            {
                if (null == instance)
                {
                    instance = new Connect();
                }
                return instance;
            }
        }

        public List<Personne> Load()
        {
            return Contacts;
        }

    }
}
