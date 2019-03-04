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
    /// Interaction logic for Window3.xaml
    /// </summary>
    public partial class NewTodoWindow : Window
    {
        public long userID = 0;
        public long listId = 0;
        public NewTodoWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (nameText.Text != null || nameText.Text != "" 
                && descriptionText.Text != null || descriptionText.Text != "" 
                && latitudeText.Text != null || latitudeText.Text != "" 
                && longitudeText.Text != null || longitudeText.Text != "" 
                && dueDateText.Text != null || dueDateText.Text != "")
            {
                if (DateTime.Parse(dueDateText.Text) == null)
                {
                    MessageBox.Show("invalid date provided");
                    return;
                }
                DatabaseHelper.openDatabaseConnection();
                DatabaseHelper.performNonQuery($"INSERT INTO todo.task(list_id,name,description,is_completed,loc_lat,loc_long,due_date) VALUES ({listId}, '{nameText.Text}' ,'{descriptionText.Text}',0,{latitudeText.Text},{longitudeText.Text},'{DateTime.Parse(dueDateText.Text)}')",new SqlParameter[] { });
            
                DatabaseHelper.closeDatabaseConnection();
                Close();
            }
            else
            {
                MessageBox.Show("Missing a field");
            }
        }
    }
}
