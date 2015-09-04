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
    public partial class frm_nyukin : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        double w_nyukin_no;         //連番退避用
        //int w_seikyu_sime_dd;       //請求締日
        //int w_seikyuu_flg = 0;      //請求済レコードがあったら1
        
        public frm_nyukin()
        {
            InitializeComponent();
        }

        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_hardcopy_Click(object sender, EventArgs e)
        {
            tss.HardCopy();
        }

        private void frm_uriage_Load(object sender, EventArgs e)
        {
            w_nyukin_no = tss.GetSeq("09");
            nyukin_no_disp();
        }

        private void nyukin_no_disp()
        {
            tb_nyukin_no.Text = w_nyukin_no.ToString("0000000000");
            tb_nyukin_no.Focus();
        }

        private void tb_nyukin_date_Validating(object sender, CancelEventArgs e)
        {
            if (tb_nyukin_date.Text != "")
            {
                if (chk_nyukin_date())
                {
                    tb_nyukin_date.Text = tss.out_datetime.ToShortDateString();
                }
                else
                {
                    MessageBox.Show("売上計上日に異常があります。");
                    tb_nyukin_date.Focus();
                }
            }
        }

        private void tb_torihikisaki_cd_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_torihikisaki_cd.Text) == false)
            {
                e.Cancel = true;
                return;
            }
            //終了ボタンを考慮して、空白は許容する
            if (tb_torihikisaki_cd.Text != "")
            {
                //既存データの場合は、取引先コードの変更、再読み込みは不可
                if (tb_nyukin_no.Text.ToString() == w_nyukin_no.ToString("0000000000"))
                {
                    if (chk_torihikisaki_cd() != true)
                    {
                        MessageBox.Show("取引先コードに異常があります。");
                        e.Cancel = true;
                    }
                    else
                    {
                        //取引先名を取得・表示
                        tb_torihikisaki_name.Text = get_torihikisaki_name(tb_torihikisaki_cd.Text);
                        //chk_torihikisaki_simebi();
                    }
                }
            }
        }

        private string get_torihikisaki_name(string in_torihikisaki_cd)
        {
            string out_torihikisaki_name = "";  //戻り値用
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_torihikisaki_m where torihikisaki_cd = '" + in_torihikisaki_cd + "'");
            if (dt_work.Rows.Count <= 0)
            {
                out_torihikisaki_name = "";
            }
            else
            {
                out_torihikisaki_name = dt_work.Rows[0]["torihikisaki_name"].ToString();
            }
            return out_torihikisaki_name;
        }



        private bool chk_torihikisaki_cd()
        {
            bool bl = true; //戻り値
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_torihikisaki_m where torihikisaki_cd  = '" + tb_torihikisaki_cd.Text.ToString() + "'");
            if (dt_work.Rows.Count <= 0)
            {
                //無し
                bl = false;
            }
            else
            {
                //既存データ有
            }
            return bl;
        }

        private bool chk_nyukin_date()
        {
            bool bl = true; //戻り値
            if (tss.try_string_to_date(tb_nyukin_date.Text.ToString()) == false)
            {
                bl = false;
            }
            return bl;
        }
    }
}
