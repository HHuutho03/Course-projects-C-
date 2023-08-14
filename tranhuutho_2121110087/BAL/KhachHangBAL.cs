using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tranhuutho_2121110087.BEL;
using tranhuutho_2121110087.DAL;

namespace tranhuutho_2121110087.BAL
{
    public class KhachHangBAL
    {
        KhachHangDAL dal = new KhachHangDAL();
        private DBConnection dbConnection = new DBConnection();

        public bool CheckMaKhachHang(int key)
        {
            using (SqlConnection conn = dbConnection.CreateConnection())
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM KhachHang WHERE MaKhachHang = @key";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@key", key);
                    int count = (int)cmd.ExecuteScalar();

                    return count > 0;
                }
            }
        }

        public List<KhachHangBEL> ReadKhachHang()
        {
            List<KhachHangBEL> lstHh = dal.ReadKhachHang();
            return lstHh;
        }

        public void NewKhachHang(KhachHangBEL hh)
        {
            dal.NewKhachHang(hh);
        }

        public void DeleteKhachHang(KhachHangBEL hh)
        {
            dal.DeleteKhachHang(hh);
        }

        public void EditKhachHang(KhachHangBEL hh)
        {
            dal.EditKhachHang(hh);
        }
    }
}
