using System.Drawing;

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
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
            this.hướngDẫnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnutxtTB = new System.Windows.Forms.ToolStripTextBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.lvChucNang = new System.Windows.Forms.ListView();
            this.mnuHTDangNhap = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuQLy.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuQLy
            // 
            this.mnuQLy.AutoSize = false;
            this.mnuQLy.BackColor = System.Drawing.Color.White;
            this.mnuQLy.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.mnuQLy.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuQlyHeThong,
            this.mnuQLyDanhMuc,
            this.mnuQlyNghiepVu,
            this.mnuQlyBCTK,
            this.mnuQlyTroGiup,
            this.mnutxtTB});
            this.mnuQLy.Location = new System.Drawing.Point(0, 0);
            this.mnuQLy.Name = "mnuQLy";
            this.mnuQLy.Size = new System.Drawing.Size(1184, 30);
            this.mnuQLy.TabIndex = 0;
            this.mnuQLy.Text = "menuStrip1";
            // 
            // mnuQlyHeThong
            // 
            this.mnuQlyHeThong.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHTDangNhap,
            this.toolStripSeparator1,
            this.mnuHTDangXuat,
            this.mnuHTThoat});
            this.mnuQlyHeThong.Name = "mnuQlyHeThong";
            this.mnuQlyHeThong.Size = new System.Drawing.Size(74, 26);
            this.mnuQlyHeThong.Text = "Hệ thống";
            // 
            // mnuHTDangXuat
            // 
            this.mnuHTDangXuat.Name = "mnuHTDangXuat";
            this.mnuHTDangXuat.Size = new System.Drawing.Size(180, 22);
            this.mnuHTDangXuat.Text = "Đăng xuất";
            this.mnuHTDangXuat.Click += new System.EventHandler(this.mnuHTDangXuat_Click);
            // 
            // mnuHTThoat
            // 
            this.mnuHTThoat.Name = "mnuHTThoat";
            this.mnuHTThoat.Size = new System.Drawing.Size(180, 22);
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
            this.toolStripSeparator2,
            this.mnuDMDMC});
            this.mnuQLyDanhMuc.Name = "mnuQLyDanhMuc";
            this.mnuQLyDanhMuc.Size = new System.Drawing.Size(78, 26);
            this.mnuQLyDanhMuc.Text = "Danh mục";
            // 
            // mnuDMSP
            // 
            this.mnuDMSP.Name = "mnuDMSP";
            this.mnuDMSP.Size = new System.Drawing.Size(202, 22);
            this.mnuDMSP.Text = "Quản lý sản phẩm";
            this.mnuDMSP.Click += new System.EventHandler(this.mnuDMSP_Click);
            // 
            // mnuDMNV
            // 
            this.mnuDMNV.Name = "mnuDMNV";
            this.mnuDMNV.Size = new System.Drawing.Size(202, 22);
            this.mnuDMNV.Text = "Quản lý nhân viên";
            this.mnuDMNV.Click += new System.EventHandler(this.mnuDMNV_Click);
            // 
            // mnuDMKH
            // 
            this.mnuDMKH.Name = "mnuDMKH";
            this.mnuDMKH.Size = new System.Drawing.Size(202, 22);
            this.mnuDMKH.Text = "Quản lý khách hàng";
            this.mnuDMKH.Click += new System.EventHandler(this.mnuDMKH_Click);
            // 
            // mnuDMNCC
            // 
            this.mnuDMNCC.Name = "mnuDMNCC";
            this.mnuDMNCC.Size = new System.Drawing.Size(202, 22);
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
            this.mnuDMDMC.Size = new System.Drawing.Size(202, 22);
            this.mnuDMDMC.Text = "Danh mục chung";
            // 
            // mnuDMCCV
            // 
            this.mnuDMCCV.Name = "mnuDMCCV";
            this.mnuDMCCV.Size = new System.Drawing.Size(160, 22);
            this.mnuDMCCV.Text = "Công việc";
            this.mnuDMCCV.Click += new System.EventHandler(this.mnuDMCCV_Click);
            // 
            // mnuDMCDX
            // 
            this.mnuDMCDX.Name = "mnuDMCDX";
            this.mnuDMCDX.Size = new System.Drawing.Size(160, 22);
            this.mnuDMCDX.Text = "Đời xe";
            this.mnuDMCDX.Click += new System.EventHandler(this.mnuDMCDX_Click);
            // 
            // mnuDMCHSX
            // 
            this.mnuDMCHSX.Name = "mnuDMCHSX";
            this.mnuDMCHSX.Size = new System.Drawing.Size(160, 22);
            this.mnuDMCHSX.Text = "Hãng sản xuất";
            this.mnuDMCHSX.Click += new System.EventHandler(this.mnuDMCHSX_Click);
            // 
            // mnuDMCLX
            // 
            this.mnuDMCLX.Name = "mnuDMCLX";
            this.mnuDMCLX.Size = new System.Drawing.Size(160, 22);
            this.mnuDMCLX.Text = "Loại xe";
            this.mnuDMCLX.Click += new System.EventHandler(this.mnuDMCLX_Click);
            // 
            // mnuDMCMS
            // 
            this.mnuDMCMS.Name = "mnuDMCMS";
            this.mnuDMCMS.Size = new System.Drawing.Size(160, 22);
            this.mnuDMCMS.Text = "Màu sắc";
            this.mnuDMCMS.Click += new System.EventHandler(this.mnuDMCMS_Click);
            // 
            // mnuDMCNSX
            // 
            this.mnuDMCNSX.Name = "mnuDMCNSX";
            this.mnuDMCNSX.Size = new System.Drawing.Size(160, 22);
            this.mnuDMCNSX.Text = "Nước sản xuất";
            this.mnuDMCNSX.Click += new System.EventHandler(this.mnuDMCNSX_Click);
            // 
            // mnuDMCSCN
            // 
            this.mnuDMCSCN.Name = "mnuDMCSCN";
            this.mnuDMCSCN.Size = new System.Drawing.Size(160, 22);
            this.mnuDMCSCN.Text = "Số chỗ ngồi";
            this.mnuDMCSCN.Click += new System.EventHandler(this.mnuDMCSCN_Click);
            // 
            // mnuDMCTT
            // 
            this.mnuDMCTT.Name = "mnuDMCTT";
            this.mnuDMCTT.Size = new System.Drawing.Size(160, 22);
            this.mnuDMCTT.Text = "Tình trạng";
            this.mnuDMCTT.Click += new System.EventHandler(this.mnuDMCTT_Click);
            // 
            // mnuQlyNghiepVu
            // 
            this.mnuQlyNghiepVu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNVBH,
            this.mnuNVNH});
            this.mnuQlyNghiepVu.Name = "mnuQlyNghiepVu";
            this.mnuQlyNghiepVu.Size = new System.Drawing.Size(80, 26);
            this.mnuQlyNghiepVu.Text = "Nghiệp vụ";
            // 
            // mnuNVBH
            // 
            this.mnuNVBH.Name = "mnuNVBH";
            this.mnuNVBH.Size = new System.Drawing.Size(180, 22);
            this.mnuNVBH.Text = "Bán hàng";
            this.mnuNVBH.Click += new System.EventHandler(this.mnuNVBH_Click);
            // 
            // mnuNVNH
            // 
            this.mnuNVNH.Name = "mnuNVNH";
            this.mnuNVNH.Size = new System.Drawing.Size(180, 22);
            this.mnuNVNH.Text = "Nhập hàng";
            this.mnuNVNH.Click += new System.EventHandler(this.mnuNVNH_Click);
            // 
            // mnuQlyBCTK
            // 
            this.mnuQlyBCTK.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuBCTKDT,
            this.mnuBCTKTK});
            this.mnuQlyBCTK.Name = "mnuQlyBCTK";
            this.mnuQlyBCTK.Size = new System.Drawing.Size(139, 26);
            this.mnuQlyBCTK.Text = "Báo cáo và thống kê";
            // 
            // mnuBCTKDT
            // 
            this.mnuBCTKDT.Name = "mnuBCTKDT";
            this.mnuBCTKDT.Size = new System.Drawing.Size(186, 22);
            this.mnuBCTKDT.Text = "Báo cáo doanh thu";
            this.mnuBCTKDT.Click += new System.EventHandler(this.mnuBCTKDT_Click);
            // 
            // mnuBCTKTK
            // 
            this.mnuBCTKTK.Name = "mnuBCTKTK";
            this.mnuBCTKTK.Size = new System.Drawing.Size(186, 22);
            this.mnuBCTKTK.Text = "Báo cáo tồn kho";
            this.mnuBCTKTK.Click += new System.EventHandler(this.mnuBCTKTK_Click);
            // 
            // mnuQlyTroGiup
            // 
            this.mnuQlyTroGiup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTGTT,
            this.hướngDẫnToolStripMenuItem});
            this.mnuQlyTroGiup.Name = "mnuQlyTroGiup";
            this.mnuQlyTroGiup.Size = new System.Drawing.Size(69, 26);
            this.mnuQlyTroGiup.Text = "Trợ giúp";
            // 
            // mnuTGTT
            // 
            this.mnuTGTT.Name = "mnuTGTT";
            this.mnuTGTT.Size = new System.Drawing.Size(180, 22);
            this.mnuTGTT.Text = "Thông tin";
            this.mnuTGTT.Click += new System.EventHandler(this.mnuTGTT_Click);
            // 
            // hướngDẫnToolStripMenuItem
            // 
            this.hướngDẫnToolStripMenuItem.Name = "hướngDẫnToolStripMenuItem";
            this.hướngDẫnToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.hướngDẫnToolStripMenuItem.Text = "Hướng dẫn";
            // 
            // mnutxtTB
            // 
            this.mnutxtTB.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.mnutxtTB.AutoSize = false;
            this.mnutxtTB.BackColor = System.Drawing.Color.White;
            this.mnutxtTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.mnutxtTB.Enabled = false;
            this.mnutxtTB.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.mnutxtTB.ForeColor = System.Drawing.Color.Black;
            this.mnutxtTB.HideSelection = false;
            this.mnutxtTB.Margin = new System.Windows.Forms.Padding(1, 0, 18, 0);
            this.mnutxtTB.Name = "mnutxtTB";
            this.mnutxtTB.ReadOnly = true;
            this.mnutxtTB.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.mnutxtTB.Size = new System.Drawing.Size(400, 28);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "sanpham_100.png");
            this.imageList1.Images.SetKeyName(1, "nhanvien-100.png");
            this.imageList1.Images.SetKeyName(2, "nhacungcap-100.png");
            this.imageList1.Images.SetKeyName(3, "khachhang-80.png");
            this.imageList1.Images.SetKeyName(4, "hoadonnhap-64.png");
            this.imageList1.Images.SetKeyName(5, "hoadonban-94.png");
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.lvChucNang);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(85, 731);
            this.panel1.TabIndex = 3;
            // 
            // lvChucNang
            // 
            this.lvChucNang.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this.lvChucNang.BackColor = System.Drawing.Color.White;
            this.lvChucNang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvChucNang.HideSelection = false;
            this.lvChucNang.LargeImageList = this.imageList1;
            this.lvChucNang.Location = new System.Drawing.Point(0, 0);
            this.lvChucNang.MultiSelect = false;
            this.lvChucNang.Name = "lvChucNang";
            this.lvChucNang.Size = new System.Drawing.Size(85, 731);
            this.lvChucNang.TabIndex = 1;
            this.lvChucNang.UseCompatibleStateImageBehavior = false;
            this.lvChucNang.DoubleClick += new System.EventHandler(this.lvChucNang_DoubleClick);
            // 
            // mnuHTDangNhap
            // 
            this.mnuHTDangNhap.Name = "mnuHTDangNhap";
            this.mnuHTDangNhap.Size = new System.Drawing.Size(196, 22);
            this.mnuHTDangNhap.Text = "Đăng nhập/ Đăng ký";
            this.mnuHTDangNhap.Click += new System.EventHandler(this.mnuHTDangNhap_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(199, 6);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::BTLQuanLyBanOTo.Properties.Resources.hinhnen1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1184, 761);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mnuQLy);
            this.DoubleBuffered = true;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mnuQLy;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý cửa hàng bán Ô tô";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.mnuQLy.ResumeLayout(false);
            this.mnuQLy.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

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
        private System.Windows.Forms.ToolStripMenuItem hướngDẫnToolStripMenuItem;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView lvChucNang;
        private System.Windows.Forms.ToolStripTextBox mnutxtTB;
        private System.Windows.Forms.ToolStripMenuItem mnuHTDangNhap;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}

