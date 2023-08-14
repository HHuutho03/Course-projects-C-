using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tranhuutho_2121110087.BEL;

namespace tranhuutho_2121110087.DAL
{
    public class HoaDonDAL : DBConnection
    {
        public List<HoaDonBEL> ReadHoaDon()
        {
            SqlConnection conn = CreateConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM HoaDon", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            List<HoaDonBEL> lstcl = new List<HoaDonBEL>();
            while (reader.Read())
            {
                HoaDonBEL cl = new HoaDonBEL();
                cl.MaHD = int.Parse(reader["MaHD"].ToString());
                cl.MaKhachHang = int.Parse(reader["MaKhachHang"].ToString());
                cl.NgayBan = reader["NgayBan"].ToString();
                cl.TongTien = float.Parse(reader["TongTien"].ToString());

                lstcl.Add(cl);
            }
            conn.Close();
            return lstcl;
        }

        public void DeleteHoaDon(int maHD)
        {
            SqlConnection conn = CreateConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM HoaDon WHERE MaHD = @id", conn);
            cmd.Parameters.Add(new SqlParameter("@id", maHD));
            cmd.ExecuteNonQuery();
            conn.Close();
        }



        public void NewHoaDon(HoaDonBEL cl)
        {
            SqlConnection conn = CreateConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO HoaDon (MaHD, MaKhachHang, NgayBan, TongTien) VALUES (@id, @idkh, @ngayban, @tongtien)", conn);
            cmd.Parameters.Add(new SqlParameter("@id", cl.MaHD));
            cmd.Parameters.Add(new SqlParameter("@idkh", cl.MaKhachHang));
            cmd.Parameters.Add(new SqlParameter("@ngayban", cl.NgayBan));
            cmd.Parameters.Add(new SqlParameter("@tongtien", cl.TongTien));
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
