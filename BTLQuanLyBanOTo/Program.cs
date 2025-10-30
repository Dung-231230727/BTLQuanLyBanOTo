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
            //Application.Run(new frmTest());

            bool keepRunning = true;

            while (keepRunning)
            {
                frmDangNhap fLogin = new frmDangNhap();
                fLogin.ShowDialog();

                if (fLogin.LoginSuccessful)
                {
                    frmMain fMain = new frmMain(fLogin.MaNhanVien, fLogin.TenNhanVien);

                    fMain.ShowDialog();

                    if (fMain.IsLoggingOut)
                    {
                        keepRunning = true;
                    }
                    else
                    {
                        keepRunning = false;
                    }
                }
                else
                {
                    keepRunning = false;
                }
            }
        }
    }
}

