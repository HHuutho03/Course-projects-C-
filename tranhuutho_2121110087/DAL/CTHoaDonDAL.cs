using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tranhuutho_2121110087.BAL;
using tranhuutho_2121110087.BEL;

namespace tranhuutho_2121110087.DAL
{
    public class CTHoaDonDAL : DBConnection
    {
        public List<CTHoaDonBEL> ReadCTHoaDon()
        {
            SqlConnection conn = CreateConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from CTHoaDon", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            List<CTHoaDonBEL> lstcl = new List<CTHoaDonBEL>();
            while (reader.Read())
            {
                CTHoaDonBEL cl = new CTHoaDonBEL();
                cl.MaHD = int.Parse(reader["MaHD"].ToString());
                cl.MaHang = int.Parse(reader["MaHang"].ToString());
                cl.Soluong = int.Parse(reader["Soluong"].ToString());
                cl.DonGia = float.Parse(reader["DonGia"].ToString());

                lstcl.Add(cl);
            }
            conn.Close();
            return lstcl;
        }

        public void DeleteCTHoaDon(int maHD)
        {
            SqlConnection conn = CreateConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM CTHoaDon WHERE MaHD = @id", conn);
            cmd.Parameters.Add(new SqlParameter("@id", maHD));
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void NewCTHoaDon(CTHoaDonBEL cl)
        {
            SqlConnection conn = CreateConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("insert into CTHoaDon values (@id, @mahang, @soluong, @dongia)", conn);
            cmd.Parameters.Add(new SqlParameter("@id", cl.MaHD));
            cmd.Parameters.Add(new SqlParameter("@mahang", cl.MaHang));
            cmd.Parameters.Add(new SqlParameter("@soluong", cl.Soluong));
            cmd.Parameters.Add(new SqlParameter("@dongia", cl.DonGia));
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void EditCTHoaDon(CTHoaDonBEL cl)
        {
            SqlConnection conn = CreateConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("update CTHoaDon set MaHang = @mahang, Soluong = @soluong, Dongia = @dongia where MaHD = @id", conn);
            cmd.Parameters.Add(new SqlParameter("@id", cl.MaHD));
            cmd.Parameters.Add(new SqlParameter("@mahang", cl.MaHang));
            cmd.Parameters.Add(new SqlParameter("@soluong", cl.Soluong));
            cmd.Parameters.Add(new SqlParameter("@dongia", cl.DonGia));
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
