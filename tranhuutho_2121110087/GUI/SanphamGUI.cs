using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using tranhuutho_2121110087.BAL;
using tranhuutho_2121110087.BEL;

namespace tranhuutho_2121110087.GUI
{
    public partial class SanphamGUI : Form
    {
        private SanphamBAL hhBAL = new SanphamBAL();
        private NhacungcapBAL clBAL = new NhacungcapBAL();
        private DBConnection dbConnection = new DBConnection();
        private Utils utils = new Utils();


        public SanphamGUI()
        {
            InitializeComponent();
            dgvHangHoa.ReadOnly = true;
            txtTenCL.ReadOnly = true;
            dgvHangHoa.CellClick += dgvHoangHoa_CellClick;
            RefreshData();


            List<NhacungcapBEL> chatLieu = clBAL.ReadChatLieu();

            dgvHangHoa.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            cbMaCL.DataSource = chatLieu;
            cbMaCL.DropDownStyle = ComboBoxStyle.DropDownList;
            cbMaCL.DisplayMember = "MaChatLieu";
            cbMaCL.ValueMember = "MaChatLieu";
            cbMaCL.SelectedItem = null;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ValidateInput(txtMaHang, "Mã hàng") && ValidateInput(txtTenHang, "Tên hàng") &&
                ValidateQuantity(txtSoLuong.Text) && ValidateInput(txtDGB, "Đơn giá bán") &&
                ValidateInput(textDGN, "Đơn giá nhập") && ValidateInput(cbMaCL, "Mã chất liệu") &&
                ValidateInput(pictureBox1, "Hình ảnh"))
            {
                if (utils.ValidateInputType(txtMaHang, typeof(int), "Mã hàng") && utils.ValidateInputType(txtSoLuong, typeof(int), "Số lượng") &&
                    utils.ValidateInputType(txtDGB, typeof(float), "Đơn giá bán") && utils.ValidateInputType(textDGN, typeof(float), "Đơn giá nhập"))
                {
                    int newId = int.Parse(txtMaHang.Text);
                    string newName = txtTenHang.Text;
                    int newIdCL = (int)cbMaCL.SelectedValue;
                    int Soluong = int.Parse(txtSoLuong.Text);
                    float newDGB = float.Parse(textDGN.Text);
                    float newDGN = float.Parse(txtDGB.Text);
                    string tenAnh = pictureBox1.Text.ToString();

                    bool keyMaHang = hhBAL.CheckMaHang(newId);

                    if (keyMaHang)
                    {
                        MessageBox.Show("Mã hàng đã tồn tại! Vui lòng nhập lại.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        SanphamBEL newHangHoa = new SanphamBEL
                        {
                            MaHang = newId,
                            TenHang = newName,
                            MaChatLieu = newIdCL,
                            SoLuong = Soluong,
                            DonGiaBan = newDGB,
                            DonGiaNhap = newDGN,
                            Anh = tenAnh,
                        };

                        hhBAL.NewHangHoa(newHangHoa);

                        dgvHangHoa.Rows.Add(newId, newName, newIdCL, Soluong, newDGN, newDGB, tenAnh);
                        ClearInputFields();
                        RefreshData();
                    }
                }
            }
        }

        private void dgvHoangHoa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectedRowIndex = e.RowIndex;
            int selectedColumnIndex = e.ColumnIndex;

            if (selectedRowIndex >= 0 && selectedRowIndex < dgvHangHoa.Rows.Count)
            {
                DataGridViewRow selectedRow = dgvHangHoa.Rows[selectedRowIndex];

                if (selectedRow != null)
                {
                    if (selectedRowIndex == dgvHangHoa.Rows.Count - 1)
                    {
                        ClearInputFields();
                    }
                    else
                    {
                        object cellValue = selectedRow.Cells[selectedColumnIndex].Value;
                        if (cellValue != null)
                        {
                            txtMaHang.Text = selectedRow.Cells[0].Value.ToString();
                            txtTenHang.Text = selectedRow.Cells[1].Value.ToString();
                            txtSoLuong.Text = selectedRow.Cells[3].Value.ToString();
                            textDGN.Text = FormatNumberWithCommas(float.Parse(selectedRow.Cells[4].Value.ToString()));
                            txtDGB.Text = FormatNumberWithCommas(float.Parse(selectedRow.Cells[5].Value.ToString()));
                            pictureBox1.Text = selectedRow.Cells[6].Value.ToString();

                            object maChatLieuValue = selectedRow.Cells[2].Value;
                            if (maChatLieuValue != null)
                            {
                                int maChatLieu = Convert.ToInt32(maChatLieuValue);
                                cbMaCL.SelectedValue = maChatLieu;
                            }

                            DisplayPreviewImage(selectedRow.Cells[6].Value.ToString());
                        }
                    }
                }
            }
        }

        private void ClearInputFields()
        {
            txtMaHang.Text = "";
            txtTenHang.Text = "";
            cbMaCL.SelectedItem = null;
            txtTenCL.Text = "";
            txtSoLuong.Text = "";
            textDGN.Text = "";
            txtDGB.Text = "";
            pictureBox1.Image = null;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            int selectedRowIndex = dgvHangHoa.CurrentCell.RowIndex;

            if (selectedRowIndex >= 0 && selectedRowIndex < dgvHangHoa.Rows.Count)
            {
                DataGridViewRow selectedRow = dgvHangHoa.Rows[selectedRowIndex];

                if (selectedRow != null)
                {
                    if (selectedRow.Cells[0].Value != null)
                    {
                        int selectedId = int.Parse(selectedRow.Cells[0].Value.ToString());

                        string tenHang = selectedRow.Cells[1].Value.ToString();
                        int maChatLieu = int.Parse(selectedRow.Cells[2].Value.ToString());
                        int soLuong = int.Parse(selectedRow.Cells[3].Value.ToString());
                        float donGiaNhap = float.Parse(selectedRow.Cells[4].Value.ToString());
                        float donGiaBan = float.Parse(selectedRow.Cells[5].Value.ToString());
                        string anh = selectedRow.Cells[6].Value.ToString();

                        SanphamBEL selectedHangHoa = new SanphamBEL
                        {
                            MaHang = selectedId,
                            TenHang = tenHang,
                            MaChatLieu = maChatLieu,
                            SoLuong = soLuong,
                            DonGiaNhap = donGiaNhap,
                            DonGiaBan = donGiaBan,
                            Anh = anh
                        };

                        try
                        {
                            hhBAL.DeleteHangHoa(selectedHangHoa);

                            dgvHangHoa.Rows.RemoveAt(selectedRowIndex);

                            RefreshData();
                            ClearInputFields();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Không thể xoá! Tồn tại mặt hàng này ở bảng khác", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnUploadImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.bmp)|*.jpg; *.jpeg; *.png; *.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedImagePath = openFileDialog.FileName;
                Image image = Image.FromFile(selectedImagePath);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Image = Image.FromFile(openFileDialog.FileName);

                pictureBox1.Text = openFileDialog.SafeFileName;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int selectedRowIndex = dgvHangHoa.CurrentCell.RowIndex;
            DataGridViewRow selectedRow = dgvHangHoa.Rows[selectedRowIndex];

            if (selectedRow != null)
            {
                bool hasEmptyCells = CheckForRowEmptyCells(selectedRow);

                if (hasEmptyCells)
                {
                    MessageBox.Show("Vui lòng chọn một dòng để sửa", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (ValidateInput(txtMaHang, "Mã hàng") && ValidateInput(txtTenHang, "Tên hàng") &&
                    ValidateQuantity(txtSoLuong.Text) && ValidateInput(textDGN, "Đơn giá nhập") &&
                    ValidateInput(txtDGB, "Đơn giá bán") && ValidateInput(cbMaCL, "Mã chất liệu"))
                {
                    if (utils.ValidateInputType(txtMaHang, typeof(int), "Mã hàng") && utils.ValidateInputType(txtSoLuong, typeof(int), "Số lượng") &&
                        utils.ValidateInputType(textDGN, typeof(float), "Đơn giá nhập") && utils.ValidateInputType(txtDGB, typeof(float), "Đơn giá bán"))
                    {
                        int selectedId = int.Parse(selectedRow.Cells[0].Value.ToString());
                        int newId = int.Parse(txtMaHang.Text);
                        string newName = txtTenHang.Text;
                        int newIdCL = (int)cbMaCL.SelectedValue;
                        int Soluong = int.Parse(txtSoLuong.Text);
                        float newDGN = float.Parse(textDGN.Text);
                        float newDGB = float.Parse(txtDGB.Text);
                        string tenAnh = pictureBox1.Text.ToString();

                        if (!string.IsNullOrEmpty(pictureBox1.Text))
                        {
                            string newImageName = Path.GetFileName(pictureBox1.Text);
                            tenAnh = newImageName;
                        }

                        SanphamBEL updatedHangHoa = new SanphamBEL
                        {
                            MaHang = newId,
                            TenHang = newName,
                            MaChatLieu = newIdCL,
                            SoLuong = Soluong,
                            DonGiaNhap = newDGN,
                            DonGiaBan = newDGB,
                            Anh = tenAnh
                        };

                        hhBAL.EditHangHoa(updatedHangHoa);

                        foreach (DataGridViewCell cell in selectedRow.Cells)
                        {
                            int columnIndex = cell.ColumnIndex;
                            switch (columnIndex)
                            {
                                case 0: // Mã hàng
                                    cell.Value = newId;
                                    break;
                                case 1: // Tên hàng
                                    cell.Value = newName;
                                    break;
                                case 2: // Mã chất liệu
                                    cell.Value = newIdCL;
                                    break;
                                case 3: // Số lượng
                                    cell.Value = Soluong;
                                    break;
                                case 4: // Đơn giá nhập
                                    cell.Value = newDGN.ToString("N0");
                                    break;
                                case 5: // Đơn giá bán
                                    cell.Value = newDGB.ToString("N0");
                                    break;
                                case 6: // Ảnh
                                    cell.Value = tenAnh;
                                    break;
                            }
                        }

                        DisplayPreviewImage(tenAnh);
                        RefreshData();
                        ClearInputFields();
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dòng cần sửa", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CheckForRowEmptyCells(DataGridViewRow row)
        {
            foreach (DataGridViewCell cell in row.Cells)
            {
                if (cell.Value == null || string.IsNullOrWhiteSpace(cell.Value.ToString()))
                {
                    return true;
                }
            }
            return false;
        }

        private void DisplayPreviewImage(string imageName)
        {
            if (!string.IsNullOrEmpty(imageName))
            {
                string imagePath = Path.Combine(@"D:\", imageName);

                if (File.Exists(imagePath))
                {
                    try
                    {
                        Image image = Image.FromFile(imagePath);

                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                        pictureBox1.Image = image;
                    }
                    catch (Exception ex)
                    {
                        pictureBox1.Image = null;
                        MessageBox.Show("Lỗi tải ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    pictureBox1.Image = null;
                }
            }
            else
            {
                pictureBox1.Image = null;
            }
        }

        private void RefreshData()
        {
            List<SanphamBEL> lstCl = hhBAL.ReadHangHoa();
            dgvHangHoa.Rows.Clear();

            int pictureBoxColumnIndex = 6;

            foreach (SanphamBEL cl in lstCl)
            {
  
                dgvHangHoa.Rows.Add(
                   cl.MaHang,
                   cl.TenHang,
                   cl.MaChatLieu,
                   cl.SoLuong,
                    cl.DonGiaNhap.ToString("N0"),
                    cl.DonGiaBan.ToString("N0"),

                    cl.Anh);

                if (dgvHangHoa.Rows[dgvHangHoa.Rows.Count - 1].Cells[pictureBoxColumnIndex].Value != null)
                {
                    string imageName = dgvHangHoa.Rows[dgvHangHoa.Rows.Count - 1].Cells[pictureBoxColumnIndex].Value.ToString();
                    string imagePath = Path.Combine(@"D:\", imageName);

                    if (File.Exists(imagePath))
                    {
                        try
                        {
                            Image image = Image.FromFile(imagePath);

                            DataGridViewImageCell imageCell = dgvHangHoa.Rows[dgvHangHoa.Rows.Count - 1].Cells[pictureBoxColumnIndex] as DataGridViewImageCell;
                            imageCell.Value = image;
                        }
                        catch (Exception ex)
                        {
                            dgvHangHoa.Rows[dgvHangHoa.Rows.Count - 1].Cells[pictureBoxColumnIndex].Value = null;
                            MessageBox.Show("Lỗi khi tải hình ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        dgvHangHoa.Rows[dgvHangHoa.Rows.Count - 1].Cells[pictureBoxColumnIndex].Value = null;
                    }
                }
                else
                {
                    dgvHangHoa.Rows[dgvHangHoa.Rows.Count - 1].Cells[pictureBoxColumnIndex].Value = null;
                }
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

        private void button1_Click(object sender, EventArgs e)
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
                            dt.Columns.Add("MaHang");
                            dt.Columns.Add("TenHang");
                            dt.Columns.Add("MaChatLieu");
                            dt.Columns.Add("SoLuong");
                            dt.Columns.Add("DonGiaNhap");
                            dt.Columns.Add("DonGiaBan");
                            dt.Columns.Add("Anh");

                            foreach (DataGridViewRow dgvRow in dgvHangHoa.Rows)
                            {
                                if (!dgvRow.IsNewRow)
                                {
                                    int maHang = Convert.ToInt32(dgvRow.Cells[0].Value);
                                    string tenHang = dgvRow.Cells[1].Value.ToString();
                                    int maChatLieu = Convert.ToInt32(dgvRow.Cells[2].Value);
                                    int soLuong = Convert.ToInt32(dgvRow.Cells[3].Value);
                                    float donGiaNhap = Convert.ToSingle(dgvRow.Cells[4].Value);
                                    float donGiaBan = Convert.ToSingle(dgvRow.Cells[5].Value);
                                    string anh = dgvRow.Cells[6].Value.ToString();
                                    dt.Rows.Add(maHang, tenHang, maChatLieu, soLuong, donGiaNhap, donGiaBan, anh);
                                }
                            }

                            workbook.Worksheets.Add(dt, "HangHoa");

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

        private void cbMaCL_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            if (cbMaCL.Text == "")
            {
                txtTenCL.Text = "";
            }

            str = "Select TenChatLieu from ChatLieu where MaChatLieu = N'" + cbMaCL.SelectedValue + "'";
            txtTenCL.Text = dbConnection.GetFieldValues(str);

        }

        private string FormatNumberWithCommas(float number)
        {
            return number.ToString("N0");
        }

        private bool ValidateQuantity(string inputQuantity)
        {
            if (!int.TryParse(inputQuantity, out int quantity))
            {
                MessageBox.Show("Vui lòng nhập số lượng hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (quantity <= 0)
            {
                MessageBox.Show("Số lượng phải lớn hơn 0.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (quantity > 1000)
            {
                MessageBox.Show("Số lượng không được vượt quá 1000.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }



        private bool ValidateInput(Control control, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(control.Text))
            {
                MessageBox.Show($"Vui lòng điền {fieldName}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                control.Focus();
                return false;
            }

            if (control == textDGN || control == txtDGB)
            {
                if (!float.TryParse(control.Text, out float value))
                {
                    MessageBox.Show($"Vui lòng nhập số hợp lệ cho {fieldName}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    control.Focus();
                    return false;
                }

                if (value < 1000 || value > 10000000000)
                {
                    MessageBox.Show($"{fieldName} phải nằm trong khoảng từ 1,000 đến 1,000,000,000,", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    control.Focus();
                    return false;
                }

                control.Text = FormatNumberWithCommas(value);
            }

            return true;
        }

        private void textDGN_TextChanged(object sender, EventArgs e)
        {
            if (!decimal.TryParse(textDGN.Text, out decimal value))
            {
                if (string.IsNullOrEmpty(textDGN.Text))
                    return;

                MessageBox.Show("Vui lòng nhập số hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                int cursorPosition = textDGN.SelectionStart;
                textDGN.Text = textDGN.Text.Remove(cursorPosition - 1, 1);
                textDGN.SelectionStart = cursorPosition - 1;
                return;
            }

            textDGN.Text = value.ToString("N0");
            textDGN.Select(textDGN.Text.Length, 0);
        }

        private void txtDGB_TextChanged(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtDGB.Text, out decimal value))
            {
                if (string.IsNullOrEmpty(txtDGB.Text))
                    return;

                MessageBox.Show("Vui lòng nhập số hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                int cursorPosition = txtDGB.SelectionStart;
                txtDGB.Text = txtDGB.Text.Remove(cursorPosition - 1, 1);
                txtDGB.SelectionStart = cursorPosition - 1;
                return;
            }

            txtDGB.Text = value.ToString("N0");
            txtDGB.Select(txtDGB.Text.Length, 0);
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            dbConnection.ExitForm();
        }
    }
}
