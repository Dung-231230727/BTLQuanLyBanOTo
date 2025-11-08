using BTLQuanLyBanOTo.Classes;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BTLQuanLyBanOTo.HeThong
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
            txtTDN.Text = TDN_DKy;
            txtMK.Text = MK_DKy;
        }

        DataProcesser dt;
        public static string TDN_DKy = "";
        public static string MK_DKy = "";

        private void frmDangNhap_Load(object sender, System.EventArgs e)
        {
            dt = new DataProcesser();
        }

        private bool kiemTra()
        {
            if (string.IsNullOrWhiteSpace(txtTDN.Text) ||
               string.IsNullOrWhiteSpace(txtMK.Text))
            {
                return false;
            }
            return true;
        }
        private void btnDN_Click(object sender, System.EventArgs e)
        {
            //kiemtratrong
            if (!kiemTra())
            {
                MessageBox.Show(
                   "Tên đăng nhập và mật khẩu không được để trống.",
                   "Cảnh báo",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Warning
                );
                frmMain.LoginSuccessful = false;
                return;
            }

            //loc
            string sql = @"SELECT nv.MaNV, nv.TenNV, nv.MaCV, cv.TenCV 
                            FROM NhanVien nv
                            Join CongViec cv on cv.MaCV = nv.MaCV
                            WHERE nv.MaNV=@ma AND nv.MatKhau=@mk and nv.TrangThai = 1";
            SqlParameter[] prms = new SqlParameter[]
            {
                new SqlParameter("@ma", txtTDN.Text),
                new SqlParameter("@mk", txtMK.Text)
            };

            var tbl = dt.ExecuteQuery(sql, prms);

            if (tbl.Rows.Count > 0)
            {
                DataRow row = tbl.Rows[0];
                frmMain.MaNV_DangNhap = row["MaNV"].ToString();
                frmMain.TenNV_DangNhap = row["TenNV"].ToString();
                frmMain.MaCV_DangNhap = row["MaCV"].ToString();
                frmMain.TenCV_DangNhap = row["TenCV"].ToString();

                frmMain.LoginSuccessful = true;

                MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                frmMain parentForm = this.MdiParent as frmMain;
                if (parentForm != null)
                {
                    parentForm.KiemSoatTrangThai(true);
                    parentForm.GanTen();
                }
                this.Close();
            }
            else
            {
                string sqlInactive = @"SELECT nv.MaNV 
                               FROM NhanVien nv
                               WHERE nv.MaNV=@ma AND nv.MatKhau=@mk AND nv.TrangThai = 0";

                var tblInactive = dt.ExecuteQuery(sqlInactive, new SqlParameter[]
                {
                    new SqlParameter("@ma", txtTDN.Text),
                    new SqlParameter("@mk", txtMK.Text)
                });

                if (tblInactive.Rows.Count > 0)
                {
                    // Tài khoản tồn tại nhưng bị vô hiệu hóa
                    MessageBox.Show(
                       "Tài khoản này đã bị vô hiệu hóa. Vui lòng liên hệ quản trị viên!",
                       "Lỗi Đăng nhập",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Error
                    );
                }
                else
                {
                    // Tài khoản không tồn tại hoặc sai mật khẩu
                    MessageBox.Show(
                       "Sai tên đăng nhập hoặc mật khẩu. Vui lòng kiểm tra lại!",
                       "Cảnh báo",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Warning
                    );
                }
                frmMain.LoginSuccessful = false;
                return;
            }
        }

        private void btnHuy_Click(object sender, System.EventArgs e)
        {
            txtTDN.Text = "";
            txtMK.Text = "";
            txtTDN.Focus();
        }

        private void lblDangKy_Click(object sender, System.EventArgs e)
        {
            frmMain parentForm = this.MdiParent as frmMain;

            if (parentForm != null)
            {
                parentForm.OpenDKy();
                this.Close();
            }
        }

        private void lblTTMK_Click(object sender, System.EventArgs e)
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
    }
}
