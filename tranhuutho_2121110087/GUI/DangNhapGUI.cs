using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using tranhuutho_2121110087.GUI;

namespace tranhuutho_2121110087.GUI
{
    public partial class DangNhapGUI : Form
    {
        public DangNhapGUI()
        {
            InitializeComponent();

            txtUsername.PasswordChar = '*';

            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (username != "admin" || password != "admin")
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không đúng.");
            }
            else
            {
                this.Hide();
                MenuApp menu = new MenuApp();
                menu.Show();
            }
        }
    }
}
