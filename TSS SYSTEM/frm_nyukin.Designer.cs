namespace TSS_SYSTEM
{
    partial class frm_nyukin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_nyukin));
            this.btn_syuuryou = new System.Windows.Forms.Button();
            this.tb_nyukin_goukei = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.lbl_seikyuu = new System.Windows.Forms.Label();
            this.tb_torihikisaki_cd = new System.Windows.Forms.TextBox();
            this.tb_nyukin_date = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.dgv_m = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.tb_nyukin_no = new System.Windows.Forms.TextBox();
            this.tb_torihikisaki_name = new System.Windows.Forms.TextBox();
            this.btn_touroku = new System.Windows.Forms.Button();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.tb_update_datetime = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.tb_update_user_cd = new System.Windows.Forms.TextBox();
            this.tb_create_user_cd = new System.Windows.Forms.TextBox();
            this.tb_create_datetime = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.btn_hardcopy = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_m)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_syuuryou
            // 
            this.btn_syuuryou.Location = new System.Drawing.Point(795, 3);
            this.btn_syuuryou.Name = "btn_syuuryou";
            this.btn_syuuryou.Size = new System.Drawing.Size(75, 23);
            this.btn_syuuryou.TabIndex = 1;
            this.btn_syuuryou.Text = "終了";
            this.btn_syuuryou.UseVisualStyleBackColor = true;
            this.btn_syuuryou.Click += new System.EventHandler(this.btn_syuuryou_Click_1);
            // 
            // tb_nyukin_goukei
            // 
            this.tb_nyukin_goukei.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_nyukin_goukei.Location = new System.Drawing.Point(770, 3);
            this.tb_nyukin_goukei.Name = "tb_nyukin_goukei";
            this.tb_nyukin_goukei.ReadOnly = true;
            this.tb_nyukin_goukei.Size = new System.Drawing.Size(100, 19);
            this.tb_nyukin_goukei.TabIndex = 3;
            this.tb_nyukin_goukei.TabStop = false;
            this.tb_nyukin_goukei.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox10
            // 
            this.textBox10.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox10.Location = new System.Drawing.Point(705, 3);
            this.textBox10.Name = "textBox10";
            this.textBox10.ReadOnly = true;
            this.textBox10.Size = new System.Drawing.Size(65, 19);
            this.textBox10.TabIndex = 2;
            this.textBox10.TabStop = false;
            this.textBox10.Text = "入金合計";
            // 
            // lbl_seikyuu
            // 
            this.lbl_seikyuu.AutoSize = true;
            this.lbl_seikyuu.ForeColor = System.Drawing.Color.Red;
            this.lbl_seikyuu.Location = new System.Drawing.Point(224, 56);
            this.lbl_seikyuu.Name = "lbl_seikyuu";
            this.lbl_seikyuu.Size = new System.Drawing.Size(85, 12);
            this.lbl_seikyuu.TabIndex = 6;
            this.lbl_seikyuu.Text = "                    ";
            // 
            // tb_torihikisaki_cd
            // 
            this.tb_torihikisaki_cd.BackColor = System.Drawing.Color.PowderBlue;
            this.tb_torihikisaki_cd.Location = new System.Drawing.Point(110, 28);
            this.tb_torihikisaki_cd.MaxLength = 6;
            this.tb_torihikisaki_cd.Name = "tb_torihikisaki_cd";
            this.tb_torihikisaki_cd.Size = new System.Drawing.Size(51, 19);
            this.tb_torihikisaki_cd.TabIndex = 1;
            this.tb_torihikisaki_cd.DoubleClick += new System.EventHandler(this.tb_torihikisaki_cd_DoubleClick);
            this.tb_torihikisaki_cd.Validating += new System.ComponentModel.CancelEventHandler(this.tb_torihikisaki_cd_Validating_1);
            // 
            // tb_nyukin_date
            // 
            this.tb_nyukin_date.Location = new System.Drawing.Point(110, 53);
            this.tb_nyukin_date.MaxLength = 10;
            this.tb_nyukin_date.Name = "tb_nyukin_date";
            this.tb_nyukin_date.Size = new System.Drawing.Size(75, 19);
            this.tb_nyukin_date.TabIndex = 2;
            this.tb_nyukin_date.Validating += new System.ComponentModel.CancelEventHandler(this.tb_nyukin_date_Validating_1);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox1.Location = new System.Drawing.Point(10, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(100, 19);
            this.textBox1.TabIndex = 0;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "入金番号";
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox6.Location = new System.Drawing.Point(10, 53);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(100, 19);
            this.textBox6.TabIndex = 5;
            this.textBox6.TabStop = false;
            this.textBox6.Text = "入金日";
            // 
            // dgv_m
            // 
            this.dgv_m.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_m.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dgv_m.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_m.Location = new System.Drawing.Point(0, 0);
            this.dgv_m.Name = "dgv_m";
            this.dgv_m.RowTemplate.Height = 21;
            this.dgv_m.Size = new System.Drawing.Size(880, 320);
            this.dgv_m.TabIndex = 0;
            this.dgv_m.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_m_CellEndEdit);
            this.dgv_m.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgv_m_CellValidating);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "入金区分";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "入金区分名称";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "入金額";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "備考";
            this.Column4.Name = "Column4";
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer4.IsSplitterFixed = true;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.dgv_m);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.tb_nyukin_goukei);
            this.splitContainer4.Panel2.Controls.Add(this.textBox10);
            this.splitContainer4.Size = new System.Drawing.Size(880, 353);
            this.splitContainer4.SplitterDistance = 320;
            this.splitContainer4.TabIndex = 0;
            this.splitContainer4.TabStop = false;
            // 
            // tb_nyukin_no
            // 
            this.tb_nyukin_no.BackColor = System.Drawing.Color.PowderBlue;
            this.tb_nyukin_no.Location = new System.Drawing.Point(110, 3);
            this.tb_nyukin_no.MaxLength = 10;
            this.tb_nyukin_no.Name = "tb_nyukin_no";
            this.tb_nyukin_no.Size = new System.Drawing.Size(75, 19);
            this.tb_nyukin_no.TabIndex = 0;
            this.tb_nyukin_no.Validating += new System.ComponentModel.CancelEventHandler(this.tb_nyukin_no_Validating);
            // 
            // tb_torihikisaki_name
            // 
            this.tb_torihikisaki_name.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_torihikisaki_name.Location = new System.Drawing.Point(161, 28);
            this.tb_torihikisaki_name.Name = "tb_torihikisaki_name";
            this.tb_torihikisaki_name.ReadOnly = true;
            this.tb_torihikisaki_name.Size = new System.Drawing.Size(398, 19);
            this.tb_torihikisaki_name.TabIndex = 4;
            this.tb_torihikisaki_name.TabStop = false;
            // 
            // btn_touroku
            // 
            this.btn_touroku.Location = new System.Drawing.Point(10, 3);
            this.btn_touroku.Name = "btn_touroku";
            this.btn_touroku.Size = new System.Drawing.Size(75, 23);
            this.btn_touroku.TabIndex = 0;
            this.btn_touroku.Text = "登録";
            this.btn_touroku.UseVisualStyleBackColor = true;
            this.btn_touroku.Click += new System.EventHandler(this.btn_touroku_Click);
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer3.IsSplitterFixed = true;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.tb_update_datetime);
            this.splitContainer3.Panel1.Controls.Add(this.textBox2);
            this.splitContainer3.Panel1.Controls.Add(this.tb_update_user_cd);
            this.splitContainer3.Panel1.Controls.Add(this.tb_create_user_cd);
            this.splitContainer3.Panel1.Controls.Add(this.tb_create_datetime);
            this.splitContainer3.Panel1.Controls.Add(this.textBox9);
            this.splitContainer3.Panel1.Controls.Add(this.lbl_seikyuu);
            this.splitContainer3.Panel1.Controls.Add(this.tb_torihikisaki_cd);
            this.splitContainer3.Panel1.Controls.Add(this.tb_nyukin_date);
            this.splitContainer3.Panel1.Controls.Add(this.textBox1);
            this.splitContainer3.Panel1.Controls.Add(this.textBox6);
            this.splitContainer3.Panel1.Controls.Add(this.tb_nyukin_no);
            this.splitContainer3.Panel1.Controls.Add(this.tb_torihikisaki_name);
            this.splitContainer3.Panel1.Controls.Add(this.textBox3);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer3.Size = new System.Drawing.Size(880, 435);
            this.splitContainer3.SplitterDistance = 78;
            this.splitContainer3.TabIndex = 7;
            // 
            // tb_update_datetime
            // 
            this.tb_update_datetime.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_update_datetime.Location = new System.Drawing.Point(743, 51);
            this.tb_update_datetime.Name = "tb_update_datetime";
            this.tb_update_datetime.ReadOnly = true;
            this.tb_update_datetime.Size = new System.Drawing.Size(127, 19);
            this.tb_update_datetime.TabIndex = 53;
            this.tb_update_datetime.TabStop = false;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.Gainsboro;
            this.textBox2.Location = new System.Drawing.Point(660, 32);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(38, 19);
            this.textBox2.TabIndex = 48;
            this.textBox2.TabStop = false;
            this.textBox2.Text = "作成";
            // 
            // tb_update_user_cd
            // 
            this.tb_update_user_cd.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_update_user_cd.Location = new System.Drawing.Point(698, 51);
            this.tb_update_user_cd.Name = "tb_update_user_cd";
            this.tb_update_user_cd.ReadOnly = true;
            this.tb_update_user_cd.Size = new System.Drawing.Size(45, 19);
            this.tb_update_user_cd.TabIndex = 52;
            this.tb_update_user_cd.TabStop = false;
            // 
            // tb_create_user_cd
            // 
            this.tb_create_user_cd.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_create_user_cd.Location = new System.Drawing.Point(698, 32);
            this.tb_create_user_cd.Name = "tb_create_user_cd";
            this.tb_create_user_cd.ReadOnly = true;
            this.tb_create_user_cd.Size = new System.Drawing.Size(45, 19);
            this.tb_create_user_cd.TabIndex = 49;
            this.tb_create_user_cd.TabStop = false;
            // 
            // tb_create_datetime
            // 
            this.tb_create_datetime.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_create_datetime.Location = new System.Drawing.Point(743, 32);
            this.tb_create_datetime.Name = "tb_create_datetime";
            this.tb_create_datetime.ReadOnly = true;
            this.tb_create_datetime.Size = new System.Drawing.Size(127, 19);
            this.tb_create_datetime.TabIndex = 50;
            this.tb_create_datetime.TabStop = false;
            // 
            // textBox9
            // 
            this.textBox9.BackColor = System.Drawing.Color.Gainsboro;
            this.textBox9.Location = new System.Drawing.Point(660, 51);
            this.textBox9.Name = "textBox9";
            this.textBox9.ReadOnly = true;
            this.textBox9.Size = new System.Drawing.Size(38, 19);
            this.textBox9.TabIndex = 51;
            this.textBox9.TabStop = false;
            this.textBox9.Text = "更新";
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox3.Location = new System.Drawing.Point(10, 28);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(100, 19);
            this.textBox3.TabIndex = 2;
            this.textBox3.TabStop = false;
            this.textBox3.Text = "取引先コード";
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.btn_touroku);
            this.splitContainer2.Panel2.Controls.Add(this.btn_syuuryou);
            this.splitContainer2.Size = new System.Drawing.Size(884, 475);
            this.splitContainer2.SplitterDistance = 439;
            this.splitContainer2.TabIndex = 0;
            this.splitContainer2.TabStop = false;
            // 
            // btn_hardcopy
            // 
            this.btn_hardcopy.Image = ((System.Drawing.Image)(resources.GetObject("btn_hardcopy.Image")));
            this.btn_hardcopy.Location = new System.Drawing.Point(10, 10);
            this.btn_hardcopy.Name = "btn_hardcopy";
            this.btn_hardcopy.Size = new System.Drawing.Size(36, 36);
            this.btn_hardcopy.TabIndex = 0;
            this.btn_hardcopy.TabStop = false;
            this.btn_hardcopy.UseVisualStyleBackColor = true;
            this.btn_hardcopy.Click += new System.EventHandler(this.btn_hardcopy_Click_1);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btn_hardcopy);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(884, 540);
            this.splitContainer1.SplitterDistance = 61;
            this.splitContainer1.TabIndex = 3;
            this.splitContainer1.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 540);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(884, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // frm_nyukin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 562);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "frm_nyukin";
            this.Text = "入金処理";
            this.Load += new System.EventHandler(this.frm_nyukin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_m)).EndInit();
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_syuuryou;
        private System.Windows.Forms.TextBox tb_nyukin_goukei;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.Label lbl_seikyuu;
        private System.Windows.Forms.TextBox tb_torihikisaki_cd;
        private System.Windows.Forms.TextBox tb_nyukin_date;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.DataGridView dgv_m;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.TextBox tb_nyukin_no;
        private System.Windows.Forms.TextBox tb_torihikisaki_name;
        private System.Windows.Forms.Button btn_touroku;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button btn_hardcopy;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TextBox tb_update_datetime;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox tb_update_user_cd;
        private System.Windows.Forms.TextBox tb_create_user_cd;
        private System.Windows.Forms.TextBox tb_create_datetime;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;

    }
}