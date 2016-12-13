namespace TSS_SYSTEM
{
    partial class frm_oracle_sql_execute
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_oracle_sql_execute));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_hardcopy = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.tb_sql = new System.Windows.Forms.TextBox();
            this.btn_execute = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.dgv_result = new System.Windows.Forms.DataGridView();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.btn_csv = new System.Windows.Forms.Button();
            this.btn_syuuryou = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rb_select = new System.Windows.Forms.RadioButton();
            this.rb_update = new System.Windows.Forms.RadioButton();
            this.rb_delete = new System.Windows.Forms.RadioButton();
            this.splitContainer6 = new System.Windows.Forms.SplitContainer();
            this.lbl_result = new System.Windows.Forms.Label();
            this.rb_insert = new System.Windows.Forms.RadioButton();
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
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_result)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).BeginInit();
            this.splitContainer6.Panel1.SuspendLayout();
            this.splitContainer6.Panel2.SuspendLayout();
            this.splitContainer6.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.btn_hardcopy);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(884, 539);
            this.splitContainer1.SplitterDistance = 60;
            this.splitContainer1.TabIndex = 1;
            this.splitContainer1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(52, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(368, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "select,update,deleteをきちんと選択してから実行しないとエラーが発生します。";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(52, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(259, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "実行するSQLを入力して、実行ボタンを押してください。";
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
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
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
            this.splitContainer2.Size = new System.Drawing.Size(884, 475);
            this.splitContainer2.SplitterDistance = 99;
            this.splitContainer2.TabIndex = 0;
            this.splitContainer2.TabStop = false;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer3.IsSplitterFixed = true;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.tb_sql);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer3.Panel2.Controls.Add(this.btn_execute);
            this.splitContainer3.Panel2.Controls.Add(this.label1);
            this.splitContainer3.Size = new System.Drawing.Size(880, 95);
            this.splitContainer3.SplitterDistance = 25;
            this.splitContainer3.TabIndex = 2;
            this.splitContainer3.TabStop = false;
            // 
            // tb_sql
            // 
            this.tb_sql.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_sql.Location = new System.Drawing.Point(0, 0);
            this.tb_sql.Multiline = true;
            this.tb_sql.Name = "tb_sql";
            this.tb_sql.Size = new System.Drawing.Size(880, 25);
            this.tb_sql.TabIndex = 0;
            // 
            // btn_execute
            // 
            this.btn_execute.Location = new System.Drawing.Point(797, 41);
            this.btn_execute.Name = "btn_execute";
            this.btn_execute.Size = new System.Drawing.Size(75, 23);
            this.btn_execute.TabIndex = 1;
            this.btn_execute.Text = "Execute";
            this.btn_execute.UseVisualStyleBackColor = true;
            this.btn_execute.Click += new System.EventHandler(this.btn_execute_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(652, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please enter the SQL to be executed.   Errors will occur without selecting, updat" +
    "ing, or deleting properly and do not execute it.";
            // 
            // splitContainer4
            // 
            this.splitContainer4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer4.IsSplitterFixed = true;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.splitContainer6);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.splitContainer5);
            this.splitContainer4.Size = new System.Drawing.Size(884, 372);
            this.splitContainer4.SplitterDistance = 335;
            this.splitContainer4.TabIndex = 0;
            this.splitContainer4.TabStop = false;
            // 
            // dgv_result
            // 
            this.dgv_result.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_result.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_result.Location = new System.Drawing.Point(0, 0);
            this.dgv_result.Name = "dgv_result";
            this.dgv_result.RowTemplate.Height = 21;
            this.dgv_result.Size = new System.Drawing.Size(880, 302);
            this.dgv_result.TabIndex = 0;
            this.dgv_result.TabStop = false;
            // 
            // splitContainer5
            // 
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer5.IsSplitterFixed = true;
            this.splitContainer5.Location = new System.Drawing.Point(0, 0);
            this.splitContainer5.Name = "splitContainer5";
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.btn_csv);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.btn_syuuryou);
            this.splitContainer5.Size = new System.Drawing.Size(880, 29);
            this.splitContainer5.SplitterDistance = 790;
            this.splitContainer5.TabIndex = 0;
            // 
            // btn_csv
            // 
            this.btn_csv.Location = new System.Drawing.Point(10, 3);
            this.btn_csv.Name = "btn_csv";
            this.btn_csv.Size = new System.Drawing.Size(75, 23);
            this.btn_csv.TabIndex = 0;
            this.btn_csv.Text = "CSV出力";
            this.btn_csv.UseVisualStyleBackColor = true;
            this.btn_csv.Click += new System.EventHandler(this.btn_csv_Click);
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rb_insert);
            this.groupBox1.Controls.Add(this.rb_delete);
            this.groupBox1.Controls.Add(this.rb_update);
            this.groupBox1.Controls.Add(this.rb_select);
            this.groupBox1.Location = new System.Drawing.Point(10, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(252, 46);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select commands.";
            // 
            // rb_select
            // 
            this.rb_select.AutoSize = true;
            this.rb_select.Checked = true;
            this.rb_select.Location = new System.Drawing.Point(6, 18);
            this.rb_select.Name = "rb_select";
            this.rb_select.Size = new System.Drawing.Size(55, 16);
            this.rb_select.TabIndex = 0;
            this.rb_select.TabStop = true;
            this.rb_select.Text = "Select";
            this.rb_select.UseVisualStyleBackColor = true;
            // 
            // rb_update
            // 
            this.rb_update.AutoSize = true;
            this.rb_update.Location = new System.Drawing.Point(67, 18);
            this.rb_update.Name = "rb_update";
            this.rb_update.Size = new System.Drawing.Size(59, 16);
            this.rb_update.TabIndex = 1;
            this.rb_update.TabStop = true;
            this.rb_update.Text = "Update";
            this.rb_update.UseVisualStyleBackColor = true;
            // 
            // rb_delete
            // 
            this.rb_delete.AutoSize = true;
            this.rb_delete.Location = new System.Drawing.Point(190, 18);
            this.rb_delete.Name = "rb_delete";
            this.rb_delete.Size = new System.Drawing.Size(56, 16);
            this.rb_delete.TabIndex = 3;
            this.rb_delete.TabStop = true;
            this.rb_delete.Text = "Delete";
            this.rb_delete.UseVisualStyleBackColor = true;
            // 
            // splitContainer6
            // 
            this.splitContainer6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer6.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer6.IsSplitterFixed = true;
            this.splitContainer6.Location = new System.Drawing.Point(0, 0);
            this.splitContainer6.Name = "splitContainer6";
            this.splitContainer6.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer6.Panel1
            // 
            this.splitContainer6.Panel1.Controls.Add(this.dgv_result);
            // 
            // splitContainer6.Panel2
            // 
            this.splitContainer6.Panel2.Controls.Add(this.lbl_result);
            this.splitContainer6.Size = new System.Drawing.Size(880, 331);
            this.splitContainer6.SplitterDistance = 302;
            this.splitContainer6.TabIndex = 1;
            this.splitContainer6.TabStop = false;
            // 
            // lbl_result
            // 
            this.lbl_result.AutoSize = true;
            this.lbl_result.Location = new System.Drawing.Point(8, 6);
            this.lbl_result.Name = "lbl_result";
            this.lbl_result.Size = new System.Drawing.Size(82, 12);
            this.lbl_result.TabIndex = 0;
            this.lbl_result.Text = "Display results";
            // 
            // rb_insert
            // 
            this.rb_insert.AutoSize = true;
            this.rb_insert.Location = new System.Drawing.Point(132, 18);
            this.rb_insert.Name = "rb_insert";
            this.rb_insert.Size = new System.Drawing.Size(52, 16);
            this.rb_insert.TabIndex = 2;
            this.rb_insert.TabStop = true;
            this.rb_insert.Text = "Insert";
            this.rb_insert.UseVisualStyleBackColor = true;
            // 
            // frm_oracle_sql_execute
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "frm_oracle_sql_execute";
            this.Text = "Oracle SQL 実行";
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
            this.splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_result)).EndInit();
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
            this.splitContainer5.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer6.Panel1.ResumeLayout(false);
            this.splitContainer6.Panel2.ResumeLayout(false);
            this.splitContainer6.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).EndInit();
            this.splitContainer6.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btn_hardcopy;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_sql;
        private System.Windows.Forms.Button btn_execute;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.DataGridView dgv_result;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private System.Windows.Forms.Button btn_syuuryou;
        private System.Windows.Forms.Button btn_csv;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rb_delete;
        private System.Windows.Forms.RadioButton rb_update;
        private System.Windows.Forms.RadioButton rb_select;
        private System.Windows.Forms.SplitContainer splitContainer6;
        private System.Windows.Forms.Label lbl_result;
        private System.Windows.Forms.RadioButton rb_insert;
    }
}