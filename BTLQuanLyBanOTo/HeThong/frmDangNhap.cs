using BTLQuanLyBanOTo.Classes;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BTLQuanLyBanOTo.HeThong
{
    public partial class frmDangNhap : Form
    {
        DataProcesser dt;
        public bool LoginSuccessful { get; private set; }
        public string MaNhanVien { get; private set; }
        public string TenNhanVien { get; private set; }
        public frmDangNhap()
        {
            InitializeComponent();
        }

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
                LoginSuccessful = false;
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

                LoginSuccessful = true;
                MaNhanVien = txtTDN.Text;
                TenNhanVien = tenNV_Result.ToString();
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
                LoginSuccessful = false;
                return;
            }
        }

        private void btnThoat_Click(object sender, System.EventArgs e)
        {
            DialogResult r = MessageBox.Show(
                "Bạn có chắc muốn thoát không?",
                "Thông báo",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
                );
            if (r == DialogResult.Yes)
            {
                LoginSuccessful = false;
                this.Close();
            }
        }
    }
}
