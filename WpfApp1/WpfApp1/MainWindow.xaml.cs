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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DatabaseHelper.openDatabaseConnection();
            var result = DatabaseHelper.getReaderForQuery("Select * From todo.list Where user_id = " + userID, new SqlParameter[] { });
            while (result.Read())
            {
                lstLists.Items.Add(result["name"]);
                listIDs.Add((long) result["id"]);

            }
            result.Close();
            DatabaseHelper.closeDatabaseConnection();
        }
    }
}
