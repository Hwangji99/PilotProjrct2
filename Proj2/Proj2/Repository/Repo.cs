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

        public bool AddtData(string productName, string code, int quantity, string explanation, string brand, string nowUser)
        {
            try
            {
                using (var connection = new SqlConnection(_connString))
                {
                    string query = @"
                INSERT INTO Product (ProductName, Code, Quantity, Explanation, Brand, NowUser)
                VALUES (@ProductName, @Code, @Quantity, @Explanation, @Brand, @NowUser)";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductName", productName);
                        command.Parameters.AddWithValue("@Code", code);
                        command.Parameters.AddWithValue("@Quantity", quantity);
                        command.Parameters.AddWithValue("@Explanation", explanation);
                        command.Parameters.AddWithValue("@Brand", brand);
                        command.Parameters.AddWithValue("@NowUser", nowUser);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0; // 성공하면 true 반환
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"데이터 삽입 중 오류 발생: {ex.Message}");
                return false;
            }
        }

        public bool UpdateData(string productCode, string productName, int quantity, string explanation, string brand, string nowUser)
        {
            try
            {
                using (var connection = new SqlConnection(_connString))
                {
                    string query = @"UPDATE Product
                             SET ProductName = @ProductName, Quantity = @Quantity,
                                 Explanation = @Explanation, Brand = @Brand, NowUser = @NowUser
                             WHERE Code = @ProductCode";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductCode", productCode);
                        command.Parameters.AddWithValue("@ProductName", productName);
                        command.Parameters.AddWithValue("@Quantity", quantity);
                        command.Parameters.AddWithValue("@Explanation", explanation);
                        command.Parameters.AddWithValue("@Brand", brand);
                        command.Parameters.AddWithValue("@NowUser", nowUser);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0; // 업데이트가 성공적으로 이루어졌는지 확인
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"데이터 수정 중 오류가 발생했습니다: {ex.Message}");
                return false;
            }
        }

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
