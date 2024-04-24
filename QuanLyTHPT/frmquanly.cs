using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;


namespace QuanLyTHPT
{
    public partial class frmquanly : Form
    {
        string TaiKhoan;
        string maloai, matk,con,tk; //tk là username
        string DATASOURCE = @"LAPTOP-GFK5C0EF\SQLEXPRESS";
        public frmquanly(string MaLoaitk,string MaTK,string username)
        {
            maloai = MaLoaitk; 
            matk = MaTK;
            tk=username;
            InitializeComponent();
        }

        //Nút đăng xuất
        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            frmDangNhap dangnhap = new frmDangNhap();
            dangnhap.Show();
            this.Hide();
        }
        //nút thống kê 
        private void mônHọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHKMonHoc hk = new frmHKMonHoc(maloai,matk,tk);
            hk.Show();
            this.Hide();
        }
        private void tổngHToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmHKTongHop th = new frmHKTongHop(maloai,matk,tk);
            th.Show();
            this.Hide();
        }

        private void mônHọcToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmNHMonhoc mh = new frmNHMonhoc(maloai,matk,tk);
            mh.Show();
            this.Hide();
        }

        private void tổngHợpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNHTonghop th = new frmNHTonghop(maloai,matk,tk);
            th.Show();
            this.Hide();
        }
        private void frmquanly_Load(object sender, EventArgs e)
        {
            // From Lop
            string sSQL = "select DISTINCT TenLop,TenKhoiLop,TenNamHoc,SiSo,TenGV" +
                    " from Lop L,KhoiLop KL,NamHoc NH,GiaoVien GV" +
                    " where L.MaKhoiLop=KL.MaKhoiLop and L.MaGiaoVien = GV.MaGiaoVien and L.MaNamHoc=NH.MaNamHoc and L.TrangThai = '1'" +
                    " group by TenLop,TenKhoiLop,TenNamHoc,TenGV,SiSo";
            XemDS(sSQL, data_lop_dslop);
            string sSQL1 = "Select TenNamHoc,MaNamHoc from NamHoc";
            XemDSCBX(sSQL1, cbb_lop_namhoc, "TenNamHoc", "MaNamHoc");
            string sSQL2 = "Select TenKhoiLop,MaKhoiLop from KhoiLop";
            XemDSCBX(sSQL2, cbb_lop_khoilop, "TenKhoiLop", "MaKhoiLop");
            cbb_lop_khoilop.Text = "";
        }
        //Đổi mật khẩu
        private void đổiMậtKhẩuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDoiMatKhau doiMatKhau = new frmDoiMatKhau(tk);
            doiMatKhau.Show();
            this.Hide();
        }
        public void XemDSCBX(string sSQL, ComboBox cbx, String temptext, String tempvalue)
        {
            string scon;
            scon = $"Data Source= {DATASOURCE};Initial Catalog=QlyHocSinh;Integrated Security=True";
            SqlConnection myConnection = new SqlConnection(scon);
            try
            {
                myConnection.Open();
                SqlDataAdapter daQuanLy = new SqlDataAdapter(sSQL, myConnection);
                DataSet dsQuanLy = new DataSet();
                daQuanLy.Fill(dsQuanLy);


                myConnection.Close();
                cbx.DataSource = dsQuanLy.Tables[0];
                cbx.DisplayMember = temptext;
                cbx.ValueMember = tempvalue;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi chi tiet " + ex.Message);
            }
        }

        public void XemDS(string sSQL,DataGridView data)
        {
            string scon;
            scon = $"Data Source={DATASOURCE};Initial Catalog=QlyHocSinh;Integrated Security=True";

            SqlConnection myConnection = new SqlConnection(scon);

            try
            {
                myConnection.Open();
                SqlDataAdapter daQuanLy = new SqlDataAdapter(sSQL, myConnection);
                DataSet dsQuanLy = new DataSet();
                daQuanLy.Fill(dsQuanLy);

                myConnection.Close();

                data.DataSource = dsQuanLy.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi chi tiet " + ex.Message);
            }
        }
        private bool ThucThi(string sSQL)
        {
            bool kq = true;
            string scon;
            scon = $"Data Source={DATASOURCE};Initial Catalog=QlyHocSinh;Integrated Security=True";
            SqlConnection myConnection = new SqlConnection(scon);
            try
            {
                myConnection.Open();
                SqlCommand command = new SqlCommand(sSQL, myConnection);
                command.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (Exception ex)
            {
                kq = false;
                MessageBox.Show("Loi chi tiet " + ex.Message);
            }
            return kq;
        }
        
        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(TabControl.SelectedTab == tab_lop)
            {
                string sSQL = "select DISTINCT TenLop,TenKhoiLop,TenNamHoc,SiSo,TenGV" +
                    " from Lop L,KhoiLop KL,NamHoc NH,GiaoVien GV" +
                    " where L.MaKhoiLop=KL.MaKhoiLop and L.MaGiaoVien = GV.MaGiaoVien and L.MaNamHoc=NH.MaNamHoc and L.TrangThai = '1'" +
                    " group by TenLop,TenKhoiLop,TenNamHoc,TenGV,SiSo";
                XemDS(sSQL, data_lop_dslop);
                string sSQL1 = "Select TenNamHoc,MaNamHoc from NamHoc";
                XemDSCBX(sSQL1, cbb_lop_namhoc, "TenNamHoc", "MaNamHoc");
                string sSQL2 = "Select TenKhoiLop,MaKhoiLop from KhoiLop";
                XemDSCBX(sSQL2, cbb_lop_khoilop, "TenKhoiLop", "MaKhoiLop");
                cbb_lop_khoilop.Text = "";
            }
            else if(TabControl.SelectedTab == tab_namhoc)
            {
                string sSQL = "Select MaNamHoc,TenNamHoc " +
                    "from NamHoc";
                XemDS(sSQL, data_namhoc_dsnamhoc);

            }
            else if(TabControl.SelectedTab == tab_monhoc)
            {
                string sSQL = "Select MaMonHoc,TenMonHoc " +
                    "from MonHoc";
                XemDS(sSQL, data_monhoc_dsmonhoc);
            }
            else if(TabControl.SelectedTab == tab_diem)
            {
                string sSQL = "select MaNamHoc,TenNamHoc from NamHoc";
                XemDSCBX(sSQL, cbb_diem_namhoc, "TenNamHoc", "MaNamHoc");
                string sSQL2 = $"Select MaLop,TenLop from Lop l where MaNamHoc = 'NH2223'";
                XemDSCBX(sSQL2, cbb_diem_lop, "TenLop", "MaLop");
                cbb_diem_lop.Text = "";
                string sSQL1 = "SELECT hs.MaHocSinh,  HoHs + ' ' + TenHS AS TenHocSinh,TenMonHoc, " +
                    " STRING_AGG( CASE WHEN ld.MaLoaiDiem = 'LD01' THEN Diem ELSE NULL END, ', ' ) AS Diem15Phut," +
                    " STRING_AGG( CASE WHEN ld.MaLoaiDiem = 'LD02' THEN Diem ELSE NULL END, ', '  ) AS Diem1Tiet," +
                    " STRING_AGG( CASE WHEN ld.MaLoaiDiem = 'LD03' THEN Diem ELSE NULL END, ', '  ) AS DiemThi " +
                    "  FROM  HocSinh hs INNER JOIN   Diem d ON hs.MaHocSinh = d.MaHocSinh" +
                    "   INNER JOIN  CotDiem cd ON d.MaCotDiem = cd.MaCotDiem" +
                    "  INNER JOIN    LoaiDiem ld ON cd.MaLoaiDiem = ld.MaLoaiDiem " +
                    "  INNER JOIN MonHoc mh ON mh.MaMonHoc = d.MaMonHoc" +
                    "  where d.MaMonHoc = 'MH001'" +
                    " GROUP BY hs.MaHocSinh, HoHs + ' ' + TenHS,TenMonHoc;";
                XemDS(sSQL1, data_diem_dsdiem);

            }
            else if(TabControl.SelectedTab == tab_hocsinh) //luu y
            {
                string sSQL1 = "select TenNamHoc,MaNamHoc from NamHoc";
                XemDSCBX(sSQL1, cbb_hocsinh_namhoc, "TenNamHoc", "MaNamHoc");
                string value_namhoc = cbb_hocsinh_namhoc.SelectedValue.ToString();
                string sSQL = "select MaHocSinh,HoHS + ' ' + TenHS TenHocSinh, GioiTinh,NgaySinh,DiaChi,CCCD,SDT,TenLop,MaTaiKhoan" +
                    " from HocSinh HS,Lop L" +
                    " where hs.TrangThai = 1 and HS.MaLop=L.MaLop and L.MaNamHoc = '" + value_namhoc + "'";
                XemDS(sSQL, data_hocsinh_dshocisnh);
                string sSQL2 = "Select MaLop,TenLop from Lop l, NamHoc nh " +
                    $"where nh.MaNamHoc = l.MaNamHoc and l.MaNamHoc = '{value_namhoc}'";
                XemDSCBX(sSQL2, cbb_hocsinh_tenlop, "TenLop", "MaLop");
     

                //string sSQL = "select MaHocSinh,HoHS + ' ' + TenHS TenHocSinh, GioiTinh,NgaySinh,DiaChi,CCCD,SDT,TenLop" +
                //    " from HocSinh HS,Lop L" +
                //    " where HS.MaLop=L.MaLop";
                //XemDS(sSQL, data_hocsinh_dshocisnh);
            }
            else if (TabControl.SelectedTab == tab_phanlop)
            {
                string sSQL = "  select TenNamHoc,MaNamHoc from NamHoc";
                string query = "select TenKhoiLop,MaKhoiLop from KhoiLop";
                XemDSCBX(sSQL, cbb_phanlop_namhoc, "TenNamHoc", "MaNamHoc");
                XemDSCBX(query, cbb_phanlop_khoilop, "TenKhoiLop", "MaKhoiLop");
                XemDSCBX(sSQL, cbb_phanlop_nhoclopmoi, "TenNamHoc", "MaNamHoc");
                XemDSCBX(query, cbb_phanlop_khoilopmoi, "TenKhoiLop", "MaKhoiLop");
            }
            else if (TabControl.SelectedTab == tab_giaovien)
            {
                string query = "  select TenMonHoc,MaMonHoc from MonHoc ";
                XemDSCBX(query, cbb_giaovien_monhoc, "TenMonHoc", "MaMonHoc");
                string query2 = " select distinct MaGiaoVien,TenGV,GioiTinh," +
                    "case ChucVu when '1' then N'Ban giám hiệu'" +
                    " when '2' then N'Giáo vụ' " +
                    "Else N'Giáo viên' end as Chucvu,CCCD,SDT,mh.TenMonHoc,NgaySinh,DiaChi,MaTaiKhoan,case gv.TrangThai " +
                    " when '1' then N'Hoạt Động' else N'Không Hoạt Động' end as TrangThai " +
                    "from GiaoVien gv,MonHoc mh " +
                    "where gv.MaMonHoc = mh.MaMonHoc ";
                XemDS(query2, data_giaovien_dsgiaovien);
            }
            else if (TabControl.SelectedTab == tab_phancong)
            {
                string sSQL = "select TenNamHoc,TenLop,TenGV,TenMonHoc" +
                    " from PhanCong pc,NamHoc nh,Lop l, MonHoc mh,GiaoVien gv" +
                    " where pc.MaGiaoVien = gv.MaGiaoVien and pc.MaLop=l.MaLop " +
                    " and pc.MaMonHoc = mh.MaMonHoc and pc.MaNamHoc=nh.MaNamHoc";
                XemDS(sSQL, data_phancong_dsphancong);
                string query1 = "Select TenNamHoc,MaNamHoc from NamHoc";
                XemDSCBX(query1, cbb_phancong_namhoc, "TenNamHoc", "MaNamHoc");
            }
            else if (TabControl.SelectedTab == tab_taikhoan)
            {
                string query = "select * from TaiKhoan";
                XemDS(query, data_taikhoan_dstaikhoan);
            }
            else if (TabControl.SelectedTab == tab_diemdanh)
            {
                string sql = "select MaNamHoc,TenNamHoc from NamHoc";
                XemDSCBX(sql, cbb_diemdanh_namhoc, "TenNamHoc", "MaNamHoc");
                string sql2 = "select MaHocKi,TenHocKi from HocKi";
                XemDSCBX(sql2, cbb_diemdanh_hocky, "TenHocKi", "MaHocKi");
                string sql3 = "select MaKhoiLop,TenKhoiLop from KhoiLop";
                XemDSCBX(sql3, cbb_diemdanh_khoilop, "TenKhoiLop", "MaKhoiLop");
                
                
                string query = "select  DD.MaHocSinh,HoHS + ' ' +TenHS As HoTenHS,TenLop,TenKhoiLop, TenHocKi,TenNamHoc,NgayPhep,NgayKhongPhep" +
                    " from DiemDanh DD,HocSinh HS, Lop L, HocKi HK, NamHoc NH,KhoiLop KL" +
                    " where DD.MaHocKi=HK.MaHocKi and DD.MaHocSinh=HS.MaHocSinh and DD.MaLop = L.MaLop " +
                    " and DD.MaNamHoc=NH.MaNamHoc and KL.MaKhoiLop = L.MaKhoiLop";
                XemDS(query, data_diemdanh_dsdiemdanh);


            }
            else if (TabControl.SelectedTab == tab_tracuu)
            {
             
                if (tabControl1.SelectedTab == tab_tracuu_hocsinh)
                {
                    string query4 = " select hs.MaHocSinh,hs.HoHS + hs.TenHS as HoTenHocSinh,GioiTinh,NgaySinh,DiaChi,l.TenLop,hs.TrangThai from Lop l,HocSinh hs where l.MaLop = hs.MaLop ";
                    XemDS(query4, data_tracuu_hocsinh_dstracuuhs);
                   
                }
            }
            else if (TabControl.SelectedTab == tab_thongtin)
            {
                string query = "select HoHS+' '+TenHS as HoTen,GioiTinh,NgaySinh,DiaChi from HocSinh hs,TaiKhoan tk where hs.MaTaiKhoan = tk.MaTaiKhoan and tk.MaTaiKhoan like '" + matk + "'";
                string scon;
                scon = $"Data Source={DATASOURCE};Initial Catalog=QlyHocSinh;Integrated Security=True";
                SqlCommand cmd;
                SqlConnection myConnection = new SqlConnection(scon);
                try
                {
                    myConnection.Open();
                    cmd = new SqlCommand(query, myConnection);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        txt_thongtin_namehs.Text = reader["HoTen"].ToString();
                        txt_thongtin_gioitinh.Text = reader["GioiTinh"].ToString();
                        string date = reader["NgaySinh"].ToString();
                        date_thongtin_ngsinh.Text = date;
                        txt_thongtin_diachi.Text = reader["DiaChi"].ToString();
                    }

                    myConnection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
        //tab Lop
        private void cbb_lop_namhoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbb_lop_khoilop.Text = "";
            cbb_lop_tenlop.Text = "";
            cbb_lop_gvcn.Text = "";
        }

        private void cbb_lop_khoilop_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value_namhoc = cbb_lop_namhoc.SelectedValue.ToString(); // lấy giá trị năm học
            string value_khoilop = cbb_lop_khoilop.SelectedValue.ToString();
            string sSQL3 = "Select TenLop,MaLop" +
                " from Lop L " +
                " where MaKhoiLop = '" + value_khoilop + "' and MaNamHoc = '" + value_namhoc + "' and L.TrangThai='1'";
            XemDSCBX(sSQL3, cbb_lop_tenlop, "TenLop", "MaLop");
            cbb_lop_tenlop.Text = "";
        }

        private void data_lop_dslop_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (data_lop_dslop.SelectedRows.Count > 0)
            {
                DataGridViewSelectedRowCollection selectedRow = data_lop_dslop.SelectedRows;
                cbb_lop_namhoc.Text = selectedRow[0].Cells["TenNamHoc"].Value.ToString();
                cbb_lop_khoilop.Text = selectedRow[0].Cells["TenKhoiLop_Lop"].Value.ToString();
                cbb_lop_tenlop.Text = selectedRow[0].Cells["TenLop"].Value.ToString();
                cbb_lop_gvcn.Text = selectedRow[0].Cells["TenGV"].Value.ToString();
            }

        }
        private void pic_lop_timkiem_Click(object sender, EventArgs e)
        {
            string temp_namhoc = cbb_lop_namhoc.Text, value_namhoc = cbb_lop_namhoc.SelectedValue.ToString();
            string temp_khoilop = cbb_lop_khoilop.Text, value_khoilop = cbb_lop_khoilop.SelectedValue.ToString();
            string text_tenlop = cbb_lop_tenlop.Text, value_tenlop = cbb_lop_tenlop.SelectedValue.ToString();
            string dieukien_namhoc = "";
            string dieukien_khoilop = "";
            string dieukien_tenlop = "";
            if (temp_namhoc != "")
            {
                dieukien_namhoc = " and L.MaNamHoc = '" + value_namhoc + "' ";
            }
            if (temp_khoilop != "")
            {
                dieukien_khoilop = " and L.MaKhoiLop = '" + value_khoilop + "' ";
            }
            if (text_tenlop != "")
            {
                dieukien_tenlop = " and L.MaLop = '" + value_tenlop + "' ";
            }
            string sSQL = "select TenGV,MaGiaoVien" +
                          " from giaovien " +
                          " where MaGiaoVien not in ( select MaGiaoVien from Lop " +
                          "                           where MaNamHoc = '" + value_namhoc + "' and TrangThai = '1' )";
            XemDSCBX(sSQL, cbb_lop_gvcn, "TenGV", "MaGiaoVien"); // hiện combobox giáo viên chưa có chủ nhiệm 
            cbb_lop_gvcn.Text = "";
            string sSQL1 = "select DISTINCT TenLop,TenKhoiLop,TenNamHoc,SiSo,TenGV" +
                    " from Lop L,KhoiLop KL,NamHoc NH,GiaoVien GV" +
                    " where L.MaKhoiLop=KL.MaKhoiLop and L.MaGiaoVien = GV.MaGiaoVien and L.MaNamHoc=NH.MaNamHoc and L.TrangThai = '1' " +
                    $" {dieukien_namhoc} {dieukien_khoilop} {dieukien_tenlop}" +
                    " group by TenLop,TenKhoiLop,TenNamHoc,TenGV,SiSo";
            XemDS(sSQL1, data_lop_dslop);
        }
        private void pic_lop_them_Click(object sender, EventArgs e)
        {
            string txt_Tenlop = cbb_lop_tenlop.Text;
            string value_namhoc = cbb_lop_namhoc.SelectedValue.ToString(), temp_namhoc = cbb_lop_namhoc.Text;
            string value_khoilop = cbb_lop_khoilop.SelectedValue.ToString(), temp_khoilop = cbb_lop_khoilop.Text;
            string value_magiaovien = cbb_lop_gvcn.SelectedValue.ToString();
            string sQuery = $"Insert into Lop (MaLop,TenLop,MaKhoiLop,MaNamHoc,MaGiaoVien,TrangThai)" +
                $" values ('0','{txt_Tenlop}','{value_khoilop}','{value_namhoc}','{value_magiaovien}','1')";
            string sSQL1 = "select DISTINCT TenLop,TenKhoiLop,TenNamHoc,SiSo,TenGV" +
                    " from Lop L,KhoiLop KL,NamHoc NH,GiaoVien GV" +
                    " where L.MaKhoiLop=KL.MaKhoiLop and L.MaGiaoVien = GV.MaGiaoVien and L.MaNamHoc=NH.MaNamHoc and L.TrangThai = '1' " +
                    " and TenNamHoc='" + temp_namhoc + "' " +
                    " and TenKhoiLop=N'" + temp_khoilop + "' and TenLop =N'" + txt_Tenlop + "' " +
                    " group by TenLop,TenKhoiLop,TenNamHoc,TenGV,SiSo";
            if (ThucThi(sQuery))
            {
                MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                XemDS(sSQL1, data_lop_dslop);
            }
            else
            {
                MessageBox.Show("Thêm thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void pic_lop_xoa_Click(object sender, EventArgs e)
        {
            string txt_Tenlop = cbb_lop_tenlop.Text, value_tenlop = cbb_lop_tenlop.SelectedValue.ToString();
            string value_namhoc = cbb_lop_namhoc.SelectedValue.ToString();
            string value_khoilop = cbb_lop_khoilop.SelectedValue.ToString();
            string value_magiaovien = cbb_lop_gvcn.SelectedValue.ToString();
            string sSQL = $"Update Lop set TrangThai = '0' " +
                        $" where MaLop = '{value_tenlop}' ";
            string sSQL1 = "select DISTINCT TenLop,TenKhoiLop,TenNamHoc,SiSo,TenGV" +
                    " from Lop L,KhoiLop KL,NamHoc NH,GiaoVien GV" +
                    " where L.MaKhoiLop=KL.MaKhoiLop and L.MaGiaoVien = GV.MaGiaoVien and L.MaNamHoc=NH.MaNamHoc and L.TrangThai = '1' " +
                    " and L.MaNamHoc='" + value_namhoc + "' " +
                    " and L.MaKhoiLop= '" + value_khoilop + "' and L.MaLop = '" + value_tenlop + "' " +
                    " group by TenLop,TenKhoiLop,TenNamHoc,TenGV,SiSo";
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Xác nhận",
                                      MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (ThucThi(sSQL))
                {
                    MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    XemDS(sSQL1, data_lop_dslop);
                }
                else
                {
                    MessageBox.Show("Xóa thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }
        }

        private void pic_lop_luu_Click(object sender, EventArgs e)
        {
            string txt_Tenlop = cbb_lop_tenlop.Text, value_tenlop = cbb_lop_tenlop.SelectedValue.ToString();
            string value_namhoc = cbb_lop_namhoc.SelectedValue.ToString();
            string value_khoilop = cbb_lop_khoilop.SelectedValue.ToString();
            string value_magiaovien = cbb_lop_gvcn.SelectedValue.ToString();
            string sSQL = $"Update Lop set MaGiaoVien = '{value_magiaovien}' " +
                        $" where MaLop = '{value_tenlop}' ";
            string sSQL1 = "select DISTINCT TenLop,TenKhoiLop,TenNamHoc,SiSo,TenGV" +
                    " from Lop L,KhoiLop KL,NamHoc NH,GiaoVien GV" +
                    " where L.MaKhoiLop=KL.MaKhoiLop and L.MaGiaoVien = GV.MaGiaoVien and L.MaNamHoc=NH.MaNamHoc and L.TrangThai = '1' " +
                    " and L.MaNamHoc='" + value_namhoc + "' " +
                    " and L.MaKhoiLop= '" + value_khoilop + "' and L.MaLop = '" + value_tenlop + "' " +
                    " group by TenLop,TenKhoiLop,TenNamHoc,TenGV,SiSo";
            if (ThucThi(sSQL))
            {
                MessageBox.Show("Lưu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                XemDS(sSQL1, data_lop_dslop);
            }
            else
            {
                MessageBox.Show("Lưu thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        //tab monhoc

        private void pic_monhoc_them_Click(object sender, EventArgs e)
        {
            string temp_mamonhoc=cbb_monhoc_mamonhoc.Text;
            string temp_tenmonhoc=cbb_monhoc_tenmonhoc.Text;


            string sSQL = "INSERT INTO MONHOC(MAMONHOC,TENMONHOC) VALUES (N'"+temp_mamonhoc+"',N'"+temp_tenmonhoc+"')";
            if (ThucThi(sSQL))
            {
                MessageBox.Show("Thêm Thành Công");
            }
            else MessageBox.Show("Thêm Thất Bại");
            string sSQL1 = "Select MaMonHoc,TenMonHoc " +
                    "from MonHoc";
            XemDS(sSQL1, data_monhoc_dsmonhoc);
        }

        private void pic_monhoc_xoa_Click(object sender, EventArgs e)
        {
            string temp_mamonhoc = cbb_monhoc_mamonhoc.Text;
            string temp_tenmonhoc = cbb_monhoc_tenmonhoc.Text;
            string sSQL = "Delete from MONHOC where MAMONHOC = '" + temp_mamonhoc + "' and TENMONHOC= N'" +temp_tenmonhoc +"'";
            if (ThucThi(sSQL))
            {
                MessageBox.Show("Xóa Thành Công");
            }
            else MessageBox.Show("Xóa Thất Bại");
            string sSQL1 = "Select MaMonHoc,TenMonHoc " +
                    "from MonHoc";
            XemDS(sSQL1, data_monhoc_dsmonhoc);
            cbb_monhoc_mamonhoc.Text = "";
            cbb_monhoc_tenmonhoc.Text = "";
        }
        private void data_monhoc_dsmonhoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (data_monhoc_dsmonhoc.SelectedRows.Count > 0)
            {
                DataGridViewSelectedRowCollection selectedRow = data_monhoc_dsmonhoc.SelectedRows;
                cbb_monhoc_mamonhoc.Text = selectedRow[0].Cells["MaMonHoc"].Value.ToString();
                cbb_monhoc_tenmonhoc.Text = selectedRow[0].Cells["TenMonHoc"].Value.ToString();
            }
        }
        private void pic_monhoc_reset_Click(object sender, EventArgs e)
        {
            cbb_monhoc_mamonhoc.Text = "";
            cbb_monhoc_tenmonhoc.Text = "";
        }
        private void pic_monhoc_luu_Click(object sender, EventArgs e) 
        {
            string sql = "  update MonHoc\r\n\t" +
                "set TenMonHoc = N'"+cbb_monhoc_tenmonhoc.Text+"' " +
                "where MaMonHoc = '"+cbb_monhoc_mamonhoc.Text+"'";
            string sql2 = "select MaMonHoc,TenMonHoc from MonHoc";
            if (ThucThi(sql))
            {
                MessageBox.Show("Lưu thành công", "Thông báo");
                XemDS(sql2, data_monhoc_dsmonhoc);
            }
            else
            {
                MessageBox.Show("Xóa thất bại","Thông báo");
            }
        }
        //TAb năm học
        private void data_namhoc_dsnamhoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (data_namhoc_dsnamhoc.SelectedRows.Count>0)
            {
                DataGridViewRow row = data_namhoc_dsnamhoc.Rows[e.RowIndex];
                txt_namhoc_manh.Text = row.Cells["MaNamHoc"].Value.ToString();
                txt_namhoc_tennh.Text = row.Cells["TenNamHoc_NH"].Value.ToString();
            }
        }

        
        private void pic_namhoc_them_Click(object sender, EventArgs e)
        {
            string query= "Insert Into NamHoc (MaNamHoc,TenNamHoc) values('" + txt_namhoc_manh.Text+"','"+txt_namhoc_tennh.Text+"')";
            string query2 = "select MaNamHoc,TenNamHoc from NamHoc";
            if (ThucThi(query)==true)
            {
                MessageBox.Show("Thêm thành công");
                XemDS(query2,data_namhoc_dsnamhoc);
            }
            else
            {
                MessageBox.Show("Thêm thất bại");
            }
        }
        private void pic_namhoc_xoa_Click(object sender, EventArgs e)
        {
            string query = "delete from NamHoc where MaNamHoc = '" + txt_namhoc_manh.Text + "'";
            string query2 = "Select MaNamHoc,TenNamHoc from NamHoc";
            if (ThucThi(query)==true)
            {
                MessageBox.Show("Xoá thành công");
                XemDS(query2,data_namhoc_dsnamhoc);
                txt_namhoc_manh.Text = "";
                txt_namhoc_tennh.Text = "";
            }
            else
            {
                MessageBox.Show("Xóa thất bại");
            }
        }
        private void pic_namhoc_reset_Click(object sender, EventArgs e)
        {
            txt_namhoc_manh.Text = "";
            txt_namhoc_tennh.Text = "";
        }
        private void pic_namhoc_luu_Click(object sender, EventArgs e)
        {
            string sql = "  update NamHoc\r\n\t" +
                "set TenNamHoc = '"+txt_namhoc_tennh.Text+"'\r\n\t" +
                "where MaNamHoc = '"+txt_namhoc_manh.Text+"'";
            string sql2 = "select MaNamHoc,TenNamHoc from NamHoc";
            if (ThucThi(sql))
            {
                MessageBox.Show("Lưu thành công", "Thông báo");
                XemDS(sql2, data_namhoc_dsnamhoc);
            }
            else
            {
                MessageBox.Show("Lưu thất bại", "Thất bại");
            }

        }

        //Phân lớp
        private void cbb_phanlop_khoilop_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "  select TenLop,MaLop from KhoiLop kl,Lop l " +
                "where kl.MaKhoiLop = l.MaKhoiLop and TenKhoiLop like N'" + cbb_phanlop_khoilop.Text + "'";
            cbb_phanlop_lop.Text = "";
            XemDSCBX(query, cbb_phanlop_lop, "TenLop", "MaLop");
        }
        private void cbb_phanlop_lop_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text_namhoc = cbb_phanlop_namhoc.Text;
            string text_khoilop = cbb_phanlop_khoilop.Text;
            string text_lop = cbb_phanlop_lop.Text;
            string query = "select distinct pl.MaHocSinh,HoHS + ' '+ TenHS As TenHS" +
                " from PhanLop pl,Lop l,HocSinh hs,khoilop kl,namhoc NH " +
                " where pl.MaLop=l.MaLop and pl.MaHocSinh = hs.MaHocSinh and pl.MaNamHoc = NH.MaNamHoc and pl.MaKhoiLop = kl.MaKhoiLop " +
                " and TenLop like '" + text_lop + "' and TenNamHoc like '" + text_namhoc + "' and TenKhoiLop like N'" + text_khoilop + "'";
            XemDS(query, data_phanlop_dsachcu);
        }
        private void cbb_phanlop_khoilopmoi_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "  select TenLop,MaLop from KhoiLop kl,Lop l " +
                " where kl.MaKhoiLop = l.MaKhoiLop and TenKhoiLop like N'" + cbb_phanlop_khoilopmoi.Text + "'";
            cbb_phanlop_lopmoi.Text = "";
            XemDSCBX(query, cbb_phanlop_lopmoi, "TenLop", "MaLop");
        }
        private void cbb_phanlop_lopmoi_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text_namhoc = cbb_phanlop_nhoclopmoi.Text;
            string text_khoilop = cbb_phanlop_khoilopmoi.Text;
            string text_lop = cbb_phanlop_lopmoi.Text;
            string query = "select distinct pl.MaHocSinh,HoHS + ' '+ TenHS As TenHS" +
                " from PhanLop pl,Lop l,HocSinh hs,khoilop kl,namhoc NH " +
                " where pl.MaLop=l.MaLop and pl.MaHocSinh = hs.MaHocSinh and pl.MaNamHoc = NH.MaNamHoc and pl.MaKhoiLop = kl.MaKhoiLop " +
                " and TenLop like '" + text_lop + "' and TenNamHoc like '" + text_namhoc + "' and TenKhoiLop like N'" + text_khoilop + "'";
            XemDS(query, data_phanlop_dsachmoi);
        }
        private void cbb_phanlop_namhoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text_namhoc = cbb_phanlop_namhoc.Text;
            string query = "select distinct TenKhoiLop,kl.MaKhoiLop" +
                " from Lop l,khoilop kl,namhoc nh" +
                " where l.MaKhoiLop=kl.MaKhoiLop and nh.MaNamHoc=l.MaNamHoc and TenNamHoc='" + text_namhoc + "'";
            cbb_phanlop_lop.Text = "";
            cbb_phanlop_khoilop.Text = "";
            XemDSCBX(query, cbb_phanlop_khoilop, "TenKhoiLop", "kl.MaKhoiLop");
        }
        private void cbb_phanlop_nhoclopmoi_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text_namhoc = cbb_phanlop_nhoclopmoi.Text;
            string query = "select distinct TenKhoiLop,kl.MaKhoiLop" +
                " from Lop l,khoilop kl,namhoc nh" +
                " where l.MaKhoiLop=kl.MaKhoiLop and nh.MaNamHoc=l.MaNamHoc and TenNamHoc='" + text_namhoc + "'";
            cbb_phanlop_lopmoi.Text = "";
            cbb_phanlop_khoilopmoi.Text = "";
            XemDSCBX(query, cbb_phanlop_khoilopmoi, "TenKhoiLop", "kl.MaKhoiLop");
        }
        private void pic_phanlop_chuyensang_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection row = data_phanlop_dsachcu.SelectedRows;
            string text_namhoccu = cbb_phanlop_namhoc.Text;
            string text_khoilopcu = cbb_phanlop_khoilop.Text;
            string text_lopcu = cbb_phanlop_lop.Text;
            string text_namhocmoi = cbb_phanlop_nhoclopmoi.Text;
            string text_khoilopmoi = cbb_phanlop_khoilopmoi.Text;
            string text_lopmoi = cbb_phanlop_lopmoi.Text;
            string mahs = row[0].Cells["MaHocSinh"].Value.ToString();
            string query = " update PhanLop set MaKhoiLop = (select MaKhoiLop from KhoiLop where TenKhoiLop like N'" + text_khoilopmoi + "')," +
                "MaLop = (select MaLop from Lop where TenLop like N'" + text_lopmoi + "' and MaNamHoc= (select MaNamHoc from NamHoc where TenNamHoc like '" + text_namhocmoi + "' ))," +
                " MaNamHoc = (select MaNamHoc from NamHoc where TenNamHoc like '" + text_namhocmoi + "' )" +
                " where MaHocSinh like '" + mahs + "'";
            string query2 = "select distinct pl.MaHocSinh,HoHS + ' '+ TenHS As TenHS" +
                " from PhanLop pl,Lop l,HocSinh hs,khoilop kl,namhoc NH " +
                " where pl.MaLop=l.MaLop and pl.MaHocSinh = hs.MaHocSinh and pl.MaNamHoc = NH.MaNamHoc and pl.MaKhoiLop = kl.MaKhoiLop " +
                " and TenLop like '" + text_lopcu + "' and TenNamHoc like '" + text_namhoccu + "' and TenKhoiLop like N'" + text_khoilopcu + "'";
            string query3 = "select distinct pl.MaHocSinh,HoHS + ' '+ TenHS As TenHS" +
                " from PhanLop pl,Lop l,HocSinh hs,khoilop kl,namhoc NH " +
                " where pl.MaLop=l.MaLop and pl.MaHocSinh = hs.MaHocSinh and pl.MaNamHoc = NH.MaNamHoc and pl.MaKhoiLop = kl.MaKhoiLop " +
                " and TenLop like '" + text_lopmoi + "' and TenNamHoc like '" + text_namhocmoi + "' and TenKhoiLop like N'" + text_khoilopmoi + "'";
            string query4 = "update HocSinh " +
                "set MaLop = (select MaLop from Lop where TenLop like N'" + text_lopmoi + "' and MaNamHoc= (select MaNamHoc from NamHoc where TenNamHoc like '" + text_namhocmoi + "' ))" +
                "where MaHocSinh like '" + mahs + "'";
            string query5 = "update Lop set SiSo = SiSo + 1" +
                        "  from lop l" +
                        " join phanlop pl on l.malop=pl.malop " +
                        " where l.malop = (select MaLop from Lop where TenLop like N'" + text_lopmoi + "' " +
                        " and MaNamHoc= (select MaNamHoc from NamHoc where TenNamHoc like '" + text_namhocmoi + "' ))";//tăng sĩ số lớp mới;
            string query6 = "update Lop set SiSo = SiSo - 1" +
                        "  from lop l" +
                        " join phanlop pl on l.malop=pl.malop " +
                        " where l.malop = (select MaLop from Lop where TenLop like N'" + text_lopcu + "' " +
                        " and MaNamHoc= (select MaNamHoc from NamHoc where TenNamHoc like '" + text_namhoccu + "' ))";//giảm sĩ số lớp cũ;

            if (ThucThi(query) == true)
            {
                ThucThi(query4);// đổi lớp học hiện tại của học sinh đó
                ThucThi(query5);//tăng sỉ số lớp mới
                ThucThi(query6);//giảm sỉ số lớp cũ
                MessageBox.Show("Chuyển thành công!", "Thông báo");
                XemDS(query2, data_phanlop_dsachcu);
                XemDS(query3, data_phanlop_dsachmoi);

            }
            else
            {
                MessageBox.Show("Chuyển thất bại", "Thông báo");
            }
        }
        private void pic_phanlop_trolai_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection row = data_phanlop_dsachmoi.SelectedRows;
            string mahs = row[0].Cells["MaHocSinh_moi"].Value.ToString();
            string text_namhoccu = cbb_phanlop_namhoc.Text;
            string text_khoilopcu = cbb_phanlop_khoilop.Text;
            string text_lopcu = cbb_phanlop_lop.Text;
            string text_namhocmoi = cbb_phanlop_nhoclopmoi.Text;
            string text_khoilopmoi = cbb_phanlop_khoilopmoi.Text;
            string text_lopmoi = cbb_phanlop_lopmoi.Text;
            string query = " update PhanLop set MaKhoiLop = (select MaKhoiLop from KhoiLop where TenKhoiLop like N'" + text_khoilopcu + "')," +
               "MaLop = (select MaLop from Lop where TenLop like N'" + text_lopcu + "' and MaNamHoc= (select MaNamHoc from NamHoc where TenNamHoc like '" + text_namhoccu + "' ))," +
               " MaNamHoc = (select MaNamHoc from NamHoc where TenNamHoc like '" + text_namhoccu + "' )" +
               " where MaHocSinh like '" + mahs + "'";
            string query2 = "select distinct pl.MaHocSinh,HoHS + ' '+ TenHS As TenHS" +
                " from PhanLop pl,Lop l,HocSinh hs,khoilop kl,namhoc NH " +
                " where pl.MaLop=l.MaLop and pl.MaHocSinh = hs.MaHocSinh and pl.MaNamHoc = NH.MaNamHoc and pl.MaKhoiLop = kl.MaKhoiLop " +
                " and TenLop like '" + text_lopcu + "' and TenNamHoc like '" + text_namhoccu + "' and TenKhoiLop like N'" + text_khoilopcu + "'";
            string query3 = "select distinct pl.MaHocSinh,HoHS + ' '+ TenHS As TenHS" +
                " from PhanLop pl,Lop l,HocSinh hs,khoilop kl,namhoc NH " +
                " where pl.MaLop=l.MaLop and pl.MaHocSinh = hs.MaHocSinh and pl.MaNamHoc = NH.MaNamHoc and pl.MaKhoiLop = kl.MaKhoiLop " +
                " and TenLop like '" + text_lopmoi + "' and TenNamHoc like '" + text_namhocmoi + "' and TenKhoiLop like N'" + text_khoilopmoi + "'";
            string query4 = "update HocSinh " +
                "set MaLop = (select MaLop from Lop where TenLop like N'" + text_lopcu + "' and MaNamHoc= (select MaNamHoc from NamHoc where TenNamHoc like '" + text_namhoccu + "' ))" +
                "where MaHocSinh like '" + mahs + "'";
            string query5 = "update Lop set SiSo = SiSo + 1" +
                        "  from lop l" +
                        " join phanlop pl on l.malop=pl.malop " +
                        " where l.malop = (select MaLop from Lop where TenLop like N'" + text_lopcu + "' " +
                        " and MaNamHoc= (select MaNamHoc from NamHoc where TenNamHoc like '" + text_namhoccu + "' ))";//tăng sĩ số lớp cũ
            string query6 = "update Lop set SiSo = SiSo - 1" +
                        "  from lop l" +
                        " join phanlop pl on l.malop=pl.malop " +
                        " where l.malop = (select MaLop from Lop where TenLop like N'" + text_lopmoi + "' " +
                        " and MaNamHoc= (select MaNamHoc from NamHoc where TenNamHoc like '" + text_namhocmoi + "' ))";//giảm sĩ số lớp mới

            if (ThucThi(query) == true)
            {
                ThucThi(query4);// đổi lớp học hiện tại của học sinh đó
                ThucThi(query5);//tăng sỉ số lớp cũ
                ThucThi(query6);//giảm sỉ số lớp mới
                MessageBox.Show("Trở về thành công");
                XemDS(query2, data_phanlop_dsachcu);
                XemDS(query3, data_phanlop_dsachmoi);
            }
            else
            {
                MessageBox.Show("Trở về thất bại");
            }
        }
        private void pic_phanlop_reset_Click(object sender, EventArgs e)
        {
            cbb_phanlop_khoilop.Text = "";
            cbb_phanlop_khoilopmoi.Text = "";
            cbb_phanlop_lop.Text = "";
            cbb_phanlop_lopmoi.Text = "";
            cbb_phanlop_namhoc.Text = "";
            cbb_phanlop_nhoclopmoi.Text = "";
        }
        //tab hoc sinh
        public string mahocsinh="";
        public string cccd = "",mataikhoan_hocsinh="";
        private void data_hocsinh_dshocisnh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (data_hocsinh_dshocisnh.SelectedRows.Count > 0)
            {
                DataGridViewRow row = data_hocsinh_dshocisnh.Rows[e.RowIndex];
                mahocsinh = row.Cells["MaHocSinh_hocsinh"].Value.ToString();
                txt_hocsinh_tenhsinh.Text = row.Cells["TenHocSinh"].Value.ToString();
                string gt = row.Cells["GioiTinh"].Value.ToString();
                if (gt=="Nam")
                {
                    rad_hocsinh_nam.Checked = true;
                }
                else
                {
                    rad_hocsinh_nu.Checked = true;
                }
                string d = row.Cells["NgaySinh"].Value.ToString();
                datetime_hocsinh_ngsinh.Text=d;
                txt_hocsinh_diachi.Text = row.Cells["DiaChi"].Value.ToString();
                cccd = row.Cells["CCCD"].Value.ToString();
                txt_hocsinh_cccd.Text = cccd;
                txt_hocsinh_sdt.Text = row.Cells["SDT"].Value.ToString();
                cbb_hocsinh_tenlop.Text = row.Cells["TenLop_HocSinh"].Value.ToString();
                mataikhoan_hocsinh = row.Cells["hocsinh_MaTaiKhoan"].Value.ToString();
            }
        }

        private void cbb_hocsinh_namhoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value_namhoc = cbb_hocsinh_namhoc.SelectedValue.ToString();
            string sSQL = "select MaHocSinh,HoHS + ' ' + TenHS TenHocSinh, GioiTinh,NgaySinh,DiaChi,CCCD,SDT,TenLop" +
                " from HocSinh HS,Lop L" +
                " where HS.MaLop=L.MaLop and L.MaNamHoc = '" + value_namhoc + "'";
            XemDS(sSQL, data_hocsinh_dshocisnh);
            string sql = "  select MaLop,TenLop\r\n  " +
                    "from Lop l ,NamHoc nh\r\n  " +
                    "where l.MaNamHoc = nh.MaNamHoc and nh.MaNamHoc = '" + value_namhoc + "'";
            XemDSCBX(sql, cbb_hocsinh_tenlop, "TenLop", "MaLop");
        }
        private void pic_them_hocsinh_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_hocsinh_cccd.Text) || string.IsNullOrEmpty(txt_hocsinh_sdt.Text) || 
                string.IsNullOrEmpty(txt_hocsinh_tenhsinh.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Thông báo");
            }
            else
            {
                string ten="", gioitinh="", ngaysinh, Diachi, cccd, sdt, namhoc, tenlop;
                string sql = "INSERT INTO TempLoaiTK Values ( '0', 'LTK004')";
                ThucThi(sql);
                string a; // ten
                string ho = "";
                string[] b;// mang chua ten
                a = txt_hocsinh_tenhsinh.Text;
                a = a.Trim();
                b = a.Split(' ');
                ten = b[b.Count() - 1];
                for (int i = 0; i < b.Count() - 1; i++)
                {
                    ho += (b[i] + " ");
                }

                if (rad_hocsinh_nam.Checked) { gioitinh = "Nam"; }
                else if (rad_hocsinh_nu.Checked) { gioitinh = "Nữ"; }
                ngaysinh = datetime_hocsinh_ngsinh.Value.ToString();
                Diachi = txt_hocsinh_diachi.Text;
                cccd = txt_hocsinh_cccd.Text;
                sdt = txt_hocsinh_sdt.Text;
                namhoc = cbb_hocsinh_namhoc.Text;
                tenlop = cbb_hocsinh_tenlop.SelectedValue.ToString();
                string sql2 = "insert into HocSinh (MaHocSinh,HoHS,TenHS,GioiTinh,NgaySinh,DiaChi,MaLop,CCCD,SDT) \r\n" +
                    "values ('0',N'"+ho+"',N'"+ten+"',N'"+gioitinh+"','"+ngaysinh+"',N'"+Diachi+"','"+tenlop+"','"+cccd+"','"+sdt+"')";
                string sql3 = "select MaHocSinh,HoHS + ' ' + TenHS TenHocSinh, GioiTinh,NgaySinh,DiaChi,CCCD,SDT,TenLop" +
                        " from HocSinh HS,Lop L" +
                        " where  hs.TrangThai = 1 and HS.MaLop=L.MaLop and L.MaNamHoc = '" + namhoc + "'";
            
                if (ThucThi(sql2)) { MessageBox.Show("Thêm thành công", "Thông báo"); XemDS(sql3, data_hocsinh_dshocisnh); }
                else { MessageBox.Show("Thêm thất bại", "Thông báo"); }
            }
           
        }
        private void pic_xoa_hocsinh_Click(object sender, EventArgs e)
        {
            
            string sql = "update HocSinh\r\n" +
                "set TrangThai = '0' \r\n" +
                "where MaHocSinh = '" + mahocsinh + "'";
            string sql2 = "select MaHocSinh,HoHS + ' ' + TenHS TenHocSinh, GioiTinh,NgaySinh,DiaChi,CCCD,SDT,TenLop" +
                    " from HocSinh HS,Lop L" +
                    " where  hs.TrangThai = 1 and HS.MaLop=L.MaLop and L.MaNamHoc = '" + cbb_hocsinh_namhoc.SelectedValue + "'";
            if (ThucThi(sql))
            { 
                MessageBox.Show("Xóa thành công");XemDS(sql2, data_hocsinh_dshocisnh);
                string sql3 = "update TaiKhoan\r\n\t" +
                "set TrangThai = '0' \r\n" +
                "where MaTaiKhoan = '" + mataikhoan_hocsinh + "'";
                ThucThi(sql3);
            }
            else 
            { 
                MessageBox.Show("Xóa thất bại"); 
            }
            
        }
        private void pic_luu_hocsinh_Click(object sender, EventArgs e)
        {
            string ten = "", gioitinh = "", ngaysinh, Diachi, cccd, sdt, namhoc, tenlop;
            string a; // ten
            string ho = "";
            string[] b;// mang chua ten
            a = txt_hocsinh_tenhsinh.Text;
            a = a.Trim();
            b = a.Split(' ');
            ten = b[b.Count() - 1];
            for (int i = 0; i < b.Count() - 1; i++)
            {
                ho += (b[i] + " ");
            }
            ho= ho.Trim();
            if (rad_hocsinh_nam.Checked) { gioitinh = "Nam"; }
            else if (rad_hocsinh_nu.Checked) { gioitinh = "Nữ"; }
            ngaysinh = datetime_hocsinh_ngsinh.Value.ToString();
            Diachi = txt_hocsinh_diachi.Text;
            cccd = txt_hocsinh_cccd.Text;
            sdt = txt_hocsinh_sdt.Text;
            namhoc = cbb_hocsinh_namhoc.Text;
            tenlop = cbb_hocsinh_tenlop.SelectedValue.ToString();

            string sql = "update HocSinh\r\n" +
                        "set HoHS = N'"+ho+"',\r\n\t" +
                        "TenHS = N'"+ten+"',\r\n\t" +
                        "GioiTinh = N'"+gioitinh+"',\r\n\t" +
                        "NgaySinh = N'"+ngaysinh+"',\r\n\t" +
                        "DiaChi = N'"+Diachi+"',\r\n\t" +
                        "MaLop = '"+tenlop+"',\r\n\t" +
                        "CCCD = '"+cccd+"',\r\n\t" +
                        "SDT = '"+sdt+"'\r\n" +
                        "where MaHocSinh = '"+mahocsinh+"'";
            string sql2 = "select MaHocSinh,HoHS + ' ' + TenHS TenHocSinh, GioiTinh,NgaySinh,DiaChi,CCCD,SDT,TenLop" +
                   " from HocSinh HS,Lop L" +
                   " where  hs.TrangThai = 1 and HS.MaLop=L.MaLop and L.MaNamHoc = '" + cbb_hocsinh_namhoc.SelectedValue.ToString() + "'";
            if (ThucThi(sql)) 
            { 
                MessageBox.Show("Lưu thành công", "Thông báo"); 
                XemDS(sql2, data_hocsinh_dshocisnh); 
            }
            else { MessageBox.Show("Lưu thất bại", "Thông báo"); }
        }
        private void pic_reset_hcosinh_Click(object sender, EventArgs e)
        {
            cbb_hocsinh_namhoc.Text = "";
            cbb_hocsinh_tenlop.Text = "";
            txt_hocsinh_cccd.Text = "";
            txt_hocsinh_diachi.Text = "";
            txt_hocsinh_sdt.Text = "";
            txt_hocsinh_tenhsinh.Text = "";
            rad_hocsinh_nam.Checked = true;
        }

        //tab tai khoan
        private void txt_taikhoan_tentaikhoan_TextChanged(object sender, EventArgs e)
        {
            string name = txt_taikhoan_tentaikhoan.Text;
            string query="Select * from TaiKhoan where Username like '%"+name+"%'";
            XemDS(query,data_taikhoan_dstaikhoan);
        }
        private void data_taikhoan_dstaikhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (data_taikhoan_dstaikhoan.SelectedRows.Count > 0)
            {
                DataGridViewSelectedRowCollection row = data_taikhoan_dstaikhoan.SelectedRows;
                txt_taikhoan_username.Text = row[0].Cells["Username"].Value.ToString();
                txt_taikhoan_password.Text = row[0].Cells["Password"].Value.ToString();
            }
        }
        private void pic_taikhoan_luu_Click(object sender, EventArgs e)
        {
            string name = txt_taikhoan_username.Text;
            string pass = txt_taikhoan_password.Text;
            string query = "  update TaiKhoan " +
                            "set Password = '"+pass+"' " +
                            "where Username like '"+name+"'";
            string query2 = "select * from TaiKhoan";
            if (ThucThi(query))
            {
                MessageBox.Show("Lưu thành công", "Thông báo");
                XemDS(query2, data_taikhoan_dstaikhoan);
            }
            else
            {
                MessageBox.Show("Lưu thất bại", "Thông báo");
            }
        }

        private void pic_taikhoan_xoa_Click(object sender, EventArgs e)
        {
            string name = txt_taikhoan_username.Text;
            string pass = txt_taikhoan_password.Text;
            string query = "delete from TaiKhoan " +
                            "where Username like '"+name+"' and Password like '"+pass+"'";
            string query2 = "select * from TaiKhoan";
            if (ThucThi(query))
            {
                MessageBox.Show("Xóa thành công", "Thông báo");
                XemDS(query2, data_taikhoan_dstaikhoan);
            }
            else
            {
                MessageBox.Show("Xóa thất bại", "Thông báo");
            }
        }
        public void NhapSoThuc(object sender, EventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            if (txtBox.Text != "")
            {
                int vt = txtBox.SelectionStart;
                double so;
                bool kq = double.TryParse(txtBox.Text, out so);
                if (!kq)
                {
                    txtBox.Text = txtBox.Text.Remove(vt - 1, 1);
                    txtBox.SelectionStart = vt - 1;
                }
            }
        }

        //Tab tra cuu diem
        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl2.SelectedTab == tab_tcdiem)
            {
                string sSQL = "select MaNamHoc,TenNamHoc from NamHoc";
                XemDSCBX(sSQL, cbb_diem_namhoc, "TenNamHoc", "MaNamHoc");
                string sSQL2 = $"Select MaLop,TenLop from Lop l where MaNamHoc = 'NH2223'";
                XemDSCBX(sSQL2, cbb_diem_lop, "TenLop", "MaLop");
                cbb_diem_lop.Text = "";
                string sSQL1 = "SELECT hs.MaHocSinh,  HoHs + ' ' + TenHS AS TenHocSinh,TenMonHoc, " +
                    " STRING_AGG( CASE WHEN ld.MaLoaiDiem = 'LD01' THEN Diem ELSE NULL END, ', ' ) AS Diem15Phut," +
                    " STRING_AGG( CASE WHEN ld.MaLoaiDiem = 'LD02' THEN Diem ELSE NULL END, ', '  ) AS Diem1Tiet," +
                    " STRING_AGG( CASE WHEN ld.MaLoaiDiem = 'LD03' THEN Diem ELSE NULL END, ', '  ) AS DiemThi " +
                    "  FROM  HocSinh hs INNER JOIN   Diem d ON hs.MaHocSinh = d.MaHocSinh" +
                    "   INNER JOIN  CotDiem cd ON d.MaCotDiem = cd.MaCotDiem" +
                    "  INNER JOIN    LoaiDiem ld ON cd.MaLoaiDiem = ld.MaLoaiDiem " +
                    "  INNER JOIN MonHoc mh ON mh.MaMonHoc = d.MaMonHoc" +
                    "  where d.MaMonHoc = 'MH001'" +
                    " GROUP BY hs.MaHocSinh, HoHs + ' ' + TenHS,TenMonHoc;";
                XemDS(sSQL1, data_diem_dsdiem);
            }
            else if (tabControl2.SelectedTab == tab_themdiem)
            {
                string sSQL = "select MaNamHoc,TenNamHoc from NamHoc";
                XemDSCBX(sSQL, cbb_Themdiem_NH, "TenNamHoc", "MaNamHoc");
                string sSQL2 = $"Select MaLop,TenLop from Lop l where MaNamHoc = 'NH2223'";
                XemDSCBX(sSQL2, cbb_Themdiem_TenLop, "TenLop", "MaLop");
                string ssQL3 = "Select MaLoaiDiem,TenLoaiDiem from LoaiDiem";
                XemDSCBX(ssQL3, cbb_themdiem_loaidiem, "TenLoaiDiem", "MaLoaiDiem");
                cbb_Themdiem_TenLop.Text = "";
                string sSQL1 = "SELECT hs.MaHocSinh,  HoHs + ' ' + TenHS AS TenHocSinh,TenMonHoc, " +
                    " STRING_AGG( CASE WHEN ld.MaLoaiDiem = 'LD01' THEN Diem ELSE NULL END, ', ' ) AS Diem15Phut," +
                    " STRING_AGG( CASE WHEN ld.MaLoaiDiem = 'LD02' THEN Diem ELSE NULL END, ', '  ) AS Diem1Tiet," +
                    " STRING_AGG( CASE WHEN ld.MaLoaiDiem = 'LD03' THEN Diem ELSE NULL END, ', '  ) AS DiemThi " +
                    "  FROM  HocSinh hs INNER JOIN   Diem d ON hs.MaHocSinh = d.MaHocSinh" +
                    "   INNER JOIN  CotDiem cd ON d.MaCotDiem = cd.MaCotDiem" +
                    "  INNER JOIN    LoaiDiem ld ON cd.MaLoaiDiem = ld.MaLoaiDiem " +
                    "  INNER JOIN MonHoc mh ON mh.MaMonHoc = d.MaMonHoc" +
                    "  where d.MaMonHoc = 'MH001'" +
                    " GROUP BY hs.MaHocSinh, HoHs + ' ' + TenHS,TenMonHoc;";
                XemDS(sSQL1, data_themdiem_dsdiem);
            }
        }
        private void cbb_diem_namhoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value_namhoc = cbb_diem_namhoc.SelectedValue.ToString();
            string sSQL = "Select MaHocKi,TenHocKi from HocKi";
            XemDSCBX(sSQL, cbb_diem_hocky, "TenHocKi", "MaHocKi");
            string sSQL2 = $"Select MaLop,TenLop from Lop l where MaNamHoc = '{value_namhoc}' and L.TrangThai='1'";
            XemDSCBX(sSQL2, cbb_diem_lop, "TenLop", "MaLop");
            string sSQL1 = "Select MaMonHoc,TenMonHoc from MonHoc";
            XemDSCBX(sSQL1, cbb_diem_monhoc, "TenMonHoc", "MaMonHoc");
            cbb_diem_hocky.Text = "";
            cbb_diem_lop.Text = "";
            cbb_diem_monhoc.Text = "";
        }

    

        private void data_diem_dsdiem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (data_diem_dsdiem.SelectedRows.Count > 0)
            {
                string diem15phut, diem1tiet;
                DataGridViewRow row = data_diem_dsdiem.Rows[e.RowIndex];
                txt_diem_mahocsinh.Text = row.Cells["MaHocSinh_Diem"].Value.ToString();
                txt_diem_hovaten.Text = row.Cells["TenHocSinh_Diem"].Value.ToString();
                diem15phut = row.Cells["Diem15phut"].Value.ToString();
                diem1tiet = row.Cells["Diem1Tiet"].Value.ToString();
                txt_diem_diemthi.Text = row.Cells["DiemThi"].Value.ToString();

                string[] mangdiem15phut = diem15phut.Split(',');
                string[] mangdiem1tiet = diem1tiet.Split(',');
                if (mangdiem15phut.Count() == 0)
                {
                    txt_diem_d15p.Text = "";
                    txt_diem_d15pL2.Text = "";
                    txt_diem_d15pL3.Text = "";
                }
                else
                {
                    if (mangdiem15phut.Count() == 1)
                    {
                        txt_diem_d15p.Text = mangdiem15phut[0];
                        txt_diem_d15pL2.Text = "";
                        txt_diem_d15pL3.Text = "";
                    }
                    if (mangdiem15phut.Count() >= 2)
                    {
                        txt_diem_d15p.Text = mangdiem15phut[0];
                        txt_diem_d15pL2.Text = mangdiem15phut[1];
                        txt_diem_d15pL3.Text = "";
                    }
                    if (mangdiem15phut.Count() == 3)
                    {
                        txt_diem_d15p.Text = mangdiem15phut[0];
                        txt_diem_d15pL2.Text = mangdiem15phut[1];
                        txt_diem_d15pL3.Text = mangdiem15phut[2];
                    }
                }
                if (mangdiem1tiet.Count() == 0)
                {
                    txt_diem_d1tiet.Text = "";
                    txt_diem_d1tietL2.Text = "";
                }
                else
                {
                    if (mangdiem1tiet.Count() == 1)
                    {
                        txt_diem_d1tiet.Text = mangdiem1tiet[0];
                        txt_diem_d1tietL2.Text = "";
                    }
                    else if (mangdiem1tiet.Count() == 2)
                    {
                        txt_diem_d1tiet.Text = mangdiem1tiet[0];
                        txt_diem_d1tietL2.Text = mangdiem1tiet[1];
                    }
                }
            }
        }

        private void pic_diem_hienthi_Click(object sender, EventArgs e)
        {
            string value_manamhoc = cbb_diem_namhoc.SelectedValue.ToString(), text_tennamhoc = cbb_diem_namhoc.Text;
            string value_mamonhoc = cbb_diem_monhoc.SelectedValue.ToString(), text_tenmonhoc = cbb_diem_monhoc.Text;
            string value_hocky = cbb_diem_hocky.SelectedValue.ToString(), text_hocky = cbb_diem_hocky.Text;
            string value_lop = cbb_diem_lop.SelectedValue.ToString(), text_lop = cbb_diem_hocky.Text;
            string dieukien_namhoc = "", dieukien_monhoc = "", dieukien_hocky = "", dieukien_lop = "";
            if (text_tennamhoc != "")
            {
                dieukien_namhoc = $" where d.MaNamHoc = '{value_manamhoc}' ";
            }
            if (text_tenmonhoc != "")
            {
                dieukien_monhoc = $" and d.MaMonHoc = '{value_mamonhoc}' ";
            }
            if (text_hocky != "")
            {
                dieukien_hocky = $" and MaHocKi = '{value_hocky}' ";
            }
            if (text_lop != "")
            {
                dieukien_lop = $" and d.MaLop = '{value_lop}'";
            }


            string sSQL1 = "SELECT hs.MaHocSinh,  HoHs + ' ' + TenHS AS TenHocSinh,TenMonHoc, " +
                    " STRING_AGG( CASE WHEN ld.MaLoaiDiem = 'LD01' THEN Diem ELSE NULL END, ', ' ) AS Diem15Phut," +
                    " STRING_AGG( CASE WHEN ld.MaLoaiDiem = 'LD02' THEN Diem ELSE NULL END, ', '  ) AS Diem1Tiet," +
                    " STRING_AGG( CASE WHEN ld.MaLoaiDiem = 'LD03' THEN Diem ELSE NULL END, ', '  ) AS DiemThi " +
                    "  FROM  HocSinh hs INNER JOIN   Diem d ON hs.MaHocSinh = d.MaHocSinh" +
                    "   INNER JOIN  CotDiem cd ON d.MaCotDiem = cd.MaCotDiem" +
                    "  INNER JOIN    LoaiDiem ld ON cd.MaLoaiDiem = ld.MaLoaiDiem " +
                    "  INNER JOIN MonHoc mh ON mh.MaMonHoc = d.MaMonHoc" +
                    $" {dieukien_namhoc} {dieukien_hocky} {dieukien_lop} {dieukien_monhoc} " +
                    " GROUP BY hs.MaHocSinh, HoHs + ' ' + TenHS,TenMonHoc;";
            XemDS(sSQL1, data_diem_dsdiem);
        }

        private void pic_diem_reset_Click(object sender, EventArgs e)
        {
            //combobox
            cbb_diem_namhoc.Text = "";
            cbb_diem_hocky.Text = "";
            cbb_diem_lop.Text = "";
            cbb_diem_monhoc.Text = "";
            //textbox
            txt_diem_d15p.Text = "";
            txt_diem_d15pL2.Text = "";
            txt_diem_d15pL3.Text = "";
            txt_diem_d1tiet.Text = "";
            txt_diem_d1tietL2.Text = "";
            txt_diem_diemthi.Text = "";
            txt_diem_hovaten.Text = "";
            txt_diem_mahocsinh.Text = "";
            data_diem_dsdiem.ClearSelection();
        }
        private void pic_diem_luu_Click(object sender, EventArgs e)
        {
            string value_mahocsinh = txt_diem_mahocsinh.Text;
            string txt_tenhocsinh = txt_diem_hovaten.Text;
            string loi = "";
            float diem15p, diem15pL2, diem15pL3, diem1tiet, diem1tietL2, diemthi;
            diem15p = float.Parse(txt_diem_d15p.Text);
            diem15pL2 = float.Parse(txt_diem_d15pL2.Text);
            diem15pL3 = float.Parse(txt_diem_d15pL3.Text);
            diem1tiet = float.Parse(txt_diem_d1tiet.Text);
            diem1tietL2 = float.Parse(txt_diem_d1tietL2.Text);
            diemthi = float.Parse(txt_diem_diemthi.Text);
            if (string.IsNullOrEmpty(txt_tenhocsinh) || string.IsNullOrEmpty(value_mahocsinh))
            {
                loi += "Kiểm tra lại thông tin mã học sinh và tên học sinh\n";
            }
            if (diem15p > 10 || diem15p < 0)
                loi += "Kiểm tra lại thông tin điểm 15 phút lần 1\n";
            if (diem15pL2 > 10 || diem15pL2 < 0)
                loi += "Kiểm tra lại thông tin điểm 15 phút lần 2\n";
            if (diem15pL3 > 10 || diem15pL3 < 0)
                loi += "Kiểm tra lại thông tin điểm 15 phút lần 3\n";
            if (diem1tiet > 10 || diem1tiet < 0)
                loi += "Kiểm tra lại thông tin điểm 1 tiết lần 1\n";
            if (diem1tietL2 > 10 || diem1tietL2 < 0)
                loi += "Kiểm tra lại thông tin điểm 1 tiết lần 2\n";
            if (diemthi > 10 || diemthi < 0)
                loi += "Kiểm tra lại thông tin điểm thi\n";
            if (loi == "")
            {
                string sQuery = "update set ";
            }
        }
        public bool checkdiem(string a)
        {
            float diem = float.Parse(a);
            if (diem < 0 || diem > 10)
            {
                return false;
            }
            return true;
        }
        //tab them diem
        private void pic_ThemDiem_Reset_Click_1(object sender, EventArgs e)
        {
            //reset them diem
            cbb_Themdiem_HK.Text = "";
            cbb_Themdiem_MH.Text = "";
            cbb_Themdiem_NH.Text = "";
            cbb_Themdiem_TenLop.Text = "";

            txt_themdiem_hoten.Text = "";
            txt_themdiem_diem.Text = "";
            txt_themdiem_mahs.Text = "";
            cbb_themdiem_loaidiem.Text = "";
            data_themdiem_dsdiem.ClearSelection();
        }
        private void cbb_Themdiem_NH_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value_namhoc = cbb_Themdiem_NH.SelectedValue.ToString();
            string sSQL = "Select MaHocKi,TenHocKi from HocKi";
            XemDSCBX(sSQL, cbb_Themdiem_HK, "TenHocKi", "MaHocKi");
            string sSQL2 = $"Select MaLop,TenLop from Lop l where MaNamHoc = '{value_namhoc}'";
            XemDSCBX(sSQL2, cbb_Themdiem_TenLop, "TenLop", "MaLop");
            string sSQL1 = "Select MaMonHoc,TenMonHoc from MonHoc";
            XemDSCBX(sSQL1, cbb_Themdiem_MH, "TenMonHoc", "MaMonHoc");
            cbb_Themdiem_HK.Text = "";
            cbb_Themdiem_TenLop.Text = "";
            cbb_Themdiem_MH.Text = "";
        }
        private void pic_themdiem_hienthi_Click(object sender, EventArgs e)
        {
            string value_manamhoc = cbb_Themdiem_NH.SelectedValue.ToString(), text_tennamhoc = cbb_Themdiem_NH.Text;
            string value_mamonhoc = cbb_Themdiem_MH.SelectedValue.ToString(), text_tenmonhoc = cbb_Themdiem_MH.Text;
            string value_hocky = cbb_Themdiem_HK.SelectedValue.ToString(), text_hocky = cbb_Themdiem_HK.Text;
            string value_lop = cbb_Themdiem_TenLop.SelectedValue.ToString(), text_lop = cbb_Themdiem_TenLop.Text;
            string dieukien_namhoc = "", dieukien_monhoc = "", dieukien_hocky = "", dieukien_lop = "";
            if (text_tennamhoc != "")
            {
                dieukien_namhoc = $" where d.MaNamHoc = '{value_manamhoc}' ";
            }
            if (text_tenmonhoc != "")
            {
                dieukien_monhoc = $" and d.MaMonHoc = '{value_mamonhoc}' ";
            }
            if (text_hocky != "")
            {
                dieukien_hocky = $" and MaHocKi = '{value_hocky}' ";
            }
            if (text_lop != "")
            {
                dieukien_lop = $" and d.MaLop = '{value_lop}'";
            }


            string sSQL1 = "SELECT hs.MaHocSinh,  HoHs + ' ' + TenHS AS TenHocSinh,TenMonHoc, " +
                    " STRING_AGG( CASE WHEN ld.MaLoaiDiem = 'LD01' THEN Diem ELSE NULL END, ', ' ) AS Diem15Phut," +
                    " STRING_AGG( CASE WHEN ld.MaLoaiDiem = 'LD02' THEN Diem ELSE NULL END, ', '  ) AS Diem1Tiet," +
                    " STRING_AGG( CASE WHEN ld.MaLoaiDiem = 'LD03' THEN Diem ELSE NULL END, ', '  ) AS DiemThi " +
                    "  FROM  HocSinh hs INNER JOIN   Diem d ON hs.MaHocSinh = d.MaHocSinh" +
                    "   INNER JOIN  CotDiem cd ON d.MaCotDiem = cd.MaCotDiem" +
                    "  INNER JOIN    LoaiDiem ld ON cd.MaLoaiDiem = ld.MaLoaiDiem " +
                    "  INNER JOIN MonHoc mh ON mh.MaMonHoc = d.MaMonHoc" +
                    $" {dieukien_namhoc} {dieukien_hocky} {dieukien_lop} {dieukien_monhoc} " +
                    " GROUP BY hs.MaHocSinh, HoHs + ' ' + TenHS,TenMonHoc;";
            XemDS(sSQL1, data_themdiem_dsdiem);
        }
        //Tab tra cứu
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tab_tracuu_hocsinh)
            {
                string query4 = " select hs.MaHocSinh,hs.HoHS + hs.TenHS as HoTenHocSinh,GioiTinh,NgaySinh,DiaChi,l.TenLop,hs.TrangThai " +
                    " from Lop l,HocSinh hs where l.MaLop = hs.MaLop ";
                XemDS(query4, data_tracuu_hocsinh_dstracuuhs);

            }
            else if (tabControl1.SelectedTab == tab_tracuu_giaovien)
            {
                string query1 = " select MaGiaoVien,TenGV,GioiTinh,TenMonHoc,DiaChi,Sdt,CCCD," +
                    " case ChucVu when '1' then N'Ban giám hiệu' when '2' then N'Giáo vụ' else N'Giáo viên' end as ChucVu,gv.TrangThai " +
                    "  from GiaoVien gv, MonHoc mh " +
                    " where gv.MaMonHoc = mh.MaMonHoc";
                XemDS(query1, data_tracuu_giaovien_dstracuugv);
            }
        }

        private void txt_tracuu_hocsinh_mahocsinh_TextChanged(object sender, EventArgs e)
        {
            string tracuu=txt_tracuu_hocsinh_mahocsinh.Text;
            string query = "   select distinct hs.MaHocSinh,hs.HoHS +' '+ hs.TenHS as HoTenHocSinh,GioiTinh,NgaySinh,DiaChi,l.TenLop,hs.TrangThai from Lop l,HocSinh hs where l.MaLop = hs.MaLop and( MaHocSinh like '%"+tracuu+ "%'  or  hs.TenHS like N'%"+tracuu+"%')";
            XemDS(query, data_tracuu_hocsinh_dstracuuhs);

        }

        private void txt_tracuu_giaovien_magv_TextChanged(object sender, EventArgs e)
        {
            string tracuu = txt_tracuu_giaovien_magv.Text;
            string query = " select distinct MaGiaoVien,TenGV,GioiTinh,TenMonHoc,DiaChi,Sdt,CCCD,case ChucVu when '1' then N'Ban giám hiệu' when '2' then N'Giáo vụ' else N'Giáo viên' end as ChucVu,gv.TrangThai " +
                "from GiaoVien gv, MonHoc mh " +
                "where gv.MaMonHoc = mh.MaMonHoc and (MaGiaoVien like '%"+tracuu+"%' or TenGV like N'%"+tracuu+"%')";
            XemDS(query, data_tracuu_giaovien_dstracuugv);
        }

        //Tab giao vien
        string magv,matk_gv;

        private void pic_giaovien_reset_Click(object sender, EventArgs e)
        {
            txt_giaovien_namegv.Text = "";
            txt_giaovien_address.Text = "";
            txt_giaovien_cccd.Text = "";
            txt_giaovien_sdt.Text = "";
            rad_giaovien_nam.Checked = true;
            rad_giaovien_giaovien.Checked = true;
            DateTime t = DateTime.Now;
            date_giaovien_ngaysinh.Value = t;
            cbb_giaovien_monhoc.Text = "";
        }
        private void data_giaovien_dsgiaovien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (data_giaovien_dsgiaovien.SelectedRows.Count > 0)
            {
                string trangthai = "";
                DataGridViewRow row = data_giaovien_dsgiaovien.Rows[e.RowIndex];
                magv = row.Cells["MaGiaoVien"].Value.ToString();
                matk_gv = row.Cells["MaTaiKhoan_gv"].Value.ToString();
                trangthai = row.Cells["TrangThai_gv"].Value.ToString();
                txt_giaovien_namegv.Text = row.Cells["giaovien_TenGV"].Value.ToString();
                string gioitinh = row.Cells["giaovien_GioiTinh"].Value.ToString();
                if(trangthai == "Hoạt Động")
                {
                    rad_giaovien_hoatdong.Checked = true;
                }
                else
                {
                    rad_giaovien_khonghoatdong.Checked = true;
                }
                if (gioitinh == "Nam")
                {
                    rad_giaovien_nam.Checked = true;
                }
                else
                {
                    rad_giaovien_nu.Checked = true;
                }
                string chucvu = row.Cells["Chucvu"].Value.ToString();
                if (chucvu == "Ban giám hiệu")
                {
                    rad_giaovien_bangiamhieu.Checked = true;
                }
                if(chucvu == "Giáo vụ")
                {
                    rad_giaovien_giaovu.Checked = true;
                }
                if (chucvu == "Giáo viên")
                {
                    rad_giaovien_giaovien.Checked = true;
                }

                txt_giaovien_cccd.Text = row.Cells["giaovien_CCCD"].Value.ToString();
                txt_giaovien_sdt.Text = row.Cells["giaovien_SDT"].Value.ToString();
                cbb_giaovien_monhoc.Text = row.Cells["giaovien_TenMonHoc"].Value.ToString();
                string t = row.Cells["giaovien_NgaySinh"].Value.ToString();
                date_giaovien_ngaysinh.Text = t;
                txt_giaovien_address.Text = row.Cells["giaovien_DiaChi"].Value.ToString();
            }
        }
        private void pic_giaovien_them_Click(object sender, EventArgs e)// Gián đoạn
        {
            string query;
            string tengv, gioitinh="Nam", chucvu="1", cccd, sdt, mamonhoc, ngaysinh, diachi;
            string query3 = "insert into TempLoaiTK values ('0','LTK001')";

            if (rad_giaovien_nu.Checked == true)
            {
                gioitinh = "Nữ";
            }
            if (rad_giaovien_giaovien.Checked)
            {
                chucvu = "3";
                query3 = "insert into TempLoaiTK values ('0','LTK003')";
            }
            if (rad_giaovien_giaovu.Checked)
            {
                chucvu = "2";
                query3 = "insert into TempLoaiTK values ('0','LTK002')";
            }
            tengv = txt_giaovien_namegv.Text;
            cccd = txt_giaovien_cccd.Text;
            sdt = txt_giaovien_sdt.Text;
            mamonhoc = cbb_giaovien_monhoc.SelectedValue.ToString();
            ngaysinh = date_giaovien_ngaysinh.Value.ToString("yyyy-MM-dd");
            diachi = txt_giaovien_address.Text;
            
            
            //them vào bảng ...
            query = $"insert into GiaoVien(MaGiaoVien, TenGV, GioiTinh, NgaySinh, DiaChi, Sdt, CCCD, ChucVu, MaMonHoc) " +
                $" values ('0',N'{tengv}',N'{gioitinh}','{ngaysinh}'," +
                $" N'{diachi}','{sdt}','{cccd}','{chucvu}','{mamonhoc}')";
            string query2 = " select distinct MaGiaoVien,TenGV,GioiTinh," +
                    "case ChucVu when '1' then N'Ban giám hiệu'" +
                    " when '2' then N'Giáo vụ' " +
                    "Else N'Giáo viên' end as Chucvu,CCCD,SDT,mh.TenMonHoc,NgaySinh,DiaChi,MaTaiKhoan,case gv.TrangThai " +
                    " when '1' then N'Hoạt Động' else N'Không Hoạt Động' end as TrangThai " +
                    "from GiaoVien gv,MonHoc mh " +
                    "where gv.MaMonHoc = mh.MaMonHoc ";
            ThucThi(query3);
            if (ThucThi(query))
            {
                MessageBox.Show("Thêm Giáo viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                XemDS(query2,data_giaovien_dsgiaovien);
            }
            else
            {
                MessageBox.Show("Thêm giáo viên thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void pic_giaovien_luu_Click(object sender, EventArgs e)
        {
            string query;
            string tengv, gioitinh="Nam", chucvu="1", cccd, sdt, mamonhoc, ngaysinh, diachi,trangthai="0";
            if (rad_giaovien_hoatdong.Checked)
            {
                trangthai = "1";
            }
            if (rad_giaovien_nu.Checked)
            {
                gioitinh = "Nữ";
            }
            if (rad_giaovien_giaovien.Checked)
            {
                chucvu = "3";
            }
            if (rad_giaovien_giaovu.Checked)
            {
                chucvu = "2";
            }
            tengv = txt_giaovien_namegv.Text;
            cccd = txt_giaovien_cccd.Text;
            sdt = txt_giaovien_sdt.Text;
            mamonhoc = cbb_giaovien_monhoc.SelectedValue.ToString();
            ngaysinh = date_giaovien_ngaysinh.Value.ToString("yyyy-MM-dd");
            diachi = txt_giaovien_address.Text;
            string query2 = " select distinct MaGiaoVien,TenGV,GioiTinh," +
                    "case ChucVu when '1' then N'Ban giám hiệu'" +
                    " when '2' then N'Giáo vụ' " +
                    "Else N'Giáo viên' end as Chucvu,CCCD,SDT,mh.TenMonHoc,NgaySinh,DiaChi,MaTaiKhoan,case gv.TrangThai " +
                    " when '1' then N'Hoạt Động' else N'Không Hoạt Động' end as TrangThai " +
                    "from GiaoVien gv,MonHoc mh " +
                    "where gv.MaMonHoc = mh.MaMonHoc ";

            query = "  update GiaoVien set TenGV = N'"+tengv+"',GioiTinh = N'"+gioitinh+"'," +
                "NgaySinh = '"+ngaysinh+"',DiaChi = N'"+diachi+"',sdt = '"+sdt+"',CCCD = '"+cccd+"',ChucVu = "+chucvu+"," +
                "MaMonHoc = '"+mamonhoc+"',TrangThai='"+trangthai+"' where  MaGiaoVien like '" + magv+"'";
            string query3 = "Update TaiKhoan set TrangThai = '" + trangthai + "' where MaTaiKhoan ='"+matk_gv+"'";
            if (ThucThi(query))
            {
                ThucThi(query3);
                MessageBox.Show("Lưu thành công", "Thông báo");
                XemDS(query2, data_giaovien_dsgiaovien);
            }
            else
            {
                MessageBox.Show("Lưu thất bại", "Thông báo");
            }

        }

        private void pic_giaovien_xoa_Click(object sender, EventArgs e)
        {
            string query = "  update GiaoVien set TrangThai = 0 " +
                " where MaGiaoVien like '"+magv+"'";
            string query3 = "  update TaiKhoan  set TrangThai = 0  " +
                "where MaTaiKhoan = '"+matk_gv+"'";
            string query2 = " select distinct MaGiaoVien,TenGV,GioiTinh," +
                    "case ChucVu when '1' then N'Ban giám hiệu'" +
                    " when '2' then N'Giáo vụ' " +
                    "Else N'Giáo viên' end as Chucvu,CCCD,SDT,mh.TenMonHoc,NgaySinh,DiaChi,MaTaiKhoan,case gv.TrangThai " +
                    " when '1' then N'Hoạt Động' else N'Không Hoạt Động' end as TrangThai " +
                    "from GiaoVien gv,MonHoc mh " +
                    "where gv.MaMonHoc = mh.MaMonHoc ";
            if (ThucThi(query))
            {
                ThucThi(query3);
                MessageBox.Show("Xóa thành công", "Thông báo");
                XemDS(query2, data_giaovien_dsgiaovien);
            }
            else
            {
                MessageBox.Show("Xóa thất bại", "Thông báo");
            }
        }

        //tab Phân Công
        private void cbb_phancong_monhoc_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string text_monhoc = cbb_phancong_monhoc.Text;
            string text_tenlop = cbb_phancong_lop.Text;
            string query = "select distinct TenGV,GV.MaGiaoVien from PhanCong PC,NamHoc NH,Lop L,GiaoVien GV,MonHoc MH " +
                           "where PC.MaGiaoVien=GV.MaGiaoVien and PC.MaLop=L.MaLop " +
                           " and PC.MaMonHoc=MH.MaMonHoc and PC.MaNamHoc=NH.MaNamHoc and TenMonHoc like N'" + text_monhoc + "' and GV.TrangThai = '1'" +
                           " and PC.MaGiaoVien not in (select PC.MaGiaoVien from PhanCong PC,NamHoc NH,Lop L,GiaoVien GV,MonHoc MH " +
                                                 " where PC.MaGiaoVien=GV.MaGiaoVien and PC.MaLop=L.MaLop and PC.MaMonHoc=MH.MaMonHoc " +
                                                 " and PC.MaNamHoc=NH.MaNamHoc and TenMonHoc like N'" + text_monhoc + "' and GV.TrangThai = '1' and TenLop like '" + text_tenlop + "')";
            cbb_phancong_giaovien.Text = "";
            XemDSCBX(query, cbb_phancong_giaovien, "TenGV", "GV.MaGiaoVien");
        }

        

        private void cbb_phancong_lop_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string query = "Select TenMonHoc,MaMonHoc from MonHoc";
   
            XemDSCBX(query, cbb_phancong_monhoc, "TenMonHoc", "MaMonHoc");
        }

        private void cbb_phancong_namhoc_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string text_namhoc = cbb_phancong_namhoc.Text;
            string query1 = "select TenLop,MaLop from Lop l , NamHoc nh " +
                " where l.MaNamHoc=nh.MaNamHoc and TenNamHoc like '" + text_namhoc + "'";
         
            XemDSCBX(query1, cbb_phancong_lop, "TenLop", "MaLop");
        }

        private void pic_phancong_them_Click(object sender, EventArgs e)
        {

        }

        //tab Diem Danh
        public string mahs_dd = "";
        private void data_diemdanh_dsdiemdanh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (data_diemdanh_dsdiemdanh.SelectedRows.Count > 0)
            {
                //năm học
                //học kỳ
                //khối lớp
                //lớp
                //tenhocsinh
                //ngayphep
                //ngaykhongphep
                DataGridViewRow row = data_diemdanh_dsdiemdanh.Rows[e.RowIndex];
                mahs_dd = row.Cells["MaHocSinh_DiemDanh"].Value.ToString();
                cbb_diemdanh_namhoc.Text = row.Cells["TenNamHoc_DiemDanh"].Value.ToString();
                cbb_diemdanh_hocky.Text = row.Cells["TenHocKi_DiemDanh"].Value.ToString();
                cbb_diemdanh_khoilop.Text = row.Cells["TenKhoiLop_DiemDanh"].Value.ToString();
                cbb_diemdanh_tenlop.Text = row.Cells["TenLop_DiemDanh"].Value.ToString();
                txt_diemdanh_tenhocsinh.Text = row.Cells["HoTenHS_DiemDanh"].Value.ToString();
                diemdanh_cophep.Value = int.Parse(row.Cells["NgayPhep"].Value.ToString());
                diemdanh_khongphep.Value = int.Parse(row.Cells["NgayKhongPhep"].Value.ToString());
            }
        }
        private void pic_diemdanh_timkiem_Click(object sender, EventArgs e)
        {
            string sql = "select  DD.MaHocSinh,HoHS + ' ' +TenHS As HoTenHS,TenLop,TenKhoiLop, TenHocKi,TenNamHoc,NgayPhep,NgayKhongPhep\r\n " +
                "from DiemDanh DD,HocSinh HS, Lop L, HocKi HK, NamHoc NH,KhoiLop KL\r\n  " +
                "where  DD.MaHocKi=HK.MaHocKi and DD.MaHocSinh=HS.MaHocSinh and DD.MaLop = L.MaLop \r\n " +
                " and DD.MaNamHoc=NH.MaNamHoc and KL.MaKhoiLop = L.MaKhoiLop and l.TenLop = '"+cbb_diemdanh_tenlop.Text+"'";
            XemDS(sql, data_diemdanh_dsdiemdanh);
        }
        
        private void cbb_diemdanh_khoilop_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql4 = "  select l.MaLop,l.TenLop\r\n  " +
                "from Lop l,KhoiLop kl, NamHoc nh\r\n  " +
                "where l.MaNamHoc = nh.MaNamHoc and l.MaKhoiLop = kl.MaKhoiLop and nh.TenNamHoc = '"+cbb_diemdanh_namhoc.Text+"' and kl.TenKhoiLop = N'"+cbb_diemdanh_khoilop.Text+"'";
            XemDSCBX(sql4, cbb_diemdanh_tenlop, "TenLop", "MaLop");
        }
        private void pic_diemdanh_luu_Click(object sender, EventArgs e)
        {
            
            int cophep = int.Parse(diemdanh_cophep.Value.ToString());
            int khongphep = int.Parse(diemdanh_khongphep.Value.ToString());
            string sql = " Update DiemDanh \r\n    " +
                "set NgayPhep = '"+cophep+"',\r\n    " +
                "NgayKhongPhep = '"+khongphep+"'\r\n\t" +
                "from HocKi hk\r\n    " +
                "where  DiemDanh.MaHocKi = hk.MaHocKi and MaHocSinh = '"+mahs_dd+"' and hk.TenHocKi = N'"+cbb_diemdanh_hocky.Text+"'";
            
            string query = "select  DD.MaHocSinh,HoHS + ' ' +TenHS As HoTenHS,TenLop,TenKhoiLop, TenHocKi,TenNamHoc,NgayPhep,NgayKhongPhep" +
                    " from DiemDanh DD,HocSinh HS, Lop L, HocKi HK, NamHoc NH,KhoiLop KL" +
                    " where DD.MaHocKi=HK.MaHocKi and DD.MaHocSinh=HS.MaHocSinh and DD.MaLop = L.MaLop " +
                    " and DD.MaNamHoc=NH.MaNamHoc and KL.MaKhoiLop = L.MaKhoiLop";
            
            if (ThucThi(sql))
            {
                MessageBox.Show("Lưu thành công", "Thông báo");
                XemDS(query, data_diemdanh_dsdiemdanh);
            }
            else
            {
                MessageBox.Show("Lưu thất bại", "Thông báo");
            }
        }

        //Long cu đen ngày mai đi học sớm nha
    }
}
