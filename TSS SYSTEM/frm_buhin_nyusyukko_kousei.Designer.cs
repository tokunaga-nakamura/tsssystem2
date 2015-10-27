namespace TSS_SYSTEM
{
    partial class frm_buhin_nyusyukko_kousei
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_buhin_nyusyukko_kousei));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_hardcopy = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_seihin_name = new System.Windows.Forms.TextBox();
            this.tb_seihin_kousei_name = new System.Windows.Forms.TextBox();
            this.rb_syukko = new System.Windows.Forms.RadioButton();
            this.rb_nyuuko = new System.Windows.Forms.RadioButton();
            this.tb_suuryo = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.tb_seihin_kousei_no = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.tb_seihin_cd = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dgv_m = new System.Windows.Forms.DataGridView();
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
            ((System.ComponentModel.ISupportInitialize)(this.dgv_m)).BeginInit();
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
            this.splitContainer1.SplitterDistance = 59;
            this.splitContainer1.TabIndex = 1;
            this.splitContainer1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(85, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(412, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "フリー在庫での入出庫を行います。　※（この画面でロット在庫の入出庫はできません。）";
            // 
            // btn_hardcopy
            // 
            this.btn_hardcopy.Image = ((System.Drawing.Image)(resources.GetObject("btn_hardcopy.Image")));
            this.btn_hardcopy.Location = new System.Drawing.Point(10, 10);
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
            this.splitContainer3.Panel1.Controls.Add(this.tb_seihin_name);
            this.splitContainer3.Panel1.Controls.Add(this.tb_seihin_kousei_name);
            this.splitContainer3.Panel1.Controls.Add(this.rb_syukko);
            this.splitContainer3.Panel1.Controls.Add(this.rb_nyuuko);
            this.splitContainer3.Panel1.Controls.Add(this.tb_suuryo);
            this.splitContainer3.Panel1.Controls.Add(this.textBox5);
            this.splitContainer3.Panel1.Controls.Add(this.tb_seihin_kousei_no);
            this.splitContainer3.Panel1.Controls.Add(this.textBox3);
            this.splitContainer3.Panel1.Controls.Add(this.tb_seihin_cd);
            this.splitContainer3.Panel1.Controls.Add(this.textBox1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.dgv_m);
            this.splitContainer3.Size = new System.Drawing.Size(884, 440);
            this.splitContainer3.SplitterDistance = 53;
            this.splitContainer3.TabIndex = 0;
            this.splitContainer3.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(308, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(387, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "←入出庫数は正数を入力してください。（出庫の場合も正数で入力してください。）";
            // 
            // tb_seihin_name
            // 
            this.tb_seihin_name.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_seihin_name.Location = new System.Drawing.Point(202, 3);
            this.tb_seihin_name.Name = "tb_seihin_name";
            this.tb_seihin_name.Size = new System.Drawing.Size(277, 19);
            this.tb_seihin_name.TabIndex = 9;
            // 
            // tb_seihin_kousei_name
            // 
            this.tb_seihin_kousei_name.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_seihin_kousei_name.Location = new System.Drawing.Point(591, 3);
            this.tb_seihin_kousei_name.Name = "tb_seihin_kousei_name";
            this.tb_seihin_kousei_name.Size = new System.Drawing.Size(279, 19);
            this.tb_seihin_kousei_name.TabIndex = 8;
            // 
            // rb_syukko
            // 
            this.rb_syukko.AutoSize = true;
            this.rb_syukko.Location = new System.Drawing.Point(63, 28);
            this.rb_syukko.Name = "rb_syukko";
            this.rb_syukko.Size = new System.Drawing.Size(47, 16);
            this.rb_syukko.TabIndex = 3;
            this.rb_syukko.Text = "出庫";
            this.rb_syukko.UseVisualStyleBackColor = true;
            // 
            // rb_nyuuko
            // 
            this.rb_nyuuko.AutoSize = true;
            this.rb_nyuuko.Checked = true;
            this.rb_nyuuko.Location = new System.Drawing.Point(10, 28);
            this.rb_nyuuko.Name = "rb_nyuuko";
            this.rb_nyuuko.Size = new System.Drawing.Size(47, 16);
            this.rb_nyuuko.TabIndex = 2;
            this.rb_nyuuko.TabStop = true;
            this.rb_nyuuko.Text = "入庫";
            this.rb_nyuuko.UseVisualStyleBackColor = true;
            // 
            // tb_suuryo
            // 
            this.tb_suuryo.Location = new System.Drawing.Point(202, 27);
            this.tb_suuryo.Name = "tb_suuryo";
            this.tb_suuryo.Size = new System.Drawing.Size(100, 19);
            this.tb_suuryo.TabIndex = 4;
            this.tb_suuryo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tb_suuryo.Validating += new System.ComponentModel.CancelEventHandler(this.tb_suuryo_Validating);
            this.tb_suuryo.Validated += new System.EventHandler(this.tb_suuryo_Validated);
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox5.Location = new System.Drawing.Point(116, 27);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(86, 19);
            this.textBox5.TabIndex = 4;
            this.textBox5.TabStop = false;
            this.textBox5.Text = "入出庫数";
            // 
            // tb_seihin_kousei_no
            // 
            this.tb_seihin_kousei_no.BackColor = System.Drawing.Color.PowderBlue;
            this.tb_seihin_kousei_no.Location = new System.Drawing.Point(571, 3);
            this.tb_seihin_kousei_no.Name = "tb_seihin_kousei_no";
            this.tb_seihin_kousei_no.Size = new System.Drawing.Size(20, 19);
            this.tb_seihin_kousei_no.TabIndex = 1;
            this.tb_seihin_kousei_no.Validating += new System.ComponentModel.CancelEventHandler(this.tb_seihin_kousei_no_Validating);
            this.tb_seihin_kousei_no.Validated += new System.EventHandler(this.tb_seihin_kousei_no_Validated);
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox3.Location = new System.Drawing.Point(485, 3);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(86, 19);
            this.textBox3.TabIndex = 2;
            this.textBox3.TabStop = false;
            this.textBox3.Text = "製品構成番号";
            // 
            // tb_seihin_cd
            // 
            this.tb_seihin_cd.BackColor = System.Drawing.Color.PowderBlue;
            this.tb_seihin_cd.Location = new System.Drawing.Point(96, 3);
            this.tb_seihin_cd.Name = "tb_seihin_cd";
            this.tb_seihin_cd.Size = new System.Drawing.Size(106, 19);
            this.tb_seihin_cd.TabIndex = 0;
            this.tb_seihin_cd.DoubleClick += new System.EventHandler(this.tb_seihin_cd_DoubleClick);
            this.tb_seihin_cd.Validating += new System.ComponentModel.CancelEventHandler(this.tb_seihin_cd_Validating);
            this.tb_seihin_cd.Validated += new System.EventHandler(this.tb_seihin_cd_Validated);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox1.Location = new System.Drawing.Point(10, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(86, 19);
            this.textBox1.TabIndex = 0;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "製品コード";
            // 
            // dgv_m
            // 
            this.dgv_m.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_m.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_m.Location = new System.Drawing.Point(0, 0);
            this.dgv_m.Name = "dgv_m";
            this.dgv_m.RowTemplate.Height = 21;
            this.dgv_m.Size = new System.Drawing.Size(880, 379);
            this.dgv_m.TabIndex = 0;
            this.dgv_m.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_m_CellValidated);
            this.dgv_m.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgv_m_CellValidating);
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
            // btn_syuuryou
            // 
            this.btn_syuuryou.Location = new System.Drawing.Point(795, 3);
            this.btn_syuuryou.Name = "btn_syuuryou";
            this.btn_syuuryou.Size = new System.Drawing.Size(75, 23);
            this.btn_syuuryou.TabIndex = 1;
            this.btn_syuuryou.Text = "終了";
            this.btn_syuuryou.UseVisualStyleBackColor = true;
            this.btn_syuuryou.Click += new System.EventHandler(this.btn_syuuryou_Click);
            // 
            // frm_buhin_nyusyukko_kousei
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "frm_buhin_nyusyukko_kousei";
            this.Text = "部品入出庫（製品構成を使用した一括処理）";
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
            ((System.ComponentModel.ISupportInitialize)(this.dgv_m)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btn_hardcopy;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.TextBox tb_seihin_name;
        private System.Windows.Forms.TextBox tb_seihin_kousei_name;
        private System.Windows.Forms.RadioButton rb_syukko;
        private System.Windows.Forms.RadioButton rb_nyuuko;
        private System.Windows.Forms.TextBox tb_suuryo;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox tb_seihin_kousei_no;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox tb_seihin_cd;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView dgv_m;
        private System.Windows.Forms.Button btn_touroku;
        private System.Windows.Forms.Button btn_syuuryou;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}