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
    public partial class frmHKTongHop : Form
    {
        string maloaitk = "", matk = "", username = "";
        SqlConnection conn;
        SqlDataAdapter adapter;
        DataSet ds;
        SqlDataReader reader;
        SqlCommand cmd;
        string con, query,host= @"LAPTOP-GFK5C0EF\SQLEXPRESS";

        private void cbb_HK_TH_HocKy_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql1 = "select TenLop,MaLop" +
                " from Lop l, NamHoc nh " +
                "where l.MaNamHoc = nh.MaNamHoc";
            loadcbb(cbb_HK_TH_TenLop, sql1, "TenLop", "MaLop");
        }

        public void loaddb(DataGridView data,string sql)
        {
            con = $"Data Source = {host};Initial Catalog=QlyHocSinh;Integrated Security=True";
            conn = new SqlConnection(con);
            query = sql;
            try
            {
                conn.Open();

                adapter = new SqlDataAdapter(query,conn);
                ds = new DataSet();
                adapter.Fill(ds);

                conn.Close();
                data.DataSource = ds.Tables[0];
            }catch (Exception ex)
            {
                MessageBox.Show("Lỗi " + ex.Message);
            }
        }

        private void pic_diemdanh_timkiem_Click(object sender, EventArgs e)
        {
            int gioi = 0,kha=0,trungbinh=0,kem=0,dem=0;
            string sql = "select kqth.MaHocSinh,hs.HoHS +' '+hs.TenHS as TenHocSinh,hk.TenHanhKiem,hl.TenHocLuc,kqth.DTBHocKi ,TenKetQua\r\n" +
                "from KetQua_HocKy_TongHop kqth,HocSinh hs,HanhKiem hk,HocLuc hl, Lop l,KetQua kq\r\n" +
                "where kqth.MaHocSinh = hs.MaHocSinh and kqth.MaHanhKiem = hk.MaHanhKiem\r\nand kqth.MaHocLuc = hl.MaHocLuc " +
                "and kq.MaKetQua = kqth.MaKetQua and kqth.MaLop = l.MaLop and l.TenLop = '"+cbb_HK_TH_TenLop.Text+"'";
            loaddb(dataGridView1, sql);
            string sql2 = "select  TenKetQua\r\n" +
                "from KetQua_HocKy_TongHop kqth,HocSinh hs,HanhKiem hk,HocLuc hl, Lop l,KetQua kq\r\n" +
                "where kqth.MaHocSinh = hs.MaHocSinh and kqth.MaHanhKiem = hk.MaHanhKiem\r\n" +
                "and kqth.MaHocLuc = hl.MaHocLuc and kq.MaKetQua = kqth.MaKetQua and kqth.MaLop = l.MaLop and l.TenLop = '"+cbb_HK_TH_TenLop.Text+"'";
            con = $"Data Source = {host};Initial Catalog=QlyHocSinh;Integrated Security=True";
            conn = new SqlConnection(con);
            query = sql2;
            try
            {
                conn.Open();

                cmd= new SqlCommand(sql2, conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["TenKetQua"].ToString() == "Giỏi")
                    {
                        gioi++;
                    }
                    else if (reader["TenKetQua"].ToString() == "Khá")
                    {
                        kha++;
                    }
                    else if(reader["TenKetQua"].ToString() == "Trung Bình")
                    {
                        trungbinh++;
                    }
                    else if(reader["TenKetQua"].ToString() == "Kém")
                    {
                        kem++;
                    }
                    dem++;
                }
                

                conn.Close();
                txt_hsgioi.Text = gioi.ToString();
                txt_hskha.Text = kha.ToString();
                txt_trungbinh.Text = trungbinh.ToString();
                txt_yeu.Text=kem.ToString();
                txt_siso.Text = dem.ToString();
            }catch (Exception ex )
            {
                MessageBox.Show ("Lỗi "+ex.Message);
            }
        }

        private void cbb_HK_TH_NamHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = "  select l.MaLop,l.TenLop\r\n  " +
                "from Lop l,NamHoc nh\r\n  " +
                "where l.MaNamHoc = nh.MaNamHoc and nh.TenNamHoc = '"+cbb_HK_TH_NamHoc.Text+"'";
            loadcbb(cbb_HK_TH_TenLop, sql, "TenLop", "MaLop");
        }

        public void loadcbb(ComboBox cbb,string sql,string display,string value)
        {
            con = $"Data Source={host};Initial Catalog=QlyHocSinh;Integrated Security=True";
            conn = new SqlConnection(con);
            try
            {
                conn.Open();

                adapter = new SqlDataAdapter(sql, conn);
                ds = new DataSet();
                adapter.Fill(ds);

                conn.Close();

                cbb.DataSource = ds.Tables[0];
                cbb.DisplayMember = display;
                cbb.ValueMember = value;

            }catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void frmHKTongHop_Load(object sender, EventArgs e)
        {
            string sql1 = "  select MaNamHoc,TenNamHoc from NamHoc";
            loadcbb(cbb_HK_TH_NamHoc, sql1, "TenNamHoc", "MaNamHoc");
            string sql2 = "select MaHocKi,TenHocKi from HocKi";
            loadcbb(cbb_HK_TH_HocKy, sql2, "TenHocKi", "MaHocKi");
        }

        public frmHKTongHop(string MALOAITK, string MATK, string Username)
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
