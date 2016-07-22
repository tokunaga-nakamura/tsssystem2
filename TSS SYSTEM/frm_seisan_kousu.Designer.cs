namespace TSS_SYSTEM
{
    partial class frm_seisan_kousu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_seisan_kousu));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_hardcopy = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.cb_meisai = new System.Windows.Forms.CheckBox();
            this.btn_hyouji = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tb_line_name = new System.Windows.Forms.TextBox();
            this.tb_busyo_name = new System.Windows.Forms.TextBox();
            this.tb_line_cd = new System.Windows.Forms.TextBox();
            this.tb_busyo_cd = new System.Windows.Forms.TextBox();
            this.tb_busyo_midasi = new System.Windows.Forms.TextBox();
            this.tb_line_midasi = new System.Windows.Forms.TextBox();
            this.cb_line_sitei = new System.Windows.Forms.CheckBox();
            this.cb_busyo_sitei = new System.Windows.Forms.CheckBox();
            this.nud_month = new System.Windows.Forms.NumericUpDown();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.nud_year = new System.Windows.Forms.NumericUpDown();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dgv_list = new System.Windows.Forms.DataGridView();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.btn_csv = new System.Windows.Forms.Button();
            this.btn_syuuryou = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rb_mm = new System.Windows.Forms.RadioButton();
            this.rb_ss = new System.Windows.Forms.RadioButton();
            this.rb_hh = new System.Windows.Forms.RadioButton();
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
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_month)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_year)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.btn_hardcopy);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(884, 539);
            this.splitContainer1.SplitterDistance = 60;
            this.splitContainer1.TabIndex = 1;
            this.splitContainer1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(171, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(397, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "明細行は、部署・工程・ライン等に関わらず同一受注は1行にまとめて表示されます。";
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
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer2.Size = new System.Drawing.Size(884, 475);
            this.splitContainer2.SplitterDistance = 436;
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
            this.splitContainer3.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer3.Panel1.Controls.Add(this.cb_meisai);
            this.splitContainer3.Panel1.Controls.Add(this.btn_hyouji);
            this.splitContainer3.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer3.Panel1.Controls.Add(this.nud_month);
            this.splitContainer3.Panel1.Controls.Add(this.textBox2);
            this.splitContainer3.Panel1.Controls.Add(this.nud_year);
            this.splitContainer3.Panel1.Controls.Add(this.textBox1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.dgv_list);
            this.splitContainer3.Size = new System.Drawing.Size(884, 436);
            this.splitContainer3.SplitterDistance = 80;
            this.splitContainer3.TabIndex = 0;
            // 
            // cb_meisai
            // 
            this.cb_meisai.AutoSize = true;
            this.cb_meisai.Location = new System.Drawing.Point(155, 5);
            this.cb_meisai.Name = "cb_meisai";
            this.cb_meisai.Size = new System.Drawing.Size(100, 16);
            this.cb_meisai.TabIndex = 1;
            this.cb_meisai.Text = "明細を表示する";
            this.cb_meisai.UseVisualStyleBackColor = true;
            // 
            // btn_hyouji
            // 
            this.btn_hyouji.Location = new System.Drawing.Point(582, 49);
            this.btn_hyouji.Name = "btn_hyouji";
            this.btn_hyouji.Size = new System.Drawing.Size(75, 23);
            this.btn_hyouji.TabIndex = 5;
            this.btn_hyouji.Text = "表示";
            this.btn_hyouji.UseVisualStyleBackColor = true;
            this.btn_hyouji.Click += new System.EventHandler(this.btn_hyouji_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tb_line_name);
            this.groupBox1.Controls.Add(this.tb_busyo_name);
            this.groupBox1.Controls.Add(this.tb_line_cd);
            this.groupBox1.Controls.Add(this.tb_busyo_cd);
            this.groupBox1.Controls.Add(this.tb_busyo_midasi);
            this.groupBox1.Controls.Add(this.tb_line_midasi);
            this.groupBox1.Controls.Add(this.cb_line_sitei);
            this.groupBox1.Controls.Add(this.cb_busyo_sitei);
            this.groupBox1.Location = new System.Drawing.Point(261, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(315, 67);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "絞込み";
            // 
            // tb_line_name
            // 
            this.tb_line_name.Location = new System.Drawing.Point(209, 41);
            this.tb_line_name.Name = "tb_line_name";
            this.tb_line_name.ReadOnly = true;
            this.tb_line_name.Size = new System.Drawing.Size(100, 19);
            this.tb_line_name.TabIndex = 17;
            this.tb_line_name.TabStop = false;
            // 
            // tb_busyo_name
            // 
            this.tb_busyo_name.Location = new System.Drawing.Point(209, 16);
            this.tb_busyo_name.Name = "tb_busyo_name";
            this.tb_busyo_name.ReadOnly = true;
            this.tb_busyo_name.Size = new System.Drawing.Size(100, 19);
            this.tb_busyo_name.TabIndex = 15;
            this.tb_busyo_name.TabStop = false;
            // 
            // tb_line_cd
            // 
            this.tb_line_cd.BackColor = System.Drawing.Color.PowderBlue;
            this.tb_line_cd.Location = new System.Drawing.Point(176, 41);
            this.tb_line_cd.Name = "tb_line_cd";
            this.tb_line_cd.Size = new System.Drawing.Size(33, 19);
            this.tb_line_cd.TabIndex = 14;
            this.tb_line_cd.DoubleClick += new System.EventHandler(this.tb_line_cd_DoubleClick);
            this.tb_line_cd.Validating += new System.ComponentModel.CancelEventHandler(this.tb_line_cd_Validating);
            // 
            // tb_busyo_cd
            // 
            this.tb_busyo_cd.BackColor = System.Drawing.Color.PowderBlue;
            this.tb_busyo_cd.Location = new System.Drawing.Point(176, 16);
            this.tb_busyo_cd.Name = "tb_busyo_cd";
            this.tb_busyo_cd.Size = new System.Drawing.Size(33, 19);
            this.tb_busyo_cd.TabIndex = 12;
            this.tb_busyo_cd.DoubleClick += new System.EventHandler(this.tb_busyo_cd_DoubleClick);
            this.tb_busyo_cd.Validating += new System.ComponentModel.CancelEventHandler(this.tb_busyo_cd_Validating);
            // 
            // tb_busyo_midasi
            // 
            this.tb_busyo_midasi.BackColor = System.Drawing.Color.NavajoWhite;
            this.tb_busyo_midasi.Location = new System.Drawing.Point(114, 16);
            this.tb_busyo_midasi.Name = "tb_busyo_midasi";
            this.tb_busyo_midasi.ReadOnly = true;
            this.tb_busyo_midasi.Size = new System.Drawing.Size(62, 19);
            this.tb_busyo_midasi.TabIndex = 11;
            this.tb_busyo_midasi.TabStop = false;
            this.tb_busyo_midasi.Text = "部署コード";
            // 
            // tb_line_midasi
            // 
            this.tb_line_midasi.BackColor = System.Drawing.Color.NavajoWhite;
            this.tb_line_midasi.Location = new System.Drawing.Point(114, 41);
            this.tb_line_midasi.Name = "tb_line_midasi";
            this.tb_line_midasi.ReadOnly = true;
            this.tb_line_midasi.Size = new System.Drawing.Size(62, 19);
            this.tb_line_midasi.TabIndex = 9;
            this.tb_line_midasi.TabStop = false;
            this.tb_line_midasi.Text = "ラインコード";
            // 
            // cb_line_sitei
            // 
            this.cb_line_sitei.AutoSize = true;
            this.cb_line_sitei.Location = new System.Drawing.Point(6, 43);
            this.cb_line_sitei.Name = "cb_line_sitei";
            this.cb_line_sitei.Size = new System.Drawing.Size(102, 16);
            this.cb_line_sitei.TabIndex = 8;
            this.cb_line_sitei.Text = "ラインを指定する";
            this.cb_line_sitei.UseVisualStyleBackColor = true;
            this.cb_line_sitei.CheckedChanged += new System.EventHandler(this.cb_line_sitei_CheckedChanged);
            // 
            // cb_busyo_sitei
            // 
            this.cb_busyo_sitei.AutoSize = true;
            this.cb_busyo_sitei.Location = new System.Drawing.Point(6, 18);
            this.cb_busyo_sitei.Name = "cb_busyo_sitei";
            this.cb_busyo_sitei.Size = new System.Drawing.Size(100, 16);
            this.cb_busyo_sitei.TabIndex = 6;
            this.cb_busyo_sitei.Text = "部署を指定する";
            this.cb_busyo_sitei.UseVisualStyleBackColor = true;
            this.cb_busyo_sitei.CheckedChanged += new System.EventHandler(this.cb_busyo_sitei_CheckedChanged);
            // 
            // nud_month
            // 
            this.nud_month.Location = new System.Drawing.Point(112, 3);
            this.nud_month.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.nud_month.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_month.Name = "nud_month";
            this.nud_month.Size = new System.Drawing.Size(37, 19);
            this.nud_month.TabIndex = 3;
            this.nud_month.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nud_month.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox2.Location = new System.Drawing.Point(89, 3);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(23, 19);
            this.textBox2.TabIndex = 2;
            this.textBox2.TabStop = false;
            this.textBox2.Text = "月";
            // 
            // nud_year
            // 
            this.nud_year.Location = new System.Drawing.Point(32, 3);
            this.nud_year.Maximum = new decimal(new int[] {
            2099,
            0,
            0,
            0});
            this.nud_year.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nud_year.Name = "nud_year";
            this.nud_year.Size = new System.Drawing.Size(51, 19);
            this.nud_year.TabIndex = 1;
            this.nud_year.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nud_year.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox1.Location = new System.Drawing.Point(10, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(22, 19);
            this.textBox1.TabIndex = 0;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "年";
            // 
            // dgv_list
            // 
            this.dgv_list.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_list.Location = new System.Drawing.Point(0, 0);
            this.dgv_list.Name = "dgv_list";
            this.dgv_list.RowTemplate.Height = 21;
            this.dgv_list.Size = new System.Drawing.Size(880, 348);
            this.dgv_list.TabIndex = 0;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer4.IsSplitterFixed = true;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.btn_csv);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.btn_syuuryou);
            this.splitContainer4.Size = new System.Drawing.Size(880, 31);
            this.splitContainer4.SplitterDistance = 795;
            this.splitContainer4.TabIndex = 1;
            // 
            // btn_csv
            // 
            this.btn_csv.Location = new System.Drawing.Point(10, 3);
            this.btn_csv.Name = "btn_csv";
            this.btn_csv.Size = new System.Drawing.Size(75, 23);
            this.btn_csv.TabIndex = 0;
            this.btn_csv.Text = "CSV出力";
            this.btn_csv.UseVisualStyleBackColor = true;
            this.btn_csv.Click += new System.EventHandler(this.btn_csv_Click);
            // 
            // btn_syuuryou
            // 
            this.btn_syuuryou.Location = new System.Drawing.Point(3, 3);
            this.btn_syuuryou.Name = "btn_syuuryou";
            this.btn_syuuryou.Size = new System.Drawing.Size(75, 23);
            this.btn_syuuryou.TabIndex = 0;
            this.btn_syuuryou.Text = "終了";
            this.btn_syuuryou.UseVisualStyleBackColor = true;
            this.btn_syuuryou.Click += new System.EventHandler(this.btn_syuuryou_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rb_hh);
            this.groupBox2.Controls.Add(this.rb_ss);
            this.groupBox2.Controls.Add(this.rb_mm);
            this.groupBox2.Location = new System.Drawing.Point(10, 28);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(139, 44);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "表示単位";
            // 
            // rb_mm
            // 
            this.rb_mm.AutoSize = true;
            this.rb_mm.Location = new System.Drawing.Point(47, 18);
            this.rb_mm.Name = "rb_mm";
            this.rb_mm.Size = new System.Drawing.Size(35, 16);
            this.rb_mm.TabIndex = 0;
            this.rb_mm.Text = "秒";
            this.rb_mm.UseVisualStyleBackColor = true;
            // 
            // rb_ss
            // 
            this.rb_ss.AutoSize = true;
            this.rb_ss.Location = new System.Drawing.Point(88, 18);
            this.rb_ss.Name = "rb_ss";
            this.rb_ss.Size = new System.Drawing.Size(35, 16);
            this.rb_ss.TabIndex = 1;
            this.rb_ss.Text = "分";
            this.rb_ss.UseVisualStyleBackColor = true;
            // 
            // rb_hh
            // 
            this.rb_hh.AutoSize = true;
            this.rb_hh.Checked = true;
            this.rb_hh.Location = new System.Drawing.Point(6, 18);
            this.rb_hh.Name = "rb_hh";
            this.rb_hh.Size = new System.Drawing.Size(35, 16);
            this.rb_hh.TabIndex = 2;
            this.rb_hh.TabStop = true;
            this.rb_hh.Text = "時";
            this.rb_hh.UseVisualStyleBackColor = true;
            // 
            // frm_seisan_kousu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "frm_seisan_kousu";
            this.Text = "生産工数参照";
            this.Load += new System.EventHandler(this.frm_seisan_kousu_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
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
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_month)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_year)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).EndInit();
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btn_hardcopy;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown nud_month;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.NumericUpDown nud_year;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn_syuuryou;
        private System.Windows.Forms.Button btn_hyouji;
        private System.Windows.Forms.TextBox tb_line_name;
        private System.Windows.Forms.TextBox tb_busyo_name;
        private System.Windows.Forms.TextBox tb_line_cd;
        private System.Windows.Forms.TextBox tb_busyo_cd;
        private System.Windows.Forms.TextBox tb_busyo_midasi;
        private System.Windows.Forms.TextBox tb_line_midasi;
        private System.Windows.Forms.CheckBox cb_line_sitei;
        private System.Windows.Forms.CheckBox cb_busyo_sitei;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.DataGridView dgv_list;
        private System.Windows.Forms.CheckBox cb_meisai;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_csv;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rb_hh;
        private System.Windows.Forms.RadioButton rb_ss;
        private System.Windows.Forms.RadioButton rb_mm;
    }
}