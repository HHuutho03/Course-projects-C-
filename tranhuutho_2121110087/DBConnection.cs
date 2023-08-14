using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using tranhuutho_2121110087.GUI;

namespace tranhuutho_2121110087
{
    public class DBConnection
    {
        public SqlConnection CreateConnection()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=HUUTHO\SQLEXPRESS; Initial Catalog=QLsanpham; User Id=sa;Password=123";
            return conn;
        }

        public string GetFieldValues(string sql)
        {
            string result = null;
            using (SqlConnection connection = CreateConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        object queryResult = command.ExecuteScalar();
                        if (queryResult != null)
                        {
                            result = queryResult.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return result;
        }

        public void ExitForm() {
            DialogResult result = MessageBox.Show("Bạn có muốn tiếp tục không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

    }
}
