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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToDo.Static;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public long userID;
        public List<long> listIDs = new List<long>();
        public List<long> taskIDs = new List<long>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnNewTodo_Click(object sender, RoutedEventArgs e)
        {
            if (lstLists.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a list to add a todo for");
                return;
            }
            var newTodoWindow = new NewTodoWindow();
            newTodoWindow.userID = userID;
            newTodoWindow.listId = listIDs[lstLists.SelectedIndex];
            newTodoWindow.ShowDialog();
        }

        private void BtnNewList_Click(object sender, RoutedEventArgs e)
        {
            var newListWindow = new NewListWindow();
            newListWindow.userID = userID;
            newListWindow.ShowDialog();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            lstLists.Items.Clear();
            DatabaseHelper.openDatabaseConnection();
            var result = DatabaseHelper.getReaderForQuery("Select * From todo.list Where user_id = " + userID, new SqlParameter[] { });
            if (result == null) { return; }
            while (result.Read())
            {
                lstLists.Items.Add(result["name"]);
                listIDs.Add((long)result["id"]);
            }
            result.Close();
            DatabaseHelper.closeDatabaseConnection();
        }

        private void LstLists_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            stkTodos.Children.Clear();
            taskIDs.Clear();
            if (lstLists.SelectedIndex == -1) { return; }
            DatabaseHelper.openDatabaseConnection();
            var result = DatabaseHelper.getReaderForQuery($"SELECT * FROM todo.Task WHERE list_id={listIDs[lstLists.SelectedIndex]}", new SqlParameter[] { });
            if (result == null) { return; }
            while (result.Read())
            {
                var todoCheck = new CheckBox();
                todoCheck.Content = $"{result["name"]} at location ({result["loc_lat"]}, {result["loc_long"]}) due {((DateTime)result["due_date"]).ToShortDateString()}";
                todoCheck.IsChecked = (bool)result["is_completed"];
                todoCheck.Checked += new RoutedEventHandler(todoChecked);
                todoCheck.Unchecked += new RoutedEventHandler(todoUnchecked);
                stkTodos.Children.Add(todoCheck);
                taskIDs.Add((long)result["id"]);
            }
            result.Close();
            DatabaseHelper.closeDatabaseConnection();
        }

        private void todoChecked(object sender, EventArgs args)
        {
            //find index of this check box in the stack
            var found = false;
            var index = 0;
            foreach (var child in stkTodos.Children)
            {
                if (child == sender)
                {
                    found = true;
                    break;
                }
                index++; 
            }
            if (!found)
            {
                return;
            }
            DatabaseHelper.openDatabaseConnection();
            DatabaseHelper.performNonQuery("UPDATE todo.task SET is_completed = 1 WHERE id = " + taskIDs[index], new SqlParameter[] { });
            DatabaseHelper.closeDatabaseConnection();
        }

        private void todoUnchecked(object sender, EventArgs args)
        {
            //find index of this check box in the stack
            var found = false;
            var index = 0;
            foreach (var child in stkTodos.Children)
            {
                if (child == sender)
                {
                    found = true;
                    break;
                }
                index++;
            }
            if (!found)
            {
                return;
            }
            DatabaseHelper.openDatabaseConnection();
            DatabaseHelper.performNonQuery("UPDATE todo.task SET is_completed = 0 WHERE id = " + taskIDs[index], new SqlParameter[] { });
            DatabaseHelper.closeDatabaseConnection();
        }
    }
}
