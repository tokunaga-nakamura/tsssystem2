namespace TSS_SYSTEM
{
    partial class frm_seisan_schedule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_seisan_schedule));
            this.btn_syuuryou = new System.Windows.Forms.Button();
            this.dgv_list = new System.Windows.Forms.DataGridView();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.tb_line_name = new System.Windows.Forms.TextBox();
            this.tb_koutei_name = new System.Windows.Forms.TextBox();
            this.tb_busyo_name = new System.Windows.Forms.TextBox();
            this.tb_line_cd = new System.Windows.Forms.TextBox();
            this.tb_koutei_cd = new System.Windows.Forms.TextBox();
            this.btn_hyouji = new System.Windows.Forms.Button();
            this.tb_busyo_cd = new System.Windows.Forms.TextBox();
            this.tb_busyo_midasi = new System.Windows.Forms.TextBox();
            this.tb_koutei_midasi = new System.Windows.Forms.TextBox();
            this.tb_line_midasi = new System.Windows.Forms.TextBox();
            this.cb_line_sitei = new System.Windows.Forms.CheckBox();
            this.cb_koutei_sitei = new System.Windows.Forms.CheckBox();
            this.cb_busyo_sitei = new System.Windows.Forms.CheckBox();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.tb_seisan_yotei_date = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.btn_hardcopy = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            this.btn_syuuryou.Location = new System.Drawing.Point(3, 3);
            this.btn_syuuryou.Name = "btn_syuuryou";
            this.btn_syuuryou.Size = new System.Drawing.Size(75, 23);
            this.btn_syuuryou.TabIndex = 0;
            this.btn_syuuryou.Text = "終了";
            this.btn_syuuryou.UseVisualStyleBackColor = true;
            this.btn_syuuryou.Click += new System.EventHandler(this.btn_syuuryou_Click);
            // 
            // dgv_list
            // 
            this.dgv_list.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_list.Location = new System.Drawing.Point(0, 0);
            this.dgv_list.Name = "dgv_list";
            this.dgv_list.RowTemplate.Height = 21;
            this.dgv_list.Size = new System.Drawing.Size(880, 322);
            this.dgv_list.TabIndex = 0;
            this.dgv_list.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_list_CellFormatting);
            this.dgv_list.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgv_list_CellPainting);
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer4.IsSplitterFixed = true;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.btn_syuuryou);
            this.splitContainer4.Size = new System.Drawing.Size(880, 31);
            this.splitContainer4.SplitterDistance = 795;
            this.splitContainer4.TabIndex = 1;
            // 
            // tb_line_name
            // 
            this.tb_line_name.Location = new System.Drawing.Point(759, 16);
            this.tb_line_name.Name = "tb_line_name";
            this.tb_line_name.ReadOnly = true;
            this.tb_line_name.Size = new System.Drawing.Size(100, 19);
            this.tb_line_name.TabIndex = 17;
            this.tb_line_name.TabStop = false;
            // 
            // tb_koutei_name
            // 
            this.tb_koutei_name.Location = new System.Drawing.Point(472, 16);
            this.tb_koutei_name.Name = "tb_koutei_name";
            this.tb_koutei_name.ReadOnly = true;
            this.tb_koutei_name.Size = new System.Drawing.Size(100, 19);
            this.tb_koutei_name.TabIndex = 16;
            this.tb_koutei_name.TabStop = false;
            // 
            // tb_busyo_name
            // 
            this.tb_busyo_name.Location = new System.Drawing.Point(170, 16);
            this.tb_busyo_name.Name = "tb_busyo_name";
            this.tb_busyo_name.ReadOnly = true;
            this.tb_busyo_name.Size = new System.Drawing.Size(100, 19);
            this.tb_busyo_name.TabIndex = 15;
            this.tb_busyo_name.TabStop = false;
            // 
            // tb_line_cd
            // 
            this.tb_line_cd.BackColor = System.Drawing.Color.PowderBlue;
            this.tb_line_cd.Location = new System.Drawing.Point(726, 16);
            this.tb_line_cd.Name = "tb_line_cd";
            this.tb_line_cd.Size = new System.Drawing.Size(33, 19);
            this.tb_line_cd.TabIndex = 14;
            this.tb_line_cd.DoubleClick += new System.EventHandler(this.tb_line_cd_DoubleClick);
            // 
            // tb_koutei_cd
            // 
            this.tb_koutei_cd.BackColor = System.Drawing.Color.PowderBlue;
            this.tb_koutei_cd.Location = new System.Drawing.Point(439, 16);
            this.tb_koutei_cd.Name = "tb_koutei_cd";
            this.tb_koutei_cd.Size = new System.Drawing.Size(33, 19);
            this.tb_koutei_cd.TabIndex = 13;
            this.tb_koutei_cd.DoubleClick += new System.EventHandler(this.tb_koutei_cd_DoubleClick);
            // 
            // btn_hyouji
            // 
            this.btn_hyouji.Location = new System.Drawing.Point(804, 5);
            this.btn_hyouji.Name = "btn_hyouji";
            this.btn_hyouji.Size = new System.Drawing.Size(75, 23);
            this.btn_hyouji.TabIndex = 5;
            this.btn_hyouji.Text = "表示";
            this.btn_hyouji.UseVisualStyleBackColor = true;
            this.btn_hyouji.Click += new System.EventHandler(this.btn_hyouji_Click);
            // 
            // tb_busyo_cd
            // 
            this.tb_busyo_cd.BackColor = System.Drawing.Color.PowderBlue;
            this.tb_busyo_cd.Location = new System.Drawing.Point(137, 16);
            this.tb_busyo_cd.Name = "tb_busyo_cd";
            this.tb_busyo_cd.Size = new System.Drawing.Size(33, 19);
            this.tb_busyo_cd.TabIndex = 12;
            this.tb_busyo_cd.DoubleClick += new System.EventHandler(this.tb_busyo_cd_DoubleClick);
            // 
            // tb_busyo_midasi
            // 
            this.tb_busyo_midasi.BackColor = System.Drawing.Color.NavajoWhite;
            this.tb_busyo_midasi.Location = new System.Drawing.Point(75, 16);
            this.tb_busyo_midasi.Name = "tb_busyo_midasi";
            this.tb_busyo_midasi.ReadOnly = true;
            this.tb_busyo_midasi.Size = new System.Drawing.Size(62, 19);
            this.tb_busyo_midasi.TabIndex = 11;
            this.tb_busyo_midasi.TabStop = false;
            this.tb_busyo_midasi.Text = "部署コード";
            // 
            // tb_koutei_midasi
            // 
            this.tb_koutei_midasi.BackColor = System.Drawing.Color.NavajoWhite;
            this.tb_koutei_midasi.Location = new System.Drawing.Point(377, 16);
            this.tb_koutei_midasi.Name = "tb_koutei_midasi";
            this.tb_koutei_midasi.ReadOnly = true;
            this.tb_koutei_midasi.Size = new System.Drawing.Size(62, 19);
            this.tb_koutei_midasi.TabIndex = 10;
            this.tb_koutei_midasi.TabStop = false;
            this.tb_koutei_midasi.Text = "工程コード";
            // 
            // tb_line_midasi
            // 
            this.tb_line_midasi.BackColor = System.Drawing.Color.NavajoWhite;
            this.tb_line_midasi.Location = new System.Drawing.Point(664, 16);
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
            this.cb_line_sitei.Location = new System.Drawing.Point(594, 18);
            this.cb_line_sitei.Name = "cb_line_sitei";
            this.cb_line_sitei.Size = new System.Drawing.Size(74, 16);
            this.cb_line_sitei.TabIndex = 8;
            this.cb_line_sitei.Text = "ライン指定";
            this.cb_line_sitei.UseVisualStyleBackColor = true;
            this.cb_line_sitei.CheckedChanged += new System.EventHandler(this.cb_line_sitei_CheckedChanged);
            // 
            // cb_koutei_sitei
            // 
            this.cb_koutei_sitei.AutoSize = true;
            this.cb_koutei_sitei.Location = new System.Drawing.Point(305, 18);
            this.cb_koutei_sitei.Name = "cb_koutei_sitei";
            this.cb_koutei_sitei.Size = new System.Drawing.Size(72, 16);
            this.cb_koutei_sitei.TabIndex = 7;
            this.cb_koutei_sitei.Text = "工程指定";
            this.cb_koutei_sitei.UseVisualStyleBackColor = true;
            this.cb_koutei_sitei.CheckedChanged += new System.EventHandler(this.cb_koutei_sitei_CheckedChanged);
            // 
            // cb_busyo_sitei
            // 
            this.cb_busyo_sitei.AutoSize = true;
            this.cb_busyo_sitei.Location = new System.Drawing.Point(6, 18);
            this.cb_busyo_sitei.Name = "cb_busyo_sitei";
            this.cb_busyo_sitei.Size = new System.Drawing.Size(72, 16);
            this.cb_busyo_sitei.TabIndex = 6;
            this.cb_busyo_sitei.Text = "部署指定";
            this.cb_busyo_sitei.UseVisualStyleBackColor = true;
            this.cb_busyo_sitei.CheckedChanged += new System.EventHandler(this.cb_busyo_sitei_CheckedChanged);
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
            this.splitContainer3.Panel1.Controls.Add(this.tb_seisan_yotei_date);
            this.splitContainer3.Panel1.Controls.Add(this.btn_hyouji);
            this.splitContainer3.Panel1.Controls.Add(this.textBox6);
            this.splitContainer3.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.dgv_list);
            this.splitContainer3.Size = new System.Drawing.Size(884, 437);
            this.splitContainer3.SplitterDistance = 107;
            this.splitContainer3.TabIndex = 0;
            // 
            // tb_seisan_yotei_date
            // 
            this.tb_seisan_yotei_date.Location = new System.Drawing.Point(78, 5);
            this.tb_seisan_yotei_date.MaxLength = 10;
            this.tb_seisan_yotei_date.Name = "tb_seisan_yotei_date";
            this.tb_seisan_yotei_date.Size = new System.Drawing.Size(74, 19);
            this.tb_seisan_yotei_date.TabIndex = 0;
            this.tb_seisan_yotei_date.Validating += new System.ComponentModel.CancelEventHandler(this.tb_seisan_yotei_date_Validating);
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox6.Location = new System.Drawing.Point(3, 5);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(75, 19);
            this.textBox6.TabIndex = 7;
            this.textBox6.TabStop = false;
            this.textBox6.Text = "生産予定日";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tb_line_name);
            this.groupBox1.Controls.Add(this.tb_koutei_name);
            this.groupBox1.Controls.Add(this.tb_busyo_name);
            this.groupBox1.Controls.Add(this.tb_line_cd);
            this.groupBox1.Controls.Add(this.tb_koutei_cd);
            this.groupBox1.Controls.Add(this.tb_busyo_cd);
            this.groupBox1.Controls.Add(this.tb_busyo_midasi);
            this.groupBox1.Controls.Add(this.tb_koutei_midasi);
            this.groupBox1.Controls.Add(this.tb_line_midasi);
            this.groupBox1.Controls.Add(this.cb_line_sitei);
            this.groupBox1.Controls.Add(this.cb_koutei_sitei);
            this.groupBox1.Controls.Add(this.cb_busyo_sitei);
            this.groupBox1.Location = new System.Drawing.Point(3, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(874, 47);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "絞込み";
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
            this.splitContainer2.Size = new System.Drawing.Size(884, 476);
            this.splitContainer2.SplitterDistance = 437;
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
            this.btn_hardcopy.Click += new System.EventHandler(this.btn_hardcopy_Click);
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
            this.splitContainer1.SplitterDistance = 60;
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
            // frm_seisan_schedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 562);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "frm_seisan_schedule";
            this.Text = "生産スケジュール";
            this.Load += new System.EventHandler(this.frm_seisan_schedule_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).EndInit();
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.DataGridView dgv_list;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.TextBox tb_line_name;
        private System.Windows.Forms.TextBox tb_koutei_name;
        private System.Windows.Forms.TextBox tb_busyo_name;
        private System.Windows.Forms.TextBox tb_line_cd;
        private System.Windows.Forms.TextBox tb_koutei_cd;
        private System.Windows.Forms.Button btn_hyouji;
        private System.Windows.Forms.TextBox tb_busyo_cd;
        private System.Windows.Forms.TextBox tb_busyo_midasi;
        private System.Windows.Forms.TextBox tb_koutei_midasi;
        private System.Windows.Forms.TextBox tb_line_midasi;
        private System.Windows.Forms.CheckBox cb_line_sitei;
        private System.Windows.Forms.CheckBox cb_koutei_sitei;
        private System.Windows.Forms.CheckBox cb_busyo_sitei;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button btn_hardcopy;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TextBox tb_seisan_yotei_date;
        private System.Windows.Forms.TextBox textBox6;
    }
}