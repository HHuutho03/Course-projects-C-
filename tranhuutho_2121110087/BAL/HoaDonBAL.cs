using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tranhuutho_2121110087.DAL;
using tranhuutho_2121110087.BEL;

namespace tranhuutho_2121110087.BAL
{
    public class HoaDonBAL
    {

            HoaDonDAL dal = new HoaDonDAL();
            private DBConnection dbConnection = new DBConnection();

            public bool CheckMaHD(int key)
            {
                using (SqlConnection conn = dbConnection.CreateConnection())
                {
                    conn.Open();
                    string sql = "SELECT COUNT(*) FROM HoaDon WHERE MaHD = @key";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@key", key);
                        int count = (int)cmd.ExecuteScalar();

                        return count > 0;
                    }
                }
            }

            public List<HoaDonBEL> ReadHoaDon()
            {
                List<HoaDonBEL> lstHh = dal.ReadHoaDon();
                return lstHh;
            }

            public void NewHoaDon(HoaDonBEL hh)
            {
                dal.NewHoaDon(hh);
            }

            public void DeleteHoaDon(int maHD)
            {
                using (SqlConnection conn = dbConnection.CreateConnection())
                {
                    conn.Open();
                    string sql = "DELETE FROM HoaDon WHERE MaHD = @id";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", maHD));
                        cmd.ExecuteNonQuery();
                        conn.Close();
                }
                }
            }

    }
}
