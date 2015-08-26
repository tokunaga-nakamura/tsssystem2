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
    public partial class frm_seihin_kousei_select : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable dt_kousei_name = new DataTable();


        //親画面から参照できるプロパティを作成
        public string fld_kubun_name;                       //選択する区分名称コード
        public DataTable fld_dt_seihin_kousei_name = new DataTable();    //選択する区分テーブル
        public string fld_seihin_cd;      
        public string fld_seihin_kousei_no;                         //選択された区分コード
        public bool fld_kubun_sentaku;                      //区分選択フラグ 選択:true エラーまたはキャンセル:false
       
        public string ppt_str_kubun_name
        {
            get
            {
                return fld_kubun_name;
            }
            set
            {
                fld_kubun_name = value;
            }
        }
        public DataTable ppt_dt_seihin_kousei_name
        {
            get
            {
                return fld_dt_seihin_kousei_name;
            }
            set
            {
                fld_dt_seihin_kousei_name = value;
            }
        }
        public string ppt_str_seihin_cd
        {
            get
            {
                return fld_seihin_cd;
            }
            set
            {
                fld_seihin_cd = value;
            }
        }
        public string ppt_str_seihin_kousei_no
        {
            get
            {
                return fld_seihin_kousei_no;
            }
            set
            {
                fld_seihin_kousei_no = value;
            }
        }
        public bool ppt_bl_sentaku
        {
            get
            {
                return fld_kubun_sentaku;
            }
            set
            {
                fld_kubun_sentaku = value;
            }
        }
        

        
        public frm_seihin_kousei_select()
        {
            InitializeComponent();
        }

        private void tb_seihin_cd_Validating(object sender, CancelEventArgs e)
        {
            tb_seihin_name.Text = get_seihin_name(tb_seihin_cd.Text);
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select seihin_kousei_no,seihin_kousei_name from tss_seihin_kousei_name_m where seihin_cd  = '" + tb_seihin_cd.Text.ToString() + "' ORDER BY seihin_kousei_no");

            dgv_kubun_m.DataSource = dt_work;
            dgv_kubun_m.Columns[0].HeaderText = "製品構成番号";
            dgv_kubun_m.Columns[1].HeaderText = "製品構成名称";

        }

        private string get_seihin_name(string in_cd)
        {
            string out_name = "";  //戻り値用
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_seihin_m where seihin_cd = '" + in_cd + "'");
            if (dt_work.Rows.Count <= 0)
            {
                out_name = "";
            }
            else
            {
                out_name = dt_work.Rows[0]["seihin_name"].ToString();
            }
            return out_name;
        }

        private bool chk_seihin_cd()
        {
            bool bl = true; //戻り値
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_seihin_m where seihin_cd  = '" + tb_seihin_cd.Text.ToString() + "'");
            if (dt_work.Rows.Count <= 0)
            {
                //無し
                bl = false;
            }
            else
            {
                //既存データ有
                tb_seihin_name.Text = get_seihin_name(tb_seihin_cd.Text);
            }
            return bl;
        }

        private void frm_seihin_kousei_select_Load(object sender, EventArgs e)
        {
            tb_seihin_cd.Text = ppt_str_seihin_cd;
            tb_seihin_cd.Focus();

            chk_seihin_cd();
            
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select seihin_kousei_no,seihin_kousei_name from tss_seihin_kousei_name_m where seihin_cd  = '" + tb_seihin_cd.Text.ToString() + "' ORDER BY seihin_kousei_no");

            dgv_kubun_m.DataSource = dt_work;

            dgv_kubun_m.Columns[0].HeaderText = "製品構成番号";
            dgv_kubun_m.Columns[1].HeaderText = "製品構成名称";


        }

        private void btn_sentaku_Click(object sender, EventArgs e)
        {
            form_close_true();
        }
            
        
        //エラー及びキャンセル時の終了処理
        private void form_close_false()
        {
            ppt_str_seihin_kousei_no = "";
            ppt_bl_sentaku = false;
            this.Close();
        }

        //選択時の終了処理
        private void form_close_true()
        {
            ppt_str_seihin_cd = tb_seihin_cd.Text;

            if (dgv_kubun_m.Rows.Count == 1)
            {
                ppt_str_seihin_kousei_no = "01";
            }
            else
            {
                ppt_str_seihin_kousei_no = dgv_kubun_m.CurrentRow.Cells[0].Value.ToString();
            }
            ppt_bl_sentaku = false;
            this.Close();
        }

        private void dgv_kubun_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            form_close_true();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        }



    
}
