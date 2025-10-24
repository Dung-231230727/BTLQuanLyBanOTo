using BTLQuanLyBanOTo.Classes;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BTLQuanLyBanOTo.HeThong
{
    public partial class frmDangNhap : Form
    {
        DataProcesser dt;
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void frmDangNhap_Load(object sender, System.EventArgs e)
        {
            if (frmMain.ktraDN == false)
            {

            }
            string connStr = "Server=Dung;Database=QuanLyCuaHangOto;Integrated Security=True";
            dt = new DataProcesser(connStr);
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
                frmMain.ktraDN = true;
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
                Application.Exit();
            }
        }
    }
}
