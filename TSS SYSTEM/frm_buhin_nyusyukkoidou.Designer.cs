namespace TSS_SYSTEM
{
    partial class frm_buhin_nyusyukkoidou
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_buhin_nyusyukkoidou));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btn_hardcopy = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.dtp_buhin_syori_date = new System.Windows.Forms.DateTimePicker();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.tb_torihikisaki_name = new System.Windows.Forms.TextBox();
            this.tb_torihikisaki_cd = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.tb_seq = new System.Windows.Forms.TextBox();
            this.tb_denpyou_no = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dgv_nyusyukkoidou = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_touroku = new System.Windows.Forms.Button();
            this.btn_syuuryou = new System.Windows.Forms.Button();
            this.ss_status = new System.Windows.Forms.StatusStrip();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
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
            ((System.ComponentModel.ISupportInitialize)(this.dgv_nyusyukkoidou)).BeginInit();
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
            this.splitContainer1.Size = new System.Drawing.Size(884, 540);
            this.splitContainer1.SplitterDistance = 60;
            this.splitContainer1.TabIndex = 3;
            this.splitContainer1.TabStop = false;
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
            this.splitContainer2.Size = new System.Drawing.Size(884, 476);
            this.splitContainer2.SplitterDistance = 440;
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
            this.splitContainer3.Panel1.Controls.Add(this.label1);
            this.splitContainer3.Panel1.Controls.Add(this.dtp_buhin_syori_date);
            this.splitContainer3.Panel1.Controls.Add(this.textBox2);
            this.splitContainer3.Panel1.Controls.Add(this.tb_torihikisaki_name);
            this.splitContainer3.Panel1.Controls.Add(this.tb_torihikisaki_cd);
            this.splitContainer3.Panel1.Controls.Add(this.textBox6);
            this.splitContainer3.Panel1.Controls.Add(this.textBox7);
            this.splitContainer3.Panel1.Controls.Add(this.tb_seq);
            this.splitContainer3.Panel1.Controls.Add(this.tb_denpyou_no);
            this.splitContainer3.Panel1.Controls.Add(this.textBox9);
            this.splitContainer3.Panel1.Controls.Add(this.label3);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.dgv_nyusyukkoidou);
            this.splitContainer3.Size = new System.Drawing.Size(884, 440);
            this.splitContainer3.SplitterDistance = 145;
            this.splitContainer3.TabIndex = 8;
            // 
            // dtp_buhin_syori_date
            // 
            this.dtp_buhin_syori_date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_buhin_syori_date.Location = new System.Drawing.Point(127, 65);
            this.dtp_buhin_syori_date.Name = "dtp_buhin_syori_date";
            this.dtp_buhin_syori_date.Size = new System.Drawing.Size(107, 19);
            this.dtp_buhin_syori_date.TabIndex = 0;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox2.Location = new System.Drawing.Point(19, 65);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(108, 19);
            this.textBox2.TabIndex = 41;
            this.textBox2.TabStop = false;
            this.textBox2.Text = "日付";
            // 
            // tb_torihikisaki_name
            // 
            this.tb_torihikisaki_name.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_torihikisaki_name.Location = new System.Drawing.Point(170, 115);
            this.tb_torihikisaki_name.MaxLength = 2;
            this.tb_torihikisaki_name.Name = "tb_torihikisaki_name";
            this.tb_torihikisaki_name.ReadOnly = true;
            this.tb_torihikisaki_name.Size = new System.Drawing.Size(196, 19);
            this.tb_torihikisaki_name.TabIndex = 39;
            // 
            // tb_torihikisaki_cd
            // 
            this.tb_torihikisaki_cd.BackColor = System.Drawing.Color.PowderBlue;
            this.tb_torihikisaki_cd.Location = new System.Drawing.Point(127, 115);
            this.tb_torihikisaki_cd.MaxLength = 6;
            this.tb_torihikisaki_cd.Name = "tb_torihikisaki_cd";
            this.tb_torihikisaki_cd.Size = new System.Drawing.Size(45, 19);
            this.tb_torihikisaki_cd.TabIndex = 2;
            this.tb_torihikisaki_cd.DoubleClick += new System.EventHandler(this.tb_torihikisaki_cd_DoubleClick);
            this.tb_torihikisaki_cd.Validating += new System.ComponentModel.CancelEventHandler(this.tb_torihikisaki_cd_Validating);
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox6.Location = new System.Drawing.Point(19, 115);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(108, 19);
            this.textBox6.TabIndex = 38;
            this.textBox6.TabStop = false;
            this.textBox6.Text = "取引先コード";
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox7.Location = new System.Drawing.Point(19, 40);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(108, 19);
            this.textBox7.TabIndex = 34;
            this.textBox7.TabStop = false;
            this.textBox7.Text = "入出庫番号";
            // 
            // tb_seq
            // 
            this.tb_seq.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_seq.Location = new System.Drawing.Point(127, 40);
            this.tb_seq.MaxLength = 2;
            this.tb_seq.Name = "tb_seq";
            this.tb_seq.ReadOnly = true;
            this.tb_seq.Size = new System.Drawing.Size(69, 19);
            this.tb_seq.TabIndex = 40;
            this.tb_seq.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tb_denpyou_no
            // 
            this.tb_denpyou_no.Location = new System.Drawing.Point(127, 90);
            this.tb_denpyou_no.MaxLength = 40;
            this.tb_denpyou_no.Name = "tb_denpyou_no";
            this.tb_denpyou_no.Size = new System.Drawing.Size(239, 19);
            this.tb_denpyou_no.TabIndex = 1;
            this.tb_denpyou_no.Validating += new System.ComponentModel.CancelEventHandler(this.tb_denpyou_no_Validating);
            // 
            // textBox9
            // 
            this.textBox9.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox9.Location = new System.Drawing.Point(19, 90);
            this.textBox9.Name = "textBox9";
            this.textBox9.ReadOnly = true;
            this.textBox9.Size = new System.Drawing.Size(108, 19);
            this.textBox9.TabIndex = 37;
            this.textBox9.TabStop = false;
            this.textBox9.Text = "伝票番号";
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
            // dgv_nyusyukkoidou
            // 
            this.dgv_nyusyukkoidou.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_nyusyukkoidou.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column1});
            this.dgv_nyusyukkoidou.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_nyusyukkoidou.Location = new System.Drawing.Point(0, 0);
            this.dgv_nyusyukkoidou.Name = "dgv_nyusyukkoidou";
            this.dgv_nyusyukkoidou.RowHeadersVisible = false;
            this.dgv_nyusyukkoidou.RowTemplate.Height = 21;
            this.dgv_nyusyukkoidou.Size = new System.Drawing.Size(880, 287);
            this.dgv_nyusyukkoidou.TabIndex = 3;
            this.dgv_nyusyukkoidou.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_nyusyukkoidou_CellDoubleClick);
            this.dgv_nyusyukkoidou.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_nyusyukkoidou_CellEndEdit);
            this.dgv_nyusyukkoidou.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_nyusyukkoidou_CellValidated);
            this.dgv_nyusyukkoidou.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgv_nyusyukkoidou_CellValidating);
            // 
            // Column2
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.PowderBlue;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column2.HeaderText = "部品コード";
            this.Column2.Name = "Column2";
            this.Column2.Width = 85;
            // 
            // Column3
            // 
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Column3.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column3.HeaderText = "部品名";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 250;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "在庫区分";
            this.Column4.Name = "Column4";
            this.Column4.Width = 80;
            // 
            // Column5
            // 
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.PowderBlue;
            this.Column5.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column5.HeaderText = "受注コード1";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "受注コード2";
            this.Column6.Name = "Column6";
            // 
            // Column7
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = null;
            this.Column7.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column7.HeaderText = "数量";
            this.Column7.Name = "Column7";
            this.Column7.Width = 70;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "備考";
            this.Column1.Name = "Column1";
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
            // ss_status
            // 
            this.ss_status.Location = new System.Drawing.Point(0, 540);
            this.ss_status.Name = "ss_status";
            this.ss_status.Size = new System.Drawing.Size(884, 22);
            this.ss_status.TabIndex = 2;
            this.ss_status.Text = "statusStrip1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(372, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(375, 12);
            this.label1.TabIndex = 42;
            this.label1.Text = "支給品、購入品にかかわらず、どの取引先用の在庫か？で判断し、登録する。";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(372, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(387, 12);
            this.label2.TabIndex = 43;
            this.label2.Text = "例：田上さんからの操作盤本体の場合、取引先コードはダイニチ（000051）となる。";
            // 
            // frm_buhin_nyusyukkoidou
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 562);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.ss_status);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "frm_buhin_nyusyukkoidou";
            this.Text = "部品入出庫";
            this.Load += new System.EventHandler(this.frm_buhin_nyusyukkoidou_Load);
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
            ((System.ComponentModel.ISupportInitialize)(this.dgv_nyusyukkoidou)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btn_hardcopy;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_touroku;
        private System.Windows.Forms.Button btn_syuuryou;
        private System.Windows.Forms.StatusStrip ss_status;
        private System.Windows.Forms.DataGridView dgv_nyusyukkoidou;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DateTimePicker dtp_buhin_syori_date;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox tb_torihikisaki_name;
        private System.Windows.Forms.TextBox tb_torihikisaki_cd;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox tb_seq;
        private System.Windows.Forms.TextBox tb_denpyou_no;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}