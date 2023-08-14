using System;
using System.Collections.Generic;
using System.Windows.Forms;
using tranhuutho_2121110087.BAL;
using tranhuutho_2121110087.BEL;

namespace tranhuutho_2121110087.GUI
{
    public partial class KhachHangGUI : Form
    {
        KhachHangBAL clBAL = new KhachHangBAL();
        DBConnection dbConnection = new DBConnection();


        public KhachHangGUI()
        {
            InitializeComponent();
            dgvKhachHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            RefreshDataGridView();
        }

        private void ResetInputFields()
        {
            txtMaKH.Text = string.Empty;
            txtTenKH.Text = string.Empty;
            txtDiaChi.Text = string.Empty;
            txtDT.Text = string.Empty;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateNotEmptyInput(txtMaKH, "Mã khách hàng") ||
                !ValidateNotEmptyInput(txtTenKH, "Tên khách hàng") ||
                !ValidateNotEmptyInput(txtDiaChi, "Địa chỉ") ||
                !ValidatePhoneNumberInput(txtDT, "Điện thoại") ||
                !ValidateNumericInput(txtMaKH, "Mã khách hàng"))
            {
                return;
            }

            if (!string.IsNullOrEmpty(txtMaKH.Text) && !string.IsNullOrEmpty(txtTenKH.Text))
            {
                int newId = int.Parse(txtMaKH.Text);
                string newName = txtTenKH.Text;
                string newDiaChi = txtDiaChi.Text;
                string newDienThoai = txtDT.Text;

                bool keyMaKH = clBAL.CheckMaKhachHang(newId);

                if (keyMaKH)
                {
                    MessageBox.Show("Mã khách hàng đã tồn tại! Vui lòng nhập lại.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    KhachHangBEL newKhachHang = new KhachHangBEL
                    {
                        MaKhachHang = newId,
                        TenKhachHang = newName,
                        DiaChi =  newDiaChi,
                        DienThoai = newDienThoai,
                    };

                    clBAL.NewKhachHang(newKhachHang);

                    dgvKhachHang.Rows.Add(newId, newName, newDiaChi, newDienThoai);
                    RefreshDataGridView();
                    ResetInputFields();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            int selectedRowIndex = dgvKhachHang.CurrentCell.RowIndex;

            if (selectedRowIndex >= 0 && selectedRowIndex < dgvKhachHang.Rows.Count)
            {
                DataGridViewRow selectedRow = dgvKhachHang.Rows[selectedRowIndex];

                if (selectedRow != null)
                {
                    if (selectedRow.Cells[0].Value != null)
                    {
                        int selectedId = int.Parse(selectedRow.Cells[0].Value.ToString());

                        DialogResult result = MessageBox.Show("Bạn có chắc là muốn xoá dòng này", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            try
                            {
                                KhachHangBEL selectedKhachHang = new KhachHangBEL
                                {
                                    MaKhachHang = selectedId,
                                    TenKhachHang = selectedRow.Cells[1].Value.ToString(),
                                    DiaChi = selectedRow.Cells[2].Value.ToString(),
                                    DienThoai = selectedRow.Cells[3].Value.ToString(),
                                };

                                clBAL.DeleteKhachHang(selectedKhachHang);

                                dgvKhachHang.Rows.RemoveAt(selectedRowIndex);

                                RefreshDataGridView();
                                ResetInputFields();
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Khách hàng này tồn tại ở bảng khác. Không thể xoá! ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng chọn dòng cần xoá", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn dòng cần xoá", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void RefreshDataGridView()
        {
            List<KhachHangBEL> lstCl = clBAL.ReadKhachHang();
            dgvKhachHang.Rows.Clear();
            foreach (KhachHangBEL cl in lstCl)
            {
                dgvKhachHang.Rows.Add(cl.MaKhachHang, cl.TenKhachHang, cl.DiaChi, cl.DienThoai);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!ValidateNotEmptyInput(txtMaKH, "Mã khách hàng") ||
                !ValidateNotEmptyInput(txtTenKH, "Tên khách hàng") ||
                !ValidateNotEmptyInput(txtDiaChi, "Địa chỉ") ||
                !ValidatePhoneNumberInput(txtDT, "Điện thoại") ||
                !ValidateNumericInput(txtMaKH, "Mã khách hàng"))
            {
                return;
            }

            int selectedRowIndex = dgvKhachHang.CurrentCell.RowIndex;

            if (selectedRowIndex >= 0 && selectedRowIndex < dgvKhachHang.Rows.Count)
            {
                DataGridViewRow selectedRow = dgvKhachHang.Rows[selectedRowIndex];

                if (selectedRow != null)
                {
                    if (selectedRow.Cells[0].Value != null)
                    {
                        int selectedId = int.Parse(selectedRow.Cells[0].Value.ToString());
                        int newId = int.Parse(txtMaKH.Text);
                        string newName = txtTenKH.Text;
                        string newDiaChi = txtDiaChi.Text;
                        string newDienThoai = txtDT.Text;

                        KhachHangBEL updatedCustomer = new KhachHangBEL
                        {
                            MaKhachHang = newId,
                            TenKhachHang = newName,
                            DiaChi = newDiaChi,
                            DienThoai = newDienThoai
                        };

                        try
                        {
                            clBAL.EditKhachHang(updatedCustomer);

                            selectedRow.Cells[0].Value = newId;
                            selectedRow.Cells[1].Value = newName;
                            selectedRow.Cells[2].Value = newDiaChi;
                            selectedRow.Cells[3].Value = newDienThoai;

                            RefreshDataGridView();
                            ResetInputFields();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi khi sửa thông tin: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng chọn dòng cần sửa và điền thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn dòng cần sửa và điền thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private bool ValidateNumericInput(TextBox textBox, string fieldName)
        {
            if (!int.TryParse(textBox.Text, out _))
            {
                MessageBox.Show($"Vui lòng nhập số nguyên cho trường '{fieldName}'", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.Focus();
                return false;
            }
            return true;
        }

        private bool ValidateNotEmptyInput(TextBox textBox, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                MessageBox.Show($"Vui lòng điền thông tin cho trường '{fieldName}'", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.Focus();
                return false;
            }
            return true;
        }

        private bool ValidatePhoneNumberInput(TextBox textBox, string fieldName)
        {
            if (!int.TryParse(textBox.Text, out _) || textBox.Text.Length < 10 || textBox.Text.Length > 11)
            {
                MessageBox.Show($"Vui lòng nhập số điện thoại hợp lệ (tối thiểu 10 và tối đa 11 chữ số)", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.Focus();
                return false;
            }
            return true;
        }

        private void dgvKhachHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectedRowIndex = e.RowIndex;

            if (selectedRowIndex >= 0 && selectedRowIndex < dgvKhachHang.Rows.Count)
            {
                DataGridViewRow selectedRow = dgvKhachHang.Rows[selectedRowIndex];

                if (selectedRow != null)
                {
                    object cellValue = selectedRow.Cells[e.ColumnIndex].Value;
                    if (cellValue != null)
                    {
                        txtMaKH.Text = selectedRow.Cells[0].Value.ToString();
                        txtTenKH.Text = selectedRow.Cells[1].Value.ToString();
                        txtDiaChi.Text = selectedRow.Cells[2].Value.ToString();
                        txtDT.Text = selectedRow.Cells[3].Value.ToString();
                    }
                }
            }
        }
    }
}
