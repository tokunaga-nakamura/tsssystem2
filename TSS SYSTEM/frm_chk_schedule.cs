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
    public partial class frm_chk_schedule : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable dt_m = new DataTable();

        public string str_mode; //画面モード
        public string str_cd;   //選択されたコード
        public bool bl_sentaku; //選択フラグ 選択:true エラーまたはキャンセル:false
        
        public frm_chk_schedule()
        {
            InitializeComponent();
        }

        private void btn_kensaku_Click(object sender, EventArgs e)
        {
            kensaku();
        }

        private void kensaku()
        {
            DataTable dt_kensaku = new DataTable();
            string[] sql_where = new string[7];
            int sql_cnt = 0;
            
            //部署コード
            if (cb_busyo.Text != "")
            {
                if (chk_busyo_cd(cb_busyo.Text))
                {
                    sql_where[sql_cnt] = "torihikisaki_cd = '" + cb_busyo.Text.ToString() + "'";
                    sql_cnt++;
                }
                else
                {
                    //コード異常
                    MessageBox.Show("取引先コードに異常があります。");
                    cb_busyo.Focus();
                    return;
                }
            }
            //生産予定計上日
            if (tb_seisan_yotei1.Text != "" && tb_seisan_yotei2.Text != "")
            {
                if (tb_seisan_yotei1.Text != "")
                {
                    if (chk_seisan_yotei_date(tb_seisan_yotei1.Text))
                    {
                        tb_seisan_yotei1.Text = tss.out_datetime.ToShortDateString();
                    }
                    else
                    {
                        MessageBox.Show("生産予定日に異常があります。");
                        tb_seisan_yotei1.Focus();
                    }
                }
                if (tb_seisan_yotei2.Text != "")
                {
                    if (chk_seisan_yotei_date(tb_seisan_yotei2.Text))
                    {
                        tb_seisan_yotei2.Text = tss.out_datetime.ToShortDateString();
                    }
                    else
                    {
                        MessageBox.Show("生産予定日に異常があります。");
                        tb_seisan_yotei2.Focus();
                    }
                }
                int w_int_hikaku = string.Compare(tb_seisan_yotei1.Text, tb_seisan_yotei2.Text);
                if (w_int_hikaku == 0)
                {
                    //左右同じコード
                    sql_where[sql_cnt] = "seisan_yotei_date = TO_DATE('" + tb_seisan_yotei1.Text.ToString() + "','YYYY/MM/DD')";
                    sql_cnt++;
                }
                else
                    if (w_int_hikaku < 0)
                    {
                        //左辺＜右辺
                        sql_where[sql_cnt] = "seisan_yotei_date >= to_date('" + tb_seisan_yotei1.Text.ToString() + "','YYYY/MM/DD') and seisan_yotei_date <= to_date('" + tb_seisan_yotei2.Text.ToString() + "','YYYY/MM/DD')";
                        sql_cnt++;
                    }
                    else
                        if (w_int_hikaku > 0)
                        {
                            //左辺＞右辺
                            sql_where[sql_cnt] = "seisan_yotei_date >= to_date('" + tb_seisan_yotei2.Text.ToString() + "','YYYY/MM/dd') and siire_date <= to_date('" + tb_seisan_yotei1.Text.ToString() + "','YYYY/MM/dd')";
                            sql_cnt++;
                        }
            }

            //検索条件が全て空白
            if (sql_cnt == 0)
            {
                MessageBox.Show("検索条件を指定してください。");
                tb_siire_no1.Focus();
                return;
            }

            string sql = "select a1.torihikisaki_cd,a1.juchu_cd1,a1.juchu_cd2,a1.seihin_cd,b1.seihin_name,c1.koutei_cd,d1.koutei_name,e1.nouhin_yotei_date,e1.nouhin_yotei_su from tss_juchu_m a1,tss_seihin_m b1,tss_seisan_koutei_m c1,tss_koutei_m d1,tss_nouhin_schedule_m e1 where a1.seihin_cd  = b1.seihin_cd AND a1.seihin_cd = c1.seihin_cd AND c1.koutei_cd = d1.koutei_cd AND a1.torihikisaki_cd = e1.torihikisaki_cd AND a1.juchu_cd1 = e1.juchu_cd1 AND a1.juchu_cd2 = e1.juchu_cd2";
            //for (int i = 1; i <= sql_cnt; i++)
            //{
            //    if (i >= 2)
            //    {
            //        sql = sql + " and ";
            //    }
            //    sql = sql + sql_where[i - 1];
            //}

            //sql = sql + " ORDER BY SIIRE_NO";

            dt_kensaku = tss.OracleSelect(sql);
            list_disp(dt_kensaku);
        }

        private bool chk_busyo_cd(string in_busyo_cd)
        {
            bool bl = true; //戻り値
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_busyo_m where usyo_cd  = '" + in_busyo_cd.ToString() + "'");
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

        private bool chk_seisan_yotei_date(string in_str)
        {
            bool bl = true; //戻り値
            if (tss.try_string_to_date(in_str) == false)
            {
                bl = false;
            }
            return bl;
        }

        private void list_disp(DataTable in_dt)
        {
            //リードオンリーにする
            dgv_m.ReadOnly = true;
            //行ヘッダーを非表示にする
            dgv_m.RowHeadersVisible = false;
            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            dgv_m.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //セルの高さ変更不可
            dgv_m.AllowUserToResizeRows = false;
            //カラムヘッダーの高さ変更不可
            dgv_m.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //削除不可にする（コードからは削除可）
            dgv_m.AllowUserToDeleteRows = false;
            //１行のみ選択可能（複数行の選択不可）
            dgv_m.MultiSelect = false;
            //セルを選択すると行全体が選択されるようにする
            dgv_m.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //DataGridView1にユーザーが新しい行を追加できないようにする
            dgv_m.AllowUserToAddRows = false;

            dgv_m.DataSource = null;
            dgv_m.DataSource = in_dt;
            dt_m = in_dt;
            //DataGridViewのカラムヘッダーテキストを変更する
            //dgv_m.Columns[0].HeaderText = "仕入番号";
            //dgv_m.Columns[1].HeaderText = "取引先コード";
            //dgv_m.Columns[2].HeaderText = "仕入計上日";
            //dgv_m.Columns[3].HeaderText = "部品コード";
            //dgv_m.Columns[4].HeaderText = "部品名";
            //dgv_m.Columns[5].HeaderText = "仕入数量";
            //dgv_m.Columns[6].HeaderText = "仕入単価";
            //dgv_m.Columns[7].HeaderText = "仕入金額";
            //dgv_m.Columns[8].HeaderText = "仕入伝票番号";
            //dgv_m.Columns[9].HeaderText = "仕入締日";
            //dgv_m.Columns[10].HeaderText = "支払計上日";
            //dgv_m.Columns[11].HeaderText = "備考";
        }

    }
}
