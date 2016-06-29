namespace TSS_SYSTEM
{
    partial class frm_search_seihin
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
            this.label1 = new System.Windows.Forms.Label();
            this.btn_kensaku = new System.Windows.Forms.Button();
            this.tb_torihikisaki_name = new System.Windows.Forms.TextBox();
            this.tb_torihikisaki_cd = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.tb_seihin_name = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.tb_seihin_cd2 = new System.Windows.Forms.TextBox();
            this.tb_seihin_cd1 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dgv_m = new System.Windows.Forms.DataGridView();
            this.btn_csv = new System.Windows.Forms.Button();
            this.btn_insatu = new System.Windows.Forms.Button();
            this.btn_sentaku = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.tb_nouhin_schedule_kbn_name = new System.Windows.Forms.TextBox();
            this.tb_nouhin_schedule_kbn = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
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
            this.splitContainer1.Size = new System.Drawing.Size(884, 584);
            this.splitContainer1.SplitterDistance = 103;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tb_nouhin_schedule_kbn_name);
            this.groupBox1.Controls.Add(this.tb_nouhin_schedule_kbn);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btn_kensaku);
            this.groupBox1.Controls.Add(this.tb_torihikisaki_name);
            this.groupBox1.Controls.Add(this.tb_torihikisaki_cd);
            this.groupBox1.Controls.Add(this.textBox6);
            this.groupBox1.Controls.Add(this.tb_seihin_name);
            this.groupBox1.Controls.Add(this.textBox4);
            this.groupBox1.Controls.Add(this.tb_seihin_cd2);
            this.groupBox1.Controls.Add(this.tb_seihin_cd1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(880, 99);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "検索条件";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(193, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "～";
            // 
            // btn_kensaku
            // 
            this.btn_kensaku.Location = new System.Drawing.Point(795, 70);
            this.btn_kensaku.Name = "btn_kensaku";
            this.btn_kensaku.Size = new System.Drawing.Size(75, 23);
            this.btn_kensaku.TabIndex = 9;
            this.btn_kensaku.Text = "検索";
            this.btn_kensaku.UseVisualStyleBackColor = true;
            this.btn_kensaku.Click += new System.EventHandler(this.btn_kensaku_Click);
            // 
            // tb_torihikisaki_name
            // 
            this.tb_torihikisaki_name.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_torihikisaki_name.Location = new System.Drawing.Point(131, 68);
            this.tb_torihikisaki_name.Name = "tb_torihikisaki_name";
            this.tb_torihikisaki_name.ReadOnly = true;
            this.tb_torihikisaki_name.Size = new System.Drawing.Size(222, 19);
            this.tb_torihikisaki_name.TabIndex = 8;
            this.tb_torihikisaki_name.TabStop = false;
            // 
            // tb_torihikisaki_cd
            // 
            this.tb_torihikisaki_cd.BackColor = System.Drawing.Color.PowderBlue;
            this.tb_torihikisaki_cd.Location = new System.Drawing.Point(85, 68);
            this.tb_torihikisaki_cd.MaxLength = 6;
            this.tb_torihikisaki_cd.Name = "tb_torihikisaki_cd";
            this.tb_torihikisaki_cd.Size = new System.Drawing.Size(46, 19);
            this.tb_torihikisaki_cd.TabIndex = 6;
            this.tb_torihikisaki_cd.DoubleClick += new System.EventHandler(this.tb_torihikisaki_cd_DoubleClick);
            this.tb_torihikisaki_cd.Validating += new System.ComponentModel.CancelEventHandler(this.tb_torihikisaki_cd_Validating);
            this.tb_torihikisaki_cd.Validated += new System.EventHandler(this.tb_torihikisaki_cd_Validated);
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox6.Location = new System.Drawing.Point(10, 68);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(75, 19);
            this.textBox6.TabIndex = 5;
            this.textBox6.TabStop = false;
            this.textBox6.Text = "取引先コード";
            // 
            // tb_seihin_name
            // 
            this.tb_seihin_name.Location = new System.Drawing.Point(85, 43);
            this.tb_seihin_name.MaxLength = 40;
            this.tb_seihin_name.Name = "tb_seihin_name";
            this.tb_seihin_name.Size = new System.Drawing.Size(232, 19);
            this.tb_seihin_name.TabIndex = 4;
            this.tb_seihin_name.Validating += new System.ComponentModel.CancelEventHandler(this.tb_seihin_name_Validating);
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
            this.textBox4.Text = "製品名";
            // 
            // tb_seihin_cd2
            // 
            this.tb_seihin_cd2.Location = new System.Drawing.Point(210, 18);
            this.tb_seihin_cd2.MaxLength = 16;
            this.tb_seihin_cd2.Name = "tb_seihin_cd2";
            this.tb_seihin_cd2.Size = new System.Drawing.Size(108, 19);
            this.tb_seihin_cd2.TabIndex = 2;
            this.tb_seihin_cd2.Validating += new System.ComponentModel.CancelEventHandler(this.tb_seihin_cd2_Validating);
            // 
            // tb_seihin_cd1
            // 
            this.tb_seihin_cd1.Location = new System.Drawing.Point(85, 18);
            this.tb_seihin_cd1.MaxLength = 16;
            this.tb_seihin_cd1.Name = "tb_seihin_cd1";
            this.tb_seihin_cd1.Size = new System.Drawing.Size(108, 19);
            this.tb_seihin_cd1.TabIndex = 1;
            this.tb_seihin_cd1.Validating += new System.ComponentModel.CancelEventHandler(this.tb_seihin_cd1_Validating);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox1.Location = new System.Drawing.Point(10, 18);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(75, 19);
            this.textBox1.TabIndex = 0;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "製品コード";
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
            this.splitContainer2.Size = new System.Drawing.Size(884, 477);
            this.splitContainer2.SplitterDistance = 439;
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
            this.dgv_m.Size = new System.Drawing.Size(880, 435);
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
            // tb_nouhin_schedule_kbn_name
            // 
            this.tb_nouhin_schedule_kbn_name.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_nouhin_schedule_kbn_name.Location = new System.Drawing.Point(506, 18);
            this.tb_nouhin_schedule_kbn_name.Name = "tb_nouhin_schedule_kbn_name";
            this.tb_nouhin_schedule_kbn_name.ReadOnly = true;
            this.tb_nouhin_schedule_kbn_name.Size = new System.Drawing.Size(156, 19);
            this.tb_nouhin_schedule_kbn_name.TabIndex = 16;
            this.tb_nouhin_schedule_kbn_name.TabStop = false;
            // 
            // tb_nouhin_schedule_kbn
            // 
            this.tb_nouhin_schedule_kbn.BackColor = System.Drawing.Color.PowderBlue;
            this.tb_nouhin_schedule_kbn.Location = new System.Drawing.Point(485, 18);
            this.tb_nouhin_schedule_kbn.Name = "tb_nouhin_schedule_kbn";
            this.tb_nouhin_schedule_kbn.Size = new System.Drawing.Size(21, 19);
            this.tb_nouhin_schedule_kbn.TabIndex = 15;
            this.tb_nouhin_schedule_kbn.DoubleClick += new System.EventHandler(this.tb_nouhin_schedule_kbn_DoubleClick);
            this.tb_nouhin_schedule_kbn.Validating += new System.ComponentModel.CancelEventHandler(this.tb_nouhin_schedule_kbn_Validating);
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox2.Location = new System.Drawing.Point(369, 18);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(116, 19);
            this.textBox2.TabIndex = 14;
            this.textBox2.TabStop = false;
            this.textBox2.Text = "納品スケジュール区分";
            // 
            // frm_search_seihin
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
            this.Name = "frm_search_seihin";
            this.Text = "製品マスタ検索";
            this.Load += new System.EventHandler(this.frm_search_seihin_Load);
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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_kensaku;
        private System.Windows.Forms.TextBox tb_torihikisaki_name;
        private System.Windows.Forms.TextBox tb_torihikisaki_cd;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox tb_seihin_name;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox tb_seihin_cd2;
        private System.Windows.Forms.TextBox tb_seihin_cd1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dgv_m;
        private System.Windows.Forms.Button btn_csv;
        private System.Windows.Forms.Button btn_insatu;
        private System.Windows.Forms.Button btn_sentaku;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_nouhin_schedule_kbn_name;
        private System.Windows.Forms.TextBox tb_nouhin_schedule_kbn;
        private System.Windows.Forms.TextBox textBox2;
    }
}