using BTLQuanLyBanOTo.Classes;
using BTLQuanLyBanOTo.HeThong;
using System;
using System.Windows.Forms;

namespace BTLQuanLyBanOTo
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            IsLogginOut = false;
        }
        //biến
        public static bool ktraDN = false;
        public bool IsLogginOut { get; private set; }
        private void frmMain_Load(object sender, EventArgs e)
        {
            //
            frmDangNhap frm = new frmDangNhap();
            frm.ShowDialog();
            if (!ktraDN)
            {
                Application.Exit();
            }

            //
        }

        private void mnuQlyHeThong_Click(object sender, EventArgs e)
        {

        }

        private void mnuHTThoat_Click(object sender, EventArgs e)
        {
            Function f = new Function();
            f.Thoat(this);
        }

        private void mnuHTDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show(
                "Bạn có chắc muốn đăng xuất?",
                "Thông báo",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
                );
            if (r == DialogResult.Yes)
            {
                IsLogginOut = true;
                ktraDN = false;
                frmMain_Load(sender, e);
            }
        }
    }
}
