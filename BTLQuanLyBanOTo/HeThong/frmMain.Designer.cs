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
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.mnuQLy = new System.Windows.Forms.MenuStrip();
            this.mnuQlyHeThong = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHTDangNhap = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuHTDangXuat = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHTThoat = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQLyDanhMuc = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDMSP = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDMNV = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDMKH = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDMNCC = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
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
            this.lvChucNang = new System.Windows.Forms.ListView();
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mnuQLy.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            // mnuQLy
            // 
            this.mnuQLy.AutoSize = false;
            this.mnuQLy.BackColor = System.Drawing.Color.White;
            this.mnuQLy.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.mnuQLy.Size = new System.Drawing.Size(1384, 30);
            this.mnuQLy.TabIndex = 1;
            // 
            // mnuQlyHeThong
            // 
            this.mnuQlyHeThong.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHTDangNhap,
            this.toolStripSeparator1,
            this.mnuHTDangXuat,
            this.mnuHTThoat});
            this.mnuQlyHeThong.Image = global::BTLQuanLyBanOTo.Properties.Resources.setting_50;
            this.mnuQlyHeThong.Name = "mnuQlyHeThong";
            this.mnuQlyHeThong.Size = new System.Drawing.Size(90, 26);
            this.mnuQlyHeThong.Text = "Hệ thống";
            // 
            // mnuHTDangNhap
            // 
            this.mnuHTDangNhap.Name = "mnuHTDangNhap";
            this.mnuHTDangNhap.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.mnuHTDangNhap.Size = new System.Drawing.Size(239, 22);
            this.mnuHTDangNhap.Text = "Đăng nhập/ Đăng ký";
            this.mnuHTDangNhap.Click += new System.EventHandler(this.mnuHTDangNhap_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(236, 6);
            // 
            // mnuHTDangXuat
            // 
            this.mnuHTDangXuat.Name = "mnuHTDangXuat";
            this.mnuHTDangXuat.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.mnuHTDangXuat.Size = new System.Drawing.Size(239, 22);
            this.mnuHTDangXuat.Text = "Đăng xuất";
            this.mnuHTDangXuat.Click += new System.EventHandler(this.mnuHTDangXuat_Click);
            // 
            // mnuHTThoat
            // 
            this.mnuHTThoat.Name = "mnuHTThoat";
            this.mnuHTThoat.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.mnuHTThoat.Size = new System.Drawing.Size(239, 22);
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
            this.mnuQLyDanhMuc.Image = global::BTLQuanLyBanOTo.Properties.Resources.icons8_category_50;
            this.mnuQLyDanhMuc.Name = "mnuQLyDanhMuc";
            this.mnuQLyDanhMuc.Size = new System.Drawing.Size(94, 26);
            this.mnuQLyDanhMuc.Text = "Danh mục";
            // 
            // mnuDMSP
            // 
            this.mnuDMSP.Name = "mnuDMSP";
            this.mnuDMSP.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mnuDMSP.Size = new System.Drawing.Size(247, 22);
            this.mnuDMSP.Text = "Quản lý sản phẩm";
            this.mnuDMSP.Click += new System.EventHandler(this.mnuDMSP_Click);
            // 
            // mnuDMNV
            // 
            this.mnuDMNV.Name = "mnuDMNV";
            this.mnuDMNV.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.mnuDMNV.Size = new System.Drawing.Size(247, 22);
            this.mnuDMNV.Text = "Quản lý nhân viên";
            this.mnuDMNV.Click += new System.EventHandler(this.mnuDMNV_Click);
            // 
            // mnuDMKH
            // 
            this.mnuDMKH.Name = "mnuDMKH";
            this.mnuDMKH.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.mnuDMKH.Size = new System.Drawing.Size(247, 22);
            this.mnuDMKH.Text = "Quản lý khách hàng";
            this.mnuDMKH.Click += new System.EventHandler(this.mnuDMKH_Click);
            // 
            // mnuDMNCC
            // 
            this.mnuDMNCC.Name = "mnuDMNCC";
            this.mnuDMNCC.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.mnuDMNCC.Size = new System.Drawing.Size(247, 22);
            this.mnuDMNCC.Text = "Quản lý nhà cung cấp";
            this.mnuDMNCC.Click += new System.EventHandler(this.mnuDMNCC_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(244, 6);
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
            this.mnuDMDMC.Size = new System.Drawing.Size(247, 22);
            this.mnuDMDMC.Text = "Danh mục chung";
            // 
            // mnuDMCCV
            // 
            this.mnuDMCCV.Name = "mnuDMCCV";
            this.mnuDMCCV.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.C)));
            this.mnuDMCCV.Size = new System.Drawing.Size(241, 22);
            this.mnuDMCCV.Text = "Công việc";
            this.mnuDMCCV.Click += new System.EventHandler(this.mnuDMCCV_Click);
            // 
            // mnuDMCDX
            // 
            this.mnuDMCDX.Name = "mnuDMCDX";
            this.mnuDMCDX.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D)));
            this.mnuDMCDX.Size = new System.Drawing.Size(241, 22);
            this.mnuDMCDX.Text = "Đời xe";
            this.mnuDMCDX.Click += new System.EventHandler(this.mnuDMCDX_Click);
            // 
            // mnuDMCHSX
            // 
            this.mnuDMCHSX.Name = "mnuDMCHSX";
            this.mnuDMCHSX.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.H)));
            this.mnuDMCHSX.Size = new System.Drawing.Size(241, 22);
            this.mnuDMCHSX.Text = "Hãng sản xuất";
            this.mnuDMCHSX.Click += new System.EventHandler(this.mnuDMCHSX_Click);
            // 
            // mnuDMCLX
            // 
            this.mnuDMCLX.Name = "mnuDMCLX";
            this.mnuDMCLX.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.L)));
            this.mnuDMCLX.Size = new System.Drawing.Size(241, 22);
            this.mnuDMCLX.Text = "Loại xe";
            this.mnuDMCLX.Click += new System.EventHandler(this.mnuDMCLX_Click);
            // 
            // mnuDMCMS
            // 
            this.mnuDMCMS.Name = "mnuDMCMS";
            this.mnuDMCMS.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.M)));
            this.mnuDMCMS.Size = new System.Drawing.Size(241, 22);
            this.mnuDMCMS.Text = "Màu sắc";
            this.mnuDMCMS.Click += new System.EventHandler(this.mnuDMCMS_Click);
            // 
            // mnuDMCNSX
            // 
            this.mnuDMCNSX.Name = "mnuDMCNSX";
            this.mnuDMCNSX.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.N)));
            this.mnuDMCNSX.Size = new System.Drawing.Size(241, 22);
            this.mnuDMCNSX.Text = "Nước sản xuất";
            this.mnuDMCNSX.Click += new System.EventHandler(this.mnuDMCNSX_Click);
            // 
            // mnuDMCSCN
            // 
            this.mnuDMCSCN.Name = "mnuDMCSCN";
            this.mnuDMCSCN.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.mnuDMCSCN.Size = new System.Drawing.Size(241, 22);
            this.mnuDMCSCN.Text = "Số chỗ ngồi";
            this.mnuDMCSCN.Click += new System.EventHandler(this.mnuDMCSCN_Click);
            // 
            // mnuDMCTT
            // 
            this.mnuDMCTT.Name = "mnuDMCTT";
            this.mnuDMCTT.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.T)));
            this.mnuDMCTT.Size = new System.Drawing.Size(241, 22);
            this.mnuDMCTT.Text = "Tình trạng";
            this.mnuDMCTT.Click += new System.EventHandler(this.mnuDMCTT_Click);
            // 
            // mnuQlyNghiepVu
            // 
            this.mnuQlyNghiepVu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNVBH,
            this.mnuNVNH});
            this.mnuQlyNghiepVu.Image = global::BTLQuanLyBanOTo.Properties.Resources.icons8_portfolio_60;
            this.mnuQlyNghiepVu.Name = "mnuQlyNghiepVu";
            this.mnuQlyNghiepVu.Size = new System.Drawing.Size(96, 26);
            this.mnuQlyNghiepVu.Text = "Nghiệp vụ";
            // 
            // mnuNVBH
            // 
            this.mnuNVBH.Name = "mnuNVBH";
            this.mnuNVBH.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.B)));
            this.mnuNVBH.Size = new System.Drawing.Size(183, 22);
            this.mnuNVBH.Text = "Bán hàng";
            this.mnuNVBH.Click += new System.EventHandler(this.mnuNVBH_Click);
            // 
            // mnuNVNH
            // 
            this.mnuNVNH.Name = "mnuNVNH";
            this.mnuNVNH.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.N)));
            this.mnuNVNH.Size = new System.Drawing.Size(183, 22);
            this.mnuNVNH.Text = "Nhập hàng";
            this.mnuNVNH.Click += new System.EventHandler(this.mnuNVNH_Click);
            // 
            // mnuQlyBCTK
            // 
            this.mnuQlyBCTK.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuBCTKDT,
            this.mnuBCTKTK});
            this.mnuQlyBCTK.Image = global::BTLQuanLyBanOTo.Properties.Resources.icons8_report_50;
            this.mnuQlyBCTK.Name = "mnuQlyBCTK";
            this.mnuQlyBCTK.Size = new System.Drawing.Size(155, 26);
            this.mnuQlyBCTK.Text = "Báo cáo và thống kê";
            // 
            // mnuBCTKDT
            // 
            this.mnuBCTKDT.Name = "mnuBCTKDT";
            this.mnuBCTKDT.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.mnuBCTKDT.Size = new System.Drawing.Size(232, 22);
            this.mnuBCTKDT.Text = "Báo cáo doanh thu";
            this.mnuBCTKDT.Click += new System.EventHandler(this.mnuBCTKDT_Click);
            // 
            // mnuBCTKTK
            // 
            this.mnuBCTKTK.Name = "mnuBCTKTK";
            this.mnuBCTKTK.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.mnuBCTKTK.Size = new System.Drawing.Size(232, 22);
            this.mnuBCTKTK.Text = "Báo cáo tồn kho";
            this.mnuBCTKTK.Click += new System.EventHandler(this.mnuBCTKTK_Click);
            // 
            // mnuQlyTroGiup
            // 
            this.mnuQlyTroGiup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTGTT,
            this.hướngDẫnToolStripMenuItem});
            this.mnuQlyTroGiup.Image = global::BTLQuanLyBanOTo.Properties.Resources.icons8_help_50;
            this.mnuQlyTroGiup.Name = "mnuQlyTroGiup";
            this.mnuQlyTroGiup.Size = new System.Drawing.Size(85, 26);
            this.mnuQlyTroGiup.Text = "Trợ giúp";
            // 
            // mnuTGTT
            // 
            this.mnuTGTT.Name = "mnuTGTT";
            this.mnuTGTT.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.T)));
            this.mnuTGTT.Size = new System.Drawing.Size(217, 22);
            this.mnuTGTT.Text = "Thông tin";
            this.mnuTGTT.Click += new System.EventHandler(this.mnuTGTT_Click);
            // 
            // hướngDẫnToolStripMenuItem
            // 
            this.hướngDẫnToolStripMenuItem.Name = "hướngDẫnToolStripMenuItem";
            this.hướngDẫnToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.H)));
            this.hướngDẫnToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.hướngDẫnToolStripMenuItem.Text = "Hướng dẫn";
            this.hướngDẫnToolStripMenuItem.Click += new System.EventHandler(this.hướngDẫnToolStripMenuItem_Click);
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
            // lvChucNang
            // 
            this.lvChucNang.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this.lvChucNang.BackColor = System.Drawing.Color.White;
            this.lvChucNang.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvChucNang.Dock = System.Windows.Forms.DockStyle.Left;
            this.lvChucNang.HideSelection = false;
            this.lvChucNang.LargeImageList = this.imageList1;
            this.lvChucNang.Location = new System.Drawing.Point(0, 30);
            this.lvChucNang.MultiSelect = false;
            this.lvChucNang.Name = "lvChucNang";
            this.lvChucNang.Size = new System.Drawing.Size(85, 831);
            this.lvChucNang.SmallImageList = this.imageList1;
            this.lvChucNang.TabIndex = 9;
            this.lvChucNang.UseCompatibleStateImageBehavior = false;
            this.lvChucNang.DoubleClick += new System.EventHandler(this.lvChucNang_DoubleClick);
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.Size = new System.Drawing.Size(1184, 39);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.mnuQLy);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1384, 30);
            this.panel1.TabIndex = 12;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::BTLQuanLyBanOTo.Properties.Resources.hinhnen1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1384, 861);
            this.Controls.Add(this.lvChucNang);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
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
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.MenuStrip mnuQLy;
        private System.Windows.Forms.ToolStripMenuItem mnuQlyHeThong;
        private System.Windows.Forms.ToolStripMenuItem mnuHTDangNhap;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuHTDangXuat;
        private System.Windows.Forms.ToolStripMenuItem mnuHTThoat;
        private System.Windows.Forms.ToolStripMenuItem mnuQLyDanhMuc;
        private System.Windows.Forms.ToolStripMenuItem mnuDMSP;
        private System.Windows.Forms.ToolStripMenuItem mnuDMNV;
        private System.Windows.Forms.ToolStripMenuItem mnuDMKH;
        private System.Windows.Forms.ToolStripMenuItem mnuDMNCC;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnuDMDMC;
        private System.Windows.Forms.ToolStripMenuItem mnuDMCCV;
        private System.Windows.Forms.ToolStripMenuItem mnuDMCDX;
        private System.Windows.Forms.ToolStripMenuItem mnuDMCHSX;
        private System.Windows.Forms.ToolStripMenuItem mnuDMCLX;
        private System.Windows.Forms.ToolStripMenuItem mnuDMCMS;
        private System.Windows.Forms.ToolStripMenuItem mnuDMCNSX;
        private System.Windows.Forms.ToolStripMenuItem mnuDMCSCN;
        private System.Windows.Forms.ToolStripMenuItem mnuDMCTT;
        private System.Windows.Forms.ToolStripMenuItem mnuQlyNghiepVu;
        private System.Windows.Forms.ToolStripMenuItem mnuNVBH;
        private System.Windows.Forms.ToolStripMenuItem mnuNVNH;
        private System.Windows.Forms.ToolStripMenuItem mnuQlyBCTK;
        private System.Windows.Forms.ToolStripMenuItem mnuBCTKDT;
        private System.Windows.Forms.ToolStripMenuItem mnuBCTKTK;
        private System.Windows.Forms.ToolStripMenuItem mnuQlyTroGiup;
        private System.Windows.Forms.ToolStripMenuItem mnuTGTT;
        private System.Windows.Forms.ToolStripMenuItem hướngDẫnToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox mnutxtTB;
        private System.Windows.Forms.ListView lvChucNang;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.Panel panel1;
    }
}

