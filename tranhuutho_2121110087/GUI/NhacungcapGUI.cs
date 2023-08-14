using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using tranhuutho_2121110087.BAL;
using tranhuutho_2121110087.BEL;
using tranhuutho_2121110087.GUI;

namespace tranhuutho_2121110087
{
    public partial class NhacungcapGUI : Form
    {
        NhacungcapBAL clBAL = new NhacungcapBAL();
        DBConnection dbConnection = new DBConnection();
        Utils utils = new Utils();

        public NhacungcapGUI()
        {
            InitializeComponent();
            dgvChatLieu.ReadOnly = true;
            RefreshData();
            dgvChatLieu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void ResetInputFields()
        {
            txtMaCL.Text = string.Empty;
            txtTenCL.Text = string.Empty;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMaCL.Text) && !string.IsNullOrEmpty(txtTenCL.Text))
            {
                if (utils.ValidateInputType(txtMaCL, typeof(int), "Mã chất liệu"))
                {
                    int newId = int.Parse(txtMaCL.Text);
                    string newName = txtTenCL.Text;

                    bool keyMaCL = clBAL.CheckMaChatLieu(newId);

                    if (keyMaCL)
                    {
                        MessageBox.Show("Mã chất liệu đã tồn tại! Vui lòng nhập lại.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        NhacungcapBEL newChatLieu = new NhacungcapBEL
                        {
                            MaChatLieu = newId,
                            TenChatLieu = newName
                        };

                        clBAL.NewChatLieu(newChatLieu);

                        dgvChatLieu.Rows.Add(newId, newName);

                        ResetInputFields();
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            int selectedRowIndex = dgvChatLieu.CurrentCell.RowIndex;

            if (selectedRowIndex >= 0 && selectedRowIndex < dgvChatLieu.Rows.Count)
            {
                DataGridViewRow selectedRow = dgvChatLieu.Rows[selectedRowIndex];

                if (!selectedRow.IsNewRow)
                {
                    int selectedId = int.Parse(selectedRow.Cells[0].Value.ToString());
                    try
                    {
                        NhacungcapBEL selectedChatLieu = new NhacungcapBEL
                        {
                            MaChatLieu = selectedId,
                            TenChatLieu = selectedRow.Cells[1].Value.ToString()
                        };

                        clBAL.DeleteChatLieu(selectedChatLieu);

                        dgvChatLieu.Rows.RemoveAt(selectedRowIndex);
                        ResetInputFields();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Không thể xoá! Tồn tại chất liệu này ở bảng khác", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int selectedRowIndex = dgvChatLieu.CurrentCell.RowIndex;
            DataGridViewRow selectedRow = dgvChatLieu.Rows[selectedRowIndex];

            if (selectedRow != null)
            {
                if (!selectedRow.IsNewRow)
                {
                    if (utils.ValidateInputType(txtMaCL, typeof(int), "Mã chất liệu"))
                    {
                        int selectedId = int.Parse(selectedRow.Cells[0].Value.ToString());
                        int newId = int.Parse(txtMaCL.Text);
                        string newName = txtTenCL.Text;

                        NhacungcapBEL updatedCustomer = new NhacungcapBEL
                        {
                            MaChatLieu = newId,
                            TenChatLieu = newName
                        };

                        clBAL.EditChatLieu(updatedCustomer);

                        selectedRow.Cells[0].Value = newId;
                        selectedRow.Cells[1].Value = newName;

                        ResetInputFields();
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn dòng cần sửa và điền thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvChatLieu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectedRowIndex = e.RowIndex;

            if (selectedRowIndex >= 0 && selectedRowIndex < dgvChatLieu.Rows.Count)
            {
                DataGridViewRow selectedRow = dgvChatLieu.Rows[selectedRowIndex];

                if (selectedRow != null)
                {
                        object cellValue = selectedRow.Cells[e.ColumnIndex].Value;
                        if (cellValue != null)
                        {
                            txtMaCL.Text = selectedRow.Cells[0].Value.ToString();
                            txtTenCL.Text = selectedRow.Cells[1].Value.ToString();
                        }
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
                        using (ClosedXML.Excel.XLWorkbook workbook = new ClosedXML.Excel.XLWorkbook())
                        {
                            DataTable dt = new DataTable();
                            dt.Columns.Add("MaChatLieu");
                            dt.Columns.Add("TenChatLieu");

                            foreach (DataGridViewRow dgvRow in dgvChatLieu.Rows)
                            {
                                if (!dgvRow.IsNewRow)
                                {
                                    int maChatLieu = Convert.ToInt32(dgvRow.Cells[0].Value);
                                    string tenChatLieu = dgvRow.Cells[1].Value.ToString();
                                    dt.Rows.Add(maChatLieu, tenChatLieu);
                                }
                            }

                            workbook.Worksheets.Add(dt, "ChatLieu");

                            workbook.SaveAs(sfd.FileName);
                            MessageBox.Show("Export successful!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshData()
        {
            List<NhacungcapBEL> lstCl = clBAL.ReadChatLieu();
            dgvChatLieu.Rows.Clear();
            foreach (NhacungcapBEL cl in lstCl)
            {
                dgvChatLieu.Rows.Add(cl.MaChatLieu, cl.TenChatLieu);
            }
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            MenuApp menu = new MenuApp();
            menu.Show();
            this.Hide();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            dbConnection.ExitForm();
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            dbConnection.ExitForm();
        }
    }
}