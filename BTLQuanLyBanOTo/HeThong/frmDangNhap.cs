using BTLQuanLyBanOTo.Classes;
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
            string sql = "select TenNV from NhanVien where MaNV=@ma and MatKhau=@mk";
            SqlParameter[] prms = new SqlParameter[]
            {
                new SqlParameter("@ma", txtTDN.Text),
                new SqlParameter("@mk", txtMK.Text)
            };

            // Dùng ExecuteScalar để lấy TenNV
            object tenNV_Result = dt.ExecuteScalar(sql, prms);

            if (tenNV_Result != null)
            {
                MessageBox.Show(
                    "Đăng nhập thành công.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                frmMain.LoginSuccessful = true;
                frmMain.MaNV_DangNhap = txtTDN.Text;
                frmMain.TenNV_DangNhap = tenNV_Result.ToString();

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
                MessageBox.Show(
                   "Sai tên đăng nhập hoặc mật khẩu. Vui lòng nhập lại!",
                   "Cảnh báo",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Warning
                );
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
    }
}
