using System.Windows.Forms;

namespace BTLQuanLyBanOTo.Classes
{
    internal class Function
    {
        public void Thoat(Form f)
        {
            DialogResult r = MessageBox.Show(
                "Bạn có chắc muốn thoát không?",
                "Thông báo",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
                );
            if (r == DialogResult.Yes)
            {
                f.Close();
            }
        }
    }
}
