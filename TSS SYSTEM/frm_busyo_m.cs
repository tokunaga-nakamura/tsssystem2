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
    public partial class frm_busyo_m : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable dt = new DataTable();

        public frm_busyo_m()
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

        private void frm_busyo_m_Load(object sender, EventArgs e)
        {
            busyo_m_disp();
        }

        private void busyo_m_disp()
        {
            dt = tss.OracleSelect("select busyo_cd,busyo_name,busyo_ryakusiki_name,ninzu,kousu,bikou,delete_flg from tss_busyo_m order by busyo_cd asc");
            dgv_busyo_m.DataSource = null;
            if (dt.Rows.Count >= 1)
            {
                dgv_busyo_m.DataSource = dt;
                dgv_busyo_m.Columns[0].HeaderText = "部署コード";
                dgv_busyo_m.Columns[1].HeaderText = "部署名称";
                dgv_busyo_m.Columns[2].HeaderText = "部署略式名称";
                dgv_busyo_m.Columns[3].HeaderText = "所属人数";
                dgv_busyo_m.Columns[4].HeaderText = "保有工数";
                dgv_busyo_m.Columns[5].HeaderText = "備考";
                dgv_busyo_m.Columns[6].HeaderText = "削除フラグ";
                dgv_busyo_m.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;   //カラム幅の自動調整
                dgv_busyo_m.AllowUserToResizeRows = false;    //セルの高さ変更不可
                dgv_busyo_m.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;    //カラムヘッダーの高さ変更不可
                dgv_busyo_m.ReadOnly = true; //編集不可
                dgv_busyo_m.AllowUserToDeleteRows = false;   //削除不可
                dgv_busyo_m.RowHeadersVisible = false;   //行ヘッダーを非表示にする
                dgv_busyo_m.AllowUserToAddRows = false;  //行を追加できないようにする（最下行を非表示にする）
            }
        }

        private void tb_busyo_cd_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_busyo_cd.Text) == false)
            {
                e.Cancel = true;
                return;
            }
            //オール０は他の画面で使用するため使用不可とする
            if(tb_busyo_cd.Text == "000000")
            {
                MessageBox.Show("部署コードに「000000」は使用できません。");
                e.Cancel = true;
                return;
            }
            //空白の場合はOKとする
            if (tb_busyo_cd.Text != "")
            {
                if (chk_busyo_cd() != true)
                {
                    MessageBox.Show("部署コードに異常があります。");
                    e.Cancel = true;
                }
            }
        }

        private bool chk_busyo_cd()
        {
            bool bl = true; //戻り値
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_busyo_m where busyo_cd  = '" + tb_busyo_cd.Text.ToString() + "'");
            if (dt_work.Rows.Count <= 0)
            {
                //新規
                gamen_sinki(tb_busyo_cd.Text);
                lbl_busyo_cd.Text = "新規の部署です。";
            }
            else
            {
                //既存データ有
                gamen_disp(dt_work);
                lbl_busyo_cd.Text = "既存のデータです。";
            }
            return bl;
        }

        private void gamen_sinki(string in_cd)
        {
            gamen_clear();
            tb_busyo_cd.Text = in_cd;
        }

        private void gamen_clear()
        {
            tb_busyo_cd.Text = "";
            tb_busyo_name.Text = "";
            tb_busyo_ryakusiki_name.Text = "";
            tb_ninzu.Text = "";
            tb_kousu.Text = "";
            tb_bikou.Text = "";
            tb_delete_flg.Text = "";
            tb_create_user_cd.Text = "";
            tb_create_datetime.Text = "";
            tb_update_user_cd.Text = "";
            tb_update_datetime.Text = "";
            lbl_busyo_cd.Text = "部署コードを入力してください";
        }

        private void gamen_disp(DataTable in_dt_work)
        {
            tb_busyo_cd.Text = in_dt_work.Rows[0]["busyo_cd"].ToString();
            tb_busyo_name.Text = in_dt_work.Rows[0]["busyo_name"].ToString();
            tb_busyo_ryakusiki_name.Text = in_dt_work.Rows[0]["busyo_ryakusiki_name"].ToString();
            tb_ninzu.Text = in_dt_work.Rows[0]["ninzu"].ToString();
            tb_kousu.Text = in_dt_work.Rows[0]["kousu"].ToString();
            tb_bikou.Text = in_dt_work.Rows[0]["bikou"].ToString();
            tb_delete_flg.Text = in_dt_work.Rows[0]["delete_flg"].ToString();
            tb_create_user_cd.Text = in_dt_work.Rows[0]["create_user_cd"].ToString();
            tb_create_datetime.Text = in_dt_work.Rows[0]["create_datetime"].ToString();
            tb_update_user_cd.Text = in_dt_work.Rows[0]["update_user_cd"].ToString();
            tb_update_datetime.Text = in_dt_work.Rows[0]["update_datetime"].ToString();

            if (tb_delete_flg.Text == "1")
            {
                tb_delete_flg.BackColor = Color.Red;
            }
            else
            {
                tb_delete_flg.BackColor = Color.Gainsboro;
            }
        }

        private void tb_busyo_name_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_busyo_name.Text) == false)
            {
                e.Cancel = true;
                return;
            }
        }

        private void tb_busyo_ryakusiki_name_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_busyo_ryakusiki_name.Text) == false)
            {
                e.Cancel = true;
                return;
            }
        }

        private void tb_ninzu_Validating(object sender, CancelEventArgs e)
        {
            if (chk_ninzu() != true)
            {
                MessageBox.Show("所属人数に異常があります。");
                e.Cancel = true;
            }
            else
            {

            }
        }

        private bool chk_busyo_name()
        {
            bool bl = true; //戻り値用

            if (tb_busyo_name.Text == null || tb_busyo_name.Text.Length == 0 || tss.StringByte(tb_busyo_name.Text) > 20)
            {
                bl = false;
            }
            return bl;
        }

        private bool chk_busyo_ryakusiki_name()
        {
            bool bl = true; //戻り値用

            if (tb_busyo_ryakusiki_name.Text == null || tb_busyo_ryakusiki_name.Text.Length == 0 || tss.StringByte(tb_busyo_ryakusiki_name.Text) > 10)
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
        private bool chk_ninzu()
        {
            bool bl = true; //戻り値
            decimal db;
            if (decimal.TryParse(tb_ninzu.Text.ToString(), out db))
            {
                //変換出来たら、dbにその数値が入る
                if (db > decimal.Parse("999.99") || db < decimal.Parse("0.00"))
                {
                    bl = false;
                }
                else
                {
                    tb_ninzu.Text = db.ToString("0.00");
                }
            }
            else
            {
                bl = false;
            }
            return bl;
        }

        private void tb_kousu_Validating(object sender, CancelEventArgs e)
        {
            if (chk_kousu() != true)
            {
                MessageBox.Show("保有工数に異常があります。");
                e.Cancel = true;
            }
            else
            {

            }
        }

        private bool chk_kousu()
        {
            bool bl = true; //戻り値
            decimal db;
            if (decimal.TryParse(tb_kousu.Text.ToString(), out db))
            {
                //変換出来たら、dbにその数値が入る
                if (db > decimal.Parse("99999999.99") || db < decimal.Parse("0.00"))
                {
                    bl = false;
                }
                else
                {
                    tb_kousu.Text = db.ToString("0.00");
                }
            }
            else
            {
                bl = false;
            }
            return bl;
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
            if (chk_busyo_name() == false)
            {
                MessageBox.Show("部署名称は1文字以上、20バイト以内で入力してください。");
                tb_busyo_name.Focus();
                return;
            }

            if (chk_busyo_ryakusiki_name() == false)
            {
                MessageBox.Show("部署略式名称は10バイト以内で入力してください。");
                tb_busyo_ryakusiki_name.Focus();
                return;
            }

            if (chk_ninzu() == false)
            {
                MessageBox.Show("所属人数は999.99までの値を入力してください。");
                tb_ninzu.Focus();
                return;
            }

            if (chk_kousu() == false)
            {
                MessageBox.Show("保有工数は99999999.99までの値を入力してください。");
                tb_kousu.Focus();
                return;
            }

            if (chk_bikou() == false)
            {
                MessageBox.Show("備考は128バイト以内で入力してください。");
                tb_bikou.Focus();
                return;
            }

            //部品コードの新規・更新チェック
            dt_work = tss.OracleSelect("select * from tss_busyo_m where busyo_cd  = '" + tb_busyo_cd.Text.ToString() + "'");
            if (dt_work.Rows.Count <= 0)
            {
                //新規
                DialogResult result = MessageBox.Show("新規に登録します。よろしいですか？", "確認", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    //「はい」が選択された時
                    busyo_insert();
                    gamen_clear();
                    tb_busyo_cd.Focus();
                    busyo_m_disp();
                }
                else
                {
                    //「いいえ」が選択された時
                    tb_busyo_name.Focus();
                }
            }
            else
            {
                //既存データ有
                DialogResult result = MessageBox.Show("既存データを更新します。よろしいですか？", "確認", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    //「はい」が選択された時
                    busyo_update();
                    chk_busyo_cd();
                }
                else
                {
                    //「いいえ」が選択された時
                    tb_busyo_name.Focus();
                }
            }
        }

        private void busyo_insert()
        {
            tss.GetUser();
            //新規書込み
            bool bl_tss = true;
            bl_tss = tss.OracleInsert("INSERT INTO tss_busyo_m (busyo_cd,busyo_name,busyo_ryakusiki_name,ninzu,kousu,bikou,delete_flg,create_user_cd,create_datetime)"
                                    + " VALUES ('" + tb_busyo_cd.Text.ToString() + "','" + tb_busyo_name.Text.ToString() + "','" + tb_busyo_ryakusiki_name.Text.ToString() + 
                                    "','" + tb_ninzu.Text.ToString() + "','" + tb_kousu.Text.ToString() + "','" + tb_bikou.Text.ToString() + "','0','" + tss.user_cd + "',SYSDATE)");
            if (bl_tss != true)
            {
                tss.ErrorLogWrite(tss.user_cd, "部署マスタ／登録", "登録ボタン押下時のOracleInsert");
                MessageBox.Show("書込みでエラーが発生しました。処理を中止します。");
                this.Close();
            }
            else
            {
                //MessageBox.Show("新規登録しました。");
            }
        }

        private void busyo_update()
        {
            tss.GetUser();
            //更新
            bool bl_tss = true;
            bl_tss = tss.OracleUpdate("UPDATE TSS_busyo_m SET busyo_name = '" + tb_busyo_name.Text.ToString() + "',busyo_ryakusiki_name = '" + tb_busyo_ryakusiki_name.Text.ToString()
                                    + "',ninzu = '" + tb_ninzu.Text.ToString() + "',kousu = '" + tb_kousu.Text.ToString() + "',bikou = '" + tb_bikou.Text.ToString()
                                    + "',UPDATE_USER_CD = '" + tss.user_cd + "',UPDATE_DATETIME = SYSDATE WHERE busyo_cd = '" + tb_busyo_cd.Text.ToString() + "'");
            if (bl_tss != true)
            {
                tss.ErrorLogWrite(tss.user_cd, "部署マスタ／登録", "登録ボタン押下時のOracleUpdate");
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
