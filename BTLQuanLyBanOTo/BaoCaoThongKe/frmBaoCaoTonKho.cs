using BTLQuanLyBanOTo.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTLQuanLyBanOTo.BaoCaoThongKe
{
    public partial class frmBaoCaoTonKho : Form
    {
        DataProcesser dt;

        public frmBaoCaoTonKho()
        {
            InitializeComponent();
        }

        private void frmBaoCaoTonKho_Load(object sender, EventArgs e)
        {
            dt = new DataProcesser();
            LoadDGV();
        }

        private void LoadDGV()
        {
            string sql = "SELECT MaHang, TenHang, SoLuong, DonGiaNhap, DonGiaBan FROM DanhMucHang ORDER BY SoLuong ASC"; // Sắp xếp theo số lượng tăng dần

            DataTable tblTonKho = dt.ExecuteQuery(sql);
            dgvTonKho.DataSource = tblTonKho;

            dgvTonKho.Columns["MaHang"].HeaderText = "Mã hàng";
            dgvTonKho.Columns["TenHang"].HeaderText = "Tên hàng";
            dgvTonKho.Columns["SoLuong"].HeaderText = "Số lượng tồn";
            dgvTonKho.Columns["DonGiaNhap"].HeaderText = "Đơn giá nhập";
            dgvTonKho.Columns["DonGiaBan"].HeaderText = "Đơn giá bán";

            dgvTonKho.Columns["DonGiaNhap"].DefaultCellStyle.Format = "N0";
            dgvTonKho.Columns["DonGiaBan"].DefaultCellStyle.Format = "N0";

            dgvTonKho.Columns["TenHang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadDGV();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show(
                "Bạn có chắc muốn thoát không?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
                );
            if (r == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
