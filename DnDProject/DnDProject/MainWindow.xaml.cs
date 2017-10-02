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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace DnDProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Database db = new Database();

        bool IsLoggedIn = false;

        public MainWindow() 
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            string message = "";

            while (this.IsLoggedIn == false)
            {
                LoginWindow dialog = new LoginWindow(this.db, message);

                dialog.ShowDialog();

                if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
                {
                    this.Visibility = System.Windows.Visibility.Visible;
                    this.Title = "Main - Loggedin";
                    this.IsLoggedIn = true;
                } 
                else
                {
                    if (dialog.TryLogin == true)
                    {
                        // the login dialog returned "false"  (ofwel cancel, ofwel foute combi)
                        message = "Ongeldige combinatie van username/password. Probeer het opnieuw.";
                    }
                    else
                    {
                        Application.Current.Shutdown();
                        break;
                    }
                    
                }

                dialog = null;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            db.Open();
            db.LogLogout();
            db.Close();
        }
    }
}
