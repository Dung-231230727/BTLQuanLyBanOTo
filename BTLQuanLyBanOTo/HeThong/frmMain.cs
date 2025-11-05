using BTLQuanLyBanOTo.BaoCaoThongKe;
using BTLQuanLyBanOTo.DanhMuc;
using BTLQuanLyBanOTo.DanhMuc.DanhMucChung;
using BTLQuanLyBanOTo.HeThong;
using BTLQuanLyBanOTo.NghiepVu;
using BTLQuanLyBanOTo.TroGiup;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BTLQuanLyBanOTo
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

            IsLoggingOut = false;
            this.FormClosing += frmMain_FormClosing;
        }
        //biến
        public bool IsLoggingOut { get; private set; }
        public static string MaNV_DangNhap = "";
        public static string TenNV_DangNhap = "";
        public static string MaCV_DangNhap = "";
        public static string TenCV_DangNhap = "";
        public static bool LoginSuccessful;

        private void mnuHTThoat_Click(object sender, EventArgs e)
        {
            IsLoggingOut = false;
            this.Close();
        }

        private void CloseAllMdiChildren()
        {
            Form[] childForms = this.MdiChildren.ToArray();

            foreach (Form childForm in childForms)
            {
                childForm.Close();
            }
        }

        private void mnuHTDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show(
                "Bạn có chắc muốn đăng xuất?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
                );
            if (r == DialogResult.Yes)
            {
                CloseAllMdiChildren();
                IsLoggingOut = true;
                KiemSoatTrangThai(false);
                mnuHTDangNhap_Click(sender, e);
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!IsLoggingOut)
            {
                DialogResult r = MessageBox.Show(
                    "Bạn có chắc muốn thoát chương trình không?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                    );

                if (r == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void PopulateListView()
        {
            lvChucNang.Items.Clear();

            // Tạo các mục ListViewItem
            ListViewItem itemSanPham = new ListViewItem("Sản Phẩm", 0);
            itemSanPham.Tag = typeof(frmSanPham);
            lvChucNang.Items.Add(itemSanPham);

            ListViewItem itemNhanVien = new ListViewItem("Nhân Viên", 1);
            itemNhanVien.Tag = typeof(frmNhanVien);
            lvChucNang.Items.Add(itemNhanVien);

            ListViewItem itemNCC = new ListViewItem("Nhà Cung Cấp", 2);
            itemNCC.Tag = typeof(frmNhaCungCap);
            lvChucNang.Items.Add(itemNCC);

            ListViewItem itemKhachHang = new ListViewItem("Khách Hàng", 3);
            itemKhachHang.Tag = typeof(frmKhachHang);
            lvChucNang.Items.Add(itemKhachHang);

            ListViewItem itemHDN = new ListViewItem("Hóa Đơn Nhập", 4);
            itemHDN.Tag = typeof(frmNhapHang);
            lvChucNang.Items.Add(itemHDN);

            ListViewItem itemHDB = new ListViewItem("Hóa Đơn Bán", 5);
            itemHDB.Tag = typeof(frmBanHang);
            lvChucNang.Items.Add(itemHDB);
        }

        private void lvChucNang_DoubleClick(object sender, EventArgs e)
        {
            if (lvChucNang.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = lvChucNang.SelectedItems[0];

                if (selectedItem.ForeColor == Color.Gray)
                {
                    MessageBox.Show("Bạn không có quyền sử dụng chức năng này!", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Type formType = selectedItem.Tag as Type;
                if (formType != null)
                {
                    try
                    {
                        Form childForm = (Form)Activator.CreateInstance(formType);
                        OpenChildForm(childForm);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi mở form: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void OpenChildForm(Form childForm)
        {
            Type formType = childForm.GetType();

            foreach (Form openForm in this.MdiChildren)
            {
                if (openForm.GetType() == formType)
                {
                    openForm.Activate();
                    if (openForm.WindowState == FormWindowState.Minimized)
                    {
                        openForm.WindowState = FormWindowState.Normal;
                    }
                    return;
                }
            }

            childForm.MdiParent = this;

            foreach (ListViewItem item in lvChucNang.Items)
            {
                if (item.Tag as Type == formType)
                {
                    childForm.Tag = item;
                    break;
                }
            }

            childForm.Activated += ChildForm_Activated;
            childForm.FormClosed += ChildForm_Closed;
            childForm.Show();

            HighlightListViewItem(childForm);
        }

        private void HighlightListViewItem(Form form)
        {
            if (form.Tag is ListViewItem item)
            {
                foreach (ListViewItem lvItem in lvChucNang.Items)
                {
                    if (lvItem.ForeColor == Color.Gray)
                        continue;

                    lvItem.BackColor = SystemColors.Window;
                    lvItem.ForeColor = SystemColors.ControlText;
                    lvItem.Font = new Font(lvChucNang.Font, FontStyle.Regular);
                }

                if (item.ForeColor != Color.Gray)
                {
                    item.BackColor = Color.LightSkyBlue;
                    item.ForeColor = Color.White;
                    item.Font = new Font(lvChucNang.Font, FontStyle.Bold);
                    item.Selected = true;
                    item.EnsureVisible();
                }
            }
        }

        private void ChildForm_Activated(object sender, EventArgs e)
        {
            HighlightListViewItem(sender as Form);
        }

        private void ChildForm_Closed(object sender, FormClosedEventArgs e)
        {
            foreach (ListViewItem lvItem in lvChucNang.Items)
            {
                // Giữ nguyên màu xám cho item bị khóa
                if (lvItem.ForeColor == Color.Gray)
                    continue;

                lvItem.BackColor = SystemColors.Window;
                lvItem.ForeColor = SystemColors.ControlText;
                lvItem.Font = new Font(lvChucNang.Font, FontStyle.Regular);
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            PopulateListView();
            KiemSoatTrangThai(false);

            frmDangNhap f = new frmDangNhap();
            OpenChildForm(f);
        }

        public void KiemSoatTrangThai(bool isEnable)
        {
            mnuHTDangNhap.Enabled = !isEnable;
            mnuHTDangXuat.Enabled = isEnable;
            mnuQlyNghiepVu.Enabled = isEnable;
            mnuQLyDanhMuc.Enabled = isEnable;
            mnuQlyBCTK.Enabled = isEnable;

            lvChucNang.Enabled = isEnable;

            if (isEnable)
            {
                PhanQuyenChucNang();
            }
        }

        public void GanTen()
        {
            mnutxtTB.Text = "Xin chào " + TenCV_DangNhap + ": " + TenNV_DangNhap;
        }

        public void PhanQuyenChucNang()
        {
            string maCV = MaCV_DangNhap ?? "";
            //2
            mnuDMNV.Enabled = false;
            mnuDMKH.Enabled = false;
            mnuDMNCC.Enabled = false;
            mnuDMSP.Enabled = false;
            mnuDMDMC.Enabled = false;
            mnuNVBH.Enabled = false;
            mnuNVNH.Enabled = false;
            mnuBCTKDT.Enabled = false;
            mnuBCTKTK.Enabled = false;
            //3
            mnuDMCCV.Enabled = false;
            mnuDMCDX.Enabled = false;
            mnuDMCHSX.Enabled = false;
            mnuDMCLX.Enabled = false;
            mnuDMCMS.Enabled = false;
            mnuDMCNSX.Enabled = false;
            mnuDMCSCN.Enabled = false;
            mnuDMCTT.Enabled = false;

            foreach (ListViewItem item in lvChucNang.Items)
                item.ForeColor = Color.Gray;
            lvChucNang.Enabled = false;

            switch (maCV)
            {
                case "CV01": //giám đốc / admin
                    //2
                    mnuDMNV.Enabled = true;
                    mnuDMKH.Enabled = true;
                    mnuDMNCC.Enabled = true;
                    mnuDMSP.Enabled = true;
                    mnuDMDMC.Enabled = true;
                    mnuNVBH.Enabled = true;
                    mnuNVNH.Enabled = true;
                    mnuBCTKDT.Enabled = true;
                    mnuBCTKTK.Enabled = true;
                    //3
                    mnuDMCCV.Enabled = true;
                    mnuDMCDX.Enabled = true;
                    mnuDMCHSX.Enabled = true;
                    mnuDMCLX.Enabled = true;
                    mnuDMCMS.Enabled = true;
                    mnuDMCNSX.Enabled = true;
                    mnuDMCSCN.Enabled = true;
                    mnuDMCTT.Enabled = true;

                    lvChucNang.Enabled = true;
                    foreach (ListViewItem item in lvChucNang.Items)
                        item.ForeColor = Color.Black;
                    break;

                case "CV02": //kế toán
                    mnuBCTKDT.Enabled = true;
                    mnuBCTKTK.Enabled = true;
                    break;

                case "CV03": //nhân viên bán hàng
                    //2
                    mnuDMKH.Enabled = true;
                    mnuDMSP.Enabled = true;
                    mnuNVBH.Enabled = true;
                    mnuBCTKDT.Enabled = true;

                    lvChucNang.Enabled = true;
                    foreach (ListViewItem item in lvChucNang.Items)
                    {
                        if (item.Text == "Khách Hàng" ||
                            item.Text == "Sản Phẩm" ||
                            item.Text == "Hóa Đơn Bán")
                            item.ForeColor = Color.Black;
                        else
                            item.ForeColor = Color.Gray;
                    }
                    break;

                case "CV04": //thủ kho
                    //2
                    mnuDMNCC.Enabled = true;
                    mnuDMSP.Enabled = true;
                    mnuNVNH.Enabled = true;
                    mnuBCTKTK.Enabled = true;

                    lvChucNang.Enabled = true;
                    foreach (ListViewItem item in lvChucNang.Items)
                    {
                        if (item.Text == "Nhà Cung Cấp" ||
                            item.Text == "Sản Phẩm" ||
                            item.Text == "Hóa Đơn Nhập")
                            item.ForeColor = Color.Black;
                        else
                            item.ForeColor = Color.Gray;
                    }
                    break;

                case "CV05": //nv kỹ thuật
                    //2
                    mnuDMSP.Enabled = true;
                    mnuDMDMC.Enabled = true;
                    //3
                    mnuDMCDX.Enabled = true;
                    mnuDMCHSX.Enabled = true;
                    mnuDMCLX.Enabled = true;
                    mnuDMCMS.Enabled = true;
                    mnuDMCNSX.Enabled = true;
                    mnuDMCSCN.Enabled = true;
                    mnuDMCTT.Enabled = true;

                    lvChucNang.Enabled = true;
                    foreach (ListViewItem item in lvChucNang.Items)
                    {
                        if (item.Text == "Sản Phẩm")
                            item.ForeColor = Color.Black;
                        else
                            item.ForeColor = Color.Gray;
                    }
                    break;
            }
        }

        private void mnuDMSP_Click(object sender, EventArgs e)
        {
            frmSanPham f = new frmSanPham();
            OpenChildForm(f);
            HighlightListViewItem(f);
        }

        private void mnuDMNV_Click(object sender, EventArgs e)
        {
            frmNhanVien f = new frmNhanVien();
            OpenChildForm(f);
            HighlightListViewItem(f);
        }

        private void mnuDMKH_Click(object sender, EventArgs e)
        {
            frmKhachHang f = new frmKhachHang();
            OpenChildForm(f);
            HighlightListViewItem(f);
        }

        private void mnuDMNCC_Click(object sender, EventArgs e)
        {
            frmNhaCungCap f = new frmNhaCungCap();
            OpenChildForm(f);
            HighlightListViewItem(f);
        }

        private void mnuDMCCV_Click(object sender, EventArgs e)
        {
            frmCongViec f = new frmCongViec();
            OpenChildForm(f);
        }

        private void mnuDMCDX_Click(object sender, EventArgs e)
        {
            frmDoiXe f = new frmDoiXe();
            OpenChildForm(f);
        }

        private void mnuDMCHSX_Click(object sender, EventArgs e)
        {
            frmHangSanXuat f = new frmHangSanXuat();
            OpenChildForm(f);
        }

        private void mnuDMCLX_Click(object sender, EventArgs e)
        {
            frmLoaiXe f = new frmLoaiXe();
            OpenChildForm(f);
        }

        private void mnuDMCMS_Click(object sender, EventArgs e)
        {
            frmMauSac f = new frmMauSac();
            OpenChildForm(f);
        }

        private void mnuDMCNSX_Click(object sender, EventArgs e)
        {
            frmNuocSanXuat f = new frmNuocSanXuat();
            OpenChildForm(f);
        }

        private void mnuDMCSCN_Click(object sender, EventArgs e)
        {
            frmSoChoNgoi f = new frmSoChoNgoi();
            OpenChildForm(f);
        }

        private void mnuDMCTT_Click(object sender, EventArgs e)
        {
            frmTinhTrang f = new frmTinhTrang();
            OpenChildForm(f);
        }

        private void mnuNVBH_Click(object sender, EventArgs e)
        {
            frmBanHang f = new frmBanHang();
            OpenChildForm(f);
            HighlightListViewItem(f);
        }

        private void mnuNVNH_Click(object sender, EventArgs e)
        {
            frmNhapHang f = new frmNhapHang();
            OpenChildForm(f);
            HighlightListViewItem(f);
        }

        private void mnuBCTKTK_Click(object sender, EventArgs e)
        {
            frmBaoCaoTonKho f = new frmBaoCaoTonKho();
            OpenChildForm(f);
        }

        private void mnuBCTKDT_Click(object sender, EventArgs e)
        {
            frmBaoCaoDoanhThu f = new frmBaoCaoDoanhThu();
            OpenChildForm(f);
        }

        private void mnuTGTT_Click(object sender, EventArgs e)
        {
            frmThongTin f = new frmThongTin();
            OpenChildForm(f);
        }

        public void mnuHTDangNhap_Click(object sender, EventArgs e)
        {
            frmDangNhap f = new frmDangNhap();
            OpenChildForm(f);
        }

        public void OpenDKy()
        {
            frmDangKy f = new frmDangKy();
            OpenChildForm(f);
        }

        private void hướngDẫnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHuongDan f = new frmHuongDan();
            OpenChildForm(f);
        }
    }
}