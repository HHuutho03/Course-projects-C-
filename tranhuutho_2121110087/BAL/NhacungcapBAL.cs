using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tranhuutho_2121110087.DAL;
using tranhuutho_2121110087.BEL;
using System.Windows.Forms;

namespace tranhuutho_2121110087.BAL
{
    public class NhacungcapBAL
    {
        NhacungcapDAL clDAL = new NhacungcapDAL();
        SanphamDAL hhDAL = new SanphamDAL();


        public List<NhacungcapBEL> ReadChatLieu()
        {
            return clDAL.ReadChatLieu();
        }

        public bool CheckMaChatLieu(int maChatLieu)
        {
            List<NhacungcapBEL> lstCL = clDAL.ReadChatLieu();
            return lstCL.Any(cl => cl.MaChatLieu == maChatLieu);
        }

        public void NewChatLieu(NhacungcapBEL cl)
        {
            clDAL.NewChatLieu(cl);
        }

        public void DeleteChatLieu(NhacungcapBEL cl)
        {
                clDAL.DeleteChatLieu(cl);
        }

        public void EditChatLieu(NhacungcapBEL cl)
        {
            clDAL.EditChatLieu(cl);
        }
    }
}
