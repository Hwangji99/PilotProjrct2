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

        //public bool AddData(string productName, string code, int quantity, string explanation, string brand, string nowUser)
        //{
        //    try
        //    {
        //        // 파라미터 확인
        //        MessageBox.Show($"ProductName: {productName}, Code: {code}, Quantity: {quantity}, Explanation: {explanation}, Brand: {brand}, NowUser: {nowUser}");

        //        using (var conn = new SqlConnection(_connString))
        //        {
        //            conn.Open();
        //            var query = "INSERT INTO Product (ProductName, Code, Quantity, Explanation, Brand, NowUser) " +
        //                        "VALUES (@ProductName, @Code, @Quantity, @Explanation, @Brand, @NowUser)";
        //            using (var cmd = new SqlCommand(query, conn))
        //            {
        //                cmd.Parameters.AddWithValue("@ProductName", productName);
        //                cmd.Parameters.AddWithValue("@Code", code);
        //                cmd.Parameters.AddWithValue("@Quantity", quantity);
        //                cmd.Parameters.AddWithValue("@Explanation", explanation);
        //                cmd.Parameters.AddWithValue("@Brand", brand);
        //                cmd.Parameters.AddWithValue("@NowUser", nowUser);

        //                cmd.ExecuteNonQuery();
        //            }
        //        }

        //        return true; // 성공
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error: " + ex.Message);
        //        return false; // 실패
        //    }
        //}



        public bool DeleteData(string productCode)
        {
            try
            {
                using (var connection = new SqlConnection(_connString))
                {
                    string query = "DELETE FROM Product WHERE Code = @ProductCode";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductCode", productCode);
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;  // 삭제가 성공적으로 이루어졌으면 true 반환
                    }
                }
            }
            catch (Exception ex)
            {
                // 예외 처리 (로그 등)
                MessageBox.Show($"삭제 중 오류가 발생했습니다: {ex.Message}");
                return false;
            }
        }

    }
}
