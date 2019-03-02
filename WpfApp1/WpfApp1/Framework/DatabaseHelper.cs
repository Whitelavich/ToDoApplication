using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ToDo.Static
{
    public static class DatabaseHelper
    {
        public static SqlConnection dbConnection;

        //open the connection on Application Start up
        static DatabaseHelper()
        {

        }


        public static void openDatabaseConnection()
        {
            dbConnection = new SqlConnection();
            dbConnection.ConnectionString = "Data Source=munchies.ci817zs9fbxu.us-east-1.rds.amazonaws.com;Initial Catalog=Munchies;Persist Security Info=True;User ID=esda98;Password=Goldpod663!";
            dbConnection.Open();
        }
        public static void closeDatabaseConnection()
        {
            dbConnection.Close();
        }

        public static SqlDataReader getReaderForQuery(string query, SqlParameter[] parameters)
        {
            SqlCommand select = new SqlCommand(query, dbConnection);
            foreach (SqlParameter p in parameters)
                select.Parameters.Add(p);
            SqlDataReader reader = select.ExecuteReader();
            //pass back null if the query returned nothing
            if (!reader.HasRows)
            {
                reader.Close();
                return null;
            }
            return reader;
        }
        public static void performNonQuery(string nonquery, SqlParameter[] parameters)
        {
            SqlCommand insertCMD = new SqlCommand(nonquery, dbConnection);
            //add all passed parameters to the command
            foreach(SqlParameter p in parameters)
                insertCMD.Parameters.Add(p);
            insertCMD.ExecuteNonQuery();
        }
    }
}
