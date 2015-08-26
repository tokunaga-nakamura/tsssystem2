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
    public partial class frm_siire : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable dt_m = new DataTable();
        string w_str = "06";
        double w_siire_no;
        
        public frm_siire()
        {
            InitializeComponent();
        }


        //取引先コードから取引先名を持ってくるメソッド
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

        //仕入日から仕入締日を計算するメソッド
        private DateTime get_siire_simebi(DateTime in_siire_date)
        {
            DateTime out_siire_simebi = new DateTime();  //戻り値用
            DataTable dt_work = new DataTable();
            string str_day;
            dt_work = tss.OracleSelect("select * from tss_torihikisaki_m where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "'");
            
            if(dt_work.Rows.Count == 0)
            {

            }
            if (dt_work.Rows.Count != 0)
            {
               str_day = dt_work.Rows[0][13].ToString(); //締日の日付
              
                //仕入締日が月末「99」のとき
               if (str_day == "99")
               {
                   //仕入日
                   DateTime dt1 = dtp_siire_date.Value;
                   //仕入日の末日の日付
                   DateTime dt2 = new DateTime(dt1.Year, dt1.Month, DateTime.DaysInMonth(dt1.Year, dt1.Month));
                   //仕入日の末日の日付を戻り値として返す
                   out_siire_simebi = dt2;
               }

               else
               {
                   //仕入日
                   DateTime dt1 = dtp_siire_date.Value;

                   //年と月の値取出し
                   int iYear = dt1.Year;
                   int iMonth = dt1.Month;

                   string str_year = iYear.ToString();
                   string str_month = iMonth.ToString();

                   //年月日をつなげる。str_dayは、取引先マスター上の仕入締日
                   string keisan = str_year + "/" + str_month + "/" + str_day;
                   //つなげた値をDatetime型に変換
                   DateTime dt_keisan = DateTime.Parse(keisan);

                   //仕入処理日と、つなげた日付の比較　仕入締日以前の日付の場合の処理
                   if (dt_keisan.DayOfYear >= dt1.DayOfYear)
                   {
                       out_siire_simebi = dt_keisan;

                   }
                   //その月の仕入締日を過ぎてしまった場合
                   else
                   {
                       DateTime dt3 = dt_keisan.AddMonths(1);
                       out_siire_simebi = dt3;
                   }
               }
            }
            

            
            return out_siire_simebi;

        }

        //取引先マスタから、端数区分と端数処理単位を持ってきて、仕入金額を計算するメソッド
         private void get_siire_kingaku()
        {
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_torihikisaki_m where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "'");

            string hasu_kbn = dt_work.Rows[0][22].ToString();//端数区分　0:切捨て　1:四捨五入　2:切上げ
            string hasu_syori_tani = dt_work.Rows[0][23].ToString();//端数処理単位　0:円未満 1:十円未満 2:百円未満

        }

        //SEQを持ってくるメソッド
        private void SEQ()
        {
            DataTable dt_work = new DataTable();
            double w_seq;
            w_seq = tss.GetSeq(w_str);
            if (w_seq == 0)
            {
                MessageBox.Show("連番マスタに異常があります。処理を中止します。");
                this.Close();
            }
            tb_siire_no.Text = (w_seq).ToString("0000000000");
        }

        private void frm_siire_Load(object sender, EventArgs e)
        {
            w_siire_no = tss.GetSeq("06");
            tb_siire_no.Text = w_siire_no.ToString("0000000000");
            
            //SEQ();

            dgv_siire_disp();
        }

        private void tb_torihikisaki_cd_Validating(object sender, CancelEventArgs e)
        {
            if (tb_torihikisaki_cd.Text == "")
            {
                tb_torihikisaki_name.Text = "";
                return;
            }

            //bool bl = true; //戻り値
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_torihikisaki_m where torihikisaki_cd  = '" + tb_torihikisaki_cd.Text + "'");
            if (dt_work.Rows.Count <= 0)
            {
                //無し
                MessageBox.Show("入力された取引先コードが存在しません。取引先マスタに登録してください。");
                tb_torihikisaki_cd.Focus();

            }
            else
            {
                //既存データ有
                tb_torihikisaki_name.Text = get_torihikisaki_name(tb_torihikisaki_cd.Text);
                dgv_siire.Focus();
            }
            
        }

       //データグリッドビューに値を入力した際の処理
        private void dgv_siire_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            DataTable dt_work2 = new DataTable();
            int j2 = dt_work2.Rows.Count;
                dt_work2 = tss.OracleSelect("select * from tss_torihikisaki_m where torihikisaki_cd = '" + tb_torihikisaki_cd.Text.ToString() + "'");
        

                string hasu_kbn = dt_work2.Rows[j2][22].ToString();//端数区分　0:切捨て　1:四捨五入　2:切上げ
                string hasu_syori_tani = dt_work2.Rows[j2][23].ToString();//端数処理単位　0:円未満 1:十円未満 2:百円未満


            //部品コードが入力されたならば、部品名と仕入単価を部品マスターから取得して表示（部品コードが空欄の際のエラー回避）
            if (dgv.Columns[e.ColumnIndex].Index == 0 && dgv.CurrentCell.Value == null)
            {
                return;
            }
            //部品コードが入力されたならば、部品名と仕入単価を部品マスターから取得して表示
            if (dgv.Columns[e.ColumnIndex].Index == 0 && dgv.CurrentCell.Value.ToString() != null)
            {
                int i = e.RowIndex;

                DataTable dtTmp = (DataTable)dgv_siire.DataSource;

                //部品コードをキーに、部品名、仕入単価を引っ張ってくる

                DataTable dt_work = new DataTable();
                int j = dt_work.Rows.Count;
                
                
                dt_work = tss.OracleSelect("select * from tss_buhin_m where buhin_cd = '" + dgv.CurrentCell.Value.ToString() + "'");

                //取引先マスタの区分を取得
                string seikyu_simebi = dt_work2.Rows[j2][13].ToString();//請求締日
                string kaisyu_tuki = dt_work2.Rows[j2][14].ToString();//回収月
                string kaisyu_hi = dt_work2.Rows[j2][15].ToString();//回収日

                string siharai_simebi = dt_work2.Rows[j2][16].ToString();//支払締日
                string siharai_tuki = dt_work2.Rows[j2][17].ToString();//支払月
                string siharai_hi = dt_work2.Rows[j2][18].ToString();//支払日

                if (dt_work.Rows.Count <= 0)
                {
                    MessageBox.Show("この部品コードは登録されていません。部品登録してください。");
                    dgv.Rows[i].Cells[1].Value = "";
                    dgv_siire.Focus();
                    dgv_siire.CurrentCell = dgv_siire[0, i];
                }
                else //データグリッドビューに一行ずつ値を入れていく
                {
                    dgv.Rows[i].Cells[1].Value = dt_work.Rows[j][1].ToString();
                    dgv.Rows[i].Cells[3].Value = dt_work.Rows[j][8].ToString();

                    //仕入締日計算メソッドの値をstring型に変換してデータグリッドビューに表示
                    string str_siire_simebi = (get_siire_simebi(dtp_siire_date.Value)).ToShortDateString();
                    dgv.Rows[i].Cells[5].Value = str_siire_simebi;
                }
                return;
            }
            
            if (dgv.Columns[e.ColumnIndex].Index == 0 && dgv.CurrentCell.Value.ToString() == null && dgv.CurrentCell.Value.ToString() == "")
            {
                return;
            }


            //仕入数量が入力されたならば、仕入単価と数量を掛け算して仕入金額に表示（取引先マスタの端数処理も組み込む）
            if (dgv.Columns[e.ColumnIndex].Index == 2 && dgv.CurrentCell.Value == null)
            {
                return;
            }

            if (dgv.Columns[e.ColumnIndex].Index == 2 && dgv.CurrentCell.Value.ToString() != null)
            {
                int i = e.RowIndex;

                DataTable dtTmp = (DataTable)dgv_siire.DataSource;

                //仕入金額計算
                
                //仕入数量の小数点第三位を切捨て
                double db = double.Parse(dgv_siire.Rows[i].Cells[2].Value.ToString());
                double dValue = ToRoundDown(db, 2);
                dgv_siire.Rows[i].Cells[2].Value = dValue;

                //仕入単価の小数点第三位を切捨て
                double db2 = double.Parse(dgv_siire.Rows[i].Cells[3].Value.ToString());
                double dValue2 = ToRoundDown(db2, 2);
                dgv_siire.Rows[i].Cells[3].Value = dValue2;


                double suryou;
                double tanka;
                double siire_kingaku;

                suryou = dValue;
                tanka = dValue2; 

                siire_kingaku = suryou * tanka;

                //端数処理 円未満の処理
                if(hasu_syori_tani == "0" && hasu_kbn == "0")
                {
                    siire_kingaku = Math.Floor(siire_kingaku);
                }

                if (hasu_syori_tani == "0" && hasu_kbn == "1")
                {
                    siire_kingaku = Math.Round(siire_kingaku, MidpointRounding.AwayFromZero);
                }

                if (hasu_syori_tani == "0" && hasu_kbn == "2")
                {
                    siire_kingaku = Math.Ceiling(siire_kingaku);
                }

                //端数処理 10円未満の処理
                //切捨て
                if (hasu_syori_tani == "1" && hasu_kbn == "0")
                {
                    siire_kingaku = Math.Floor(siire_kingaku / 10) * 10;
                }
                //四捨五入
                if (hasu_syori_tani == "1" && hasu_kbn == "1")
                {
                    siire_kingaku = Math.Round(siire_kingaku / 10) * 10;
                }
                //切上げ
                if (hasu_syori_tani == "1" && hasu_kbn == "2")
                {
                    siire_kingaku = Math.Ceiling(siire_kingaku / 10) * 10;
                }

                //端数処理 100円未満の処理
                //切捨て
                if (hasu_syori_tani == "2" && hasu_kbn == "0")
                {
                    siire_kingaku = Math.Floor(siire_kingaku / 100) * 100;
                }
                //四捨五入
                if (hasu_syori_tani == "2" && hasu_kbn == "1")
                {
                    siire_kingaku = Math.Round(siire_kingaku / 100) * 100;
                }
                //切上げ
                if (hasu_syori_tani == "2" && hasu_kbn == "2")
                {
                    siire_kingaku = Math.Ceiling(siire_kingaku / 100) * 100;
                }

                dgv.Rows[i].Cells[4].Value = siire_kingaku;

            }

            if (dgv.Columns[e.ColumnIndex].Index == 2 && dgv.CurrentCell.Value.ToString() == null && dgv.CurrentCell.Value.ToString() == "")
            {
                return;

            }

        }

        //データグリッドビューに入力された数値の小数点以下第三桁を切り捨てる
        public static double ToRoundDown(double dValue, int iDigits)
        {
            double dCoef = System.Math.Pow(10, iDigits);

            return dValue > 0 ? System.Math.Floor(dValue * dCoef) / dCoef :
                                System.Math.Ceiling(dValue * dCoef) / dCoef;
        }


        private void btn_touroku_Click(object sender, EventArgs e)
        {
            DataTable dt_work = new DataTable();


            //登録前に全ての項目をチェック
            //仕入番号
            if (chk_siire_no() == false)
            {
                MessageBox.Show("仕入番号は10バイト以内で入力してください");
                tb_siire_no.Focus();
                return;
            }

            //仕入伝票番号
            if (chk_siire_denpyou_no() == false)
            {
                MessageBox.Show("仕入伝票番号は16バイト以内で入力してください");
                tb_siire_denpyou_no.Focus();
                return;
            }


            //取引先コードのチェック
            if (chk_torihikisaki_cd() == false)
            {
                MessageBox.Show("取引先コードは6文字で入力してください。");
                tb_torihikisaki_cd.Focus();
                return;
            }
            

            //データグリッドビューの中を1行ずつループしてチェック
            int dgvrc = dgv_siire.Rows.Count;
            if (dgvrc == 1)
            {
                MessageBox.Show("表の中に何も入力されていません");
                return;
            }

            tss.GetUser();  //ユーザー情報の取得

            //テキストボックスとデータグリッドビューの入力内容チェック
            for (int i = 0; i < dgvrc - 1; i++)
            {
                if (dgv_siire.Rows[i].Cells[0].Value == null || tss.StringByte(dgv_siire.Rows[i].Cells[0].Value.ToString()) > 16)
                {
                    MessageBox.Show("部品コードの値が異常です");
                    return;
                }

                if (dgv_siire.Rows[i].Cells[1].Value == null)
                {
                    MessageBox.Show("部品名を入力してください");
                    return;
                }

                if (dgv_siire.Rows[i].Cells[2].Value == null || tss.StringByte(dgv_siire.Rows[i].Cells[2].Value.ToString()) > 12)
                {
                    MessageBox.Show("仕入数量の値が異常です");
                    return;
                }

                if (dgv_siire.Rows[i].Cells[3].Value == null || tss.StringByte(dgv_siire.Rows[i].Cells[3].Value.ToString()) > 12)
                {
                    MessageBox.Show("仕入単価を入力してください");
                    return;
                }

                if (dgv_siire.Rows[i].Cells[4].Value == null)
                {
                    MessageBox.Show("仕入締日を入力してください");
                    return;
                }


                //支払計上日が空白の場合、""を代入  空欄だとnull扱いされ、SQエラー回避
                if (dgv_siire.Rows[i].Cells[6].Value == null)
                {
                    dgv_siire.Rows[i].Cells[6].Value = "";
                }

                //備考が空白の場合、""を代入  空欄だとnull扱いされ、SQエラー回避
                if (dgv_siire.Rows[i].Cells[7].Value == null)
                {
                    dgv_siire.Rows[i].Cells[7].Value = "";
                }
            }

            dt_work = tss.OracleSelect("select * from tss_siire_m where siire_no = '" + tb_siire_no.Text.ToString() + "'");
            int rc = dt_work.Rows.Count;
            int rc2 = dgv_siire.Rows.Count;
            tss.GetUser();

            if (rc == 0)
            {
                for (int i = 0; i < rc2 - 1; i++)
                {
                    bool bl = tss.OracleInsert("insert into tss_siire_m (siire_no, seq,torihikisaki_cd, siire_date,buhin_cd,buhin_name,siire_su,siire_tanka,siire_kingaku,siire_denpyo_no,siire_simebi,bikou,create_user_cd,create_datetime) values ('"

                              + tb_siire_no.Text.ToString() + "','"
                              + (i + 1) + "','"
                              + tb_torihikisaki_cd.Text.ToString() + "','"
                              + dtp_siire_date.Value.ToShortDateString() + "','"
                              + dgv_siire.Rows[i].Cells[0].Value.ToString() + "','"
                              + dgv_siire.Rows[i].Cells[1].Value.ToString() + "','"
                              + dgv_siire.Rows[i].Cells[2].Value.ToString() + "','"
                              + dgv_siire.Rows[i].Cells[3].Value.ToString() + "','"
                              + dgv_siire.Rows[i].Cells[4].Value.ToString() + "','"
                              + tb_siire_denpyou_no.Text.ToString() + "','"
                              + dgv_siire.Rows[i].Cells[5].Value.ToString() + "','"
                            //+ "to_date('" + dgv_siire.Rows[i].Cells[5].Value.ToString() + "','YYYY/MM/DD HH24:MI:SS'),'"
                            //+ "to_date('" + dgv_siire.Rows[i].Cells[6].Value.ToString() + "','YYYY/MM/DD HH24:MI:SS'),'"
                              + dgv_siire.Rows[i].Cells[7].Value.ToString() + "','"
                              + tss.user_cd + "',SYSDATE)");


                    if (bl != true)
                    {
                        tss.ErrorLogWrite(tss.user_cd, "仕入登録", "登録ボタン押下時のOracleInsert");
                        MessageBox.Show("仕入処理でエラーが発生しました。" + Environment.NewLine + "処理を中止します。");
                        this.Close();
                    }
                    else
                    {
                        
                    }
                }

                tb_create_user_cd.Text = tss.user_cd;
                tb_create_datetime.Text = DateTime.Now.ToString();
                MessageBox.Show("仕入登録しました。");

                w_siire_no = tss.GetSeq("06");
                dgv_siire.Rows.Clear();
                tb_torihikisaki_cd.Clear();
                tb_torihikisaki_name.Clear();
                dtp_siire_date.Value = DateTime.Today;
                tb_siire_denpyou_no.Clear();
                tb_create_user_cd.Clear();
                tb_create_datetime.Clear();
                tb_update_user_cd.Clear();
                tb_update_datetime.Clear();
                tb_siire_no.Text = w_siire_no.ToString("0000000000");

                return;

            }
            else
            {
                DialogResult result = MessageBox.Show("既存の仕入データを上書きしますか？",
                        "仕入データの上書き確認",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button2);
                
                if (result == DialogResult.OK)
                {
                    //仕入マスタから削除してインサート
                    tss.OracleDelete("delete from tss_siire_m WHERE siire_no = '" + tb_siire_no.Text.ToString() + "'");

                    for (int i = 0; i < rc2 - 1 ; i++)
                    {
                        bool bl = tss.OracleInsert("insert into tss_siire_m (siire_no, seq,torihikisaki_cd, siire_date,buhin_cd,buhin_name,siire_su,siire_tanka,siire_kingaku,siire_denpyo_no,siire_simebi,bikou,create_user_cd,create_datetime,update_user_cd,update_datetime) values ('"

                                  + tb_siire_no.Text.ToString() + "','"
                                  + (i + 1) + "','"
                                  + tb_torihikisaki_cd.Text.ToString() + "','"
                                  + dtp_siire_date.Value.ToShortDateString() + "','"
                                  + dgv_siire.Rows[i].Cells[0].Value.ToString() + "','"
                                  + dgv_siire.Rows[i].Cells[1].Value.ToString() + "','"
                                  + dgv_siire.Rows[i].Cells[2].Value.ToString() + "','"
                                  + dgv_siire.Rows[i].Cells[3].Value.ToString() + "','"
                                  + dgv_siire.Rows[i].Cells[4].Value.ToString() + "','"
                                  + tb_siire_denpyou_no.Text.ToString() + "','"
                                  + dgv_siire.Rows[i].Cells[5].Value.ToString() + "','"
                                  + dgv_siire.Rows[i].Cells[7].Value.ToString() + "','"
                                  + tb_create_user_cd.Text.ToString() + "',"//←カンマがあると、日付をインサートする際にエラーになるので注意する
                                  + "to_date('" + tb_create_datetime.Text.ToString() + "','YYYY/MM/DD HH24:MI:SS'),'"　
                                  + tss.user_cd + "',SYSDATE)");


                        if (bl != true)
                        {
                            tss.ErrorLogWrite(tss.user_cd, "仕入登録", "登録ボタン押下時のOracleInsert");
                            MessageBox.Show("仕入処理でエラーが発生しました。" + Environment.NewLine + "処理を中止します。");
                            this.Close();
                        }
                        else
                        {

                        }
                    }
                    tb_update_user_cd.Text = tss.user_cd.ToString();
                    tb_update_datetime.Text = DateTime.Now.ToString();
                    MessageBox.Show("仕入登録しました。");

                    w_siire_no = tss.GetSeq("06");
                    dgv_siire.Rows.Clear();
                    tb_torihikisaki_cd.Clear();
                    tb_torihikisaki_name.Clear();
                    dtp_siire_date.Value = DateTime.Today;
                    tb_siire_denpyou_no.Clear();
                    tb_create_user_cd.Clear();
                    tb_create_datetime.Clear();
                    tb_update_user_cd.Clear();
                    tb_update_datetime.Clear();
                    tb_siire_no.Text = w_siire_no.ToString("0000000000");
                    
                    return;
                    

                }
                //「いいえ」が選択された時
                else if (result == DialogResult.Cancel)
                {
                    return;
                }                
            }

        }


        //仕入番号チェック用
        private bool chk_siire_no()
        {
            bool bl = true; //戻り値用

            if (tss.StringByte(tb_siire_no.Text) > 10)
            {
                bl = false;
            }
            return bl;
        }

        //仕入伝票番号チェック用
        private bool chk_siire_denpyou_no()
        {
            bool bl = true; //戻り値用

            if (tss.StringByte(tb_siire_denpyou_no.Text) > 16)
            {
                bl = false;
            }
            return bl;
        }

        //取引先コードチェック用
        private bool chk_torihikisaki_cd()
        {
            bool bl = true; //戻り値用

            if (tb_torihikisaki_cd.Text == null || tb_torihikisaki_cd.Text.Length > 6 || tb_torihikisaki_cd.Text.Length < 6)
            {
                bl = false;
            }
            return bl;
        }

        //製品構成のデータグリッドビュー表示共通メソッド
        private void dgv_siire_disp()
        {
            dgv_siire.Columns[0].HeaderText = "部品コード";
            dgv_siire.Columns[1].HeaderText = "部品名";
            dgv_siire.Columns[2].HeaderText = "仕入数量";
            dgv_siire.Columns[3].HeaderText = "仕入単価";
            dgv_siire.Columns[4].HeaderText = "仕入金額（税抜）";
            dgv_siire.Columns[5].HeaderText = "仕入締日";
            dgv_siire.Columns[6].HeaderText = "支払計上日";
            dgv_siire.Columns[7].HeaderText = "備考";

            dgv_siire.Columns[0].Width = 85;
            dgv_siire.Columns[1].Width = 200;
            dgv_siire.Columns[2].Width = 80;
            dgv_siire.Columns[3].Width = 80;
            dgv_siire.Columns[4].Width = 120;
            dgv_siire.Columns[5].Width = 100;
            dgv_siire.Columns[6].Width = 100;
            dgv_siire.Columns[7].Width = 80;

            //使用数量右寄せ、カンマ区切り
            dgv_siire.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_siire.Columns[2].DefaultCellStyle.Format = "#,0.##";

            dgv_siire.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_siire.Columns[3].DefaultCellStyle.Format = "#,0.##";

            dgv_siire.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_siire.Columns[4].DefaultCellStyle.Format = "#,0.##";

            //部品名は入力不可
            dgv_siire.Columns[1].ReadOnly = true;
            //行ヘッダーを表示する
            dgv_siire.RowHeadersVisible = true;
            //セルの高さ変更不可
            dgv_siire.AllowUserToResizeRows = false;
            //カラムヘッダーの高さ変更不可
            dgv_siire.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            //dgv_seihin_kousei.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //並べ替え不可
            foreach (DataGridViewColumn c in dgv_siire.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;

        }

        private void tb_siire_no_Validating(object sender, CancelEventArgs e)
        {

            //入力された仕入番号を"0000000000"形式の文字列に変換
            double w_double;
            if (double.TryParse(tb_siire_no.Text.ToString(), out w_double))
            {
                tb_siire_no.Text = w_double.ToString("0000000000");
            }
            else
            {
                MessageBox.Show("仕入番号に異常があります。");
                tb_siire_no.Focus();
            }
            //新規か既存かの判定
            if (tb_siire_no.Text.ToString() == w_siire_no.ToString("0000000000"))
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
                dt_work = tss.OracleSelect("select siire_no, seq,torihikisaki_cd, siire_date,buhin_cd,buhin_name,siire_su,siire_tanka,siire_kingaku,siire_denpyo_no,TO_CHAR(siire_simebi, 'YYYY/MM/DD'),TO_CHAR(shiharai_date, 'YYYY/MM/DD'),bikou,DELETE_FLG,create_user_cd,create_datetime,update_user_cd,update_datetime from tss_siire_m where siire_no = '" + tb_siire_no.Text.ToString() + "' ORDER BY SEQ");
                int rc = dt_work.Rows.Count;
                
                if (dt_work.Rows.Count == 0)
                {
                    MessageBox.Show("データがありません。");
                    dgv_siire.Rows.Clear();
                    tb_torihikisaki_cd.Clear();
                    tb_torihikisaki_name.Clear();
                    dtp_siire_date.Value = DateTime.Today;
                    tb_siire_denpyou_no.Clear();
                    tb_create_user_cd.Clear();
                    tb_create_datetime.Clear();
                    tb_update_user_cd.Clear();
                    tb_update_datetime.Clear();
                    tb_siire_no.Text = w_siire_no.ToString("0000000000");
                    tb_siire_no.Focus();
                    return;
                }

                else
                {
                    dgv_siire.Rows.Clear();
                    tb_siire_denpyou_no.Text = dt_work.Rows[0][9].ToString();
                    tb_torihikisaki_cd.Text = dt_work.Rows[0][2].ToString();

                    tb_torihikisaki_name.Text = tss.get_torihikisaki_name(tb_torihikisaki_cd.Text);

                    dtp_siire_date.Value = DateTime.Parse(dt_work.Rows[0][3].ToString());
                    tb_create_user_cd.Text = dt_work.Rows[0][14].ToString();
                    tb_create_datetime.Text = dt_work.Rows[0][15].ToString();

                    tb_update_user_cd.Text = dt_work.Rows[0][16].ToString();
                    tb_update_datetime.Text = dt_work.Rows[0][17].ToString();


                    for (int i = 0; i < rc  ; i++)
                    {
                        dgv_siire.Rows.Add();
                        dgv_siire.Rows[i].Cells[0].Value = dt_work.Rows[i][4].ToString();
                        dgv_siire.Rows[i].Cells[1].Value = dt_work.Rows[i][5].ToString();
                        dgv_siire.Rows[i].Cells[2].Value = dt_work.Rows[i][6].ToString();
                        dgv_siire.Rows[i].Cells[3].Value = dt_work.Rows[i][7].ToString();
                        dgv_siire.Rows[i].Cells[4].Value = dt_work.Rows[i][8].ToString();

                        dgv_siire.Rows[i].Cells[5].Value = dt_work.Rows[i][10].ToString();
                        dgv_siire.Rows[i].Cells[6].Value = dt_work.Rows[i][11].ToString();
                        dgv_siire.Rows[i].Cells[7].Value = dt_work.Rows[i][12].ToString();

                    }
                }
               
            }

        }


        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //仕入計上日を変更した際の動き
        private void dtp_siire_date_ValueChanged(object sender, EventArgs e)
        {
            int rc = dgv_siire.Rows.Count;
            string str_siire_simebi = (get_siire_simebi(dtp_siire_date.Value)).ToShortDateString();

            for (int i = 0; i < rc-1; i++)
            {
                dgv_siire.Rows[i].Cells[5].Value = str_siire_simebi;
            }
        }

        //仕入伝票番号を変更した際の動き
        private void tb_siire_denpyou_no_Validating(object sender, CancelEventArgs e)
        {
            //既存仕入の表示
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select siire_no, seq,torihikisaki_cd, siire_date,buhin_cd,buhin_name,siire_su,siire_tanka,siire_kingaku,siire_denpyo_no,TO_CHAR(siire_simebi, 'YYYY/MM/DD'),TO_CHAR(shiharai_date, 'YYYY/MM/DD'),bikou,DELETE_FLG,create_user_cd,create_datetime,update_user_cd,update_datetime from tss_siire_m where siire_denpyo_no = '" + tb_siire_denpyou_no.Text.ToString() + "' ORDER BY SEQ");
            int rc = dt_work.Rows.Count;

            if (dt_work.Rows.Count == 0)
            {
                //MessageBox.Show("データがありません。");
                dgv_siire.Rows.Clear();
                tb_torihikisaki_cd.Clear();
                tb_torihikisaki_name.Clear();
                dtp_siire_date.Value = DateTime.Today;
                //tb_siire_denpyou_no.Clear();
                tb_create_user_cd.Clear();
                tb_create_datetime.Clear();
                tb_update_user_cd.Clear();
                tb_update_datetime.Clear();
                tb_siire_no.Text = w_siire_no.ToString("0000000000");
                //tb_siire_no.Focus();
                return;
            }

            else
            {
                dgv_siire.Rows.Clear();
                tb_siire_no.Text = dt_work.Rows[0][0].ToString();
                tb_torihikisaki_cd.Text = dt_work.Rows[0][2].ToString();

                tb_torihikisaki_name.Text = tss.get_torihikisaki_name(tb_torihikisaki_cd.Text);

                dtp_siire_date.Value = DateTime.Parse(dt_work.Rows[0][3].ToString());
                tb_create_user_cd.Text = dt_work.Rows[0][14].ToString();
                tb_create_datetime.Text = dt_work.Rows[0][15].ToString();

                tb_update_user_cd.Text = dt_work.Rows[0][16].ToString();
                tb_update_datetime.Text = dt_work.Rows[0][17].ToString();


                for (int i = 0; i < rc; i++)
                {
                    dgv_siire.Rows.Add();
                    dgv_siire.Rows[i].Cells[0].Value = dt_work.Rows[i][4].ToString();
                    dgv_siire.Rows[i].Cells[1].Value = dt_work.Rows[i][5].ToString();
                    dgv_siire.Rows[i].Cells[2].Value = dt_work.Rows[i][6].ToString();
                    dgv_siire.Rows[i].Cells[3].Value = dt_work.Rows[i][7].ToString();
                    dgv_siire.Rows[i].Cells[4].Value = dt_work.Rows[i][8].ToString();

                    dgv_siire.Rows[i].Cells[5].Value = dt_work.Rows[i][10].ToString();
                    dgv_siire.Rows[i].Cells[6].Value = dt_work.Rows[i][11].ToString();
                    dgv_siire.Rows[i].Cells[7].Value = dt_work.Rows[i][12].ToString();

                }
            }
               
        }
   
    }
}
