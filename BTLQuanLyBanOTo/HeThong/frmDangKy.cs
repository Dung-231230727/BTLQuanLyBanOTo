using BTLQuanLyBanOTo.Classes;
using BTLQuanLyBanOTo.HeThong;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace BTLQuanLyBanOTo.NghiepVu
{
    public partial class frmDangKy : Form
    {
        public frmDangKy()
        {
            InitializeComponent();
        }

        DataProcesser dt;

        private void lblDangNhap_Click(object sender, EventArgs e)
        {
            frmMain parentForm = this.MdiParent as frmMain;

            if (parentForm != null)
            {
                parentForm.mnuHTDangNhap_Click(sender, e);
                this.Close();
            }
        }

        private void frmDangKy_Load(object sender, EventArgs e)
        {
            dt = new DataProcesser();

            LoadCBO();
            reset();
        }

        private void LoadCBO()
        {
            string sql = "select MaCV, TenCV from CongViec where MaCV = N'CV03'";
            cboChucVu.DataSource = dt.ExecuteQuery(sql);
            cboChucVu.DisplayMember = "TenCV";
            cboChucVu.ValueMember = "MaCV";
        }

        private void reset()
        {
            txtMa.Text = "";
            txtMa.Focus();
            txtTen.Text = "";
            dtpNgaySinh.Value = DateTime.Now;
            radNam.Checked = false;
            radNu.Checked = false;
            txtDienThoai.Text = "";
            txtDiaChi.Text = "";
            txtMK.Text = "";
            txtXNMK.Text = "";
        }

        private void btnDK_Click(object sender, EventArgs e)
        {
            string maNV = txtMa.Text.Trim();
            string tenNV = txtTen.Text.Trim();
            string mk = txtMK.Text;
            string xnMk = txtXNMK.Text;
            string sdt = txtDienThoai.Text.Trim();
            DateTime ngaySinh = dtpNgaySinh.Value;

            // Kiểm tra trống
            if (string.IsNullOrEmpty(maNV) || string.IsNullOrEmpty(tenNV) ||
                string.IsNullOrEmpty(mk) || string.IsNullOrEmpty(xnMk) ||
                string.IsNullOrEmpty(sdt) || (!radNam.Checked && !radNu.Checked))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Lỗi Đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMa.Focus();
                return;
            }

            // ktra tuổi
            DateTime tuoiToiThieu = DateTime.Now.Date.AddYears(-18);

            if (ngaySinh.Date > tuoiToiThieu)
            {
                MessageBox.Show("Bạn phải đủ 18 tuổi trở lên để đăng ký!", "Lỗi Đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra mật khẩu khớp
            if (mk != xnMk)
            {
                MessageBox.Show("Mật khẩu và Xác nhận Mật khẩu không khớp!", "Lỗi Đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMK.Focus();
                return;
            }

            // Kiểm tra sdt chỉ có số và đủ 10 chữ số
            if (sdt.Length != 10 || !sdt.All(char.IsDigit))
            {
                MessageBox.Show("Số điện thoại phải là 10 chữ số. Vui lòng kiểm tra lại!", "Lỗi Đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDienThoai.Focus();
                return;
            }

            //lọc trùng
            try
            {
                string sqlKT = "SELECT COUNT(*) FROM NhanVien WHERE MaNV = @ma";
                int count = (int)dt.ExecuteScalar(sqlKT, new SqlParameter[] { new SqlParameter("@ma", maNV) });

                if (count > 0)
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại. Vui lòng chọn tên khác!", "Lỗi Đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMa.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kiểm tra trùng lặp: " + ex.Message, "Lỗi DB", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //lưu vào db
            string gioiTinh = radNam.Checked ? "Nam" : "Nữ";

            string sql = @"INSERT INTO NhanVien (MaNV, TenNV, GioiTinh, NgaySinh, DienThoai, DiaChi, MaCV, MatKhau)
                         VALUES (@ma, @ten, @gt, @ns, @dt, @dc, @maCV, @mk)";

            SqlParameter[] prms = new SqlParameter[]
            {
                new SqlParameter("@ma", maNV),
                new SqlParameter("@ten", tenNV),
                new SqlParameter("@gt", gioiTinh),
                new SqlParameter("@ns", ngaySinh),
                new SqlParameter("@dt", sdt),
                new SqlParameter("@dc", txtDiaChi.Text.Trim()),
                new SqlParameter("@maCV", cboChucVu.SelectedValue),
                new SqlParameter("@mk", mk)
            };

            int r = dt.ExecuteNonQuery(sql, prms);
            if (r > 0)
            {
                MessageBox.Show("Đăng ký thành công! Bạn có thể đăng nhập ngay.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmDangNhap.TDN_DKy = txtMa.Text;
                frmDangNhap.MK_DKy = txtMK.Text;
                lblDangNhap_Click(sender, e);
                this.Close();
            }
            else
            {
                MessageBox.Show("Đăng ký thất bại. Vui lòng kiểm tra lại thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void lblTTMK_Click(object sender, EventArgs e)
        {
            if (txtMK.UseSystemPasswordChar == true)
            {
                txtMK.UseSystemPasswordChar = false;
                lblTTMK.Text = "( Ẩn )";
            }
            else
            {
                txtMK.UseSystemPasswordChar = true;
                lblTTMK.Text = "( Hiện )";
            }
        }

        private void lblTTXNMK_Click(object sender, EventArgs e)
        {
            if (txtXNMK.UseSystemPasswordChar == true)
            {
                txtXNMK.UseSystemPasswordChar = false;
                lblTTXNMK.Text = "( Ẩn )";
            }
            else
            {
                txtXNMK.UseSystemPasswordChar = true;
                lblTTXNMK.Text = "( Hiện )";
            }
        }
    }
}
