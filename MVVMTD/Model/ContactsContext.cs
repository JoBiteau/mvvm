using ContactModele.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MvvmTD.Model
{
    public class ContactsContext : DbContext
    {
        public ContactsContext() : base()
        {
        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Ami> Amis { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseMySql("server=localhost;port=3306;userid=root;password=dev;database=c_sharp_ef;persistsecurityinfo = True");
        }

}
