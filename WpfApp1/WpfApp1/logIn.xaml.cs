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
            if (login(usernameText.Text, passwordText.Password)) {
                Close();
            }
        }

        public static bool login(string user, string pass) {
            DatabaseHelper.openDatabaseConnection();
            //get all users from the database
            var result = DatabaseHelper.getReaderForQuery("Select * From todo.user_profile where email = @email AND password = @passHash", new SqlParameter[] {
                new SqlParameter("@email", user), new SqlParameter("@passHash", CryptographyHelper.generateSHA256String(pass))});
            while (result != null && result.Read()) {
                //matched, get the user id out
                var window = new MainWindow();
                long resultID = (long)result["id"];
                window.userID = resultID;
                //display the Main view for this user
                window.Show();
                result.Close();
                DatabaseHelper.closeDatabaseConnection();

                return true;
            }
            //not found, close database items and inform user
            if (result != null)
                result.Close();
            DatabaseHelper.closeDatabaseConnection();
            MessageBox.Show("Could not find a user with the given credentials");
            return false;
        }

        private void Register_Button_Click(object sender, RoutedEventArgs e)
        {
            register(usernameText.Text, passwordText.Password);
        }

        public static bool register(string user, string pass) {
            //ensure the user does not already exist
            bool isFound = false;
            DatabaseHelper.openDatabaseConnection();
            var result = DatabaseHelper.getReaderForQuery("Select * From todo.user_profile", new SqlParameter[] { });

            while (result != null && result.Read()) {
                string userEmail = (string)result["email"];
                if (userEmail == user) {
                    MessageBox.Show("User for this email already exists");
                    isFound = true;
                    break;
                }
            }
            if (result != null)
                result.Close();
            //ensure the record was not found
            if (!isFound) {
                //ensure a password was entered
                if (pass.Length == 0) {
                    MessageBox.Show("Please Enter a password");
                    return false;
                } else {
                    //insert this user into the database
                    DatabaseHelper.performNonQuery("Insert into todo.user_profile (email, password) Values (@email, @passHash)", new SqlParameter[] {
                        new SqlParameter("@email", user), new SqlParameter("@passHash", CryptographyHelper.generateSHA256String(pass))});
                    MessageBox.Show("Successful Registration, please log in");
                    return true;
                }
            }
            DatabaseHelper.closeDatabaseConnection();
            return false;
        }

    }
}