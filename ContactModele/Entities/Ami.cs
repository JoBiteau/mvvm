using System;
using System.Collections.Generic;
using System.Text;

namespace ContactModele.Entities
{
    public class Ami : Personne
    {
        public DateTime Anniversaire { get; set; }
        public string Num_mobile { get; set; }

        public override string ToString()
        {
            return $"{Prenom} {Nom} - anniversaire : {Anniversaire} - {Num_mobile}";
        }
    }
}
