using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DnDProject
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public bool TryLogin = false;

        public LoginWindow(string message)
        {
            InitializeComponent();

            // zorg dat de message wordt weergegeven
            if (message.Length > 0)
            {
                ErrorMessage.Visibility = Visibility.Visible;
                ErrorMessage.Text = message;
            }
            else
            {
                ErrorMessage.Visibility = Visibility.Hidden;
            }
            
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            TryLogin = true;

            // haal waarde op van tekstvak "username"
            string u = this.Username.Text;
            string p = this.Password.Text;

            System.Console.WriteLine("username: " + u);

            // haal waarde op van tekstvak "password"
            MySqlConnection connection;
            string server;
            string database;
            string uid;
            string password;

            server = "localhost";
            database = "dnd_db";
            uid = "root";
            password = "";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
            // try to open database connection; show error if it failes
            try
            {
                connection.Open();
                Console.WriteLine("connection to mysql database ready.");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Cannot open connection to MySQL database. " + ex.Message + " Errornumber: " + ex.Number.ToString() );
                return ;
            }

            // query database

            // stuur query op naar database
            List<User> users = new List<User>();

            try
            {
                string query = String.Format("SELECT * FROM Users WHERE Username='{0}' AND Password='{1}' ", u, p);
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

                MessageBox.Show(ex.Message);
                return;
            }
            
            // try to close database connection, show error if it failed
            try
            {
                connection.Close();
                Console.WriteLine("Database connection closed");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return ;
            }

            // see how many results there are; 0 means "user/pass combo not found", 1 row found means: we found the user/pass combi
            Console.WriteLine("User records found: " + users.Count);

            if ( users.Count == 1 )
            {
                this.DialogResult = true;
                this.Close();
            } else
            {
                // klopt niet; niks gevonden of meerdere; allebei niet goed
                this.DialogResult = false;
                this.Close();
            }
            
            // uitzoeken wat er moet gebeuren als results 0 is of meer dan 1

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            RegisterForm rf = new RegisterForm();
            rf.ShowDialog();

            if (rf.DialogResult.HasValue && rf.DialogResult.Value)
            {

            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
