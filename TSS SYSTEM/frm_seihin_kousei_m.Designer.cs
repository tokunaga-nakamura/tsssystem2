namespace TSS_SYSTEM
{
    partial class frm_seihin_kousei_m
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_seihin_kousei_m));
            this.label3 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_tuika = new System.Windows.Forms.Button();
            this.btn_touroku = new System.Windows.Forms.Button();
            this.ss_status = new System.Windows.Forms.StatusStrip();
            this.dgv_seihin_kousei = new System.Windows.Forms.DataGridView();
            this.btn_syuuryou = new System.Windows.Forms.Button();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_seihin_kousei_no = new System.Windows.Forms.TextBox();
            this.tb_seihin_kousei_name = new System.Windows.Forms.TextBox();
            this.tb_update_datetime = new System.Windows.Forms.TextBox();
            this.tb_seihin_cd = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.tb_update_user_cd = new System.Windows.Forms.TextBox();
            this.tb_seihin_name = new System.Windows.Forms.TextBox();
            this.tb_create_user_cd = new System.Windows.Forms.TextBox();
            this.tb_create_datetime = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.dgv_seihin_kousei_name = new System.Windows.Forms.DataGridView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.btn_hardcopy = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_seihin_kousei)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_seihin_kousei_name)).BeginInit();
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(13, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 19);
            this.label3.TabIndex = 32;
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox4.Location = new System.Drawing.Point(10, 10);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(108, 19);
            this.textBox4.TabIndex = 9;
            this.textBox4.TabStop = false;
            this.textBox4.Text = "製品コード";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox1.Location = new System.Drawing.Point(10, 37);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(108, 19);
            this.textBox1.TabIndex = 0;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "製品構成番号";
            // 
            // btn_tuika
            // 
            this.btn_tuika.Location = new System.Drawing.Point(10, 11);
            this.btn_tuika.Name = "btn_tuika";
            this.btn_tuika.Size = new System.Drawing.Size(108, 23);
            this.btn_tuika.TabIndex = 4;
            this.btn_tuika.Text = "1行追加";
            this.btn_tuika.UseVisualStyleBackColor = true;
            this.btn_tuika.Click += new System.EventHandler(this.btn_tsuika_Click);
            // 
            // btn_touroku
            // 
            this.btn_touroku.Location = new System.Drawing.Point(685, 7);
            this.btn_touroku.Name = "btn_touroku";
            this.btn_touroku.Size = new System.Drawing.Size(75, 23);
            this.btn_touroku.TabIndex = 3;
            this.btn_touroku.Text = "登録";
            this.btn_touroku.UseVisualStyleBackColor = true;
            this.btn_touroku.Click += new System.EventHandler(this.btn_touroku_Click);
            // 
            // ss_status
            // 
            this.ss_status.Location = new System.Drawing.Point(0, 540);
            this.ss_status.Name = "ss_status";
            this.ss_status.Size = new System.Drawing.Size(884, 22);
            this.ss_status.TabIndex = 4;
            this.ss_status.Text = "statusStrip1";
            // 
            // dgv_seihin_kousei
            // 
            this.dgv_seihin_kousei.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_seihin_kousei.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_seihin_kousei.Location = new System.Drawing.Point(0, 0);
            this.dgv_seihin_kousei.Name = "dgv_seihin_kousei";
            this.dgv_seihin_kousei.RowTemplate.Height = 21;
            this.dgv_seihin_kousei.Size = new System.Drawing.Size(880, 275);
            this.dgv_seihin_kousei.TabIndex = 0;
            this.dgv_seihin_kousei.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_seihin_kousei_CellEndEdit);
            this.dgv_seihin_kousei.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgv_seihin_kousei_DataError);
            // 
            // btn_syuuryou
            // 
            this.btn_syuuryou.Location = new System.Drawing.Point(790, 7);
            this.btn_syuuryou.Name = "btn_syuuryou";
            this.btn_syuuryou.Size = new System.Drawing.Size(75, 23);
            this.btn_syuuryou.TabIndex = 0;
            this.btn_syuuryou.Text = "終了";
            this.btn_syuuryou.UseVisualStyleBackColor = true;
            this.btn_syuuryou.Click += new System.EventHandler(this.btn_syuuryou_Click);
            // 
            // splitContainer3
            // 
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.splitContainer4);
            this.splitContainer3.Panel1.Controls.Add(this.label3);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.dgv_seihin_kousei);
            this.splitContainer3.Size = new System.Drawing.Size(884, 431);
            this.splitContainer3.SplitterDistance = 148;
            this.splitContainer3.TabIndex = 8;
            // 
            // splitContainer4
            // 
            this.splitContainer4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.label1);
            this.splitContainer4.Panel1.Controls.Add(this.tb_seihin_kousei_no);
            this.splitContainer4.Panel1.Controls.Add(this.textBox4);
            this.splitContainer4.Panel1.Controls.Add(this.tb_seihin_kousei_name);
            this.splitContainer4.Panel1.Controls.Add(this.textBox1);
            this.splitContainer4.Panel1.Controls.Add(this.tb_update_datetime);
            this.splitContainer4.Panel1.Controls.Add(this.tb_seihin_cd);
            this.splitContainer4.Panel1.Controls.Add(this.textBox3);
            this.splitContainer4.Panel1.Controls.Add(this.tb_update_user_cd);
            this.splitContainer4.Panel1.Controls.Add(this.tb_seihin_name);
            this.splitContainer4.Panel1.Controls.Add(this.tb_create_user_cd);
            this.splitContainer4.Panel1.Controls.Add(this.tb_create_datetime);
            this.splitContainer4.Panel1.Controls.Add(this.textBox9);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.dgv_seihin_kousei_name);
            this.splitContainer4.Size = new System.Drawing.Size(884, 148);
            this.splitContainer4.SplitterDistance = 484;
            this.splitContainer4.TabIndex = 33;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(310, 12);
            this.label1.TabIndex = 43;
            this.label1.Text = "※製品構成番号を変えると製品構成マスタの表示も変わります。";
            // 
            // tb_seihin_kousei_no
            // 
            this.tb_seihin_kousei_no.BackColor = System.Drawing.Color.PowderBlue;
            this.tb_seihin_kousei_no.Location = new System.Drawing.Point(118, 37);
            this.tb_seihin_kousei_no.MaxLength = 2;
            this.tb_seihin_kousei_no.Name = "tb_seihin_kousei_no";
            this.tb_seihin_kousei_no.Size = new System.Drawing.Size(83, 19);
            this.tb_seihin_kousei_no.TabIndex = 42;
            this.tb_seihin_kousei_no.TextChanged += new System.EventHandler(this.tb_seihin_kousei_no_TextChanged);
            this.tb_seihin_kousei_no.DoubleClick += new System.EventHandler(this.tb_seihin_kousei_no_DoubleClick);
            // 
            // tb_seihin_kousei_name
            // 
            this.tb_seihin_kousei_name.BackColor = System.Drawing.SystemColors.Window;
            this.tb_seihin_kousei_name.Location = new System.Drawing.Point(203, 37);
            this.tb_seihin_kousei_name.Name = "tb_seihin_kousei_name";
            this.tb_seihin_kousei_name.Size = new System.Drawing.Size(222, 19);
            this.tb_seihin_kousei_name.TabIndex = 35;
            this.tb_seihin_kousei_name.TabStop = false;
            // 
            // tb_update_datetime
            // 
            this.tb_update_datetime.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_update_datetime.Location = new System.Drawing.Point(93, 107);
            this.tb_update_datetime.Name = "tb_update_datetime";
            this.tb_update_datetime.ReadOnly = true;
            this.tb_update_datetime.Size = new System.Drawing.Size(127, 19);
            this.tb_update_datetime.TabIndex = 41;
            this.tb_update_datetime.TabStop = false;
            // 
            // tb_seihin_cd
            // 
            this.tb_seihin_cd.BackColor = System.Drawing.Color.PowderBlue;
            this.tb_seihin_cd.Location = new System.Drawing.Point(118, 10);
            this.tb_seihin_cd.MaxLength = 16;
            this.tb_seihin_cd.Name = "tb_seihin_cd";
            this.tb_seihin_cd.Size = new System.Drawing.Size(83, 19);
            this.tb_seihin_cd.TabIndex = 33;
            this.tb_seihin_cd.DoubleClick += new System.EventHandler(this.tb_seihin_cd_DoubleClick);
            this.tb_seihin_cd.Validating += new System.ComponentModel.CancelEventHandler(this.tb_seihin_cd_Validating);
            this.tb_seihin_cd.Validated += new System.EventHandler(this.tb_seihin_cd_Validated);
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.Gainsboro;
            this.textBox3.Location = new System.Drawing.Point(10, 88);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(38, 19);
            this.textBox3.TabIndex = 36;
            this.textBox3.TabStop = false;
            this.textBox3.Text = "作成";
            // 
            // tb_update_user_cd
            // 
            this.tb_update_user_cd.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_update_user_cd.Location = new System.Drawing.Point(48, 107);
            this.tb_update_user_cd.Name = "tb_update_user_cd";
            this.tb_update_user_cd.ReadOnly = true;
            this.tb_update_user_cd.Size = new System.Drawing.Size(45, 19);
            this.tb_update_user_cd.TabIndex = 40;
            this.tb_update_user_cd.TabStop = false;
            // 
            // tb_seihin_name
            // 
            this.tb_seihin_name.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_seihin_name.Location = new System.Drawing.Point(203, 10);
            this.tb_seihin_name.Name = "tb_seihin_name";
            this.tb_seihin_name.ReadOnly = true;
            this.tb_seihin_name.Size = new System.Drawing.Size(222, 19);
            this.tb_seihin_name.TabIndex = 34;
            this.tb_seihin_name.TabStop = false;
            // 
            // tb_create_user_cd
            // 
            this.tb_create_user_cd.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_create_user_cd.Location = new System.Drawing.Point(48, 88);
            this.tb_create_user_cd.Name = "tb_create_user_cd";
            this.tb_create_user_cd.ReadOnly = true;
            this.tb_create_user_cd.Size = new System.Drawing.Size(45, 19);
            this.tb_create_user_cd.TabIndex = 37;
            this.tb_create_user_cd.TabStop = false;
            // 
            // tb_create_datetime
            // 
            this.tb_create_datetime.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_create_datetime.Location = new System.Drawing.Point(93, 88);
            this.tb_create_datetime.Name = "tb_create_datetime";
            this.tb_create_datetime.ReadOnly = true;
            this.tb_create_datetime.Size = new System.Drawing.Size(127, 19);
            this.tb_create_datetime.TabIndex = 38;
            this.tb_create_datetime.TabStop = false;
            // 
            // textBox9
            // 
            this.textBox9.BackColor = System.Drawing.Color.Gainsboro;
            this.textBox9.Location = new System.Drawing.Point(10, 107);
            this.textBox9.Name = "textBox9";
            this.textBox9.ReadOnly = true;
            this.textBox9.Size = new System.Drawing.Size(38, 19);
            this.textBox9.TabIndex = 39;
            this.textBox9.TabStop = false;
            this.textBox9.Text = "更新";
            // 
            // dgv_seihin_kousei_name
            // 
            this.dgv_seihin_kousei_name.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_seihin_kousei_name.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_seihin_kousei_name.Location = new System.Drawing.Point(0, 0);
            this.dgv_seihin_kousei_name.Name = "dgv_seihin_kousei_name";
            this.dgv_seihin_kousei_name.ReadOnly = true;
            this.dgv_seihin_kousei_name.RowTemplate.Height = 21;
            this.dgv_seihin_kousei_name.Size = new System.Drawing.Size(392, 144);
            this.dgv_seihin_kousei_name.TabIndex = 43;
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
            this.splitContainer2.Panel2.Controls.Add(this.btn_tuika);
            this.splitContainer2.Panel2.Controls.Add(this.btn_touroku);
            this.splitContainer2.Panel2.Controls.Add(this.btn_syuuryou);
            this.splitContainer2.Size = new System.Drawing.Size(884, 476);
            this.splitContainer2.SplitterDistance = 431;
            this.splitContainer2.TabIndex = 0;
            this.splitContainer2.TabStop = false;
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
            this.splitContainer1.SplitterDistance = 60;
            this.splitContainer1.TabIndex = 5;
            this.splitContainer1.TabStop = false;
            // 
            // frm_seihin_kousei_m
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 562);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.ss_status);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "frm_seihin_kousei_m";
            this.Text = "製品構成マスタ";
            this.Load += new System.EventHandler(this.frm_seihin_kousei_m_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_seihin_kousei)).EndInit();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel1.PerformLayout();
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_seihin_kousei_name)).EndInit();
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

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn_tuika;
        private System.Windows.Forms.Button btn_touroku;
        private System.Windows.Forms.StatusStrip ss_status;
        private System.Windows.Forms.DataGridView dgv_seihin_kousei;
        private System.Windows.Forms.Button btn_syuuryou;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button btn_hardcopy;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox tb_seihin_cd;
        private System.Windows.Forms.TextBox tb_seihin_name;
        private System.Windows.Forms.TextBox tb_seihin_kousei_name;
        private System.Windows.Forms.TextBox tb_update_datetime;
        private System.Windows.Forms.TextBox tb_update_user_cd;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox tb_create_datetime;
        private System.Windows.Forms.TextBox tb_create_user_cd;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox tb_seihin_kousei_no;
        private System.Windows.Forms.DataGridView dgv_seihin_kousei_name;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.Label label1;
    }
}