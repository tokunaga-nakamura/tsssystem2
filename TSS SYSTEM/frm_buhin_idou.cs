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
    public partial class frm_buhin_idou : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable dt_m = new DataTable();

        string w_str = "03";
        
        
        public frm_buhin_idou()
        {
            InitializeComponent();
        }

        private void frm_buhin_idou_Load(object sender, EventArgs e)
        {
            //SEQ更新メソッド
            SEQ();

            //データグリッドビューの部品名は編集不可
            dgv_idou.Columns[1].ReadOnly = true;
        }



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
            tb_seq.Text = (w_seq).ToString("0000000000");
        }

        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_sakujyo_Click(object sender, EventArgs e)
        {
            int i = dgv_idou.CurrentCell.RowIndex;
            dgv_idou.Rows.RemoveAt(dgv_idou.Rows[i].Index);
        }

        private void dgv_idou_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            int i = e.RowIndex;

            if (dgv.Columns[e.ColumnIndex].Index == 0 && dgv.CurrentCell.Value == null)
            {
                dgv.Rows[i].Cells[1].Value = "";
                return;
            }



        }

        //登録ボタン押した時の処理
        private void btn_touroku_Click(object sender, EventArgs e)
        {
            DataTable dt_work = new DataTable();

            //登録前に全ての項目をチェック
            //伝票番号
            if (chk_denpyou_no() == false)
            {
                MessageBox.Show("伝票番号の値が異常です");
                tb_denpyou_no.Focus();
                return;
            }
            
            //データグリッドビューの中を1行ずつループしてチェック
            int dgvrc = dgv_idou.Rows.Count;
            if (dgvrc == 1)
            {
                MessageBox.Show("表の中に何も入力されていません");
                return;
            }

            tss.GetUser();  //ユーザー情報の取得

            //テキストボックスとデータグリッドビューの入力内容チェック
            for (int i = 0; i < dgvrc - 1; i++)
            {
                if (dgv_idou.Rows[i].Cells[0].Value == null)
                {
                    MessageBox.Show("部品コードを入力してください");
                    return;
                }

                if (dgv_idou.Rows[i].Cells[2].Value == null)
                {
                    MessageBox.Show("在庫区分を入力してください");
                    return;
                }
               
                dt_work = tss.OracleSelect("select * from tss_kubun_m where kubun_meisyou_cd  = '01' and kubun_cd = '" + dgv_idou.Rows[i].Cells[2].Value.ToString() + "'");
                if (dt_work.Rows.Count <= 0)
                {
                    MessageBox.Show("入力された在庫区分が存在しません");
                    return;
                }
                
                if (dgv_idou.Rows[i].Cells[2].Value.ToString() == "02" && dgv_idou.Rows[i].Cells[3].Value == null)
                {
                    MessageBox.Show("取引先コードを入力してください");
                    return;
                }
                //dt_work = tss.OracleSelect("select * from tss_torihikisaki_m where torihikisaki_cd  =  '" + dgv_idou.Rows[i].Cells[3].Value.ToString() + "'");
                //if (dt_work.Rows.Count <= 0)
                //{
                //    MessageBox.Show("入力された移動元の取引先コードが存在しません");
                //    return;
                //}
                if (dgv_idou.Rows[i].Cells[2].Value.ToString() == "02" && dgv_idou.Rows[i].Cells[4].Value == null)
                {
                    MessageBox.Show("受注コード1を入力してください");
                    return;
                }
                if (dgv_idou.Rows[i].Cells[2].Value.ToString() != "02" && dgv_idou.Rows[i].Cells[4].Value != null && dgv_idou.Rows[i].Cells[5].Value != null)
                {
                    MessageBox.Show("在庫区分02以外の時は、受注コード1、2に入力しないでください。");
                    return;
                }

                if (dgv_idou.Rows[i].Cells[6].Value == null)
                {
                    MessageBox.Show("移動先在庫区分を入力してください");
                    return;
                }
                
                //dt_work = tss.OracleSelect("select * from tss_kubun_m where kubun_meisyou_cd  = '01' and kubun_cd = '" + dgv_idou.Rows[i].Cells[6].Value.ToString() + "'");
                //if (dt_work.Rows.Count <= 0)
                //{
                //    MessageBox.Show("入力された移動先在庫区分が存在しません");
                //    return;
                //}
                //dt_work = new DataTable();
                if (dgv_idou.Rows[i].Cells[6].Value.ToString() == "02" && dgv_idou.Rows[i].Cells[7].Value == null)
                {
                    MessageBox.Show("移動先取引先コードを入力してください");
                    return;
                }
                //dt_work = tss.OracleSelect("select * from tss_torihikisaki_m where torihikisaki_cd  =  '" + dgv_idou.Rows[i].Cells[7].Value.ToString() + "'");
                //if (dt_work.Rows.Count <= 0)
                //{
                //    MessageBox.Show("入力された移動先取引先コードが存在しません");
                //    return;
                //}
                if (dgv_idou.Rows[i].Cells[6].Value.ToString() == "02" && dgv_idou.Rows[0].Cells[8].Value == null)
                {
                    MessageBox.Show("移動先受注コード1を入力してください");
                    return;
                }
                if (dgv_idou.Rows[i].Cells[6].Value.ToString() != "02" && dgv_idou.Rows[i].Cells[8].Value != null && dgv_idou.Rows[i].Cells[9].Value != null)
                {
                    MessageBox.Show("移動先在庫区分が02以外の時は、移動先受注コード1、2に入力しないでください。");
                    return;
                }

                if (dgv_idou.Rows[i].Cells[10].Value == null)
                {
                    MessageBox.Show("数量を入力してください");
                    return;
                }
                
                //備考が空白の場合、""を代入  空欄だとnull扱いされ、SQエラー回避
                if (dgv_idou.Rows[i].Cells[11].Value == null)
                {
                    dgv_idou.Rows[i].Cells[11].Value = "";
                }

            }


            for (int i = 0; i < dgvrc - 1; i++)
            {
                //受注コードが空白の場合、9999999999999999を代入
                if (dgv_idou.Rows[i].Cells[2].Value.ToString() == "02" && dgv_idou.Rows[i].Cells[4].Value != null && dgv_idou.Rows[i].Cells[5].Value == null)
                {
                    dgv_idou.Rows[i].Cells[4].Value = 9999999999999999;
                }

                if (dgv_idou.Rows[i].Cells[2].Value.ToString() != "02" && dgv_idou.Rows[i].Cells[3].Value == null && dgv_idou.Rows[i].Cells[4].Value == null && dgv_idou.Rows[i].Cells[5].Value == null)
                {
                    dgv_idou.Rows[i].Cells[3].Value = 999999;
                    dgv_idou.Rows[i].Cells[4].Value = 9999999999999999;
                    dgv_idou.Rows[i].Cells[5].Value = 9999999999999999;
                }

                if (dgv_idou.Rows[i].Cells[2].Value.ToString() != "02" && dgv_idou.Rows[i].Cells[3].Value == null)
                {
                    dgv_idou.Rows[i].Cells[3].Value = 999999;
                }

                if (dgv_idou.Rows[i].Cells[6].Value.ToString() == "02" && dgv_idou.Rows[i].Cells[8].Value != null && dgv_idou.Rows[i].Cells[9].Value == null)
                {
                    dgv_idou.Rows[i].Cells[9].Value = 9999999999999999;
                }

                if (dgv_idou.Rows[i].Cells[6].Value.ToString() != "02" && dgv_idou.Rows[i].Cells[7].Value == null && dgv_idou.Rows[i].Cells[8].Value == null && dgv_idou.Rows[i].Cells[9].Value == null)
                {
                    dgv_idou.Rows[i].Cells[7].Value = 999999;
                    dgv_idou.Rows[i].Cells[8].Value = 9999999999999999;
                    dgv_idou.Rows[i].Cells[9].Value = 9999999999999999;
                }

            }


            //レコードの行数分ループしてインサート
            int dgvrc2 = dgv_idou.Rows.Count;

            for (int i = 0; i < dgvrc2 - 1; i++)
            {
                
                bool bl6 = tss.OracleInsert("INSERT INTO tss_buhin_nyusyukko_m (buhin_syori_kbn,buhin_syori_no,seq,buhin_syori_date,buhin_cd,zaiko_kbn,torihikisaki_cd,juchu_cd1,juchu_cd2,suryou,idousaki_zaiko_kbn,idousaki_torihikisaki_cd,idousaki_juchu_cd1,idousaki_juchu_cd2,denpyou_no,barcode,syori_kbn,bikou,create_user_cd,create_datetime) VALUES ('"
                                    + "03" + "','"
                                    + tb_seq.Text.ToString() + "','"
                                    + (i + 1) + "','"
                                    + dtp_buhin_syori_date.Value.ToShortDateString() + "','"
                                    + dgv_idou.Rows[i].Cells[0].Value.ToString() + "','"
                                    + dgv_idou.Rows[i].Cells[2].Value.ToString() + "','"
                                    + dgv_idou.Rows[i].Cells[3].Value.ToString() + "','"
                                    + dgv_idou.Rows[i].Cells[4].Value.ToString() + "','"
                                    + dgv_idou.Rows[i].Cells[5].Value.ToString() + "','"
                                    + double.Parse(dgv_idou.Rows[i].Cells[10].Value.ToString()) + "','"
                                    + dgv_idou.Rows[i].Cells[6].Value.ToString() + "','"
                                    + dgv_idou.Rows[i].Cells[7].Value.ToString() + "','"
                                    + dgv_idou.Rows[i].Cells[8].Value.ToString() + "','"
                                    + dgv_idou.Rows[i].Cells[9].Value.ToString() + "','"
                                    + tb_denpyou_no.Text.ToString() + "','"
                                    + "" + "','"
                                    + "01" + "','"
                                    + dgv_idou.Rows[i].Cells[11].Value.ToString() + "','"
                                    + tss.user_cd + "',SYSDATE)");

                if (bl6 != true)
                {
                    tss.ErrorLogWrite(tss.user_cd, "入出庫移動／登録", "登録ボタン押下時のOracleInsert");
                    MessageBox.Show("移動処理でエラーが発生しました。" + Environment.NewLine + "処理を中止します。");
                    this.Close();
                }
            }


            //部品在庫マスタの更新
            //既存の区分があるかチェック
            int j = dgv_idou.Rows.Count;
            DataTable dt_work5 = new DataTable();
            tss.GetUser();
            for (int i = 0; i < j - 1; i++)
            {
                dt_work5 = tss.OracleSelect("select * from tss_buhin_zaiko_m where torihikisaki_cd = '" + dgv_idou.Rows[i].Cells[3].Value.ToString() + "'and buhin_cd = '" + dgv_idou.Rows[i].Cells[0].Value.ToString() + "'and zaiko_kbn = '" + dgv_idou.Rows[i].Cells[2].Value.ToString() + "' and juchu_cd1 = '" + dgv_idou.Rows[i].Cells[4].Value.ToString() + "'and juchu_cd2 = '" + dgv_idou.Rows[i].Cells[5].Value.ToString() + "'");


                if (dt_work5.Rows.Count == 0)
                {

                    //出庫処理の場合は、数量をマイナスにする
                    double syukko = double.Parse(dgv_idou.Rows[i].Cells[10].Value.ToString()) * -1;


                    bool bl3 = tss.OracleInsert("insert into tss_buhin_zaiko_m (buhin_cd, zaiko_kbn,torihikisaki_cd, juchu_cd1, juchu_cd2, zaiko_su,create_user_cd,create_datetime) values ('"
                                              + dgv_idou.Rows[i].Cells[0].Value.ToString() + "','"
                                              + dgv_idou.Rows[i].Cells[2].Value.ToString() + "','"
                                              + dgv_idou.Rows[i].Cells[3].Value.ToString() + "','"
                                              + dgv_idou.Rows[i].Cells[4].Value.ToString() + "','"
                                              + dgv_idou.Rows[i].Cells[5].Value.ToString() + "','"
                                              + syukko + "','"
                                              + tss.user_cd + "',SYSDATE)");
                }

                if (dt_work5.Rows.Count != 0)
                {
                    double zaikosu1 = double.Parse(dt_work5.Rows[0][5].ToString());
                    double zaikosu2 = double.Parse(dgv_idou.Rows[i].Cells[10].Value.ToString());

                    double zaikosu3 = zaikosu1 - zaikosu2;

                    bool bl5 = tss.OracleUpdate("UPDATE TSS_BUHIN_ZAIKO_M SET ZAIKO_SU = '" + zaikosu3 + "',UPDATE_DATETIME = SYSDATE,UPDATE_USER_CD = '" + tss.user_cd + "' WHERE buhin_cd = '" + dgv_idou.Rows[i].Cells[0].Value.ToString() + "' and torihikisaki_cd = '" + dgv_idou.Rows[i].Cells[3].Value.ToString() + "' and juchu_cd1 = '" + dgv_idou.Rows[i].Cells[4].Value.ToString() + "' and juchu_cd2 = '" + dgv_idou.Rows[i].Cells[5].Value.ToString() + "'");
                }
            }

            MessageBox.Show("出庫処理されました。");

            //入庫処理
            //レコードの行数分ループしてインサート
            //int dgvrc2 = dgv_idou.Rows.Count;


            //部品在庫マスタの更新
            //既存の区分があるかチェック
            //int j = dgv_idou.Rows.Count;
            //DataTable dt_work5 = new DataTable();
            tss.GetUser();
            for (int i = 0; i < j - 1; i++)
            {
                dt_work5 = tss.OracleSelect("select * from tss_buhin_zaiko_m where torihikisaki_cd = '" + dgv_idou.Rows[i].Cells[7].Value.ToString() + "'and buhin_cd = '" + dgv_idou.Rows[i].Cells[0].Value.ToString() + "'and zaiko_kbn = '" + dgv_idou.Rows[i].Cells[6].Value.ToString() + "' and juchu_cd1 = '" + dgv_idou.Rows[i].Cells[8].Value.ToString() + "'and juchu_cd2 = '" + dgv_idou.Rows[i].Cells[9].Value.ToString() + "'");


                if (dt_work5.Rows.Count == 0)
                {
                    bool bl3 = tss.OracleInsert("insert into tss_buhin_zaiko_m (buhin_cd, zaiko_kbn,torihikisaki_cd, juchu_cd1, juchu_cd2, zaiko_su,create_user_cd,create_datetime) values ('"
                                                            + dgv_idou.Rows[i].Cells[0].Value.ToString() + "','"
                                                            + dgv_idou.Rows[i].Cells[6].Value.ToString() + "','"
                                                            + dgv_idou.Rows[i].Cells[7].Value.ToString() + "','"
                                                            + dgv_idou.Rows[i].Cells[8].Value.ToString() + "','"
                                                            + dgv_idou.Rows[i].Cells[9].Value.ToString() + "','"
                                                            + dgv_idou.Rows[i].Cells[10].Value.ToString() + "','"
                                                            + tss.user_cd + "',SYSDATE)");
                }

                if (dt_work5.Rows.Count != 0)
                {
                    double zaikosu1 = double.Parse(dt_work5.Rows[0][5].ToString());
                    double zaikosu2 = double.Parse(dgv_idou.Rows[i].Cells[10].Value.ToString());

                    double zaikosu3 = zaikosu1 + zaikosu2;

                    bool bl5 = tss.OracleUpdate("UPDATE TSS_BUHIN_ZAIKO_M SET ZAIKO_SU = '" + zaikosu3 + "',UPDATE_DATETIME = SYSDATE,UPDATE_USER_CD = '" + tss.user_cd + "' WHERE buhin_cd = '" + dgv_idou.Rows[i].Cells[0].Value.ToString() + "' and torihikisaki_cd = '" + dgv_idou.Rows[i].Cells[7].Value.ToString() + "' and juchu_cd1 = '" + dgv_idou.Rows[i].Cells[8].Value.ToString() + "' and juchu_cd2 = '" + dgv_idou.Rows[i].Cells[9].Value.ToString() + "'");

                }
            }
            MessageBox.Show("入庫処理されました。");

 
            SEQ();
            tb_denpyou_no.Clear();
            dgv_idou.Rows.Clear();
        }


        //伝票番号チェック用
        private bool chk_denpyou_no()
        {
            bool bl = true; //戻り値用

            if (tss.StringByte(tb_denpyou_no.Text) > 16)
            {
                bl = false;
            }
            return bl;
        }

        private void dgv_idou_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //DataGridView dgv = sender as DataGridView;

            //if (e.ColumnIndex >= 0)
            //{
            //    if (dgv.Columns[e.ColumnIndex] is DataGridViewTextBoxColumn)
            //    {
            //        SendKeys.Send("{F2}");
            //    }
            //}
        }

        private void dgv_idou_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.ColumnIndex;

            if (i == 0)
            {
                //選択画面へ
                string w_buhin_cd;
                w_buhin_cd = tss.search_buhin("2", "");
                if (w_buhin_cd != "")
                {
                    dgv_idou.CurrentCell.Value = w_buhin_cd;
                    dgv_idou.Rows[e.RowIndex].Cells[i + 1].Value = tss.get_buhin_name(w_buhin_cd);
                    dgv_idou.EndEdit();
                }
            }

            if (i == 3)
            {

                if (dgv_idou.CurrentRow.Cells[2].Value == null || dgv_idou.CurrentRow.Cells[2].Value.ToString() != "01")
                {
                    //torihikisaki_dc_ck();

                    //選択画面へ
                    string w_cd;
                    w_cd = tss.search_torihikisaki("2", "");
                    if (w_cd != "")
                    {
                        dgv_idou.CurrentCell.Value = w_cd;



                        if (dgv_idou.Rows[e.RowIndex].Cells[0].Value != null)
                        {
                            //torihikisaki_ckメソッド

                            string str = dgv_idou.Rows[e.RowIndex].Cells[0].Value.ToString();
                            string str2 = w_cd;

                            DataTable dt_w2 = new DataTable();
                            dt_w2 = tss.OracleSelect("select torihikisaki_cd from tss_buhin_m where buhin_cd  =  '" + str + "'");

                            if (dt_w2.Rows.Count == 0)
                            {
                                MessageBox.Show("入力された移動先取引先コードが存在しません");
                                return;
                            }
                            else
                            {
                                string str3 = dt_w2.Rows[0][0].ToString();

                                if (str2 == str3)
                                {

                                }
                                else
                                {
                                    DialogResult result = MessageBox.Show("移動する部品コードの取引先コードと部品マスタの取引先コードが異なりますがよろしいですか？",
                                      "部品入移動登録",
                                      MessageBoxButtons.OKCancel,
                                      MessageBoxIcon.Exclamation,
                                      MessageBoxDefaultButton.Button2);

                                    if (result == DialogResult.OK)
                                    {
                                        dgv_idou.EndEdit();
                                        //dgv_seihin_kousei.EndEdit();
                                        dgv_idou.Focus();

                                    }
                                    if (result == DialogResult.Cancel)
                                    {
                                        return;
                                    }
                                }
                            }
                        }

                    }
                    dgv_idou.EndEdit();
                }

                else
                {
                    return;
                }

            }

            if (i == 7)
            {

                if (dgv_idou.CurrentRow.Cells[6].Value == null || dgv_idou.CurrentRow.Cells[6].Value.ToString() != "01")
                {
                    //選択画面へ
                     string w_cd;
                     w_cd = tss.search_torihikisaki("2", "");
                     if (w_cd != "")
                     {
                         dgv_idou.CurrentCell.Value = w_cd;



                         if (dgv_idou.Rows[e.RowIndex].Cells[0].Value != null)
                         {
                             //torihikisaki_ckメソッド

                             string str = dgv_idou.Rows[e.RowIndex].Cells[0].Value.ToString();
                             string str2 = w_cd;

                             DataTable dt_w2 = new DataTable();
                             dt_w2 = tss.OracleSelect("select torihikisaki_cd from tss_buhin_m where buhin_cd  =  '" + str + "'");

                             if (dt_w2.Rows.Count == 0)
                             {
                                 MessageBox.Show("入力された移動先取引先コードが存在しません");
                                 return;
                             }
                             else
                             {
                                 string str3 = dt_w2.Rows[0][0].ToString();

                                 if (str2 == str3)
                                 {

                                 }
                                 else
                                 {
                                     DialogResult result = MessageBox.Show("移動する部品コードの取引先コードと部品マスタの取引先コードが異なりますがよろしいですか？",
                                       "部品入移動登録",
                                       MessageBoxButtons.OKCancel,
                                       MessageBoxIcon.Exclamation,
                                       MessageBoxDefaultButton.Button2);

                                     if (result == DialogResult.OK)
                                     {
                                         dgv_idou.EndEdit();
                                         //dgv_seihin_kousei.EndEdit();
                                         dgv_idou.Focus();

                                     }
                                     if (result == DialogResult.Cancel)
                                     {
                                         return;
                                     }
                                 }
                             }
                         }

                     }
                         dgv_idou.EndEdit();
                }
                else
                {
                    return;
                }

            }

            if (i == 4)
            {

                if (dgv_idou.CurrentRow.Cells[2].Value == null)
                {
                    MessageBox.Show("取引先コードを入力してください");
                    return;
                }
                
                if (dgv_idou.CurrentRow.Cells[3].Value == null && dgv_idou.CurrentRow.Cells[2].Value.ToString() != "01")
                {
                    MessageBox.Show("取引先コードを入力してください");
                    return;
                }
                
                if (dgv_idou.CurrentRow.Cells[2].Value == null || dgv_idou.CurrentRow.Cells[2].Value.ToString() != "01")
                {
                    //選択画面へ
                    string w_juchu_cd;
                    w_juchu_cd = tss.search_juchu("2", dgv_idou.CurrentRow.Cells[3].Value.ToString(), "", "", "");

                    if (w_juchu_cd.ToString() != "")
                    {
                        string str_w2 = w_juchu_cd.Substring(6, 16).TrimEnd();
                        string str_w3 = w_juchu_cd.Substring(22).TrimEnd();

                        dgv_idou.CurrentRow.Cells[i].Value = str_w2.ToString();
                        dgv_idou.CurrentRow.Cells[i + 1].Value = str_w3.ToString();
                        dgv_idou.EndEdit();
                    }
                }
                else
                {
                    return;
                }
            }
            
            if (i == 8)
            {
                if (dgv_idou.CurrentRow.Cells[6].Value == null)
                {
                    MessageBox.Show("移動先取引先コードを入力してください");
                    return;
                }
                
                if (dgv_idou.CurrentRow.Cells[7].Value == null && dgv_idou.CurrentRow.Cells[6].Value.ToString() != "01")
                {
                    MessageBox.Show("移動先取引先コードを入力してください");
                    return;
                }
                
                if (dgv_idou.CurrentRow.Cells[6].Value == null || dgv_idou.CurrentRow.Cells[6].Value.ToString() != "01")
                {
                    //選択画面へ
                    string w_juchu_cd;
                    w_juchu_cd = tss.search_juchu("2", dgv_idou.CurrentRow.Cells[7].Value.ToString(), "", "", "");

                    if (w_juchu_cd.ToString() != "")
                    {
                        string str_w2 = w_juchu_cd.Substring(6, 16).TrimEnd();
                        string str_w3 = w_juchu_cd.Substring(22).TrimEnd();

                        dgv_idou.CurrentRow.Cells[i].Value = str_w2.ToString();
                        dgv_idou.CurrentRow.Cells[i + 1].Value = str_w3.ToString();
                        dgv_idou.EndEdit();
                    }
                }
                else
                {
                    return;
                }
            }

        }

        private void dgv_idou_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            int i = e.ColumnIndex;
            int j = e.RowIndex;


            if (dgv_idou.CurrentCell.Value == null)
            { 
            
            }
            else
            {
                if (e.FormattedValue.ToString() == dgv_idou.CurrentCell.Value.ToString())
                {
                    return;
                }
            }
           


            if (tss.Check_String_Escape(e.FormattedValue.ToString()) == false)
            {
                e.Cancel = true;
                return;
            }

            if (i == 0)
            {
              
                　　string w_buhin_cd = e.FormattedValue.ToString();

                    if (w_buhin_cd != "")
                    {
                        DataTable dt_w = new DataTable();

                        dt_w = tss.OracleSelect("select torihikisaki_cd from TSS_BUHIN_M WHERE buhin_cd = '" + w_buhin_cd.ToString() + "'");

                        if (dt_w.Rows.Count == 0)
                        {
                            MessageBox.Show("この部品コードは登録されていません。部品登録してください。");
                            e.Cancel = true;
                            dgv_idou.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = "";
                            return;
                        }


                            //移動元取引先コードと移動先取引先コードがnullでない場合
                            if (dgv_idou.Rows[j].Cells[3].Value != null)
                            {
                                string str = dt_w.Rows[0][0].ToString();
                                string str2 = dgv_idou.Rows[j].Cells[3].Value.ToString();
                                //string str3 = dgv_idou.Rows[j].Cells[7].Value.ToString();

                                    if (str == str2)
                                    {

                                    }

                                    //if (str == str3)
                                    //{

                                    //}

                                    else
                                    {
                                        DialogResult result = MessageBox.Show("移動する部品コードの取引先コードと部品マスタの取引先コードが異なりますがよろしいですか？",
                                          "部品入移動登録",
                                          MessageBoxButtons.OKCancel,
                                          MessageBoxIcon.Exclamation,
                                          MessageBoxDefaultButton.Button2);

                                        if (result == DialogResult.OK)
                                        {
                                            dgv_idou.EndEdit();
                                            //dgv_seihin_kousei.EndEdit();
                                            dgv_idou.Focus();

                                        }
                                        if (result == DialogResult.Cancel)
                                        {
                                            e.Cancel = true;
                                            dgv_idou.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = "";
                                            return;
                                        }
                                    }
                                }

                            if (dgv_idou.Rows[j].Cells[7].Value != null)
                            {
                                string str = dt_w.Rows[0][0].ToString();
                                //string str2 = dgv_idou.Rows[j].Cells[3].Value.ToString();
                                string str3 = dgv_idou.Rows[j].Cells[7].Value.ToString();


                                if (str == str3)
                                {

                                }

                                else
                                {
                                    DialogResult result = MessageBox.Show("移動する部品コードの取引先コードと部品マスタの取引先コードが異なりますがよろしいですか？",
                                      "部品入移動登録",
                                      MessageBoxButtons.OKCancel,
                                      MessageBoxIcon.Exclamation,
                                      MessageBoxDefaultButton.Button2);

                                    if (result == DialogResult.OK)
                                    {
                                        dgv_idou.EndEdit();
                                        //dgv_seihin_kousei.EndEdit();
                                        dgv_idou.Focus();

                                    }
                                    if (result == DialogResult.Cancel)
                                    {
                                        e.Cancel = true;
                                        dgv_idou.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = "";
                                        return;
                                    }
                                }
                            }                
                            dgv_idou.Rows[e.RowIndex].Cells[i + 1].Value = tss.get_buhin_name(w_buhin_cd);
                            dgv_idou.EndEdit();
                        }
                     }

            if (i == 2)
            {
                string zaiko_kbn = e.FormattedValue.ToString();


                //在庫区分が01（フリー）なら、取引先コードと受注コード1、2はリードオンリーで色をグレーにする。
                if (zaiko_kbn != "02")
                {
                    dgv_idou[3, j].Value = null;
                    dgv_idou[4, j].Value = null;
                    dgv_idou[5, j].Value = null;
                    dgv_idou[3, j].Style.BackColor = Color.LightGray;
                    dgv_idou[4, j].Style.BackColor = Color.LightGray;
                    dgv_idou[5, j].Style.BackColor = Color.LightGray;
                    dgv_idou[3, j].ReadOnly = true;
                    dgv_idou[4, j].ReadOnly = true;
                    dgv_idou[5, j].ReadOnly = true;

                    dgv_idou.EndEdit();
                }
                else
                {
                    dgv_idou[3, j].Style.BackColor = Color.PowderBlue;
                    dgv_idou[4, j].Style.BackColor = Color.PowderBlue;
                    dgv_idou[5, j].Style.BackColor = Color.White;
                    dgv_idou[3, j].ReadOnly = false;
                    dgv_idou[4, j].ReadOnly = false;
                    dgv_idou[5, j].ReadOnly = false;

                    dgv_idou.EndEdit();
                }
            }

            if (i == 6)
            {
                string zaiko_kbn = e.FormattedValue.ToString();


                //在庫区分が01（フリー）なら、受注コード1、2はリードオンリーで色をグレーにする。
                if (zaiko_kbn != "02")
                {
                    dgv_idou[7, j].Value = null;
                    dgv_idou[8, j].Value = null;
                    dgv_idou[9, j].Value = null;
                    dgv_idou[7, j].Style.BackColor = Color.LightGray;
                    dgv_idou[8, j].Style.BackColor = Color.LightGray;
                    dgv_idou[9, j].Style.BackColor = Color.LightGray;
                    dgv_idou[7, j].ReadOnly = true;
                    dgv_idou[8, j].ReadOnly = true;
                    dgv_idou[9, j].ReadOnly = true;

                    dgv_idou.EndEdit();
                }
                else
                {
                    dgv_idou[7, j].Style.BackColor = Color.PowderBlue;
                    dgv_idou[8, j].Style.BackColor = Color.PowderBlue;
                    dgv_idou[9, j].Style.BackColor = Color.White;
                    dgv_idou[7, j].ReadOnly = false;
                    dgv_idou[8, j].ReadOnly = false;
                    dgv_idou[9, j].ReadOnly = false;

                    dgv_idou.EndEdit();
                }
            }

            if (i == 3 || i == 7)
            {
                
                if (e.FormattedValue == null || e.FormattedValue.ToString() == "")
                {
                    return;
                }
                if (e.FormattedValue == dgv_idou.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
                {
                    return;
                }
                else
                {
                    DataTable dt_w = new DataTable();
                    dt_w = tss.OracleSelect("select torihikisaki_name from tss_torihikisaki_m where torihikisaki_cd  =  '" + e.FormattedValue.ToString() + "'");

                    if (dt_w.Rows.Count == 0)
                    {
                        MessageBox.Show("入力された取引先コードが存在しません");
                        e.Cancel = true;
                        return;
                    }
                    else
                    {
                        if (dgv_idou.Rows[e.RowIndex].Cells[0].Value != null)
                        {
                            //torihikisaki_ck();

                            string str = dgv_idou.Rows[e.RowIndex].Cells[0].Value.ToString();
                            string str2 = e.FormattedValue.ToString();

                            DataTable dt_w2 = new DataTable();
                            dt_w2 = tss.OracleSelect("select torihikisaki_cd from tss_buhin_m where buhin_cd  =  '" + str + "'");

                            if (dt_w2.Rows.Count == 0)
                            {
                                MessageBox.Show("入力された移動先取引先コードが存在しません");
                                return;
                            }
                            else
                            {
                                string str3 = dt_w2.Rows[0][0].ToString();

                                if (str2 == str3)
                                {

                                }
                                else
                                {
                                    DialogResult result = MessageBox.Show("移動する部品コードの取引先コードと部品マスタの取引先コードが異なりますがよろしいですか？",
                                      "部品入移動登録",
                                      MessageBoxButtons.OKCancel,
                                      MessageBoxIcon.Exclamation,
                                      MessageBoxDefaultButton.Button2);

                                    if (result == DialogResult.OK)
                                    {
                                        dgv_idou.EndEdit();
                                        //dgv_seihin_kousei.EndEdit();
                                        dgv_idou.Focus();

                                    }
                                    if (result == DialogResult.Cancel)
                                    {
                                        e.Cancel = true;
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void tb_denpyou_no_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_denpyou_no.Text) == false)
            {
                e.Cancel = true;
                return;
            }
        }

        private void dgv_idou_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 10)
            {
                if (dgv_idou.Rows[e.RowIndex].Cells[10].Value != null && dgv_idou.Rows[e.RowIndex].Cells[10].Value.ToString() != "")
                {
                    dgv_idou.Rows[e.RowIndex].Cells[10].Value = tss.try_string_to_double(dgv_idou.Rows[e.RowIndex].Cells[10].Value.ToString()).ToString("#,0.00");
                }

            }
        }


        private void torihikisaki_dc_ck()
        {
           
        }

        private void btn_hardcopy_Click(object sender, EventArgs e)
        {
            tss.HardCopy();
        }


    }




     


}
