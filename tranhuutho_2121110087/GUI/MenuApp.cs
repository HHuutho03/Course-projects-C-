using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tranhuutho_2121110087.GUI
{
    public partial class MenuApp : Form
    {
        public MenuApp()
        {
            InitializeComponent();
        }

        private void quanLyKhachHangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            KhachHangGUI myForm = new KhachHangGUI();
            myForm.Dock = System.Windows.Forms.DockStyle.Fill;
            myForm.TopLevel = false;
            myForm.AutoScroll = true;
            panel1.Controls.Add(myForm);
            myForm.Show();
        }

        private void quanLyHoaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            HoaDonGUI myForm = new HoaDonGUI();
            myForm.Dock = System.Windows.Forms.DockStyle.Fill;
            myForm.TopLevel = false;
            myForm.AutoScroll = true;
            panel1.Controls.Add(myForm);
            myForm.Show();
        }

        private void quanLyChâtChiêuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            NhacungcapGUI myForm = new NhacungcapGUI();
            myForm.Dock = System.Windows.Forms.DockStyle.Fill;
            myForm.TopLevel = false;
            myForm.AutoScroll = true;
            panel1.Controls.Add(myForm);
            myForm.Show();
        }

        private void đăngXuâtToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void hangHoaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            SanphamGUI myForm = new SanphamGUI();
            myForm.Dock = System.Windows.Forms.DockStyle.Fill;
            myForm.TopLevel = false;
            myForm.AutoScroll = true;
            panel1.Controls.Add(myForm);
            myForm.Show();
        }

        private void thoatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn tiếp tục không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void MenuApp_Load(object sender, EventArgs e)
        {

        }

        private void đăngXuâtToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            DangNhapGUI dg = new DangNhapGUI();
            dg.Show();
        }
    }
}
