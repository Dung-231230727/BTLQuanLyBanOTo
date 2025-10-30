using BTLQuanLyBanOTo.BaoCaoThongKe;
using BTLQuanLyBanOTo.DanhMuc;
using BTLQuanLyBanOTo.DanhMuc.DanhMucChung;
using BTLQuanLyBanOTo.NghiepVu;
using BTLQuanLyBanOTo.TroGiup;
using System;
using System.Windows.Forms;

namespace BTLQuanLyBanOTo
{
    public partial class frmMain : Form
    {
        public frmMain(string maNV, string tenNV)
        {
            InitializeComponent();

            MaNV_DangNhap = maNV;
            TenNV_DangNhap = tenNV;

            IsLoggingOut = false;
            this.FormClosing += frmMain_FormClosing;
        }
        //biến
        public bool IsLoggingOut { get; private set; }
        public static string MaNV_DangNhap;
        public static string TenNV_DangNhap;

        private void mnuHTThoat_Click(object sender, EventArgs e)
        {
            IsLoggingOut = false;
            this.Close();
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
                IsLoggingOut = true;
                this.Close();
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
            foreach (Form form in this.MdiChildren)
            {
                if (form.GetType() == childForm.GetType())
                {
                    form.Activate();
                    return;
                }
            }
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            PopulateListView();
            toolStripTextBox1.Text = "Xin chào: " + TenNV_DangNhap;
        }

        private void mnuDMSP_Click(object sender, EventArgs e)
        {
            frmSanPham f = new frmSanPham();
            OpenChildForm(f);
        }

        private void mnuDMNV_Click(object sender, EventArgs e)
        {
            frmNhanVien f = new frmNhanVien();
            OpenChildForm(f);
        }

        private void mnuDMKH_Click(object sender, EventArgs e)
        {
            frmKhachHang f = new frmKhachHang();
            OpenChildForm(f);
        }

        private void mnuDMNCC_Click(object sender, EventArgs e)
        {
            frmNhaCungCap f = new frmNhaCungCap();
            OpenChildForm(f);
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
        }

        private void mnuNVNH_Click(object sender, EventArgs e)
        {
            frmNhapHang f = new frmNhapHang();
            OpenChildForm(f);
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
    }
}
