namespace TSS_SYSTEM
{
    partial class frm_search_juchu
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_kensaku = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rb_uriage_all = new System.Windows.Forms.RadioButton();
            this.rb_uriage_knryou = new System.Windows.Forms.RadioButton();
            this.rb_uriage_tochuu = new System.Windows.Forms.RadioButton();
            this.rb_miuriage = new System.Windows.Forms.RadioButton();
            this.tb_seihin_name = new System.Windows.Forms.TextBox();
            this.tb_seihin_cd = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.tb_juchu_cd2_2 = new System.Windows.Forms.TextBox();
            this.tb_juchu_cd2_1 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.tb_juchu_cd1_2 = new System.Windows.Forms.TextBox();
            this.tb_juchu_cd1_1 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.tb_torihikisaki_cd2 = new System.Windows.Forms.TextBox();
            this.tb_torihikisaki_cd1 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dgv_m = new System.Windows.Forms.DataGridView();
            this.btn_csv = new System.Windows.Forms.Button();
            this.btn_insatu = new System.Windows.Forms.Button();
            this.btn_sentaku = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.tb_create_date1 = new System.Windows.Forms.TextBox();
            this.tb_create_date2 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_m)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(884, 539);
            this.splitContainer1.SplitterDistance = 150;
            this.splitContainer1.TabIndex = 1;
            this.splitContainer1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tb_create_date2);
            this.groupBox1.Controls.Add(this.tb_create_date1);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.btn_kensaku);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.tb_seihin_name);
            this.groupBox1.Controls.Add(this.tb_seihin_cd);
            this.groupBox1.Controls.Add(this.textBox10);
            this.groupBox1.Controls.Add(this.tb_juchu_cd2_2);
            this.groupBox1.Controls.Add(this.tb_juchu_cd2_1);
            this.groupBox1.Controls.Add(this.textBox7);
            this.groupBox1.Controls.Add(this.tb_juchu_cd1_2);
            this.groupBox1.Controls.Add(this.tb_juchu_cd1_1);
            this.groupBox1.Controls.Add(this.textBox4);
            this.groupBox1.Controls.Add(this.tb_torihikisaki_cd2);
            this.groupBox1.Controls.Add(this.tb_torihikisaki_cd1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(880, 146);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "検索条件";
            // 
            // btn_kensaku
            // 
            this.btn_kensaku.Location = new System.Drawing.Point(795, 105);
            this.btn_kensaku.Name = "btn_kensaku";
            this.btn_kensaku.Size = new System.Drawing.Size(75, 23);
            this.btn_kensaku.TabIndex = 13;
            this.btn_kensaku.Text = "検索";
            this.btn_kensaku.UseVisualStyleBackColor = true;
            this.btn_kensaku.Click += new System.EventHandler(this.btn_kensaku_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rb_uriage_all);
            this.groupBox2.Controls.Add(this.rb_uriage_knryou);
            this.groupBox2.Controls.Add(this.rb_uriage_tochuu);
            this.groupBox2.Controls.Add(this.rb_miuriage);
            this.groupBox2.Location = new System.Drawing.Point(456, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(202, 112);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "売上状況";
            // 
            // rb_uriage_all
            // 
            this.rb_uriage_all.AutoSize = true;
            this.rb_uriage_all.Checked = true;
            this.rb_uriage_all.Location = new System.Drawing.Point(6, 84);
            this.rb_uriage_all.Name = "rb_uriage_all";
            this.rb_uriage_all.Size = new System.Drawing.Size(44, 16);
            this.rb_uriage_all.TabIndex = 3;
            this.rb_uriage_all.TabStop = true;
            this.rb_uriage_all.Text = "全て";
            this.rb_uriage_all.UseVisualStyleBackColor = true;
            // 
            // rb_uriage_knryou
            // 
            this.rb_uriage_knryou.AutoSize = true;
            this.rb_uriage_knryou.Location = new System.Drawing.Point(6, 62);
            this.rb_uriage_knryou.Name = "rb_uriage_knryou";
            this.rb_uriage_knryou.Size = new System.Drawing.Size(71, 16);
            this.rb_uriage_knryou.TabIndex = 2;
            this.rb_uriage_knryou.Text = "売上完了";
            this.rb_uriage_knryou.UseVisualStyleBackColor = true;
            // 
            // rb_uriage_tochuu
            // 
            this.rb_uriage_tochuu.AutoSize = true;
            this.rb_uriage_tochuu.Location = new System.Drawing.Point(6, 40);
            this.rb_uriage_tochuu.Name = "rb_uriage_tochuu";
            this.rb_uriage_tochuu.Size = new System.Drawing.Size(175, 16);
            this.rb_uriage_tochuu.TabIndex = 1;
            this.rb_uriage_tochuu.Text = "売上途中（受注数ノ≠売上数）";
            this.rb_uriage_tochuu.UseVisualStyleBackColor = true;
            // 
            // rb_miuriage
            // 
            this.rb_miuriage.AutoSize = true;
            this.rb_miuriage.Location = new System.Drawing.Point(6, 18);
            this.rb_miuriage.Name = "rb_miuriage";
            this.rb_miuriage.Size = new System.Drawing.Size(59, 16);
            this.rb_miuriage.TabIndex = 0;
            this.rb_miuriage.Text = "未売上";
            this.rb_miuriage.UseVisualStyleBackColor = true;
            // 
            // tb_seihin_name
            // 
            this.tb_seihin_name.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_seihin_name.Location = new System.Drawing.Point(230, 93);
            this.tb_seihin_name.Name = "tb_seihin_name";
            this.tb_seihin_name.ReadOnly = true;
            this.tb_seihin_name.Size = new System.Drawing.Size(220, 19);
            this.tb_seihin_name.TabIndex = 11;
            this.tb_seihin_name.TabStop = false;
            // 
            // tb_seihin_cd
            // 
            this.tb_seihin_cd.BackColor = System.Drawing.Color.PowderBlue;
            this.tb_seihin_cd.Location = new System.Drawing.Point(116, 93);
            this.tb_seihin_cd.MaxLength = 16;
            this.tb_seihin_cd.Name = "tb_seihin_cd";
            this.tb_seihin_cd.Size = new System.Drawing.Size(108, 19);
            this.tb_seihin_cd.TabIndex = 10;
            this.tb_seihin_cd.DoubleClick += new System.EventHandler(this.tb_seihin_cd_DoubleClick);
            this.tb_seihin_cd.Validating += new System.ComponentModel.CancelEventHandler(this.tb_seihin_cd_Validating);
            // 
            // textBox10
            // 
            this.textBox10.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox10.Location = new System.Drawing.Point(10, 93);
            this.textBox10.Name = "textBox10";
            this.textBox10.ReadOnly = true;
            this.textBox10.Size = new System.Drawing.Size(100, 19);
            this.textBox10.TabIndex = 9;
            this.textBox10.TabStop = false;
            this.textBox10.Text = "製品コード";
            // 
            // tb_juchu_cd2_2
            // 
            this.tb_juchu_cd2_2.Location = new System.Drawing.Point(230, 68);
            this.tb_juchu_cd2_2.MaxLength = 16;
            this.tb_juchu_cd2_2.Name = "tb_juchu_cd2_2";
            this.tb_juchu_cd2_2.Size = new System.Drawing.Size(108, 19);
            this.tb_juchu_cd2_2.TabIndex = 8;
            // 
            // tb_juchu_cd2_1
            // 
            this.tb_juchu_cd2_1.Location = new System.Drawing.Point(116, 68);
            this.tb_juchu_cd2_1.MaxLength = 16;
            this.tb_juchu_cd2_1.Name = "tb_juchu_cd2_1";
            this.tb_juchu_cd2_1.Size = new System.Drawing.Size(108, 19);
            this.tb_juchu_cd2_1.TabIndex = 7;
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox7.Location = new System.Drawing.Point(10, 68);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(100, 19);
            this.textBox7.TabIndex = 6;
            this.textBox7.TabStop = false;
            this.textBox7.Text = "受注コード２";
            // 
            // tb_juchu_cd1_2
            // 
            this.tb_juchu_cd1_2.Location = new System.Drawing.Point(230, 43);
            this.tb_juchu_cd1_2.MaxLength = 16;
            this.tb_juchu_cd1_2.Name = "tb_juchu_cd1_2";
            this.tb_juchu_cd1_2.Size = new System.Drawing.Size(108, 19);
            this.tb_juchu_cd1_2.TabIndex = 5;
            // 
            // tb_juchu_cd1_1
            // 
            this.tb_juchu_cd1_1.Location = new System.Drawing.Point(116, 43);
            this.tb_juchu_cd1_1.MaxLength = 16;
            this.tb_juchu_cd1_1.Name = "tb_juchu_cd1_1";
            this.tb_juchu_cd1_1.Size = new System.Drawing.Size(108, 19);
            this.tb_juchu_cd1_1.TabIndex = 4;
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
            this.textBox4.Text = "受注コード１";
            // 
            // tb_torihikisaki_cd2
            // 
            this.tb_torihikisaki_cd2.Location = new System.Drawing.Point(173, 18);
            this.tb_torihikisaki_cd2.MaxLength = 6;
            this.tb_torihikisaki_cd2.Name = "tb_torihikisaki_cd2";
            this.tb_torihikisaki_cd2.Size = new System.Drawing.Size(51, 19);
            this.tb_torihikisaki_cd2.TabIndex = 2;
            // 
            // tb_torihikisaki_cd1
            // 
            this.tb_torihikisaki_cd1.Location = new System.Drawing.Point(116, 18);
            this.tb_torihikisaki_cd1.MaxLength = 6;
            this.tb_torihikisaki_cd1.Name = "tb_torihikisaki_cd1";
            this.tb_torihikisaki_cd1.Size = new System.Drawing.Size(51, 19);
            this.tb_torihikisaki_cd1.TabIndex = 1;
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
            this.textBox1.Text = "取引先コード";
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
            this.splitContainer2.Size = new System.Drawing.Size(884, 385);
            this.splitContainer2.SplitterDistance = 345;
            this.splitContainer2.TabIndex = 0;
            this.splitContainer2.TabStop = false;
            // 
            // dgv_m
            // 
            this.dgv_m.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_m.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_m.Location = new System.Drawing.Point(0, 0);
            this.dgv_m.Name = "dgv_m";
            this.dgv_m.RowTemplate.Height = 21;
            this.dgv_m.Size = new System.Drawing.Size(880, 341);
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
            this.btn_sentaku.Click += new System.EventHandler(this.btn_sentaku_Click);
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
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox2.Location = new System.Drawing.Point(10, 118);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(100, 19);
            this.textBox2.TabIndex = 14;
            this.textBox2.TabStop = false;
            this.textBox2.Text = "登録日";
            // 
            // tb_create_date1
            // 
            this.tb_create_date1.Location = new System.Drawing.Point(116, 118);
            this.tb_create_date1.Name = "tb_create_date1";
            this.tb_create_date1.Size = new System.Drawing.Size(100, 19);
            this.tb_create_date1.TabIndex = 15;
            this.tb_create_date1.Validating += new System.ComponentModel.CancelEventHandler(this.tb_create_date1_Validating);
            // 
            // tb_create_date2
            // 
            this.tb_create_date2.Location = new System.Drawing.Point(222, 118);
            this.tb_create_date2.Name = "tb_create_date2";
            this.tb_create_date2.Size = new System.Drawing.Size(100, 19);
            this.tb_create_date2.TabIndex = 16;
            this.tb_create_date2.Validating += new System.ComponentModel.CancelEventHandler(this.tb_create_date2_Validating);
            // 
            // frm_search_juchu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OliveDrab;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "frm_search_juchu";
            this.Text = "受注検索";
            this.Load += new System.EventHandler(this.frm_search_juchu_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_m)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_kensaku;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rb_uriage_all;
        private System.Windows.Forms.RadioButton rb_uriage_knryou;
        private System.Windows.Forms.RadioButton rb_uriage_tochuu;
        private System.Windows.Forms.RadioButton rb_miuriage;
        private System.Windows.Forms.TextBox tb_seihin_name;
        private System.Windows.Forms.TextBox tb_seihin_cd;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.TextBox tb_juchu_cd2_2;
        private System.Windows.Forms.TextBox tb_juchu_cd2_1;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox tb_juchu_cd1_2;
        private System.Windows.Forms.TextBox tb_juchu_cd1_1;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox tb_torihikisaki_cd2;
        private System.Windows.Forms.TextBox tb_torihikisaki_cd1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dgv_m;
        private System.Windows.Forms.Button btn_csv;
        private System.Windows.Forms.Button btn_insatu;
        private System.Windows.Forms.Button btn_sentaku;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.TextBox tb_create_date2;
        private System.Windows.Forms.TextBox tb_create_date1;
        private System.Windows.Forms.TextBox textBox2;
    }
}