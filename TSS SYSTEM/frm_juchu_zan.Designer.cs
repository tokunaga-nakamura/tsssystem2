namespace TSS_SYSTEM
{
    partial class frm_juchu_zan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_juchu_zan));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_hardcopy = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_chuusyutu = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_create_datetime2 = new System.Windows.Forms.TextBox();
            this.tb_create_datetime1 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.tb_seihin_cd2 = new System.Windows.Forms.TextBox();
            this.tb_seihin_cd1 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.tb_torihikisaki_cd2 = new System.Windows.Forms.TextBox();
            this.tb_torihikisaki_cd1 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dgv_m = new System.Windows.Forms.DataGridView();
            this.btn_csv = new System.Windows.Forms.Button();
            this.btn_insatu = new System.Windows.Forms.Button();
            this.btn_syuuryou = new System.Windows.Forms.Button();
            this.cb_syubetu_kbn = new System.Windows.Forms.CheckBox();
            this.tb_syubetu_kbn = new System.Windows.Forms.TextBox();
            this.tb_syubetu_name = new System.Windows.Forms.TextBox();
            this.tb_type_name = new System.Windows.Forms.TextBox();
            this.cb_bunrui_kbn = new System.Windows.Forms.CheckBox();
            this.tb_type_kbn = new System.Windows.Forms.TextBox();
            this.tb_bunrui_name = new System.Windows.Forms.TextBox();
            this.cb_type_kbn = new System.Windows.Forms.CheckBox();
            this.tb_bunrui_kbn = new System.Windows.Forms.TextBox();
            this.tb_sijou_name = new System.Windows.Forms.TextBox();
            this.cb_sijou_kbn = new System.Windows.Forms.CheckBox();
            this.tb_sijou_kbn = new System.Windows.Forms.TextBox();
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
            ((System.ComponentModel.ISupportInitialize)(this.dgv_m)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.btn_hardcopy);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(884, 539);
            this.splitContainer1.SplitterDistance = 61;
            this.splitContainer1.TabIndex = 1;
            this.splitContainer1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(84, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(318, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "指定条件受注情報から、受注数≠売上数のデータを抽出します。";
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
            this.splitContainer2.Panel2.Controls.Add(this.btn_csv);
            this.splitContainer2.Panel2.Controls.Add(this.btn_insatu);
            this.splitContainer2.Panel2.Controls.Add(this.btn_syuuryou);
            this.splitContainer2.Size = new System.Drawing.Size(884, 474);
            this.splitContainer2.SplitterDistance = 437;
            this.splitContainer2.TabIndex = 0;
            this.splitContainer2.TabStop = false;
            // 
            // splitContainer3
            // 
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.cb_syubetu_kbn);
            this.splitContainer3.Panel1.Controls.Add(this.tb_syubetu_kbn);
            this.splitContainer3.Panel1.Controls.Add(this.label5);
            this.splitContainer3.Panel1.Controls.Add(this.tb_syubetu_name);
            this.splitContainer3.Panel1.Controls.Add(this.tb_type_name);
            this.splitContainer3.Panel1.Controls.Add(this.btn_chuusyutu);
            this.splitContainer3.Panel1.Controls.Add(this.cb_bunrui_kbn);
            this.splitContainer3.Panel1.Controls.Add(this.label3);
            this.splitContainer3.Panel1.Controls.Add(this.tb_type_kbn);
            this.splitContainer3.Panel1.Controls.Add(this.label2);
            this.splitContainer3.Panel1.Controls.Add(this.tb_bunrui_name);
            this.splitContainer3.Panel1.Controls.Add(this.label1);
            this.splitContainer3.Panel1.Controls.Add(this.cb_type_kbn);
            this.splitContainer3.Panel1.Controls.Add(this.tb_create_datetime2);
            this.splitContainer3.Panel1.Controls.Add(this.tb_bunrui_kbn);
            this.splitContainer3.Panel1.Controls.Add(this.tb_create_datetime1);
            this.splitContainer3.Panel1.Controls.Add(this.tb_sijou_name);
            this.splitContainer3.Panel1.Controls.Add(this.textBox7);
            this.splitContainer3.Panel1.Controls.Add(this.cb_sijou_kbn);
            this.splitContainer3.Panel1.Controls.Add(this.tb_seihin_cd2);
            this.splitContainer3.Panel1.Controls.Add(this.tb_sijou_kbn);
            this.splitContainer3.Panel1.Controls.Add(this.tb_seihin_cd1);
            this.splitContainer3.Panel1.Controls.Add(this.textBox4);
            this.splitContainer3.Panel1.Controls.Add(this.tb_torihikisaki_cd2);
            this.splitContainer3.Panel1.Controls.Add(this.tb_torihikisaki_cd1);
            this.splitContainer3.Panel1.Controls.Add(this.textBox1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.dgv_m);
            this.splitContainer3.Size = new System.Drawing.Size(884, 437);
            this.splitContainer3.SplitterDistance = 98;
            this.splitContainer3.TabIndex = 0;
            this.splitContainer3.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(579, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(291, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "全て空白の状態で抽出すると、全受注情報から抽出します。";
            // 
            // btn_chuusyutu
            // 
            this.btn_chuusyutu.Location = new System.Drawing.Point(795, 66);
            this.btn_chuusyutu.Name = "btn_chuusyutu";
            this.btn_chuusyutu.Size = new System.Drawing.Size(75, 23);
            this.btn_chuusyutu.TabIndex = 6;
            this.btn_chuusyutu.Text = "抽出";
            this.btn_chuusyutu.UseVisualStyleBackColor = true;
            this.btn_chuusyutu.Click += new System.EventHandler(this.btn_chuusyutu_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(157, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "～";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(191, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "～";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(131, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "～";
            // 
            // tb_create_datetime2
            // 
            this.tb_create_datetime2.Location = new System.Drawing.Point(174, 53);
            this.tb_create_datetime2.MaxLength = 10;
            this.tb_create_datetime2.Name = "tb_create_datetime2";
            this.tb_create_datetime2.Size = new System.Drawing.Size(71, 19);
            this.tb_create_datetime2.TabIndex = 5;
            this.tb_create_datetime2.Validating += new System.ComponentModel.CancelEventHandler(this.tb_create_datetime2_Validating);
            // 
            // tb_create_datetime1
            // 
            this.tb_create_datetime1.Location = new System.Drawing.Point(86, 53);
            this.tb_create_datetime1.MaxLength = 10;
            this.tb_create_datetime1.Name = "tb_create_datetime1";
            this.tb_create_datetime1.Size = new System.Drawing.Size(71, 19);
            this.tb_create_datetime1.TabIndex = 4;
            this.tb_create_datetime1.Validating += new System.ComponentModel.CancelEventHandler(this.tb_create_datetime1_Validating);
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox7.Location = new System.Drawing.Point(10, 53);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(76, 19);
            this.textBox7.TabIndex = 6;
            this.textBox7.TabStop = false;
            this.textBox7.Text = "受注入力日";
            // 
            // tb_seihin_cd2
            // 
            this.tb_seihin_cd2.Location = new System.Drawing.Point(208, 28);
            this.tb_seihin_cd2.MaxLength = 16;
            this.tb_seihin_cd2.Name = "tb_seihin_cd2";
            this.tb_seihin_cd2.Size = new System.Drawing.Size(105, 19);
            this.tb_seihin_cd2.TabIndex = 3;
            this.tb_seihin_cd2.Validating += new System.ComponentModel.CancelEventHandler(this.tb_seihin_cd2_Validating);
            // 
            // tb_seihin_cd1
            // 
            this.tb_seihin_cd1.Location = new System.Drawing.Point(86, 28);
            this.tb_seihin_cd1.MaxLength = 16;
            this.tb_seihin_cd1.Name = "tb_seihin_cd1";
            this.tb_seihin_cd1.Size = new System.Drawing.Size(105, 19);
            this.tb_seihin_cd1.TabIndex = 2;
            this.tb_seihin_cd1.Validating += new System.ComponentModel.CancelEventHandler(this.tb_seihin_cd1_Validating);
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox4.Location = new System.Drawing.Point(10, 28);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(76, 19);
            this.textBox4.TabIndex = 3;
            this.textBox4.TabStop = false;
            this.textBox4.Text = "製品コード";
            // 
            // tb_torihikisaki_cd2
            // 
            this.tb_torihikisaki_cd2.Location = new System.Drawing.Point(148, 3);
            this.tb_torihikisaki_cd2.MaxLength = 6;
            this.tb_torihikisaki_cd2.Name = "tb_torihikisaki_cd2";
            this.tb_torihikisaki_cd2.Size = new System.Drawing.Size(45, 19);
            this.tb_torihikisaki_cd2.TabIndex = 1;
            this.tb_torihikisaki_cd2.Validating += new System.ComponentModel.CancelEventHandler(this.tb_torihikisaki_cd2_Validating);
            // 
            // tb_torihikisaki_cd1
            // 
            this.tb_torihikisaki_cd1.Location = new System.Drawing.Point(86, 3);
            this.tb_torihikisaki_cd1.MaxLength = 6;
            this.tb_torihikisaki_cd1.Name = "tb_torihikisaki_cd1";
            this.tb_torihikisaki_cd1.Size = new System.Drawing.Size(45, 19);
            this.tb_torihikisaki_cd1.TabIndex = 0;
            this.tb_torihikisaki_cd1.Validating += new System.ComponentModel.CancelEventHandler(this.tb_torihikisaki_cd1_Validating);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox1.Location = new System.Drawing.Point(10, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(76, 19);
            this.textBox1.TabIndex = 0;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "取引先コード";
            // 
            // dgv_m
            // 
            this.dgv_m.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_m.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_m.Location = new System.Drawing.Point(0, 0);
            this.dgv_m.Name = "dgv_m";
            this.dgv_m.RowTemplate.Height = 21;
            this.dgv_m.Size = new System.Drawing.Size(880, 331);
            this.dgv_m.TabIndex = 0;
            // 
            // btn_csv
            // 
            this.btn_csv.Location = new System.Drawing.Point(91, 3);
            this.btn_csv.Name = "btn_csv";
            this.btn_csv.Size = new System.Drawing.Size(75, 23);
            this.btn_csv.TabIndex = 1;
            this.btn_csv.Text = "CSV出力";
            this.btn_csv.UseVisualStyleBackColor = true;
            this.btn_csv.Click += new System.EventHandler(this.btn_csv_Click);
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
            this.btn_syuuryou.Location = new System.Drawing.Point(795, 3);
            this.btn_syuuryou.Name = "btn_syuuryou";
            this.btn_syuuryou.Size = new System.Drawing.Size(75, 23);
            this.btn_syuuryou.TabIndex = 2;
            this.btn_syuuryou.Text = "終了";
            this.btn_syuuryou.UseVisualStyleBackColor = true;
            this.btn_syuuryou.Click += new System.EventHandler(this.btn_syuuryou_Click);
            // 
            // cb_syubetu_kbn
            // 
            this.cb_syubetu_kbn.AutoSize = true;
            this.cb_syubetu_kbn.Location = new System.Drawing.Point(330, 6);
            this.cb_syubetu_kbn.Name = "cb_syubetu_kbn";
            this.cb_syubetu_kbn.Size = new System.Drawing.Size(72, 16);
            this.cb_syubetu_kbn.TabIndex = 3;
            this.cb_syubetu_kbn.Text = "種別区分";
            this.cb_syubetu_kbn.UseVisualStyleBackColor = true;
            this.cb_syubetu_kbn.CheckedChanged += new System.EventHandler(this.cb_syubetu_kbn_CheckedChanged);
            // 
            // tb_syubetu_kbn
            // 
            this.tb_syubetu_kbn.BackColor = System.Drawing.Color.PowderBlue;
            this.tb_syubetu_kbn.Location = new System.Drawing.Point(410, 4);
            this.tb_syubetu_kbn.MaxLength = 2;
            this.tb_syubetu_kbn.Name = "tb_syubetu_kbn";
            this.tb_syubetu_kbn.Size = new System.Drawing.Size(24, 19);
            this.tb_syubetu_kbn.TabIndex = 4;
            this.tb_syubetu_kbn.DoubleClick += new System.EventHandler(this.tb_syubetu_kbn_DoubleClick);
            this.tb_syubetu_kbn.Validating += new System.ComponentModel.CancelEventHandler(this.tb_syubetu_kbn_Validating);
            // 
            // tb_syubetu_name
            // 
            this.tb_syubetu_name.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_syubetu_name.Location = new System.Drawing.Point(434, 4);
            this.tb_syubetu_name.Name = "tb_syubetu_name";
            this.tb_syubetu_name.ReadOnly = true;
            this.tb_syubetu_name.Size = new System.Drawing.Size(90, 19);
            this.tb_syubetu_name.TabIndex = 9;
            this.tb_syubetu_name.TabStop = false;
            // 
            // tb_type_name
            // 
            this.tb_type_name.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_type_name.Location = new System.Drawing.Point(434, 70);
            this.tb_type_name.Name = "tb_type_name";
            this.tb_type_name.ReadOnly = true;
            this.tb_type_name.Size = new System.Drawing.Size(90, 19);
            this.tb_type_name.TabIndex = 18;
            this.tb_type_name.TabStop = false;
            // 
            // cb_bunrui_kbn
            // 
            this.cb_bunrui_kbn.AutoSize = true;
            this.cb_bunrui_kbn.Location = new System.Drawing.Point(330, 28);
            this.cb_bunrui_kbn.Name = "cb_bunrui_kbn";
            this.cb_bunrui_kbn.Size = new System.Drawing.Size(72, 16);
            this.cb_bunrui_kbn.TabIndex = 5;
            this.cb_bunrui_kbn.Text = "分類区分";
            this.cb_bunrui_kbn.UseVisualStyleBackColor = true;
            this.cb_bunrui_kbn.CheckedChanged += new System.EventHandler(this.cb_bunrui_kbn_CheckedChanged);
            // 
            // tb_type_kbn
            // 
            this.tb_type_kbn.BackColor = System.Drawing.Color.PowderBlue;
            this.tb_type_kbn.Location = new System.Drawing.Point(410, 70);
            this.tb_type_kbn.MaxLength = 2;
            this.tb_type_kbn.Name = "tb_type_kbn";
            this.tb_type_kbn.Size = new System.Drawing.Size(24, 19);
            this.tb_type_kbn.TabIndex = 10;
            this.tb_type_kbn.DoubleClick += new System.EventHandler(this.tb_type_kbn_DoubleClick);
            this.tb_type_kbn.Validating += new System.ComponentModel.CancelEventHandler(this.tb_type_kbn_Validating);
            // 
            // tb_bunrui_name
            // 
            this.tb_bunrui_name.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_bunrui_name.Location = new System.Drawing.Point(434, 26);
            this.tb_bunrui_name.Name = "tb_bunrui_name";
            this.tb_bunrui_name.ReadOnly = true;
            this.tb_bunrui_name.Size = new System.Drawing.Size(90, 19);
            this.tb_bunrui_name.TabIndex = 12;
            this.tb_bunrui_name.TabStop = false;
            // 
            // cb_type_kbn
            // 
            this.cb_type_kbn.AutoSize = true;
            this.cb_type_kbn.Location = new System.Drawing.Point(330, 72);
            this.cb_type_kbn.Name = "cb_type_kbn";
            this.cb_type_kbn.Size = new System.Drawing.Size(74, 16);
            this.cb_type_kbn.TabIndex = 9;
            this.cb_type_kbn.Text = "タイプ区分";
            this.cb_type_kbn.UseVisualStyleBackColor = true;
            this.cb_type_kbn.CheckedChanged += new System.EventHandler(this.cb_type_kbn_CheckedChanged);
            // 
            // tb_bunrui_kbn
            // 
            this.tb_bunrui_kbn.BackColor = System.Drawing.Color.PowderBlue;
            this.tb_bunrui_kbn.Location = new System.Drawing.Point(410, 26);
            this.tb_bunrui_kbn.MaxLength = 2;
            this.tb_bunrui_kbn.Name = "tb_bunrui_kbn";
            this.tb_bunrui_kbn.Size = new System.Drawing.Size(24, 19);
            this.tb_bunrui_kbn.TabIndex = 6;
            this.tb_bunrui_kbn.DoubleClick += new System.EventHandler(this.tb_bunrui_kbn_DoubleClick);
            this.tb_bunrui_kbn.Validating += new System.ComponentModel.CancelEventHandler(this.tb_bunrui_kbn_Validating);
            // 
            // tb_sijou_name
            // 
            this.tb_sijou_name.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_sijou_name.Location = new System.Drawing.Point(434, 48);
            this.tb_sijou_name.Name = "tb_sijou_name";
            this.tb_sijou_name.ReadOnly = true;
            this.tb_sijou_name.Size = new System.Drawing.Size(90, 19);
            this.tb_sijou_name.TabIndex = 15;
            this.tb_sijou_name.TabStop = false;
            // 
            // cb_sijou_kbn
            // 
            this.cb_sijou_kbn.AutoSize = true;
            this.cb_sijou_kbn.Location = new System.Drawing.Point(330, 50);
            this.cb_sijou_kbn.Name = "cb_sijou_kbn";
            this.cb_sijou_kbn.Size = new System.Drawing.Size(72, 16);
            this.cb_sijou_kbn.TabIndex = 7;
            this.cb_sijou_kbn.Text = "市場区分";
            this.cb_sijou_kbn.UseVisualStyleBackColor = true;
            this.cb_sijou_kbn.CheckedChanged += new System.EventHandler(this.cb_sijou_kbn_CheckedChanged);
            // 
            // tb_sijou_kbn
            // 
            this.tb_sijou_kbn.BackColor = System.Drawing.Color.PowderBlue;
            this.tb_sijou_kbn.Location = new System.Drawing.Point(410, 48);
            this.tb_sijou_kbn.MaxLength = 2;
            this.tb_sijou_kbn.Name = "tb_sijou_kbn";
            this.tb_sijou_kbn.Size = new System.Drawing.Size(24, 19);
            this.tb_sijou_kbn.TabIndex = 8;
            this.tb_sijou_kbn.DoubleClick += new System.EventHandler(this.tb_sijou_kbn_DoubleClick);
            this.tb_sijou_kbn.Validating += new System.ComponentModel.CancelEventHandler(this.tb_sijou_kbn_Validating);
            // 
            // frm_juchu_zan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "frm_juchu_zan";
            this.Text = "受注残照会";
            this.Load += new System.EventHandler(this.frm_juchu_zan_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
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
            ((System.ComponentModel.ISupportInitialize)(this.dgv_m)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btn_hardcopy;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.TextBox tb_seihin_cd2;
        private System.Windows.Forms.TextBox tb_seihin_cd1;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox tb_torihikisaki_cd2;
        private System.Windows.Forms.TextBox tb_torihikisaki_cd1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn_syuuryou;
        private System.Windows.Forms.Button btn_chuusyutu;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_create_datetime2;
        private System.Windows.Forms.TextBox tb_create_datetime1;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.DataGridView dgv_m;
        private System.Windows.Forms.Button btn_csv;
        private System.Windows.Forms.Button btn_insatu;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cb_syubetu_kbn;
        private System.Windows.Forms.TextBox tb_syubetu_kbn;
        private System.Windows.Forms.TextBox tb_syubetu_name;
        private System.Windows.Forms.TextBox tb_type_name;
        private System.Windows.Forms.CheckBox cb_bunrui_kbn;
        private System.Windows.Forms.TextBox tb_type_kbn;
        private System.Windows.Forms.TextBox tb_bunrui_name;
        private System.Windows.Forms.CheckBox cb_type_kbn;
        private System.Windows.Forms.TextBox tb_bunrui_kbn;
        private System.Windows.Forms.TextBox tb_sijou_name;
        private System.Windows.Forms.CheckBox cb_sijou_kbn;
        private System.Windows.Forms.TextBox tb_sijou_kbn;
    }
}