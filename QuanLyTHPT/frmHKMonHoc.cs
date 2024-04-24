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
    public partial class frmHKMonHoc : Form
    {
        string maloaitk = "", matk="", username="";
        SqlConnection conn;
        SqlDataAdapter adapter;
        DataSet ds;
        SqlCommand cmd;
        SqlDataReader reader;
        string con,query,host = @"LAPTOP-GFK5C0EF\SQLEXPRESS";

        public void loadcbb(ComboBox cbb,string sql,string display,string value)
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
                cbb.DataSource = ds.Tables[0];
                cbb.DisplayMember = display;
                cbb.ValueMember = value;


            }catch (Exception ex)
            {
                MessageBox.Show("Lỗi " + ex.Message);
            }
        }

        private void cbb_MH_NamHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql3 = "  select MaLop,TenLop" +
                " from Lop l,NamHoc nh" +
                " where  l.MaNamHoc = nh.MaNamHoc and nh.TenNamHoc = '"+cbb_MH_NamHoc.Text+"' ";
            loadcbb(cbb_MH_TenLop, sql3, "TenLop", "MaLop");
            string sql4 = "select MaMonHoc,TenMonHoc from MonHoc";
            loadcbb(cbb_MH_TenMH, sql4, "TenMonHoc", "MaMonHoc");
            
        }

        private void pic_diemdanh_timkiem_Click(object sender, EventArgs e)
        {
            int tren8=0, tren65=0, tren5 = 0,duoi5=0,dem=0;
            string sql1 = "  select kqmh.MaHocSinh,hs.HoHS+' '+ hs.TenHS as TenHocSinh,kqmh.DTBMonHK  " +
                "from Lop l,HocSinh hs,MonHoc mh,KetQua_HocKy_MonHoc kqmh  " +
                "where kqmh.MaMonHoc = mh.MaMonHoc and kqmh.MaHocSinh = hs.MaHocSinh and kqmh.MaLop = l.MaLop and mh.TenMonHoc = N'"+cbb_MH_TenMH.Text+"' and l.TenLop = '"+cbb_MH_TenLop.Text+"'";
            loaddb(dataGridView1, sql1);
            con = $"Data Source = {host};Initial Catalog = QlyHocSinh;Integrated Security=True";
            conn = new SqlConnection(con);
            string sql = "   select kqmh.DTBMonHK  \r\n                 " +
                "from Lop l,HocSinh hs,MonHoc mh,KetQua_HocKy_MonHoc kqmh  \r\n                " +
                " where kqmh.MaMonHoc = mh.MaMonHoc and kqmh.MaHocSinh = hs.MaHocSinh and kqmh.MaLop = l.MaLop and mh.TenMonHoc = N'"+cbb_MH_TenMH.Text+"' and l.TenLop = '"+cbb_MH_TenLop.Text+"'";
            try
            {
                conn.Open();

                cmd = new SqlCommand(sql, conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    float diem = float.Parse(reader["DTBMonHK"].ToString());
                    if (diem >= 8)
                    {
                        tren8++;
                    }
                    else if (diem >= 6.5)
                    {
                        tren65++;
                    }
                    else if(diem >= 5)
                    {
                        tren5++;
                    }
                    else if(diem < 5)
                    {
                        duoi5++;
                    }
                    dem++;
                }
                txt_tren8.Text = tren8.ToString();
                txt_tren65.Text = tren65.ToString();
                txt_tren5.Text = tren5.ToString();
                txt_duoi5.Text = duoi5.ToString();
                txt_siso.Text = dem.ToString();
                conn.Close();
            }catch (Exception ex)
            {
                MessageBox.Show("Lỗi " + ex.Message);
            }
        }

        public void loaddb(DataGridView data,string sql)
        {
            con = $"Data Source = {host};Initial Catalog = QlyHocSinh;Integrated Security=True";
            conn = new SqlConnection(con);
            query = sql;
            try
            {
                conn.Open();

                adapter = new SqlDataAdapter (query,conn);
                ds = new DataSet();
                adapter.Fill(ds);

                conn.Close();

                data.DataSource = ds.Tables[0];
            }catch (Exception ex)
            {
                MessageBox.Show (ex.Message);
            }
        }

        private void frmHKMonHoc_Load(object sender, EventArgs e)
        {
            string sql1 = "select MaNamHoc,TenNamHoc from NamHoc";
            loadcbb(cbb_MH_NamHoc,sql1, "TenNamHoc", "MaNamHoc");
            string sql2 = "select MaHocKi,TenHocKi from HocKi";
            loadcbb(cbb_MH_HocKy, sql2, "TenHocKi", "MaHocKi");
           
        }

        public frmHKMonHoc(string MaLoaiTK, string MATK, string Username)
        {
            maloaitk = MaLoaiTK;
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
