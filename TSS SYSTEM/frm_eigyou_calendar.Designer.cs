﻿namespace TSS_SYSTEM
{
    partial class frm_eigyou_calendar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_eigyou_calendar));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_hardcopy = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.btn_hyouji = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.nud_month = new System.Windows.Forms.NumericUpDown();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.nud_year = new System.Windows.Forms.NumericUpDown();
            this.dgv_calendar = new System.Windows.Forms.DataGridView();
            this.btn_touroku = new System.Windows.Forms.Button();
            this.btn_syuuryou = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_month)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_year)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_calendar)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.btn_hardcopy);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(884, 539);
            this.splitContainer1.SplitterDistance = 63;
            this.splitContainer1.TabIndex = 1;
            this.splitContainer1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(247, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "営業区分 1:休日 2:変則出勤";
            // 
            // btn_hardcopy
            // 
            this.btn_hardcopy.Image = ((System.Drawing.Image)(resources.GetObject("btn_hardcopy.Image")));
            this.btn_hardcopy.Location = new System.Drawing.Point(12, 12);
            this.btn_hardcopy.Name = "btn_hardcopy";
            this.btn_hardcopy.Size = new System.Drawing.Size(36, 36);
            this.btn_hardcopy.TabIndex = 0;
            this.btn_hardcopy.UseVisualStyleBackColor = true;
            this.btn_hardcopy.Click += new System.EventHandler(this.btn_hardcopy_Click);
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
            this.splitContainer2.Panel2.Controls.Add(this.btn_touroku);
            this.splitContainer2.Panel2.Controls.Add(this.btn_syuuryou);
            this.splitContainer2.Size = new System.Drawing.Size(884, 472);
            this.splitContainer2.SplitterDistance = 436;
            this.splitContainer2.TabIndex = 0;
            this.splitContainer2.TabStop = false;
            // 
            // splitContainer3
            // 
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.btn_hyouji);
            this.splitContainer3.Panel1.Controls.Add(this.textBox2);
            this.splitContainer3.Panel1.Controls.Add(this.nud_month);
            this.splitContainer3.Panel1.Controls.Add(this.textBox1);
            this.splitContainer3.Panel1.Controls.Add(this.nud_year);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.dgv_calendar);
            this.splitContainer3.Size = new System.Drawing.Size(884, 436);
            this.splitContainer3.SplitterDistance = 28;
            this.splitContainer3.TabIndex = 0;
            this.splitContainer3.TabStop = false;
            // 
            // btn_hyouji
            // 
            this.btn_hyouji.Location = new System.Drawing.Point(162, 1);
            this.btn_hyouji.Name = "btn_hyouji";
            this.btn_hyouji.Size = new System.Drawing.Size(75, 23);
            this.btn_hyouji.TabIndex = 4;
            this.btn_hyouji.Text = "表示";
            this.btn_hyouji.UseVisualStyleBackColor = true;
            this.btn_hyouji.Click += new System.EventHandler(this.btn_hyouji_Click);
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox2.Location = new System.Drawing.Point(133, 3);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(23, 19);
            this.textBox2.TabIndex = 3;
            this.textBox2.TabStop = false;
            this.textBox2.Text = "月";
            // 
            // nud_month
            // 
            this.nud_month.Location = new System.Drawing.Point(95, 3);
            this.nud_month.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.nud_month.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_month.Name = "nud_month";
            this.nud_month.Size = new System.Drawing.Size(38, 19);
            this.nud_month.TabIndex = 2;
            this.nud_month.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox1.Location = new System.Drawing.Point(66, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(23, 19);
            this.textBox1.TabIndex = 1;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "年";
            // 
            // nud_year
            // 
            this.nud_year.Location = new System.Drawing.Point(12, 3);
            this.nud_year.Maximum = new decimal(new int[] {
            2099,
            0,
            0,
            0});
            this.nud_year.Minimum = new decimal(new int[] {
            2015,
            0,
            0,
            0});
            this.nud_year.Name = "nud_year";
            this.nud_year.Size = new System.Drawing.Size(54, 19);
            this.nud_year.TabIndex = 0;
            this.nud_year.Value = new decimal(new int[] {
            2015,
            0,
            0,
            0});
            // 
            // dgv_calendar
            // 
            this.dgv_calendar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_calendar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_calendar.Location = new System.Drawing.Point(0, 0);
            this.dgv_calendar.Name = "dgv_calendar";
            this.dgv_calendar.RowTemplate.Height = 21;
            this.dgv_calendar.Size = new System.Drawing.Size(880, 400);
            this.dgv_calendar.TabIndex = 0;
            this.dgv_calendar.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_calendar_CellMouseDoubleClick);
            this.dgv_calendar.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_calendar_CellValueChanged);
            // 
            // btn_touroku
            // 
            this.btn_touroku.Location = new System.Drawing.Point(10, 3);
            this.btn_touroku.Name = "btn_touroku";
            this.btn_touroku.Size = new System.Drawing.Size(75, 23);
            this.btn_touroku.TabIndex = 1;
            this.btn_touroku.Text = "登録";
            this.btn_touroku.UseVisualStyleBackColor = true;
            this.btn_touroku.Click += new System.EventHandler(this.btn_touroku_Click);
            // 
            // btn_syuuryou
            // 
            this.btn_syuuryou.Location = new System.Drawing.Point(795, 3);
            this.btn_syuuryou.Name = "btn_syuuryou";
            this.btn_syuuryou.Size = new System.Drawing.Size(75, 23);
            this.btn_syuuryou.TabIndex = 0;
            this.btn_syuuryou.Text = "終了";
            this.btn_syuuryou.UseVisualStyleBackColor = true;
            this.btn_syuuryou.Click += new System.EventHandler(this.btn_syuuryou_Click);
            // 
            // frm_eigyou_calendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "frm_eigyou_calendar";
            this.Text = "営業カレンダー";
            this.Load += new System.EventHandler(this.frm_eigyou_calender_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nud_month)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_year)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_calendar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btn_hardcopy;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button btn_syuuryou;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.NumericUpDown nud_month;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.NumericUpDown nud_year;
        private System.Windows.Forms.DataGridView dgv_calendar;
        private System.Windows.Forms.Button btn_touroku;
        private System.Windows.Forms.Button btn_hyouji;
        private System.Windows.Forms.Label label1;
    }
}