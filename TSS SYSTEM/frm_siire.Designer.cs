﻿namespace TSS_SYSTEM
{
    partial class frm_siire
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_siire));
            this.label3 = new System.Windows.Forms.Label();
            this.dtp_siire_date = new System.Windows.Forms.DateTimePicker();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.tb_torihikisaki_name = new System.Windows.Forms.TextBox();
            this.tb_torihikisaki_cd = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tb_siire_no = new System.Windows.Forms.TextBox();
            this.btn_touroku = new System.Windows.Forms.Button();
            this.ss_status = new System.Windows.Forms.StatusStrip();
            this.tb_siire_denpyou_no = new System.Windows.Forms.TextBox();
            this.dgv_siire = new System.Windows.Forms.DataGridView();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.btn_syuuryou = new System.Windows.Forms.Button();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.tb_update_datetime = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.tb_update_user_cd = new System.Windows.Forms.TextBox();
            this.tb_create_user_cd = new System.Windows.Forms.TextBox();
            this.tb_create_datetime = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.tb_siire_goukei = new System.Windows.Forms.TextBox();
            this.btn_hardcopy = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_siire)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
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
            // dtp_siire_date
            // 
            this.dtp_siire_date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_siire_date.Location = new System.Drawing.Point(118, 35);
            this.dtp_siire_date.Name = "dtp_siire_date";
            this.dtp_siire_date.Size = new System.Drawing.Size(107, 19);
            this.dtp_siire_date.TabIndex = 1;
            this.dtp_siire_date.ValueChanged += new System.EventHandler(this.dtp_siire_date_ValueChanged);
            // 
            // textBox11
            // 
            this.textBox11.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox11.Location = new System.Drawing.Point(10, 35);
            this.textBox11.Name = "textBox11";
            this.textBox11.ReadOnly = true;
            this.textBox11.Size = new System.Drawing.Size(108, 19);
            this.textBox11.TabIndex = 24;
            this.textBox11.TabStop = false;
            this.textBox11.Text = "仕入計上日";
            // 
            // tb_torihikisaki_name
            // 
            this.tb_torihikisaki_name.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_torihikisaki_name.Location = new System.Drawing.Point(168, 85);
            this.tb_torihikisaki_name.MaxLength = 2;
            this.tb_torihikisaki_name.Name = "tb_torihikisaki_name";
            this.tb_torihikisaki_name.ReadOnly = true;
            this.tb_torihikisaki_name.Size = new System.Drawing.Size(154, 19);
            this.tb_torihikisaki_name.TabIndex = 4;
            this.tb_torihikisaki_name.TabStop = false;
            // 
            // tb_torihikisaki_cd
            // 
            this.tb_torihikisaki_cd.BackColor = System.Drawing.Color.PowderBlue;
            this.tb_torihikisaki_cd.Location = new System.Drawing.Point(118, 85);
            this.tb_torihikisaki_cd.MaxLength = 6;
            this.tb_torihikisaki_cd.Name = "tb_torihikisaki_cd";
            this.tb_torihikisaki_cd.Size = new System.Drawing.Size(50, 19);
            this.tb_torihikisaki_cd.TabIndex = 3;
            this.tb_torihikisaki_cd.DoubleClick += new System.EventHandler(this.tb_torihikisaki_cd_DoubleClick);
            this.tb_torihikisaki_cd.Validating += new System.ComponentModel.CancelEventHandler(this.tb_torihikisaki_cd_Validating);
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox4.Location = new System.Drawing.Point(10, 85);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(108, 19);
            this.textBox4.TabIndex = 9;
            this.textBox4.TabStop = false;
            this.textBox4.Text = "取引先コード";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox1.Location = new System.Drawing.Point(10, 10);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(108, 19);
            this.textBox1.TabIndex = 0;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "仕入番号";
            // 
            // tb_siire_no
            // 
            this.tb_siire_no.BackColor = System.Drawing.SystemColors.Window;
            this.tb_siire_no.Location = new System.Drawing.Point(118, 10);
            this.tb_siire_no.MaxLength = 10;
            this.tb_siire_no.Name = "tb_siire_no";
            this.tb_siire_no.Size = new System.Drawing.Size(67, 19);
            this.tb_siire_no.TabIndex = 0;
            this.tb_siire_no.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tb_siire_no.Validating += new System.ComponentModel.CancelEventHandler(this.tb_siire_no_Validating);
            // 
            // btn_touroku
            // 
            this.btn_touroku.Location = new System.Drawing.Point(3, 3);
            this.btn_touroku.Name = "btn_touroku";
            this.btn_touroku.Size = new System.Drawing.Size(75, 23);
            this.btn_touroku.TabIndex = 4;
            this.btn_touroku.Text = "登録";
            this.btn_touroku.UseVisualStyleBackColor = true;
            this.btn_touroku.Click += new System.EventHandler(this.btn_touroku_Click);
            // 
            // ss_status
            // 
            this.ss_status.Location = new System.Drawing.Point(0, 539);
            this.ss_status.Name = "ss_status";
            this.ss_status.Size = new System.Drawing.Size(884, 22);
            this.ss_status.TabIndex = 4;
            this.ss_status.Text = "statusStrip1";
            // 
            // tb_siire_denpyou_no
            // 
            this.tb_siire_denpyou_no.Location = new System.Drawing.Point(118, 60);
            this.tb_siire_denpyou_no.MaxLength = 40;
            this.tb_siire_denpyou_no.Name = "tb_siire_denpyou_no";
            this.tb_siire_denpyou_no.Size = new System.Drawing.Size(239, 19);
            this.tb_siire_denpyou_no.TabIndex = 2;
            this.tb_siire_denpyou_no.Validating += new System.ComponentModel.CancelEventHandler(this.tb_siire_denpyou_no_Validating);
            // 
            // dgv_siire
            // 
            this.dgv_siire.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_siire.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_siire.Location = new System.Drawing.Point(0, 0);
            this.dgv_siire.Name = "dgv_siire";
            this.dgv_siire.RowHeadersVisible = false;
            this.dgv_siire.RowTemplate.Height = 21;
            this.dgv_siire.Size = new System.Drawing.Size(880, 283);
            this.dgv_siire.TabIndex = 5;
            this.dgv_siire.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_siire_CellDoubleClick);
            this.dgv_siire.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_siire_CellEndEdit);
            this.dgv_siire.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgv_siire_CellValidating);
            this.dgv_siire.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgv_siire_DataError);
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox2.Location = new System.Drawing.Point(10, 60);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(108, 19);
            this.textBox2.TabIndex = 3;
            this.textBox2.TabStop = false;
            this.textBox2.Text = "仕入伝票番号";
            // 
            // btn_syuuryou
            // 
            this.btn_syuuryou.Location = new System.Drawing.Point(795, 3);
            this.btn_syuuryou.Name = "btn_syuuryou";
            this.btn_syuuryou.Size = new System.Drawing.Size(75, 23);
            this.btn_syuuryou.TabIndex = 5;
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
            this.splitContainer3.Panel1.Controls.Add(this.tb_update_datetime);
            this.splitContainer3.Panel1.Controls.Add(this.textBox3);
            this.splitContainer3.Panel1.Controls.Add(this.tb_update_user_cd);
            this.splitContainer3.Panel1.Controls.Add(this.tb_create_user_cd);
            this.splitContainer3.Panel1.Controls.Add(this.tb_create_datetime);
            this.splitContainer3.Panel1.Controls.Add(this.textBox9);
            this.splitContainer3.Panel1.Controls.Add(this.label3);
            this.splitContainer3.Panel1.Controls.Add(this.dtp_siire_date);
            this.splitContainer3.Panel1.Controls.Add(this.textBox11);
            this.splitContainer3.Panel1.Controls.Add(this.tb_torihikisaki_name);
            this.splitContainer3.Panel1.Controls.Add(this.tb_torihikisaki_cd);
            this.splitContainer3.Panel1.Controls.Add(this.textBox4);
            this.splitContainer3.Panel1.Controls.Add(this.textBox1);
            this.splitContainer3.Panel1.Controls.Add(this.tb_siire_no);
            this.splitContainer3.Panel1.Controls.Add(this.tb_siire_denpyou_no);
            this.splitContainer3.Panel1.Controls.Add(this.textBox2);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.dgv_siire);
            this.splitContainer3.Size = new System.Drawing.Size(884, 407);
            this.splitContainer3.SplitterDistance = 116;
            this.splitContainer3.TabIndex = 8;
            // 
            // tb_update_datetime
            // 
            this.tb_update_datetime.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_update_datetime.Location = new System.Drawing.Point(743, 85);
            this.tb_update_datetime.Name = "tb_update_datetime";
            this.tb_update_datetime.ReadOnly = true;
            this.tb_update_datetime.Size = new System.Drawing.Size(127, 19);
            this.tb_update_datetime.TabIndex = 47;
            this.tb_update_datetime.TabStop = false;
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.Gainsboro;
            this.textBox3.Location = new System.Drawing.Point(660, 66);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(38, 19);
            this.textBox3.TabIndex = 42;
            this.textBox3.TabStop = false;
            this.textBox3.Text = "作成";
            // 
            // tb_update_user_cd
            // 
            this.tb_update_user_cd.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_update_user_cd.Location = new System.Drawing.Point(698, 85);
            this.tb_update_user_cd.Name = "tb_update_user_cd";
            this.tb_update_user_cd.ReadOnly = true;
            this.tb_update_user_cd.Size = new System.Drawing.Size(45, 19);
            this.tb_update_user_cd.TabIndex = 46;
            this.tb_update_user_cd.TabStop = false;
            // 
            // tb_create_user_cd
            // 
            this.tb_create_user_cd.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_create_user_cd.Location = new System.Drawing.Point(698, 66);
            this.tb_create_user_cd.Name = "tb_create_user_cd";
            this.tb_create_user_cd.ReadOnly = true;
            this.tb_create_user_cd.Size = new System.Drawing.Size(45, 19);
            this.tb_create_user_cd.TabIndex = 43;
            this.tb_create_user_cd.TabStop = false;
            // 
            // tb_create_datetime
            // 
            this.tb_create_datetime.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_create_datetime.Location = new System.Drawing.Point(743, 66);
            this.tb_create_datetime.Name = "tb_create_datetime";
            this.tb_create_datetime.ReadOnly = true;
            this.tb_create_datetime.Size = new System.Drawing.Size(127, 19);
            this.tb_create_datetime.TabIndex = 44;
            this.tb_create_datetime.TabStop = false;
            // 
            // textBox9
            // 
            this.textBox9.BackColor = System.Drawing.Color.Gainsboro;
            this.textBox9.Location = new System.Drawing.Point(660, 85);
            this.textBox9.Name = "textBox9";
            this.textBox9.ReadOnly = true;
            this.textBox9.Size = new System.Drawing.Size(38, 19);
            this.textBox9.TabIndex = 45;
            this.textBox9.TabStop = false;
            this.textBox9.Text = "更新";
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
            this.splitContainer2.Size = new System.Drawing.Size(884, 497);
            this.splitContainer2.SplitterDistance = 407;
            this.splitContainer2.TabIndex = 0;
            this.splitContainer2.TabStop = false;
            // 
            // splitContainer4
            // 
            this.splitContainer4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer4.IsSplitterFixed = true;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.textBox5);
            this.splitContainer4.Panel1.Controls.Add(this.tb_siire_goukei);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.btn_syuuryou);
            this.splitContainer4.Panel2.Controls.Add(this.btn_touroku);
            this.splitContainer4.Size = new System.Drawing.Size(884, 86);
            this.splitContainer4.SplitterDistance = 28;
            this.splitContainer4.TabIndex = 6;
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox5.Location = new System.Drawing.Point(590, 3);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(108, 19);
            this.textBox5.TabIndex = 48;
            this.textBox5.TabStop = false;
            this.textBox5.Text = "仕入合計";
            // 
            // tb_siire_goukei
            // 
            this.tb_siire_goukei.BackColor = System.Drawing.SystemColors.Window;
            this.tb_siire_goukei.Location = new System.Drawing.Point(698, 3);
            this.tb_siire_goukei.MaxLength = 10;
            this.tb_siire_goukei.Name = "tb_siire_goukei";
            this.tb_siire_goukei.Size = new System.Drawing.Size(172, 19);
            this.tb_siire_goukei.TabIndex = 49;
            this.tb_siire_goukei.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.btn_hardcopy);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(884, 561);
            this.splitContainer1.SplitterDistance = 60;
            this.splitContainer1.TabIndex = 5;
            this.splitContainer1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(366, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(504, 12);
            this.label1.TabIndex = 33;
            this.label1.Text = "一時的な単価変更はこの画面で行ってください。恒久的な単価変更は部品マスタメンテで修正してください。";
            // 
            // frm_siire
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.ss_status);
            this.Controls.Add(this.splitContainer1);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "frm_siire";
            this.Text = "仕入処理";
            this.Load += new System.EventHandler(this.frm_siire_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_siire)).EndInit();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel1.PerformLayout();
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtp_siire_date;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.TextBox tb_torihikisaki_name;
        private System.Windows.Forms.TextBox tb_torihikisaki_cd;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox tb_siire_no;
        private System.Windows.Forms.Button btn_touroku;
        private System.Windows.Forms.StatusStrip ss_status;
        private System.Windows.Forms.TextBox tb_siire_denpyou_no;
        private System.Windows.Forms.DataGridView dgv_siire;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button btn_syuuryou;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button btn_hardcopy;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_update_datetime;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox tb_update_user_cd;
        private System.Windows.Forms.TextBox tb_create_user_cd;
        private System.Windows.Forms.TextBox tb_create_datetime;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox tb_siire_goukei;
    }
}