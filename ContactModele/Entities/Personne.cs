using System;
using System.Collections.Generic;
using System.Text;

namespace ContactModele.Entities
{
    public class Personne
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; } 
        public string Adresse { get; set; } 
    }
}
