namespace TSS_SYSTEM
{
    partial class frm_bcr
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
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.lbl_msg1 = new System.Windows.Forms.Label();
            this.btn_syuuryou = new System.Windows.Forms.Button();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.lbl_msg2 = new System.Windows.Forms.Label();
            this.lbl_msg3 = new System.Windows.Forms.Label();
            this.lbl_msg4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.lbl_msg1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(456, 140);
            this.splitContainer1.SplitterDistance = 28;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.TabStop = false;
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
            this.splitContainer2.Panel1.Controls.Add(this.lbl_msg4);
            this.splitContainer2.Panel1.Controls.Add(this.lbl_msg3);
            this.splitContainer2.Panel1.Controls.Add(this.lbl_msg2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.btn_syuuryou);
            this.splitContainer2.Size = new System.Drawing.Size(456, 108);
            this.splitContainer2.SplitterDistance = 72;
            this.splitContainer2.TabIndex = 0;
            this.splitContainer2.TabStop = false;
            // 
            // lbl_msg1
            // 
            this.lbl_msg1.AutoSize = true;
            this.lbl_msg1.Location = new System.Drawing.Point(10, 7);
            this.lbl_msg1.Name = "lbl_msg1";
            this.lbl_msg1.Size = new System.Drawing.Size(35, 12);
            this.lbl_msg1.TabIndex = 0;
            this.lbl_msg1.Text = "label1";
            // 
            // btn_syuuryou
            // 
            this.btn_syuuryou.Location = new System.Drawing.Point(374, 3);
            this.btn_syuuryou.Name = "btn_syuuryou";
            this.btn_syuuryou.Size = new System.Drawing.Size(75, 23);
            this.btn_syuuryou.TabIndex = 0;
            this.btn_syuuryou.Text = "終了";
            this.btn_syuuryou.UseVisualStyleBackColor = true;
            this.btn_syuuryou.Click += new System.EventHandler(this.btn_syuuryou_Click);
            // 
            // lbl_msg2
            // 
            this.lbl_msg2.AutoSize = true;
            this.lbl_msg2.Location = new System.Drawing.Point(10, 9);
            this.lbl_msg2.Name = "lbl_msg2";
            this.lbl_msg2.Size = new System.Drawing.Size(35, 12);
            this.lbl_msg2.TabIndex = 0;
            this.lbl_msg2.Text = "label1";
            // 
            // lbl_msg3
            // 
            this.lbl_msg3.AutoSize = true;
            this.lbl_msg3.Location = new System.Drawing.Point(10, 30);
            this.lbl_msg3.Name = "lbl_msg3";
            this.lbl_msg3.Size = new System.Drawing.Size(35, 12);
            this.lbl_msg3.TabIndex = 1;
            this.lbl_msg3.Text = "label2";
            // 
            // lbl_msg4
            // 
            this.lbl_msg4.AutoSize = true;
            this.lbl_msg4.Location = new System.Drawing.Point(10, 51);
            this.lbl_msg4.Name = "lbl_msg4";
            this.lbl_msg4.Size = new System.Drawing.Size(35, 12);
            this.lbl_msg4.TabIndex = 2;
            this.lbl_msg4.Text = "label3";
            // 
            // frm_bcr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MistyRose;
            this.ClientSize = new System.Drawing.Size(456, 140);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frm_bcr";
            this.Text = "frm_bcr";
            this.Load += new System.EventHandler(this.frm_bcr_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button btn_syuuryou;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Label lbl_msg1;
        private System.Windows.Forms.Label lbl_msg4;
        private System.Windows.Forms.Label lbl_msg3;
        private System.Windows.Forms.Label lbl_msg2;
    }
}