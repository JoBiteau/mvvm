using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ContactModele.Entities
{
    public abstract class Personne
    {
        public int Id { get; set; }

        [Required]
        [StringLength(80)]
        public string Nom { get; set; }

        [Required]
        [StringLength(80)]
        public string Prenom { get; set; }

        [StringLength(1600)]
        public string Email { get; set; }

        [StringLength(20)]
        public string Telephone { get; set; }

        [StringLength(255)]
        public string Adresse { get; set; } 
    }
}
