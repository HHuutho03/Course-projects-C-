using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tranhuutho_2121110087.BEL;

namespace tranhuutho_2121110087.DAL
{
    public class KhachHangDAL : DBConnection
    {
        public List<KhachHangBEL> ReadKhachHang()
        {
            SqlConnection conn = CreateConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM KhachHang", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            List<KhachHangBEL> lstcl = new List<KhachHangBEL>();
            while (reader.Read())
            {
                KhachHangBEL cl = new KhachHangBEL();
                cl.MaKhachHang = int.Parse(reader["MaKhachHang"].ToString());
                cl.TenKhachHang = reader["TenKhachHang"].ToString();
                cl.DiaChi = reader["DiaChi"].ToString();
                cl.DienThoai= reader["DienThoai"].ToString();


                lstcl.Add(cl);
            }
            conn.Close();
            return lstcl;
        }

        public void DeleteKhachHang(KhachHangBEL cl)
        {
            SqlConnection conn = CreateConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM KhachHang WHERE MaKhachHang = @id", conn);
            cmd.Parameters.Add(new SqlParameter("@id", cl.MaKhachHang));
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void NewKhachHang(KhachHangBEL cl)
        {
            SqlConnection conn = CreateConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO KhachHang (MaKhachHang, TenKhachHang, DiaChi, DienThoai) VALUES (@id, @name, @diachi, @dienthoai)", conn);
            cmd.Parameters.Add(new SqlParameter("@id", cl.MaKhachHang));
            cmd.Parameters.Add(new SqlParameter("@name", cl.TenKhachHang));
            cmd.Parameters.Add(new SqlParameter("@diachi", cl.DiaChi));
            cmd.Parameters.Add(new SqlParameter("@dienthoai", cl.DienThoai));
         
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void EditKhachHang(KhachHangBEL cl)
        {
            SqlConnection conn = CreateConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("UPDATE KhachHang SET TenKhachHang = @name, DiaChi = @diachi, DienThoai = @dienthoai WHERE MaKhachHang = @id", conn);
            cmd.Parameters.Add(new SqlParameter("@id", cl.MaKhachHang));
            cmd.Parameters.Add(new SqlParameter("@name", cl.TenKhachHang));
            cmd.Parameters.Add(new SqlParameter("@diachi", cl.DiaChi));
            cmd.Parameters.Add(new SqlParameter("@dienthoai", cl.DienThoai));

            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
