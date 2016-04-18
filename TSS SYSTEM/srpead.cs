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
    public partial class srpead : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable dt_m = new DataTable();
        
        
        
        public srpead()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //fpSpread1.OpenExcel("c:\\work\\dai.xlsx");
            //fpSpread2.OpenExcel("c:\\work\\kousu.xlsx");
            
            //fpSpread1.OpenExcel("c:\\work\\test.xlsx");
            //fpSpread1.OpenExcel("c:\\excelfile.xls", 2);
            //fpSpread1.ActiveSheetView.PageSize = FpSpread1.Rows.Count;

            //fpSpread1.ActiveSheet.OpenExcel("c:¥work\test.xlsx", "Sheet2");


            //dt_m = tss.OracleSelect("select * from tss_getumatu_zaiko_m");

            //fpSpread1.ActiveSheet.DataSource = dt_m;

            //dataGridView1.DataSource = dt_m;

            //DataGridViewのContextMenuStripを設定する
            //dataGridView1.ContextMenuStrip = this.contextMenuStrip1;

            ////列のContextMenuStripを設定する
            //dataGridView1.Columns[0].ContextMenuStrip = this.ContextMenuStrip2;
            ////列ヘッダーのContextMenuStripを設定する
            //dataGridView1.Columns[0].HeaderCell.ContextMenuStrip = this.ContextMenuStrip2;

            ////行のContextMenuStripを設定する
            //dataGridView1.Rows[0].ContextMenuStrip = this.ContextMenuStrip3;

            ////セルのContextMenuStripを設定する
            //dataGridView1[0, 1].ContextMenuStrip = this.ContextMenuStrip4;



            //dataGridView1.AllowUserToOrderColumns = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //spread2
            spread2 frm_sp2 = new spread2();
            frm_sp2.ShowDialog(this);
            frm_sp2.Dispose();
        }
    }
}
