using BTLQuanLyBanOTo.HeThong;
using System;
using System.Windows.Forms;

namespace BTLQuanLyBanOTo
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            bool keepRunning = true; // Biến kiểm soát vòng lặp chính

            while (keepRunning)
            {
                frmDangNhap fLogin = new frmDangNhap();
                fLogin.ShowDialog(); // 1. Hiển thị form Đăng nhập

                // 2. Nếu đăng nhập thành công
                if (fLogin.LoginSuccessful)
                {
                    frmMain fMain = new frmMain();
                    fMain.ShowDialog(); // 3. Hiển thị form Chính

                    // 4. Sau khi form Chính đóng, kiểm tra xem có phải là đăng xuất không
                    if (fMain.IsLoggingOut)
                    {
                        keepRunning = true; // Nếu là đăng xuất, tiếp tục vòng lặp (để hiện lại fLogin)
                    }
                    else
                    {
                        keepRunning = false; // Nếu là Thoát (hoặc nhấn 'X'), dừng vòng lặp
                    }
                }
                else
                {
                    // 5. Nếu đăng nhập không thành công (nhấn 'Thoát' hoặc 'X' trên fLogin)
                    keepRunning = false; // Dừng vòng lặp
                }
            }
        }
    }
}
