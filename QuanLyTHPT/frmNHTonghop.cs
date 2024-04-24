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
    public partial class frmNHTonghop : Form
    {
        string maloaitk = "", matk = "", username = "",con,query,host = @"LAPTOP-GFK5C0EF\SQLEXPRESS";
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        SqlDataReader reader;
        DataSet ds;

        private void pic_diemdanh_timkiem_Click(object sender, EventArgs e)
        {
            int gioi=0, kha=0, trungbinh = 0, yeu = 0,dem=0;
            string sql = "  select cnth.MaHocSinh,hs.HoHS +' '+hs.TenHS as TenHocSinh,hl.TenHocLuc,hk.TenHanhKiem,kq.TenKetQua,DTBHocKi\r\n  " +
                "from KetQua_CaNam_TongHop cnth,HocSinh hs,HocLuc hl,HanhKiem hk,Lop l,KetQua kq\r\n  " +
                "where cnth.MaHocSinh = hs.MaHocSinh and cnth.MaHocLuc = hl.MaHocLuc and cnth.MaHanhKiem = hk.MaHanhKiem and cnth.MaLop = l.MaLop " +
                "and cnth.MaKetQua = kq.MaKetQua \r\n  and l.TenLop = '"+cbb_tenlop.Text+"'";
            loaddb(dataGridView1,sql);
            con = $"Data Source = {host};Initial Catalog = QlyHocSinh;Integrated Security = True";
            conn = new SqlConnection(con);
            string sql2 = " select kq.TenKetQua\r\n  " +
                "from KetQua_CaNam_TongHop cnth,HocSinh hs,HocLuc hl,HanhKiem hk,Lop l,KetQua kq\r\n  " +
                "where cnth.MaHocSinh = hs.MaHocSinh and cnth.MaHocLuc = hl.MaHocLuc and cnth.MaHanhKiem = hk.MaHanhKiem " +
                "and cnth.MaLop = l.MaLop and cnth.MaKetQua = kq.MaKetQua \r\n  and l.TenLop = '"+cbb_tenlop.Text+"'";
            try
            {
                conn.Open();

                cmd = new SqlCommand(sql2,conn);
                reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    if (reader["TenKetQua"].ToString() == "Giỏi")
                    {
                        gioi++;   
                    }
                    else if (reader["TenKetQua"].ToString() == "Khá")
                    {
                        kha++;    
                    }
                    else if (reader["TenKetQua"].ToString() == "Trung Bình")
                    {
                        trungbinh++;
                    }
                    else if (reader["TenKetQua"].ToString() == "Kém")
                    {
                        yeu++;
                    }
                    dem++;
                }

                conn.Close();

                txt_gioi.Text = gioi.ToString();
                txt_kha.Text = kha.ToString();
                txt_trungbinh.Text = trungbinh.ToString();
                txt_yeu.Text = yeu.ToString();
                txt_siso.Text = dem.ToString();
            }catch(Exception ex)
            {
                MessageBox.Show("Lỗi " + ex.Message);
            }
        }

        private void cbb_namhoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql2 = "  select l.MaLop,l.TenLop\r\n  " +
                "from NamHoc nh,Lop l\r\n  " +
                "where nh.MaNamHoc = l.MaNamHoc and TenNamHoc = '" + cbb_namhoc.Text + "'";
            loadcbb(cbb_tenlop, sql2, "TenLop", "MaLop");
        }

        private void frmNHTonghop_Load(object sender, EventArgs e)
        {
            string sql = "select MaNamHoc,TenNamHoc from NamHoc";
            loadcbb(cbb_namhoc, sql, "TenNamHoc", "MaNamHoc");
           
        }

        public void loadcbb(ComboBox cbb,string sql,string display,string value)
        {
            con = $"Data Source = {host};Initial Catalog = QlyHocSinh;Integrated Security = True";
            conn = new SqlConnection(con);
            query = sql;
            try
            {
                conn.Open();

                adapter = new SqlDataAdapter(query, conn);
                ds = new DataSet();
                adapter.Fill(ds);

                conn.Close();
                cbb.DataSource = ds.Tables[0];
                cbb.DisplayMember = display;
                cbb.ValueMember = value;
            }catch (Exception ex)
            {
                MessageBox.Show("Lỗi " + ex.Message);
            }
        }

        public void loaddb(DataGridView data,string sql)
        {
            con = $"Data Source = {host};Initial Catalog = QlyHocSinh;Integrated Security = True";
            conn = new SqlConnection(con);
            query = sql;
            try
            {
                conn.Open();

                adapter = new SqlDataAdapter(query, conn);
                ds = new DataSet();
                adapter.Fill(ds);

                conn.Close();

                data.DataSource = ds.Tables[0];
            }catch(Exception ex)
            {
                MessageBox.Show("lỗi" + ex.Message);
            }
        }

        public frmNHTonghop(string MALOAITK, string MATK, string Username)
        {
            maloaitk = MALOAITK;
            matk = MATK;
            username = Username;
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            frmquanly ql = new frmquanly(maloaitk, matk, username);
            ql.Show();
            this.Hide();
        }
    }
}
