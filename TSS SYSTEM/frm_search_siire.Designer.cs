namespace TSS_SYSTEM
{
    partial class frm_search_siire
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
            this.btn_csv = new System.Windows.Forms.Button();
            this.btn_insatu = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.tb_buhin_name = new System.Windows.Forms.TextBox();
            this.tb_buhin_cd = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.btn_kensaku = new System.Windows.Forms.Button();
            this.tb_torihikisaki_name = new System.Windows.Forms.TextBox();
            this.dgv_m = new System.Windows.Forms.DataGridView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.btn_sentaku = new System.Windows.Forms.Button();
            this.tb_siire_date2 = new System.Windows.Forms.TextBox();
            this.tb_siire_date1 = new System.Windows.Forms.TextBox();
            this.tb_torihikisaki_cd = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.tb_siire_no2 = new System.Windows.Forms.TextBox();
            this.tb_siire_no1 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_m)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_csv
            // 
            this.btn_csv.Location = new System.Drawing.Point(172, 3);
            this.btn_csv.Name = "btn_csv";
            this.btn_csv.Size = new System.Drawing.Size(75, 23);
            this.btn_csv.TabIndex = 2;
            this.btn_csv.Text = "CSV出力";
            this.btn_csv.UseVisualStyleBackColor = true;
            this.btn_csv.Click += new System.EventHandler(this.btn_csv_Click);
            // 
            // btn_insatu
            // 
            this.btn_insatu.Location = new System.Drawing.Point(91, 3);
            this.btn_insatu.Name = "btn_insatu";
            this.btn_insatu.Size = new System.Drawing.Size(75, 23);
            this.btn_insatu.TabIndex = 1;
            this.btn_insatu.Text = "印刷";
            this.btn_insatu.UseVisualStyleBackColor = true;
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(795, 3);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 3;
            this.btn_cancel.Text = "キャンセル";
            this.btn_cancel.UseVisualStyleBackColor = true;
            // 
            // tb_buhin_name
            // 
            this.tb_buhin_name.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_buhin_name.Location = new System.Drawing.Point(232, 93);
            this.tb_buhin_name.Name = "tb_buhin_name";
            this.tb_buhin_name.ReadOnly = true;
            this.tb_buhin_name.Size = new System.Drawing.Size(284, 19);
            this.tb_buhin_name.TabIndex = 16;
            this.tb_buhin_name.TabStop = false;
            // 
            // tb_buhin_cd
            // 
            this.tb_buhin_cd.BackColor = System.Drawing.Color.PowderBlue;
            this.tb_buhin_cd.Location = new System.Drawing.Point(116, 93);
            this.tb_buhin_cd.MaxLength = 16;
            this.tb_buhin_cd.Name = "tb_buhin_cd";
            this.tb_buhin_cd.Size = new System.Drawing.Size(110, 19);
            this.tb_buhin_cd.TabIndex = 7;
            this.tb_buhin_cd.DoubleClick += new System.EventHandler(this.tb_buhinn_cd_DoubleClick);
            this.tb_buhin_cd.Validating += new System.ComponentModel.CancelEventHandler(this.tb_buhin_cd_Validating);
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox2.Location = new System.Drawing.Point(10, 93);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(100, 19);
            this.textBox2.TabIndex = 14;
            this.textBox2.TabStop = false;
            this.textBox2.Text = "部品コード";
            // 
            // btn_kensaku
            // 
            this.btn_kensaku.Location = new System.Drawing.Point(795, 93);
            this.btn_kensaku.Name = "btn_kensaku";
            this.btn_kensaku.Size = new System.Drawing.Size(75, 23);
            this.btn_kensaku.TabIndex = 8;
            this.btn_kensaku.Text = "検索";
            this.btn_kensaku.UseVisualStyleBackColor = true;
            this.btn_kensaku.Click += new System.EventHandler(this.btn_kensaku_Click);
            // 
            // tb_torihikisaki_name
            // 
            this.tb_torihikisaki_name.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_torihikisaki_name.Location = new System.Drawing.Point(172, 43);
            this.tb_torihikisaki_name.Name = "tb_torihikisaki_name";
            this.tb_torihikisaki_name.ReadOnly = true;
            this.tb_torihikisaki_name.Size = new System.Drawing.Size(284, 19);
            this.tb_torihikisaki_name.TabIndex = 8;
            this.tb_torihikisaki_name.TabStop = false;
            // 
            // dgv_m
            // 
            this.dgv_m.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_m.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_m.Location = new System.Drawing.Point(0, 0);
            this.dgv_m.Name = "dgv_m";
            this.dgv_m.RowTemplate.Height = 21;
            this.dgv_m.Size = new System.Drawing.Size(880, 356);
            this.dgv_m.TabIndex = 0;
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
            this.splitContainer2.Panel1.Controls.Add(this.dgv_m);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.btn_csv);
            this.splitContainer2.Panel2.Controls.Add(this.btn_insatu);
            this.splitContainer2.Panel2.Controls.Add(this.btn_sentaku);
            this.splitContainer2.Panel2.Controls.Add(this.btn_cancel);
            this.splitContainer2.Size = new System.Drawing.Size(884, 401);
            this.splitContainer2.SplitterDistance = 360;
            this.splitContainer2.TabIndex = 0;
            this.splitContainer2.TabStop = false;
            // 
            // btn_sentaku
            // 
            this.btn_sentaku.Location = new System.Drawing.Point(10, 3);
            this.btn_sentaku.Name = "btn_sentaku";
            this.btn_sentaku.Size = new System.Drawing.Size(75, 23);
            this.btn_sentaku.TabIndex = 0;
            this.btn_sentaku.Text = "選択";
            this.btn_sentaku.UseVisualStyleBackColor = true;
            // 
            // tb_siire_date2
            // 
            this.tb_siire_date2.Location = new System.Drawing.Point(196, 68);
            this.tb_siire_date2.MaxLength = 10;
            this.tb_siire_date2.Name = "tb_siire_date2";
            this.tb_siire_date2.Size = new System.Drawing.Size(74, 19);
            this.tb_siire_date2.TabIndex = 4;
            this.tb_siire_date2.Validating += new System.ComponentModel.CancelEventHandler(this.tb_siire_date2_Validating);
            // 
            // tb_siire_date1
            // 
            this.tb_siire_date1.Location = new System.Drawing.Point(116, 68);
            this.tb_siire_date1.MaxLength = 10;
            this.tb_siire_date1.Name = "tb_siire_date1";
            this.tb_siire_date1.Size = new System.Drawing.Size(74, 19);
            this.tb_siire_date1.TabIndex = 3;
            this.tb_siire_date1.Validating += new System.ComponentModel.CancelEventHandler(this.tb_siire_date1_Validating);
            // 
            // tb_torihikisaki_cd
            // 
            this.tb_torihikisaki_cd.BackColor = System.Drawing.Color.PowderBlue;
            this.tb_torihikisaki_cd.Location = new System.Drawing.Point(116, 43);
            this.tb_torihikisaki_cd.MaxLength = 6;
            this.tb_torihikisaki_cd.Name = "tb_torihikisaki_cd";
            this.tb_torihikisaki_cd.Size = new System.Drawing.Size(50, 19);
            this.tb_torihikisaki_cd.TabIndex = 2;
            this.tb_torihikisaki_cd.DoubleClick += new System.EventHandler(this.tb_torihikisaki_cd_DoubleClick);
            this.tb_torihikisaki_cd.Validating += new System.ComponentModel.CancelEventHandler(this.tb_torihikisaki_cd_Validating);
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox4.Location = new System.Drawing.Point(10, 43);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(100, 19);
            this.textBox4.TabIndex = 3;
            this.textBox4.TabStop = false;
            this.textBox4.Text = "取引先コード";
            // 
            // tb_siire_no2
            // 
            this.tb_siire_no2.Location = new System.Drawing.Point(196, 18);
            this.tb_siire_no2.MaxLength = 10;
            this.tb_siire_no2.Name = "tb_siire_no2";
            this.tb_siire_no2.Size = new System.Drawing.Size(74, 19);
            this.tb_siire_no2.TabIndex = 1;
            this.tb_siire_no2.Validating += new System.ComponentModel.CancelEventHandler(this.tb_siire_date2_Validating);
            // 
            // tb_siire_no1
            // 
            this.tb_siire_no1.Location = new System.Drawing.Point(116, 18);
            this.tb_siire_no1.MaxLength = 10;
            this.tb_siire_no1.Name = "tb_siire_no1";
            this.tb_siire_no1.Size = new System.Drawing.Size(74, 19);
            this.tb_siire_no1.TabIndex = 0;
            this.tb_siire_no1.Validating += new System.ComponentModel.CancelEventHandler(this.tb_siire_date1_Validating);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox1.Location = new System.Drawing.Point(10, 18);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(100, 19);
            this.textBox1.TabIndex = 0;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "仕入番号";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.OliveDrab;
            this.groupBox1.Controls.Add(this.tb_buhin_name);
            this.groupBox1.Controls.Add(this.tb_buhin_cd);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.btn_kensaku);
            this.groupBox1.Controls.Add(this.tb_torihikisaki_name);
            this.groupBox1.Controls.Add(this.tb_siire_date2);
            this.groupBox1.Controls.Add(this.tb_siire_date1);
            this.groupBox1.Controls.Add(this.textBox6);
            this.groupBox1.Controls.Add(this.tb_torihikisaki_cd);
            this.groupBox1.Controls.Add(this.textBox4);
            this.groupBox1.Controls.Add(this.tb_siire_no2);
            this.groupBox1.Controls.Add(this.tb_siire_no1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(880, 131);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "検索条件";
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox6.Location = new System.Drawing.Point(10, 68);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(100, 19);
            this.textBox6.TabIndex = 5;
            this.textBox6.TabStop = false;
            this.textBox6.Text = "仕入計上日";
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
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(884, 540);
            this.splitContainer1.SplitterDistance = 135;
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
            // frm_search_siire
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 562);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "frm_search_siire";
            this.Text = "仕入検索";
            this.Load += new System.EventHandler(this.frm_search_siire_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_m)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_csv;
        private System.Windows.Forms.Button btn_insatu;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.TextBox tb_buhin_name;
        private System.Windows.Forms.TextBox tb_buhin_cd;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button btn_kensaku;
        private System.Windows.Forms.TextBox tb_torihikisaki_name;
        private System.Windows.Forms.DataGridView dgv_m;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button btn_sentaku;
        private System.Windows.Forms.TextBox tb_siire_date2;
        private System.Windows.Forms.TextBox tb_siire_date1;
        private System.Windows.Forms.TextBox tb_torihikisaki_cd;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox tb_siire_no2;
        private System.Windows.Forms.TextBox tb_siire_no1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.StatusStrip statusStrip1;
    }
}