//  SYSTEM NAME     TSS SYSTEM
//  PROGRAM NAME    入金一覧
//  CREATE          J.OKUDA
//  UPDATE LOG
//  xxxx/xx/xx  NAMExxxx    NAIYOU

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
    public partial class frm_nyukin_ichiran : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable dt_m = new DataTable();
        DataTable w_dt_insatu = new DataTable();
        
        
        public frm_nyukin_ichiran()
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

        private void btn_hyouji_Click(object sender, EventArgs e)
        {
            kensaku();
        }

        private void kensaku()
        {
            DataTable dt_kensaku = new DataTable();
            string[] sql_where = new string[7];
            int sql_cnt = 0;

            //入金日
            int w_int_hikaku = string.Compare(dtp_nyukin_date1.Value.ToShortDateString(), dtp_nyukin_date2.Value.ToShortDateString());
                if (w_int_hikaku == 0)
                {
                    //左右同じコード
                    sql_where[sql_cnt] = "TO_CHAR(nyukin_date, 'YYYY/MM/DD') = '" + dtp_nyukin_date1.Value.ToShortDateString() + "'";
                    sql_cnt++;
                }
                else
                    if (w_int_hikaku < 0)
                    {
                        //左辺＜右辺
                        sql_where[sql_cnt] = "to_char(nyukin_date,'yyyy/mm/dd') >= '" + dtp_nyukin_date1.Value.ToShortDateString() + "' and to_char(nyukin_date,'yyyy/mm/dd') <= '" + dtp_nyukin_date2.Value.ToShortDateString() + "'";
                        sql_cnt++;
                    }
                    else
                        if (w_int_hikaku > 0)
                        {
                            //左辺＞右辺
                            sql_where[sql_cnt] = "to_char(nyukin_date,'yyyy/mm/dd') >= '" + dtp_nyukin_date2.Value.ToShortDateString() + "' and to_char(nyukin_date,'yyyy/mm/dd') <= '" + dtp_nyukin_date1.Value.ToShortDateString() + "'";
                            sql_cnt++;
                        }
           

            //取引先コード
            if (tb_torihikisaki_cd1.Text != "" || tb_torihikisaki_cd2.Text != "")
            {
                w_int_hikaku = string.Compare(tb_torihikisaki_cd1.Text, tb_torihikisaki_cd2.Text);
                if (w_int_hikaku == 0)
                {
                    //左右同じコード
                    sql_where[sql_cnt] = "torihikisaki_cd = '" + tb_torihikisaki_cd1.Text.ToString() + "'";
                    sql_cnt++;
                }
                else
                    if (w_int_hikaku < 0)
                    {
                        //左辺＜右辺
                        sql_where[sql_cnt] = "torihikisaki_cd >= '" + tb_torihikisaki_cd1.Text.ToString() + "' and torihikisaki_cd <= '" + tb_torihikisaki_cd2.Text.ToString() + "'";
                        sql_cnt++;
                    }
                    else
                        if (w_int_hikaku > 0)
                        {
                            //左辺＞右辺
                            sql_where[sql_cnt] = "torihikisaki_cd >= '" + tb_torihikisaki_cd2.Text.ToString() + "' and torihikisaki_cd <= '" + tb_torihikisaki_cd1.Text.ToString() + "'";
                            sql_cnt++;
                        }
            }


            //検索条件が全て空白
            if (sql_cnt == 0)
            {
                //MessageBox.Show("検索条件を指定してください。");
                //dtp_siire_simebi.Focus();
                //return;
            }

            string sql = "select torihikisaki_cd,nyukin_kbn,nyukingaku,nyukin_date,bikou from tss_nyukin_m where ";


            for (int i = 1; i <= sql_cnt; i++)
            {
                if (i >= 2)
                {
                    sql = sql + " and ";
                }
                sql = sql + sql_where[i - 1];
            }

            sql = sql + " order by nyukin_date,torihikisaki_cd,nyukin_kbn ";


            dt_kensaku = tss.OracleSelect(sql);

            if (dt_kensaku.Rows.Count == 0)
            {
                MessageBox.Show("指定した条件のデータがありません");
                return;
            }
            else
            {
                int rc = dt_kensaku.Rows.Count;

                dt_kensaku.Columns.Add("Torihikisaki_name", Type.GetType("System.String")).SetOrdinal(1);
                dt_kensaku.Columns.Add("nyukin_kbn_name", Type.GetType("System.String")).SetOrdinal(3);
               

                for (int i = 0; i <= rc - 1; i++)
                {
                    //取引先名
                    DataTable dt_tori = new DataTable();
                    dt_tori = tss.OracleSelect("select * from tss_torihikisaki_m where torihikisaki_cd = '" + dt_kensaku.Rows[i][0].ToString() + "'");
                    dt_kensaku.Rows[i][1] = dt_tori.Rows[0][1].ToString();

                    //入金区分
                    DataTable dt_work = new DataTable();
                    dt_work = tss.OracleSelect("select kubun_cd,kubun_name from tss_kubun_m where kubun_meisyou_cd = '12' and kubun_cd = '" + dt_kensaku.Rows[i][2].ToString() + "'");
                    dt_kensaku.Rows[i][3] = dt_work.Rows[0][1].ToString();
                }
            }


            list_disp(dt_kensaku);

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
            dgv_m.Columns[0].HeaderText = "取引先コード";
            dgv_m.Columns[1].HeaderText = "取引先名";
            dgv_m.Columns[2].HeaderText = "区分コード";
            dgv_m.Columns[3].HeaderText = "区分名称";
            dgv_m.Columns[4].HeaderText = "入金額";
            dgv_m.Columns[5].HeaderText = "入金日";
            dgv_m.Columns[6].HeaderText = "備考";


            //DataGridViewの書式設定
            dgv_m.Columns[4].DefaultCellStyle.Format = "#,0";
           

            dgv_m.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


            w_dt_insatu = dt_m;
        }

        private void btn_csv_Click(object sender, EventArgs e)
        {
            if (dt_m.Rows.Count != 0)
            {
                string w_str_now = DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");
                string w_str_filename = "入金一覧" + w_str_now + ".csv";
                if (tss.DataTableCSV(dt_m, true, w_str_filename, "\"", true))
                {
                    MessageBox.Show("保存されました。");
                }
                else
                {
                    //MessageBox.Show("キャンセルまたはエラー");
                }
            }
            else
            {
                MessageBox.Show("出力するデータがありません。");
            }
        }

        private void btn_insatu_Click(object sender, EventArgs e)
        {

            frm_nyukin_ichiran_preview frm_rpt = new frm_nyukin_ichiran_preview();

            //子画面のプロパティに値をセットする
            frm_rpt.ppt_dt = w_dt_insatu;

            frm_rpt.w_hd01 = dtp_nyukin_date1.Text;
            frm_rpt.w_hd02 = dtp_nyukin_date2.Text;

            if (tb_torihikisaki_cd1.Text.ToString() == "")
            {
                frm_rpt.w_hd11 = "指定なし";
            }

            if (tb_torihikisaki_cd2.Text.ToString() == "")
            {
                frm_rpt.w_hd20 = "指定なし";
            }

            else
            {
                frm_rpt.w_hd11 = tb_torihikisaki_cd1.Text;
                frm_rpt.w_hd20 = tb_torihikisaki_cd2.Text;
            }

            frm_rpt.ShowDialog();
            //子画面から値を取得する
            frm_rpt.Dispose();
        }

        private void frm_nyukin_ichiran_Load(object sender, EventArgs e)
        {
            //月初の日付をデフォルトとする
            double day = DateTime.Now.Day;
            
            dtp_nyukin_date1.Value = DateTime.Now.AddDays(-day + 1); ;
 　　　　　 dtp_nyukin_date1.Checked = false;
         }

        
    }
}
