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
            IsLoggingOut = false;
            this.FormClosing += frmMain_FormClosing;
        }
        //biến
        public bool IsLoggingOut { get; private set; }

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
                    "Bạn có chắc muốn thoát khỏi chương trình không?",
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
    }
}
