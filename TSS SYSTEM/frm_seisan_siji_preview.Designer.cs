﻿namespace TSS_SYSTEM
{
    partial class frm_seisan_siji_preview
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_seisan_siji_preview));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btn_hardcopy = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.btn_preview = new System.Windows.Forms.Button();
            this.tb_line_name = new System.Windows.Forms.TextBox();
            this.tb_koutei_name = new System.Windows.Forms.TextBox();
            this.tb_busyo_name = new System.Windows.Forms.TextBox();
            this.tb_line_cd = new System.Windows.Forms.TextBox();
            this.tb_koutei_cd = new System.Windows.Forms.TextBox();
            this.tb_busyo_cd = new System.Windows.Forms.TextBox();
            this.tb_seisanbi = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.viewer1 = new GrapeCity.ActiveReports.Viewer.Win.Viewer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.btn_insatu = new System.Windows.Forms.Button();
            this.btn_syuuryou = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
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
            this.splitContainer1.SplitterDistance = 62;
            this.splitContainer1.TabIndex = 0;
            // 
            // btn_hardcopy
            // 
            this.btn_hardcopy.Image = ((System.Drawing.Image)(resources.GetObject("btn_hardcopy.Image")));
            this.btn_hardcopy.Location = new System.Drawing.Point(10, 10);
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
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer4);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(884, 495);
            this.splitContainer2.SplitterDistance = 457;
            this.splitContainer2.TabIndex = 0;
            // 
            // splitContainer4
            // 
            this.splitContainer4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer4.IsSplitterFixed = true;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.btn_preview);
            this.splitContainer4.Panel1.Controls.Add(this.tb_line_name);
            this.splitContainer4.Panel1.Controls.Add(this.tb_koutei_name);
            this.splitContainer4.Panel1.Controls.Add(this.tb_busyo_name);
            this.splitContainer4.Panel1.Controls.Add(this.tb_line_cd);
            this.splitContainer4.Panel1.Controls.Add(this.tb_koutei_cd);
            this.splitContainer4.Panel1.Controls.Add(this.tb_busyo_cd);
            this.splitContainer4.Panel1.Controls.Add(this.tb_seisanbi);
            this.splitContainer4.Panel1.Controls.Add(this.textBox4);
            this.splitContainer4.Panel1.Controls.Add(this.textBox3);
            this.splitContainer4.Panel1.Controls.Add(this.textBox2);
            this.splitContainer4.Panel1.Controls.Add(this.textBox1);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.viewer1);
            this.splitContainer4.Size = new System.Drawing.Size(884, 457);
            this.splitContainer4.SplitterDistance = 30;
            this.splitContainer4.TabIndex = 0;
            // 
            // btn_preview
            // 
            this.btn_preview.Location = new System.Drawing.Point(692, 3);
            this.btn_preview.Name = "btn_preview";
            this.btn_preview.Size = new System.Drawing.Size(75, 23);
            this.btn_preview.TabIndex = 11;
            this.btn_preview.Text = "プレビュー";
            this.btn_preview.UseVisualStyleBackColor = true;
            // 
            // tb_line_name
            // 
            this.tb_line_name.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_line_name.Location = new System.Drawing.Point(586, 5);
            this.tb_line_name.Name = "tb_line_name";
            this.tb_line_name.ReadOnly = true;
            this.tb_line_name.Size = new System.Drawing.Size(100, 19);
            this.tb_line_name.TabIndex = 10;
            this.tb_line_name.TabStop = false;
            // 
            // tb_koutei_name
            // 
            this.tb_koutei_name.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_koutei_name.Location = new System.Drawing.Point(405, 5);
            this.tb_koutei_name.Name = "tb_koutei_name";
            this.tb_koutei_name.ReadOnly = true;
            this.tb_koutei_name.Size = new System.Drawing.Size(100, 19);
            this.tb_koutei_name.TabIndex = 9;
            this.tb_koutei_name.TabStop = false;
            // 
            // tb_busyo_name
            // 
            this.tb_busyo_name.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_busyo_name.Location = new System.Drawing.Point(224, 5);
            this.tb_busyo_name.Name = "tb_busyo_name";
            this.tb_busyo_name.ReadOnly = true;
            this.tb_busyo_name.Size = new System.Drawing.Size(100, 19);
            this.tb_busyo_name.TabIndex = 8;
            this.tb_busyo_name.TabStop = false;
            // 
            // tb_line_cd
            // 
            this.tb_line_cd.Location = new System.Drawing.Point(559, 5);
            this.tb_line_cd.MaxLength = 3;
            this.tb_line_cd.Name = "tb_line_cd";
            this.tb_line_cd.Size = new System.Drawing.Size(27, 19);
            this.tb_line_cd.TabIndex = 7;
            // 
            // tb_koutei_cd
            // 
            this.tb_koutei_cd.Location = new System.Drawing.Point(378, 5);
            this.tb_koutei_cd.MaxLength = 3;
            this.tb_koutei_cd.Name = "tb_koutei_cd";
            this.tb_koutei_cd.Size = new System.Drawing.Size(27, 19);
            this.tb_koutei_cd.TabIndex = 6;
            // 
            // tb_busyo_cd
            // 
            this.tb_busyo_cd.Location = new System.Drawing.Point(187, 5);
            this.tb_busyo_cd.MaxLength = 4;
            this.tb_busyo_cd.Name = "tb_busyo_cd";
            this.tb_busyo_cd.Size = new System.Drawing.Size(37, 19);
            this.tb_busyo_cd.TabIndex = 5;
            // 
            // tb_seisanbi
            // 
            this.tb_seisanbi.Location = new System.Drawing.Point(58, 5);
            this.tb_seisanbi.MaxLength = 10;
            this.tb_seisanbi.Name = "tb_seisanbi";
            this.tb_seisanbi.Size = new System.Drawing.Size(75, 19);
            this.tb_seisanbi.TabIndex = 4;
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox4.Location = new System.Drawing.Point(511, 5);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(48, 19);
            this.textBox4.TabIndex = 3;
            this.textBox4.TabStop = false;
            this.textBox4.Text = "ライン";
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox3.Location = new System.Drawing.Point(330, 5);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(48, 19);
            this.textBox3.TabIndex = 2;
            this.textBox3.TabStop = false;
            this.textBox3.Text = "工程";
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox2.Location = new System.Drawing.Point(139, 5);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(48, 19);
            this.textBox2.TabIndex = 1;
            this.textBox2.TabStop = false;
            this.textBox2.Text = "部署";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox1.Location = new System.Drawing.Point(10, 5);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(48, 19);
            this.textBox1.TabIndex = 0;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "作業日";
            // 
            // viewer1
            // 
            this.viewer1.CurrentPage = 0;
            this.viewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewer1.Location = new System.Drawing.Point(0, 0);
            this.viewer1.Name = "viewer1";
            this.viewer1.PreviewPages = 0;
            // 
            // 
            // 
            // 
            // 
            // 
            this.viewer1.Sidebar.ParametersPanel.ContextMenu = null;
            this.viewer1.Sidebar.ParametersPanel.Text = "パラメータ";
            this.viewer1.Sidebar.ParametersPanel.Width = 200;
            // 
            // 
            // 
            this.viewer1.Sidebar.SearchPanel.ContextMenu = null;
            this.viewer1.Sidebar.SearchPanel.Text = "検索";
            this.viewer1.Sidebar.SearchPanel.Width = 200;
            // 
            // 
            // 
            this.viewer1.Sidebar.ThumbnailsPanel.ContextMenu = null;
            this.viewer1.Sidebar.ThumbnailsPanel.Text = "サムネイル";
            this.viewer1.Sidebar.ThumbnailsPanel.Width = 200;
            // 
            // 
            // 
            this.viewer1.Sidebar.TocPanel.ContextMenu = null;
            this.viewer1.Sidebar.TocPanel.Text = "見出しマップラベル";
            this.viewer1.Sidebar.TocPanel.Width = 200;
            this.viewer1.Sidebar.Width = 200;
            this.viewer1.Size = new System.Drawing.Size(880, 419);
            this.viewer1.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer3.IsSplitterFixed = true;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.btn_insatu);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.btn_syuuryou);
            this.splitContainer3.Size = new System.Drawing.Size(880, 30);
            this.splitContainer3.SplitterDistance = 788;
            this.splitContainer3.TabIndex = 0;
            // 
            // btn_insatu
            // 
            this.btn_insatu.Location = new System.Drawing.Point(10, 3);
            this.btn_insatu.Name = "btn_insatu";
            this.btn_insatu.Size = new System.Drawing.Size(75, 23);
            this.btn_insatu.TabIndex = 0;
            this.btn_insatu.Text = "印刷";
            this.btn_insatu.UseVisualStyleBackColor = true;
            // 
            // btn_syuuryou
            // 
            this.btn_syuuryou.Location = new System.Drawing.Point(3, 3);
            this.btn_syuuryou.Name = "btn_syuuryou";
            this.btn_syuuryou.Size = new System.Drawing.Size(75, 23);
            this.btn_syuuryou.TabIndex = 0;
            this.btn_syuuryou.Text = "終了";
            this.btn_syuuryou.UseVisualStyleBackColor = true;
            this.btn_syuuryou.Click += new System.EventHandler(this.btn_syuuryou_Click);
            // 
            // frm_seisan_siji_preview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.splitContainer1);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "frm_seisan_siji_preview";
            this.Text = "生産指示書発行";
            this.Load += new System.EventHandler(this.frm_seisan_siji_preview_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel1.PerformLayout();
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btn_hardcopy;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Button btn_insatu;
        private System.Windows.Forms.Button btn_syuuryou;
        private System.Windows.Forms.TextBox tb_line_name;
        private System.Windows.Forms.TextBox tb_koutei_name;
        private System.Windows.Forms.TextBox tb_busyo_name;
        private System.Windows.Forms.TextBox tb_line_cd;
        private System.Windows.Forms.TextBox tb_koutei_cd;
        private System.Windows.Forms.TextBox tb_busyo_cd;
        private System.Windows.Forms.TextBox tb_seisanbi;
        private System.Windows.Forms.Button btn_preview;
        private GrapeCity.ActiveReports.Viewer.Win.Viewer viewer1;
    }
}