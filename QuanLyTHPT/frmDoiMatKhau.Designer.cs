namespace QuanLyTHPT
{
    partial class frmDoiMatKhau
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grb_Doimatkhau = new System.Windows.Forms.GroupBox();
            this.chk_hienmatkhau = new System.Windows.Forms.CheckBox();
            this.txt_Xacnhanmatkhau = new System.Windows.Forms.TextBox();
            this.lbl_Xacnhanmatkhau = new System.Windows.Forms.Label();
            this.btn_xacnhan = new System.Windows.Forms.Button();
            this.btn_trove = new System.Windows.Forms.Button();
            this.txt_MatKhauCu = new System.Windows.Forms.TextBox();
            this.txt_MatKhauMoi = new System.Windows.Forms.TextBox();
            this.lbl_Matkhaumoi = new System.Windows.Forms.Label();
            this.lbl_Matkhaucu = new System.Windows.Forms.Label();
            this.grb_Doimatkhau.SuspendLayout();
            this.SuspendLayout();
            // 
            // grb_Doimatkhau
            // 
            this.grb_Doimatkhau.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(223)))), ((int)(((byte)(230)))));
            this.grb_Doimatkhau.Controls.Add(this.chk_hienmatkhau);
            this.grb_Doimatkhau.Controls.Add(this.txt_Xacnhanmatkhau);
            this.grb_Doimatkhau.Controls.Add(this.lbl_Xacnhanmatkhau);
            this.grb_Doimatkhau.Controls.Add(this.btn_xacnhan);
            this.grb_Doimatkhau.Controls.Add(this.btn_trove);
            this.grb_Doimatkhau.Controls.Add(this.txt_MatKhauCu);
            this.grb_Doimatkhau.Controls.Add(this.txt_MatKhauMoi);
            this.grb_Doimatkhau.Controls.Add(this.lbl_Matkhaumoi);
            this.grb_Doimatkhau.Controls.Add(this.lbl_Matkhaucu);
            this.grb_Doimatkhau.Location = new System.Drawing.Point(31, 14);
            this.grb_Doimatkhau.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grb_Doimatkhau.Name = "grb_Doimatkhau";
            this.grb_Doimatkhau.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grb_Doimatkhau.Size = new System.Drawing.Size(575, 365);
            this.grb_Doimatkhau.TabIndex = 2;
            this.grb_Doimatkhau.TabStop = false;
            this.grb_Doimatkhau.Text = "Đổi mật khẩu";
            // 
            // chk_hienmatkhau
            // 
            this.chk_hienmatkhau.AutoSize = true;
            this.chk_hienmatkhau.Location = new System.Drawing.Point(381, 248);
            this.chk_hienmatkhau.Name = "chk_hienmatkhau";
            this.chk_hienmatkhau.Size = new System.Drawing.Size(138, 24);
            this.chk_hienmatkhau.TabIndex = 5;
            this.chk_hienmatkhau.Text = "Hiện mật khẩu";
            this.chk_hienmatkhau.UseVisualStyleBackColor = true;
            // 
            // txt_Xacnhanmatkhau
            // 
            this.txt_Xacnhanmatkhau.Location = new System.Drawing.Point(188, 208);
            this.txt_Xacnhanmatkhau.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_Xacnhanmatkhau.Name = "txt_Xacnhanmatkhau";
            this.txt_Xacnhanmatkhau.Size = new System.Drawing.Size(331, 26);
            this.txt_Xacnhanmatkhau.TabIndex = 4;
            // 
            // lbl_Xacnhanmatkhau
            // 
            this.lbl_Xacnhanmatkhau.AutoSize = true;
            this.lbl_Xacnhanmatkhau.Location = new System.Drawing.Point(42, 215);
            this.lbl_Xacnhanmatkhau.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Xacnhanmatkhau.Name = "lbl_Xacnhanmatkhau";
            this.lbl_Xacnhanmatkhau.Size = new System.Drawing.Size(147, 20);
            this.lbl_Xacnhanmatkhau.TabIndex = 3;
            this.lbl_Xacnhanmatkhau.Text = "Xác nhận mật khẩu";
            // 
            // btn_xacnhan
            // 
            this.btn_xacnhan.Location = new System.Drawing.Point(52, 298);
            this.btn_xacnhan.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_xacnhan.Name = "btn_xacnhan";
            this.btn_xacnhan.Size = new System.Drawing.Size(151, 42);
            this.btn_xacnhan.TabIndex = 2;
            this.btn_xacnhan.Text = "Xác nhận";
            this.btn_xacnhan.UseVisualStyleBackColor = true;
            this.btn_xacnhan.Click += new System.EventHandler(this.btn_xacnhan_Click);
            // 
            // btn_trove
            // 
            this.btn_trove.Location = new System.Drawing.Point(368, 298);
            this.btn_trove.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_trove.Name = "btn_trove";
            this.btn_trove.Size = new System.Drawing.Size(151, 42);
            this.btn_trove.TabIndex = 2;
            this.btn_trove.Text = "Trở về";
            this.btn_trove.UseVisualStyleBackColor = true;
            this.btn_trove.Click += new System.EventHandler(this.btn_trove_Click);
            // 
            // txt_MatKhauCu
            // 
            this.txt_MatKhauCu.Location = new System.Drawing.Point(188, 65);
            this.txt_MatKhauCu.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_MatKhauCu.Name = "txt_MatKhauCu";
            this.txt_MatKhauCu.Size = new System.Drawing.Size(331, 26);
            this.txt_MatKhauCu.TabIndex = 1;
            // 
            // txt_MatKhauMoi
            // 
            this.txt_MatKhauMoi.Location = new System.Drawing.Point(188, 135);
            this.txt_MatKhauMoi.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_MatKhauMoi.Name = "txt_MatKhauMoi";
            this.txt_MatKhauMoi.Size = new System.Drawing.Size(331, 26);
            this.txt_MatKhauMoi.TabIndex = 1;
            // 
            // lbl_Matkhaumoi
            // 
            this.lbl_Matkhaumoi.AutoSize = true;
            this.lbl_Matkhaumoi.Location = new System.Drawing.Point(42, 142);
            this.lbl_Matkhaumoi.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Matkhaumoi.Name = "lbl_Matkhaumoi";
            this.lbl_Matkhaumoi.Size = new System.Drawing.Size(104, 20);
            this.lbl_Matkhaumoi.TabIndex = 0;
            this.lbl_Matkhaumoi.Text = "Mật khẩu mới";
            // 
            // lbl_Matkhaucu
            // 
            this.lbl_Matkhaucu.AutoSize = true;
            this.lbl_Matkhaucu.Location = new System.Drawing.Point(42, 71);
            this.lbl_Matkhaucu.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Matkhaucu.Name = "lbl_Matkhaucu";
            this.lbl_Matkhaucu.Size = new System.Drawing.Size(96, 20);
            this.lbl_Matkhaucu.TabIndex = 0;
            this.lbl_Matkhaucu.Text = "Mật khẩu cũ";
            // 
            // frmDoiMatKhau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(165)))), ((int)(((byte)(173)))));
            this.ClientSize = new System.Drawing.Size(678, 415);
            this.Controls.Add(this.grb_Doimatkhau);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmDoiMatKhau";
            this.Text = "frmDoiMatKhau";
            this.grb_Doimatkhau.ResumeLayout(false);
            this.grb_Doimatkhau.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grb_Doimatkhau;
        private System.Windows.Forms.Button btn_trove;
        private System.Windows.Forms.TextBox txt_MatKhauCu;
        private System.Windows.Forms.TextBox txt_MatKhauMoi;
        private System.Windows.Forms.Label lbl_Matkhaumoi;
        private System.Windows.Forms.Label lbl_Matkhaucu;
        private System.Windows.Forms.TextBox txt_Xacnhanmatkhau;
        private System.Windows.Forms.Label lbl_Xacnhanmatkhau;
        private System.Windows.Forms.Button btn_xacnhan;
        private System.Windows.Forms.CheckBox chk_hienmatkhau;
    }
}