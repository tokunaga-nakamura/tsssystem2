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
    public partial class frm_siharai_no_select : Form
    {
       TssSystemLibrary tss = new TssSystemLibrary();
        DataTable dt_kousei_name = new DataTable();


        //親画面から参照できるプロパティを作成
        public string fld_kubun_name;                       //選択する区分名称コード
        public DataTable fld_dt_siharai_no = new DataTable();    //選択する区分テーブル
        public string fld_torihikisaki_cd;      
        public string fld_siharai_no;                         //選択された区分コード
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
        public DataTable ppt_dt_siharai_no
        {
            get
            {
                return fld_dt_siharai_no;
            }
            set
            {
                fld_dt_siharai_no = value;
            }
        }
        public string ppt_str_torihikisaki_cd
        {
            get
            {
                return fld_torihikisaki_cd;
            }
            set
            {
                fld_torihikisaki_cd = value;
            }
        }
        public string ppt_str_siharai_no
        {
            get
            {
                return fld_siharai_no;
            }
            set
            {
                fld_siharai_no = value;
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
        

        
        public frm_siharai_no_select()
        {
            InitializeComponent();
        }

        private void tb_seihin_cd_Validating(object sender, CancelEventArgs e)
        {
          

        }



        private void frm_seihin_kousei_select_Load(object sender, EventArgs e)
        {
           


        }

        private void btn_sentaku_Click(object sender, EventArgs e)
        {
            form_close_true();
        }
            
        
        //エラー及びキャンセル時の終了処理
        private void form_close_false()
        {
            ppt_str_siharai_no = "";
            ppt_bl_sentaku = false;
            this.Close();
        }

        //選択時の終了処理
        private void form_close_true()
        {
           ppt_str_torihikisaki_cd = tb_torihikisaki_cd.Text;

           ppt_str_siharai_no = dgv_kubun_m.CurrentRow.Cells[0].Value.ToString();
         
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

        private void tb_torihikisaki_cd_Validating(object sender, CancelEventArgs e)
        {
            tb_torihikisaki_name.Text = tss.get_torihikisaki_name(tb_torihikisaki_cd.Text);
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select seihin_kousei_no,seihin_kousei_name from tss_seihin_kousei_name_m where seihin_cd  = '" + tb_torihikisaki_cd.Text.ToString() + "' ORDER BY seihin_kousei_no");

            dgv_kubun_m.DataSource = dt_work;
            dgv_kubun_m.Columns[0].HeaderText = "製品構成番号";
            dgv_kubun_m.Columns[1].HeaderText = "製品構成名称";
        }

        private void frm_siharai_no_select_Load(object sender, EventArgs e)
        {
            tb_torihikisaki_cd.Text = ppt_str_torihikisaki_cd;
            tb_torihikisaki_cd.Focus();


            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select siharai_no,siharai_date from tss_siharai_m where torihikisaki_cd  = '" + tb_torihikisaki_cd.Text.ToString() + "' ORDER BY siharai_no");

            dgv_kubun_m.DataSource = dt_work;

            dgv_kubun_m.Columns[0].HeaderText = "支払番号";
            dgv_kubun_m.Columns[1].HeaderText = "支払計上日";
        }

        private void tb_torihikisaki_cd_TextChanged(object sender, EventArgs e)
        {
            tb_torihikisaki_name.Text = tss.get_torihikisaki_name(tb_torihikisaki_cd.Text);
        }

        private void btn_sentaku_Click_1(object sender, EventArgs e)
        {
            form_close_true();
        }

        private void btn_cancel_Click_1(object sender, EventArgs e)
        {
            this.Close();
            //form_close_false();
        }
    }
        
        
    
}
