using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tranhuutho_2121110087
{
    public class Utils
    {
        public bool ValidateInputType(TextBox textBox, Type expectedType, string fieldName)
        {
            if (expectedType == typeof(int))
            {
                if (!int.TryParse(textBox.Text, out _))
                {
                    MessageBox.Show($"Trường {fieldName} phải là số nguyên!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }

        internal bool ValidateInput(TextBox txtMaHang, string v)
        {
            throw new NotImplementedException();
        }
    }
}
