using BTLQuanLyBanOTo.Classes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BTLQuanLyBanOTo.BaoCaoThongKe
{
    public partial class frmBaoCaoDoanhThu : Form
    {
        DataProcesser dt;

        public frmBaoCaoDoanhThu()
        {
            InitializeComponent();
        }

        private void frmBaoCaoDoanhThu_Load(object sender, EventArgs e)
        {
            dt = new DataProcesser();

            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDenNgay.Value = DateTime.Now;

            dgvDoanhThu.DataSource = null;
            txtTDT.Text = "0";
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1).AddSeconds(-1);

            if (tuNgay > denNgay)
            {
                MessageBox.Show("Ngày bắt đầu không thể lớn hơn ngày kết thúc!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sql = @"
                SELECT 
                    dh.SoDDH AS N'Mã HĐ', 
                    dh.NgayDat AS N'Ngày Bán', 
                    kh.TenKhach AS N'Tên Khách Hàng', 
                    nv.TenNV AS N'Nhân Viên Bán',
                    SUM(ct.ThanhTien) AS N'Tổng Tiền Hàng', -- Tổng tiền hàng chưa thuế
                    dh.Thue AS N'Thuế (%)',
                    dh.DatCoc AS N'Đặt Cọc',
                    dh.TongTien AS N'Tổng Thanh Toán' -- Tổng tiền cuối cùng
                FROM DonDatHang dh
                JOIN KhachHang kh ON dh.MaKhach = kh.MaKhach
                JOIN NhanVien nv ON dh.MaNV = nv.MaNV
                JOIN ChiTietDonDatHang ct ON dh.SoDDH = ct.SoDDH
                WHERE dh.NgayDat >= @TuNgay AND dh.NgayDat <= @DenNgay
                GROUP BY dh.SoDDH, dh.NgayDat, kh.TenKhach, nv.TenNV, dh.Thue, dh.DatCoc, dh.TongTien
                ORDER BY dh.NgayDat ASC";

            SqlParameter[] prms = new SqlParameter[] {
                new SqlParameter("@TuNgay", tuNgay),
                new SqlParameter("@DenNgay", denNgay)
            };

            DataTable tblDoanhThu = dt.ExecuteQuery(sql, prms);
            dgvDoanhThu.DataSource = tblDoanhThu;

            decimal tongDoanhThu = 0;
            foreach (DataRow row in tblDoanhThu.Rows)
            {
                tongDoanhThu += Convert.ToDecimal(row["Tổng Thanh Toán"]);
            }
            lblTongDoanhThu.Text = tongDoanhThu.ToString("N0");

            dgvDoanhThu.Columns["Mã HĐ"].Width = 120;
            dgvDoanhThu.Columns["Ngày Bán"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvDoanhThu.Columns["Tên Khách Hàng"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDoanhThu.Columns["Tổng Tiền Hàng"].DefaultCellStyle.Format = "N0";
            dgvDoanhThu.Columns["Đặt Cọc"].DefaultCellStyle.Format = "N0";
            dgvDoanhThu.Columns["Tổng Thanh Toán"].DefaultCellStyle.Format = "N0";
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
