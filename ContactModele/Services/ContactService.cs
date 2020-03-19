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

            MySqlConnectionStringBuilder csb = this.GetConnection();

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

                    personne.Id = reader.GetInt32(1);
                    personne.Nom = reader.GetString(2);
                    personne.Prenom = reader.GetString(3); 
                    
                    if (reader.IsDBNull(4) == false) personne.Adresse = reader.GetString(4);
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

        private MySqlConnectionStringBuilder GetConnection()
        {
            return new MySqlConnectionStringBuilder
            {
                Server = "localhost",
                Database = "c_sharp_td",
                UserID = "root",
                Password = "dev"
            };
        }

        public void Edit(Personne person)
        {
            string sql = "";
         
            
            if (person.Id > 0)
            {
                sql = "update personnes set nom=@nom, prenom=@prenom, adresse=@adresse, " +
                                            "email=@email, telephone=@telephone, " +
                                            "societe=@societe, noclient=@noclient, " +
                                            "mobile=@mobile, dtnaissance=@dtnaissance " +
                        "where id=@id";
            }
            else
            {
                sql = "insert into personnes (nom,prenom,adresse,email,telephone," +
                                                "societe,noclient,mobile,dtnaissance) " +
                        "values (@nom,@prenom,@adresse,@email,@telephone," +
                                "@societe,@noclient,@mobile,@dtnaissance)";
            }

            MySqlConnectionStringBuilder csb = GetConnection();

            using MySqlConnection conn = new MySqlConnection(csb.ConnectionString);
            conn.Open();

            MySqlCommand command = new MySqlCommand(sql, conn);
            if (person.Id > 0)
            {
                command.Parameters.Add(new MySqlParameter("@id", person.Id));
            }

            command.Parameters.Add(new MySqlParameter("@nom", person.Nom));
            command.Parameters.Add(new MySqlParameter("@prenom", person.Prenom));
            command.Parameters.Add(new MySqlParameter("@adresse", person.Adresse));

            if (person.GetType() == typeof(Client))
            {
                command.Parameters.Add(new MySqlParameter("@societe", (person as Client).Societe));
                command.Parameters.Add(new MySqlParameter("@noclient", (person as Client).Num_client));
            }
            else if (person.GetType() == typeof(Ami))
            {
                command.Parameters.Add(new MySqlParameter("@dtnaissance", (person as Ami).Anniversaire));
                command.Parameters.Add(new MySqlParameter("@mobile", (person as Ami).Num_mobile));
            }

            int nbLignesModifies = command.ExecuteNonQuery();

            conn.Close();
        }
    }
}
