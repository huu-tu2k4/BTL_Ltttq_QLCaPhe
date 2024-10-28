using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_Ltttq_QLCaPhe.DAO
{
    public class DataProvider
    {
        private static DataProvider instance;
        private string connectionString = ""; /*gắn conncetionString của cơ sở dữ liệu vào đây*/

        public static DataProvider Instance 
        { 
            get { if (instance == null) instance = new DataProvider(); return DataProvider.instance; }
            private set { DataProvider.instance = value; }
        }

        private DataProvider() { }
        public DataTable ExecuteQuery(string query, object[] parameter = null)
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);  
                if(parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string iteam in listPara)
                    {
                        if (iteam.Contains("@"))
                        {
                            cmd.Parameters.AddWithValue(iteam, parameter[i]);
                            i++;
                        }
                    }
                }
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(data);
                connection.Close();
                
            }
            return data;
        }
        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            int data = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string iteam in listPara)
                    {
                        if (iteam.Contains("@"))
                        {
                            cmd.Parameters.AddWithValue(iteam, parameter[i]);
                            i++;
                        }
                    }
                }
                
                data = cmd.ExecuteNonQuery();
                connection.Close();
            }
            return data;
        }

        public object ExecuteScalar(string query, object[] parameter = null)
        {
            object data = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string iteam in listPara)
                    {
                        if (iteam.Contains("@"))
                        {
                            cmd.Parameters.AddWithValue(iteam, parameter[i]);
                            i++;
                        }
                    }
                }

                data = cmd.ExecuteScalar();
                connection.Close();
            }
            return data;
        }
    }
}
