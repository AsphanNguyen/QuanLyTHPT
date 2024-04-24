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
    public partial class frmNHMonhoc : Form
    {
        string maloaitk = "", matk = "", username = "",con,query,host= @"LAPTOP-GFK5C0EF\SQLEXPRESS";
        SqlConnection conn;
        SqlDataAdapter adapter;
        SqlCommand cmd;
        SqlDataReader reader;

        private void pic_diemdanh_timkiem_Click(object sender, EventArgs e)
        {
            int tren8 = 0,tren65=0,dem=0;
            string sql = "   select cnmh.MaHocSinh,hs.HoHS +' '+ hs.TenHS as TenHocSinh,DTB_CaNam_Mon\r\n                " +
                "from KetQua_CaNam_MonHoc cnmh,HocSinh hs,Lop l,MonHoc mh\r\n                " +
                "where cnmh.MaLop = l.MaLop and hs.MaHocSinh = cnmh.MaHocSinh and cnmh.MaMonHoc = mh.MaMonHoc and TenLop = '"+cbb_tenlop.Text+"' and mh.TenMonHoc = N'"+cbb_monhoc.Text+"'";
            loaddb(dataGridView1,sql);

            con = $"Data Source = {host};Initial Catalog = QlyHocSinh;Integrated Security=True";
            conn = new SqlConnection(con);
            string sql2 = "\r\n  select DTB_CaNam_Mon\r\n  " +
                "from KetQua_CaNam_MonHoc cnmh,HocSinh hs,Lop l, MonHoc mh\r\n  " +
                "where cnmh.MaLop = l.MaLop and hs.MaHocSinh = cnmh.MaHocSinh and TenLop = '"+cbb_tenlop.Text+"' and cnmh.MaMonHoc = mh.MaMonHoc and mh.TenMonHoc = N'"+cbb_monhoc.Text+"' ";
            try
            {
                conn.Open();

                cmd = new SqlCommand(sql2,conn);
                reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    float diem = float.Parse(reader["DTB_CaNam_Mon"].ToString());
                   
                    if (diem >= 8)
                    {
                        tren8++;
                    }
                    else if(diem >= 6.5)
                    {
                        tren65++;
                    }
                    dem++;
                }

                conn.Close();
                txt_tren8.Text = tren8.ToString();
                txt_tren65.Text = tren65.ToString();
                txt_siso.Text = dem.ToString();
            }catch (Exception ex) 
            {
                MessageBox.Show("Lỗi " + ex.Message);
            }

        }

        DataSet ds;

        private void cbb_namhoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = "  select l.MaLop,l.TenLop\r\n  " +
                "from Lop l,NamHoc nh\r\n  " +
                "where l.MaNamHoc = nh.MaNamHoc and nh.TenNamHoc = '"+cbb_namhoc.Text+"'";
            loadcbb(cbb_tenlop, sql, "TenLop", "MaLop");
        }

        private void frmNHMonhoc_Load(object sender, EventArgs e)
        {
            string sql = "select MaNamHoc,TenNamHoc from NamHoc";
            loadcbb(cbb_namhoc, sql,"TenNamHoc","MaNamHoc");
            string sql2 = "select MaMonHoc,TenMonHoc from MonHoc";
            loadcbb(cbb_monhoc, sql2, "TenMonHoc", "MaMonHoc");
        }

        public void loaddb(DataGridView data,string sql)
        {
            con = $"Data Source = {host};Initial Catalog = QlyHocSinh;Integrated Security=True";
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

        public void loadcbb(ComboBox cbb,string sql,string display,string value)
        {
            con = $"Data Source = {host};Initial Catalog = QlyHocSinh;Integrated Security=True";
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

        public frmNHMonhoc(string MALOAITK, string MATK, string Username)
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
