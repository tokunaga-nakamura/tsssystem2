namespace TSS_SYSTEM
{
    partial class frm_bank_m
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_bank_m));
            this.bt_close = new System.Windows.Forms.Button();
            this.btn_sakujyo = new System.Windows.Forms.Button();
            this.btn_touroku = new System.Windows.Forms.Button();
            this.tb_kouza_meigi = new System.Windows.Forms.TextBox();
            this.textBox17 = new System.Windows.Forms.TextBox();
            this.tb_kouza_no = new System.Windows.Forms.TextBox();
            this.textBox15 = new System.Windows.Forms.TextBox();
            this.tb_kouza_syubetu = new System.Windows.Forms.TextBox();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.tb_siten_name = new System.Windows.Forms.TextBox();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.tb_siten_cd = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.tb_bank_name = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.tb_torihikisaki_name = new System.Windows.Forms.TextBox();
            this.tb_torihikisaki_cd = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.tb_bank_cd = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgv_bank_m = new System.Windows.Forms.DataGridView();
            this.bt_hensyu = new System.Windows.Forms.Button();
            this.btn_hardcopy = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_bank_m)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bt_close
            // 
            this.bt_close.Location = new System.Drawing.Point(795, 3);
            this.bt_close.Name = "bt_close";
            this.bt_close.Size = new System.Drawing.Size(75, 23);
            this.bt_close.TabIndex = 3;
            this.bt_close.Text = "終了";
            this.bt_close.UseVisualStyleBackColor = true;
            this.bt_close.Click += new System.EventHandler(this.bt_close_Click);
            // 
            // btn_sakujyo
            // 
            this.btn_sakujyo.Location = new System.Drawing.Point(714, 3);
            this.btn_sakujyo.Name = "btn_sakujyo";
            this.btn_sakujyo.Size = new System.Drawing.Size(75, 23);
            this.btn_sakujyo.TabIndex = 2;
            this.btn_sakujyo.Text = "削除";
            this.btn_sakujyo.UseVisualStyleBackColor = true;
            this.btn_sakujyo.Click += new System.EventHandler(this.btn_sakujyo_Click);
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
            // tb_kouza_meigi
            // 
            this.tb_kouza_meigi.Location = new System.Drawing.Point(329, 78);
            this.tb_kouza_meigi.Name = "tb_kouza_meigi";
            this.tb_kouza_meigi.Size = new System.Drawing.Size(313, 19);
            this.tb_kouza_meigi.TabIndex = 6;
            // 
            // textBox17
            // 
            this.textBox17.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox17.Location = new System.Drawing.Point(244, 78);
            this.textBox17.Name = "textBox17";
            this.textBox17.ReadOnly = true;
            this.textBox17.Size = new System.Drawing.Size(85, 19);
            this.textBox17.TabIndex = 37;
            this.textBox17.TabStop = false;
            this.textBox17.Text = "口座名義";
            // 
            // tb_kouza_no
            // 
            this.tb_kouza_no.Location = new System.Drawing.Point(93, 78);
            this.tb_kouza_no.Name = "tb_kouza_no";
            this.tb_kouza_no.Size = new System.Drawing.Size(145, 19);
            this.tb_kouza_no.TabIndex = 5;
            // 
            // textBox15
            // 
            this.textBox15.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox15.Location = new System.Drawing.Point(10, 78);
            this.textBox15.Name = "textBox15";
            this.textBox15.ReadOnly = true;
            this.textBox15.Size = new System.Drawing.Size(83, 19);
            this.textBox15.TabIndex = 35;
            this.textBox15.TabStop = false;
            this.textBox15.Text = "口座番号";
            // 
            // tb_kouza_syubetu
            // 
            this.tb_kouza_syubetu.BackColor = System.Drawing.Color.PowderBlue;
            this.tb_kouza_syubetu.Location = new System.Drawing.Point(93, 103);
            this.tb_kouza_syubetu.Name = "tb_kouza_syubetu";
            this.tb_kouza_syubetu.Size = new System.Drawing.Size(21, 19);
            this.tb_kouza_syubetu.TabIndex = 7;
            this.tb_kouza_syubetu.TextChanged += new System.EventHandler(this.tb_kouza_syubetu_TextChanged);
            this.tb_kouza_syubetu.DoubleClick += new System.EventHandler(this.tb_kouza_syubetu_DoubleClick);
            // 
            // textBox13
            // 
            this.textBox13.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox13.Location = new System.Drawing.Point(10, 103);
            this.textBox13.Name = "textBox13";
            this.textBox13.ReadOnly = true;
            this.textBox13.Size = new System.Drawing.Size(83, 19);
            this.textBox13.TabIndex = 34;
            this.textBox13.TabStop = false;
            this.textBox13.Text = "口座種別";
            // 
            // tb_siten_name
            // 
            this.tb_siten_name.Location = new System.Drawing.Point(235, 53);
            this.tb_siten_name.Name = "tb_siten_name";
            this.tb_siten_name.Size = new System.Drawing.Size(292, 19);
            this.tb_siten_name.TabIndex = 4;
            // 
            // textBox11
            // 
            this.textBox11.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox11.Location = new System.Drawing.Point(152, 53);
            this.textBox11.Name = "textBox11";
            this.textBox11.ReadOnly = true;
            this.textBox11.Size = new System.Drawing.Size(83, 19);
            this.textBox11.TabIndex = 32;
            this.textBox11.TabStop = false;
            this.textBox11.Text = "支店名";
            // 
            // tb_siten_cd
            // 
            this.tb_siten_cd.Location = new System.Drawing.Point(93, 53);
            this.tb_siten_cd.Name = "tb_siten_cd";
            this.tb_siten_cd.Size = new System.Drawing.Size(53, 19);
            this.tb_siten_cd.TabIndex = 3;
            // 
            // textBox9
            // 
            this.textBox9.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox9.Location = new System.Drawing.Point(10, 53);
            this.textBox9.Name = "textBox9";
            this.textBox9.ReadOnly = true;
            this.textBox9.Size = new System.Drawing.Size(83, 19);
            this.textBox9.TabIndex = 30;
            this.textBox9.TabStop = false;
            this.textBox9.Text = "支店コード";
            // 
            // tb_bank_name
            // 
            this.tb_bank_name.Location = new System.Drawing.Point(235, 28);
            this.tb_bank_name.Name = "tb_bank_name";
            this.tb_bank_name.Size = new System.Drawing.Size(292, 19);
            this.tb_bank_name.TabIndex = 2;
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox7.Location = new System.Drawing.Point(152, 28);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(83, 19);
            this.textBox7.TabIndex = 28;
            this.textBox7.TabStop = false;
            this.textBox7.Text = "金融機関名";
            // 
            // tb_torihikisaki_name
            // 
            this.tb_torihikisaki_name.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_torihikisaki_name.Location = new System.Drawing.Point(146, 3);
            this.tb_torihikisaki_name.Name = "tb_torihikisaki_name";
            this.tb_torihikisaki_name.Size = new System.Drawing.Size(180, 19);
            this.tb_torihikisaki_name.TabIndex = 24;
            this.tb_torihikisaki_name.TabStop = false;
            // 
            // tb_torihikisaki_cd
            // 
            this.tb_torihikisaki_cd.Location = new System.Drawing.Point(93, 3);
            this.tb_torihikisaki_cd.MaxLength = 6;
            this.tb_torihikisaki_cd.Name = "tb_torihikisaki_cd";
            this.tb_torihikisaki_cd.Size = new System.Drawing.Size(53, 19);
            this.tb_torihikisaki_cd.TabIndex = 0;
            this.tb_torihikisaki_cd.Leave += new System.EventHandler(this.tb_torihikisaki_cd_Leave);
            this.tb_torihikisaki_cd.Validating += new System.ComponentModel.CancelEventHandler(this.tb_torihikisaki_cd_Validating);
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox5.Location = new System.Drawing.Point(10, 3);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(83, 19);
            this.textBox5.TabIndex = 23;
            this.textBox5.TabStop = false;
            this.textBox5.Text = "取引先コード";
            // 
            // tb_bank_cd
            // 
            this.tb_bank_cd.Location = new System.Drawing.Point(93, 28);
            this.tb_bank_cd.MaxLength = 6;
            this.tb_bank_cd.Name = "tb_bank_cd";
            this.tb_bank_cd.Size = new System.Drawing.Size(53, 19);
            this.tb_bank_cd.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox1.Location = new System.Drawing.Point(10, 28);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(83, 19);
            this.textBox1.TabIndex = 19;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "金融機関コード";
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
            this.splitContainer2.Panel2.Controls.Add(this.bt_hensyu);
            this.splitContainer2.Panel2.Controls.Add(this.bt_close);
            this.splitContainer2.Panel2.Controls.Add(this.btn_sakujyo);
            this.splitContainer2.Panel2.Controls.Add(this.btn_touroku);
            this.splitContainer2.Size = new System.Drawing.Size(884, 474);
            this.splitContainer2.SplitterDistance = 438;
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
            this.splitContainer3.Panel1.Controls.Add(this.label2);
            this.splitContainer3.Panel1.Controls.Add(this.textBox13);
            this.splitContainer3.Panel1.Controls.Add(this.tb_bank_cd);
            this.splitContainer3.Panel1.Controls.Add(this.tb_siten_name);
            this.splitContainer3.Panel1.Controls.Add(this.tb_kouza_no);
            this.splitContainer3.Panel1.Controls.Add(this.textBox11);
            this.splitContainer3.Panel1.Controls.Add(this.label1);
            this.splitContainer3.Panel1.Controls.Add(this.tb_siten_cd);
            this.splitContainer3.Panel1.Controls.Add(this.textBox15);
            this.splitContainer3.Panel1.Controls.Add(this.textBox9);
            this.splitContainer3.Panel1.Controls.Add(this.textBox17);
            this.splitContainer3.Panel1.Controls.Add(this.tb_bank_name);
            this.splitContainer3.Panel1.Controls.Add(this.tb_kouza_syubetu);
            this.splitContainer3.Panel1.Controls.Add(this.textBox7);
            this.splitContainer3.Panel1.Controls.Add(this.tb_kouza_meigi);
            this.splitContainer3.Panel1.Controls.Add(this.tb_torihikisaki_name);
            this.splitContainer3.Panel1.Controls.Add(this.tb_torihikisaki_cd);
            this.splitContainer3.Panel1.Controls.Add(this.textBox1);
            this.splitContainer3.Panel1.Controls.Add(this.textBox5);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.dgv_bank_m);
            this.splitContainer3.Size = new System.Drawing.Size(884, 438);
            this.splitContainer3.SplitterDistance = 158;
            this.splitContainer3.TabIndex = 40;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 40;
            this.label2.Text = "登録一覧";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(120, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 39;
            this.label1.Text = "1:普通　2:当座";
            // 
            // dgv_bank_m
            // 
            this.dgv_bank_m.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_bank_m.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_bank_m.Location = new System.Drawing.Point(0, 0);
            this.dgv_bank_m.Name = "dgv_bank_m";
            this.dgv_bank_m.RowTemplate.Height = 21;
            this.dgv_bank_m.Size = new System.Drawing.Size(880, 272);
            this.dgv_bank_m.TabIndex = 0;
            // 
            // bt_hensyu
            // 
            this.bt_hensyu.Location = new System.Drawing.Point(631, 3);
            this.bt_hensyu.Name = "bt_hensyu";
            this.bt_hensyu.Size = new System.Drawing.Size(77, 23);
            this.bt_hensyu.TabIndex = 1;
            this.bt_hensyu.Text = "編集";
            this.bt_hensyu.UseVisualStyleBackColor = true;
            this.bt_hensyu.Click += new System.EventHandler(this.bt_hensyu_Click);
            // 
            // btn_hardcopy
            // 
            this.btn_hardcopy.Image = ((System.Drawing.Image)(resources.GetObject("btn_hardcopy.Image")));
            this.btn_hardcopy.Location = new System.Drawing.Point(12, 12);
            this.btn_hardcopy.Name = "btn_hardcopy";
            this.btn_hardcopy.Size = new System.Drawing.Size(36, 36);
            this.btn_hardcopy.TabIndex = 0;
            this.btn_hardcopy.TabStop = false;
            this.btn_hardcopy.UseVisualStyleBackColor = true;
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
            this.splitContainer1.SplitterDistance = 62;
            this.splitContainer1.TabIndex = 5;
            this.splitContainer1.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 540);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(884, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // frm_bank_m
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 562);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "frm_bank_m";
            this.Text = "銀行マスタ";
            this.Load += new System.EventHandler(this.frm_bank_m_Load);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_bank_m)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_close;
        private System.Windows.Forms.Button btn_sakujyo;
        private System.Windows.Forms.Button btn_touroku;
        private System.Windows.Forms.TextBox tb_kouza_meigi;
        private System.Windows.Forms.TextBox textBox17;
        private System.Windows.Forms.TextBox tb_kouza_no;
        private System.Windows.Forms.TextBox textBox15;
        private System.Windows.Forms.TextBox tb_kouza_syubetu;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.TextBox tb_siten_name;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.TextBox tb_siten_cd;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox tb_bank_name;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox tb_torihikisaki_name;
        public System.Windows.Forms.TextBox tb_torihikisaki_cd;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox tb_bank_cd;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button btn_hardcopy;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Button bt_hensyu;
        private System.Windows.Forms.DataGridView dgv_bank_m;
        private System.Windows.Forms.Label label2;
    }
}