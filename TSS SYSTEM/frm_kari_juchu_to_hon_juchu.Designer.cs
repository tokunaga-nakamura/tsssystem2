namespace TSS_SYSTEM
{
    partial class frm_kari_juchu_to_hon_juchu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_kari_juchu_to_hon_juchu));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_hardcopy = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.btn_jikkou = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tb_torihikisaki_name = new System.Windows.Forms.TextBox();
            this.tb_juchu_su = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.tb_seihin_name = new System.Windows.Forms.TextBox();
            this.tb_seihin_cd = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.tb_juchu_cd2 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.tb_juchu_cd1 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.tb_torihikisaki_cd = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tb_torihikisaki_name2 = new System.Windows.Forms.TextBox();
            this.tb_juchu_cd12 = new System.Windows.Forms.TextBox();
            this.tb_torihikisaki_cd2 = new System.Windows.Forms.TextBox();
            this.textBox22 = new System.Windows.Forms.TextBox();
            this.tb_juchu_cd22 = new System.Windows.Forms.TextBox();
            this.textBox18 = new System.Windows.Forms.TextBox();
            this.textBox20 = new System.Windows.Forms.TextBox();
            this.tb_log = new System.Windows.Forms.TextBox();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
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
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
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
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.btn_hardcopy);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(884, 539);
            this.splitContainer1.SplitterDistance = 58;
            this.splitContainer1.TabIndex = 1;
            this.splitContainer1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(90, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(478, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "他の端末で移行する対象データを処理していると更新できずに整合性が取れなくなる恐れがあります。";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(90, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(537, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "この処理は、「受注」・「納品」・「部品入出庫移動」・「売上」・「生産スケジュール」等のデータも同時に更新します。";
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
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer4);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(884, 477);
            this.splitContainer2.SplitterDistance = 439;
            this.splitContainer2.TabIndex = 0;
            this.splitContainer2.TabStop = false;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer4.IsSplitterFixed = true;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.btn_jikkou);
            this.splitContainer4.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer4.Panel1.Controls.Add(this.label1);
            this.splitContainer4.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.tb_log);
            this.splitContainer4.Size = new System.Drawing.Size(880, 435);
            this.splitContainer4.SplitterDistance = 175;
            this.splitContainer4.TabIndex = 12;
            this.splitContainer4.TabStop = false;
            // 
            // btn_jikkou
            // 
            this.btn_jikkou.Location = new System.Drawing.Point(795, 144);
            this.btn_jikkou.Name = "btn_jikkou";
            this.btn_jikkou.Size = new System.Drawing.Size(75, 23);
            this.btn_jikkou.TabIndex = 4;
            this.btn_jikkou.Text = "実行";
            this.btn_jikkou.UseVisualStyleBackColor = true;
            this.btn_jikkou.Click += new System.EventHandler(this.btn_jikkou_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tb_torihikisaki_name);
            this.groupBox1.Controls.Add(this.tb_juchu_su);
            this.groupBox1.Controls.Add(this.textBox10);
            this.groupBox1.Controls.Add(this.tb_seihin_name);
            this.groupBox1.Controls.Add(this.tb_seihin_cd);
            this.groupBox1.Controls.Add(this.textBox7);
            this.groupBox1.Controls.Add(this.tb_juchu_cd2);
            this.groupBox1.Controls.Add(this.textBox5);
            this.groupBox1.Controls.Add(this.tb_juchu_cd1);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.tb_torihikisaki_cd);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Location = new System.Drawing.Point(10, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(390, 164);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "仮登録されている受注";
            // 
            // tb_torihikisaki_name
            // 
            this.tb_torihikisaki_name.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_torihikisaki_name.Location = new System.Drawing.Point(130, 18);
            this.tb_torihikisaki_name.Name = "tb_torihikisaki_name";
            this.tb_torihikisaki_name.ReadOnly = true;
            this.tb_torihikisaki_name.Size = new System.Drawing.Size(254, 19);
            this.tb_torihikisaki_name.TabIndex = 11;
            this.tb_torihikisaki_name.TabStop = false;
            // 
            // tb_juchu_su
            // 
            this.tb_juchu_su.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_juchu_su.Location = new System.Drawing.Point(82, 137);
            this.tb_juchu_su.Name = "tb_juchu_su";
            this.tb_juchu_su.ReadOnly = true;
            this.tb_juchu_su.Size = new System.Drawing.Size(100, 19);
            this.tb_juchu_su.TabIndex = 10;
            this.tb_juchu_su.TabStop = false;
            // 
            // textBox10
            // 
            this.textBox10.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox10.Location = new System.Drawing.Point(6, 137);
            this.textBox10.Name = "textBox10";
            this.textBox10.ReadOnly = true;
            this.textBox10.Size = new System.Drawing.Size(76, 19);
            this.textBox10.TabIndex = 9;
            this.textBox10.TabStop = false;
            this.textBox10.Text = "受注数";
            // 
            // tb_seihin_name
            // 
            this.tb_seihin_name.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_seihin_name.Location = new System.Drawing.Point(82, 112);
            this.tb_seihin_name.Name = "tb_seihin_name";
            this.tb_seihin_name.ReadOnly = true;
            this.tb_seihin_name.Size = new System.Drawing.Size(302, 19);
            this.tb_seihin_name.TabIndex = 8;
            this.tb_seihin_name.TabStop = false;
            // 
            // tb_seihin_cd
            // 
            this.tb_seihin_cd.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_seihin_cd.Location = new System.Drawing.Point(82, 93);
            this.tb_seihin_cd.Name = "tb_seihin_cd";
            this.tb_seihin_cd.ReadOnly = true;
            this.tb_seihin_cd.Size = new System.Drawing.Size(107, 19);
            this.tb_seihin_cd.TabIndex = 7;
            this.tb_seihin_cd.TabStop = false;
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox7.Location = new System.Drawing.Point(6, 93);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(76, 19);
            this.textBox7.TabIndex = 6;
            this.textBox7.TabStop = false;
            this.textBox7.Text = "製品コード";
            // 
            // tb_juchu_cd2
            // 
            this.tb_juchu_cd2.BackColor = System.Drawing.Color.PowderBlue;
            this.tb_juchu_cd2.Location = new System.Drawing.Point(82, 68);
            this.tb_juchu_cd2.MaxLength = 16;
            this.tb_juchu_cd2.Name = "tb_juchu_cd2";
            this.tb_juchu_cd2.Size = new System.Drawing.Size(107, 19);
            this.tb_juchu_cd2.TabIndex = 2;
            this.tb_juchu_cd2.DoubleClick += new System.EventHandler(this.tb_juchu_cd2_DoubleClick);
            this.tb_juchu_cd2.Validating += new System.ComponentModel.CancelEventHandler(this.tb_juchu_cd2_Validating);
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox5.Location = new System.Drawing.Point(6, 68);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(76, 19);
            this.textBox5.TabIndex = 4;
            this.textBox5.TabStop = false;
            this.textBox5.Text = "受注コード2";
            // 
            // tb_juchu_cd1
            // 
            this.tb_juchu_cd1.Location = new System.Drawing.Point(82, 43);
            this.tb_juchu_cd1.MaxLength = 16;
            this.tb_juchu_cd1.Name = "tb_juchu_cd1";
            this.tb_juchu_cd1.Size = new System.Drawing.Size(107, 19);
            this.tb_juchu_cd1.TabIndex = 1;
            this.tb_juchu_cd1.Validating += new System.ComponentModel.CancelEventHandler(this.tb_juchu_cd1_Validating);
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox3.Location = new System.Drawing.Point(6, 43);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(76, 19);
            this.textBox3.TabIndex = 2;
            this.textBox3.TabStop = false;
            this.textBox3.Text = "受注コード1";
            // 
            // tb_torihikisaki_cd
            // 
            this.tb_torihikisaki_cd.BackColor = System.Drawing.Color.PowderBlue;
            this.tb_torihikisaki_cd.Location = new System.Drawing.Point(82, 18);
            this.tb_torihikisaki_cd.MaxLength = 6;
            this.tb_torihikisaki_cd.Name = "tb_torihikisaki_cd";
            this.tb_torihikisaki_cd.Size = new System.Drawing.Size(48, 19);
            this.tb_torihikisaki_cd.TabIndex = 0;
            this.tb_torihikisaki_cd.DoubleClick += new System.EventHandler(this.tb_torihikisaki_cd_DoubleClick);
            this.tb_torihikisaki_cd.Validating += new System.ComponentModel.CancelEventHandler(this.tb_torihikisaki_cd_Validating);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox1.Location = new System.Drawing.Point(6, 18);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(76, 19);
            this.textBox1.TabIndex = 0;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "取引先コード";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(406, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 48);
            this.label1.TabIndex = 2;
            this.label1.Text = "→";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tb_torihikisaki_name2);
            this.groupBox2.Controls.Add(this.tb_juchu_cd12);
            this.groupBox2.Controls.Add(this.tb_torihikisaki_cd2);
            this.groupBox2.Controls.Add(this.textBox22);
            this.groupBox2.Controls.Add(this.tb_juchu_cd22);
            this.groupBox2.Controls.Add(this.textBox18);
            this.groupBox2.Controls.Add(this.textBox20);
            this.groupBox2.Location = new System.Drawing.Point(480, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(390, 97);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "本登録する受注";
            // 
            // tb_torihikisaki_name2
            // 
            this.tb_torihikisaki_name2.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_torihikisaki_name2.Location = new System.Drawing.Point(130, 18);
            this.tb_torihikisaki_name2.Name = "tb_torihikisaki_name2";
            this.tb_torihikisaki_name2.ReadOnly = true;
            this.tb_torihikisaki_name2.Size = new System.Drawing.Size(254, 19);
            this.tb_torihikisaki_name2.TabIndex = 11;
            this.tb_torihikisaki_name2.TabStop = false;
            // 
            // tb_juchu_cd12
            // 
            this.tb_juchu_cd12.BackColor = System.Drawing.SystemColors.Window;
            this.tb_juchu_cd12.Location = new System.Drawing.Point(82, 43);
            this.tb_juchu_cd12.MaxLength = 16;
            this.tb_juchu_cd12.Name = "tb_juchu_cd12";
            this.tb_juchu_cd12.Size = new System.Drawing.Size(107, 19);
            this.tb_juchu_cd12.TabIndex = 3;
            this.tb_juchu_cd12.Validating += new System.ComponentModel.CancelEventHandler(this.tb_juchu_cd12_Validating);
            // 
            // tb_torihikisaki_cd2
            // 
            this.tb_torihikisaki_cd2.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_torihikisaki_cd2.Location = new System.Drawing.Point(82, 18);
            this.tb_torihikisaki_cd2.Name = "tb_torihikisaki_cd2";
            this.tb_torihikisaki_cd2.ReadOnly = true;
            this.tb_torihikisaki_cd2.Size = new System.Drawing.Size(48, 19);
            this.tb_torihikisaki_cd2.TabIndex = 1;
            this.tb_torihikisaki_cd2.TabStop = false;
            // 
            // textBox22
            // 
            this.textBox22.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox22.Location = new System.Drawing.Point(6, 18);
            this.textBox22.Name = "textBox22";
            this.textBox22.ReadOnly = true;
            this.textBox22.Size = new System.Drawing.Size(76, 19);
            this.textBox22.TabIndex = 0;
            this.textBox22.TabStop = false;
            this.textBox22.Text = "取引先コード";
            // 
            // tb_juchu_cd22
            // 
            this.tb_juchu_cd22.Location = new System.Drawing.Point(82, 68);
            this.tb_juchu_cd22.MaxLength = 16;
            this.tb_juchu_cd22.Name = "tb_juchu_cd22";
            this.tb_juchu_cd22.Size = new System.Drawing.Size(107, 19);
            this.tb_juchu_cd22.TabIndex = 3;
            this.tb_juchu_cd22.Validating += new System.ComponentModel.CancelEventHandler(this.tb_juchu_cd22_Validating);
            // 
            // textBox18
            // 
            this.textBox18.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox18.Location = new System.Drawing.Point(6, 68);
            this.textBox18.Name = "textBox18";
            this.textBox18.ReadOnly = true;
            this.textBox18.Size = new System.Drawing.Size(76, 19);
            this.textBox18.TabIndex = 4;
            this.textBox18.TabStop = false;
            this.textBox18.Text = "受注コード2";
            // 
            // textBox20
            // 
            this.textBox20.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox20.Location = new System.Drawing.Point(6, 43);
            this.textBox20.Name = "textBox20";
            this.textBox20.ReadOnly = true;
            this.textBox20.Size = new System.Drawing.Size(76, 19);
            this.textBox20.TabIndex = 2;
            this.textBox20.TabStop = false;
            this.textBox20.Text = "受注コード1";
            // 
            // tb_log
            // 
            this.tb_log.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_log.Location = new System.Drawing.Point(0, 0);
            this.tb_log.Multiline = true;
            this.tb_log.Name = "tb_log";
            this.tb_log.ReadOnly = true;
            this.tb_log.Size = new System.Drawing.Size(880, 256);
            this.tb_log.TabIndex = 0;
            this.tb_log.TabStop = false;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer3.IsSplitterFixed = true;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.btn_syuuryou);
            this.splitContainer3.Size = new System.Drawing.Size(880, 30);
            this.splitContainer3.SplitterDistance = 794;
            this.splitContainer3.TabIndex = 0;
            this.splitContainer3.TabStop = false;
            // 
            // btn_syuuryou
            // 
            this.btn_syuuryou.Location = new System.Drawing.Point(3, 3);
            this.btn_syuuryou.Name = "btn_syuuryou";
            this.btn_syuuryou.Size = new System.Drawing.Size(75, 23);
            this.btn_syuuryou.TabIndex = 5;
            this.btn_syuuryou.Text = "終了";
            this.btn_syuuryou.UseVisualStyleBackColor = true;
            this.btn_syuuryou.Click += new System.EventHandler(this.btn_syuuryou_Click);
            // 
            // frm_kari_juchu_to_hon_juchu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "frm_kari_juchu_to_hon_juchu";
            this.Text = "仮受注から正式受注へ移行";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
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
            this.splitContainer4.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
        private System.Windows.Forms.Button btn_syuuryou;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox tb_juchu_cd2;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox tb_juchu_cd1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox tb_torihikisaki_cd;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox tb_juchu_su;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.TextBox tb_seihin_name;
        private System.Windows.Forms.TextBox tb_seihin_cd;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tb_torihikisaki_name2;
        private System.Windows.Forms.TextBox tb_juchu_cd12;
        private System.Windows.Forms.TextBox tb_torihikisaki_cd2;
        private System.Windows.Forms.TextBox textBox22;
        private System.Windows.Forms.TextBox tb_juchu_cd22;
        private System.Windows.Forms.TextBox textBox18;
        private System.Windows.Forms.TextBox textBox20;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_torihikisaki_name;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.TextBox tb_log;
        private System.Windows.Forms.Button btn_jikkou;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}