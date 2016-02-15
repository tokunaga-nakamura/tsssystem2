namespace TSS_SYSTEM
{
    partial class frm_tankabetu_uriage
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
            this.btn_kensaku = new System.Windows.Forms.Button();
            this.dgv_m = new System.Windows.Forms.DataGridView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.tb_kouchin_hukusizai = new System.Windows.Forms.TextBox();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.tb_buhin = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.tb_hukusizai = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.tb_kouchin = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.tb_uriage = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.tb_torihikisaki_name = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.tb_torihikisaki_cd = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_uriage_date = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_m)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_csv
            // 
            this.btn_csv.Location = new System.Drawing.Point(85, 8);
            this.btn_csv.Name = "btn_csv";
            this.btn_csv.Size = new System.Drawing.Size(75, 23);
            this.btn_csv.TabIndex = 5;
            this.btn_csv.Text = "CSV出力";
            this.btn_csv.UseVisualStyleBackColor = true;
            this.btn_csv.Click += new System.EventHandler(this.btn_csv_Click);
            // 
            // btn_insatu
            // 
            this.btn_insatu.Location = new System.Drawing.Point(4, 8);
            this.btn_insatu.Name = "btn_insatu";
            this.btn_insatu.Size = new System.Drawing.Size(75, 23);
            this.btn_insatu.TabIndex = 4;
            this.btn_insatu.Text = "印刷";
            this.btn_insatu.UseVisualStyleBackColor = true;
            this.btn_insatu.Click += new System.EventHandler(this.btn_insatu_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(795, 8);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 6;
            this.btn_cancel.Text = "終了";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_kensaku
            // 
            this.btn_kensaku.Location = new System.Drawing.Point(795, 41);
            this.btn_kensaku.Name = "btn_kensaku";
            this.btn_kensaku.Size = new System.Drawing.Size(75, 23);
            this.btn_kensaku.TabIndex = 3;
            this.btn_kensaku.Text = "検索";
            this.btn_kensaku.UseVisualStyleBackColor = true;
            this.btn_kensaku.Click += new System.EventHandler(this.btn_kensaku_Click);
            // 
            // dgv_m
            // 
            this.dgv_m.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_m.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_m.Location = new System.Drawing.Point(0, 0);
            this.dgv_m.Name = "dgv_m";
            this.dgv_m.RowTemplate.Height = 21;
            this.dgv_m.Size = new System.Drawing.Size(880, 368);
            this.dgv_m.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
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
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(884, 464);
            this.splitContainer2.SplitterDistance = 372;
            this.splitContainer2.TabIndex = 0;
            this.splitContainer2.TabStop = false;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.tb_kouchin_hukusizai);
            this.splitContainer3.Panel1.Controls.Add(this.textBox11);
            this.splitContainer3.Panel1.Controls.Add(this.tb_buhin);
            this.splitContainer3.Panel1.Controls.Add(this.textBox9);
            this.splitContainer3.Panel1.Controls.Add(this.tb_hukusizai);
            this.splitContainer3.Panel1.Controls.Add(this.textBox5);
            this.splitContainer3.Panel1.Controls.Add(this.tb_kouchin);
            this.splitContainer3.Panel1.Controls.Add(this.textBox2);
            this.splitContainer3.Panel1.Controls.Add(this.tb_uriage);
            this.splitContainer3.Panel1.Controls.Add(this.textBox7);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.btn_cancel);
            this.splitContainer3.Panel2.Controls.Add(this.btn_csv);
            this.splitContainer3.Panel2.Controls.Add(this.btn_insatu);
            this.splitContainer3.Size = new System.Drawing.Size(880, 84);
            this.splitContainer3.SplitterDistance = 46;
            this.splitContainer3.TabIndex = 7;
            // 
            // tb_kouchin_hukusizai
            // 
            this.tb_kouchin_hukusizai.Location = new System.Drawing.Point(777, 12);
            this.tb_kouchin_hukusizai.MaxLength = 16;
            this.tb_kouchin_hukusizai.Name = "tb_kouchin_hukusizai";
            this.tb_kouchin_hukusizai.ReadOnly = true;
            this.tb_kouchin_hukusizai.Size = new System.Drawing.Size(98, 19);
            this.tb_kouchin_hukusizai.TabIndex = 17;
            this.tb_kouchin_hukusizai.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox11
            // 
            this.textBox11.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox11.Location = new System.Drawing.Point(686, 12);
            this.textBox11.Name = "textBox11";
            this.textBox11.ReadOnly = true;
            this.textBox11.Size = new System.Drawing.Size(91, 19);
            this.textBox11.TabIndex = 16;
            this.textBox11.TabStop = false;
            this.textBox11.Text = "工賃＋副資材";
            // 
            // tb_buhin
            // 
            this.tb_buhin.Location = new System.Drawing.Point(582, 12);
            this.tb_buhin.MaxLength = 16;
            this.tb_buhin.Name = "tb_buhin";
            this.tb_buhin.ReadOnly = true;
            this.tb_buhin.Size = new System.Drawing.Size(98, 19);
            this.tb_buhin.TabIndex = 15;
            this.tb_buhin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox9
            // 
            this.textBox9.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox9.Location = new System.Drawing.Point(513, 12);
            this.textBox9.Name = "textBox9";
            this.textBox9.ReadOnly = true;
            this.textBox9.Size = new System.Drawing.Size(69, 19);
            this.textBox9.TabIndex = 14;
            this.textBox9.TabStop = false;
            this.textBox9.Text = "部品費合計";
            // 
            // tb_hukusizai
            // 
            this.tb_hukusizai.Location = new System.Drawing.Point(413, 12);
            this.tb_hukusizai.MaxLength = 16;
            this.tb_hukusizai.Name = "tb_hukusizai";
            this.tb_hukusizai.ReadOnly = true;
            this.tb_hukusizai.Size = new System.Drawing.Size(98, 19);
            this.tb_hukusizai.TabIndex = 13;
            this.tb_hukusizai.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox5.Location = new System.Drawing.Point(344, 12);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(69, 19);
            this.textBox5.TabIndex = 12;
            this.textBox5.TabStop = false;
            this.textBox5.Text = "副資材合計";
            // 
            // tb_kouchin
            // 
            this.tb_kouchin.Location = new System.Drawing.Point(245, 12);
            this.tb_kouchin.MaxLength = 16;
            this.tb_kouchin.Name = "tb_kouchin";
            this.tb_kouchin.ReadOnly = true;
            this.tb_kouchin.Size = new System.Drawing.Size(94, 19);
            this.tb_kouchin.TabIndex = 11;
            this.tb_kouchin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox2.Location = new System.Drawing.Point(187, 12);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(58, 19);
            this.textBox2.TabIndex = 10;
            this.textBox2.TabStop = false;
            this.textBox2.Text = "工賃合計";
            // 
            // tb_uriage
            // 
            this.tb_uriage.Location = new System.Drawing.Point(85, 12);
            this.tb_uriage.MaxLength = 16;
            this.tb_uriage.Name = "tb_uriage";
            this.tb_uriage.ReadOnly = true;
            this.tb_uriage.Size = new System.Drawing.Size(97, 19);
            this.tb_uriage.TabIndex = 9;
            this.tb_uriage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox7.Location = new System.Drawing.Point(10, 12);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(75, 19);
            this.textBox7.TabIndex = 8;
            this.textBox7.TabStop = false;
            this.textBox7.Text = "売上合計";
            // 
            // tb_torihikisaki_name
            // 
            this.tb_torihikisaki_name.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_torihikisaki_name.Location = new System.Drawing.Point(135, 43);
            this.tb_torihikisaki_name.Name = "tb_torihikisaki_name";
            this.tb_torihikisaki_name.ReadOnly = true;
            this.tb_torihikisaki_name.Size = new System.Drawing.Size(240, 19);
            this.tb_torihikisaki_name.TabIndex = 8;
            this.tb_torihikisaki_name.TabStop = false;
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox6.Location = new System.Drawing.Point(10, 18);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(75, 19);
            this.textBox6.TabIndex = 5;
            this.textBox6.TabStop = false;
            this.textBox6.Text = "売上計上日";
            // 
            // tb_torihikisaki_cd
            // 
            this.tb_torihikisaki_cd.BackColor = System.Drawing.Color.PowderBlue;
            this.tb_torihikisaki_cd.Location = new System.Drawing.Point(85, 43);
            this.tb_torihikisaki_cd.MaxLength = 6;
            this.tb_torihikisaki_cd.Name = "tb_torihikisaki_cd";
            this.tb_torihikisaki_cd.Size = new System.Drawing.Size(50, 19);
            this.tb_torihikisaki_cd.TabIndex = 2;
            this.tb_torihikisaki_cd.Validating += new System.ComponentModel.CancelEventHandler(this.tb_torihikisaki_cd_Validating);
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox4.Location = new System.Drawing.Point(10, 43);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(75, 19);
            this.textBox4.TabIndex = 3;
            this.textBox4.TabStop = false;
            this.textBox4.Text = "取引先コード";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btn_kensaku);
            this.groupBox1.Controls.Add(this.tb_torihikisaki_name);
            this.groupBox1.Controls.Add(this.tb_uriage_date);
            this.groupBox1.Controls.Add(this.textBox6);
            this.groupBox1.Controls.Add(this.tb_torihikisaki_cd);
            this.groupBox1.Controls.Add(this.textBox4);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(880, 68);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "検索条件";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(398, 43);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 16);
            this.checkBox1.TabIndex = 10;
            this.checkBox1.Text = "明細不要";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(396, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 12);
            this.label1.TabIndex = 9;
            // 
            // tb_uriage_date
            // 
            this.tb_uriage_date.Location = new System.Drawing.Point(85, 18);
            this.tb_uriage_date.MaxLength = 10;
            this.tb_uriage_date.Name = "tb_uriage_date";
            this.tb_uriage_date.Size = new System.Drawing.Size(74, 19);
            this.tb_uriage_date.TabIndex = 1;
            this.tb_uriage_date.Validating += new System.ComponentModel.CancelEventHandler(this.tb_uriage_date_Validating);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
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
            this.splitContainer1.SplitterDistance = 72;
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
            // frm_tankabetu_uriage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 562);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "frm_tankabetu_uriage";
            this.Text = "単価区分別売上明細";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_m)).EndInit();
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
        private System.Windows.Forms.Button btn_kensaku;
        private System.Windows.Forms.DataGridView dgv_m;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TextBox tb_torihikisaki_name;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox tb_torihikisaki_cd;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tb_uriage_date;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.TextBox tb_kouchin_hukusizai;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.TextBox tb_buhin;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox tb_hukusizai;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox tb_kouchin;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox tb_uriage;
        private System.Windows.Forms.TextBox textBox7;

    }
}