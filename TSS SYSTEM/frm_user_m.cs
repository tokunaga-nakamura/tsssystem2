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
    public partial class frm_user_m : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();

        public frm_user_m()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tb_user_cd_Validating(object sender, CancelEventArgs e)
        {
            //空白の場合はOKとする
            if (tb_user_cd.Text != "")
            {
                if (chk_user_cd() != true)
                {
                    MessageBox.Show("ユーザーコードに異常があります。");
                    e.Cancel = true;
                }
            }
        }

        private bool chk_user_cd()
        {
            bool bl = true; //戻り値
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_user_m where user_cd  = '" + tb_user_cd.Text.ToString() + "'");
            if (dt_work.Rows.Count <= 0)
            {
                //新規
                gamen_sinki(tb_user_cd.Text);
            }
            else
            {
                //既存データ有
                gamen_disp(dt_work);
            }
            return bl;
        }

        private void gamen_sinki(string in_cd)
        {
            gamen_clear();
            tb_user_cd.Text = in_cd;
            lbl_user_cd.Text = "新規のユーザーです";
        }

        private void gamen_clear()
        {
            tb_user_cd.Text = "";
            tb_user_name.Text = "";
            tb_user_name2.Text = "";
            mtb_password.Text = "";
            tb_syain_kbn.Text = "";
            tb_syain_kbn_name.Text = "";
            tb_busyo_cd.Text = "";
            tb_busyo_cd_name.Text = "";
            tb_login_kyoka_kbn.Text = "";
            tb_kinmu_time1.Text = "";
            tb_kinmu_time2.Text = "";
            tb_kengen1.Text = "";
            tb_kengen2.Text = "";
            tb_kengen3.Text = "";
            tb_kengen4.Text = "";
            tb_kengen5.Text = "";
            tb_kengen6.Text = "";
            tb_login_flg.Text = "";
            tb_login_datetime.Text = "";
            tb_logout_datetime.Text = "";
            tb_bikou.Text = "";
            tb_create_user_cd.Text = "";
            tb_create_datetime.Text = "";
            tb_update_user_cd.Text = "";
            tb_update_datetime.Text = "";
        }

        private void gamen_disp(DataTable in_dt_work)
        {
            tb_user_cd.Text = in_dt_work.Rows[0]["user_cd"].ToString();
            tb_user_name.Text = in_dt_work.Rows[0]["user_name"].ToString();
            tb_user_name2.Text = in_dt_work.Rows[0]["user_name2"].ToString();
            mtb_password.Text = in_dt_work.Rows[0]["password"].ToString();
            tb_syain_kbn.Text = in_dt_work.Rows[0]["syain_kbn"].ToString();
            tb_syain_kbn_name.Text = get_syain_kbn(tb_syain_kbn.Text);
            tb_busyo_cd.Text = in_dt_work.Rows[0]["busyo_cd"].ToString();
            tb_busyo_cd_name.Text = get_busyo_cd(tb_busyo_cd.Text);
            tb_login_kyoka_kbn.Text = in_dt_work.Rows[0]["login_kyoka_kbn"].ToString();
            tb_login_kyoka_kbn_name.Text = get_login_kyoka_kbn(tb_login_kyoka_kbn.Text);
            tb_kinmu_time1.Text = in_dt_work.Rows[0]["kinmu_time1"].ToString();
            tb_kinmu_time2.Text = in_dt_work.Rows[0]["kinmu_time2"].ToString();
            tb_kengen1.Text = in_dt_work.Rows[0]["kengen1"].ToString();
            tb_kengen1_name.Text = get_kengen1(tb_kengen1.Text);
            tb_kengen2.Text = in_dt_work.Rows[0]["kengen2"].ToString();
            tb_kengen2_name.Text = get_kengen2(tb_kengen2.Text);
            tb_kengen3.Text = in_dt_work.Rows[0]["kengen3"].ToString();
            tb_kengen3_name.Text = get_kengen3(tb_kengen3.Text);
            tb_kengen4.Text = in_dt_work.Rows[0]["kengen4"].ToString();
            tb_kengen4_name.Text = get_kengen4(tb_kengen4.Text);
            tb_kengen5.Text = in_dt_work.Rows[0]["kengen5"].ToString();
            tb_kengen5_name.Text = get_kengen5(tb_kengen5.Text);
            tb_kengen6.Text = in_dt_work.Rows[0]["kengen6"].ToString();
            tb_kengen6_name.Text = get_kengen6(tb_kengen6.Text);
            tb_login_flg.Text = in_dt_work.Rows[0]["login_flg"].ToString();
            tb_login_datetime.Text = in_dt_work.Rows[0]["login_datetime"].ToString();
            tb_logout_datetime.Text = in_dt_work.Rows[0]["logout_datetime"].ToString();
            tb_bikou.Text = in_dt_work.Rows[0]["bikou"].ToString();
            tb_create_user_cd.Text = in_dt_work.Rows[0]["create_user_cd"].ToString();
            tb_create_datetime.Text = in_dt_work.Rows[0]["create_datetime"].ToString();
            tb_update_user_cd.Text = in_dt_work.Rows[0]["update_user_cd"].ToString();
            tb_update_datetime.Text = in_dt_work.Rows[0]["update_datetime"].ToString();
        }

        private string get_syain_kbn(string in_cd)
        {
            string out_name = "";  //戻り値用
            switch(in_cd)
            {
                case "0":
                    out_name = "未使用";
                    break;
                case "1":
                    out_name = "正社員";
                    break;
                case "2":
                    out_name = "パート";
                    break;
                case "3":
                    out_name = "嘱託";
                    break;
                case "4":
                    out_name = "派遣・アルバイト・臨時";
                    break;
                default:
                    out_name = "";
                    break;
            }
            return out_name;
        }

        private string get_busyo_cd(string in_cd)
        {
            string out_name = "";  //戻り値用

            //現在はまだ未使用の為、なんでもOKにする

            return out_name;
        }

        private string get_login_kyoka_kbn(string in_cd)
        {
            string out_name = "";  //戻り値用
            switch (in_cd)
            {
                case "0":
                    out_name = "許可しない";
                    break;
                case "1":
                    out_name = "許可する";
                    break;
                default:
                    out_name = "";
                    break;
            }
            return out_name;
        }

        private string get_kengen1(string in_cd)
        {
            string out_name = "";  //戻り値用
            switch (in_cd)
            {
                case "0":
                    out_name = "不可";
                    break;
                case "1":
                    out_name = "参照のみ";
                    break;
                case "9":
                    out_name = "すべて可";
                    break;
                default:
                    out_name = "";
                    break;
            }
            return out_name;
        }
        private string get_kengen2(string in_cd)
        {
            string out_name = "";  //戻り値用
            switch (in_cd)
            {
                case "0":
                    out_name = "不可";
                    break;
                case "1":
                    out_name = "参照のみ";
                    break;
                case "9":
                    out_name = "すべて可";
                    break;
                default:
                    out_name = "";
                    break;
            }
            return out_name;
        }
        private string get_kengen3(string in_cd)
        {
            string out_name = "";  //戻り値用
            switch (in_cd)
            {
                case "0":
                    out_name = "不可";
                    break;
                case "1":
                    out_name = "参照のみ";
                    break;
                case "9":
                    out_name = "すべて可";
                    break;
                default:
                    out_name = "";
                    break;
            }
            return out_name;
        }
        private string get_kengen4(string in_cd)
        {
            string out_name = "";  //戻り値用
            switch (in_cd)
            {
                case "0":
                    out_name = "不可";
                    break;
                case "1":
                    out_name = "参照のみ";
                    break;
                case "9":
                    out_name = "すべて可";
                    break;
                default:
                    out_name = "";
                    break;
            }
            return out_name;
        }
        private string get_kengen5(string in_cd)
        {
            string out_name = "";  //戻り値用
            switch (in_cd)
            {
                case "0":
                    out_name = "不可";
                    break;
                case "1":
                    out_name = "参照のみ";
                    break;
                case "9":
                    out_name = "すべて可";
                    break;
                default:
                    out_name = "";
                    break;
            }
            return out_name;
        }
        private string get_kengen6(string in_cd)
        {
            string out_name = "";  //戻り値用
            switch (in_cd)
            {
                case "0":
                    out_name = "不可";
                    break;
                case "1":
                    out_name = "参照のみ";
                    break;
                case "9":
                    out_name = "すべて可";
                    break;
                default:
                    out_name = "";
                    break;
            }
            return out_name;
        }

        private void tb_syain_kbn_Validating(object sender, CancelEventArgs e)
        {
            tb_syain_kbn_name.Text = get_syain_kbn(tb_syain_kbn.Text);
        }

        private void tb_busyo_cd_Validating(object sender, CancelEventArgs e)
        {
            tb_busyo_cd_name.Text = get_busyo_cd(tb_busyo_cd.Text);
        }
        private void tb_login_kyoka_kbn_Validating(object sender, CancelEventArgs e)
        {
            tb_login_kyoka_kbn_name.Text = get_login_kyoka_kbn(tb_login_kyoka_kbn.Text);
        }
        private void tb_kengen1_Validating(object sender, CancelEventArgs e)
        {
            tb_kengen1_name.Text = get_kengen1(tb_kengen1.Text);
        }

        private void tb_kengen2_Validating(object sender, CancelEventArgs e)
        {
            tb_kengen2_name.Text = get_kengen2(tb_kengen2.Text);
        }

        private void tb_kengen3_Validating(object sender, CancelEventArgs e)
        {
            tb_kengen3_name.Text = get_kengen3(tb_kengen3.Text);
        }

        private void tb_kengen4_Validating(object sender, CancelEventArgs e)
        {
            tb_kengen4_name.Text = get_kengen4(tb_kengen4.Text);
        }

        private void tb_kengen5_Validating(object sender, CancelEventArgs e)
        {
            tb_kengen5_name.Text = get_kengen5(tb_kengen5.Text);
        }

        private void tb_kengen6_Validating(object sender, CancelEventArgs e)
        {
            tb_kengen6_name.Text = get_kengen6(tb_kengen6.Text);
        }

        private void btn_hardcopy_Click(object sender, EventArgs e)
        {
            tss.HardCopy();
        }

        private void btn_touroku_Click(object sender, EventArgs e)
        {
            //登録前に全ての項目をチェック
            if (chk_user_name() == false)
            {
                MessageBox.Show("ユーザー名は1文字以上、40バイト以内で入力してください。");
                tb_user_name.Focus();
                return;
            }
            if (chk_user_name2() == false)
            {
                MessageBox.Show("略式名は1文字以上、10バイト以内で入力してください。");
                tb_user_name2.Focus();
                return;
            }
            if (chk_password() == false)
            {
                MessageBox.Show("パスワードは1文字以上、20バイト以内で入力してください。");
                mtb_password.Focus();
                return;
            }
            if (chk_syain_kbn() == false)
            {
                MessageBox.Show("社員区分に異常があります。");
                tb_syain_kbn.Focus();
                return;
            }
            if (chk_busyo_cd() == false)
            {
                MessageBox.Show("部署コードに異常があります。");
                tb_busyo_cd.Focus();
                return;
            }
            if (chk_kinmu_time1() == false)
            {
                MessageBox.Show("勤務開始時刻に異常があります。");
                tb_kinmu_time1.Focus();
                return;
            }
            if (chk_kinmu_time2() == false)
            {
                MessageBox.Show("勤務終了時刻に異常があります。");
                tb_kinmu_time2.Focus();
                return;
            }
            if (chk_kengen1() == false)
            {
                MessageBox.Show("権限（受注）に異常があります。");
                tb_kengen1.Focus();
                return;
            }
            if (chk_kengen2() == false)
            {
                MessageBox.Show("権限（売上）に異常があります。");
                tb_kengen2.Focus();
                return;
            }
            if (chk_kengen3() == false)
            {
                MessageBox.Show("権限（仕入）に異常があります。");
                tb_kengen3.Focus();
                return;
            }
            if (chk_kengen4() == false)
            {
                MessageBox.Show("権限（在庫）に異常があります。");
                tb_kengen4.Focus();
                return;
            }
            if (chk_kengen5() == false)
            {
                MessageBox.Show("権限（社内情報）に異常があります。");
                tb_kengen5.Focus();
                return;
            }
            if (chk_kengen6() == false)
            {
                MessageBox.Show("権限（マスタ）に異常があります。");
                tb_kengen6.Focus();
                return;
            }
            if (chk_bikou() == false)
            {
                MessageBox.Show("備考は128バイト以内で入力してください。");
                tb_bikou.Focus();
                return;
            }
            //新規・更新チェック
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_user_m where user_cd  = '" + tb_user_cd.Text.ToString() + "'");
            if (dt_work.Rows.Count <= 0)
            {
                //新規
                DialogResult result = MessageBox.Show("新規に登録します。よろしいですか？", "確認", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    //「はい」が選択された時
                    record_insert();
                    chk_user_cd();
                }
                else
                {
                    //「いいえ」が選択された時
                    return;
                }
            }
            else
            {
                //既存データ有
                DialogResult result = MessageBox.Show("既存データを更新します。よろしいですか？", "確認", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    //「はい」が選択された時
                    record_update();
                    chk_user_cd();
                }
                else
                {
                    //「いいえ」が選択された時
                    return;
                }
            }
        }

        private bool chk_user_name()
        {
            bool bl = true; //戻り値用
            if (tb_user_name.Text == null || tb_user_name.Text.Length == 0 || tss.StringByte(tb_user_name.Text) > 40)
            {
                bl = false;
            }
            return bl;
        }
        private bool chk_user_name2()
        {
            bool bl = true; //戻り値用
            if (tb_user_name2.Text == null || tb_user_name2.Text.Length == 0 || tss.StringByte(tb_user_name2.Text) > 10)
            {
                bl = false;
            }
            return bl;
        }
        private bool chk_password()
        {
            bool bl = true; //戻り値用
            if (mtb_password.Text == null || mtb_password.Text.Length == 0 || tss.StringByte(mtb_password.Text) > 20)
            {
                bl = false;
            }
            return bl;
        }
        private bool chk_syain_kbn()
        {
            bool bl = true; //戻り値用
            if (tb_syain_kbn.Text == null || tb_syain_kbn.Text.Length == 0 || tss.StringByte(tb_syain_kbn.Text) > 1)
            {
                bl = false;
            }
            if (get_syain_kbn(tb_syain_kbn.Text) == "")
            {
                bl = false;
            }
            return bl;
        }
        private bool chk_busyo_cd()
        {
            bool bl = true; //戻り値用
            //現在、部署コードは未使用なので、なんでもOKにする
            return bl;
        }
        private bool chk_login_kyoka_kbn()
        {
            bool bl = true; //戻り値用
            if (tb_login_kyoka_kbn.Text == null || tb_login_kyoka_kbn.Text.Length == 0 || tss.StringByte(tb_login_kyoka_kbn.Text) > 1)
            {
                bl = false;
            }
            if (get_login_kyoka_kbn(tb_login_kyoka_kbn.Text) == "")
            {
                bl = false;
            }
            return bl;
        }
        private bool chk_kinmu_time1()
        {
            bool bl = true; //戻り値用
            //現在、勤務開始時刻は未使用なので、なんでもOKにする
            //とりあえず文字数制限で落ちるといけないので、文字数だけチェック
            if (tss.StringByte(tb_kinmu_time1.Text) > 5)
            {
                bl = false;
            }
            return bl;
        }
        private bool chk_kinmu_time2()
        {
            bool bl = true; //戻り値用
            //現在、勤務開始時刻は未使用なので、なんでもOKにする
            //とりあえず文字数制限で落ちるといけないので、文字数だけチェック
            if (tss.StringByte(tb_kinmu_time2.Text) > 5)
            {
                bl = false;
            }
            return bl;
        }
        private bool chk_kengen1()
        {
            bool bl = true; //戻り値用
            if (tb_kengen1.Text == null || tb_kengen1.Text.Length == 0 || tss.StringByte(tb_kengen1.Text) > 1)
            {
                bl = false;
            }
            if (get_kengen1(tb_kengen1.Text) == "")
            {
                bl = false;
            }
            return bl;
        }
        private bool chk_kengen2()
        {
            bool bl = true; //戻り値用
            if (tb_kengen2.Text == null || tb_kengen2.Text.Length == 0 || tss.StringByte(tb_kengen2.Text) > 1)
            {
                bl = false;
            }
            if (get_kengen2(tb_kengen2.Text) == "")
            {
                bl = false;
            }
            return bl;
        }
        private bool chk_kengen3()
        {
            bool bl = true; //戻り値用
            if (tb_kengen3.Text == null || tb_kengen3.Text.Length == 0 || tss.StringByte(tb_kengen3.Text) > 1)
            {
                bl = false;
            }
            if (get_kengen3(tb_kengen1.Text) == "")
            {
                bl = false;
            }
            return bl;
        }
        private bool chk_kengen4()
        {
            bool bl = true; //戻り値用
            if (tb_kengen4.Text == null || tb_kengen4.Text.Length == 0 || tss.StringByte(tb_kengen4.Text) > 1)
            {
                bl = false;
            }
            if (get_kengen4(tb_kengen1.Text) == "")
            {
                bl = false;
            }
            return bl;
        }
        private bool chk_kengen5()
        {
            bool bl = true; //戻り値用
            if (tb_kengen5.Text == null || tb_kengen5.Text.Length == 0 || tss.StringByte(tb_kengen5.Text) > 1)
            {
                bl = false;
            }
            if (get_kengen5(tb_kengen1.Text) == "")
            {
                bl = false;
            }
            return bl;
        }
        private bool chk_kengen6()
        {
            bool bl = true; //戻り値用
            if (tb_kengen6.Text == null || tb_kengen6.Text.Length == 0 || tss.StringByte(tb_kengen6.Text) > 1)
            {
                bl = false;
            }
            if (get_kengen6(tb_kengen1.Text) == "")
            {
                bl = false;
            }
            return bl;
        }
        private bool chk_bikou()
        {
            bool bl = true; //戻り値用
            if (tss.StringByte(tb_bikou.Text) > 128)
            {
                bl = false;
            }
            return bl;
        }

        private void record_insert()
        {
            tss.GetUser();
            //新規書込み
            bool bl_tss = true;
            bl_tss = tss.OracleInsert("INSERT INTO tss_user_m (user_cd,user_name,user_name2,password,syain_kbn,busyo_cd,login_kyoka_kbn,kinmu_time1,kinmu_time2,kengen1,kengen2,kengen3,kengen4,kengen5,kengen6,bikou,create_user_cd,create_datetime)"
                                    + " VALUES ('" + tb_user_cd.Text.ToString() + "','" + tb_user_name.Text.ToString() + "','" + tb_user_name2.Text.ToString() + "','" + mtb_password.Text.ToString() + "','" + tb_syain_kbn.Text.ToString() + "','" + tb_busyo_cd.Text.ToString() + "','" + tb_login_kyoka_kbn.Text.ToString() + "','" + tb_kinmu_time1.Text.ToString() + "','" + tb_kinmu_time2.Text.ToString() + "','" + tb_kengen1.Text.ToString() + "','" + tb_kengen2.Text.ToString() + "','" + tb_kengen3.Text.ToString() + "','" + tb_kengen4.Text.ToString() + "','" + tb_kengen5.Text.ToString() + "','" + tb_kengen6.Text.ToString() + "','" + tb_bikou.Text.ToString() + "','" + tss.user_cd + "',SYSDATE)");
            if (bl_tss != true)
            {
                tss.ErrorLogWrite(tss.user_cd, "ユーザーマスタ／登録", "登録ボタン押下時のOracleInsert");
                MessageBox.Show("書込みでエラーが発生しました。処理を中止します。");
                this.Close();
            }
            else
            {
                MessageBox.Show("新規登録しました。");
            }
        }


        private void record_update()
        {
            tss.GetUser();
            //更新
            bool bl_tss = true;
            bl_tss = tss.OracleUpdate("UPDATE TSS_user_m SET user_name = '" + tb_user_name.Text.ToString() + "',user_name2 = '" + tb_user_name2.Text.ToString()
                + "',password = '" + mtb_password.Text.ToString() + "',syain_kbn = '" + tb_syain_kbn.Text.ToString() + "',busyo_cd = '" + tb_busyo_cd.Text.ToString()
                + "',login_kyoka_kbn = '" + tb_login_kyoka_kbn.Text.ToString() + "',kinmu_time1 = '" + tb_kinmu_time1.Text.ToString() + "',kinmu_time2 = '" + tb_kinmu_time2.Text.ToString()
                + "',kengen1 = '" + tb_kengen1.Text.ToString() + "',kengen2 = '" + tb_kengen2.Text.ToString() + "',kengen3 = '" + tb_kengen3.Text.ToString()
                + "',kengen4 = '" + tb_kengen4.Text.ToString() + "',kengen5 = '" + tb_kengen5.Text.ToString() + "',kengen6 = '" + tb_kengen6.Text.ToString()
                + "',bikou = '" + tb_bikou.Text.ToString() + "',UPDATE_USER_CD = '" + tss.user_cd + "',UPDATE_DATETIME = SYSDATE "
                + "WHERE user_cd = '" + tb_user_cd.Text.ToString() + "'");
            if (bl_tss != true)
            {
                tss.ErrorLogWrite(tss.user_cd, "ユーザーマスタ／登録", "登録ボタン押下時のOracleUpdate");
                MessageBox.Show("書込みでエラーが発生しました。処理を中止します。");
                this.Close();
            }
            else
            {
                MessageBox.Show("更新しました。");
            }
        }

        private void tb_syain_kbn_DoubleClick(object sender, EventArgs e)
        {
            //選択用のdatatableの作成
            DataTable dt_work = new DataTable();
            //列の定義
            dt_work.Columns.Add("区分");
            dt_work.Columns.Add("区分名");
            //行追加
            DataRow dr_work = dt_work.NewRow();
            dr_work["区分"] = "0";
            dr_work["区分名"] = "未使用";
            dt_work.Rows.Add(dr_work);

            dr_work = dt_work.NewRow();
            dr_work["区分"] = "1";
            dr_work["区分名"] = "正社員";
            dt_work.Rows.Add(dr_work);
            
            dr_work = dt_work.NewRow();
            dr_work["区分"] = "2";
            dr_work["区分名"] = "パート";
            dt_work.Rows.Add(dr_work);
            
            dr_work = dt_work.NewRow();
            dr_work["区分"] = "3";
            dr_work["区分名"] = "嘱託";
            dt_work.Rows.Add(dr_work);
            
            dr_work = dt_work.NewRow();
            dr_work["区分"] = "4";
            dr_work["区分名"] = "派遣・アルバイト・臨時";
            dt_work.Rows.Add(dr_work);
            
            //選択画面へ
            this.tb_syain_kbn.Text = tss.kubun_cd_select_dt("社員区分", dt_work,tb_syain_kbn.Text);
            tb_syain_kbn_name.Text = get_syain_kbn(tb_syain_kbn.Text);
        }

        private void tb_login_kyoka_kbn_DoubleClick(object sender, EventArgs e)
        {
            //選択用のdatatableの作成
            DataTable dt_work = new DataTable();
            //列の定義
            dt_work.Columns.Add("区分");
            dt_work.Columns.Add("区分名");
            //行追加
            DataRow dr_work = dt_work.NewRow();
            dr_work["区分"] = "0";
            dr_work["区分名"] = "許可しない";
            dt_work.Rows.Add(dr_work);

            dr_work = dt_work.NewRow();
            dr_work["区分"] = "1";
            dr_work["区分名"] = "許可する";
            dt_work.Rows.Add(dr_work);

            //選択画面へ
            this.tb_login_kyoka_kbn.Text = tss.kubun_cd_select_dt("ログイン許可区分", dt_work,tb_login_kyoka_kbn.Text);
            tb_login_kyoka_kbn_name.Text = get_login_kyoka_kbn(tb_login_kyoka_kbn.Text);
        }

        private void tb_kengen1_DoubleClick(object sender, EventArgs e)
        {
            //選択用のdatatableの作成
            DataTable dt_work = new DataTable();
            //列の定義
            dt_work.Columns.Add("区分");
            dt_work.Columns.Add("区分名");
            //行追加
            DataRow dr_work = dt_work.NewRow();
            dr_work["区分"] = "0";
            dr_work["区分名"] = "不可";
            dt_work.Rows.Add(dr_work);

            dr_work = dt_work.NewRow();
            dr_work["区分"] = "1";
            dr_work["区分名"] = "参照のみ";
            dt_work.Rows.Add(dr_work);

            dr_work = dt_work.NewRow();
            dr_work["区分"] = "9";
            dr_work["区分名"] = "すべて可";
            dt_work.Rows.Add(dr_work);

            //選択画面へ
            this.tb_kengen1.Text = tss.kubun_cd_select_dt("権限（受注）", dt_work,tb_kengen1.Text);
            tb_kengen1_name.Text = get_kengen1(tb_kengen1.Text);
        }

        private void tb_kengen2_DoubleClick(object sender, EventArgs e)
        {
            //選択用のdatatableの作成
            DataTable dt_work = new DataTable();
            //列の定義
            dt_work.Columns.Add("区分");
            dt_work.Columns.Add("区分名");
            //行追加
            DataRow dr_work = dt_work.NewRow();
            dr_work["区分"] = "0";
            dr_work["区分名"] = "不可";
            dt_work.Rows.Add(dr_work);

            dr_work = dt_work.NewRow();
            dr_work["区分"] = "1";
            dr_work["区分名"] = "参照のみ";
            dt_work.Rows.Add(dr_work);

            dr_work = dt_work.NewRow();
            dr_work["区分"] = "9";
            dr_work["区分名"] = "すべて可";
            dt_work.Rows.Add(dr_work);

            //選択画面へ
            this.tb_kengen2.Text = tss.kubun_cd_select_dt("権限（売上）", dt_work, tb_kengen2.Text);
            tb_kengen2_name.Text = get_kengen2(tb_kengen2.Text);
        }

        private void tb_kengen3_DoubleClick(object sender, EventArgs e)
        {
            //選択用のdatatableの作成
            DataTable dt_work = new DataTable();
            //列の定義
            dt_work.Columns.Add("区分");
            dt_work.Columns.Add("区分名");
            //行追加
            DataRow dr_work = dt_work.NewRow();
            dr_work["区分"] = "0";
            dr_work["区分名"] = "不可";
            dt_work.Rows.Add(dr_work);

            dr_work = dt_work.NewRow();
            dr_work["区分"] = "1";
            dr_work["区分名"] = "参照のみ";
            dt_work.Rows.Add(dr_work);

            dr_work = dt_work.NewRow();
            dr_work["区分"] = "9";
            dr_work["区分名"] = "すべて可";
            dt_work.Rows.Add(dr_work);

            //選択画面へ
            this.tb_kengen3.Text = tss.kubun_cd_select_dt("権限（仕入）", dt_work, tb_kengen3.Text);
            tb_kengen3_name.Text = get_kengen3(tb_kengen3.Text);
        }

        private void tb_kengen4_DoubleClick(object sender, EventArgs e)
        {
            //選択用のdatatableの作成
            DataTable dt_work = new DataTable();
            //列の定義
            dt_work.Columns.Add("区分");
            dt_work.Columns.Add("区分名");
            //行追加
            DataRow dr_work = dt_work.NewRow();
            dr_work["区分"] = "0";
            dr_work["区分名"] = "不可";
            dt_work.Rows.Add(dr_work);

            dr_work = dt_work.NewRow();
            dr_work["区分"] = "1";
            dr_work["区分名"] = "参照のみ";
            dt_work.Rows.Add(dr_work);

            dr_work = dt_work.NewRow();
            dr_work["区分"] = "9";
            dr_work["区分名"] = "すべて可";
            dt_work.Rows.Add(dr_work);

            //選択画面へ
            this.tb_kengen4.Text = tss.kubun_cd_select_dt("権限（在庫）", dt_work, tb_kengen4.Text);
            tb_kengen4_name.Text = get_kengen4(tb_kengen4.Text);
        }

        private void tb_kengen5_DoubleClick(object sender, EventArgs e)
        {
            //選択用のdatatableの作成
            DataTable dt_work = new DataTable();
            //列の定義
            dt_work.Columns.Add("区分");
            dt_work.Columns.Add("区分名");
            //行追加
            DataRow dr_work = dt_work.NewRow();
            dr_work["区分"] = "0";
            dr_work["区分名"] = "不可";
            dt_work.Rows.Add(dr_work);

            dr_work = dt_work.NewRow();
            dr_work["区分"] = "1";
            dr_work["区分名"] = "参照のみ";
            dt_work.Rows.Add(dr_work);

            dr_work = dt_work.NewRow();
            dr_work["区分"] = "9";
            dr_work["区分名"] = "すべて可";
            dt_work.Rows.Add(dr_work);

            //選択画面へ
            this.tb_kengen5.Text = tss.kubun_cd_select_dt("権限（社内情報）", dt_work, tb_kengen5.Text);
            tb_kengen5_name.Text = get_kengen5(tb_kengen5.Text);
        }

        private void tb_kengen6_DoubleClick(object sender, EventArgs e)
        {
            //選択用のdatatableの作成
            DataTable dt_work = new DataTable();
            //列の定義
            dt_work.Columns.Add("区分");
            dt_work.Columns.Add("区分名");
            //行追加
            DataRow dr_work = dt_work.NewRow();
            dr_work["区分"] = "0";
            dr_work["区分名"] = "不可";
            dt_work.Rows.Add(dr_work);

            dr_work = dt_work.NewRow();
            dr_work["区分"] = "1";
            dr_work["区分名"] = "参照のみ";
            dt_work.Rows.Add(dr_work);

            dr_work = dt_work.NewRow();
            dr_work["区分"] = "9";
            dr_work["区分名"] = "すべて可";
            dt_work.Rows.Add(dr_work);

            //選択画面へ
            this.tb_kengen6.Text = tss.kubun_cd_select_dt("権限（マスタ）", dt_work, tb_kengen6.Text);
            tb_kengen6_name.Text = get_kengen6(tb_kengen6.Text);
        }








    }
}
