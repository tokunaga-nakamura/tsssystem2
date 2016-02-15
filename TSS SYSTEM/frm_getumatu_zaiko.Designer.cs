namespace TSS_SYSTEM
{
    partial class frm_getumatu_zaiko
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_touroku = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cb_year = new System.Windows.Forms.ComboBox();
            this.cb_month = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(110, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "年";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(195, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "月";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(153, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "月末在庫登録する年月を入力";
            // 
            // btn_touroku
            // 
            this.btn_touroku.Location = new System.Drawing.Point(92, 171);
            this.btn_touroku.Name = "btn_touroku";
            this.btn_touroku.Size = new System.Drawing.Size(75, 23);
            this.btn_touroku.TabIndex = 5;
            this.btn_touroku.Text = "登録";
            this.btn_touroku.UseVisualStyleBackColor = true;
            this.btn_touroku.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(223, 24);
            this.label4.TabIndex = 6;
            this.label4.Text = "現時点の在庫数量が、上記年月の月末在庫\r\nとして登録されます";
            // 
            // cb_year
            // 
            this.cb_year.FormattingEnabled = true;
            this.cb_year.Items.AddRange(new object[] {
            "2015",
            "2016",
            "2017",
            "2018"});
            this.cb_year.Location = new System.Drawing.Point(25, 66);
            this.cb_year.Name = "cb_year";
            this.cb_year.Size = new System.Drawing.Size(79, 20);
            this.cb_year.TabIndex = 7;
            // 
            // cb_month
            // 
            this.cb_month.FormattingEnabled = true;
            this.cb_month.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12"});
            this.cb_month.Location = new System.Drawing.Point(150, 66);
            this.cb_month.Name = "cb_month";
            this.cb_month.Size = new System.Drawing.Size(39, 20);
            this.cb_month.TabIndex = 8;
            // 
            // frm_getumatu_zaiko
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 238);
            this.Controls.Add(this.cb_month);
            this.Controls.Add(this.cb_year);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_touroku);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frm_getumatu_zaiko";
            this.Text = "月末在庫登録処理";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_touroku;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cb_year;
        private System.Windows.Forms.ComboBox cb_month;
    }
}