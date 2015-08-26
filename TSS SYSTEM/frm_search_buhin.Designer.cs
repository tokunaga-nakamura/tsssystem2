namespace TSS_SYSTEM
{
    partial class frm_search_buhin
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_kensaku = new System.Windows.Forms.Button();
            this.tb_torihikisaki_name = new System.Windows.Forms.TextBox();
            this.tb_siiresaki_name = new System.Windows.Forms.TextBox();
            this.tb_siire_kbn_name = new System.Windows.Forms.TextBox();
            this.tb_siire_kbn = new System.Windows.Forms.TextBox();
            this.textBox15 = new System.Windows.Forms.TextBox();
            this.tb_siiresaki_cd = new System.Windows.Forms.TextBox();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.tb_torihikisaki_cd = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.tb_maker_name = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.tb_buhin_hosoku = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.tb_buhin_name = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.tb_buhin_cd2 = new System.Windows.Forms.TextBox();
            this.tb_buhin_cd1 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dgv_m = new System.Windows.Forms.DataGridView();
            this.btn_csv = new System.Windows.Forms.Button();
            this.btn_insatu = new System.Windows.Forms.Button();
            this.btn_sentaku = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_m)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.splitContainer1.Size = new System.Drawing.Size(884, 584);
            this.splitContainer1.SplitterDistance = 128;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_kensaku);
            this.groupBox1.Controls.Add(this.tb_torihikisaki_name);
            this.groupBox1.Controls.Add(this.tb_siiresaki_name);
            this.groupBox1.Controls.Add(this.tb_siire_kbn_name);
            this.groupBox1.Controls.Add(this.tb_siire_kbn);
            this.groupBox1.Controls.Add(this.textBox15);
            this.groupBox1.Controls.Add(this.tb_siiresaki_cd);
            this.groupBox1.Controls.Add(this.textBox13);
            this.groupBox1.Controls.Add(this.tb_torihikisaki_cd);
            this.groupBox1.Controls.Add(this.textBox10);
            this.groupBox1.Controls.Add(this.tb_maker_name);
            this.groupBox1.Controls.Add(this.textBox8);
            this.groupBox1.Controls.Add(this.tb_buhin_hosoku);
            this.groupBox1.Controls.Add(this.textBox6);
            this.groupBox1.Controls.Add(this.tb_buhin_name);
            this.groupBox1.Controls.Add(this.textBox4);
            this.groupBox1.Controls.Add(this.tb_buhin_cd2);
            this.groupBox1.Controls.Add(this.tb_buhin_cd1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(880, 124);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "検索条件";
            // 
            // btn_kensaku
            // 
            this.btn_kensaku.Location = new System.Drawing.Point(795, 91);
            this.btn_kensaku.Name = "btn_kensaku";
            this.btn_kensaku.Size = new System.Drawing.Size(75, 23);
            this.btn_kensaku.TabIndex = 19;
            this.btn_kensaku.Text = "検索";
            this.btn_kensaku.UseVisualStyleBackColor = true;
            this.btn_kensaku.Click += new System.EventHandler(this.btn_kensaku_Click);
            // 
            // tb_torihikisaki_name
            // 
            this.tb_torihikisaki_name.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_torihikisaki_name.Location = new System.Drawing.Point(513, 68);
            this.tb_torihikisaki_name.Name = "tb_torihikisaki_name";
            this.tb_torihikisaki_name.ReadOnly = true;
            this.tb_torihikisaki_name.Size = new System.Drawing.Size(206, 19);
            this.tb_torihikisaki_name.TabIndex = 18;
            this.tb_torihikisaki_name.TabStop = false;
            // 
            // tb_siiresaki_name
            // 
            this.tb_siiresaki_name.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_siiresaki_name.Location = new System.Drawing.Point(513, 18);
            this.tb_siiresaki_name.Name = "tb_siiresaki_name";
            this.tb_siiresaki_name.ReadOnly = true;
            this.tb_siiresaki_name.Size = new System.Drawing.Size(206, 19);
            this.tb_siiresaki_name.TabIndex = 17;
            this.tb_siiresaki_name.TabStop = false;
            // 
            // tb_siire_kbn_name
            // 
            this.tb_siire_kbn_name.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_siire_kbn_name.Location = new System.Drawing.Point(488, 43);
            this.tb_siire_kbn_name.Name = "tb_siire_kbn_name";
            this.tb_siire_kbn_name.Size = new System.Drawing.Size(100, 19);
            this.tb_siire_kbn_name.TabIndex = 16;
            // 
            // tb_siire_kbn
            // 
            this.tb_siire_kbn.BackColor = System.Drawing.Color.PowderBlue;
            this.tb_siire_kbn.Location = new System.Drawing.Point(460, 43);
            this.tb_siire_kbn.MaxLength = 2;
            this.tb_siire_kbn.Name = "tb_siire_kbn";
            this.tb_siire_kbn.Size = new System.Drawing.Size(22, 19);
            this.tb_siire_kbn.TabIndex = 15;
            this.tb_siire_kbn.DoubleClick += new System.EventHandler(this.tb_siire_kbn_DoubleClick);
            this.tb_siire_kbn.Validating += new System.ComponentModel.CancelEventHandler(this.tb_siire_kbn_Validating);
            // 
            // textBox15
            // 
            this.textBox15.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox15.Location = new System.Drawing.Point(354, 43);
            this.textBox15.Name = "textBox15";
            this.textBox15.ReadOnly = true;
            this.textBox15.Size = new System.Drawing.Size(100, 19);
            this.textBox15.TabIndex = 14;
            this.textBox15.TabStop = false;
            this.textBox15.Text = "仕入区分";
            // 
            // tb_siiresaki_cd
            // 
            this.tb_siiresaki_cd.BackColor = System.Drawing.Color.PowderBlue;
            this.tb_siiresaki_cd.Location = new System.Drawing.Point(460, 18);
            this.tb_siiresaki_cd.MaxLength = 6;
            this.tb_siiresaki_cd.Name = "tb_siiresaki_cd";
            this.tb_siiresaki_cd.Size = new System.Drawing.Size(47, 19);
            this.tb_siiresaki_cd.TabIndex = 13;
            this.tb_siiresaki_cd.DoubleClick += new System.EventHandler(this.tb_siiresaki_cd_DoubleClick);
            this.tb_siiresaki_cd.Validating += new System.ComponentModel.CancelEventHandler(this.tb_siiresaki_cd_Validating);
            // 
            // textBox13
            // 
            this.textBox13.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox13.Location = new System.Drawing.Point(10, 68);
            this.textBox13.Name = "textBox13";
            this.textBox13.ReadOnly = true;
            this.textBox13.Size = new System.Drawing.Size(100, 19);
            this.textBox13.TabIndex = 12;
            this.textBox13.TabStop = false;
            this.textBox13.Text = "部品補足";
            // 
            // tb_torihikisaki_cd
            // 
            this.tb_torihikisaki_cd.BackColor = System.Drawing.Color.PowderBlue;
            this.tb_torihikisaki_cd.Location = new System.Drawing.Point(460, 68);
            this.tb_torihikisaki_cd.MaxLength = 6;
            this.tb_torihikisaki_cd.Name = "tb_torihikisaki_cd";
            this.tb_torihikisaki_cd.Size = new System.Drawing.Size(47, 19);
            this.tb_torihikisaki_cd.TabIndex = 10;
            this.tb_torihikisaki_cd.DoubleClick += new System.EventHandler(this.tb_torihikisaki_cd_DoubleClick);
            // 
            // textBox10
            // 
            this.textBox10.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox10.Location = new System.Drawing.Point(354, 68);
            this.textBox10.Name = "textBox10";
            this.textBox10.ReadOnly = true;
            this.textBox10.Size = new System.Drawing.Size(100, 19);
            this.textBox10.TabIndex = 9;
            this.textBox10.TabStop = false;
            this.textBox10.Text = "取引先コード";
            // 
            // tb_maker_name
            // 
            this.tb_maker_name.Location = new System.Drawing.Point(116, 93);
            this.tb_maker_name.MaxLength = 40;
            this.tb_maker_name.Name = "tb_maker_name";
            this.tb_maker_name.Size = new System.Drawing.Size(232, 19);
            this.tb_maker_name.TabIndex = 8;
            // 
            // textBox8
            // 
            this.textBox8.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox8.Location = new System.Drawing.Point(354, 18);
            this.textBox8.Name = "textBox8";
            this.textBox8.ReadOnly = true;
            this.textBox8.Size = new System.Drawing.Size(100, 19);
            this.textBox8.TabIndex = 7;
            this.textBox8.TabStop = false;
            this.textBox8.Text = "仕入先コード";
            // 
            // tb_buhin_hosoku
            // 
            this.tb_buhin_hosoku.Location = new System.Drawing.Point(116, 68);
            this.tb_buhin_hosoku.MaxLength = 40;
            this.tb_buhin_hosoku.Name = "tb_buhin_hosoku";
            this.tb_buhin_hosoku.Size = new System.Drawing.Size(232, 19);
            this.tb_buhin_hosoku.TabIndex = 6;
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox6.Location = new System.Drawing.Point(10, 93);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(100, 19);
            this.textBox6.TabIndex = 5;
            this.textBox6.TabStop = false;
            this.textBox6.Text = "メーカー";
            // 
            // tb_buhin_name
            // 
            this.tb_buhin_name.Location = new System.Drawing.Point(116, 43);
            this.tb_buhin_name.MaxLength = 40;
            this.tb_buhin_name.Name = "tb_buhin_name";
            this.tb_buhin_name.Size = new System.Drawing.Size(232, 19);
            this.tb_buhin_name.TabIndex = 4;
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
            this.textBox4.Text = "部品名";
            // 
            // tb_buhin_cd2
            // 
            this.tb_buhin_cd2.Location = new System.Drawing.Point(228, 18);
            this.tb_buhin_cd2.MaxLength = 16;
            this.tb_buhin_cd2.Name = "tb_buhin_cd2";
            this.tb_buhin_cd2.Size = new System.Drawing.Size(106, 19);
            this.tb_buhin_cd2.TabIndex = 2;
            // 
            // tb_buhin_cd1
            // 
            this.tb_buhin_cd1.Location = new System.Drawing.Point(116, 18);
            this.tb_buhin_cd1.MaxLength = 16;
            this.tb_buhin_cd1.Name = "tb_buhin_cd1";
            this.tb_buhin_cd1.Size = new System.Drawing.Size(106, 19);
            this.tb_buhin_cd1.TabIndex = 1;
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
            this.textBox1.Text = "部品コード";
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.splitContainer2.Size = new System.Drawing.Size(884, 452);
            this.splitContainer2.SplitterDistance = 407;
            this.splitContainer2.TabIndex = 0;
            // 
            // dgv_m
            // 
            this.dgv_m.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_m.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_m.Location = new System.Drawing.Point(0, 0);
            this.dgv_m.Name = "dgv_m";
            this.dgv_m.RowTemplate.Height = 21;
            this.dgv_m.Size = new System.Drawing.Size(880, 403);
            this.dgv_m.TabIndex = 0;
            this.dgv_m.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_m_CellMouseDoubleClick);
            // 
            // btn_csv
            // 
            this.btn_csv.Location = new System.Drawing.Point(172, 3);
            this.btn_csv.Name = "btn_csv";
            this.btn_csv.Size = new System.Drawing.Size(75, 23);
            this.btn_csv.TabIndex = 3;
            this.btn_csv.Text = "CSV出力";
            this.btn_csv.UseVisualStyleBackColor = true;
            this.btn_csv.Click += new System.EventHandler(this.btn_csv_Click);
            // 
            // btn_insatu
            // 
            this.btn_insatu.Location = new System.Drawing.Point(91, 3);
            this.btn_insatu.Name = "btn_insatu";
            this.btn_insatu.Size = new System.Drawing.Size(75, 23);
            this.btn_insatu.TabIndex = 2;
            this.btn_insatu.Text = "印刷";
            this.btn_insatu.UseVisualStyleBackColor = true;
            // 
            // btn_sentaku
            // 
            this.btn_sentaku.Location = new System.Drawing.Point(10, 3);
            this.btn_sentaku.Name = "btn_sentaku";
            this.btn_sentaku.Size = new System.Drawing.Size(75, 23);
            this.btn_sentaku.TabIndex = 1;
            this.btn_sentaku.Text = "選択";
            this.btn_sentaku.UseVisualStyleBackColor = true;
            this.btn_sentaku.Click += new System.EventHandler(this.btn_sentaku_Click_1);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(795, 3);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 0;
            this.btn_cancel.Text = "キャンセル";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // frm_search_buhin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OliveDrab;
            this.ClientSize = new System.Drawing.Size(884, 584);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainer1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "frm_search_buhin";
            this.Text = "部品検索";
            this.Load += new System.EventHandler(this.frm_search_buhin_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_m)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_kensaku;
        private System.Windows.Forms.TextBox tb_torihikisaki_name;
        private System.Windows.Forms.TextBox tb_siiresaki_name;
        private System.Windows.Forms.TextBox tb_siire_kbn_name;
        private System.Windows.Forms.TextBox tb_siire_kbn;
        private System.Windows.Forms.TextBox textBox15;
        private System.Windows.Forms.TextBox tb_siiresaki_cd;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.TextBox tb_torihikisaki_cd;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.TextBox tb_maker_name;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox tb_buhin_hosoku;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox tb_buhin_name;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox tb_buhin_cd2;
        private System.Windows.Forms.TextBox tb_buhin_cd1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView dgv_m;
        private System.Windows.Forms.Button btn_csv;
        private System.Windows.Forms.Button btn_insatu;
        private System.Windows.Forms.Button btn_sentaku;
        private System.Windows.Forms.Button btn_cancel;
    }
}