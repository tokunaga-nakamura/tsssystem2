namespace TSS_SYSTEM
{
    partial class frm_search_syain
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
            this.tb_busyo_name = new System.Windows.Forms.TextBox();
            this.tb_busyo_cd = new System.Windows.Forms.TextBox();
            this.tb_syain_kbn_name = new System.Windows.Forms.TextBox();
            this.tb_syain_kbn = new System.Windows.Forms.TextBox();
            this.tb_syain_name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_syain_cd2 = new System.Windows.Forms.TextBox();
            this.tb_syain_cd1 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dgv_m = new System.Windows.Forms.DataGridView();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_csv = new System.Windows.Forms.Button();
            this.btn_insatu = new System.Windows.Forms.Button();
            this.btn_sentaku = new System.Windows.Forms.Button();
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
            this.splitContainer1.Size = new System.Drawing.Size(884, 561);
            this.splitContainer1.SplitterDistance = 123;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_kensaku);
            this.groupBox1.Controls.Add(this.tb_busyo_name);
            this.groupBox1.Controls.Add(this.tb_busyo_cd);
            this.groupBox1.Controls.Add(this.tb_syain_kbn_name);
            this.groupBox1.Controls.Add(this.tb_syain_kbn);
            this.groupBox1.Controls.Add(this.tb_syain_name);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tb_syain_cd2);
            this.groupBox1.Controls.Add(this.tb_syain_cd1);
            this.groupBox1.Controls.Add(this.textBox4);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(880, 119);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "検索条件";
            // 
            // btn_kensaku
            // 
            this.btn_kensaku.Location = new System.Drawing.Point(795, 89);
            this.btn_kensaku.Name = "btn_kensaku";
            this.btn_kensaku.Size = new System.Drawing.Size(75, 23);
            this.btn_kensaku.TabIndex = 12;
            this.btn_kensaku.Text = "検索";
            this.btn_kensaku.UseVisualStyleBackColor = true;
            this.btn_kensaku.Click += new System.EventHandler(this.btn_kensaku_Click);
            // 
            // tb_busyo_name
            // 
            this.tb_busyo_name.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_busyo_name.Location = new System.Drawing.Point(104, 93);
            this.tb_busyo_name.Name = "tb_busyo_name";
            this.tb_busyo_name.ReadOnly = true;
            this.tb_busyo_name.Size = new System.Drawing.Size(100, 19);
            this.tb_busyo_name.TabIndex = 11;
            this.tb_busyo_name.TabStop = false;
            // 
            // tb_busyo_cd
            // 
            this.tb_busyo_cd.Location = new System.Drawing.Point(72, 93);
            this.tb_busyo_cd.MaxLength = 4;
            this.tb_busyo_cd.Name = "tb_busyo_cd";
            this.tb_busyo_cd.Size = new System.Drawing.Size(32, 19);
            this.tb_busyo_cd.TabIndex = 10;
            this.tb_busyo_cd.Validating += new System.ComponentModel.CancelEventHandler(this.tb_busyo_cd_Validating);
            this.tb_busyo_cd.Validated += new System.EventHandler(this.tb_busyo_cd_Validated);
            // 
            // tb_syain_kbn_name
            // 
            this.tb_syain_kbn_name.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_syain_kbn_name.Location = new System.Drawing.Point(90, 68);
            this.tb_syain_kbn_name.Name = "tb_syain_kbn_name";
            this.tb_syain_kbn_name.ReadOnly = true;
            this.tb_syain_kbn_name.Size = new System.Drawing.Size(100, 19);
            this.tb_syain_kbn_name.TabIndex = 9;
            this.tb_syain_kbn_name.TabStop = false;
            // 
            // tb_syain_kbn
            // 
            this.tb_syain_kbn.Location = new System.Drawing.Point(72, 68);
            this.tb_syain_kbn.MaxLength = 1;
            this.tb_syain_kbn.Name = "tb_syain_kbn";
            this.tb_syain_kbn.Size = new System.Drawing.Size(18, 19);
            this.tb_syain_kbn.TabIndex = 8;
            this.tb_syain_kbn.Validating += new System.ComponentModel.CancelEventHandler(this.tb_syain_kbn_Validating);
            this.tb_syain_kbn.Validated += new System.EventHandler(this.tb_syain_kbn_Validated);
            // 
            // tb_syain_name
            // 
            this.tb_syain_name.Location = new System.Drawing.Point(72, 43);
            this.tb_syain_name.MaxLength = 40;
            this.tb_syain_name.Name = "tb_syain_name";
            this.tb_syain_name.Size = new System.Drawing.Size(204, 19);
            this.tb_syain_name.TabIndex = 7;
            this.tb_syain_name.Validating += new System.ComponentModel.CancelEventHandler(this.tb_syain_name_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(127, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "～";
            // 
            // tb_syain_cd2
            // 
            this.tb_syain_cd2.Location = new System.Drawing.Point(150, 18);
            this.tb_syain_cd2.MaxLength = 6;
            this.tb_syain_cd2.Name = "tb_syain_cd2";
            this.tb_syain_cd2.Size = new System.Drawing.Size(49, 19);
            this.tb_syain_cd2.TabIndex = 5;
            this.tb_syain_cd2.Validating += new System.ComponentModel.CancelEventHandler(this.tb_syain_cd2_Validating);
            // 
            // tb_syain_cd1
            // 
            this.tb_syain_cd1.Location = new System.Drawing.Point(72, 18);
            this.tb_syain_cd1.MaxLength = 6;
            this.tb_syain_cd1.Name = "tb_syain_cd1";
            this.tb_syain_cd1.Size = new System.Drawing.Size(49, 19);
            this.tb_syain_cd1.TabIndex = 4;
            this.tb_syain_cd1.Validating += new System.ComponentModel.CancelEventHandler(this.tb_syain_cd1_Validating);
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox4.Location = new System.Drawing.Point(10, 93);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(62, 19);
            this.textBox4.TabIndex = 3;
            this.textBox4.TabStop = false;
            this.textBox4.Text = "部署コード";
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox3.Location = new System.Drawing.Point(10, 68);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(62, 19);
            this.textBox3.TabIndex = 2;
            this.textBox3.TabStop = false;
            this.textBox3.Text = "社員区分";
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox2.Location = new System.Drawing.Point(10, 43);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(62, 19);
            this.textBox2.TabIndex = 1;
            this.textBox2.TabStop = false;
            this.textBox2.Text = "社員名";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox1.Location = new System.Drawing.Point(10, 18);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(62, 19);
            this.textBox1.TabIndex = 0;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "社員コード";
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
            this.splitContainer2.Panel2.Controls.Add(this.btn_cancel);
            this.splitContainer2.Panel2.Controls.Add(this.btn_csv);
            this.splitContainer2.Panel2.Controls.Add(this.btn_insatu);
            this.splitContainer2.Panel2.Controls.Add(this.btn_sentaku);
            this.splitContainer2.Size = new System.Drawing.Size(884, 434);
            this.splitContainer2.SplitterDistance = 396;
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
            this.dgv_m.Size = new System.Drawing.Size(880, 392);
            this.dgv_m.TabIndex = 0;
            this.dgv_m.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_m_CellMouseDoubleClick);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(795, 3);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 3;
            this.btn_cancel.Text = "キャンセル";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
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
            // btn_sentaku
            // 
            this.btn_sentaku.Location = new System.Drawing.Point(10, 3);
            this.btn_sentaku.Name = "btn_sentaku";
            this.btn_sentaku.Size = new System.Drawing.Size(75, 23);
            this.btn_sentaku.TabIndex = 0;
            this.btn_sentaku.Text = "選択";
            this.btn_sentaku.UseVisualStyleBackColor = true;
            this.btn_sentaku.Click += new System.EventHandler(this.btn_sentaku_Click);
            // 
            // frm_search_syain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGreen;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.splitContainer1);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "frm_search_syain";
            this.Text = "社員検索";
            this.Load += new System.EventHandler(this.frm_search_syain_Load);
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
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_csv;
        private System.Windows.Forms.Button btn_insatu;
        private System.Windows.Forms.Button btn_sentaku;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tb_busyo_name;
        private System.Windows.Forms.TextBox tb_busyo_cd;
        private System.Windows.Forms.TextBox tb_syain_kbn_name;
        private System.Windows.Forms.TextBox tb_syain_kbn;
        private System.Windows.Forms.TextBox tb_syain_name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_syain_cd2;
        private System.Windows.Forms.TextBox tb_syain_cd1;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView dgv_m;
        private System.Windows.Forms.Button btn_kensaku;
    }
}