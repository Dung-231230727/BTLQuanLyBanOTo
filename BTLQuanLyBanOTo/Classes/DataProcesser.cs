using System.Data;
using System.Data.SqlClient;

namespace BTLQuanLyBanOTo.Classes
{
    internal class DataProcesser
    {
        //
        private SqlConnection conn;
        private string connectionString;

        //hàm tạo có đối nhân chuỗi kết nối
        public DataProcesser(string connStr)
        {
            //gán và khởi tạo
            connectionString = connStr;
            conn = new SqlConnection(connStr);
        }

        //mở kết nối
        public void OpenConnection()
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
        }

        //đóng kết nối
        public void CloseConnection()
        {
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
        }

        //hàm thực thi câu lệnh select -> trả về bảng
        public DataTable ExecuteQuery(string query, SqlParameter[] prms = null)
        {
            DataTable dt = new DataTable();
            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (prms != null)
                    {
                        cmd.Parameters.AddRange(prms);
                    }
                    SqlDataAdapter adt = new SqlDataAdapter(cmd);
                    adt.Fill(dt);
                }
            }
            finally
            {
                CloseConnection();
            }
            return dt;
        }

        //thực thi câu lệnh ins, upd, del -> trả về số dòng bị ảnh hưởng
        public int ExecuteNonQuery(string query, SqlParameter[] prms = null)
        {
            int rows = 0;
            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (prms != null)
                    {
                        cmd.Parameters.AddRange(prms);
                    }
                    rows = cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                CloseConnection();
            }
            return rows;
        }

        //thực thi truy vấn -> trả về một giá trị (count, max, min, sum, ...)
        public object ExecuteScalar(string query, SqlParameter[] prms = null)
        {
            object o = null;
            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (prms != null)
                    {
                        cmd.Parameters.AddRange(prms);
                    }
                    o = cmd.ExecuteScalar();
                }
            }
            finally
            {
                CloseConnection();
            }
            return o;
        }
    }
}
