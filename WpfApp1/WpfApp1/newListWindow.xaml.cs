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
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class NewListWindow : Window
    {
        public long userID = 0;

        public NewListWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //when list name is valid, add it to the database for this user
            if (nameText.Text != null || nameText.Text != "") {
                DatabaseHelper.openDatabaseConnection();
                DatabaseHelper.performNonQuery($"INSERT INTO todo.list(user_id, name) VALUES ({userID},'{nameText.Text}')", new SqlParameter[] {});
                DatabaseHelper.closeDatabaseConnection();
                Close();
            }
        } 
    }
}
