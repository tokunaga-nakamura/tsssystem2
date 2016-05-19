namespace TSS_SYSTEM
{
    partial class frm_nouhin_schedule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_nouhin_schedule));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btn_hardcopy = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.tb_nouhin_schedule_kbn = new System.Windows.Forms.TextBox();
            this.tb_nouhin_schedule_kbn_name = new System.Windows.Forms.TextBox();
            this.btn_hyouji = new System.Windows.Forms.Button();
            this.tb_torihikisaki_name = new System.Windows.Forms.TextBox();
            this.tb_torihikisaki_cd = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.nud_month = new System.Windows.Forms.NumericUpDown();
            this.nud_year = new System.Windows.Forms.NumericUpDown();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.dgv_nouhin_schedule = new System.Windows.Forms.DataGridView();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.dgv_nouhin_rireki = new System.Windows.Forms.DataGridView();
            this.btn_csv = new System.Windows.Forms.Button();
            this.btn_insatu = new System.Windows.Forms.Button();
            this.btn_syuuryou = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.btn_nouhin_jun_up = new System.Windows.Forms.Button();
            this.btn_nouhin_jun_down = new System.Windows.Forms.Button();
            this.btn_nouhin_jun_touroku = new System.Windows.Forms.Button();
            this.lbl_nouhin_jun = new System.Windows.Forms.Label();
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
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_nouhin_schedule)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_nouhin_rireki)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.btn_hardcopy);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(884, 561);
            this.splitContainer1.SplitterDistance = 63;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.TabStop = false;
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
            this.splitContainer2.Panel2.Controls.Add(this.btn_csv);
            this.splitContainer2.Panel2.Controls.Add(this.btn_insatu);
            this.splitContainer2.Panel2.Controls.Add(this.btn_syuuryou);
            this.splitContainer2.Size = new System.Drawing.Size(884, 494);
            this.splitContainer2.SplitterDistance = 436;
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
            this.splitContainer3.Panel1.Controls.Add(this.btn_nouhin_jun_touroku);
            this.splitContainer3.Panel1.Controls.Add(this.lbl_nouhin_jun);
            this.splitContainer3.Panel1.Controls.Add(this.btn_nouhin_jun_down);
            this.splitContainer3.Panel1.Controls.Add(this.btn_nouhin_jun_up);
            this.splitContainer3.Panel1.Controls.Add(this.textBox6);
            this.splitContainer3.Panel1.Controls.Add(this.tb_nouhin_schedule_kbn);
            this.splitContainer3.Panel1.Controls.Add(this.tb_nouhin_schedule_kbn_name);
            this.splitContainer3.Panel1.Controls.Add(this.btn_hyouji);
            this.splitContainer3.Panel1.Controls.Add(this.tb_torihikisaki_name);
            this.splitContainer3.Panel1.Controls.Add(this.tb_torihikisaki_cd);
            this.splitContainer3.Panel1.Controls.Add(this.textBox3);
            this.splitContainer3.Panel1.Controls.Add(this.textBox2);
            this.splitContainer3.Panel1.Controls.Add(this.textBox1);
            this.splitContainer3.Panel1.Controls.Add(this.nud_month);
            this.splitContainer3.Panel1.Controls.Add(this.nud_year);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer3.Size = new System.Drawing.Size(884, 436);
            this.splitContainer3.SplitterDistance = 82;
            this.splitContainer3.TabIndex = 0;
            this.splitContainer3.TabStop = false;
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox6.Location = new System.Drawing.Point(11, 53);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(122, 19);
            this.textBox6.TabIndex = 23;
            this.textBox6.TabStop = false;
            this.textBox6.Text = "納品スケジュール区分";
            // 
            // tb_nouhin_schedule_kbn
            // 
            this.tb_nouhin_schedule_kbn.BackColor = System.Drawing.Color.PowderBlue;
            this.tb_nouhin_schedule_kbn.Location = new System.Drawing.Point(133, 53);
            this.tb_nouhin_schedule_kbn.MaxLength = 2;
            this.tb_nouhin_schedule_kbn.Name = "tb_nouhin_schedule_kbn";
            this.tb_nouhin_schedule_kbn.Size = new System.Drawing.Size(24, 19);
            this.tb_nouhin_schedule_kbn.TabIndex = 3;
            this.tb_nouhin_schedule_kbn.DoubleClick += new System.EventHandler(this.tb_nouhin_schedule_kbn_DoubleClick);
            this.tb_nouhin_schedule_kbn.Validating += new System.ComponentModel.CancelEventHandler(this.tb_nouhin_schedule_kbn_Validating);
            // 
            // tb_nouhin_schedule_kbn_name
            // 
            this.tb_nouhin_schedule_kbn_name.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_nouhin_schedule_kbn_name.Location = new System.Drawing.Point(157, 53);
            this.tb_nouhin_schedule_kbn_name.Name = "tb_nouhin_schedule_kbn_name";
            this.tb_nouhin_schedule_kbn_name.ReadOnly = true;
            this.tb_nouhin_schedule_kbn_name.Size = new System.Drawing.Size(153, 19);
            this.tb_nouhin_schedule_kbn_name.TabIndex = 22;
            this.tb_nouhin_schedule_kbn_name.TabStop = false;
            // 
            // btn_hyouji
            // 
            this.btn_hyouji.Location = new System.Drawing.Point(409, 51);
            this.btn_hyouji.Name = "btn_hyouji";
            this.btn_hyouji.Size = new System.Drawing.Size(75, 23);
            this.btn_hyouji.TabIndex = 4;
            this.btn_hyouji.Text = "表示";
            this.btn_hyouji.UseVisualStyleBackColor = true;
            this.btn_hyouji.Click += new System.EventHandler(this.btn_hyouji_Click);
            // 
            // tb_torihikisaki_name
            // 
            this.tb_torihikisaki_name.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_torihikisaki_name.Location = new System.Drawing.Point(133, 28);
            this.tb_torihikisaki_name.Name = "tb_torihikisaki_name";
            this.tb_torihikisaki_name.ReadOnly = true;
            this.tb_torihikisaki_name.Size = new System.Drawing.Size(351, 19);
            this.tb_torihikisaki_name.TabIndex = 6;
            this.tb_torihikisaki_name.TabStop = false;
            // 
            // tb_torihikisaki_cd
            // 
            this.tb_torihikisaki_cd.BackColor = System.Drawing.Color.PowderBlue;
            this.tb_torihikisaki_cd.Location = new System.Drawing.Point(84, 28);
            this.tb_torihikisaki_cd.MaxLength = 6;
            this.tb_torihikisaki_cd.Name = "tb_torihikisaki_cd";
            this.tb_torihikisaki_cd.Size = new System.Drawing.Size(49, 19);
            this.tb_torihikisaki_cd.TabIndex = 2;
            this.tb_torihikisaki_cd.DoubleClick += new System.EventHandler(this.tb_torihikisaki_cd_DoubleClick);
            this.tb_torihikisaki_cd.Validating += new System.ComponentModel.CancelEventHandler(this.tb_torihikisaki_cd_Validating);
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox3.Location = new System.Drawing.Point(10, 28);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(74, 19);
            this.textBox3.TabIndex = 4;
            this.textBox3.TabStop = false;
            this.textBox3.Text = "取引先コード";
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox2.Location = new System.Drawing.Point(100, 3);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(24, 19);
            this.textBox2.TabIndex = 1;
            this.textBox2.TabStop = false;
            this.textBox2.Text = "月";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox1.Location = new System.Drawing.Point(10, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(24, 19);
            this.textBox1.TabIndex = 0;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "年";
            // 
            // nud_month
            // 
            this.nud_month.Location = new System.Drawing.Point(124, 3);
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
            this.nud_month.Size = new System.Drawing.Size(36, 19);
            this.nud_month.TabIndex = 1;
            this.nud_month.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nud_year
            // 
            this.nud_year.Location = new System.Drawing.Point(34, 3);
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
            this.nud_year.Size = new System.Drawing.Size(60, 19);
            this.nud_year.TabIndex = 0;
            this.nud_year.Value = new decimal(new int[] {
            2015,
            0,
            0,
            0});
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
            this.splitContainer4.Panel1.Controls.Add(this.dgv_nouhin_schedule);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.splitContainer5);
            this.splitContainer4.Size = new System.Drawing.Size(884, 350);
            this.splitContainer4.SplitterDistance = 179;
            this.splitContainer4.TabIndex = 0;
            this.splitContainer4.TabStop = false;
            // 
            // dgv_nouhin_schedule
            // 
            this.dgv_nouhin_schedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_nouhin_schedule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_nouhin_schedule.Location = new System.Drawing.Point(0, 0);
            this.dgv_nouhin_schedule.Name = "dgv_nouhin_schedule";
            this.dgv_nouhin_schedule.RowTemplate.Height = 21;
            this.dgv_nouhin_schedule.Size = new System.Drawing.Size(880, 175);
            this.dgv_nouhin_schedule.TabIndex = 0;
            this.dgv_nouhin_schedule.TabStop = false;
            this.dgv_nouhin_schedule.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_nouhin_schedule_CellMouseClick);
            // 
            // splitContainer5
            // 
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer5.Location = new System.Drawing.Point(0, 0);
            this.splitContainer5.Name = "splitContainer5";
            this.splitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.dgv_nouhin_rireki);
            this.splitContainer5.Size = new System.Drawing.Size(880, 163);
            this.splitContainer5.SplitterDistance = 26;
            this.splitContainer5.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(339, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "更新履歴　（更新履歴は上に表示されている受注分が表示されます。）";
            // 
            // dgv_nouhin_rireki
            // 
            this.dgv_nouhin_rireki.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_nouhin_rireki.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_nouhin_rireki.Location = new System.Drawing.Point(0, 0);
            this.dgv_nouhin_rireki.Name = "dgv_nouhin_rireki";
            this.dgv_nouhin_rireki.RowTemplate.Height = 21;
            this.dgv_nouhin_rireki.Size = new System.Drawing.Size(880, 133);
            this.dgv_nouhin_rireki.TabIndex = 0;
            this.dgv_nouhin_rireki.TabStop = false;
            this.dgv_nouhin_rireki.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_nouhin_rireki_CellMouseClick);
            // 
            // btn_csv
            // 
            this.btn_csv.Location = new System.Drawing.Point(91, 3);
            this.btn_csv.Name = "btn_csv";
            this.btn_csv.Size = new System.Drawing.Size(75, 23);
            this.btn_csv.TabIndex = 1;
            this.btn_csv.Text = "CSV出力";
            this.btn_csv.UseVisualStyleBackColor = true;
            this.btn_csv.Click += new System.EventHandler(this.btn_csv_Click);
            // 
            // btn_insatu
            // 
            this.btn_insatu.Location = new System.Drawing.Point(10, 3);
            this.btn_insatu.Name = "btn_insatu";
            this.btn_insatu.Size = new System.Drawing.Size(75, 23);
            this.btn_insatu.TabIndex = 0;
            this.btn_insatu.Text = "印刷";
            this.btn_insatu.UseVisualStyleBackColor = true;
            this.btn_insatu.Click += new System.EventHandler(this.btn_insatu_Click);
            // 
            // btn_syuuryou
            // 
            this.btn_syuuryou.Location = new System.Drawing.Point(795, 3);
            this.btn_syuuryou.Name = "btn_syuuryou";
            this.btn_syuuryou.Size = new System.Drawing.Size(75, 23);
            this.btn_syuuryou.TabIndex = 2;
            this.btn_syuuryou.Text = "終了";
            this.btn_syuuryou.UseVisualStyleBackColor = true;
            this.btn_syuuryou.Click += new System.EventHandler(this.btn_syuuryou_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 539);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(884, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // btn_nouhin_jun_up
            // 
            this.btn_nouhin_jun_up.Location = new System.Drawing.Point(546, 20);
            this.btn_nouhin_jun_up.Name = "btn_nouhin_jun_up";
            this.btn_nouhin_jun_up.Size = new System.Drawing.Size(75, 23);
            this.btn_nouhin_jun_up.TabIndex = 0;
            this.btn_nouhin_jun_up.Text = "上へ";
            this.btn_nouhin_jun_up.UseVisualStyleBackColor = true;
            // 
            // btn_nouhin_jun_down
            // 
            this.btn_nouhin_jun_down.Location = new System.Drawing.Point(546, 49);
            this.btn_nouhin_jun_down.Name = "btn_nouhin_jun_down";
            this.btn_nouhin_jun_down.Size = new System.Drawing.Size(75, 23);
            this.btn_nouhin_jun_down.TabIndex = 1;
            this.btn_nouhin_jun_down.Text = "下へ";
            this.btn_nouhin_jun_down.UseVisualStyleBackColor = true;
            // 
            // btn_nouhin_jun_touroku
            // 
            this.btn_nouhin_jun_touroku.Location = new System.Drawing.Point(627, 49);
            this.btn_nouhin_jun_touroku.Name = "btn_nouhin_jun_touroku";
            this.btn_nouhin_jun_touroku.Size = new System.Drawing.Size(75, 23);
            this.btn_nouhin_jun_touroku.TabIndex = 2;
            this.btn_nouhin_jun_touroku.Text = "納品順登録";
            this.btn_nouhin_jun_touroku.UseVisualStyleBackColor = true;
            // 
            // lbl_nouhin_jun
            // 
            this.lbl_nouhin_jun.AutoSize = true;
            this.lbl_nouhin_jun.Location = new System.Drawing.Point(544, 5);
            this.lbl_nouhin_jun.Name = "lbl_nouhin_jun";
            this.lbl_nouhin_jun.Size = new System.Drawing.Size(123, 12);
            this.lbl_nouhin_jun.TabIndex = 25;
            this.lbl_nouhin_jun.Text = "納品順の変更ができます";
            // 
            // frm_nouhin_schedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitContainer1);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "frm_nouhin_schedule";
            this.Text = "納品スケジュール";
            this.Load += new System.EventHandler(this.frm_nouhin_schedule_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
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
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_nouhin_schedule)).EndInit();
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel1.PerformLayout();
            this.splitContainer5.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
            this.splitContainer5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_nouhin_rireki)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btn_hardcopy;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button btn_syuuryou;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Button btn_csv;
        private System.Windows.Forms.Button btn_insatu;
        private System.Windows.Forms.TextBox tb_torihikisaki_name;
        private System.Windows.Forms.TextBox tb_torihikisaki_cd;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.NumericUpDown nud_month;
        private System.Windows.Forms.NumericUpDown nud_year;
        private System.Windows.Forms.Button btn_hyouji;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.DataGridView dgv_nouhin_schedule;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgv_nouhin_rireki;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox tb_nouhin_schedule_kbn;
        private System.Windows.Forms.TextBox tb_nouhin_schedule_kbn_name;
        private System.Windows.Forms.Button btn_nouhin_jun_touroku;
        private System.Windows.Forms.Label lbl_nouhin_jun;
        private System.Windows.Forms.Button btn_nouhin_jun_down;
        private System.Windows.Forms.Button btn_nouhin_jun_up;
    }
}