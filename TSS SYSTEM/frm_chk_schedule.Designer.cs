namespace TSS_SYSTEM
{
    partial class frm_chk_schedule
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
            this.label2 = new System.Windows.Forms.Label();
            this.btn_kensaku = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_kensaku2 = new System.Windows.Forms.Button();
            this.cb_busyo = new System.Windows.Forms.ComboBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.tb_nouhin_yotei2 = new System.Windows.Forms.TextBox();
            this.tb_nouhin_yotei1 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.dgv_no_schedule = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dgv_m = new System.Windows.Forms.DataGridView();
            this.btn_csv = new System.Windows.Forms.Button();
            this.btn_insatu = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.dgv_no_koutei = new System.Windows.Forms.DataGridView();
            this.btn_kensaku3 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_no_schedule)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_m)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_no_koutei)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 537);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(884, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(154, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 18;
            this.label2.Text = "～";
            // 
            // btn_kensaku
            // 
            this.btn_kensaku.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_kensaku.Location = new System.Drawing.Point(628, 6);
            this.btn_kensaku.Name = "btn_kensaku";
            this.btn_kensaku.Size = new System.Drawing.Size(75, 23);
            this.btn_kensaku.TabIndex = 8;
            this.btn_kensaku.Text = "検索";
            this.btn_kensaku.UseVisualStyleBackColor = true;
            this.btn_kensaku.Click += new System.EventHandler(this.btn_kensaku_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.splitContainer3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(880, 333);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "検索条件";
            // 
            // btn_kensaku2
            // 
            this.btn_kensaku2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_kensaku2.Location = new System.Drawing.Point(709, 6);
            this.btn_kensaku2.Name = "btn_kensaku2";
            this.btn_kensaku2.Size = new System.Drawing.Size(75, 23);
            this.btn_kensaku2.TabIndex = 22;
            this.btn_kensaku2.Text = "検索2";
            this.btn_kensaku2.UseVisualStyleBackColor = true;
            this.btn_kensaku2.Click += new System.EventHandler(this.btn_kensaku2_Click);
            // 
            // cb_busyo
            // 
            this.cb_busyo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_busyo.FormattingEnabled = true;
            this.cb_busyo.Location = new System.Drawing.Point(80, 27);
            this.cb_busyo.Name = "cb_busyo";
            this.cb_busyo.Size = new System.Drawing.Size(121, 20);
            this.cb_busyo.TabIndex = 19;
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox3.Location = new System.Drawing.Point(6, 28);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(74, 19);
            this.textBox3.TabIndex = 20;
            this.textBox3.TabStop = false;
            this.textBox3.Text = "部署";
            // 
            // tb_nouhin_yotei2
            // 
            this.tb_nouhin_yotei2.Location = new System.Drawing.Point(171, 3);
            this.tb_nouhin_yotei2.MaxLength = 10;
            this.tb_nouhin_yotei2.Name = "tb_nouhin_yotei2";
            this.tb_nouhin_yotei2.Size = new System.Drawing.Size(74, 19);
            this.tb_nouhin_yotei2.TabIndex = 4;
            this.tb_nouhin_yotei2.Validating += new System.ComponentModel.CancelEventHandler(this.tb_seisan_yotei2_Validating);
            // 
            // tb_nouhin_yotei1
            // 
            this.tb_nouhin_yotei1.Location = new System.Drawing.Point(80, 3);
            this.tb_nouhin_yotei1.MaxLength = 10;
            this.tb_nouhin_yotei1.Name = "tb_nouhin_yotei1";
            this.tb_nouhin_yotei1.Size = new System.Drawing.Size(74, 19);
            this.tb_nouhin_yotei1.TabIndex = 3;
            this.tb_nouhin_yotei1.Validating += new System.ComponentModel.CancelEventHandler(this.tb_seisan_yotei1_Validating);
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox6.Location = new System.Drawing.Point(5, 3);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(75, 19);
            this.textBox6.TabIndex = 5;
            this.textBox6.TabStop = false;
            this.textBox6.Text = "納品予定日";
            // 
            // splitContainer3
            // 
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(3, 15);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.splitContainer4);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.dgv_no_koutei);
            this.splitContainer3.Size = new System.Drawing.Size(874, 315);
            this.splitContainer3.SplitterDistance = 187;
            this.splitContainer3.TabIndex = 23;
            // 
            // dgv_no_schedule
            // 
            this.dgv_no_schedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_no_schedule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_no_schedule.Location = new System.Drawing.Point(0, 0);
            this.dgv_no_schedule.Name = "dgv_no_schedule";
            this.dgv_no_schedule.RowTemplate.Height = 21;
            this.dgv_no_schedule.Size = new System.Drawing.Size(870, 124);
            this.dgv_no_schedule.TabIndex = 21;
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
            this.splitContainer1.Size = new System.Drawing.Size(884, 559);
            this.splitContainer1.SplitterDistance = 337;
            this.splitContainer1.TabIndex = 5;
            this.splitContainer1.TabStop = false;
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
            this.splitContainer2.Panel2.Controls.Add(this.btn_csv);
            this.splitContainer2.Panel2.Controls.Add(this.btn_insatu);
            this.splitContainer2.Panel2.Controls.Add(this.btn_cancel);
            this.splitContainer2.Size = new System.Drawing.Size(884, 218);
            this.splitContainer2.SplitterDistance = 138;
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
            this.dgv_m.Size = new System.Drawing.Size(880, 134);
            this.dgv_m.TabIndex = 0;
            this.dgv_m.DoubleClick += new System.EventHandler(this.dgv_m_DoubleClick);
            // 
            // btn_csv
            // 
            this.btn_csv.Location = new System.Drawing.Point(91, 3);
            this.btn_csv.Name = "btn_csv";
            this.btn_csv.Size = new System.Drawing.Size(75, 23);
            this.btn_csv.TabIndex = 2;
            this.btn_csv.Text = "CSV出力";
            this.btn_csv.UseVisualStyleBackColor = true;
            // 
            // btn_insatu
            // 
            this.btn_insatu.Location = new System.Drawing.Point(10, 3);
            this.btn_insatu.Name = "btn_insatu";
            this.btn_insatu.Size = new System.Drawing.Size(75, 23);
            this.btn_insatu.TabIndex = 1;
            this.btn_insatu.Text = "印刷";
            this.btn_insatu.UseVisualStyleBackColor = true;
            // 
            // btn_cancel
            // 
            this.btn_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_cancel.Location = new System.Drawing.Point(795, 3);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 3;
            this.btn_cancel.Text = "キャンセル";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // splitContainer4
            // 
            this.splitContainer4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.btn_kensaku3);
            this.splitContainer4.Panel1.Controls.Add(this.btn_kensaku2);
            this.splitContainer4.Panel1.Controls.Add(this.textBox6);
            this.splitContainer4.Panel1.Controls.Add(this.cb_busyo);
            this.splitContainer4.Panel1.Controls.Add(this.tb_nouhin_yotei2);
            this.splitContainer4.Panel1.Controls.Add(this.textBox3);
            this.splitContainer4.Panel1.Controls.Add(this.btn_kensaku);
            this.splitContainer4.Panel1.Controls.Add(this.tb_nouhin_yotei1);
            this.splitContainer4.Panel1.Controls.Add(this.label2);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.dgv_no_schedule);
            this.splitContainer4.Size = new System.Drawing.Size(874, 187);
            this.splitContainer4.SplitterDistance = 55;
            this.splitContainer4.TabIndex = 0;
            // 
            // dgv_no_koutei
            // 
            this.dgv_no_koutei.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_no_koutei.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_no_koutei.Location = new System.Drawing.Point(0, 0);
            this.dgv_no_koutei.Name = "dgv_no_koutei";
            this.dgv_no_koutei.RowTemplate.Height = 21;
            this.dgv_no_koutei.Size = new System.Drawing.Size(870, 120);
            this.dgv_no_koutei.TabIndex = 0;
            // 
            // btn_kensaku3
            // 
            this.btn_kensaku3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_kensaku3.Location = new System.Drawing.Point(790, 6);
            this.btn_kensaku3.Name = "btn_kensaku3";
            this.btn_kensaku3.Size = new System.Drawing.Size(75, 23);
            this.btn_kensaku3.TabIndex = 23;
            this.btn_kensaku3.Text = "検索3";
            this.btn_kensaku3.UseVisualStyleBackColor = true;
            this.btn_kensaku3.Click += new System.EventHandler(this.btn_kensaku3_Click);
            // 
            // frm_chk_schedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 559);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frm_chk_schedule";
            this.Text = "スケジュール差異チェック";
            this.Load += new System.EventHandler(this.frm_chk_schedule_Load);
            this.groupBox1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_no_schedule)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_m)).EndInit();
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel1.PerformLayout();
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_no_koutei)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_kensaku;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tb_nouhin_yotei2;
        private System.Windows.Forms.TextBox tb_nouhin_yotei1;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dgv_m;
        private System.Windows.Forms.Button btn_csv;
        private System.Windows.Forms.Button btn_insatu;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.ComboBox cb_busyo;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.DataGridView dgv_no_schedule;
        private System.Windows.Forms.Button btn_kensaku2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.Button btn_kensaku3;
        private System.Windows.Forms.DataGridView dgv_no_koutei;
    }
}