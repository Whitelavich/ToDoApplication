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
using ToDo.Static;
using System.Data.SqlClient;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for EditUser.xaml
    /// </summary>
    public partial class EditUser : Window
    {
        private long userID;

        public EditUser(long givenUserID)
        {
            InitializeComponent();
            userID = givenUserID;
            //get user profile information
            DatabaseHelper.openDatabaseConnection();
            var reader = DatabaseHelper.getReaderForQuery("SELECT * FROM todo.user_profile WHERE id = " + userID, new SqlParameter[] { });
            if (reader == null) { return; }
            reader.Read();
            //display
            txtEmail.Text = (string)reader["email"];
            txtPassword.Password = (string)reader["password"];
            DatabaseHelper.closeDatabaseConnection();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            //update record in database
            DatabaseHelper.openDatabaseConnection();
            DatabaseHelper.performNonQuery($"UPDATE todo.user_profile SET email='{txtEmail.Text}', password = '{txtPassword.Password}' WHERE id={userID}", new SqlParameter[] { });
            DatabaseHelper.closeDatabaseConnection();
            //close this dialog
            Close();
        }
    }
}
