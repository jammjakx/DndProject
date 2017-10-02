
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
        public Database db;

        public LoginWindow(Database database, string message)
        {
            InitializeComponent();

            this.db = database;

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

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TryLogin = true;

                // haal waarde op van tekstvak "username"
                string u = this.Username.Text;
                string p = this.Password.Text;

                if (!db.Open())
                {
                    MessageBox.Show("Cannot open connection to MySQL database. ");
                    return;
                }

                // stuur query op naar database
                List<User> users = db.GetUsersWithEmailPass(u, p);
                Console.WriteLine("User records found: " + users.Count);

                if (users.Count == 1)
                {
                    // keep track of when this user logged in
                    db.LogLogin(users[0].Id);

                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    // klopt niet; niks gevonden of meerdere; allebei niet goed
                    this.DialogResult = false;
                    this.Close();
                }

                // uitzoeken wat er moet gebeuren als results 0 is of meer dan 1
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Close();
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterForm rf = new RegisterForm();
            rf.ShowDialog();

            if (rf.DialogResult.HasValue && rf.DialogResult.Value)
            {
                throw new NotImplementedException();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
