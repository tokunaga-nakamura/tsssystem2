namespace TSS_SYSTEM
{
    partial class frm_system_administrator
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
            this.btn_syuuryou = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_syuuryou
            // 
            this.btn_syuuryou.Location = new System.Drawing.Point(797, 526);
            this.btn_syuuryou.Name = "btn_syuuryou";
            this.btn_syuuryou.Size = new System.Drawing.Size(75, 23);
            this.btn_syuuryou.TabIndex = 0;
            this.btn_syuuryou.Text = "終了";
            this.btn_syuuryou.UseVisualStyleBackColor = true;
            this.btn_syuuryou.Click += new System.EventHandler(this.btn_syuuryou_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(317, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "製品マスタの納品スケジュール区分に適当な値を入れる";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // frm_system_administrator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_syuuryou);
            this.Name = "frm_system_administrator";
            this.Text = "frm_system_administrator";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_syuuryou;
        private System.Windows.Forms.Button button1;
    }
}