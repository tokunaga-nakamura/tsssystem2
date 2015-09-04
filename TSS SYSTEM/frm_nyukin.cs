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

        private void btn_syuuryou_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_hardcopy_Click_1(object sender, EventArgs e)
        {
            tss.HardCopy();
        }

        private void frm_nyukin_Load(object sender, EventArgs e)
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

        private void tb_torihikisaki_cd_DoubleClick(object sender, EventArgs e)
        {
            //選択画面へ
            string w_cd;
            w_cd = tss.search_torihikisaki("2", "");
            if (w_cd != "")
            {
                tb_torihikisaki_cd.Text = w_cd;
                tb_torihikisaki_name.Text = get_torihikisaki_name(tb_torihikisaki_cd.Text);
                tb_nyukin_date.Focus();
            }
        }

        private void tb_torihikisaki_cd_Validating_1(object sender, CancelEventArgs e)
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

        private void tb_nyukin_no_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_nyukin_no.Text) == false)
            {
                e.Cancel = true;
                return;
            }

            //入力された売上番号を"0000000000"形式の文字列に変換
            double w_double;

            if (double.TryParse(tb_nyukin_no.Text.ToString(), out w_double))
            {
                //nyukin_no_disp();
                tb_nyukin_no.Text = w_double.ToString("0000000000");
            }
            else
            {
                MessageBox.Show("入金番号に異常があります。");
                tb_nyukin_no.Focus();
            }
            //新規か既存かの判定
            if (tb_nyukin_no.Text.ToString() == w_nyukin_no.ToString("0000000000"))
            {
                //新規
                //dgvに空のデータを表示するためのダミー抽出
                //DataTable dt_work = new DataTable();
                //dt_work = tss.OracleSelect("select * from tss_uriage_m where uriage_no = '" + tb_siire_no.Text.ToString() + "' order by uriage_no asc,seq asc");
                ////uriage_sinki(w_dt);
            }
            else
            {
                //既存仕入の表示
                DataTable dt_work = new DataTable();
                dt_work = tss.OracleSelect("select * from tss_nyukin_m where nyukin_no = '" + tb_nyukin_no.Text.ToString() + "' ORDER BY SEQ");
                int rc = dt_work.Rows.Count;

                if (dt_work.Rows.Count == 0)
                {
                    MessageBox.Show("データがありません。");

                    dt_work = null;
                    dgv_m.DataSource = dt_work;

                    ////dgv_siire.Rows.Clear();
                    //tb_torihikisaki_cd.Clear();
                    //tb_torihikisaki_name.Clear();
                    //dtp_siire_date.Value = DateTime.Today;
                    //tb_siire_denpyou_no.Clear();
                    //tb_create_user_cd.Clear();
                    //tb_create_datetime.Clear();
                    //tb_update_user_cd.Clear();
                    //tb_update_datetime.Clear();
                    tb_nyukin_no.Text = w_nyukin_no.ToString("0000000000");
                    ////tb_siire_no.Focus();
                    return;
                }

                else
                {
                    //dgv_siire.Rows.Clear();
                    
                    tb_torihikisaki_cd.Text = dt_work.Rows[0][2].ToString();
                    tb_torihikisaki_name.Text = tss.get_torihikisaki_name(tb_torihikisaki_cd.Text);
                    tb_nyukin_date.Text = dt_work.Rows[0][4].ToString();
                   
                    tb_create_user_cd.Text = dt_work.Rows[0][7].ToString();
                    tb_create_datetime.Text = dt_work.Rows[0][8].ToString();

                    tb_update_user_cd.Text = dt_work.Rows[0][9].ToString();
                    tb_update_datetime.Text = dt_work.Rows[0][10].ToString();


                    dt_work.Columns.Remove("torihikisaki_cd");
                    dt_work.Columns.Remove("nyukin_no");
                    dt_work.Columns.Remove("seq");
                    dt_work.Columns.Remove("nyukin_date");
                    //dt_work.Columns.Remove("delete_flg");
                    //dt_work.Columns.Remove("siire_denpyo_no");
                    dt_work.Columns.Remove("create_user_cd");
                    dt_work.Columns.Remove("create_datetime");
                    dt_work.Columns.Remove("update_user_cd");
                    dt_work.Columns.Remove("update_datetime");

                    dgv_m.DataSource = dt_work;
                    //dgv_nyukin_disp();
                }

            }
            
        }

        private void nyukin_sinki(DataTable in_dt)
        {
            //画面の項目をクリア
            tb_torihikisaki_cd.Text = "";
            tb_torihikisaki_name.Text = "";
            tb_nyukin_date.Text = "";
            //dgvをクリア
            dgv_m.DataSource = null;
            dgv_m.Columns.Clear();
            dgv_m.Rows.Clear();

            //dgvにデータをバインド
            dgv_m.DataSource = in_dt;

            //dgvの表示設定
            nyukin_init();

            //合計を表示
            nyukin_goukei_disp();
        }

        private void nyukin_disp(DataTable in_dt)
        {
            //dgvをクリア
            dgv_m.DataSource = null;
            dgv_m.Columns.Clear();
            dgv_m.Rows.Clear();

            //画面の見出し項目を表示
            tb_torihikisaki_cd.Text = in_dt.Rows[0]["torihikisaki_cd"].ToString();
            tb_torihikisaki_name.Text = tss.get_torihikisaki_name(in_dt.Rows[0]["torihikisaki_cd"].ToString());
            tb_nyukin_date.Text = DateTime.Parse(in_dt.Rows[0]["uriage_date"].ToString()).ToShortDateString();

            //dgvにデータをバインド
            dgv_m.DataSource = in_dt;


            //dgvの表示設定
            nyukin_init();

            //合計を表示
            nyukin_goukei_disp();


        }

        private void nyukin_init()
        {
           
        }

        private void nyukin_goukei_disp()
        {
            double w_dou;
            double w_uriage_goukei = 0;
            for (int i = 0; i < dgv_m.Rows.Count - 1; i++)
            {
                if (double.TryParse(dgv_m.Rows[i].Cells["uriage_kingaku"].Value.ToString(), out w_dou))
                {
                    w_uriage_goukei = w_uriage_goukei + w_dou;
                }
            }
            tb_nyukin_goukei.Text = w_uriage_goukei.ToString("#,###,###,##0");
        }

        private void tb_nyukin_date_Validating_1(object sender, CancelEventArgs e)
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

        private void btn_touroku_Click(object sender, EventArgs e)
        {
             DataTable dt_work = new DataTable();

            //登録前に全ての項目をチェック
            //取引先コード
            if (chk_torihikisaki_cd() == false)
            {
                MessageBox.Show("取引先コードは6桁の数字で入力してください（空白不可）");
                tb_torihikisaki_cd.Focus();
                return;
            }

            //データグリッドビューの中を1行ずつループしてチェック
            int dgvrc = dgv_m.Rows.Count;

            if (dgvrc == 1)
            {
                MessageBox.Show("表の中に何も入力されていません");
                return;
            }
            
            tss.GetUser();  //ユーザー情報の取得

            //データグリッドビューの入力内容チェック
            for (int i = 0; i < dgvrc - 1; i++)
            {
                if (dgv_m.Rows[i].Cells[0].Value == null)
                {
                    MessageBox.Show("部品コードを入力してください");
                    dgv_m.Focus();
                    dgv_m.CurrentCell = dgv_m[0, i];
                    return;
                }

                if (dgv_m.Rows[i].Cells[2].Value == null)
                {
                    MessageBox.Show("入金額を入力してください");
                    return;
                }
                
                //備考が空白の場合、""を代入  空欄だとnull扱いされ、SQエラー回避
                if (dgv_m.Rows[i].Cells[3].Value == null)
                {
                    dgv_m.Rows[i].Cells[3].Value = "";
                }
            }

            //入金番号の重複チェック
            dt_work = tss.OracleSelect("select * from tss_nyukin_m where nyukin_no  =  '" + tb_nyukin_no.ToString() + "'");
            if (dt_work.Rows.Count == 0)
            {
                //レコードの行数分ループしてインサート

                int dgvrc2 = dgv_m.Rows.Count;

                for (int i = 0; i < dgvrc2 - 1; i++)
                {
                    bool bl6 = tss.OracleInsert("INSERT INTO tss_nyukin_m (nyukin_no,seq,torihikisaki_cd,nyukin_kbn,nyukin_date,nyukingaku,bikou,create_user_cd,create_datetime) VALUES ('"
                                       + tb_nyukin_no.Text.ToString() + "','"
                                       + (i + 1) + "','"
                                       + tb_torihikisaki_cd.Text.ToString() + "','"
                                       + dgv_m.Rows[i].Cells[0].Value.ToString() + "','"
                                       + tb_nyukin_date.Text.ToString() + "','"
                                       + double.Parse(dgv_m.Rows[i].Cells[2].Value.ToString()) + "','"
                                       + dgv_m.Rows[i].Cells[3].Value.ToString() + "','"
                                       + tss.user_cd + "',SYSDATE)");

                    if (bl6 != true)
                    {
                        tss.ErrorLogWrite(tss.user_cd, "入金／登録", "登録ボタン押下時のOracleInsert");
                        MessageBox.Show("入金処理でエラーが発生しました。" + Environment.NewLine + "処理を中止します。");
                        this.Close();
                    }
                }

                tb_create_user_cd.Text = tss.user_cd;
                tb_create_datetime.Text = DateTime.Now.ToString();
                MessageBox.Show("入金処理が完了しました");
            }

            //売掛マスタの更新
            dt_work = tss.OracleSelect("select * from tss_urikake_m where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "'and nyukin_kanryou_flg = '0'  ORDER BY uriage_simebi");
            if (dt_work.Rows.Count == 0)
            {


            }

            else
            {
                int rc = dt_work.Rows.Count;

                double nyukingaku = double.Parse(dt_work.Rows[rc-1][5].ToString()) + double.Parse(tb_nyukin_goukei.Text.ToString());
                double kounyukingaku = double.Parse(dt_work.Rows[rc-1][3].ToString()) + double.Parse(dt_work.Rows[rc-1][4].ToString());

               if(kounyukingaku > nyukingaku)
               {
                   tss.OracleUpdate("UPDATE TSS_urikake_m SET nyukingaku ='" + nyukingaku + "',UPDATE_USER_CD = '" + tss.user_cd + "',UPDATE_DATETIME = SYSDATE WHERE torihikisaki_cd = '" + tb_torihikisaki_cd.Text.ToString() + "'and uriage_simebi = "
                   + "to_date('" + dt_work.Rows[0][1].ToString() + "','YYYY/MM/DD HH24:MI:SS')");

               }

               if (kounyukingaku == nyukingaku)
               {
                   tss.OracleUpdate("UPDATE TSS_urikake_m SET nyukingaku ='" + nyukingaku + "',nyukin_kanryou_flg = '1',UPDATE_USER_CD = '" + tss.user_cd + "',UPDATE_DATETIME = SYSDATE WHERE torihikisaki_cd = '" + tb_torihikisaki_cd.Text.ToString() + "'and uriage_simebi = "
                   + "to_date('" + dt_work.Rows[0][1].ToString() + "','YYYY/MM/DD HH24:MI:SS')");

               }
                
                if (kounyukingaku < nyukingaku)
                {
                    double nyuukin_amari = nyukingaku - kounyukingaku;
                    
                    tss.OracleUpdate("UPDATE TSS_urikake_m SET nyukingaku ='" + kounyukingaku + "',nyukin_kanryou_flg = '1',UPDATE_USER_CD = '" + tss.user_cd + "',UPDATE_DATETIME = SYSDATE WHERE torihikisaki_cd = '" + tb_torihikisaki_cd.Text.ToString() + "'and uriage_simebi = "
                    + "to_date('" + dt_work.Rows[0][1].ToString() + "','YYYY/MM/DD HH24:MI:SS')");

                    MessageBox.Show(nyuukin_amari.ToString());


                }
                
                
                
                
                
                MessageBox.Show("売掛マスタを更新しました");

            }


                
        }

        private void dgv_m_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
          


        }

        private void dgv_m_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //int i = e.RowIndex;
            int dgvrc = dgv_m.Rows.Count;

            if (e.ColumnIndex == 2)
            {
                DataTable dt_w2 = new DataTable();
                dt_w2.Columns.Add("nyukingoukei", Type.GetType("System.Int32"));

                for (int i = 0; i < dgvrc - 1; i++)
                {
                    dt_w2.Rows.Add();
                    dt_w2.Rows[i][0] = double.Parse(dgv_m.Rows[i].Cells[2].Value.ToString());
                }

                string goukei = dt_w2.Compute("SUM(nyukingoukei)", null).ToString();
                tb_nyukin_goukei.Text = goukei;
            }
        }

       }
}
