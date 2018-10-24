using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Server
{
    class DAO
    {
        private SqlConnectionStringBuilder builder = null;
        private SqlConnection connection = null;
        private SqlCommand command = null;
        public DAO()
        {
            try
            {
                builder = new SqlConnectionStringBuilder();
                builder.DataSource = "TRUONGDAO-PC\\KhangHoa";
                builder.UserID = "sa";
                builder.Password = "hct";
                builder.InitialCatalog = "master";
                connection = new SqlConnection(builder.ConnectionString);
                connection.Open();
                MessageBox.Show("DAO: connect thanh cong");
            }
            catch
            {
                MessageBox.Show("DAO: không connect được tới cơ sở dữ liệu");
                return;
            }
        }
        public void add(Student st)
        {
            try
            {
                string sql = "INSERT INTO HTPT.dbo.student (name,lop,diem1,diem2,diem3,diemTB) VALUES (@name,@lop,@diem1,@diem2,@diem3,@diemTB);";
                command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@name", st.Name);
                command.Parameters.AddWithValue("@lop", st.Lop);
                command.Parameters.AddWithValue("@diem1", st.Diem[0]);
                command.Parameters.AddWithValue("@diem2", st.Diem[1]);
                command.Parameters.AddWithValue("@diem3", st.Diem[2]);
                command.Parameters.AddWithValue("@diemTB", st.DiemTB);
                command.ExecuteNonQuery();
                close();
                MessageBox.Show("DAO: ghi thanh cong csdl");
            }
            catch
            {
                MessageBox.Show("DAO: ghi vào cơ sở dữ liệu thất bại");
            }
        }
        public void close()
        {
            connection.Close();
        }
    }
}
