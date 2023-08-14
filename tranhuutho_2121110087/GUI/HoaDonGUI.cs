using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq.Expressions;
using System.Windows.Forms;
using tranhuutho_2121110087.BAL;
using tranhuutho_2121110087.BEL;

namespace tranhuutho_2121110087.GUI
{
    public partial class HoaDonGUI : Form
    {
        HoaDonBAL hdBAL = new HoaDonBAL();
        CTHoaDonBAL cthdBAL = new CTHoaDonBAL();
        KhachHangBAL khBAL = new KhachHangBAL();
        SanphamBAL hhBAL = new SanphamBAL();
        DBConnection dbConnection = new DBConnection();
        private Utils utils = new Utils();

        public HoaDonGUI()
        {
            InitializeComponent();

            dvgHD.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dvgHD.ReadOnly = true;
            txtTenKH.ReadOnly = true;
            txtDiaChi.ReadOnly = true;
            txtDT.ReadOnly = true;
            txtTenHang.ReadOnly = true;
            txtDG.ReadOnly = true;
            RefreshData();

            // Mã KH
            List<KhachHangBEL> khachhang = khBAL.ReadKhachHang();
            cbMaKH.DataSource = khachhang;
            cbMaKH.DropDownStyle = ComboBoxStyle.DropDownList;
            cbMaKH.DisplayMember = "MaKhachHang";
            cbMaKH.ValueMember = "MaKhachHang";
            cbMaKH.SelectedItem = null;



            // Mã Hàng
            List<SanphamBEL> hanghoa = hhBAL.ReadHangHoa();
            cbMH.DropDownStyle = ComboBoxStyle.DropDownList;
            cbMH.DataSource = hanghoa;
            cbMH.DisplayMember = "MaHang";
            cbMH.ValueMember = "MaHang";
            cbMH.SelectedItem = null;


        }

        private bool ValidateFields()
        {
            if (!int.TryParse(txtSoLuong.Text, out int quantity) || quantity <= 0 || quantity > 1000)
            {
                MessageBox.Show("Vui lòng nhập số hợp lệ, lớn hơn 0 và không vượt quá 1000.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = dpNgayBan.Value;

            string formattedDate = selectedDate.ToString("dd-MM-yyyy");

            if (!string.IsNullOrEmpty(txtMaHD.Text) && !string.IsNullOrEmpty(txtTenKH.Text) && !string.IsNullOrEmpty(txtTenHang.Text))
            {
                if (utils.ValidateInputType(txtMaHD, typeof(int), "Mã hoá đơn") && ValidateFields())
                { 
                    
                int newId = int.Parse(txtMaHD.Text);
                int newMaKH = (int)cbMaKH.SelectedValue;
                int newMaHang = (int)cbMH.SelectedValue;
                string newTenKH = txtTenKH.Text;
                string newTenHang = txtTenHang.Text;
                int Soluong = int.Parse(txtSoLuong.Text);
                float newDG = float.Parse(txtDG.Text);
                string NgayBan = formattedDate;
                float tongTien = Soluong * newDG;

                bool keyMaHD = hdBAL.CheckMaHD(newId);

                if (keyMaHD)
                {
                    MessageBox.Show("Mã hoá đơn đã tồn tại! Vui lòng nhập lại.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    HoaDonBEL newHoaDon = new HoaDonBEL
                    {
                        MaHD = newId,
                        MaKhachHang = newMaKH,
                        NgayBan = NgayBan,
                        TongTien = tongTien,
                    };

                    hdBAL.NewHoaDon(newHoaDon);

                    CTHoaDonBEL ctHoaDon = new CTHoaDonBEL
                    {
                        MaHD = newId,
                        MaHang = newMaHang,
                        DonGia = newDG,
                        Soluong = Soluong,
                    };

                    cthdBAL.NewCTHoaDon(ctHoaDon);

                    dvgHD.Rows.Add(newId, newMaHang, newTenHang, newMaKH, newTenKH, NgayBan, Soluong, newDG, tongTien);
                    ClearInputFields();
                    RefreshData();
                }
            }
                }
            else
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin", "Lỗi hiển thị", MessageBoxButtons.OK, MessageBoxIcon.Error);
              
            }
        }

        //Mã Khách hàng
        private void cbMaKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            if (cbMaKH.Text == "")
            {
                txtTenKH.Text = "";
                txtDiaChi.Text = "";
                txtDT.Text = "";
            }

            str = "Select TenKhachHang from KhachHang where MaKhachHang = N'" + cbMaKH.SelectedValue + "'";
            txtTenKH.Text = dbConnection.GetFieldValues(str);

            str = "Select DiaChi from KhachHang where MaKhachHang = N'" + cbMaKH.SelectedValue + "'";
            txtDiaChi.Text = dbConnection.GetFieldValues(str);

            str = "Select DienThoai from KhachHang where MaKhachHang= N'" + cbMaKH.SelectedValue + "'";
            txtDT.Text = dbConnection.GetFieldValues(str);
        }

        private void cbMH_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            if (cbMH.Text == "")
            {
                txtTenHang.Text = "";
                txtDG.Text = "";
            }

            str = "Select TenHang from Sanpham where MaHang = N'" + cbMH.SelectedValue + "'";
            txtTenHang.Text = dbConnection.GetFieldValues(str);

            str = "Select DonGiaBan from Sanpham where MaHang = N'" + cbMH.SelectedValue + "'";
            txtDG.Text = dbConnection.GetFieldValues(str);
        }

        private void RefreshData()
        {
            dvgHD.Rows.Clear();

            List<HoaDonBEL> lstCl = hdBAL.ReadHoaDon();

            foreach (HoaDonBEL cl in lstCl)
            {
                string tenKH = dbConnection.GetFieldValues("Select TenKhachHang from KhachHang where MaKhachHang = N'" + cl.MaKhachHang + "'");
                List<CTHoaDonBEL> lstCTHoaDon = cthdBAL.ReadCTHoaDon();

                foreach (CTHoaDonBEL cthd in lstCTHoaDon)
                {
                    if (cthd.MaHD == cl.MaHD)
                    {
                        string tenHang = dbConnection.GetFieldValues("Select TenHang from Sanpham where MaHang = N'" + cthd.MaHang + "'");

                        string formattedTongTien = cl.TongTien.ToString("N0");

                        dvgHD.Rows.Add(
                            cl.MaHD,
                            cthd.MaHang,
                            tenHang,
                            cl.MaKhachHang,
                            tenKH,
                            cl.NgayBan,
                            cthd.Soluong,
                            cthd.DonGia.ToString("N0"),
                            formattedTongTien
                        );
                    }
                }
            }
        }


        private void ClearInputFields()
        {
            txtMaHD.Text = "";
            cbMaKH.SelectedItem = null;
            cbMH.SelectedItem = null;
            txtTenKH.Text = "";
            txtDiaChi.Text = "";
            txtDT.Text = "";
            txtTenHang.Text = "";
            txtSoLuong.Text = "";
            txtDG.Text = "";
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            int selectedRowIndex = dvgHD.CurrentCell.RowIndex;

            if (selectedRowIndex >= 0 && selectedRowIndex < dvgHD.Rows.Count)
            {
                DataGridViewRow selectedRow = dvgHD.Rows[selectedRowIndex];

                if (dvgHD.SelectedRows.Count > 0 && selectedRow != null)
                {
                    if (selectedRow.Cells[0].Value != null)
                    {
                        int maHD = int.Parse(selectedRow.Cells[0].Value.ToString());

                        DialogResult result = MessageBox.Show("Bạn có chắc là muốn xoá dòng này", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            try
                            {
                                cthdBAL.DeleteCTHoaDon(maHD);
                                hdBAL.DeleteHoaDon(maHD);
                                dvgHD.Rows.Remove(selectedRow);
                                ClearInputFields();
                                RefreshData();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Lỗi khi xoá dòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Bạn hãy chọn một dòng để xoá.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Bạn hãy chọn một dòng để xoá.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }



        private void btnMenu_Click(object sender, EventArgs e)
        {
            MenuApp menu = new MenuApp();
            menu.Show();
            this.Hide();
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            dbConnection.ExitForm();
        }

        private void dvgHD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectedRowIndex = e.RowIndex;

            if (selectedRowIndex >= 0 && selectedRowIndex < dvgHD.Rows.Count)
            {
                DataGridViewRow selectedRow = dvgHD.Rows[selectedRowIndex];

                if (selectedRow.Cells[0].Value != null)
                {
                    txtMaHD.Text = selectedRow.Cells[0].Value.ToString();
                }
                else
                {
                    txtMaHD.Text = "";
                }

                object maKhachValue = selectedRow.Cells[3].Value;
                if (maKhachValue != null)
                {
                    int maKH = Convert.ToInt32(maKhachValue);
                    cbMaKH.SelectedValue = maKH;
                }
                else
                {
                    cbMaKH.SelectedItem = null;
                }

                object maHangHoa = selectedRow.Cells[1].Value;
                if (maHangHoa != null)
                {
                    int maHH = Convert.ToInt32(maHangHoa);
                    cbMH.SelectedValue = maHH;
                }
                else
                {
                    cbMH.SelectedItem = null;
                }

                if (selectedRow.Cells[6].Value != null)
                {
                    txtSoLuong.Text = selectedRow.Cells[6].Value.ToString();
                }
                else
                {
                    txtSoLuong.Text = "";
                }

                if (selectedRow.Cells[7].Value != null)
                {
                    txtDG.Text = selectedRow.Cells[7].Value.ToString();
                }
                else
                {
                    txtDG.Text = "";
                }
            }
        }


        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx" })
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        using (XLWorkbook workbook = new XLWorkbook())
                        {
                            DataTable dt = new DataTable();
                            dt.Columns.Add("MaHD");
                            dt.Columns.Add("MaHang");
                            dt.Columns.Add("TenHang");
                            dt.Columns.Add("MaKhachHang");
                            dt.Columns.Add("TenKhachHang");
                            dt.Columns.Add("NgayBan");
                            dt.Columns.Add("SoLuong");
                            dt.Columns.Add("DonGia");
                            dt.Columns.Add("TongTien");

                            foreach (DataGridViewRow dgvRow in dvgHD.Rows)
                            {
                                if (!dgvRow.IsNewRow)
                                {
                                    int maHD = Convert.ToInt32(dgvRow.Cells[0].Value);
                                    int maHang = Convert.ToInt32(dgvRow.Cells[1].Value);
                                    string tenHang = dgvRow.Cells[2].Value.ToString();
                                    int maKhachHang = Convert.ToInt32(dgvRow.Cells[3].Value);
                                    string tenKhachHang = dgvRow.Cells[4].Value.ToString();
                                    string ngayBan = dgvRow.Cells[5].Value.ToString();
                                    int soLuong = Convert.ToInt32(dgvRow.Cells[6].Value);
                                    float donGia = Convert.ToSingle(dgvRow.Cells[7].Value);
                                    float tongTien = Convert.ToSingle(dgvRow.Cells[8].Value);
                                    dt.Rows.Add(maHD, maHang, tenHang, maKhachHang, tenKhachHang, ngayBan, soLuong, donGia, tongTien);
                                }
                            }

                            workbook.Worksheets.Add(dt, "HoaDon");

                            workbook.SaveAs(sfd.FileName);
                            MessageBox.Show("Export thành công!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void txtTenHang_TextChanged(object sender, EventArgs e)
        {

        }

        private void HoaDonGUI_Load(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            dbConnection.ExitForm();
        }
    }
}
