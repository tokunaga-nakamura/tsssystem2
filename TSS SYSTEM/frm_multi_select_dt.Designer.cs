﻿namespace TSS_SYSTEM
{
    partial class frm_multi_select_dt
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lbl_message = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dgv_sentaku = new System.Windows.Forms.DataGridView();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_sentaku = new System.Windows.Forms.Button();
            this.lbl_mode_message = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_sentaku)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lbl_mode_message);
            this.splitContainer1.Panel1.Controls.Add(this.lbl_message);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(284, 361);
            this.splitContainer1.SplitterDistance = 51;
            this.splitContainer1.TabIndex = 0;
            // 
            // lbl_message
            // 
            this.lbl_message.AutoSize = true;
            this.lbl_message.Location = new System.Drawing.Point(12, 9);
            this.lbl_message.Name = "lbl_message";
            this.lbl_message.Size = new System.Drawing.Size(35, 12);
            this.lbl_message.TabIndex = 0;
            this.lbl_message.Text = "label1";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dgv_sentaku);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.btn_cancel);
            this.splitContainer2.Panel2.Controls.Add(this.btn_sentaku);
            this.splitContainer2.Size = new System.Drawing.Size(284, 306);
            this.splitContainer2.SplitterDistance = 272;
            this.splitContainer2.TabIndex = 0;
            // 
            // dgv_sentaku
            // 
            this.dgv_sentaku.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_sentaku.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_sentaku.Location = new System.Drawing.Point(0, 0);
            this.dgv_sentaku.Name = "dgv_sentaku";
            this.dgv_sentaku.RowTemplate.Height = 21;
            this.dgv_sentaku.Size = new System.Drawing.Size(284, 272);
            this.dgv_sentaku.TabIndex = 0;
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(197, 3);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 1;
            this.btn_cancel.Text = "キャンセル";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_sentaku
            // 
            this.btn_sentaku.Location = new System.Drawing.Point(12, 3);
            this.btn_sentaku.Name = "btn_sentaku";
            this.btn_sentaku.Size = new System.Drawing.Size(75, 23);
            this.btn_sentaku.TabIndex = 0;
            this.btn_sentaku.Text = "選択";
            this.btn_sentaku.UseVisualStyleBackColor = true;
            this.btn_sentaku.Click += new System.EventHandler(this.btn_sentaku_Click);
            // 
            // lbl_mode_message
            // 
            this.lbl_mode_message.AutoSize = true;
            this.lbl_mode_message.Location = new System.Drawing.Point(12, 30);
            this.lbl_mode_message.Name = "lbl_mode_message";
            this.lbl_mode_message.Size = new System.Drawing.Size(35, 12);
            this.lbl_mode_message.TabIndex = 1;
            this.lbl_mode_message.Text = "label1";
            // 
            // frm_multi_select_dt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGreen;
            this.ClientSize = new System.Drawing.Size(284, 361);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainer1);
            this.Name = "frm_multi_select_dt";
            this.Text = "選択";
            this.Load += new System.EventHandler(this.frm_multi_select_dt_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_sentaku)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lbl_message;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dgv_sentaku;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_sentaku;
        private System.Windows.Forms.Label lbl_mode_message;
    }
}