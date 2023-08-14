using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using tranhuutho_2121110087.BEL;

namespace tranhuutho_2121110087.DAL
{
    public class NhacungcapDAL : DBConnection
    {
        public List<NhacungcapBEL> ReadChatLieu()
        {
            SqlConnection conn = CreateConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from NhaCungCap", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            List<NhacungcapBEL> lstcl = new List<NhacungcapBEL>();
            while (reader.Read())
            {
                NhacungcapBEL cl = new NhacungcapBEL();
                cl.MaChatLieu = int.Parse(reader["MaChatLieu"].ToString());
                cl.TenChatLieu = reader["TenChatLieu"].ToString();
                lstcl.Add(cl);
            }
            conn.Close();
            return lstcl;
        }

        public void DeleteChatLieu(NhacungcapBEL cl)
        {
            SqlConnection conn = CreateConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM NhaCungCap WHERE MaChatLieu = @id", conn);
            cmd.Parameters.Add(new SqlParameter("@id", cl.MaChatLieu));
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void NewChatLieu(NhacungcapBEL cl)
        {
            SqlConnection conn = CreateConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("insert into NhaCungCap values (@id, @name)", conn);
            cmd.Parameters.Add(new SqlParameter("@id", cl.MaChatLieu));
            cmd.Parameters.Add(new SqlParameter("@name", cl.TenChatLieu));
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void EditChatLieu(NhacungcapBEL cl)
        {
            SqlConnection conn = CreateConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("update NhaCungCap set TenChatLieu = @name where MaChatLieu = @id", conn);
            cmd.Parameters.Add(new SqlParameter("@id", cl.MaChatLieu));
            cmd.Parameters.Add(new SqlParameter("@name", cl.TenChatLieu));
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
