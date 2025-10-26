namespace BTLQuanLyBanOTo
{
    partial class frmMain
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
            this.mnuQLy = new System.Windows.Forms.MenuStrip();
            this.mnuQlyHeThong = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHTDangXuat = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHTThoat = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQLyDanhMuc = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDMSP = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDMNV = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDMKH = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDMNCC = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDMDMC = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDMCCV = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDMCDX = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDMCHSX = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDMCLX = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDMCMS = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDMCNSX = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDMCSCN = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDMCTT = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQlyNghiepVu = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNVBH = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNVNH = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQlyBCTK = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBCTKDT = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBCTKTK = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQlyTroGiup = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTGTT = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQLy.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuQLy
            // 
            this.mnuQLy.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuQlyHeThong,
            this.mnuQLyDanhMuc,
            this.mnuQlyNghiepVu,
            this.mnuQlyBCTK,
            this.mnuQlyTroGiup});
            this.mnuQLy.Location = new System.Drawing.Point(0, 0);
            this.mnuQLy.Name = "mnuQLy";
            this.mnuQLy.Size = new System.Drawing.Size(584, 24);
            this.mnuQLy.TabIndex = 0;
            this.mnuQLy.Text = "menuStrip1";
            // 
            // mnuQlyHeThong
            // 
            this.mnuQlyHeThong.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHTDangXuat,
            this.mnuHTThoat});
            this.mnuQlyHeThong.Name = "mnuQlyHeThong";
            this.mnuQlyHeThong.Size = new System.Drawing.Size(69, 20);
            this.mnuQlyHeThong.Text = "Hệ thống";
            // 
            // mnuHTDangXuat
            // 
            this.mnuHTDangXuat.Name = "mnuHTDangXuat";
            this.mnuHTDangXuat.Size = new System.Drawing.Size(127, 22);
            this.mnuHTDangXuat.Text = "Đăng xuất";
            this.mnuHTDangXuat.Click += new System.EventHandler(this.mnuHTDangXuat_Click);
            // 
            // mnuHTThoat
            // 
            this.mnuHTThoat.Name = "mnuHTThoat";
            this.mnuHTThoat.Size = new System.Drawing.Size(127, 22);
            this.mnuHTThoat.Text = "Thoát";
            this.mnuHTThoat.Click += new System.EventHandler(this.mnuHTThoat_Click);
            // 
            // mnuQLyDanhMuc
            // 
            this.mnuQLyDanhMuc.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDMSP,
            this.mnuDMNV,
            this.mnuDMKH,
            this.mnuDMNCC,
            this.mnuDMDMC});
            this.mnuQLyDanhMuc.Name = "mnuQLyDanhMuc";
            this.mnuQLyDanhMuc.Size = new System.Drawing.Size(74, 20);
            this.mnuQLyDanhMuc.Text = "Danh mục";
            // 
            // mnuDMSP
            // 
            this.mnuDMSP.Name = "mnuDMSP";
            this.mnuDMSP.Size = new System.Drawing.Size(190, 22);
            this.mnuDMSP.Text = "Quản lý sản phẩm";
            this.mnuDMSP.Click += new System.EventHandler(this.mnuDMSP_Click);
            // 
            // mnuDMNV
            // 
            this.mnuDMNV.Name = "mnuDMNV";
            this.mnuDMNV.Size = new System.Drawing.Size(190, 22);
            this.mnuDMNV.Text = "Quản lý nhân viên";
            this.mnuDMNV.Click += new System.EventHandler(this.mnuDMNV_Click);
            // 
            // mnuDMKH
            // 
            this.mnuDMKH.Name = "mnuDMKH";
            this.mnuDMKH.Size = new System.Drawing.Size(190, 22);
            this.mnuDMKH.Text = "Quản lý khách hàng";
            this.mnuDMKH.Click += new System.EventHandler(this.mnuDMKH_Click);
            // 
            // mnuDMNCC
            // 
            this.mnuDMNCC.Name = "mnuDMNCC";
            this.mnuDMNCC.Size = new System.Drawing.Size(190, 22);
            this.mnuDMNCC.Text = "Quản lý nhà cung cấp";
            this.mnuDMNCC.Click += new System.EventHandler(this.mnuDMNCC_Click);
            // 
            // mnuDMDMC
            // 
            this.mnuDMDMC.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDMCCV,
            this.mnuDMCDX,
            this.mnuDMCHSX,
            this.mnuDMCLX,
            this.mnuDMCMS,
            this.mnuDMCNSX,
            this.mnuDMCSCN,
            this.mnuDMCTT});
            this.mnuDMDMC.Name = "mnuDMDMC";
            this.mnuDMDMC.Size = new System.Drawing.Size(190, 22);
            this.mnuDMDMC.Text = "Danh mục chung";
            // 
            // mnuDMCCV
            // 
            this.mnuDMCCV.Name = "mnuDMCCV";
            this.mnuDMCCV.Size = new System.Drawing.Size(149, 22);
            this.mnuDMCCV.Text = "Công việc";
            this.mnuDMCCV.Click += new System.EventHandler(this.mnuDMCCV_Click);
            // 
            // mnuDMCDX
            // 
            this.mnuDMCDX.Name = "mnuDMCDX";
            this.mnuDMCDX.Size = new System.Drawing.Size(149, 22);
            this.mnuDMCDX.Text = "Đời xe";
            this.mnuDMCDX.Click += new System.EventHandler(this.mnuDMCDX_Click);
            // 
            // mnuDMCHSX
            // 
            this.mnuDMCHSX.Name = "mnuDMCHSX";
            this.mnuDMCHSX.Size = new System.Drawing.Size(149, 22);
            this.mnuDMCHSX.Text = "Hãng sản xuất";
            this.mnuDMCHSX.Click += new System.EventHandler(this.mnuDMCHSX_Click);
            // 
            // mnuDMCLX
            // 
            this.mnuDMCLX.Name = "mnuDMCLX";
            this.mnuDMCLX.Size = new System.Drawing.Size(149, 22);
            this.mnuDMCLX.Text = "Loại xe";
            this.mnuDMCLX.Click += new System.EventHandler(this.mnuDMCLX_Click);
            // 
            // mnuDMCMS
            // 
            this.mnuDMCMS.Name = "mnuDMCMS";
            this.mnuDMCMS.Size = new System.Drawing.Size(149, 22);
            this.mnuDMCMS.Text = "Màu sắc";
            this.mnuDMCMS.Click += new System.EventHandler(this.mnuDMCMS_Click);
            // 
            // mnuDMCNSX
            // 
            this.mnuDMCNSX.Name = "mnuDMCNSX";
            this.mnuDMCNSX.Size = new System.Drawing.Size(149, 22);
            this.mnuDMCNSX.Text = "Nước sản xuất";
            this.mnuDMCNSX.Click += new System.EventHandler(this.mnuDMCNSX_Click);
            // 
            // mnuDMCSCN
            // 
            this.mnuDMCSCN.Name = "mnuDMCSCN";
            this.mnuDMCSCN.Size = new System.Drawing.Size(149, 22);
            this.mnuDMCSCN.Text = "Số chỗ ngồi";
            this.mnuDMCSCN.Click += new System.EventHandler(this.mnuDMCSCN_Click);
            // 
            // mnuDMCTT
            // 
            this.mnuDMCTT.Name = "mnuDMCTT";
            this.mnuDMCTT.Size = new System.Drawing.Size(149, 22);
            this.mnuDMCTT.Text = "Tình trạng";
            this.mnuDMCTT.Click += new System.EventHandler(this.mnuDMCTT_Click);
            // 
            // mnuQlyNghiepVu
            // 
            this.mnuQlyNghiepVu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNVBH,
            this.mnuNVNH});
            this.mnuQlyNghiepVu.Name = "mnuQlyNghiepVu";
            this.mnuQlyNghiepVu.Size = new System.Drawing.Size(74, 20);
            this.mnuQlyNghiepVu.Text = "Nghiệp vụ";
            // 
            // mnuNVBH
            // 
            this.mnuNVBH.Name = "mnuNVBH";
            this.mnuNVBH.Size = new System.Drawing.Size(133, 22);
            this.mnuNVBH.Text = "Bán hàng";
            this.mnuNVBH.Click += new System.EventHandler(this.mnuNVBH_Click);
            // 
            // mnuNVNH
            // 
            this.mnuNVNH.Name = "mnuNVNH";
            this.mnuNVNH.Size = new System.Drawing.Size(133, 22);
            this.mnuNVNH.Text = "Nhập hàng";
            // 
            // mnuQlyBCTK
            // 
            this.mnuQlyBCTK.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuBCTKDT,
            this.mnuBCTKTK});
            this.mnuQlyBCTK.Name = "mnuQlyBCTK";
            this.mnuQlyBCTK.Size = new System.Drawing.Size(126, 20);
            this.mnuQlyBCTK.Text = "Báo cáo và thống kê";
            // 
            // mnuBCTKDT
            // 
            this.mnuBCTKDT.Name = "mnuBCTKDT";
            this.mnuBCTKDT.Size = new System.Drawing.Size(174, 22);
            this.mnuBCTKDT.Text = "Báo cáo doanh thu";
            // 
            // mnuBCTKTK
            // 
            this.mnuBCTKTK.Name = "mnuBCTKTK";
            this.mnuBCTKTK.Size = new System.Drawing.Size(174, 22);
            this.mnuBCTKTK.Text = "Báo cáo tồn kho";
            // 
            // mnuQlyTroGiup
            // 
            this.mnuQlyTroGiup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTGTT});
            this.mnuQlyTroGiup.Name = "mnuQlyTroGiup";
            this.mnuQlyTroGiup.Size = new System.Drawing.Size(63, 20);
            this.mnuQlyTroGiup.Text = "Trợ giúp";
            // 
            // mnuTGTT
            // 
            this.mnuTGTT.Name = "mnuTGTT";
            this.mnuTGTT.Size = new System.Drawing.Size(126, 22);
            this.mnuTGTT.Text = "Thông tin";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.mnuQLy);
            this.MainMenuStrip = this.mnuQLy;
            this.Name = "frmMain";
            this.Text = "Quản lý cửa hàng bán Ô tô";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.mnuQLy.ResumeLayout(false);
            this.mnuQLy.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuQLy;
        private System.Windows.Forms.ToolStripMenuItem mnuQlyHeThong;
        private System.Windows.Forms.ToolStripMenuItem mnuQLyDanhMuc;
        private System.Windows.Forms.ToolStripMenuItem mnuQlyNghiepVu;
        private System.Windows.Forms.ToolStripMenuItem mnuQlyBCTK;
        private System.Windows.Forms.ToolStripMenuItem mnuQlyTroGiup;
        private System.Windows.Forms.ToolStripMenuItem mnuHTDangXuat;
        private System.Windows.Forms.ToolStripMenuItem mnuHTThoat;
        private System.Windows.Forms.ToolStripMenuItem mnuDMSP;
        private System.Windows.Forms.ToolStripMenuItem mnuDMNV;
        private System.Windows.Forms.ToolStripMenuItem mnuDMKH;
        private System.Windows.Forms.ToolStripMenuItem mnuDMNCC;
        private System.Windows.Forms.ToolStripMenuItem mnuNVBH;
        private System.Windows.Forms.ToolStripMenuItem mnuDMDMC;
        private System.Windows.Forms.ToolStripMenuItem mnuDMCCV;
        private System.Windows.Forms.ToolStripMenuItem mnuDMCDX;
        private System.Windows.Forms.ToolStripMenuItem mnuDMCHSX;
        private System.Windows.Forms.ToolStripMenuItem mnuDMCLX;
        private System.Windows.Forms.ToolStripMenuItem mnuDMCMS;
        private System.Windows.Forms.ToolStripMenuItem mnuDMCNSX;
        private System.Windows.Forms.ToolStripMenuItem mnuDMCSCN;
        private System.Windows.Forms.ToolStripMenuItem mnuDMCTT;
        private System.Windows.Forms.ToolStripMenuItem mnuNVNH;
        private System.Windows.Forms.ToolStripMenuItem mnuBCTKDT;
        private System.Windows.Forms.ToolStripMenuItem mnuBCTKTK;
        private System.Windows.Forms.ToolStripMenuItem mnuTGTT;
    }
}

