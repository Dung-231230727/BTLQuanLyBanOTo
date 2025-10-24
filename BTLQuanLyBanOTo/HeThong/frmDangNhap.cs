using BTLQuanLyBanOTo.Classes;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BTLQuanLyBanOTo.HeThong
{
    public partial class frmDangNhap : Form
    {
        DataProcesser dt;
        public bool LoginSuccessful { get; private set; }
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

            //loc trung
            string sql = "select count(*) from NhanVien where MaNV=@ma and MatKhau=@mk";
            int count = (int)dt.ExecuteScalar(sql, new SqlParameter[]
            {
                new SqlParameter("@ma", txtTDN.Text),
                new SqlParameter("@mk", txtMK.Text)
            });
            if (count > 0)
            {
                MessageBox.Show(
                   "Đăng nhập thành công.",
                   "Thông báo",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Information
                );
                LoginSuccessful = true;
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
