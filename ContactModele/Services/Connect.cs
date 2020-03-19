using ContactModele.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactModele.Services
{
    public class Connect
    {
        private static Connect instance = null;
        public List<Personne> Contacts { get; set;}

        public Connect()
        {
            Contacts = new List<Personne>
            {
                new Ami { Prenom = "Bernadette", Nom = "Dejeu" },
                new Client { Prenom = "Barrack", Nom = "Haffritt" },
                new Client { Prenom = "Barbara", Nom = "Dégout" },
                new Client { Prenom = "Bart", Nom = "Haba" },
                new Ami { Prenom = "Jean-Loup", Nom = "Chretien" },
                new Client { Prenom = "Marie", Nom = "Curie" },
                new Ami { Prenom = "Victor", Nom = "Hugo" },
                new Ami { Prenom = "Victor", Nom = "Hugo" },
                new Ami { Prenom = "Victor", Nom = "Hugo" },
                new Ami { Prenom = "Victor", Nom = "Hugo" },
                new Ami { Prenom = "Victor", Nom = "Hugo" },
                new Ami { Prenom = "Victor", Nom = "Hugo" },
                new Ami { Prenom = "Victor", Nom = "Hugo" },
                new Ami { Prenom = "Victor", Nom = "Hugo" },
                new Ami { Prenom = "Victor", Nom = "Hugo" },
                new Client { Prenom = "Don", Nom = "Corleone" }
            };
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
