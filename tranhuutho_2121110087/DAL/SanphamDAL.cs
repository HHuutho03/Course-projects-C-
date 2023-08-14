using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tranhuutho_2121110087.BEL;

namespace tranhuutho_2121110087.DAL
{
    public class SanphamDAL : DBConnection
    {
        DBConnection dbConnection = new DBConnection();

        public List<SanphamBEL> ReadHangHoa()
        {
            SqlConnection conn = CreateConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Sanpham", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            List<SanphamBEL> lstcl = new List<SanphamBEL>();
            while (reader.Read())
            {
                SanphamBEL cl = new SanphamBEL();
                cl.MaHang = int.Parse(reader["MaHang"].ToString());
                cl.TenHang = reader["TenHang"].ToString();
                cl.MaChatLieu = int.Parse(reader["MaChatLieu"].ToString());
                cl.SoLuong = int.Parse(reader["SoLuong"].ToString());
                cl.DonGiaNhap = float.Parse(reader["DonGiaNhap"].ToString());
                cl.DonGiaBan = float.Parse(reader["DonGiaBan"].ToString());
                cl.Anh = reader["Anh"].ToString();

                lstcl.Add(cl);
            }
            conn.Close();
            return lstcl;
        }

        public void DeleteHangHoa(SanphamBEL cl)
        {
            SqlConnection conn = CreateConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM Sanpham WHERE MaHang = @id", conn);
            cmd.Parameters.Add(new SqlParameter("@id", cl.MaHang));
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void NewHangHoa(SanphamBEL cl)
        {
            SqlConnection conn = CreateConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Sanpham (MaHang, TenHang, MaChatLieu, SoLuong, DonGiaNhap, DonGiaBan, Anh) VALUES (@id, @name, @maChatLieu, @soLuong, @donGiaNhap, @donGiaBan, @anh)", conn);
            cmd.Parameters.Add(new SqlParameter("@id", cl.MaHang));
            cmd.Parameters.Add(new SqlParameter("@name", cl.TenHang));
            cmd.Parameters.Add(new SqlParameter("@maChatLieu", cl.MaChatLieu));
            cmd.Parameters.Add(new SqlParameter("@soLuong", cl.SoLuong));
            cmd.Parameters.Add(new SqlParameter("@donGiaNhap", cl.DonGiaNhap));
            cmd.Parameters.Add(new SqlParameter("@donGiaBan", cl.DonGiaBan));
            cmd.Parameters.Add(new SqlParameter("@anh", cl.Anh));
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void EditHangHoa(SanphamBEL cl)
        {
            SqlConnection conn = CreateConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("UPDATE Sanpham SET TenHang = @name, MaChatLieu = @maChatLieu, SoLuong = @soLuong, DonGiaNhap = @donGiaNhap, DonGiaBan = @donGiaBan, Anh = @anh WHERE MaHang = @id", conn);
            cmd.Parameters.Add(new SqlParameter("@id", cl.MaHang));
            cmd.Parameters.Add(new SqlParameter("@name", cl.TenHang));
            cmd.Parameters.Add(new SqlParameter("@maChatLieu", cl.MaChatLieu));
            cmd.Parameters.Add(new SqlParameter("@soLuong", cl.SoLuong));
            cmd.Parameters.Add(new SqlParameter("@donGiaNhap", cl.DonGiaNhap));
            cmd.Parameters.Add(new SqlParameter("@donGiaBan", cl.DonGiaBan));
            cmd.Parameters.Add(new SqlParameter("@anh", cl.Anh));
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public bool CheckForeignKeyExists(int maChatLieu)
        {
            string sql = $"SELECT COUNT(*) FROM Sanpham WHERE MaChatLieu = {maChatLieu}";

            string result = dbConnection.GetFieldValues(sql);

            int count;
            if (int.TryParse(result, out count))
            {
                return count > 0;
            }

            return false;
        }
    }
}
