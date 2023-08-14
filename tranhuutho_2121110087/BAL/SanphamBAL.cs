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
    public class SanphamBAL
    {
        SanphamDAL dal = new SanphamDAL();
        private DBConnection dbConnection = new DBConnection();

        public bool CheckMaHang(int key)
        {
            using (SqlConnection conn = dbConnection.CreateConnection())
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM Sanpham WHERE MaHang = @key";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@key", key);
                    int count = (int)cmd.ExecuteScalar();

                    return count > 0;
                }
            }
        }

        public List<SanphamBEL> ReadHangHoa()
        {
            List<SanphamBEL> lstHh = dal.ReadHangHoa();
            return lstHh;
        }

        public void NewHangHoa(SanphamBEL hh)
        {
            dal.NewHangHoa(hh);
        }

        public void DeleteHangHoa(SanphamBEL hh)
        {
            dal.DeleteHangHoa(hh);
        }

        public void EditHangHoa(SanphamBEL hh)
        {
            dal.EditHangHoa(hh);
        }

    }
}
