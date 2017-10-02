using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DnDProject
{
    internal class Database
    {
        string servername = "localhost";
        string databasename = "dnd_db";
        string username = "root";
        string password = "";
        string connectionString;
        MySqlConnection connection;
        public bool Connected = false;


        public Database()
        {
            connectionString = "SERVER=" + servername + ";" + "DATABASE=" + databasename + ";" + "UID=" + username + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
        }

        public bool Open()
        {
            // try to open database connection; show error if it failes
            try
            {
                connection.Open();
                Console.WriteLine("Connection to mysql database ready.");
                Connected = true;
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Cannot open connection to MySQL database. " + ex.Message + " Errornumber: " + ex.Number.ToString());
                Connected = false;
                return false;
            }
        }

        public bool Close()
        {
            try
            {
                connection.Close();
                Console.WriteLine("Database connection closed");
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }

        public List<User> GetUsersWithEmailPass(string email, string password)
        {
            List<User> users = new List<User>();

            try
            {
                string query = String.Format("SELECT * FROM Users WHERE Username='{0}' AND Password='{1}' ", email, password);
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Check is the reader has any rows at all before starting to read.
                        if (reader.HasRows)
                        {
                            // Read advances to the next row.
                            while (reader.Read())
                            {
                                User us = new User();
                                // To avoid unexpected bugs access columns by name.
                                us.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                                us.Username = reader.GetString(reader.GetOrdinal("Username"));
                                us.Email = reader.GetString(reader.GetOrdinal("Email"));
                                us.Password = reader.GetString(reader.GetOrdinal("Password"));
                                users.Add(us);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // check for errors and show errors if anything goes wrong

                Console.WriteLine(ex.Message);
                return null;
            }

            return users;
        }

        public void LogLogin(int id)
        {
            // store a record with the login time for the current user
            string query = String.Format("INSERT INTO Users_sessions (user_id) VALUES ({0}) ", id);
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                var result = cmd.ExecuteNonQuery();
            }

            this.Close();
            
        }
    }
}
