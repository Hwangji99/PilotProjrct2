using System.Data;
using System.Windows;
using Microsoft.Data.SqlClient;

namespace Proj2.Repository
{
    class Repo
    {
        private readonly string _connString;

        public Repo(string connString)
        {
            _connString = connString;
        }

        public DataTable GetData()
        {
            using (SqlConnection connection = new SqlConnection(_connString))
            {
                try
                {
                    connection.Open();
                    string sql = "SELECT * FROM Product";
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    return table;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    return null;
                }
            }
        }
    }
}
