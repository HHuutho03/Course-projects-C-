namespace tranhuutho_2121110087.GUI
{
    partial class MenuApp
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.quanLyKhachHangToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hangHoaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quanLyChâtChiêuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quanLyHoaĐơnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tuyChonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.đăngXuâtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quanLyKhachHangToolStripMenuItem,
            this.hangHoaToolStripMenuItem,
            this.quanLyChâtChiêuToolStripMenuItem,
            this.quanLyHoaĐơnToolStripMenuItem,
            this.tuyChonToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1482, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // quanLyKhachHangToolStripMenuItem
            // 
            this.quanLyKhachHangToolStripMenuItem.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.quanLyKhachHangToolStripMenuItem.Name = "quanLyKhachHangToolStripMenuItem";
            this.quanLyKhachHangToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.quanLyKhachHangToolStripMenuItem.Text = "Quản lý khách hàng";
            this.quanLyKhachHangToolStripMenuItem.Click += new System.EventHandler(this.quanLyKhachHangToolStripMenuItem_Click);
            // 
            // hangHoaToolStripMenuItem
            // 
            this.hangHoaToolStripMenuItem.Name = "hangHoaToolStripMenuItem";
            this.hangHoaToolStripMenuItem.Size = new System.Drawing.Size(141, 24);
            this.hangHoaToolStripMenuItem.Text = "Quản lý sản phẩm";
            this.hangHoaToolStripMenuItem.Click += new System.EventHandler(this.hangHoaToolStripMenuItem_Click);
            // 
            // quanLyChâtChiêuToolStripMenuItem
            // 
            this.quanLyChâtChiêuToolStripMenuItem.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.quanLyChâtChiêuToolStripMenuItem.Name = "quanLyChâtChiêuToolStripMenuItem";
            this.quanLyChâtChiêuToolStripMenuItem.Size = new System.Drawing.Size(165, 24);
            this.quanLyChâtChiêuToolStripMenuItem.Text = "Quản lý nhà cung cấp";
            this.quanLyChâtChiêuToolStripMenuItem.Click += new System.EventHandler(this.quanLyChâtChiêuToolStripMenuItem_Click);
            // 
            // quanLyHoaĐơnToolStripMenuItem
            // 
            this.quanLyHoaĐơnToolStripMenuItem.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.quanLyHoaĐơnToolStripMenuItem.Name = "quanLyHoaĐơnToolStripMenuItem";
            this.quanLyHoaĐơnToolStripMenuItem.Size = new System.Drawing.Size(132, 24);
            this.quanLyHoaĐơnToolStripMenuItem.Text = "Quản lý hóa đơn";
            this.quanLyHoaĐơnToolStripMenuItem.Click += new System.EventHandler(this.quanLyHoaĐơnToolStripMenuItem_Click);
            // 
            // tuyChonToolStripMenuItem
            // 
            this.tuyChonToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.đăngXuâtToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.tuyChonToolStripMenuItem.Name = "tuyChonToolStripMenuItem";
            this.tuyChonToolStripMenuItem.Size = new System.Drawing.Size(82, 24);
            this.tuyChonToolStripMenuItem.Text = "Tùy chọn";
            // 
            // đăngXuâtToolStripMenuItem
            // 
            this.đăngXuâtToolStripMenuItem.Name = "đăngXuâtToolStripMenuItem";
            this.đăngXuâtToolStripMenuItem.Size = new System.Drawing.Size(198, 26);
            this.đăngXuâtToolStripMenuItem.Text = "đăng xuát";
            this.đăngXuâtToolStripMenuItem.Click += new System.EventHandler(this.đăngXuâtToolStripMenuItem_Click_1);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(198, 26);
            this.exitToolStripMenuItem.Text = "Thoát ứng dụng";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.thoatToolStripMenuItem_Click);
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // panel1
            // 
            this.panel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.panel1.Location = new System.Drawing.Point(0, 45);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1482, 914);
            this.panel1.TabIndex = 1;
            // 
            // MenuApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1482, 953);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Location = new System.Drawing.Point(30, 30);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MenuApp";
            this.Text = "Chào mừng đến với trang quản lý sản phầm cùng Hữu Thọ";
            this.Load += new System.EventHandler(this.MenuApp_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.ToolStripMenuItem quanLyKhachHangToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quanLyHoaĐơnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quanLyChâtChiêuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hangHoaToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem tuyChonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem đăngXuâtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}