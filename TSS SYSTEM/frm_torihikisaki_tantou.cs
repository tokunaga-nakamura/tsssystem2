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
    public partial class frm_torihikisaki_tantou : Form
    {
        DataTable dt = new DataTable();
        TssSystemLibrary tss = new TssSystemLibrary();


        //親画面から参照できるプロパティを作成
        public string fld_torihikisaki_cd;    //選択された取引先コード
        public string fld_tantousya_cd;       //選択された担当者コード

        public string str_torihikisaki_cd
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
        public string str_tantousya_cd
        {
            get
            {
                return fld_tantousya_cd;
            }
            set
            {
                fld_tantousya_cd = value;
            }
        }


        public frm_torihikisaki_tantou()
        {
            InitializeComponent();
        }

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            //取引先コードのチェック
            if (chk_torihikisaki_cd() == false)
            {
                MessageBox.Show("取引先コード6文字で入力してください。");
                tb_torihikisaki_cd.Focus();
                return;
            }
            //取引先名のチェック
            if (chk_torihikisaki_name() == false)
            {
                MessageBox.Show("取引先名は1文字以上、40バイト以内で入力してください");
                tb_torihikisaki_cd.Focus();
                return;
            }
            //担当者コードのチェック
            if (chk_tantousya_cd() == false)
            {
                MessageBox.Show("担当者コードは6文字で入力してください。");
                tb_tantousya_cd.Focus();
                return;
            }
            //担当者名のチェック
            if (chk_tantousya_name() == false)
            {
                MessageBox.Show("取引先名は1文字以上、40バイト以内で入力してください");
                tb_tantousya_name.Focus();
                return;
            }
            //郵便番号のチェック
            if (chk_yubin_no() == false)
            {
                MessageBox.Show("郵便番号は10バイト以内で入力してください。");
                tb_yubin_no.Focus();
                return;
            }
            //住所1のチェック
            if (chk_jusyo1() == false)
            {
                MessageBox.Show("住所1は40バイト以内で入力してください。");
                tb_jusyo1.Focus();
                return;
            }
            //住所2のチェック
            if (chk_jusyo2() == false)
            {
                MessageBox.Show("住所2は40バイト以内で入力してください。");
                tb_jusyo2.Focus();
                return;
            }
            //電話番号のチェック
            if (chk_tel_no() == false)
            {
                MessageBox.Show("電話番号は20バイト以内で入力してください。");
                tb_tel_no.Focus();
                return;
            }
            //FAX番号のチェック
            if (chk_fax_no() == false)
            {
                MessageBox.Show("FAX番号は20バイト以内で入力してください。");
                tb_fax_no.Focus();
                return;
            }
            //メールアドレスのチェック
            if (chk_mail_address() == false)
            {
                MessageBox.Show("URLは60バイト以内で入力してください。");
                tb_mail_address.Focus();
                return;
            }



            else
            //書込み
            {
                tss.GetUser();
                bool bl_tss;
                //既存の区分があるかチェック
                DataTable dt_work = new DataTable();
                dt_work = tss.OracleSelect("select * from TSS_TORIHIKISAKI_TANTOU_M where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and tantousya_cd = '" + tb_tantousya_cd.Text + "'");
                
                if (dt_work.Rows.Count != 0)

                {      
                        DialogResult result = MessageBox.Show("この担当者コードは既に登録されています。上書きしますか？",
                        "担当者削除",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button2);

                        //何が選択されたか調べる
                        if (result == DialogResult.OK)
                        {
                            //「はい」が選択された時
                            tss.GetUser();
                        //更新
                        //bool bl_tss = true;
                        bl_tss = tss.OracleUpdate("UPDATE TSS_torihikisaki_tantou_m SET TORIHIKISAKI_NAME = '" + tb_torihikisaki_name.Text + "',TANTOUSYA_NAME = '" + tb_tantousya_name.Text
                            + "',YUBIN_NO = '" + tb_yubin_no.Text + "',JUSYO1 = '" + tb_jusyo1.Text + "',JUSYO2 = '" + tb_jusyo2.Text + "',TEL_NO = '" + tb_tel_no.Text
                            + "',FAX_NO = '" + tb_fax_no.Text + "',KEITAI_NO = '" + tb_keitai_no.Text + "',MAIL_ADDRESS = '" + tb_mail_address.Text
                            + "',SYOZOKU = '" + tb_syozoku.Text + "',YAKUSYOKU = '" + tb_yakusyoku.Text
                            + "',UPDATE_USER_CD = '" + tss.user_cd + "',UPDATE_DATETIME = SYSDATE WHERE torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and TANTOUSYA_CD = '" + tb_tantousya_cd.Text + "'");
                        if (bl_tss != true)
                        {
                            tss.ErrorLogWrite(tss.user_cd, "取引先担当者マスタ／登録", "登録ボタン押下時のOracleUpdate");
                            MessageBox.Show("登録でエラーが発生しました。処理を中止します。");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("取引先担当者情報を更新しました。");
                            this.Close();
                        }
                            }

           
                        else if (result == DialogResult.Cancel)
                        {
                            //「キャンセル」が選択された時
                            Console.WriteLine("「キャンセル」が選択されました");
                        }

                }
                else
                {
                    //新規
                    bl_tss = tss.OracleInsert("INSERT INTO TSS_TORIHIKISAKI_TANTOU_M (torihikisaki_cd,torihikisaki_name,tantousya_cd,tantousya_name,yubin_no,jusyo1,jusyo2,tel_no,fax_no,syozoku,yakusyoku,keitai_no,mail_address,create_user_cd) "
                                              + "VALUES ('" + tb_torihikisaki_cd.Text + "','" + tb_torihikisaki_name.Text + "','" + tb_tantousya_cd.Text + "','" + tb_tantousya_name.Text + "','" + tb_yubin_no.Text + "','" + tb_jusyo1.Text + "','" + tb_jusyo2.Text + "','" + tb_tel_no.Text + "','" + tb_fax_no.Text + "','" + tb_syozoku.Text + "','" + tb_yakusyoku.Text + "','" + tb_keitai_no.Text + "','" + tb_mail_address.Text + "','" + tss.user_cd + "')");
                    if (bl_tss != true)
                    {
                        tss.ErrorLogWrite(tss.UserID, "取引先担当者マスタ／登録", "登録ボタン押下時のOracleInsert");
                        MessageBox.Show("登録でエラーが発生しました。処理を中止します。");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("取引先担当者マスタに登録しました。");
                        this.Close();
                    }


                }

            }
        }



        private void frm_torihikisaki_tantou_Load(object sender, EventArgs e)
        {
            tb_torihikisaki_cd.Text = str_torihikisaki_cd;
            tb_tantousya_cd.Text = str_tantousya_cd;

            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from TSS_TORIHIKISAKI_TANTOU_M where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and tantousya_cd = '" + tb_tantousya_cd.Text + "'");

            if (dt_work.Rows.Count != 0)
            {
                tb_torihikisaki_name.Text = dt_work.Rows[0][2].ToString();
                tb_tantousya_name.Text = dt_work.Rows[0][3].ToString();
                tb_syozoku.Text = dt_work.Rows[0][9].ToString();
                tb_yakusyoku.Text = dt_work.Rows[0][10].ToString();
                tb_yubin_no.Text = dt_work.Rows[0][4].ToString();
                tb_jusyo1.Text = dt_work.Rows[0][5].ToString();
                tb_jusyo2.Text = dt_work.Rows[0][6].ToString();
                tb_tel_no.Text = dt_work.Rows[0][7].ToString();
                tb_fax_no.Text = dt_work.Rows[0][8].ToString();
                tb_keitai_no.Text = dt_work.Rows[0][11].ToString();
                tb_mail_address.Text = dt_work.Rows[0][12].ToString();
            }

            else
            {
                DataTable dt_work2 = new DataTable();
                dt_work2 = tss.OracleSelect("select torihikisaki_name from TSS_TORIHIKISAKI_M where torihikisaki_cd = '" + tb_torihikisaki_cd.Text  + "'");
                tb_torihikisaki_name.Text = dt_work2.Rows[0][0].ToString();
            }

        }

        //フォーム内のテキストボックスチェックメソッド
        private bool chk_torihikisaki_cd()
        {
            bool bl = true; //戻り値用

            if (tb_torihikisaki_cd.Text == null || tb_torihikisaki_cd.Text.Length > 6 || tb_torihikisaki_cd.Text.Length < 6)
            {
                bl = false;
            }
            return bl;
        }

        private bool chk_torihikisaki_name()
        {
            bool bl = true; //戻り値用

            if (tb_torihikisaki_name.Text == null || tb_torihikisaki_name.Text.Length == 0 || tss.StringByte(tb_torihikisaki_name.Text) > 40)
            {
                bl = false;
            }
            return bl;
        }
        private bool chk_tantousya_cd()
        {
            bool bl = true; //戻り値用

            if (tb_tantousya_cd.Text == null || tb_tantousya_cd.Text.Length > 6 || tb_tantousya_cd.Text.Length < 6)
            {
                bl = false;
            }
            return bl;
        }

        private bool chk_tantousya_name()
        {
            bool bl = true; //戻り値用

            if (tb_tantousya_name.Text == null || tb_tantousya_name.Text.Length == 0 || tss.StringByte(tb_tantousya_name.Text) > 40)
            {
                bl = false;
            }
            return bl;
        }
        private bool chk_yubin_no()
        {
            bool bl = true; //戻り値用

            if (tss.StringByte(tb_yubin_no.Text) > 10)
            {
                bl = false;
            }
            return bl;
        }

        private bool chk_jusyo1()
        {
            bool bl = true; //戻り値用

            if (tss.StringByte(tb_jusyo1.Text) > 40)
            {
                bl = false;
            }
            return bl;
        }

        private bool chk_jusyo2()
        {
            bool bl = true; //戻り値用

            if (tss.StringByte(tb_jusyo2.Text) > 40)
            {
                bl = false;
            }
            return bl;
        }

        private bool chk_tel_no()
        {
            bool bl = true; //戻り値用

            if (tss.StringByte(tb_tel_no.Text) > 20)
            {
                bl = false;
            }
            return bl;
        }

        private bool chk_fax_no()
        {
            bool bl = true; //戻り値用

            if (tss.StringByte(tb_fax_no.Text) > 20)
            {
                bl = false;
            }
            return bl;
        }

        private bool chk_mail_address()
        {
            bool bl = true; //戻り値用

            if (tss.StringByte(tb_mail_address.Text) > 60)
            {
                bl = false;
            }
            return bl;
        }

        private void btn_sakujyo_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("担当者情報を削除しますか？",
            "担当者削除",
            MessageBoxButtons.OKCancel,
            MessageBoxIcon.Exclamation,
            MessageBoxDefaultButton.Button2);

            //何が選択されたか調べる
            if (result == DialogResult.OK)
            {
                //「はい」が選択された時
                tss.GetUser();
                bool bl_tss;
                bl_tss = tss.OracleDelete("delete from TSS_TORIHIKISAKI_TANTOU_M where TANTOUSYA_CD = '" + tb_tantousya_cd.Text + "'");
                    if (bl_tss != true)
                    {
                        tss.ErrorLogWrite(tss.user_cd, "取引先担当者マスタ／登録", "登録ボタン押下時のOracleUpdate");
                        MessageBox.Show("エラーが発生しました。処理を中止します。");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("担当者情報から削除しました。");
                        this.Close();
                    }
            }

           
            else if (result == DialogResult.Cancel)
            {
                //「キャンセル」が選択された時
                Console.WriteLine("「キャンセル」が選択されました");
            }


        }

        private void bt_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
