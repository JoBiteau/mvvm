﻿using Lib1.Entities;
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
            Contacts = new List<Personne>
            {
                new Client { Prenom = "Barrack", Nom = "Haffritt" },
                new Client { Prenom = "Barbara", Nom = "Dégout" },
                new Client { Prenom = "Bart", Nom = "Haba" },
                new Ami { Prenom = "Bernadette", Nom = "Dejeu" }
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
