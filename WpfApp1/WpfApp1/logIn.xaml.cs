using System.Data.SqlClient;
using System.Windows;
using ToDo.Static;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            DatabaseHelper.openDatabaseConnection();
            //get all users from the database

            //Pls fix :)
            var result = DatabaseHelper.getReaderForQuery("Select * From todo.user_profile", new SqlParameter[] { });
            while (result.Read())
            {
                //find a match for the email
                string userEmail = (string)result["email"];
                if (userEmail == usernameText.Text)
                {
                    //check if this matched email has the same password too
                    if((string) result["password"] == passwordText.Password)
                    {
                        //matched, get the user id out
                        var window = new MainWindow();
                        long resultID = (long) result["id"];
                        window.userID = resultID;
                        //display the Main view for this user
                        window.Show();
                        result.Close();
                        DatabaseHelper.closeDatabaseConnection();
                        Close();
                        return;
                    }
                }
            }
            //not found, close database items and inform user
            result.Close();
            DatabaseHelper.closeDatabaseConnection();
            MessageBox.Show("Could not find a user with the given credentials");
        }
        private void Register_Button_Click(object sender, RoutedEventArgs e)
        {
            //ensure the user does not already exist
            bool isFound = false;
            DatabaseHelper.openDatabaseConnection();
            var result = DatabaseHelper.getReaderForQuery("Select * From todo.user_profile", new SqlParameter[] { });
            while (result.Read())
            {
                string userEmail = (string) result["email"];
                if(userEmail == usernameText.Text)
                {
                    MessageBox.Show("User for this email already exists");
                    isFound = true;
                    break;
                }
            }
            result.Close();
            //ensure the record was not found
            if (!isFound)
            {
                //ensure a password was entered
                if (passwordText.Password.Length == 0)
                {
                    MessageBox.Show("Please Enter a password");
                }
                else
                {
                    //insert this user into the database
                    DatabaseHelper.performNonQuery("Insert into todo.user_profile (email, password) Values ('@username', '@password')",new SqlParameter[] {new SqlParameter("@username", usernameText.Text),new SqlParameter("@password", passwordText.Password) });
                    MessageBox.Show("Successful Registration, please log in");
                }
            }
            DatabaseHelper.closeDatabaseConnection();
        }
    }
}