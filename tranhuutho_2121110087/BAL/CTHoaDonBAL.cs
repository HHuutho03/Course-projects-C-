using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tranhuutho_2121110087.DAL;
using tranhuutho_2121110087.BEL;
using System.Data.SqlClient;

namespace tranhuutho_2121110087.BAL
{
    public class CTHoaDonBAL
    {

        CTHoaDonDAL dal = new CTHoaDonDAL();
        private DBConnection dbConnection = new DBConnection();

        public List<CTHoaDonBEL> ReadCTHoaDon()
        {
            List<CTHoaDonBEL> lstHh = dal.ReadCTHoaDon();
            return lstHh;
        }

        public void NewCTHoaDon(CTHoaDonBEL hh)
        {
            dal.NewCTHoaDon(hh);
        }

        public void DeleteCTHoaDon(int maHD)
        {
            dal.DeleteCTHoaDon(maHD);
        }

        public void EditCTHoaDon(CTHoaDonBEL hh)
        {
            dal.EditCTHoaDon(hh);
        }
    }
}
