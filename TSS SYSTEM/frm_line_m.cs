using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TSS_SYSTEM
{
    public partial class frm_line_m : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable dt = new DataTable();

        public frm_line_m()
        {
            InitializeComponent();
        }

        private void btn_hardcopy_Click(object sender, EventArgs e)
        {
            tss.HardCopy();
        }

        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void line_m_disp()
        {
            dt = tss.OracleSelect("select line_cd,line_name,line_ryakusiki_name,bikou,delete_flg from tss_line_m order by line_cd asc");
            dgv_line_m.DataSource = null;
            dgv_line_m.DataSource = dt;
            if(dt.Rows.Count >= 1)
            {
                dgv_line_m.Columns[0].HeaderText = "ラインコード";
                dgv_line_m.Columns[1].HeaderText = "ライン名称";
                dgv_line_m.Columns[2].HeaderText = "ライン略式名称";
                dgv_line_m.Columns[3].HeaderText = "備考";
                dgv_line_m.Columns[4].HeaderText = "削除フラグ";
                dgv_line_m.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;   //カラム幅の自動調整
                dgv_line_m.AllowUserToResizeRows = false;    //セルの高さ変更不可
                dgv_line_m.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;    //カラムヘッダーの高さ変更不可
                dgv_line_m.ReadOnly = true; //編集不可
            }

        }

        private void frm_line_m_Load(object sender, EventArgs e)
        {
            line_m_disp();
        }
    }
}
