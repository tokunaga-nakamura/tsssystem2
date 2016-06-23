namespace TSS_SYSTEM
{
    partial class frm_utl_tss_nouhin_m_to_tss_nouhin_schedule_m
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
            this.label1 = new System.Windows.Forms.Label();
            this.btn_kaisi = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_err1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_false = new System.Windows.Forms.Label();
            this.lbl_true = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_db = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(409, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "pdb2aの環境で、tss_nouhin_m から tss_nouhin_schedule_m へデータコンバートします";
            // 
            // btn_kaisi
            // 
            this.btn_kaisi.Location = new System.Drawing.Point(170, 117);
            this.btn_kaisi.Name = "btn_kaisi";
            this.btn_kaisi.Size = new System.Drawing.Size(75, 23);
            this.btn_kaisi.TabIndex = 1;
            this.btn_kaisi.Text = "開始";
            this.btn_kaisi.UseVisualStyleBackColor = true;
            this.btn_kaisi.Click += new System.EventHandler(this.btn_kaisi_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 174);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(188, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "nouhin_yotei_dateのTryPerseエラー数";
            // 
            // lbl_err1
            // 
            this.lbl_err1.AutoSize = true;
            this.lbl_err1.Location = new System.Drawing.Point(206, 174);
            this.lbl_err1.Name = "lbl_err1";
            this.lbl_err1.Size = new System.Drawing.Size(11, 12);
            this.lbl_err1.TabIndex = 3;
            this.lbl_err1.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 198);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "insert true";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 220);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "insert false";
            // 
            // lbl_false
            // 
            this.lbl_false.AutoSize = true;
            this.lbl_false.Location = new System.Drawing.Point(81, 220);
            this.lbl_false.Name = "lbl_false";
            this.lbl_false.Size = new System.Drawing.Size(11, 12);
            this.lbl_false.TabIndex = 6;
            this.lbl_false.Text = "0";
            // 
            // lbl_true
            // 
            this.lbl_true.AutoSize = true;
            this.lbl_true.Location = new System.Drawing.Point(81, 198);
            this.lbl_true.Name = "lbl_true";
            this.lbl_true.Size = new System.Drawing.Size(11, 12);
            this.lbl_true.TabIndex = 7;
            this.lbl_true.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "接続DB:";
            // 
            // lbl_db
            // 
            this.lbl_db.AutoSize = true;
            this.lbl_db.Location = new System.Drawing.Point(65, 9);
            this.lbl_db.Name = "lbl_db";
            this.lbl_db.Size = new System.Drawing.Size(41, 12);
            this.lbl_db.TabIndex = 9;
            this.lbl_db.Text = "******";
            // 
            // frm_utl_tss_nouhin_m_to_tss_nouhin_schedule_m
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 250);
            this.Controls.Add(this.lbl_db);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lbl_true);
            this.Controls.Add(this.lbl_false);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbl_err1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_kaisi);
            this.Controls.Add(this.label1);
            this.Name = "frm_utl_tss_nouhin_m_to_tss_nouhin_schedule_m";
            this.Text = "納品スケジュールマスタのコンバート";
            this.Load += new System.EventHandler(this.frm_utl_tss_nouhin_m_to_tss_nouhin_schedule_m_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_kaisi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_err1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbl_false;
        private System.Windows.Forms.Label lbl_true;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbl_db;
    }
}