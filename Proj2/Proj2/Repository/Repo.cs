using System.Data;
using System.Windows;
using Microsoft.Data.SqlClient;

namespace Proj2.Repository
{
    // Repo 클래스는 데이터베이스와의 통신을 담당하는 클래스입니다.
    class Repo
    {
        // _connString은 데이터베이스 연결 문자열을 저장하는 변수입니다.
        private readonly string _connString;

        // 생성자: 데이터베이스 연결 문자열을 받아서 _connString에 저장합니다.
        public Repo(string connString)
        {
            _connString = connString;
        }

        // GetData 메서드는 Product 테이블에서 모든 데이터를 조회하여 반환합니다.
        public DataTable GetData()
        {
            using (SqlConnection connection = new SqlConnection(_connString))  // SqlConnection 객체를 사용해 데이터베이스 연결을 관리합니다.
            {
                try
                {
                    connection.Open();  // 데이터베이스 연결을 엽니다.
                    string sql = "SELECT * FROM Product";  // 실행할 SQL 쿼리문입니다.
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);  // 데이터 어댑터를 사용하여 쿼리 실행 및 데이터 가져오기
                    DataTable table = new DataTable();  // 데이터를 담을 테이블 객체를 생성합니다.
                    adapter.Fill(table);  // 쿼리 결과를 DataTable에 채웁니다.

                    return table;  // 결과로 채운 DataTable을 반환합니다.
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);  // 예외가 발생하면 오류 메시지를 보여줍니다.
                    return null;  // 오류 발생 시 null을 반환합니다.
                }
            }
        }

        // AddtData 메서드는 새로운 제품 데이터를 Product 테이블에 추가합니다.
        public bool AddtData(string productName, string code, int quantity, string explanation, string brand, string nowUser)
        {
            try
            {
                // SqlConnection을 사용해 데이터베이스 연결을 생성합니다.
                using (var connection = new SqlConnection(_connString))
                {
                    // 실행할 SQL 쿼리문입니다.
                    string query = @"
                    INSERT INTO Product (ProductName, Code, Quantity, Explanation, Brand, NowUser)
                    VALUES (@ProductName, @Code, @Quantity, @Explanation, @Brand, @NowUser)";

                    // SqlCommand 객체를 사용하여 쿼리문을 실행할 준비를 합니다.
                    using (var command = new SqlCommand(query, connection))
                    {
                        // 파라미터를 추가하여 SQL 쿼리문에 값을 바인딩합니다.
                        command.Parameters.AddWithValue("@ProductName", productName);
                        command.Parameters.AddWithValue("@Code", code);
                        command.Parameters.AddWithValue("@Quantity", quantity);
                        command.Parameters.AddWithValue("@Explanation", explanation);
                        command.Parameters.AddWithValue("@Brand", brand);
                        command.Parameters.AddWithValue("@NowUser", nowUser);

                        connection.Open();  // 데이터베이스 연결을 엽니다.
                        int rowsAffected = command.ExecuteNonQuery();  // 쿼리 실행 및 영향 받은 행 수를 반환합니다.
                        return rowsAffected > 0;  // 행이 하나 이상 영향을 받았다면 성공, true 반환
                    }
                }
            }
            catch (Exception ex)
            {
                // 예외 발생 시 오류 메시지를 표시합니다.
                MessageBox.Show($"데이터 삽입 중 오류 발생: {ex.Message}");
                return false;  // 오류 발생 시 false 반환
            }
        }

        // UpdateData 메서드는 기존 제품 정보를 업데이트합니다.
        public bool UpdateData(string productCode, string productName, int quantity, string explanation, string brand, string nowUser)
        {
            try
            {
                using (var connection = new SqlConnection(_connString))
                {
                    // 실행할 SQL 쿼리문입니다. 제품 코드로 특정 제품을 찾아 업데이트합니다.
                    string query = @"UPDATE Product
                                     SET ProductName = @ProductName, Quantity = @Quantity,
                                         Explanation = @Explanation, Brand = @Brand, NowUser = @NowUser
                                     WHERE Code = @ProductCode";

                    using (var command = new SqlCommand(query, connection))
                    {
                        // 파라미터를 추가하여 SQL 쿼리문에 값을 바인딩합니다.
                        command.Parameters.AddWithValue("@ProductCode", productCode);
                        command.Parameters.AddWithValue("@ProductName", productName);
                        command.Parameters.AddWithValue("@Quantity", quantity);
                        command.Parameters.AddWithValue("@Explanation", explanation);
                        command.Parameters.AddWithValue("@Brand", brand);
                        command.Parameters.AddWithValue("@NowUser", nowUser);

                        connection.Open();  // 데이터베이스 연결을 엽니다.
                        int rowsAffected = command.ExecuteNonQuery();  // 쿼리 실행 후 영향을 받은 행 수를 반환합니다.
                        return rowsAffected > 0;  // 업데이트가 성공적으로 이루어졌는지 확인하고 true/false 반환
                    }
                }
            }
            catch (Exception ex)
            {
                // 예외 발생 시 오류 메시지를 표시합니다.
                MessageBox.Show($"데이터 수정 중 오류가 발생했습니다: {ex.Message}");
                return false;  // 오류 발생 시 false 반환
            }
        }

        // DeleteData 메서드는 제품을 삭제합니다.
        public bool DeleteData(string productCode)
        {
            try
            {
                using (var connection = new SqlConnection(_connString))
                {
                    // 실행할 SQL 쿼리문입니다. 제품 코드로 특정 제품을 찾아 삭제합니다.
                    string query = "DELETE FROM Product WHERE Code = @ProductCode";
                    using (var command = new SqlCommand(query, connection))
                    {
                        // 파라미터를 추가하여 SQL 쿼리문에 값을 바인딩합니다.
                        command.Parameters.AddWithValue("@ProductCode", productCode);
                        connection.Open();  // 데이터베이스 연결을 엽니다.
                        int rowsAffected = command.ExecuteNonQuery();  // 쿼리 실행 후 영향을 받은 행 수를 반환합니다.
                        return rowsAffected > 0;  // 삭제가 성공적으로 이루어졌으면 true 반환
                    }
                }
            }
            catch (Exception ex)
            {
                // 예외 발생 시 오류 메시지를 표시합니다.
                MessageBox.Show($"삭제 중 오류가 발생했습니다: {ex.Message}");
                return false;  // 오류 발생 시 false 반환
            }
        }
    }
}
