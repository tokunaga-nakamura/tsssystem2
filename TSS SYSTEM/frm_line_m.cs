//  SYSTEM NAME     TSS SYSTEM
//  PROGRAM NAME    ラインマスタ
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
    public partial class frm_line_m : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable dt = new DataTable();

        public frm_line_m()
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

        private void line_m_disp()
        {
            dt = tss.OracleSelect("select line_cd,line_name,line_ryakusiki_name,bikou,delete_flg from tss_line_m order by line_cd asc");
            dgv_line_m.DataSource = null;
            if(dt.Rows.Count >= 1)
            {
                dgv_line_m.DataSource = dt;
                dgv_line_m.Columns[0].HeaderText = "ラインコード";
                dgv_line_m.Columns[1].HeaderText = "ライン名称";
                dgv_line_m.Columns[2].HeaderText = "ライン略式名称";
                dgv_line_m.Columns[3].HeaderText = "備考";
                dgv_line_m.Columns[4].HeaderText = "削除フラグ";
                dgv_line_m.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;   //カラム幅の自動調整
                dgv_line_m.AllowUserToResizeRows = false;    //セルの高さ変更不可
                dgv_line_m.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;    //カラムヘッダーの高さ変更不可
                dgv_line_m.ReadOnly = true; //編集不可
                dgv_line_m.AllowUserToDeleteRows = false;   //削除不可
                dgv_line_m.RowHeadersVisible = false;   //行ヘッダーを非表示にする
                dgv_line_m.AllowUserToAddRows = false;  //行を追加できないようにする（最下行を非表示にする）
            }
        }

        private void frm_line_m_Load(object sender, EventArgs e)
        {
            line_m_disp();
        }

        private void tb_line_cd_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_line_cd.Text) == false)
            {
                e.Cancel = true;
                return;
            }
            //空白の場合はOKとする
            if (tb_line_cd.Text != "")
            {
                if (chk_line_cd() != true)
                {
                    MessageBox.Show("ラインコードに異常があります。");
                    e.Cancel = true;
                }
            }
        }

        private bool chk_line_cd()
        {
            bool bl = true; //戻り値
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_line_m where line_cd  = '" + tb_line_cd.Text.ToString() + "'");
            if (dt_work.Rows.Count <= 0)
            {
                //新規
                gamen_sinki(tb_line_cd.Text);
                lbl_line_cd.Text = "新規のラインです。";
            }
            else
            {
                //既存データ有
                gamen_disp(dt_work);
                lbl_line_cd.Text = "既存のデータです。";
            }
            return bl;
        }

        private void gamen_sinki(string in_cd)
        {
            gamen_clear();
            tb_line_cd.Text = in_cd;
        }

        private void gamen_clear()
        {
            tb_line_cd.Text = "";
            tb_line_name.Text = "";
            tb_line_ryakusiki_name.Text = "";
            tb_bikou.Text = "";
            tb_delete_flg.Text = "";
            tb_create_user_cd.Text = "";
            tb_create_datetime.Text = "";
            tb_update_user_cd.Text = "";
            tb_update_datetime.Text = "";
            lbl_line_cd.Text = "ラインコードを入力してください";
        }

        private void gamen_disp(DataTable in_dt_work)
        {
            tb_line_cd.Text = in_dt_work.Rows[0]["line_cd"].ToString();
            tb_line_name.Text = in_dt_work.Rows[0]["line_name"].ToString();
            tb_line_ryakusiki_name.Text = in_dt_work.Rows[0]["line_ryakusiki_name"].ToString();
            tb_bikou.Text = in_dt_work.Rows[0]["bikou"].ToString();
            tb_delete_flg.Text = in_dt_work.Rows[0]["delete_flg"].ToString();
            tb_create_user_cd.Text = in_dt_work.Rows[0]["create_user_cd"].ToString();
            tb_create_datetime.Text = in_dt_work.Rows[0]["create_datetime"].ToString();
            tb_update_user_cd.Text = in_dt_work.Rows[0]["update_user_cd"].ToString();
            tb_update_datetime.Text = in_dt_work.Rows[0]["update_datetime"].ToString();

            if(tb_delete_flg.Text == "1")
            {
                tb_delete_flg.BackColor = Color.Red;
            }
            else
            {
                tb_delete_flg.BackColor = Color.Gainsboro;
            }
        }

        private void tb_line_name_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_line_name.Text) == false)
            {
                e.Cancel = true;
                return;
            }
        }

        private void tb_line_ryakusiki_name_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_line_ryakusiki_name.Text) == false)
            {
                e.Cancel = true;
                return;
            }
        }

        private void tb_bikou_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_bikou.Text) == false)
            {
                e.Cancel = true;
                return;
            }
        }

        private void btn_touroku_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(4, 5) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            DataTable dt_work = new DataTable();

            //登録前に全ての項目をチェック
            if (chk_line_name() == false)
            {
                MessageBox.Show("ライン名称は1文字以上、40バイト以内で入力してください。");
                tb_line_name.Focus();
                return;
            }

            if (chk_line_ryakusiki_name() == false)
            {
                MessageBox.Show("ライン略式名称は20バイト以内で入力してください。");
                tb_line_ryakusiki_name.Focus();
                return;
            }

            if (chk_bikou() == false)
            {
                MessageBox.Show("備考は128バイト以内で入力してください。");
                tb_bikou.Focus();
                return;
            }

            //部品コードの新規・更新チェック
            dt_work = tss.OracleSelect("select * from tss_line_m where line_cd  = '" + tb_line_cd.Text.ToString() + "'");
            if (dt_work.Rows.Count <= 0)
            {
                //新規
                DialogResult result = MessageBox.Show("新規に登録します。よろしいですか？", "確認", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    //「はい」が選択された時
                    line_insert();
                    //chk_line_cd();
                    gamen_clear();
                    tb_line_cd.Focus();
                    line_m_disp();
                }
                else
                {
                    //「いいえ」が選択された時
                    tb_line_name.Focus();
                }
            }
            else
            {
                //既存データ有
                DialogResult result = MessageBox.Show("既存データを更新します。よろしいですか？", "確認", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    //「はい」が選択された時
                    line_update();
                    chk_line_cd();
                }
                else
                {
                    //「いいえ」が選択された時
                    tb_line_name.Focus();
                }
            }

        }

        private bool chk_line_name()
        {
            bool bl = true; //戻り値用

            if (tb_line_name.Text == null || tb_line_name.Text.Length == 0 || tss.StringByte(tb_line_name.Text) > 40)
            {
                bl = false;
            }
            return bl;
        }

        private bool chk_line_ryakusiki_name()
        {
            bool bl = true; //戻り値用

            if (tb_line_ryakusiki_name.Text == null || tb_line_ryakusiki_name.Text.Length == 0 || tss.StringByte(tb_line_ryakusiki_name.Text) > 20)
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

        private void line_insert()
        {
            tss.GetUser();
            //新規書込み
            bool bl_tss = true;
            bl_tss = tss.OracleInsert("INSERT INTO tss_line_m (line_cd,line_name,line_ryakusiki_name,bikou,delete_flg,create_user_cd,create_datetime)"
                                    + " VALUES ('" + tb_line_cd.Text.ToString() + "','" + tb_line_name.Text.ToString() + "','" + tb_line_ryakusiki_name.Text.ToString() + "','" + tb_bikou.Text.ToString() + "','0','" + tss.user_cd + "',SYSDATE)");
            if (bl_tss != true)
            {
                tss.ErrorLogWrite(tss.user_cd, "ラインマスタ／登録", "登録ボタン押下時のOracleInsert");
                MessageBox.Show("書込みでエラーが発生しました。処理を中止します。");
                this.Close();
            }
            else
            {
                //MessageBox.Show("新規登録しました。");
            }
        }

        private void line_update()
        {
            tss.GetUser();
            //更新
            bool bl_tss = true;
            bl_tss = tss.OracleUpdate("UPDATE TSS_line_m SET line_name = '" + tb_line_name.Text.ToString() + "',line_ryakusiki_name = '" + tb_line_ryakusiki_name.Text.ToString()
                                    + "',bikou = '" + tb_bikou.Text.ToString()
                                    + "',UPDATE_USER_CD = '" + tss.user_cd + "',UPDATE_DATETIME = SYSDATE WHERE line_cd = '" + tb_line_cd.Text.ToString() + "'");
            if (bl_tss != true)
            {
                tss.ErrorLogWrite(tss.user_cd, "ラインマスタ／登録", "登録ボタン押下時のOracleUpdate");
                MessageBox.Show("書込みでエラーが発生しました。処理を中止します。");
                this.Close();
            }
            else
            {
                MessageBox.Show("更新しました。");
            }
        }

    }
}
