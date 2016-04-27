namespace TSS_SYSTEM
{
    partial class frm_busyo_m
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_busyo_m));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btn_hardcopy = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.tb_kousu = new System.Windows.Forms.TextBox();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.tb_ninzu = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.lbl_busyo_cd = new System.Windows.Forms.Label();
            this.tb_update_datetime = new System.Windows.Forms.TextBox();
            this.tb_update_user_cd = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.tb_create_datetime = new System.Windows.Forms.TextBox();
            this.tb_create_user_cd = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.tb_delete_flg = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.tb_bikou = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.tb_busyo_ryakusiki_name = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tb_busyo_name = new System.Windows.Forms.TextBox();
            this.tb_busyo_cd = new System.Windows.Forms.TextBox();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.dgv_busyo_m = new System.Windows.Forms.DataGridView();
            this.btn_touroku = new System.Windows.Forms.Button();
            this.btn_syuuryou = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_busyo_m)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 539);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(884, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
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
            this.splitContainer1.Size = new System.Drawing.Size(884, 539);
            this.splitContainer1.SplitterDistance = 61;
            this.splitContainer1.TabIndex = 1;
            this.splitContainer1.TabStop = false;
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
            this.btn_hardcopy.Click += new System.EventHandler(this.btn_hardcopy_Click);
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
            this.splitContainer2.Size = new System.Drawing.Size(884, 474);
            this.splitContainer2.SplitterDistance = 437;
            this.splitContainer2.TabIndex = 0;
            this.splitContainer2.TabStop = false;
            // 
            // splitContainer3
            // 
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer3.IsSplitterFixed = true;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.tb_kousu);
            this.splitContainer3.Panel1.Controls.Add(this.textBox11);
            this.splitContainer3.Panel1.Controls.Add(this.tb_ninzu);
            this.splitContainer3.Panel1.Controls.Add(this.textBox8);
            this.splitContainer3.Panel1.Controls.Add(this.lbl_busyo_cd);
            this.splitContainer3.Panel1.Controls.Add(this.tb_update_datetime);
            this.splitContainer3.Panel1.Controls.Add(this.tb_update_user_cd);
            this.splitContainer3.Panel1.Controls.Add(this.textBox9);
            this.splitContainer3.Panel1.Controls.Add(this.tb_create_datetime);
            this.splitContainer3.Panel1.Controls.Add(this.tb_create_user_cd);
            this.splitContainer3.Panel1.Controls.Add(this.textBox6);
            this.splitContainer3.Panel1.Controls.Add(this.tb_delete_flg);
            this.splitContainer3.Panel1.Controls.Add(this.textBox4);
            this.splitContainer3.Panel1.Controls.Add(this.tb_bikou);
            this.splitContainer3.Panel1.Controls.Add(this.textBox5);
            this.splitContainer3.Panel1.Controls.Add(this.tb_busyo_ryakusiki_name);
            this.splitContainer3.Panel1.Controls.Add(this.textBox3);
            this.splitContainer3.Panel1.Controls.Add(this.textBox2);
            this.splitContainer3.Panel1.Controls.Add(this.textBox1);
            this.splitContainer3.Panel1.Controls.Add(this.tb_busyo_name);
            this.splitContainer3.Panel1.Controls.Add(this.tb_busyo_cd);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer3.Size = new System.Drawing.Size(884, 437);
            this.splitContainer3.SplitterDistance = 157;
            this.splitContainer3.TabIndex = 0;
            this.splitContainer3.TabStop = false;
            // 
            // tb_kousu
            // 
            this.tb_kousu.Location = new System.Drawing.Point(77, 103);
            this.tb_kousu.MaxLength = 8;
            this.tb_kousu.Name = "tb_kousu";
            this.tb_kousu.Size = new System.Drawing.Size(54, 19);
            this.tb_kousu.TabIndex = 4;
            this.tb_kousu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tb_kousu.Validating += new System.ComponentModel.CancelEventHandler(this.tb_kousu_Validating);
            // 
            // textBox11
            // 
            this.textBox11.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox11.Location = new System.Drawing.Point(10, 103);
            this.textBox11.Name = "textBox11";
            this.textBox11.ReadOnly = true;
            this.textBox11.Size = new System.Drawing.Size(67, 19);
            this.textBox11.TabIndex = 37;
            this.textBox11.TabStop = false;
            this.textBox11.Text = "保有工数";
            // 
            // tb_ninzu
            // 
            this.tb_ninzu.Location = new System.Drawing.Point(77, 78);
            this.tb_ninzu.MaxLength = 6;
            this.tb_ninzu.Name = "tb_ninzu";
            this.tb_ninzu.Size = new System.Drawing.Size(54, 19);
            this.tb_ninzu.TabIndex = 3;
            this.tb_ninzu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tb_ninzu.Validating += new System.ComponentModel.CancelEventHandler(this.tb_ninzu_Validating);
            // 
            // textBox8
            // 
            this.textBox8.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox8.Location = new System.Drawing.Point(10, 78);
            this.textBox8.Name = "textBox8";
            this.textBox8.ReadOnly = true;
            this.textBox8.Size = new System.Drawing.Size(67, 19);
            this.textBox8.TabIndex = 35;
            this.textBox8.TabStop = false;
            this.textBox8.Text = "所属人数";
            // 
            // lbl_busyo_cd
            // 
            this.lbl_busyo_cd.AutoSize = true;
            this.lbl_busyo_cd.Location = new System.Drawing.Point(123, 6);
            this.lbl_busyo_cd.Name = "lbl_busyo_cd";
            this.lbl_busyo_cd.Size = new System.Drawing.Size(140, 12);
            this.lbl_busyo_cd.TabIndex = 33;
            this.lbl_busyo_cd.Text = "部署コードを入力してください";
            // 
            // tb_update_datetime
            // 
            this.tb_update_datetime.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_update_datetime.Location = new System.Drawing.Point(747, 128);
            this.tb_update_datetime.Name = "tb_update_datetime";
            this.tb_update_datetime.ReadOnly = true;
            this.tb_update_datetime.Size = new System.Drawing.Size(123, 19);
            this.tb_update_datetime.TabIndex = 32;
            this.tb_update_datetime.TabStop = false;
            // 
            // tb_update_user_cd
            // 
            this.tb_update_user_cd.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_update_user_cd.Location = new System.Drawing.Point(695, 128);
            this.tb_update_user_cd.Name = "tb_update_user_cd";
            this.tb_update_user_cd.ReadOnly = true;
            this.tb_update_user_cd.Size = new System.Drawing.Size(52, 19);
            this.tb_update_user_cd.TabIndex = 31;
            this.tb_update_user_cd.TabStop = false;
            // 
            // textBox9
            // 
            this.textBox9.BackColor = System.Drawing.Color.Gainsboro;
            this.textBox9.Location = new System.Drawing.Point(654, 128);
            this.textBox9.Name = "textBox9";
            this.textBox9.ReadOnly = true;
            this.textBox9.Size = new System.Drawing.Size(41, 19);
            this.textBox9.TabIndex = 30;
            this.textBox9.TabStop = false;
            this.textBox9.Text = "更新";
            // 
            // tb_create_datetime
            // 
            this.tb_create_datetime.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_create_datetime.Location = new System.Drawing.Point(747, 109);
            this.tb_create_datetime.Name = "tb_create_datetime";
            this.tb_create_datetime.ReadOnly = true;
            this.tb_create_datetime.Size = new System.Drawing.Size(123, 19);
            this.tb_create_datetime.TabIndex = 29;
            this.tb_create_datetime.TabStop = false;
            // 
            // tb_create_user_cd
            // 
            this.tb_create_user_cd.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_create_user_cd.Location = new System.Drawing.Point(695, 109);
            this.tb_create_user_cd.Name = "tb_create_user_cd";
            this.tb_create_user_cd.ReadOnly = true;
            this.tb_create_user_cd.Size = new System.Drawing.Size(52, 19);
            this.tb_create_user_cd.TabIndex = 28;
            this.tb_create_user_cd.TabStop = false;
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.Color.Gainsboro;
            this.textBox6.Location = new System.Drawing.Point(654, 109);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(41, 19);
            this.textBox6.TabIndex = 27;
            this.textBox6.TabStop = false;
            this.textBox6.Text = "作成";
            // 
            // tb_delete_flg
            // 
            this.tb_delete_flg.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_delete_flg.Location = new System.Drawing.Point(846, 3);
            this.tb_delete_flg.Name = "tb_delete_flg";
            this.tb_delete_flg.ReadOnly = true;
            this.tb_delete_flg.Size = new System.Drawing.Size(24, 19);
            this.tb_delete_flg.TabIndex = 26;
            this.tb_delete_flg.TabStop = false;
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.Color.Gainsboro;
            this.textBox4.Location = new System.Drawing.Point(780, 3);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(66, 19);
            this.textBox4.TabIndex = 25;
            this.textBox4.TabStop = false;
            this.textBox4.Text = "削除フラグ";
            // 
            // tb_bikou
            // 
            this.tb_bikou.Location = new System.Drawing.Point(77, 128);
            this.tb_bikou.MaxLength = 128;
            this.tb_bikou.Name = "tb_bikou";
            this.tb_bikou.Size = new System.Drawing.Size(571, 19);
            this.tb_bikou.TabIndex = 5;
            this.tb_bikou.Validating += new System.ComponentModel.CancelEventHandler(this.tb_bikou_Validating);
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox5.Location = new System.Drawing.Point(10, 128);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(67, 19);
            this.textBox5.TabIndex = 24;
            this.textBox5.TabStop = false;
            this.textBox5.Text = "備考";
            // 
            // tb_busyo_ryakusiki_name
            // 
            this.tb_busyo_ryakusiki_name.Location = new System.Drawing.Point(77, 53);
            this.tb_busyo_ryakusiki_name.MaxLength = 10;
            this.tb_busyo_ryakusiki_name.Name = "tb_busyo_ryakusiki_name";
            this.tb_busyo_ryakusiki_name.Size = new System.Drawing.Size(76, 19);
            this.tb_busyo_ryakusiki_name.TabIndex = 2;
            this.tb_busyo_ryakusiki_name.Validating += new System.ComponentModel.CancelEventHandler(this.tb_busyo_ryakusiki_name_Validating);
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox3.Location = new System.Drawing.Point(10, 53);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(67, 19);
            this.textBox3.TabIndex = 23;
            this.textBox3.TabStop = false;
            this.textBox3.Text = "略式名称";
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox2.Location = new System.Drawing.Point(10, 28);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(67, 19);
            this.textBox2.TabIndex = 21;
            this.textBox2.TabStop = false;
            this.textBox2.Text = "部署名称";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox1.Location = new System.Drawing.Point(10, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(67, 19);
            this.textBox1.TabIndex = 18;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "部署コード";
            // 
            // tb_busyo_name
            // 
            this.tb_busyo_name.BackColor = System.Drawing.SystemColors.Window;
            this.tb_busyo_name.Location = new System.Drawing.Point(77, 28);
            this.tb_busyo_name.MaxLength = 20;
            this.tb_busyo_name.Name = "tb_busyo_name";
            this.tb_busyo_name.Size = new System.Drawing.Size(139, 19);
            this.tb_busyo_name.TabIndex = 1;
            this.tb_busyo_name.Validating += new System.ComponentModel.CancelEventHandler(this.tb_busyo_name_Validating);
            // 
            // tb_busyo_cd
            // 
            this.tb_busyo_cd.BackColor = System.Drawing.SystemColors.Window;
            this.tb_busyo_cd.Location = new System.Drawing.Point(77, 3);
            this.tb_busyo_cd.MaxLength = 4;
            this.tb_busyo_cd.Name = "tb_busyo_cd";
            this.tb_busyo_cd.Size = new System.Drawing.Size(40, 19);
            this.tb_busyo_cd.TabIndex = 0;
            this.tb_busyo_cd.Validating += new System.ComponentModel.CancelEventHandler(this.tb_busyo_cd_Validating);
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer4.IsSplitterFixed = true;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.dgv_busyo_m);
            this.splitContainer4.Size = new System.Drawing.Size(880, 272);
            this.splitContainer4.SplitterDistance = 32;
            this.splitContainer4.TabIndex = 0;
            this.splitContainer4.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "登録済みの部署マスタ";
            // 
            // dgv_busyo_m
            // 
            this.dgv_busyo_m.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_busyo_m.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_busyo_m.Location = new System.Drawing.Point(0, 0);
            this.dgv_busyo_m.Name = "dgv_busyo_m";
            this.dgv_busyo_m.RowTemplate.Height = 21;
            this.dgv_busyo_m.Size = new System.Drawing.Size(880, 236);
            this.dgv_busyo_m.TabIndex = 0;
            // 
            // btn_touroku
            // 
            this.btn_touroku.Location = new System.Drawing.Point(10, 3);
            this.btn_touroku.Name = "btn_touroku";
            this.btn_touroku.Size = new System.Drawing.Size(75, 23);
            this.btn_touroku.TabIndex = 6;
            this.btn_touroku.Text = "登録";
            this.btn_touroku.UseVisualStyleBackColor = true;
            this.btn_touroku.Click += new System.EventHandler(this.btn_touroku_Click);
            // 
            // btn_syuuryou
            // 
            this.btn_syuuryou.Location = new System.Drawing.Point(795, 3);
            this.btn_syuuryou.Name = "btn_syuuryou";
            this.btn_syuuryou.Size = new System.Drawing.Size(75, 23);
            this.btn_syuuryou.TabIndex = 7;
            this.btn_syuuryou.Text = "終了";
            this.btn_syuuryou.UseVisualStyleBackColor = true;
            this.btn_syuuryou.Click += new System.EventHandler(this.btn_syuuryou_Click);
            // 
            // frm_busyo_m
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "frm_busyo_m";
            this.Text = "部署マスタ";
            this.Load += new System.EventHandler(this.frm_busyo_m_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel1.PerformLayout();
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_busyo_m)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Button btn_hardcopy;
        private System.Windows.Forms.Button btn_syuuryou;
        private System.Windows.Forms.Button btn_touroku;
        private System.Windows.Forms.TextBox tb_kousu;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.TextBox tb_ninzu;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Label lbl_busyo_cd;
        private System.Windows.Forms.TextBox tb_update_datetime;
        private System.Windows.Forms.TextBox tb_update_user_cd;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox tb_create_datetime;
        private System.Windows.Forms.TextBox tb_create_user_cd;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox tb_delete_flg;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox tb_bikou;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox tb_busyo_ryakusiki_name;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox tb_busyo_name;
        private System.Windows.Forms.TextBox tb_busyo_cd;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgv_busyo_m;
    }
}