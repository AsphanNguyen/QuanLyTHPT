using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace QuanLyTHPT
{
    public partial class frmDoiMatKhau : Form
    {
        SqlCommand cmd;
        SqlConnection conn;
        string query, con,taikhoan,Maloai,MaTK;

        private void btn_trove_Click(object sender, EventArgs e)
        {
            frmquanly quanly = new frmquanly(Maloai, MaTK, taikhoan);
            quanly.Show();
            this.Hide();
        }

        public frmDoiMatKhau(string tk)
        {
            taikhoan = tk;
            InitializeComponent();
        }

        private void btn_xacnhan_Click(object sender, EventArgs e)
        {
            string query2;
            con = @"Data Source=LAPTOP-GFK5C0EF\SQLEXPRESS;Initial Catalog=QlyHocSinh;Integrated Security=True";
            conn = new SqlConnection(con);
            query = "select * from TaiKhoan where Password like '" + txt_MatKhauCu.Text + "' and Username like '"+taikhoan+"'";
            try
            {
                conn.Open();
                cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read()==false)
                {
                    MessageBox.Show("Mật khẩu không đúng");
                }
                else
                {
                    if (!txt_Xacnhanmatkhau.Text.Equals(txt_MatKhauMoi.Text))
                    {
                        MessageBox.Show("Mật khẩu mới và cũ không khớp");
                    }
                    else
                    {
                        string data = "";
                        string data2 = "";
                        reader.Close();
                        query2 = "update TaiKhoan " +
                                    "set Password ='"+txt_Xacnhanmatkhau.Text+
                                    "' where Password like '"+txt_MatKhauCu.Text+"' " +
                                    "and Username like '"+taikhoan+"'";
                        cmd = new SqlCommand(query2, conn);
                        int a = cmd.ExecuteNonQuery();
                        if (a > 0)
                        {
                            MessageBox.Show("Đổi mật khẩu thành công!");
                            frmquanly quanly = new frmquanly(data, data2,taikhoan);
                            quanly.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Đổi mật khẩu thất bại!");
                        }
                    }
                }
                conn.Close();
            }catch (Exception ex)
            {
                MessageBox.Show("Lỗi" + ex.Message);
            }
        }
    }
}
