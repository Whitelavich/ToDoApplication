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
using ToDo.Static;
using System.Data.SqlClient;

namespace WpfApp1.UserControls
{
    /// <summary>
    /// Interaction logic for Task.xaml
    /// </summary>
    public partial class Task : UserControl
    {
        private long taskID;
        private string taskName;
        private bool taskFinished;
        public Task(long givenID, string givenName, bool givenFinish)
        {
            InitializeComponent();
            //set instance properties
            taskID = givenID;
            taskName = givenName;
            taskFinished = givenFinish;
            //using new properties, refresh the UI
            refreshUI();
        }

        public void refreshUI()
        {
            chkTask.IsChecked = taskFinished;
            chkTask.Content = taskName;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            //Remove task from the database
            DatabaseHelper.openDatabaseConnection();
            DatabaseHelper.performNonQuery("DELETE FROM todo.Task WHERE id = " + taskID, new SqlParameter[] { });
            DatabaseHelper.closeDatabaseConnection();
            ((StackPanel)VisualParent).Children.Remove(this);
        }

        private void ChkTask_Checked(object sender, RoutedEventArgs e)
        {
            var isChecked = 0;
            if (((CheckBox)sender).IsChecked == true)
            {
                isChecked = 1;
            }
            //set completion status in database for this task
            DatabaseHelper.openDatabaseConnection();
            DatabaseHelper.performNonQuery($"UPDATE todo.task SET is_completed = {isChecked} WHERE id = {taskID}", new SqlParameter[] { });
            DatabaseHelper.closeDatabaseConnection();
        }
    }
}
