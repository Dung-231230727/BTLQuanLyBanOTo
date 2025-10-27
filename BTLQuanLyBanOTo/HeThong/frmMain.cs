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

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void mnuDMSP_Click(object sender, EventArgs e)
        {
            frmSanPham f = new frmSanPham();
            f.ShowDialog();
        }

        private void mnuDMNV_Click(object sender, EventArgs e)
        {
            frmNhanVien f = new frmNhanVien();
            f.ShowDialog();
        }

        private void mnuDMKH_Click(object sender, EventArgs e)
        {
            frmKhachHang f = new frmKhachHang();
            f.ShowDialog();
        }

        private void mnuDMNCC_Click(object sender, EventArgs e)
        {
            frmNhaCungCap f = new frmNhaCungCap();
            f.ShowDialog();
        }

        private void mnuDMCCV_Click(object sender, EventArgs e)
        {
            frmCongViec f = new frmCongViec();
            f.ShowDialog();
        }

        private void mnuDMCDX_Click(object sender, EventArgs e)
        {
            frmDoiXe f = new frmDoiXe();
            f.ShowDialog();
        }

        private void mnuDMCHSX_Click(object sender, EventArgs e)
        {
            frmHangSanXuat f = new frmHangSanXuat();
            f.ShowDialog();
        }

        private void mnuDMCLX_Click(object sender, EventArgs e)
        {
            frmLoaiXe f = new frmLoaiXe();
            f.ShowDialog();
        }

        private void mnuDMCMS_Click(object sender, EventArgs e)
        {
            frmMauSac f = new frmMauSac();
            f.ShowDialog();
        }

        private void mnuDMCNSX_Click(object sender, EventArgs e)
        {
            frmNuocSanXuat f = new frmNuocSanXuat();
            f.ShowDialog();
        }

        private void mnuDMCSCN_Click(object sender, EventArgs e)
        {
            frmSoChoNgoi f = new frmSoChoNgoi();
            f.ShowDialog();
        }

        private void mnuDMCTT_Click(object sender, EventArgs e)
        {
            frmTinhTrang f = new frmTinhTrang();
            f.ShowDialog();
        }

        private void mnuNVBH_Click(object sender, EventArgs e)
        {
            frmBanHang f = new frmBanHang();
            f.ShowDialog();
        }

        private void mnuNVNH_Click(object sender, EventArgs e)
        {
            frmNhapHang f = new frmNhapHang();
            f.ShowDialog();
        }

        private void mnuBCTKTK_Click(object sender, EventArgs e)
        {
            frmBaoCaoTonKho f = new frmBaoCaoTonKho();
            f.ShowDialog();
        }

        private void mnuBCTKDT_Click(object sender, EventArgs e)
        {
            frmBaoCaoDoanhThu f = new frmBaoCaoDoanhThu();
            f.ShowDialog();
        }

        private void mnuTGTT_Click(object sender, EventArgs e)
        {
            frmThongTin f = new frmThongTin();
            f.ShowDialog();
        }
    }
}
