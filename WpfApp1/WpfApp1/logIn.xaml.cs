using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using ToDo.Static;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            DatabaseHelper.openDatabaseConnection();
            var result = DatabaseHelper.getReaderForQuery("Select * From todo.user_profile", new SqlParameter[] { });
            while (result.Read())
            {
                string userEmail = (string)result["email"];
                if (userEmail == usernameText.Text)
                {
                    if((string) result["password"] == passwordText.Password)
                    {
                        var window = new MainWindow();
                        long resultID = (long) result["id"];
                        window.userID = resultID;
                        window.Show();
                        Close();
                    }
                }
            }
            result.Close();
            DatabaseHelper.closeDatabaseConnection();
        }
        private void Register_Button_Click(object sender, RoutedEventArgs e)
        {
            bool isFound = false;
            DatabaseHelper.openDatabaseConnection();
            var result = DatabaseHelper.getReaderForQuery("Select * From todo.user_profile", new SqlParameter[] { });
            while (result.Read())
            {
                string userEmail = (string) result["email"];
                if(userEmail == usernameText.Text)
                {
                    fyiText.Content = "This email already exists: Would you like to login instead?";
                    isFound = true;
                    break;
                }
            }
            result.Close();

            if (!isFound)
            {
                if (passwordText.Password.Length == 0)
                {
                    fyiText.Content = "Please enter a password";
                }
                else
                {
                    DatabaseHelper.performNonQuery(string.Format("Insert into todo.user_profile (email, password) Values ('{0}', '{1}')", usernameText.Text, passwordText.Password), new SqlParameter[] { });
                }
            }
            DatabaseHelper.closeDatabaseConnection();
        }
    }
}