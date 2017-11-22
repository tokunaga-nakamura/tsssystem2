namespace TSS_SYSTEM
{
    partial class frm_buhin_nyusyukko_rireki
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
            this.btn_syuryou = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_buhin_syori_date2 = new System.Windows.Forms.TextBox();
            this.tb_buhin_syori_date1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.btn_kensaku = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rb_uriage_all = new System.Windows.Forms.RadioButton();
            this.rb_uriage_syori = new System.Windows.Forms.RadioButton();
            this.rb_gamen_syori = new System.Windows.Forms.RadioButton();
            this.dgv_m = new System.Windows.Forms.DataGridView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tb_buhin_cd2 = new System.Windows.Forms.TextBox();
            this.tb_buhin_cd1 = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rb_idou = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.rb_syukko = new System.Windows.Forms.RadioButton();
            this.rb_nyuko = new System.Windows.Forms.RadioButton();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.tb_torihikisaki_cd2 = new System.Windows.Forms.TextBox();
            this.tb_torihikisaki_cd1 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_m)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_csv
            // 
            this.btn_csv.Location = new System.Drawing.Point(91, 3);
            this.btn_csv.Name = "btn_csv";
            this.btn_csv.Size = new System.Drawing.Size(75, 23);
            this.btn_csv.TabIndex = 16;
            this.btn_csv.Text = "CSV出力";
            this.btn_csv.UseVisualStyleBackColor = true;
            this.btn_csv.Click += new System.EventHandler(this.btn_csv_Click);
            // 
            // btn_insatu
            // 
            this.btn_insatu.Location = new System.Drawing.Point(10, 3);
            this.btn_insatu.Name = "btn_insatu";
            this.btn_insatu.Size = new System.Drawing.Size(75, 23);
            this.btn_insatu.TabIndex = 15;
            this.btn_insatu.Text = "印刷";
            this.btn_insatu.UseVisualStyleBackColor = true;
            this.btn_insatu.Click += new System.EventHandler(this.btn_insatu_Click);
            // 
            // btn_syuryou
            // 
            this.btn_syuryou.Location = new System.Drawing.Point(795, 3);
            this.btn_syuryou.Name = "btn_syuryou";
            this.btn_syuryou.Size = new System.Drawing.Size(75, 23);
            this.btn_syuryou.TabIndex = 17;
            this.btn_syuryou.Text = "終了";
            this.btn_syuryou.UseVisualStyleBackColor = true;
            this.btn_syuryou.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(159, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 20;
            this.label4.Text = "～";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(193, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 18;
            this.label2.Text = "～";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(136, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 17;
            this.label1.Text = "～";
            // 
            // tb_buhin_syori_date2
            // 
            this.tb_buhin_syori_date2.Location = new System.Drawing.Point(182, 18);
            this.tb_buhin_syori_date2.Name = "tb_buhin_syori_date2";
            this.tb_buhin_syori_date2.Size = new System.Drawing.Size(68, 19);
            this.tb_buhin_syori_date2.TabIndex = 2;
            this.tb_buhin_syori_date2.Validating += new System.ComponentModel.CancelEventHandler(this.tb_buhin_syori_date2_Validating);
            // 
            // tb_buhin_syori_date1
            // 
            this.tb_buhin_syori_date1.Location = new System.Drawing.Point(85, 18);
            this.tb_buhin_syori_date1.Name = "tb_buhin_syori_date1";
            this.tb_buhin_syori_date1.Size = new System.Drawing.Size(68, 19);
            this.tb_buhin_syori_date1.TabIndex = 1;
            this.tb_buhin_syori_date1.Validating += new System.ComponentModel.CancelEventHandler(this.tb_buhin_syori_date1_Validating);
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox2.Location = new System.Drawing.Point(10, 18);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(75, 19);
            this.textBox2.TabIndex = 14;
            this.textBox2.TabStop = false;
            this.textBox2.Text = "処理日";
            // 
            // btn_kensaku
            // 
            this.btn_kensaku.Location = new System.Drawing.Point(799, 100);
            this.btn_kensaku.Name = "btn_kensaku";
            this.btn_kensaku.Size = new System.Drawing.Size(75, 23);
            this.btn_kensaku.TabIndex = 14;
            this.btn_kensaku.Text = "検索";
            this.btn_kensaku.UseVisualStyleBackColor = true;
            this.btn_kensaku.Click += new System.EventHandler(this.btn_kensaku_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rb_uriage_all);
            this.groupBox2.Controls.Add(this.rb_uriage_syori);
            this.groupBox2.Controls.Add(this.rb_gamen_syori);
            this.groupBox2.Location = new System.Drawing.Point(532, 18);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(229, 87);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "処理区分";
            // 
            // rb_uriage_all
            // 
            this.rb_uriage_all.AutoSize = true;
            this.rb_uriage_all.Checked = true;
            this.rb_uriage_all.Location = new System.Drawing.Point(6, 62);
            this.rb_uriage_all.Name = "rb_uriage_all";
            this.rb_uriage_all.Size = new System.Drawing.Size(44, 16);
            this.rb_uriage_all.TabIndex = 13;
            this.rb_uriage_all.TabStop = true;
            this.rb_uriage_all.Text = "全て";
            this.rb_uriage_all.UseVisualStyleBackColor = true;
            // 
            // rb_uriage_syori
            // 
            this.rb_uriage_syori.AutoSize = true;
            this.rb_uriage_syori.Location = new System.Drawing.Point(6, 40);
            this.rb_uriage_syori.Name = "rb_uriage_syori";
            this.rb_uriage_syori.Size = new System.Drawing.Size(138, 16);
            this.rb_uriage_syori.TabIndex = 12;
            this.rb_uriage_syori.Text = "売上で処理したもの(02)";
            this.rb_uriage_syori.UseVisualStyleBackColor = true;
            // 
            // rb_gamen_syori
            // 
            this.rb_gamen_syori.AutoSize = true;
            this.rb_gamen_syori.Location = new System.Drawing.Point(6, 18);
            this.rb_gamen_syori.Name = "rb_gamen_syori";
            this.rb_gamen_syori.Size = new System.Drawing.Size(198, 16);
            this.rb_gamen_syori.TabIndex = 11;
            this.rb_gamen_syori.Text = "入出庫移動画面で処理したもの(01)";
            this.rb_gamen_syori.UseVisualStyleBackColor = true;
            // 
            // dgv_m
            // 
            this.dgv_m.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_m.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_m.Location = new System.Drawing.Point(0, 0);
            this.dgv_m.Name = "dgv_m";
            this.dgv_m.RowTemplate.Height = 21;
            this.dgv_m.Size = new System.Drawing.Size(880, 357);
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
            this.splitContainer2.Panel2.Controls.Add(this.btn_syuryou);
            this.splitContainer2.Size = new System.Drawing.Size(884, 423);
            this.splitContainer2.SplitterDistance = 361;
            this.splitContainer2.TabIndex = 0;
            this.splitContainer2.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 540);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(884, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tb_buhin_cd2
            // 
            this.tb_buhin_cd2.BackColor = System.Drawing.Color.PowderBlue;
            this.tb_buhin_cd2.Location = new System.Drawing.Point(210, 68);
            this.tb_buhin_cd2.MaxLength = 16;
            this.tb_buhin_cd2.Name = "tb_buhin_cd2";
            this.tb_buhin_cd2.Size = new System.Drawing.Size(108, 19);
            this.tb_buhin_cd2.TabIndex = 6;
            this.tb_buhin_cd2.DoubleClick += new System.EventHandler(this.tb_buhin_cd2_DoubleClick);
            this.tb_buhin_cd2.Validating += new System.ComponentModel.CancelEventHandler(this.tb_buhin_cd2_Validating);
            // 
            // tb_buhin_cd1
            // 
            this.tb_buhin_cd1.BackColor = System.Drawing.Color.PowderBlue;
            this.tb_buhin_cd1.Location = new System.Drawing.Point(85, 68);
            this.tb_buhin_cd1.MaxLength = 16;
            this.tb_buhin_cd1.Name = "tb_buhin_cd1";
            this.tb_buhin_cd1.Size = new System.Drawing.Size(108, 19);
            this.tb_buhin_cd1.TabIndex = 5;
            this.tb_buhin_cd1.DoubleClick += new System.EventHandler(this.tb_buhin_cd1_DoubleClick);
            this.tb_buhin_cd1.Validating += new System.ComponentModel.CancelEventHandler(this.tb_buhin_cd1_Validating);
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
            this.splitContainer1.Size = new System.Drawing.Size(884, 562);
            this.splitContainer1.SplitterDistance = 135;
            this.splitContainer1.TabIndex = 3;
            this.splitContainer1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tb_buhin_syori_date2);
            this.groupBox1.Controls.Add(this.tb_buhin_syori_date1);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.btn_kensaku);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.tb_buhin_cd2);
            this.groupBox1.Controls.Add(this.tb_buhin_cd1);
            this.groupBox1.Controls.Add(this.textBox4);
            this.groupBox1.Controls.Add(this.tb_torihikisaki_cd2);
            this.groupBox1.Controls.Add(this.tb_torihikisaki_cd1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(880, 131);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "検索条件";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rb_idou);
            this.groupBox3.Controls.Add(this.radioButton1);
            this.groupBox3.Controls.Add(this.rb_syukko);
            this.groupBox3.Controls.Add(this.rb_nyuko);
            this.groupBox3.Location = new System.Drawing.Point(324, 18);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(202, 105);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "入出庫移動区分";
            // 
            // rb_idou
            // 
            this.rb_idou.AutoSize = true;
            this.rb_idou.Location = new System.Drawing.Point(6, 60);
            this.rb_idou.Name = "rb_idou";
            this.rb_idou.Size = new System.Drawing.Size(67, 16);
            this.rb_idou.TabIndex = 9;
            this.rb_idou.Text = "移動(03)";
            this.rb_idou.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 80);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(44, 16);
            this.radioButton1.TabIndex = 10;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "全て";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // rb_syukko
            // 
            this.rb_syukko.AutoSize = true;
            this.rb_syukko.Location = new System.Drawing.Point(6, 38);
            this.rb_syukko.Name = "rb_syukko";
            this.rb_syukko.Size = new System.Drawing.Size(67, 16);
            this.rb_syukko.TabIndex = 8;
            this.rb_syukko.Text = "出庫(02)";
            this.rb_syukko.UseVisualStyleBackColor = true;
            // 
            // rb_nyuko
            // 
            this.rb_nyuko.AutoSize = true;
            this.rb_nyuko.Location = new System.Drawing.Point(6, 18);
            this.rb_nyuko.Name = "rb_nyuko";
            this.rb_nyuko.Size = new System.Drawing.Size(67, 16);
            this.rb_nyuko.TabIndex = 7;
            this.rb_nyuko.Text = "入庫(01)";
            this.rb_nyuko.UseVisualStyleBackColor = true;
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox4.Location = new System.Drawing.Point(10, 68);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(75, 19);
            this.textBox4.TabIndex = 3;
            this.textBox4.TabStop = false;
            this.textBox4.Text = "部品コード";
            // 
            // tb_torihikisaki_cd2
            // 
            this.tb_torihikisaki_cd2.BackColor = System.Drawing.Color.PowderBlue;
            this.tb_torihikisaki_cd2.Location = new System.Drawing.Point(153, 43);
            this.tb_torihikisaki_cd2.MaxLength = 6;
            this.tb_torihikisaki_cd2.Name = "tb_torihikisaki_cd2";
            this.tb_torihikisaki_cd2.Size = new System.Drawing.Size(51, 19);
            this.tb_torihikisaki_cd2.TabIndex = 4;
            this.tb_torihikisaki_cd2.DoubleClick += new System.EventHandler(this.tb_torihikisaki_cd2_DoubleClick);
            this.tb_torihikisaki_cd2.Validating += new System.ComponentModel.CancelEventHandler(this.tb_torihikisaki_cd2_Validating);
            // 
            // tb_torihikisaki_cd1
            // 
            this.tb_torihikisaki_cd1.BackColor = System.Drawing.Color.PowderBlue;
            this.tb_torihikisaki_cd1.Location = new System.Drawing.Point(85, 43);
            this.tb_torihikisaki_cd1.MaxLength = 6;
            this.tb_torihikisaki_cd1.Name = "tb_torihikisaki_cd1";
            this.tb_torihikisaki_cd1.Size = new System.Drawing.Size(51, 19);
            this.tb_torihikisaki_cd1.TabIndex = 3;
            this.tb_torihikisaki_cd1.DoubleClick += new System.EventHandler(this.tb_torihikisaki_cd1_DoubleClick);
            this.tb_torihikisaki_cd1.Validating += new System.ComponentModel.CancelEventHandler(this.tb_torihikisaki_cd1_Validating);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox1.Location = new System.Drawing.Point(10, 43);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(75, 19);
            this.textBox1.TabIndex = 0;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "取引先コード";
            // 
            // frm_buhin_nyusyukko_rireki
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 562);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitContainer1);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "frm_buhin_nyusyukko_rireki";
            this.Text = "部品入出庫履歴照会";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_m)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_csv;
        private System.Windows.Forms.Button btn_insatu;
        private System.Windows.Forms.Button btn_syuryou;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_buhin_syori_date2;
        private System.Windows.Forms.TextBox tb_buhin_syori_date1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button btn_kensaku;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rb_uriage_all;
        private System.Windows.Forms.RadioButton rb_uriage_syori;
        private System.Windows.Forms.RadioButton rb_gamen_syori;
        private System.Windows.Forms.DataGridView dgv_m;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TextBox tb_buhin_cd2;
        private System.Windows.Forms.TextBox tb_buhin_cd1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox tb_torihikisaki_cd2;
        private System.Windows.Forms.TextBox tb_torihikisaki_cd1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rb_idou;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton rb_syukko;
        private System.Windows.Forms.RadioButton rb_nyuko;
    }
}