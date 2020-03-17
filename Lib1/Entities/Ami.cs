using System;
using System.Collections.Generic;
using System.Text;

namespace Lib1.Entities
{
    public class Ami : Personne
    {
        public DateTime Anniversaire { get; set; }
        public int Num_mobile { get; set; }
        public string toString()
        {
            return $"{Prenom} {Nom} - anniversaire : {Anniversaire} - {Num_mobile}";
        }
    }
}
