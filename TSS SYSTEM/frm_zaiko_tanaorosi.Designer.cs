﻿namespace TSS_SYSTEM
{
    partial class frm_zaiko_tanaorosi
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
            this.btn_insatu = new System.Windows.Forms.Button();
            this.btn_touroku = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_kensaku = new System.Windows.Forms.Button();
            this.btn_csv = new System.Windows.Forms.Button();
            this.dgv_m = new System.Windows.Forms.DataGridView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.btn_syuryou = new System.Windows.Forms.Button();
            this.tb_buhin_cd2 = new System.Windows.Forms.TextBox();
            this.tb_buhin_cd1 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_m)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 539);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(884, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // btn_insatu
            // 
            this.btn_insatu.Location = new System.Drawing.Point(10, 3);
            this.btn_insatu.Name = "btn_insatu";
            this.btn_insatu.Size = new System.Drawing.Size(75, 23);
            this.btn_insatu.TabIndex = 15;
            this.btn_insatu.Text = "印刷";
            this.btn_insatu.UseVisualStyleBackColor = true;
            // 
            // btn_touroku
            // 
            this.btn_touroku.Location = new System.Drawing.Point(795, 16);
            this.btn_touroku.Name = "btn_touroku";
            this.btn_touroku.Size = new System.Drawing.Size(75, 23);
            this.btn_touroku.TabIndex = 19;
            this.btn_touroku.Text = "登録";
            this.btn_touroku.UseVisualStyleBackColor = true;
            this.btn_touroku.Click += new System.EventHandler(this.btn_touroku_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(193, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 18;
            this.label2.Text = "～";
            // 
            // btn_kensaku
            // 
            this.btn_kensaku.Location = new System.Drawing.Point(324, 16);
            this.btn_kensaku.Name = "btn_kensaku";
            this.btn_kensaku.Size = new System.Drawing.Size(75, 23);
            this.btn_kensaku.TabIndex = 14;
            this.btn_kensaku.Text = "検索";
            this.btn_kensaku.UseVisualStyleBackColor = true;
            this.btn_kensaku.Click += new System.EventHandler(this.btn_kensaku_Click);
            // 
            // btn_csv
            // 
            this.btn_csv.Location = new System.Drawing.Point(91, 3);
            this.btn_csv.Name = "btn_csv";
            this.btn_csv.Size = new System.Drawing.Size(75, 23);
            this.btn_csv.TabIndex = 16;
            this.btn_csv.Text = "CSV出力";
            this.btn_csv.UseVisualStyleBackColor = true;
            // 
            // dgv_m
            // 
            this.dgv_m.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_m.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_m.Location = new System.Drawing.Point(0, 0);
            this.dgv_m.Name = "dgv_m";
            this.dgv_m.RowTemplate.Height = 21;
            this.dgv_m.Size = new System.Drawing.Size(880, 446);
            this.dgv_m.TabIndex = 0;
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
            this.splitContainer2.Panel2.Controls.Add(this.btn_syuryou);
            this.splitContainer2.Size = new System.Drawing.Size(884, 486);
            this.splitContainer2.SplitterDistance = 450;
            this.splitContainer2.TabIndex = 0;
            this.splitContainer2.TabStop = false;
            // 
            // btn_syuryou
            // 
            this.btn_syuryou.Location = new System.Drawing.Point(795, 3);
            this.btn_syuryou.Name = "btn_syuryou";
            this.btn_syuryou.Size = new System.Drawing.Size(75, 23);
            this.btn_syuryou.TabIndex = 17;
            this.btn_syuryou.Text = "終了";
            this.btn_syuryou.UseVisualStyleBackColor = true;
            this.btn_syuryou.Click += new System.EventHandler(this.btn_syuryou_Click);
            // 
            // tb_buhin_cd2
            // 
            this.tb_buhin_cd2.Location = new System.Drawing.Point(210, 18);
            this.tb_buhin_cd2.MaxLength = 16;
            this.tb_buhin_cd2.Name = "tb_buhin_cd2";
            this.tb_buhin_cd2.Size = new System.Drawing.Size(108, 19);
            this.tb_buhin_cd2.TabIndex = 6;
            // 
            // tb_buhin_cd1
            // 
            this.tb_buhin_cd1.Location = new System.Drawing.Point(85, 18);
            this.tb_buhin_cd1.MaxLength = 16;
            this.tb_buhin_cd1.Name = "tb_buhin_cd1";
            this.tb_buhin_cd1.Size = new System.Drawing.Size(108, 19);
            this.tb_buhin_cd1.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btn_touroku);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btn_kensaku);
            this.groupBox1.Controls.Add(this.tb_buhin_cd2);
            this.groupBox1.Controls.Add(this.tb_buhin_cd1);
            this.groupBox1.Controls.Add(this.textBox4);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(880, 45);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "検索条件";
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox4.Location = new System.Drawing.Point(10, 18);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(75, 19);
            this.textBox4.TabIndex = 3;
            this.textBox4.TabStop = false;
            this.textBox4.Text = "部品コード";
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
            this.splitContainer1.Size = new System.Drawing.Size(884, 539);
            this.splitContainer1.SplitterDistance = 49;
            this.splitContainer1.TabIndex = 7;
            this.splitContainer1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(416, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(358, 12);
            this.label1.TabIndex = 20;
            this.label1.Text = "ロット在庫はそのままにし、フリー在庫で調整して、入力した在庫数にします。";
            // 
            // frm_zaiko_tanaorosi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.MaximumSize = new System.Drawing.Size(900, 600);
            this.Name = "frm_zaiko_tanaorosi";
            this.Text = "部品棚卸入力";
            this.Load += new System.EventHandler(this.frm_zaiko_tanaorisi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_m)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
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

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button btn_insatu;
        private System.Windows.Forms.Button btn_touroku;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_kensaku;
        private System.Windows.Forms.Button btn_csv;
        private System.Windows.Forms.DataGridView dgv_m;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button btn_syuryou;
        private System.Windows.Forms.TextBox tb_buhin_cd2;
        private System.Windows.Forms.TextBox tb_buhin_cd1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
    }
}