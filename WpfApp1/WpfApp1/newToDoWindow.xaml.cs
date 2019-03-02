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
    public partial class Window3 : Window
    {
        public int userID = 0;
        public int listId = 0;
        public Window3()
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
                DatabaseHelper.openDatabaseConnection();
                DatabaseHelper.performNonQuery($"INSERT INTO todo.task(list_id,name,description,is_completed,loc_lat,loc_long,due_date)" +
                    $" VALUES({listId}, {nameText.Text} ,{ descriptionText.Text} ,FALSE,{latitudeText.Text},{longitudeText.Text},{dueDateText.Text } )",new SqlParameter[] { });
            
                DatabaseHelper.closeDatabaseConnection();
            }
        }
    }
}
