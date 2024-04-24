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
    public partial class frmDangNhap : Form
    {
        string taikhoan;
        SqlCommand cmd;
        SqlConnection conn;
        string con,query;
        public frmDangNhap()
        {
            InitializeComponent();
        }
        //Nút hiện mật khẩu
        private void chk_hienmatkhau_Click(object sender, EventArgs e)
        {
            if (chk_hienmatkhau.Checked)
            {
                txt_matkhau.PasswordChar = '\0'; // \0 là ký tự rỗng
            }
            else
            {
                txt_matkhau.PasswordChar = '*';
            }
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmDangNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult no = MessageBox.Show("Bạn có muốn thoát!", "Thông báo", MessageBoxButtons.YesNo);
            if (no == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {

        }

        private void btn_dangnhap_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_taikhoan.Text) || string.IsNullOrEmpty(txt_matkhau.Text))
            {
                MessageBox.Show("Vui lòng nhập tài khoản và mật khẩu", "Thông báo");
            }
            else
            {
                con = @"Data Source=LAPTOP-GFK5C0EF\SQLEXPRESS;Initial Catalog=QlyHocSinh;Integrated Security=True";
                conn = new SqlConnection(con);
                query = "  select * " +
                        "from TaiKhoan " +
                        "where Username like '" + txt_taikhoan.Text+ 
                        "' and Password like '"+txt_matkhau.Text+"' and TrangThai = '1' ";
                try
                {
                    conn.Open();

                    cmd= new SqlCommand(query,conn);
                    SqlDataReader read = cmd.ExecuteReader();
                    if (read.Read())
                    {
                        taikhoan = txt_taikhoan.Text;
                            string MaLoaiTK = read["MaLoaiTaiKhoan"].ToString();
                            string MaTK = read["MaTaiKhoan"].ToString();
                            frmquanly quanly = new frmquanly(MaLoaiTK,MaTK,taikhoan);
                            quanly.Show();
                            this.Hide();                    }
                    else
                    {
                        MessageBox.Show("Thông tin không hợp lệ", "Thông báo");
                    }
                    conn.Close();
                }catch (Exception ex)
                {
                    MessageBox.Show("Lỗi " + ex.Message);
                }
            }
           
        }
    }
}
