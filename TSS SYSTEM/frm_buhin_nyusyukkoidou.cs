﻿//  SYSTEM NAME     TSS SYSTEM
//  PROGRAM NAME    部品入出庫
//  CREATE          J.OKUDA
//  UPDATE LOG
//  2017/10/20  nakamura    tss.OracleXXXの処理後、エラー処理がされていない箇所が多数あり、エラー処理とログ出力記述を各所に追加（ついでにインデント、不要な空白行等コードの整理）

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
    /// <summary>
    /// <para>プロパティ str_mode 1:入庫モード　 2:出庫モード　//3:移動モード</para>
    /// </summary>
    
    public partial class frm_buhin_nyusyukkoidou : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable dt_m = new DataTable();

        string w_str = "";

        //親画面から参照できるプロパティを作成
        public string fld_mode; 　 //画面モード
        public string fld_cd;     //選択された部品コード
        
        public string str_mode
        {
            get
            {
                return fld_mode;
            }
            set
            {
                fld_mode = value;
            }
        }

        public string str_cd
        {
            get
            {
                return fld_cd;
            }
            set
            {
                fld_cd = value;
            }
        }

        public frm_buhin_nyusyukkoidou()
        {
            InitializeComponent();
        }

        public string in_cd
        {
            get
            {
                return in_cd;
            }
            set
            {
                in_cd = value;
            }
        }

        private void frm_buhin_nyusyukkoidou_Load(object sender, EventArgs e)
        {
            //モードによってフォームの表示内容を変える
            switch (str_mode)
            {
                case "1":
                    //入庫モード
                    mode1();
                    break;

                case "2":
                    //出庫モード
                    mode2();
                    break;
                
                //case "3":
                //    //移動モード
                //    mode3();
                //    break;
                
                default:
                    MessageBox.Show("画面モードのプロパティに異常があります。処理を中止します。");
                    //form_close_false();
                    break;
            }

            //SEQ更新メソッド
            SEQ();

            //データグリッドビューの部品名は編集不可
            dgv_nyusyukkoidou.Columns[1].ReadOnly = true;
            //dgv_nyusyukkoidou.Columns["Column7"].DefaultCellStyle.Format = "#,0";
            
            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            //dgv_nyusyukkoidou.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //セルの高さ変更不可
            dgv_nyusyukkoidou.AllowUserToResizeRows = false;
            //カラムヘッダーの高さ変更不可
            dgv_nyusyukkoidou.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //１行のみ選択可能（複数行の選択不可）
            dgv_nyusyukkoidou.MultiSelect = false;
            //カラム色の変更
            dgv_nyusyukkoidou.Columns[1].DefaultCellStyle.BackColor = Color.LightGray;
            //行ヘッダーを非表示にする
            dgv_nyusyukkoidou.RowHeadersVisible = true;
            //数量の小数点桁数
            dgv_nyusyukkoidou.Columns[5].DefaultCellStyle.Format = "#,0.00";
        }

        private void mode1()
        {
            label3.Text = "入庫処理";
            w_str = "01";
        }

        private void mode2()
        {
            label3.Text = "出庫処理";
            w_str = "02";
        }


        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        //取引先コード入力時の処理
        private void tb_torihikisaki_cd_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_torihikisaki_cd.Text) == false)
            {
                e.Cancel = true;
                return;
            }

            if (tb_torihikisaki_cd.Text == "")
            {
                tb_torihikisaki_name.Text = "";
                return;
            }

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
            }
            //データグリッドビューにフォーカス移動
            dgv_nyusyukkoidou.Focus();
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

       //登録ボタンが押された時の処理//////////////////////////////////////////////////////////////////////////////////////
        private void btn_touroku_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(4, 5) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }

            DataTable dt_work = new DataTable();

            //登録前に全ての項目をチェック
            //伝票番号
            if (chk_denpyou_no() == false)
            {
                MessageBox.Show("伝票番号の値が異常です");
                tb_denpyou_no.Focus();
                return;
            }
            //取引先コード
            if (chk_torihikisaki_cd() == false)
            {
                MessageBox.Show("取引先コードは6桁の数字で入力してください（空白不可）");
                tb_torihikisaki_cd.Focus();
                return;
            }

            //データグリッドビューの中を1行ずつループしてチェック
            int dgvrc = dgv_nyusyukkoidou.Rows.Count;

            if (dgvrc == 1)
            {
                MessageBox.Show("表の中に何も入力されていません");
                return;
            }

            tss.GetUser();  //ユーザー情報の取得

            //テキストボックスとデータグリッドビューの入力内容チェック
            for (int i = 0; i < dgvrc - 1; i++)
            {
                if (dgv_nyusyukkoidou.Rows[i].Cells[0].Value == null)
                {
                    MessageBox.Show("部品コードを入力してください");
                    dgv_nyusyukkoidou.Focus();
                    dgv_nyusyukkoidou.CurrentCell = dgv_nyusyukkoidou[0, i];
                    return;
                }

                if (dgv_nyusyukkoidou.Rows[i].Cells[2].Value == null)
                {
                    MessageBox.Show("在庫区分を入力してください");
                    return;
                }

                //在庫区分チェック
                dt_work = tss.OracleSelect("select * from tss_kubun_m where kubun_meisyou_cd  = '01' and kubun_cd = '" + dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() + "'");
                if (dt_work.Rows.Count <= 0)
                {
                    MessageBox.Show("入力された在庫区分が存在しません");
                    return;
                }
                //if (dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() != "01" && dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() != "02" && dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() != "03")
                //{
                //    MessageBox.Show("在庫区分は01～03で入力してください");
                //    return;
                //}
                if (dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() == "02" && dgv_nyusyukkoidou.Rows[i].Cells[3].Value == null)
                {
                    MessageBox.Show("受注コード1を入力してください");
                    return;
                }
                if (dgv_nyusyukkoidou.Rows[i].Cells[5].Value == null)
                {
                    MessageBox.Show("数量を入力してください");
                    return;
                }
                if (dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() != "02" && dgv_nyusyukkoidou.Rows[i].Cells[3].Value != null && dgv_nyusyukkoidou.Rows[i].Cells[4].Value != null)
                {
                    MessageBox.Show("在庫区分01の時は、受注コード1、2に入力できません。");
                    return;
                }
            }

            for (int i = 0; i < dgvrc - 1; i++)
            {
                //ロット在庫で、受注コード2が空白の場合、9999999999999999を代入
                if (dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() == "02" && dgv_nyusyukkoidou.Rows[i].Cells[3].Value != null && dgv_nyusyukkoidou.Rows[i].Cells[4].Value == null)
                {
                    dgv_nyusyukkoidou.Rows[i].Cells[4].Value = 9999999999999999;
                }

                //ロット在庫以外の場合、受注コード1、受注コード2に9999999999999999を代入
                if (dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() != "02" && dgv_nyusyukkoidou.Rows[i].Cells[3].Value == null && dgv_nyusyukkoidou.Rows[i].Cells[4].Value == null)
                {
                    dgv_nyusyukkoidou.Rows[i].Cells[3].Value = 9999999999999999;
                    dgv_nyusyukkoidou.Rows[i].Cells[4].Value = 9999999999999999;
                }
                //備考が空白の場合、""を代入（オラクルインサート時のnullエラー回避）
                if (dgv_nyusyukkoidou.Rows[i].Cells[6].Value == null)
                {
                    dgv_nyusyukkoidou.Rows[i].Cells[6].Value = "";
                }
            }

            //入庫モード/////////////////////////////////////////////////////////////////////////////////
            if (str_mode == "1")　
            {
                //データグリッドビューのレコードの行数分ループしてインサート
                int dgvrc2 = dgv_nyusyukkoidou.Rows.Count;
                for (int i = 0; i < dgvrc - 1; i++)
                {
                    //在庫区分が01以外（ロット在庫等）のとき
                    if (dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() != "01")
                    {
                        bool bl6 = tss.OracleInsert("INSERT INTO tss_buhin_nyusyukko_m (buhin_syori_kbn,buhin_syori_no,seq,buhin_syori_date,buhin_cd,zaiko_kbn,torihikisaki_cd,juchu_cd1,juchu_cd2,suryou,denpyou_no,barcode,syori_kbn,bikou,create_user_cd,create_datetime) VALUES ('"
                                        + "01" + "','"
                                        + tb_seq.Text.ToString() + "','"
                                        + (i + 1) + "',"
                                        + "to_date('" + dtp_buhin_syori_date.Value.ToString() + "','YYYY/MM/DD HH24:MI:SS'),'"
                                        + dgv_nyusyukkoidou.Rows[i].Cells[0].Value.ToString() + "','"
                                        + dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() + "','"
                                        + tb_torihikisaki_cd.Text.ToString() + "','"
                                        + dgv_nyusyukkoidou.Rows[i].Cells[3].Value.ToString() + "','"
                                        + dgv_nyusyukkoidou.Rows[i].Cells[4].Value.ToString() + "','"
                                        + decimal.Parse(dgv_nyusyukkoidou.Rows[i].Cells[5].Value.ToString()) + "','"
                                        + tb_denpyou_no.Text.ToString() + "','"
                                        + "" + "','"
                                        + "01" + "','"
                                        + dgv_nyusyukkoidou.Rows[i].Cells[6].Value.ToString() + "','"
                                        + tss.user_cd + "',SYSDATE)");
                        if (bl6 != true)
                        {
                            tss.ErrorLogWrite(tss.user_cd, "入出庫移動／登録", "登録ボタン押下時のOracleInsert 01");
                            MessageBox.Show("入庫処理でエラーが発生しました。" + Environment.NewLine + "処理を中止します。");
                            this.Close();
                        }
                    }

                    //在庫区分が01（フリー在庫等）のとき　取引先コードは999999にする。
                    if (dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() == "01")
                    {
                        bool bl6 = tss.OracleInsert("INSERT INTO tss_buhin_nyusyukko_m (buhin_syori_kbn,buhin_syori_no,seq,buhin_syori_date,buhin_cd,zaiko_kbn,torihikisaki_cd,juchu_cd1,juchu_cd2,suryou,denpyou_no,barcode,syori_kbn,bikou,create_user_cd,create_datetime) VALUES ('"
                                        + "01" + "','"
                                        + tb_seq.Text.ToString() + "','"
                                        + (i + 1) + "',"
                                        + "to_date('" + dtp_buhin_syori_date.Value.ToString() + "','YYYY/MM/DD HH24:MI:SS'),'"
                                        + dgv_nyusyukkoidou.Rows[i].Cells[0].Value.ToString() + "','"
                                        + dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() + "','"
                                        + 999999 + "','"
                                        + dgv_nyusyukkoidou.Rows[i].Cells[3].Value.ToString() + "','"
                                        + dgv_nyusyukkoidou.Rows[i].Cells[4].Value.ToString() + "','"
                                        + decimal.Parse(dgv_nyusyukkoidou.Rows[i].Cells[5].Value.ToString()) + "','"
                                        + tb_denpyou_no.Text.ToString() + "','"
                                        + "" + "','"
                                        + "01" + "','"
                                        + dgv_nyusyukkoidou.Rows[i].Cells[6].Value.ToString() + "','"
                                        + tss.user_cd + "',SYSDATE)");
                        if (bl6 != true)
                        {
                            tss.ErrorLogWrite(tss.user_cd, "入出庫移動／登録", "登録ボタン押下時のOracleInsert 02");
                            MessageBox.Show("入庫処理でエラーが発生しました。" + Environment.NewLine + "処理を中止します。");
                            this.Close();
                        }
                    }
                }

                //部品在庫マスタの更新
                //既存の区分があるかチェック
                int j = dgv_nyusyukkoidou.Rows.Count;
                DataTable dt_work5 = new DataTable();
                tss.GetUser();
                for (int i = 0; i < j - 1; i++)
                {
                    if (dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() != "01")
                    {
                        dt_work5 = tss.OracleSelect("select * from tss_buhin_zaiko_m where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "'and buhin_cd = '" + dgv_nyusyukkoidou.Rows[i].Cells[0].Value.ToString() + "'and zaiko_kbn = '" + dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() + "' and juchu_cd1 = '" + dgv_nyusyukkoidou.Rows[i].Cells[3].Value.ToString() + "'and juchu_cd2 = '" + dgv_nyusyukkoidou.Rows[i].Cells[4].Value.ToString() + "'");

                        if (dt_work5.Rows.Count == 0)
                        {
                            bool bl3 = tss.OracleInsert("insert into tss_buhin_zaiko_m (buhin_cd, zaiko_kbn,torihikisaki_cd, juchu_cd1, juchu_cd2, zaiko_su,create_user_cd,create_datetime) values ('"
                                                 + dgv_nyusyukkoidou.Rows[i].Cells[0].Value.ToString() + "','"
                                                 + dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() + "','"
                                                 + tb_torihikisaki_cd.Text.ToString() + "','"
                                                 + dgv_nyusyukkoidou.Rows[i].Cells[3].Value.ToString() + "','"
                                                 + dgv_nyusyukkoidou.Rows[i].Cells[4].Value.ToString() + "','"
                                                 + decimal.Parse(dgv_nyusyukkoidou.Rows[i].Cells[5].Value.ToString()) + "','"
                                                 + tss.user_cd + "',SYSDATE)");
                            if (bl3 != true)
                            {
                                tss.ErrorLogWrite(tss.user_cd, "入出庫移動／登録", "登録ボタン押下時のOracleInsert 03");
                                MessageBox.Show("入庫処理でエラーが発生しました。" + Environment.NewLine + "処理を中止します。");
                                this.Close();
                            }
                        }

                        if (dt_work5.Rows.Count != 0)
                        {
                            decimal zaikosu1 = decimal.Parse(dt_work5.Rows[0][5].ToString());
                            decimal zaikosu2 = decimal.Parse(dgv_nyusyukkoidou.Rows[i].Cells[5].Value.ToString());

                            decimal zaikosu3 = zaikosu1 + zaikosu2;

                            if (dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() != "01")
                            {
                                bool bl5 = tss.OracleUpdate("UPDATE TSS_BUHIN_ZAIKO_M SET ZAIKO_SU = '" + zaikosu3 + "',UPDATE_DATETIME = SYSDATE,UPDATE_USER_CD = '" + tss.user_cd + "' WHERE buhin_cd = '" + dgv_nyusyukkoidou.Rows[i].Cells[0].Value.ToString() + "' and torihikisaki_cd = '" + tb_torihikisaki_cd.Text.ToString() + "' and juchu_cd1 = '" + dgv_nyusyukkoidou.Rows[i].Cells[3].Value.ToString() + "' and juchu_cd2 = '" + dgv_nyusyukkoidou.Rows[i].Cells[4].Value.ToString() + "'");
                                if (bl5 != true)
                                {
                                    tss.ErrorLogWrite(tss.user_cd, "入出庫移動／登録", "登録ボタン押下時のOracleUpdate 01");
                                    MessageBox.Show("入庫処理でエラーが発生しました。" + Environment.NewLine + "処理を中止します。");
                                    this.Close();
                                }
                            }
                            if (dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() == "01")
                            {
                                bool bl5 = tss.OracleUpdate("UPDATE TSS_BUHIN_ZAIKO_M SET ZAIKO_SU = '" + zaikosu3 + "',UPDATE_DATETIME = SYSDATE,UPDATE_USER_CD = '" + tss.user_cd + "' WHERE buhin_cd = '" + dgv_nyusyukkoidou.Rows[i].Cells[0].Value.ToString() + "' and torihikisaki_cd = '" + 999999 + "' and juchu_cd1 = '" + dgv_nyusyukkoidou.Rows[i].Cells[3].Value.ToString() + "' and juchu_cd2 = '" + dgv_nyusyukkoidou.Rows[i].Cells[4].Value.ToString() + "'");
                                if (bl5 != true)
                                {
                                    tss.ErrorLogWrite(tss.user_cd, "入出庫移動／登録", "登録ボタン押下時のOracleUpdate 02");
                                    MessageBox.Show("入庫処理でエラーが発生しました。" + Environment.NewLine + "処理を中止します。");
                                    this.Close();
                                }
                            }
                        }
                    }

                    if (dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() == "01")
                    { 
                        dt_work5 = tss.OracleSelect("select * from tss_buhin_zaiko_m where torihikisaki_cd = '" + 999999 + "'and buhin_cd = '" + dgv_nyusyukkoidou.Rows[i].Cells[0].Value.ToString() + "'and zaiko_kbn = '" + dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() + "' and juchu_cd1 = '" + dgv_nyusyukkoidou.Rows[i].Cells[3].Value.ToString() + "'and juchu_cd2 = '" + dgv_nyusyukkoidou.Rows[i].Cells[4].Value.ToString() + "'");
                        if (dt_work5.Rows.Count == 0)
                        {
                            bool bl3 = tss.OracleInsert("insert into tss_buhin_zaiko_m (buhin_cd, zaiko_kbn,torihikisaki_cd, juchu_cd1, juchu_cd2, zaiko_su,create_user_cd,create_datetime) values ('"
                                                 + dgv_nyusyukkoidou.Rows[i].Cells[0].Value.ToString() + "','"
                                                 + dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() + "','"
                                                 + 999999 + "','"
                                                 + dgv_nyusyukkoidou.Rows[i].Cells[3].Value.ToString() + "','"
                                                 + dgv_nyusyukkoidou.Rows[i].Cells[4].Value.ToString() + "','"
                                                 + decimal.Parse(dgv_nyusyukkoidou.Rows[i].Cells[5].Value.ToString()) + "','"
                                                 + tss.user_cd + "',SYSDATE)");
                            if (bl3 != true)
                            {
                                tss.ErrorLogWrite(tss.user_cd, "入出庫移動／登録", "登録ボタン押下時のOracleInsert 04");
                                MessageBox.Show("入出庫処理でエラーが発生しました。" + Environment.NewLine + "処理を中止します。");
                                this.Close();
                            }
                        }

                        if (dt_work5.Rows.Count != 0)
                        {
                            decimal zaikosu1 = decimal.Parse(dt_work5.Rows[0][5].ToString());
                            decimal zaikosu2 = decimal.Parse(dgv_nyusyukkoidou.Rows[i].Cells[5].Value.ToString());

                            decimal zaikosu3 = zaikosu1 + zaikosu2;

                            if (dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() != "01")
                            {
                                bool bl5 = tss.OracleUpdate("UPDATE TSS_BUHIN_ZAIKO_M SET ZAIKO_SU = '" + zaikosu3 + "',UPDATE_DATETIME = SYSDATE,UPDATE_USER_CD = '" + tss.user_cd + "' WHERE buhin_cd = '" + dgv_nyusyukkoidou.Rows[i].Cells[0].Value.ToString() + "' and torihikisaki_cd = '" + tb_torihikisaki_cd.Text.ToString() + "' and juchu_cd1 = '" + dgv_nyusyukkoidou.Rows[i].Cells[3].Value.ToString() + "' and juchu_cd2 = '" + dgv_nyusyukkoidou.Rows[i].Cells[4].Value.ToString() + "'");
                                if (bl5 != true)
                                {
                                    tss.ErrorLogWrite(tss.user_cd, "入出庫移動／登録", "登録ボタン押下時のOracleUpdate 03");
                                    MessageBox.Show("入庫処理でエラーが発生しました。" + Environment.NewLine + "処理を中止します。");
                                    this.Close();
                                }
                            }

                            if (dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() == "01")
                            {
                                bool bl5 = tss.OracleUpdate("UPDATE TSS_BUHIN_ZAIKO_M SET ZAIKO_SU = '" + zaikosu3 + "',UPDATE_DATETIME = SYSDATE,UPDATE_USER_CD = '" + tss.user_cd + "' WHERE buhin_cd = '" + dgv_nyusyukkoidou.Rows[i].Cells[0].Value.ToString() + "' and torihikisaki_cd = '" + 999999 + "' and juchu_cd1 = '" + dgv_nyusyukkoidou.Rows[i].Cells[3].Value.ToString() + "' and juchu_cd2 = '" + dgv_nyusyukkoidou.Rows[i].Cells[4].Value.ToString() + "'");
                                if (bl5 != true)
                                {
                                    tss.ErrorLogWrite(tss.user_cd, "入出庫移動／登録", "登録ボタン押下時のOracleUpdate 04");
                                    MessageBox.Show("入庫処理でエラーが発生しました。" + Environment.NewLine + "処理を中止します。");
                                    this.Close();
                                }
                            }
                        }
                    }
                }
                MessageBox.Show("入庫処理されました。");

                SEQ();
                tb_denpyou_no.Clear();
                tb_torihikisaki_cd.Clear();
                tb_torihikisaki_name.Clear();
                dgv_nyusyukkoidou.Rows.Clear();
            }

            //出庫モード///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (str_mode == "2")
            {
                //レコードの行数分ループしてインサート
                int dgvrc2 = dgv_nyusyukkoidou.Rows.Count;

                for (int i = 0; i < dgvrc - 1; i++)
                {
                    if (dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() != "01")
                    {
                        bool bl6 = tss.OracleInsert("INSERT INTO tss_buhin_nyusyukko_m (buhin_syori_kbn,buhin_syori_no,seq,buhin_syori_date,buhin_cd,zaiko_kbn,torihikisaki_cd,juchu_cd1,juchu_cd2,suryou,denpyou_no,barcode,syori_kbn,bikou,create_user_cd,create_datetime) VALUES ('"
                                        + "02" + "','"
                                        + tb_seq.Text.ToString() + "','"
                                        + (i + 1) + "',"
                                        + "to_date('" + dtp_buhin_syori_date.Value.ToString() + "','YYYY/MM/DD HH24:MI:SS'),'"
                                        + dgv_nyusyukkoidou.Rows[i].Cells[0].Value.ToString() + "','"
                                        + dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() + "','"
                                        + tb_torihikisaki_cd.Text.ToString() + "','"
                                        + dgv_nyusyukkoidou.Rows[i].Cells[3].Value.ToString() + "','"
                                        + dgv_nyusyukkoidou.Rows[i].Cells[4].Value.ToString() + "','"
                                        + decimal.Parse(dgv_nyusyukkoidou.Rows[i].Cells[5].Value.ToString()) + "','"
                                        + tb_denpyou_no.Text.ToString() + "','"
                                        + "" + "','"
                                        + "01" + "','"
                                        + dgv_nyusyukkoidou.Rows[i].Cells[6].Value.ToString() + "','"
                                        + tss.user_cd + "',SYSDATE)");
                        if (bl6 != true)
                        {
                            tss.ErrorLogWrite(tss.user_cd, "入出庫移動／登録", "登録ボタン押下時のOracleInsert 05");
                            MessageBox.Show("出庫処理でエラーが発生しました。" + Environment.NewLine + "処理を中止します。");
                            this.Close();
                        }
                    }
                    if (dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() == "01")
                    {
                        bool bl6 = tss.OracleInsert("INSERT INTO tss_buhin_nyusyukko_m (buhin_syori_kbn,buhin_syori_no,seq,buhin_syori_date,buhin_cd,zaiko_kbn,torihikisaki_cd,juchu_cd1,juchu_cd2,suryou,denpyou_no,barcode,syori_kbn,bikou,create_user_cd,create_datetime) VALUES ('"
                                        + "02" + "','"
                                        + tb_seq.Text.ToString() + "','"
                                        + (i + 1) + "',"
                                        + "to_date('" + dtp_buhin_syori_date.Value.ToString() + "','YYYY/MM/DD HH24:MI:SS'),'"
                                        + dgv_nyusyukkoidou.Rows[i].Cells[0].Value.ToString() + "','"
                                        + dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() + "','"
                                        + 999999 + "','"
                                        + dgv_nyusyukkoidou.Rows[i].Cells[3].Value.ToString() + "','"
                                        + dgv_nyusyukkoidou.Rows[i].Cells[4].Value.ToString() + "','"
                                        + decimal.Parse(dgv_nyusyukkoidou.Rows[i].Cells[5].Value.ToString()) + "','"
                                        + tb_denpyou_no.Text.ToString() + "','"
                                        + "" + "','"
                                        + "01" + "','"
                                        + dgv_nyusyukkoidou.Rows[i].Cells[6].Value.ToString() + "','"
                                        + tss.user_cd + "',SYSDATE)");
                        if (bl6 != true)
                        {
                            tss.ErrorLogWrite(tss.user_cd, "入出庫移動／登録", "登録ボタン押下時のOracleInsert 06");
                            MessageBox.Show("出庫処理でエラーが発生しました。" + Environment.NewLine + "処理を中止します。");
                            this.Close();
                        }
                    }
                }

                //部品在庫マスタの更新
                //既存の区分があるかチェック
                int j = dgv_nyusyukkoidou.Rows.Count;
                DataTable dt_work5 = new DataTable();
                tss.GetUser();
                for (int i = 0; i < j - 1; i++)
                {
                    if (dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() != "01")
                    {
                        dt_work5 = tss.OracleSelect("select * from tss_buhin_zaiko_m where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "'and buhin_cd = '" + dgv_nyusyukkoidou.Rows[i].Cells[0].Value.ToString() + "'and zaiko_kbn = '" + dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() + "' and juchu_cd1 = '" + dgv_nyusyukkoidou.Rows[i].Cells[3].Value.ToString() + "'and juchu_cd2 = '" + dgv_nyusyukkoidou.Rows[i].Cells[4].Value.ToString() + "'");
                        if (dt_work5.Rows.Count == 0)
                        {
                            bool bl3 = tss.OracleInsert("insert into tss_buhin_zaiko_m (buhin_cd, zaiko_kbn,torihikisaki_cd, juchu_cd1, juchu_cd2, zaiko_su,create_user_cd,create_datetime) values ('"
                                             + dgv_nyusyukkoidou.Rows[i].Cells[0].Value.ToString() + "','"
                                             + dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() + "','"
                                             + tb_torihikisaki_cd.Text.ToString() + "','"
                                             + dgv_nyusyukkoidou.Rows[i].Cells[3].Value.ToString() + "','"
                                             + dgv_nyusyukkoidou.Rows[i].Cells[4].Value.ToString() + "','"
                                             + decimal.Parse(dgv_nyusyukkoidou.Rows[i].Cells[5].Value.ToString()) + "','"
                                             + tss.user_cd + "',SYSDATE)");
                            if (bl3 != true)
                            {
                                tss.ErrorLogWrite(tss.user_cd, "入出庫移動／登録", "登録ボタン押下時のOracleInsert 07");
                                MessageBox.Show("出庫処理でエラーが発生しました。" + Environment.NewLine + "処理を中止します。");
                                this.Close();
                            }
                        }

                        if (dt_work5.Rows.Count != 0)
                        {
                            decimal zaikosu1 = decimal.Parse(dt_work5.Rows[0][5].ToString());
                            decimal zaikosu2 = decimal.Parse(dgv_nyusyukkoidou.Rows[i].Cells[5].Value.ToString());
                            decimal zaikosu3 = zaikosu1 - zaikosu2;

                            if (dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() != "01")
                            {
                                bool bl5 = tss.OracleUpdate("UPDATE TSS_BUHIN_ZAIKO_M SET ZAIKO_SU = '" + zaikosu3 + "',UPDATE_DATETIME = SYSDATE,UPDATE_USER_CD = '" + tss.user_cd + "' WHERE buhin_cd = '" + dgv_nyusyukkoidou.Rows[i].Cells[0].Value.ToString() + "' and torihikisaki_cd = '" + tb_torihikisaki_cd.Text.ToString() + "' and juchu_cd1 = '" + dgv_nyusyukkoidou.Rows[i].Cells[3].Value.ToString() + "' and juchu_cd2 = '" + dgv_nyusyukkoidou.Rows[i].Cells[4].Value.ToString() + "'");
                                if (bl5 != true)
                                {
                                    tss.ErrorLogWrite(tss.user_cd, "入出庫移動／登録", "登録ボタン押下時のOracleUpdate 05");
                                    MessageBox.Show("出庫処理でエラーが発生しました。" + Environment.NewLine + "処理を中止します。");
                                    this.Close();
                                }
                            }

                            if (dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() == "01")
                            {
                                bool bl5 = tss.OracleUpdate("UPDATE TSS_BUHIN_ZAIKO_M SET ZAIKO_SU = '" + zaikosu3 + "',UPDATE_DATETIME = SYSDATE,UPDATE_USER_CD = '" + tss.user_cd + "' WHERE buhin_cd = '" + dgv_nyusyukkoidou.Rows[i].Cells[0].Value.ToString() + "' and torihikisaki_cd = '" + 999999 + "' and juchu_cd1 = '" + dgv_nyusyukkoidou.Rows[i].Cells[3].Value.ToString() + "' and juchu_cd2 = '" + dgv_nyusyukkoidou.Rows[i].Cells[4].Value.ToString() + "'");
                                if (bl5 != true)
                                {
                                    tss.ErrorLogWrite(tss.user_cd, "入出庫移動／登録", "登録ボタン押下時のOracleUpdate 06");
                                    MessageBox.Show("出庫処理でエラーが発生しました。" + Environment.NewLine + "処理を中止します。");
                                    this.Close();
                                }
                            }
                        }
                    }

                    if (dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() == "01")
                    {
                        dt_work5 = tss.OracleSelect("select * from tss_buhin_zaiko_m where torihikisaki_cd = '" + 999999 + "'and buhin_cd = '" + dgv_nyusyukkoidou.Rows[i].Cells[0].Value.ToString() + "'and zaiko_kbn = '" + dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() + "' and juchu_cd1 = '" + dgv_nyusyukkoidou.Rows[i].Cells[3].Value.ToString() + "'and juchu_cd2 = '" + dgv_nyusyukkoidou.Rows[i].Cells[4].Value.ToString() + "'");

                        if (dt_work5.Rows.Count == 0)
                        {
                            bool bl3 = tss.OracleInsert("insert into tss_buhin_zaiko_m (buhin_cd, zaiko_kbn,torihikisaki_cd, juchu_cd1, juchu_cd2, zaiko_su,create_user_cd,create_datetime) values ('"
                                              + dgv_nyusyukkoidou.Rows[i].Cells[0].Value.ToString() + "','"
                                              + dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() + "','"
                                              + 999999 + "','"
                                              + dgv_nyusyukkoidou.Rows[i].Cells[3].Value.ToString() + "','"
                                              + dgv_nyusyukkoidou.Rows[i].Cells[4].Value.ToString() + "','"
                                              + decimal.Parse(dgv_nyusyukkoidou.Rows[i].Cells[5].Value.ToString()) + "','"
                                              + tss.user_cd + "',SYSDATE)");
                            if (bl3 != true)
                            {
                                tss.ErrorLogWrite(tss.user_cd, "入出庫移動／登録", "登録ボタン押下時のOracleInsert 08");
                                MessageBox.Show("出庫処理でエラーが発生しました。" + Environment.NewLine + "処理を中止します。");
                                this.Close();
                            }
                        }
                        if (dt_work5.Rows.Count != 0)
                        {
                            decimal zaikosu1 = decimal.Parse(dt_work5.Rows[0][5].ToString());
                            decimal zaikosu2 = decimal.Parse(dgv_nyusyukkoidou.Rows[i].Cells[5].Value.ToString());

                            decimal zaikosu3 = zaikosu1 - zaikosu2;

                            if (dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() != "01")
                            {
                                bool bl5 = tss.OracleUpdate("UPDATE TSS_BUHIN_ZAIKO_M SET ZAIKO_SU = '" + zaikosu3 + "',UPDATE_DATETIME = SYSDATE,UPDATE_USER_CD = '" + tss.user_cd + "' WHERE buhin_cd = '" + dgv_nyusyukkoidou.Rows[i].Cells[0].Value.ToString() + "' and torihikisaki_cd = '" + tb_torihikisaki_cd.Text.ToString() + "' and juchu_cd1 = '" + dgv_nyusyukkoidou.Rows[i].Cells[3].Value.ToString() + "' and juchu_cd2 = '" + dgv_nyusyukkoidou.Rows[i].Cells[4].Value.ToString() + "'");
                                if (bl5 != true)
                                {
                                    tss.ErrorLogWrite(tss.user_cd, "入出庫移動／登録", "登録ボタン押下時のOracleUpdate 07");
                                    MessageBox.Show("出庫処理でエラーが発生しました。" + Environment.NewLine + "処理を中止します。");
                                    this.Close();
                                }
                            }

                            if (dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() == "01")
                            {
                                bool bl5 = tss.OracleUpdate("UPDATE TSS_BUHIN_ZAIKO_M SET ZAIKO_SU = '" + zaikosu3 + "',UPDATE_DATETIME = SYSDATE,UPDATE_USER_CD = '" + tss.user_cd + "' WHERE buhin_cd = '" + dgv_nyusyukkoidou.Rows[i].Cells[0].Value.ToString() + "' and torihikisaki_cd = '" + 999999 + "' and juchu_cd1 = '" + dgv_nyusyukkoidou.Rows[i].Cells[3].Value.ToString() + "' and juchu_cd2 = '" + dgv_nyusyukkoidou.Rows[i].Cells[4].Value.ToString() + "'");
                                if (bl5 != true)
                                {
                                    tss.ErrorLogWrite(tss.user_cd, "入出庫移動／登録", "登録ボタン押下時のOracleUpdate 08");
                                    MessageBox.Show("出庫処理でエラーが発生しました。" + Environment.NewLine + "処理を中止します。");
                                    this.Close();
                                }
                            }
                        }
                    }
                }
                MessageBox.Show("出庫処理されました。");

                SEQ();
                tb_denpyou_no.Clear();
                tb_torihikisaki_cd.Clear();
                tb_torihikisaki_name.Clear();
                dgv_nyusyukkoidou.Rows.Clear();
                    
                if (str_mode == "3")
                {
                    MessageBox.Show("str_mode=3 です。処理を中止します。");
                }
            }
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
        
        //取引先コードチェック用
        private bool chk_torihikisaki_cd()
        {
            bool bl = true; //戻り値用

            if (tb_torihikisaki_cd.Text == null || tb_torihikisaki_cd.Text.Length > 6)
            {
                bl = false;
            }
            return bl;
        }

        //データグリッドビューのセルの値が変わった時のイベント
        private void dgv_nyusyukkoidou_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            int i = e.RowIndex;
            
            if (dgv.Columns[e.ColumnIndex].Index == 0 && dgv.CurrentCell.Value == null)
            {
                dgv.Rows[i].Cells[1].Value = "";
                return;
            }

            if (e.ColumnIndex == 0)
            {
                if (tb_torihikisaki_cd.Text == "")
                {
                    if (dgv.CurrentCell.Value.ToString() == "")
                    {

                    }
                    else
                    {
                        MessageBox.Show("取引先コードが入力されていません");
                        dgv.Rows[i].Cells[0].Value = "";
                        dgv.Rows[i].Cells[1].Value = "";
                        tb_torihikisaki_cd.Focus();
                        return;
                    }
                }
            }

            //部品コードが入力されたならば、部品名を部品マスターから取得して表示
            if (dgv.Columns[e.ColumnIndex].Index == 0 && dgv.CurrentCell.Value.ToString() != "")
            {
                //int i = e.RowIndex;
                
                DataTable dtTmp = (DataTable)dgv_nyusyukkoidou.DataSource;

                //部品コードをキーに、部品名を引っ張ってくる

                DataTable dt_work = new DataTable();
                int j = dt_work.Rows.Count;
                dt_work = tss.OracleSelect("select * from tss_buhin_m where buhin_cd = '" + dgv.CurrentCell.Value.ToString() + "'");
                if (dt_work.Rows.Count <= 0)
                {
                    MessageBox.Show("この部品コードは登録されていません。部品登録してください。");
                    dgv.Rows[i].Cells[1].Value = "";
                }
                else
                {
                    dgv.Rows[i].Cells[1].Value = dt_work.Rows[j][1].ToString();
                    //dgv.CurrentCell = dgv.Rows[i].Cells[2]; //指定セルにフォーカスをあてるが、 コケるので使用しない
                }              
                return;
            }
        }
      
        //入出庫番号
        private void SEQ()
        {
            DataTable dt_work = new DataTable();
            decimal w_seq;
            w_seq = tss.GetSeq(w_str);
            if (w_seq == 0)
            {
                MessageBox.Show("連番マスタに異常があります。処理を中止します。");
                this.Close();
            }
            tb_seq.Text = (w_seq).ToString("0000000000");
        }

        //取引先コードダブルクリックイベント
        private void tb_torihikisaki_cd_DoubleClick(object sender, EventArgs e)
        {
            //選択画面へ
            string w_cd;
            w_cd = tss.search_torihikisaki("2", "");
            if (w_cd != "")
            {
                tb_torihikisaki_cd.Text = w_cd;
                tb_torihikisaki_name.Text = get_torihikisaki_name(tb_torihikisaki_cd.Text);
            }
        }

        //ハードコピーボタンクリックイベント
        private void btn_hardcopy_Click(object sender, EventArgs e)
        {
            tss.HardCopy();
        }

        //データグリッドビューのセルダブルクリックイベント
        private void dgv_nyusyukkoidou_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.ColumnIndex;
        
            if (i == 0)
            {
                if (tb_torihikisaki_cd.Text != "")
                {
                    //選択画面へ
                    string w_buhin_cd;
                    w_buhin_cd = tss.search_buhin("2", "");

                    if (w_buhin_cd != "")
                    {
                        dgv_nyusyukkoidou.CurrentCell.Value = w_buhin_cd;

                        string str1;
                        string str2;

                        DataTable dt_w = new DataTable();

                        dt_w = tss.OracleSelect("select torihikisaki_cd from TSS_BUHIN_M WHERE buhin_cd = '" + w_buhin_cd.ToString() + "'");
                        str1 = dt_w.Rows[0][0].ToString();

                        str2 = tb_torihikisaki_cd.Text.ToString();

                        if (dt_w.Rows.Count == 0)
                        {
                            return;
                        }
                        else
                        {
                            if (str1 != str2)
                            {
                                DialogResult result = MessageBox.Show("入出庫する部品コードの取引先コードと部品マスタの取引先コードが異なりますが登録しますか？",
                                "部品入出庫登録",
                                MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Exclamation,
                                MessageBoxDefaultButton.Button1);

                                if (result == DialogResult.OK)
                                {
                                    dgv_nyusyukkoidou.EndEdit();
                                    //dgv_seihin_kousei.EndEdit();
                                    dgv_nyusyukkoidou.Focus();
                                }
                                if (result == DialogResult.Cancel)
                                {
                                    dgv_nyusyukkoidou.Rows[e.RowIndex].Cells[i + 1].Value = "";
                                    return;
                                }
                            }
                        }
                        dgv_nyusyukkoidou.Rows[e.RowIndex].Cells[i + 1].Value = tss.get_buhin_name(w_buhin_cd);
                        dgv_nyusyukkoidou.EndEdit();
                    }
                }
                else
                {
                    MessageBox.Show("取引先コードが入力されていません");
                    dgv_nyusyukkoidou.Rows[e.RowIndex].Cells[i + 1].Value = "";
                    return;
                }
            }
            if (i == 3)
            {
                if (dgv_nyusyukkoidou.CurrentRow.Cells[2].Value == null || dgv_nyusyukkoidou.CurrentRow.Cells[2].Value.ToString() != "01")
                {
                    //選択画面へ
                    string w_juchu_cd;
                    w_juchu_cd = tss.search_juchu("2", tb_torihikisaki_cd.Text, "", "", "");

                    if (w_juchu_cd.ToString() != "")
                    {
                        string str_w2 = w_juchu_cd.Substring(6, 16).TrimEnd();
                        string str_w3 = w_juchu_cd.Substring(22).TrimEnd();

                        dgv_nyusyukkoidou.CurrentRow.Cells[i].Value = str_w2.ToString();
                        dgv_nyusyukkoidou.CurrentRow.Cells[i + 1].Value = str_w3.ToString();
                        dgv_nyusyukkoidou.EndEdit();
                    }
                }
                else
                {
                    dgv_nyusyukkoidou.EndEdit();
                    return;
                }
            }
        }

        //データグリッドビューのセル検証中イベント
        private void dgv_nyusyukkoidou_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            int i = e.ColumnIndex;
            int j = e.RowIndex;

            if (tss.Check_String_Escape(e.FormattedValue.ToString()) == false)
            {
                e.Cancel = true;
                return;
            }

            //部品コード検証イベント
            if (i == 0)
            {
                if (tb_torihikisaki_cd.Text != "")
                {
                    //選択画面へ
                    string w_buhin_cd = e.FormattedValue.ToString();

                    if (w_buhin_cd != "")
                    {
                        string str1;
                        string str2;

                        DataTable dt_w = new DataTable();

                        dt_w = tss.OracleSelect("select torihikisaki_cd from TSS_BUHIN_M WHERE buhin_cd = '" + w_buhin_cd.ToString() + "'");

                        if (dt_w.Rows.Count == 0)
                        {
                            MessageBox.Show("この部品コードは登録されていません。部品登録してください。");
                            e.Cancel = true;
                        }
                        else
                        {
                            str1 = dt_w.Rows[0][0].ToString();
                            str2 = tb_torihikisaki_cd.Text.ToString();

                            if (dt_w.Rows.Count == 0)
                            {
                                return;
                            }
                            else
                            {
                                if (dgv_nyusyukkoidou.Rows[e.RowIndex].Cells[0].Value == null)
                                {
                                    if (str1 != str2 && str1 != "000000")
                                    {
                                        MessageBox.Show("この部品は、この取引先コードで入庫できません");
                                        e.Cancel = true;
                                        dgv_nyusyukkoidou.Rows[e.RowIndex].Cells[i + 1].Value = "";
                                        return;
                                    }
                                    if (str1 != str2 && str1 == "000000")
                                    {
                                        DialogResult result = MessageBox.Show("入出庫する部品コードの取引先コードと部品マスタの取引先コードが異なりますが登録しますか？",
                                       "部品入出庫登録",
                                        MessageBoxButtons.OKCancel,
                                        MessageBoxIcon.Exclamation,
                                        MessageBoxDefaultButton.Button1);

                                        if (result == DialogResult.OK)
                                        {
                                            dgv_nyusyukkoidou.EndEdit();
                                            //dgv_seihin_kousei.EndEdit();
                                            dgv_nyusyukkoidou.Focus();
                                        }

                                        if (result == DialogResult.Cancel)
                                        {
                                            e.Cancel = true;
                                            dgv_nyusyukkoidou.Rows[e.RowIndex].Cells[i + 1].Value = "";
                                            return;
                                        }
                                    }
                                }
                                else
                                {
                                    if (e.FormattedValue.ToString() == dgv_nyusyukkoidou.Rows[e.RowIndex].Cells[0].Value.ToString())
                                    {

                                    }
                                }
                            }
                            dgv_nyusyukkoidou.Rows[e.RowIndex].Cells[i + 1].Value = tss.get_buhin_name(w_buhin_cd);
                            dgv_nyusyukkoidou.EndEdit();
                        }
                    }
                }
            }

            //在庫区分検証イベント
            if (i == 2)
            {
                string zaiko_kbn = e.FormattedValue.ToString();

                //在庫区分が02（ロット在庫）以外なら、受注コード1、2はリードオンリーで色をグレーにする。
                if (zaiko_kbn != "02")
                {
                    dgv_nyusyukkoidou[3,j].Value = null;
                    dgv_nyusyukkoidou[4,j].Value = null;
                    dgv_nyusyukkoidou[3,j].Style.BackColor = Color.LightGray;
                    dgv_nyusyukkoidou[4,j].Style.BackColor = Color.LightGray;
                    dgv_nyusyukkoidou[3,j].ReadOnly = true;
                    dgv_nyusyukkoidou[4,j].ReadOnly = true;

                    dgv_nyusyukkoidou.EndEdit();
                }
                //在庫区分が01（フリー）以外なら、受注コード1、2のリードオンリーを解除する。
                else
                {
                    dgv_nyusyukkoidou[3, j].Style.BackColor = Color.PowderBlue;
                    dgv_nyusyukkoidou[4, j].Style.BackColor = Color.White;
                    dgv_nyusyukkoidou[3, j].ReadOnly = false;
                    dgv_nyusyukkoidou[4, j].ReadOnly = false;

                    dgv_nyusyukkoidou.EndEdit();
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
      
        //データグリッドビューのセル検証後のイベント
        private void dgv_nyusyukkoidou_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            //数量セルの書式設定
            if(e.ColumnIndex == 5)
            {
                if (dgv_nyusyukkoidou.Rows[e.RowIndex].Cells[5].Value != null && dgv_nyusyukkoidou.Rows[e.RowIndex].Cells[5].Value.ToString() != "")
                {
                    dgv_nyusyukkoidou.Rows[e.RowIndex].Cells[5].Value = tss.try_string_to_decimal(dgv_nyusyukkoidou.Rows[e.RowIndex].Cells[5].Value.ToString()).ToString("#,0.00");
                }
            }
        }
    }
}
