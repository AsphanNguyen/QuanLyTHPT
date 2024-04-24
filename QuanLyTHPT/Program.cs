using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyTHPT
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string taikhoan = "";
            string data = "";
            string data2 = "";
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new frmDangNhap());
            //Application.Run(new frmDoiMatKhau(taikhoan));
            Application.Run(new frmquanly(data,data2,taikhoan));
            //Application.Run(new frmHKTongHop());
        }
    }
}
