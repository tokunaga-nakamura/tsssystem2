namespace TSS_SYSTEM
{
    partial class frm_select_dt
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
            this.lbl5 = new System.Windows.Forms.Label();
            this.lbl4 = new System.Windows.Forms.Label();
            this.lbl3 = new System.Windows.Forms.Label();
            this.lbl2 = new System.Windows.Forms.Label();
            this.lbl1 = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dgv_dt = new System.Windows.Forms.DataGridView();
            this.btn_cansel = new System.Windows.Forms.Button();
            this.btn_no_select = new System.Windows.Forms.Button();
            this.btn_sentaku = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_dt)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.lbl5);
            this.splitContainer1.Panel1.Controls.Add(this.lbl4);
            this.splitContainer1.Panel1.Controls.Add(this.lbl3);
            this.splitContainer1.Panel1.Controls.Add(this.lbl2);
            this.splitContainer1.Panel1.Controls.Add(this.lbl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(536, 289);
            this.splitContainer1.SplitterDistance = 108;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.TabStop = false;
            // 
            // lbl5
            // 
            this.lbl5.AutoSize = true;
            this.lbl5.Location = new System.Drawing.Point(11, 86);
            this.lbl5.Name = "lbl5";
            this.lbl5.Size = new System.Drawing.Size(35, 12);
            this.lbl5.TabIndex = 4;
            this.lbl5.Text = "label1";
            // 
            // lbl4
            // 
            this.lbl4.AutoSize = true;
            this.lbl4.Location = new System.Drawing.Point(11, 68);
            this.lbl4.Name = "lbl4";
            this.lbl4.Size = new System.Drawing.Size(35, 12);
            this.lbl4.TabIndex = 3;
            this.lbl4.Text = "label4";
            // 
            // lbl3
            // 
            this.lbl3.AutoSize = true;
            this.lbl3.Location = new System.Drawing.Point(11, 48);
            this.lbl3.Name = "lbl3";
            this.lbl3.Size = new System.Drawing.Size(242, 12);
            this.lbl3.TabIndex = 2;
            this.lbl3.Text = "新規に入力する場合はキャンセルを押してください。";
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Location = new System.Drawing.Point(11, 28);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(428, 12);
            this.lbl2.TabIndex = 1;
            this.lbl2.Text = "これを表示させて実績入力する場合は、下のリストから表示させるデータを選択してください。";
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new System.Drawing.Point(11, 8);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(270, 12);
            this.lbl1.TabIndex = 0;
            this.lbl1.Text = "入力したデータに一致する生産スケジュールがありました。";
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dgv_dt);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.btn_cansel);
            this.splitContainer2.Panel2.Controls.Add(this.btn_no_select);
            this.splitContainer2.Panel2.Controls.Add(this.btn_sentaku);
            this.splitContainer2.Size = new System.Drawing.Size(536, 177);
            this.splitContainer2.SplitterDistance = 141;
            this.splitContainer2.TabIndex = 0;
            // 
            // dgv_dt
            // 
            this.dgv_dt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_dt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_dt.Location = new System.Drawing.Point(0, 0);
            this.dgv_dt.Name = "dgv_dt";
            this.dgv_dt.RowTemplate.Height = 21;
            this.dgv_dt.Size = new System.Drawing.Size(534, 139);
            this.dgv_dt.TabIndex = 0;
            this.dgv_dt.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_dt_CellMouseDoubleClick);
            // 
            // btn_cansel
            // 
            this.btn_cansel.Location = new System.Drawing.Point(448, 3);
            this.btn_cansel.Name = "btn_cansel";
            this.btn_cansel.Size = new System.Drawing.Size(75, 23);
            this.btn_cansel.TabIndex = 2;
            this.btn_cansel.Text = "戻る";
            this.btn_cansel.UseVisualStyleBackColor = true;
            this.btn_cansel.Click += new System.EventHandler(this.btn_cansel_Click);
            // 
            // btn_no_select
            // 
            this.btn_no_select.Location = new System.Drawing.Point(231, 3);
            this.btn_no_select.Name = "btn_no_select";
            this.btn_no_select.Size = new System.Drawing.Size(75, 23);
            this.btn_no_select.TabIndex = 1;
            this.btn_no_select.Text = "選択しない";
            this.btn_no_select.UseVisualStyleBackColor = true;
            this.btn_no_select.Click += new System.EventHandler(this.btn_no_select_Click);
            // 
            // btn_sentaku
            // 
            this.btn_sentaku.Location = new System.Drawing.Point(11, 3);
            this.btn_sentaku.Name = "btn_sentaku";
            this.btn_sentaku.Size = new System.Drawing.Size(75, 23);
            this.btn_sentaku.TabIndex = 0;
            this.btn_sentaku.Text = "選択";
            this.btn_sentaku.UseVisualStyleBackColor = true;
            this.btn_sentaku.Click += new System.EventHandler(this.btn_sentaku_Click);
            // 
            // frm_select_dt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGreen;
            this.ClientSize = new System.Drawing.Size(536, 289);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainer1);
            this.Name = "frm_select_dt";
            this.Text = "選択";
            this.Load += new System.EventHandler(this.frm_select_dt_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_dt)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label lbl4;
        private System.Windows.Forms.Label lbl3;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.DataGridView dgv_dt;
        private System.Windows.Forms.Button btn_cansel;
        private System.Windows.Forms.Button btn_no_select;
        private System.Windows.Forms.Button btn_sentaku;
        private System.Windows.Forms.Label lbl5;
    }
}