using ContactModele.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactModele.Services
{
    public class ContactService
    {
        private static ContactService instance = null;
        public List<Personne> Contacts { get; set; }
        private MySqlConnectionStringBuilder con;


        public static ContactService Instance
        {
            get
            {
                if (null == instance)
                {
                    instance = new ContactService();
                }
                return instance;
            }
        }

        public List<Entities.Personne> Load()
        {
            List<Entities.Personne> liste = new List<Entities.Personne>();

            string sql = "select type, id, nom, prenom, email, telephone, adresse, " +
                "societe, noclient, mobile, dtnaissance " +
                "from personnes";

            MySqlConnectionStringBuilder csb = new MySqlConnectionStringBuilder
            {
                Server = "localhost",
                Database = "c_sharp_td",
                UserID = "root",
                Password = "dev"
            };

            using (MySqlConnection conn = new MySqlConnection(csb.ConnectionString))
            {
                Contacts = new List<Personne>();
                conn.Open();

                MySqlCommand command = new MySqlCommand(sql, conn);


                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string type = reader.GetString(0);
                    Personne personne;

                    if ("c" == type)
                    {
                        personne = new Client();
                    }
                    else
                    {
                        personne = new Ami();
                    }

                    personne.Nom = reader.GetString(2);
                    personne.Prenom = reader.GetString(3); 
                    
                    if (reader.IsDBNull(4) == false) personne.Email = reader.GetString(4);
                    if (reader.IsDBNull(5) == false) personne.Telephone = reader.GetString(5);

                    if (reader.IsDBNull(7) == false) (personne as Client).Societe = reader.GetString(7);
                    if (reader.IsDBNull(8) == false) (personne as Client).Num_client = reader.GetInt32(8);

                    if (reader.IsDBNull(9) == false) (personne as Ami).Num_mobile = reader.GetInt32(9);
                    if (reader.IsDBNull(10) == false) (personne as Ami).Anniversaire = reader.GetDateTime(10);

                    string str = personne.ToString();
                    Contacts.Add(personne);
                }
                reader.Close();
                conn.Close();

            }

            return Contacts;
        }
    }
}
