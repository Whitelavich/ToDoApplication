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

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnNewTodo_Click(object sender, RoutedEventArgs e)
        {
            //ensure a list was selected
            if (lstLists.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a list to add a todo for");
                return;
            }
            //display new todo creation window as dialog
            var newTodoWindow = new NewTodoWindow();
            newTodoWindow.userID = userID;
            newTodoWindow.listId = listIDs[lstLists.SelectedIndex];
            newTodoWindow.ShowDialog();
        }

        private void BtnNewList_Click(object sender, RoutedEventArgs e)
        {
            //display new list creation window as dialog
            var newListWindow = new NewListWindow();
            newListWindow.userID = userID;
            newListWindow.ShowDialog();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            //refresh todo lists when the window is activated (shown)
            lstLists.Items.Clear();
            listIDs.Clear();
            DatabaseHelper.openDatabaseConnection();
            var result = DatabaseHelper.getReaderForQuery("Select * From todo.list Where user_id = @userId", new SqlParameter[] { new SqlParameter("@userId", userID)});
            if (result == null) { return; }
            //add all lists to the list view
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
            //clear todos from previous list selection
            stkTodos.Children.Clear();
            if (lstLists.SelectedIndex == -1) { return; }
            DatabaseHelper.openDatabaseConnection();
            //get all todos for this list
            var result = DatabaseHelper.getReaderForQuery($"SELECT * FROM todo.Task WHERE list_id={listIDs[lstLists.SelectedIndex]}", new SqlParameter[] {});
            if (result == null) { return; }
            //put all todos on stackpanel
            while (result.Read())
            {
                //create UserControl instance to display the task (usercontrol gives delete button)
                var displayName = $"{(string)result["name"]} at ({(decimal)result["loc_lat"]}, {(decimal)result["loc_long"]}) due {((DateTime)result["due_date"]).ToShortDateString()}";
                var todoCheck = new UserControls.Task((long)result["id"], displayName, (bool)result["is_completed"]);
                stkTodos.Children.Add(todoCheck);
            }
            result.Close();
            DatabaseHelper.closeDatabaseConnection();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Destroy this window and show the login window again
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //display the edit user window as a dialog
            var editUser = new EditUser(userID);
            editUser.ShowDialog();
        }
    }
}
