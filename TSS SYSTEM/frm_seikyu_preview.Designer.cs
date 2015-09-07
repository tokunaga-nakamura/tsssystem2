namespace TSS_SYSTEM
{
    partial class frm_seikyu_preview
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_seikyu_preview));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btn_hardcopy = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.rb_seikyu_no = new System.Windows.Forms.RadioButton();
            this.rb_torihikisaki_cd = new System.Windows.Forms.RadioButton();
            this.btn_preview = new System.Windows.Forms.Button();
            this.tb_torihikisaki_midasi = new System.Windows.Forms.TextBox();
            this.tb_simebi = new System.Windows.Forms.TextBox();
            this.tb_urikake_no_midasi = new System.Windows.Forms.TextBox();
            this.tb_torihikisaki_cd1 = new System.Windows.Forms.TextBox();
            this.tb_urikake_no = new System.Windows.Forms.TextBox();
            this.tb_simebi_midasi = new System.Windows.Forms.TextBox();
            this.tb_torihikisaki_cd2 = new System.Windows.Forms.TextBox();
            this.viewer1 = new GrapeCity.ActiveReports.Viewer.Win.Viewer();
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
            this.splitContainer2.Panel2.Controls.Add(this.btn_syuuryou);
            this.splitContainer2.Size = new System.Drawing.Size(884, 476);
            this.splitContainer2.SplitterDistance = 439;
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
            this.splitContainer3.Panel1.Controls.Add(this.label1);
            this.splitContainer3.Panel1.Controls.Add(this.rb_seikyu_no);
            this.splitContainer3.Panel1.Controls.Add(this.rb_torihikisaki_cd);
            this.splitContainer3.Panel1.Controls.Add(this.btn_preview);
            this.splitContainer3.Panel1.Controls.Add(this.tb_torihikisaki_midasi);
            this.splitContainer3.Panel1.Controls.Add(this.tb_simebi);
            this.splitContainer3.Panel1.Controls.Add(this.tb_urikake_no_midasi);
            this.splitContainer3.Panel1.Controls.Add(this.tb_torihikisaki_cd1);
            this.splitContainer3.Panel1.Controls.Add(this.tb_urikake_no);
            this.splitContainer3.Panel1.Controls.Add(this.tb_simebi_midasi);
            this.splitContainer3.Panel1.Controls.Add(this.tb_torihikisaki_cd2);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.viewer1);
            this.splitContainer3.Size = new System.Drawing.Size(884, 439);
            this.splitContainer3.SplitterDistance = 79;
            this.splitContainer3.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(231, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "～";
            // 
            // rb_seikyu_no
            // 
            this.rb_seikyu_no.AutoSize = true;
            this.rb_seikyu_no.Checked = true;
            this.rb_seikyu_no.Location = new System.Drawing.Point(10, 4);
            this.rb_seikyu_no.Name = "rb_seikyu_no";
            this.rb_seikyu_no.Size = new System.Drawing.Size(83, 16);
            this.rb_seikyu_no.TabIndex = 2;
            this.rb_seikyu_no.TabStop = true;
            this.rb_seikyu_no.Text = "請求書番号";
            this.rb_seikyu_no.UseVisualStyleBackColor = true;
            this.rb_seikyu_no.CheckedChanged += new System.EventHandler(this.rb_seikyu_no_CheckedChanged);
            // 
            // rb_torihikisaki_cd
            // 
            this.rb_torihikisaki_cd.AutoSize = true;
            this.rb_torihikisaki_cd.Location = new System.Drawing.Point(10, 30);
            this.rb_torihikisaki_cd.Name = "rb_torihikisaki_cd";
            this.rb_torihikisaki_cd.Size = new System.Drawing.Size(95, 16);
            this.rb_torihikisaki_cd.TabIndex = 3;
            this.rb_torihikisaki_cd.Text = "取引先＋締日";
            this.rb_torihikisaki_cd.UseVisualStyleBackColor = true;
            // 
            // btn_preview
            // 
            this.btn_preview.Location = new System.Drawing.Point(795, 49);
            this.btn_preview.Name = "btn_preview";
            this.btn_preview.Size = new System.Drawing.Size(75, 23);
            this.btn_preview.TabIndex = 4;
            this.btn_preview.Text = "プレビュー";
            this.btn_preview.UseVisualStyleBackColor = true;
            this.btn_preview.Click += new System.EventHandler(this.btn_preview_Click);
            // 
            // tb_torihikisaki_midasi
            // 
            this.tb_torihikisaki_midasi.BackColor = System.Drawing.Color.NavajoWhite;
            this.tb_torihikisaki_midasi.Enabled = false;
            this.tb_torihikisaki_midasi.Location = new System.Drawing.Point(111, 28);
            this.tb_torihikisaki_midasi.Name = "tb_torihikisaki_midasi";
            this.tb_torihikisaki_midasi.ReadOnly = true;
            this.tb_torihikisaki_midasi.Size = new System.Drawing.Size(75, 19);
            this.tb_torihikisaki_midasi.TabIndex = 4;
            this.tb_torihikisaki_midasi.TabStop = false;
            this.tb_torihikisaki_midasi.Text = "取引先コード";
            // 
            // tb_simebi
            // 
            this.tb_simebi.Enabled = false;
            this.tb_simebi.Location = new System.Drawing.Point(186, 53);
            this.tb_simebi.MaxLength = 10;
            this.tb_simebi.Name = "tb_simebi";
            this.tb_simebi.Size = new System.Drawing.Size(70, 19);
            this.tb_simebi.TabIndex = 3;
            this.tb_simebi.Validating += new System.ComponentModel.CancelEventHandler(this.tb_simebi_Validating);
            this.tb_simebi.Validated += new System.EventHandler(this.tb_simebi_Validated);
            // 
            // tb_urikake_no_midasi
            // 
            this.tb_urikake_no_midasi.BackColor = System.Drawing.Color.NavajoWhite;
            this.tb_urikake_no_midasi.Location = new System.Drawing.Point(111, 3);
            this.tb_urikake_no_midasi.Name = "tb_urikake_no_midasi";
            this.tb_urikake_no_midasi.ReadOnly = true;
            this.tb_urikake_no_midasi.Size = new System.Drawing.Size(75, 19);
            this.tb_urikake_no_midasi.TabIndex = 0;
            this.tb_urikake_no_midasi.TabStop = false;
            this.tb_urikake_no_midasi.Text = "請求書番号";
            // 
            // tb_torihikisaki_cd1
            // 
            this.tb_torihikisaki_cd1.Enabled = false;
            this.tb_torihikisaki_cd1.Location = new System.Drawing.Point(186, 28);
            this.tb_torihikisaki_cd1.MaxLength = 6;
            this.tb_torihikisaki_cd1.Name = "tb_torihikisaki_cd1";
            this.tb_torihikisaki_cd1.Size = new System.Drawing.Size(45, 19);
            this.tb_torihikisaki_cd1.TabIndex = 1;
            this.tb_torihikisaki_cd1.Validating += new System.ComponentModel.CancelEventHandler(this.tb_torihikisaki_cd1_Validating);
            // 
            // tb_urikake_no
            // 
            this.tb_urikake_no.Location = new System.Drawing.Point(186, 3);
            this.tb_urikake_no.MaxLength = 10;
            this.tb_urikake_no.Name = "tb_urikake_no";
            this.tb_urikake_no.Size = new System.Drawing.Size(70, 19);
            this.tb_urikake_no.TabIndex = 0;
            this.tb_urikake_no.Validating += new System.ComponentModel.CancelEventHandler(this.tb_urikake_no_Validating);
            // 
            // tb_simebi_midasi
            // 
            this.tb_simebi_midasi.BackColor = System.Drawing.Color.NavajoWhite;
            this.tb_simebi_midasi.Enabled = false;
            this.tb_simebi_midasi.Location = new System.Drawing.Point(111, 53);
            this.tb_simebi_midasi.Name = "tb_simebi_midasi";
            this.tb_simebi_midasi.ReadOnly = true;
            this.tb_simebi_midasi.Size = new System.Drawing.Size(75, 19);
            this.tb_simebi_midasi.TabIndex = 7;
            this.tb_simebi_midasi.TabStop = false;
            this.tb_simebi_midasi.Text = "請求締日";
            // 
            // tb_torihikisaki_cd2
            // 
            this.tb_torihikisaki_cd2.Enabled = false;
            this.tb_torihikisaki_cd2.Location = new System.Drawing.Point(248, 28);
            this.tb_torihikisaki_cd2.MaxLength = 6;
            this.tb_torihikisaki_cd2.Name = "tb_torihikisaki_cd2";
            this.tb_torihikisaki_cd2.Size = new System.Drawing.Size(45, 19);
            this.tb_torihikisaki_cd2.TabIndex = 2;
            this.tb_torihikisaki_cd2.Validating += new System.ComponentModel.CancelEventHandler(this.tb_torihikisaki_cd2_Validating);
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
            this.viewer1.Size = new System.Drawing.Size(880, 352);
            this.viewer1.TabIndex = 0;
            // 
            // btn_syuuryou
            // 
            this.btn_syuuryou.Location = new System.Drawing.Point(795, 3);
            this.btn_syuuryou.Name = "btn_syuuryou";
            this.btn_syuuryou.Size = new System.Drawing.Size(75, 23);
            this.btn_syuuryou.TabIndex = 0;
            this.btn_syuuryou.Text = "終了";
            this.btn_syuuryou.UseVisualStyleBackColor = true;
            this.btn_syuuryou.Click += new System.EventHandler(this.btn_syuuryou_Click);
            // 
            // frm_seikyu_preview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "frm_seikyu_preview";
            this.Text = "請求書印刷";
            this.Load += new System.EventHandler(this.frm_seikyu_preview_Load);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btn_hardcopy;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.TextBox tb_torihikisaki_midasi;
        private System.Windows.Forms.RadioButton rb_torihikisaki_cd;
        private System.Windows.Forms.RadioButton rb_seikyu_no;
        private System.Windows.Forms.TextBox tb_urikake_no;
        private System.Windows.Forms.TextBox tb_urikake_no_midasi;
        private System.Windows.Forms.Button btn_syuuryou;
        private System.Windows.Forms.TextBox tb_simebi_midasi;
        private System.Windows.Forms.TextBox tb_torihikisaki_cd2;
        private System.Windows.Forms.TextBox tb_torihikisaki_cd1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_simebi;
        private GrapeCity.ActiveReports.Viewer.Win.Viewer viewer1;
        private System.Windows.Forms.Button btn_preview;
    }
}