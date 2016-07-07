namespace TSS_SYSTEM
{
    partial class frm_seisan_schedule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_seisan_schedule));
            this.btn_syuuryou = new System.Windows.Forms.Button();
            this.dgv_list = new System.Windows.Forms.DataGridView();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.btn_touroku = new System.Windows.Forms.Button();
            this.btn_insatu = new System.Windows.Forms.Button();
            this.btn_csv = new System.Windows.Forms.Button();
            this.tb_line_name = new System.Windows.Forms.TextBox();
            this.tb_koutei_name = new System.Windows.Forms.TextBox();
            this.tb_busyo_name = new System.Windows.Forms.TextBox();
            this.tb_line_cd = new System.Windows.Forms.TextBox();
            this.tb_koutei_cd = new System.Windows.Forms.TextBox();
            this.btn_hyouji = new System.Windows.Forms.Button();
            this.tb_busyo_cd = new System.Windows.Forms.TextBox();
            this.tb_busyo_midasi = new System.Windows.Forms.TextBox();
            this.tb_koutei_midasi = new System.Windows.Forms.TextBox();
            this.tb_line_midasi = new System.Windows.Forms.TextBox();
            this.cb_line_sitei = new System.Windows.Forms.CheckBox();
            this.cb_koutei_sitei = new System.Windows.Forms.CheckBox();
            this.cb_busyo_sitei = new System.Windows.Forms.CheckBox();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.btn_line_tuika_under = new System.Windows.Forms.Button();
            this.textBox32 = new System.Windows.Forms.TextBox();
            this.tb_create_user_cd = new System.Windows.Forms.TextBox();
            this.tb_create_datetime = new System.Windows.Forms.TextBox();
            this.textBox35 = new System.Windows.Forms.TextBox();
            this.tb_update_user_cd = new System.Windows.Forms.TextBox();
            this.tb_update_datetime = new System.Windows.Forms.TextBox();
            this.btn_line_tuika = new System.Windows.Forms.Button();
            this.btn_seisan_jun_down = new System.Windows.Forms.Button();
            this.btn_seisan_jun_up = new System.Windows.Forms.Button();
            this.tb_seisan_yotei_date = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.btn_hardcopy = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
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
            // dgv_list
            // 
            this.dgv_list.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_list.Location = new System.Drawing.Point(0, 0);
            this.dgv_list.Name = "dgv_list";
            this.dgv_list.RowTemplate.Height = 21;
            this.dgv_list.Size = new System.Drawing.Size(1180, 299);
            this.dgv_list.TabIndex = 0;
            this.dgv_list.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_list_CellDoubleClick);
            this.dgv_list.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_list_CellFormatting);
            this.dgv_list.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgv_list_CellPainting);
            this.dgv_list.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_list_CellValidated);
            this.dgv_list.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgv_list_CellValidating);
            this.dgv_list.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgv_list_UserDeletedRow);
            this.dgv_list.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgv_list_UserDeletingRow);
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer4.IsSplitterFixed = true;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.btn_touroku);
            this.splitContainer4.Panel1.Controls.Add(this.btn_insatu);
            this.splitContainer4.Panel1.Controls.Add(this.btn_csv);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.btn_syuuryou);
            this.splitContainer4.Size = new System.Drawing.Size(1180, 31);
            this.splitContainer4.SplitterDistance = 1095;
            this.splitContainer4.TabIndex = 1;
            // 
            // btn_touroku
            // 
            this.btn_touroku.Location = new System.Drawing.Point(6, 5);
            this.btn_touroku.Name = "btn_touroku";
            this.btn_touroku.Size = new System.Drawing.Size(75, 23);
            this.btn_touroku.TabIndex = 13;
            this.btn_touroku.Text = "登録";
            this.btn_touroku.UseVisualStyleBackColor = true;
            this.btn_touroku.Click += new System.EventHandler(this.btn_touroku_Click);
            // 
            // btn_insatu
            // 
            this.btn_insatu.Location = new System.Drawing.Point(119, 5);
            this.btn_insatu.Name = "btn_insatu";
            this.btn_insatu.Size = new System.Drawing.Size(75, 23);
            this.btn_insatu.TabIndex = 2;
            this.btn_insatu.Text = "印刷";
            this.btn_insatu.UseVisualStyleBackColor = true;
            this.btn_insatu.Click += new System.EventHandler(this.btn_insatu_Click);
            // 
            // btn_csv
            // 
            this.btn_csv.Location = new System.Drawing.Point(200, 5);
            this.btn_csv.Name = "btn_csv";
            this.btn_csv.Size = new System.Drawing.Size(75, 23);
            this.btn_csv.TabIndex = 3;
            this.btn_csv.Text = "CSV出力";
            this.btn_csv.UseVisualStyleBackColor = true;
            this.btn_csv.Click += new System.EventHandler(this.btn_csv_Click);
            // 
            // tb_line_name
            // 
            this.tb_line_name.Enabled = false;
            this.tb_line_name.Location = new System.Drawing.Point(171, 60);
            this.tb_line_name.Name = "tb_line_name";
            this.tb_line_name.ReadOnly = true;
            this.tb_line_name.Size = new System.Drawing.Size(100, 19);
            this.tb_line_name.TabIndex = 17;
            this.tb_line_name.TabStop = false;
            // 
            // tb_koutei_name
            // 
            this.tb_koutei_name.Enabled = false;
            this.tb_koutei_name.Location = new System.Drawing.Point(170, 38);
            this.tb_koutei_name.Name = "tb_koutei_name";
            this.tb_koutei_name.ReadOnly = true;
            this.tb_koutei_name.Size = new System.Drawing.Size(100, 19);
            this.tb_koutei_name.TabIndex = 16;
            this.tb_koutei_name.TabStop = false;
            // 
            // tb_busyo_name
            // 
            this.tb_busyo_name.Enabled = false;
            this.tb_busyo_name.Location = new System.Drawing.Point(170, 16);
            this.tb_busyo_name.Name = "tb_busyo_name";
            this.tb_busyo_name.ReadOnly = true;
            this.tb_busyo_name.Size = new System.Drawing.Size(100, 19);
            this.tb_busyo_name.TabIndex = 15;
            this.tb_busyo_name.TabStop = false;
            // 
            // tb_line_cd
            // 
            this.tb_line_cd.BackColor = System.Drawing.Color.PowderBlue;
            this.tb_line_cd.Enabled = false;
            this.tb_line_cd.Location = new System.Drawing.Point(138, 60);
            this.tb_line_cd.Name = "tb_line_cd";
            this.tb_line_cd.Size = new System.Drawing.Size(33, 19);
            this.tb_line_cd.TabIndex = 14;
            this.tb_line_cd.DoubleClick += new System.EventHandler(this.tb_line_cd_DoubleClick);
            // 
            // tb_koutei_cd
            // 
            this.tb_koutei_cd.BackColor = System.Drawing.Color.PowderBlue;
            this.tb_koutei_cd.Enabled = false;
            this.tb_koutei_cd.Location = new System.Drawing.Point(137, 38);
            this.tb_koutei_cd.Name = "tb_koutei_cd";
            this.tb_koutei_cd.Size = new System.Drawing.Size(33, 19);
            this.tb_koutei_cd.TabIndex = 13;
            this.tb_koutei_cd.DoubleClick += new System.EventHandler(this.tb_koutei_cd_DoubleClick);
            // 
            // btn_hyouji
            // 
            this.btn_hyouji.Enabled = false;
            this.btn_hyouji.Location = new System.Drawing.Point(219, 5);
            this.btn_hyouji.Name = "btn_hyouji";
            this.btn_hyouji.Size = new System.Drawing.Size(75, 23);
            this.btn_hyouji.TabIndex = 5;
            this.btn_hyouji.Text = "表示";
            this.btn_hyouji.UseVisualStyleBackColor = true;
            this.btn_hyouji.Click += new System.EventHandler(this.btn_hyouji_Click);
            // 
            // tb_busyo_cd
            // 
            this.tb_busyo_cd.BackColor = System.Drawing.Color.PowderBlue;
            this.tb_busyo_cd.Enabled = false;
            this.tb_busyo_cd.Location = new System.Drawing.Point(137, 16);
            this.tb_busyo_cd.Name = "tb_busyo_cd";
            this.tb_busyo_cd.Size = new System.Drawing.Size(33, 19);
            this.tb_busyo_cd.TabIndex = 12;
            this.tb_busyo_cd.DoubleClick += new System.EventHandler(this.tb_busyo_cd_DoubleClick);
            // 
            // tb_busyo_midasi
            // 
            this.tb_busyo_midasi.BackColor = System.Drawing.Color.NavajoWhite;
            this.tb_busyo_midasi.Enabled = false;
            this.tb_busyo_midasi.Location = new System.Drawing.Point(75, 16);
            this.tb_busyo_midasi.Name = "tb_busyo_midasi";
            this.tb_busyo_midasi.ReadOnly = true;
            this.tb_busyo_midasi.Size = new System.Drawing.Size(62, 19);
            this.tb_busyo_midasi.TabIndex = 11;
            this.tb_busyo_midasi.TabStop = false;
            this.tb_busyo_midasi.Text = "部署コード";
            // 
            // tb_koutei_midasi
            // 
            this.tb_koutei_midasi.BackColor = System.Drawing.Color.NavajoWhite;
            this.tb_koutei_midasi.Enabled = false;
            this.tb_koutei_midasi.Location = new System.Drawing.Point(75, 38);
            this.tb_koutei_midasi.Name = "tb_koutei_midasi";
            this.tb_koutei_midasi.ReadOnly = true;
            this.tb_koutei_midasi.Size = new System.Drawing.Size(62, 19);
            this.tb_koutei_midasi.TabIndex = 10;
            this.tb_koutei_midasi.TabStop = false;
            this.tb_koutei_midasi.Text = "工程コード";
            // 
            // tb_line_midasi
            // 
            this.tb_line_midasi.BackColor = System.Drawing.Color.NavajoWhite;
            this.tb_line_midasi.Enabled = false;
            this.tb_line_midasi.Location = new System.Drawing.Point(76, 60);
            this.tb_line_midasi.Name = "tb_line_midasi";
            this.tb_line_midasi.ReadOnly = true;
            this.tb_line_midasi.Size = new System.Drawing.Size(62, 19);
            this.tb_line_midasi.TabIndex = 9;
            this.tb_line_midasi.TabStop = false;
            this.tb_line_midasi.Text = "ラインコード";
            // 
            // cb_line_sitei
            // 
            this.cb_line_sitei.AutoSize = true;
            this.cb_line_sitei.Enabled = false;
            this.cb_line_sitei.Location = new System.Drawing.Point(6, 62);
            this.cb_line_sitei.Name = "cb_line_sitei";
            this.cb_line_sitei.Size = new System.Drawing.Size(74, 16);
            this.cb_line_sitei.TabIndex = 8;
            this.cb_line_sitei.Text = "ライン指定";
            this.cb_line_sitei.UseVisualStyleBackColor = true;
            this.cb_line_sitei.CheckedChanged += new System.EventHandler(this.cb_line_sitei_CheckedChanged);
            // 
            // cb_koutei_sitei
            // 
            this.cb_koutei_sitei.AutoSize = true;
            this.cb_koutei_sitei.Enabled = false;
            this.cb_koutei_sitei.Location = new System.Drawing.Point(6, 40);
            this.cb_koutei_sitei.Name = "cb_koutei_sitei";
            this.cb_koutei_sitei.Size = new System.Drawing.Size(72, 16);
            this.cb_koutei_sitei.TabIndex = 7;
            this.cb_koutei_sitei.Text = "工程指定";
            this.cb_koutei_sitei.UseVisualStyleBackColor = true;
            this.cb_koutei_sitei.CheckedChanged += new System.EventHandler(this.cb_koutei_sitei_CheckedChanged);
            // 
            // cb_busyo_sitei
            // 
            this.cb_busyo_sitei.AutoSize = true;
            this.cb_busyo_sitei.Enabled = false;
            this.cb_busyo_sitei.Location = new System.Drawing.Point(6, 18);
            this.cb_busyo_sitei.Name = "cb_busyo_sitei";
            this.cb_busyo_sitei.Size = new System.Drawing.Size(72, 16);
            this.cb_busyo_sitei.TabIndex = 6;
            this.cb_busyo_sitei.Text = "部署指定";
            this.cb_busyo_sitei.UseVisualStyleBackColor = true;
            this.cb_busyo_sitei.CheckedChanged += new System.EventHandler(this.cb_busyo_sitei_CheckedChanged);
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
            this.splitContainer3.Panel1.Controls.Add(this.btn_line_tuika_under);
            this.splitContainer3.Panel1.Controls.Add(this.textBox32);
            this.splitContainer3.Panel1.Controls.Add(this.tb_create_user_cd);
            this.splitContainer3.Panel1.Controls.Add(this.tb_create_datetime);
            this.splitContainer3.Panel1.Controls.Add(this.textBox35);
            this.splitContainer3.Panel1.Controls.Add(this.tb_update_user_cd);
            this.splitContainer3.Panel1.Controls.Add(this.tb_update_datetime);
            this.splitContainer3.Panel1.Controls.Add(this.btn_line_tuika);
            this.splitContainer3.Panel1.Controls.Add(this.btn_seisan_jun_down);
            this.splitContainer3.Panel1.Controls.Add(this.btn_seisan_jun_up);
            this.splitContainer3.Panel1.Controls.Add(this.tb_seisan_yotei_date);
            this.splitContainer3.Panel1.Controls.Add(this.btn_hyouji);
            this.splitContainer3.Panel1.Controls.Add(this.textBox6);
            this.splitContainer3.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.dgv_list);
            this.splitContainer3.Size = new System.Drawing.Size(1184, 437);
            this.splitContainer3.SplitterDistance = 130;
            this.splitContainer3.TabIndex = 0;
            // 
            // btn_line_tuika_under
            // 
            this.btn_line_tuika_under.Location = new System.Drawing.Point(328, 94);
            this.btn_line_tuika_under.Name = "btn_line_tuika_under";
            this.btn_line_tuika_under.Size = new System.Drawing.Size(122, 23);
            this.btn_line_tuika_under.TabIndex = 111;
            this.btn_line_tuika_under.TabStop = false;
            this.btn_line_tuika_under.Text = "下に1行追加";
            this.btn_line_tuika_under.UseVisualStyleBackColor = true;
            this.btn_line_tuika_under.Click += new System.EventHandler(this.btn_line_tuika_under_Click);
            // 
            // textBox32
            // 
            this.textBox32.BackColor = System.Drawing.Color.Gainsboro;
            this.textBox32.Location = new System.Drawing.Point(959, 9);
            this.textBox32.Name = "textBox32";
            this.textBox32.ReadOnly = true;
            this.textBox32.Size = new System.Drawing.Size(45, 19);
            this.textBox32.TabIndex = 105;
            this.textBox32.TabStop = false;
            this.textBox32.Text = "作成";
            // 
            // tb_create_user_cd
            // 
            this.tb_create_user_cd.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_create_user_cd.Location = new System.Drawing.Point(1004, 9);
            this.tb_create_user_cd.Name = "tb_create_user_cd";
            this.tb_create_user_cd.ReadOnly = true;
            this.tb_create_user_cd.Size = new System.Drawing.Size(42, 19);
            this.tb_create_user_cd.TabIndex = 106;
            this.tb_create_user_cd.TabStop = false;
            // 
            // tb_create_datetime
            // 
            this.tb_create_datetime.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_create_datetime.Location = new System.Drawing.Point(1046, 9);
            this.tb_create_datetime.Name = "tb_create_datetime";
            this.tb_create_datetime.ReadOnly = true;
            this.tb_create_datetime.Size = new System.Drawing.Size(130, 19);
            this.tb_create_datetime.TabIndex = 107;
            this.tb_create_datetime.TabStop = false;
            // 
            // textBox35
            // 
            this.textBox35.BackColor = System.Drawing.Color.Gainsboro;
            this.textBox35.Location = new System.Drawing.Point(959, 28);
            this.textBox35.Name = "textBox35";
            this.textBox35.ReadOnly = true;
            this.textBox35.Size = new System.Drawing.Size(45, 19);
            this.textBox35.TabIndex = 108;
            this.textBox35.TabStop = false;
            this.textBox35.Text = "更新";
            // 
            // tb_update_user_cd
            // 
            this.tb_update_user_cd.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_update_user_cd.Location = new System.Drawing.Point(1004, 28);
            this.tb_update_user_cd.Name = "tb_update_user_cd";
            this.tb_update_user_cd.ReadOnly = true;
            this.tb_update_user_cd.Size = new System.Drawing.Size(42, 19);
            this.tb_update_user_cd.TabIndex = 109;
            this.tb_update_user_cd.TabStop = false;
            // 
            // tb_update_datetime
            // 
            this.tb_update_datetime.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_update_datetime.Location = new System.Drawing.Point(1046, 28);
            this.tb_update_datetime.Name = "tb_update_datetime";
            this.tb_update_datetime.ReadOnly = true;
            this.tb_update_datetime.Size = new System.Drawing.Size(130, 19);
            this.tb_update_datetime.TabIndex = 110;
            this.tb_update_datetime.TabStop = false;
            // 
            // btn_line_tuika
            // 
            this.btn_line_tuika.Location = new System.Drawing.Point(328, 63);
            this.btn_line_tuika.Name = "btn_line_tuika";
            this.btn_line_tuika.Size = new System.Drawing.Size(122, 23);
            this.btn_line_tuika.TabIndex = 104;
            this.btn_line_tuika.TabStop = false;
            this.btn_line_tuika.Text = "上に1行追加";
            this.btn_line_tuika.UseVisualStyleBackColor = true;
            this.btn_line_tuika.Click += new System.EventHandler(this.btn_line_tuika_Click);
            // 
            // btn_seisan_jun_down
            // 
            this.btn_seisan_jun_down.Location = new System.Drawing.Point(577, 95);
            this.btn_seisan_jun_down.Name = "btn_seisan_jun_down";
            this.btn_seisan_jun_down.Size = new System.Drawing.Size(75, 23);
            this.btn_seisan_jun_down.TabIndex = 9;
            this.btn_seisan_jun_down.Text = "下に移動";
            this.btn_seisan_jun_down.UseVisualStyleBackColor = true;
            this.btn_seisan_jun_down.Click += new System.EventHandler(this.btn_seisan_jun_down_Click);
            // 
            // btn_seisan_jun_up
            // 
            this.btn_seisan_jun_up.Location = new System.Drawing.Point(496, 95);
            this.btn_seisan_jun_up.Name = "btn_seisan_jun_up";
            this.btn_seisan_jun_up.Size = new System.Drawing.Size(75, 23);
            this.btn_seisan_jun_up.TabIndex = 8;
            this.btn_seisan_jun_up.Text = "上に移動";
            this.btn_seisan_jun_up.UseVisualStyleBackColor = true;
            this.btn_seisan_jun_up.Click += new System.EventHandler(this.btn_seisan_jun_up_Click);
            // 
            // tb_seisan_yotei_date
            // 
            this.tb_seisan_yotei_date.Location = new System.Drawing.Point(78, 5);
            this.tb_seisan_yotei_date.MaxLength = 10;
            this.tb_seisan_yotei_date.Name = "tb_seisan_yotei_date";
            this.tb_seisan_yotei_date.Size = new System.Drawing.Size(74, 19);
            this.tb_seisan_yotei_date.TabIndex = 0;
            this.tb_seisan_yotei_date.Validating += new System.ComponentModel.CancelEventHandler(this.tb_seisan_yotei_date_Validating);
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.Color.NavajoWhite;
            this.textBox6.Location = new System.Drawing.Point(3, 5);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(75, 19);
            this.textBox6.TabIndex = 7;
            this.textBox6.TabStop = false;
            this.textBox6.Text = "生産予定日";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tb_line_name);
            this.groupBox1.Controls.Add(this.tb_koutei_name);
            this.groupBox1.Controls.Add(this.tb_busyo_name);
            this.groupBox1.Controls.Add(this.tb_line_cd);
            this.groupBox1.Controls.Add(this.tb_koutei_cd);
            this.groupBox1.Controls.Add(this.tb_busyo_cd);
            this.groupBox1.Controls.Add(this.tb_busyo_midasi);
            this.groupBox1.Controls.Add(this.tb_koutei_midasi);
            this.groupBox1.Controls.Add(this.tb_line_midasi);
            this.groupBox1.Controls.Add(this.cb_line_sitei);
            this.groupBox1.Controls.Add(this.cb_koutei_sitei);
            this.groupBox1.Controls.Add(this.cb_busyo_sitei);
            this.groupBox1.Location = new System.Drawing.Point(3, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(291, 88);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "絞込み";
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
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer2.Size = new System.Drawing.Size(1184, 476);
            this.splitContainer2.SplitterDistance = 437;
            this.splitContainer2.TabIndex = 0;
            this.splitContainer2.TabStop = false;
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
            this.splitContainer1.Size = new System.Drawing.Size(1184, 540);
            this.splitContainer1.SplitterDistance = 60;
            this.splitContainer1.TabIndex = 3;
            this.splitContainer1.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 540);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1184, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // frm_seisan_schedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 562);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.MinimumSize = new System.Drawing.Size(1200, 600);
            this.Name = "frm_seisan_schedule";
            this.Text = "生産スケジュール";
            this.Load += new System.EventHandler(this.frm_seisan_schedule_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).EndInit();
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_syuuryou;
        private System.Windows.Forms.DataGridView dgv_list;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.TextBox tb_line_name;
        private System.Windows.Forms.TextBox tb_koutei_name;
        private System.Windows.Forms.TextBox tb_busyo_name;
        private System.Windows.Forms.TextBox tb_line_cd;
        private System.Windows.Forms.TextBox tb_koutei_cd;
        private System.Windows.Forms.Button btn_hyouji;
        private System.Windows.Forms.TextBox tb_busyo_cd;
        private System.Windows.Forms.TextBox tb_busyo_midasi;
        private System.Windows.Forms.TextBox tb_koutei_midasi;
        private System.Windows.Forms.TextBox tb_line_midasi;
        private System.Windows.Forms.CheckBox cb_line_sitei;
        private System.Windows.Forms.CheckBox cb_koutei_sitei;
        private System.Windows.Forms.CheckBox cb_busyo_sitei;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button btn_hardcopy;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TextBox tb_seisan_yotei_date;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Button btn_insatu;
        private System.Windows.Forms.Button btn_csv;
        private System.Windows.Forms.Button btn_touroku;
        private System.Windows.Forms.Button btn_seisan_jun_down;
        private System.Windows.Forms.Button btn_seisan_jun_up;
        private System.Windows.Forms.Button btn_line_tuika;
        private System.Windows.Forms.TextBox textBox32;
        private System.Windows.Forms.TextBox tb_create_user_cd;
        private System.Windows.Forms.TextBox tb_create_datetime;
        private System.Windows.Forms.TextBox textBox35;
        private System.Windows.Forms.TextBox tb_update_user_cd;
        private System.Windows.Forms.TextBox tb_update_datetime;
        private System.Windows.Forms.Button btn_line_tuika_under;
    }
}