﻿namespace TSS_SYSTEM
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
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.btn_control_m = new System.Windows.Forms.Button();
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
            this.button1.Location = new System.Drawing.Point(586, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(281, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "テーブルメンテナンス（管理者用）";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(281, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "ロット在庫→フリー在庫";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 41);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(281, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "フリー在庫レコード作成";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(12, 70);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(281, 52);
            this.button4.TabIndex = 4;
            this.button4.Text = "tss_nouhin_m → tss_nouhin_schedule_mコンバート時に1回だけ実行する";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // btn_control_m
            // 
            this.btn_control_m.Location = new System.Drawing.Point(299, 99);
            this.btn_control_m.Name = "btn_control_m";
            this.btn_control_m.Size = new System.Drawing.Size(281, 23);
            this.btn_control_m.TabIndex = 5;
            this.btn_control_m.Text = "コントロールマスタ";
            this.btn_control_m.UseVisualStyleBackColor = true;
            this.btn_control_m.Click += new System.EventHandler(this.btn_control_m_Click);
            // 
            // frm_system_administrator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Purple;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.btn_control_m);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_syuuryou);
            this.Name = "frm_system_administrator";
            this.Text = "システム管理者用メニュー（絶対に実行しないでください！！！）";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_syuuryou;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button btn_control_m;
    }
}