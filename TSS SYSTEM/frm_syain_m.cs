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
    public partial class frm_syain_m : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable w_dt_syain = new DataTable();
        
        public frm_syain_m()
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

        private void tb_syain_cd_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_syain_cd.Text) == false)
            {
                e.Cancel = true;
                return;
            }
            //空白の場合はOKとする
            if (tb_syain_cd.Text != "")
            {
                if (chk_syain_cd() != true)
                {
                    MessageBox.Show("社員コードに異常があります。");
                    e.Cancel = true;
                }
            }
        }

        private bool chk_syain_cd()
        {
            bool bl = true; //戻り値
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_syain_m where syain_cd  = '" + tb_syain_cd.Text.ToString() + "'");
            if (dt_work.Rows.Count <= 0)
            {
                //新規
                gamen_sinki(tb_syain_cd.Text);
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
            tb_syain_cd.Text = in_cd;
        }

        private void gamen_clear()
        {
            tb_syain_cd.Text = "";
            tb_syain_name.Text = "";
            tb_syain_kbn.Text = "";
            tb_syain_kbn_name.Text = "";
            tb_busyo_cd.Text = "";
            tb_busyo_name.Text = "";
            tb_kinmu_time1.Text = "";
            tb_kinmu_time2.Text = "";
            tb_bikou.Text = "";
            cb_delete_flg.Checked = true;
            tb_create_user_cd.Text = "";
            tb_create_datetime.Text = "";
            tb_update_user_cd.Text = "";
            tb_update_datetime.Text = "";
        }

        private void gamen_disp(DataTable in_dt_work)
        {
            tb_syain_cd.Text = in_dt_work.Rows[0]["syain_cd"].ToString();
            tb_syain_name.Text = in_dt_work.Rows[0]["syain_name"].ToString();
            tb_syain_kbn.Text = in_dt_work.Rows[0]["syain_kbn"].ToString();
            tb_syain_kbn_name.Text = get_syain_kbn(tb_syain_kbn.Text);
            tb_busyo_cd.Text = in_dt_work.Rows[0]["busyo_cd"].ToString();
            tb_busyo_name.Text = get_busyo_name(tb_busyo_cd.Text);
            tb_kinmu_time1.Text = in_dt_work.Rows[0]["kinmu_time1"].ToString();
            tb_kinmu_time2.Text = in_dt_work.Rows[0]["kinmu_time2"].ToString();
            tb_bikou.Text = in_dt_work.Rows[0]["bikou"].ToString();
            if (in_dt_work.Rows[0]["delete_flg"].ToString() == "1")
            {
                cb_delete_flg.Checked = true;
            }
            else
            {
                cb_delete_flg.Checked = false;
            }
            tb_create_user_cd.Text = in_dt_work.Rows[0]["create_user_cd"].ToString();
            tb_create_datetime.Text = in_dt_work.Rows[0]["create_datetime"].ToString();
            tb_update_user_cd.Text = in_dt_work.Rows[0]["update_user_cd"].ToString();
            tb_update_datetime.Text = in_dt_work.Rows[0]["update_datetime"].ToString();
            syain_list_disp();
        }

        private string get_syain_kbn(string in_cd)
        {
            string out_name = "";  //戻り値用
            switch (in_cd)
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

        private string get_busyo_name(string in_cd)
        {
            string out_name = "";  //戻り値用
            DataTable w_dt = new DataTable();
            w_dt = tss.OracleSelect("select * from tss_busyo_m where busyo_cd = '" + in_cd + "'");
            if (w_dt.Rows.Count <= 0)
            {
                out_name = "";
            }
            else
            {
                out_name = w_dt.Rows[0]["busyo_name"].ToString();
            }
            return out_name;
        }

        private void tb_syain_name_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_syain_name.Text) == false)
            {
                e.Cancel = true;
                return;
            }
        }

        private void tb_syain_kbn_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_syain_kbn.Text) == false)
            {
                e.Cancel = true;
                return;
            }
            tb_syain_kbn_name.Text = get_syain_kbn(tb_syain_kbn.Text);
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
            this.tb_syain_kbn.Text = tss.kubun_cd_select_dt("社員区分", dt_work, tb_syain_kbn.Text);
            tb_syain_kbn_name.Text = get_syain_kbn(tb_syain_kbn.Text);
        }

        private void tb_kinmu_time1_Validating(object sender, CancelEventArgs e)
        {
                        if (tss.Check_String_Escape(tb_kinmu_time1.Text) == false)
            {
                e.Cancel = true;
                return;
            }
        }

        private void tb_kinmu_time2_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_kinmu_time2.Text) == false)
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

        private void syain_list_disp()
        {
            w_dt_syain = tss.OracleSelect("select A.syain_cd,A.syain_name,A.syain_kbn,A.busyo_cd,B.busyo_name,A.kinmu_time1,A.kinmu_time2,A.bikou,A.delete_flg from tss_syain_m A left outer join tss_busyo_m B on (A.busyo_cd = B.busyo_cd) order by A.syain_cd");
            dgv_syain.DataSource = null;
            dgv_syain.DataSource = w_dt_syain;
            //リードオンリーにする（編集できなくなる）
            dgv_syain.ReadOnly = true;
            //行ヘッダーを非表示にする
            dgv_syain.RowHeadersVisible = false;
            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            dgv_syain.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //セルの高さ変更不可
            dgv_syain.AllowUserToResizeRows = false;
            //カラムヘッダーの高さ変更不可
            dgv_syain.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //削除不可にする（コードからは削除可）
            dgv_syain.AllowUserToDeleteRows = false;
            //１行のみ選択可能（複数行の選択不可）
            dgv_syain.MultiSelect = false;
            //セルを選択すると行全体が選択されるようにする
            dgv_syain.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //DataGridView1にユーザーが新しい行を追加できないようにする
            dgv_syain.AllowUserToAddRows = false;
            //DataGridViewのカラムヘッダーテキストを変更する
            dgv_syain.Columns[0].HeaderText = "社員CD";
            dgv_syain.Columns[1].HeaderText = "社員名";
            dgv_syain.Columns[2].HeaderText = "社員区分";
            dgv_syain.Columns[3].HeaderText = "部署CD";
            dgv_syain.Columns[4].HeaderText = "部署名";
            dgv_syain.Columns[5].HeaderText = "勤務開始時刻";
            dgv_syain.Columns[6].HeaderText = "勤務終了時刻";
            dgv_syain.Columns[7].HeaderText = "備考";
            dgv_syain.Columns[8].HeaderText = "有効/無効";
        }

        private void frm_syain_m_Load(object sender, EventArgs e)
        {
            syain_list_disp();  //社員リスト表示
        }

        private void btn_touroku_Click(object sender, EventArgs e)
        {
            //登録前に全ての項目をチェック
            if (chk_syain_name() == false)
            {
                MessageBox.Show("社員名は1文字以上、40バイト以内で入力してください。");
                tb_syain_name.Focus();
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
            if (chk_bikou() == false)
            {
                MessageBox.Show("備考は128バイト以内で入力してください。");
                tb_bikou.Focus();
                return;
            }
            //新規・更新チェック
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_syain_m where syain_cd  = '" + tb_syain_cd.Text.ToString() + "'");
            if (dt_work.Rows.Count <= 0)
            {
                //新規
                DialogResult result = MessageBox.Show("新規に登録します。よろしいですか？", "確認", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    //「はい」が選択された時
                    record_insert();
                    chk_syain_cd();
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
                    chk_syain_cd();
                }
                else
                {
                    //「いいえ」が選択された時
                    return;
                }
            }
        }

        private bool chk_syain_name()
        {
            bool bl = true; //戻り値用
            if (tb_syain_name.Text == null || tb_syain_name.Text.Length == 0 || tss.StringByte(tb_syain_name.Text) > 40)
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
            DataTable w_dt = new DataTable();
            if (tb_busyo_cd.Text != null && tb_busyo_cd.Text.Length != 0)
            {
                w_dt = tss.OracleSelect("select * from tss_busyo_m where busyo_cd = '" + tb_busyo_cd.Text + "'");
                if (w_dt.Rows.Count <= 0)
                {
                    bl = false;
                }
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
            string w_delete_flg;
            if(cb_delete_flg.Checked)
            {
                w_delete_flg = "1";
            }
            else
            {
                w_delete_flg = "0";
            }
            bool bl_tss = true;
            bl_tss = tss.OracleInsert("INSERT INTO tss_syain_m (syain_cd,syain_name,syain_kbn,busyo_cd,kinmu_time1,kinmu_time2,bikou,delete_flg,create_user_cd,create_datetime)"
                                    + " VALUES ('" + tb_syain_cd.Text.ToString() + "','" + tb_syain_name.Text.ToString() + "','" + tb_syain_kbn.Text.ToString() + "','" + tb_busyo_cd.Text.ToString() + "','" + tb_kinmu_time1.Text.ToString() + "','" + tb_kinmu_time2.Text.ToString() + "','" + tb_bikou.Text.ToString() + "','" + w_delete_flg + "','" + tss.user_cd + "',SYSDATE)");
            if (bl_tss != true)
            {
                tss.ErrorLogWrite(tss.user_cd, "社員マスタ／登録", "登録ボタン押下時のOracleInsert");
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
            string w_delete_flg;
            if (cb_delete_flg.Checked)
            {
                w_delete_flg = "1";
            }
            else
            {
                w_delete_flg = "0";
            }
            bool bl_tss = true;
            bl_tss = tss.OracleUpdate("UPDATE TSS_syain_m SET syain_name = '" + tb_syain_name.Text.ToString()
                + "',syain_kbn = '" + tb_syain_kbn.Text.ToString() + "',busyo_cd = '" + tb_busyo_cd.Text.ToString()
                + "',kinmu_time1 = '" + tb_kinmu_time1.Text.ToString() + "',kinmu_time2 = '" + tb_kinmu_time2.Text.ToString()
                + "',bikou = '" + tb_bikou.Text.ToString() + "',delete_flg = '" + w_delete_flg
                + "',UPDATE_USER_CD = '" + tss.user_cd + "',UPDATE_DATETIME = SYSDATE "
                + "WHERE syain_cd = '" + tb_syain_cd.Text.ToString() + "'");
            if (bl_tss != true)
            {
                tss.ErrorLogWrite(tss.user_cd, "社員マスタ／登録", "登録ボタン押下時のOracleUpdate");
                MessageBox.Show("書込みでエラーが発生しました。処理を中止します。");
                this.Close();
            }
            else
            {
                MessageBox.Show("更新しました。");
            }
        }

        private void tb_busyo_cd_DoubleClick(object sender, EventArgs e)
        {
            //選択用のdatatableの作成
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select busyo_cd,busyo_name from tss_busyo_m where delete_flg <> '1' order by busyo_cd");
            //選択画面へ
            this.tb_busyo_cd.Text = tss.kubun_cd_select_dt("部署コード", dt_work, tb_busyo_cd.Text);
            tb_busyo_name.Text = get_busyo_name(tb_busyo_cd.Text);
        }

        private void tb_syain_cd_DoubleClick(object sender, EventArgs e)
        {
            //選択画面へ
            string w_cd;
            w_cd = tss.search_syain("2", tb_syain_cd.Text);
            if (w_cd != "")
            {
                tb_syain_cd.Text = w_cd;
                if (chk_syain_cd() != true)
                {
                    MessageBox.Show("社員コードに異常があります。");
                    tb_syain_cd.Focus();
                }
            }
        }

        private void tb_busyo_cd_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_busyo_cd.Text) == false)
            {
                e.Cancel = true;
                return;
            }
            tb_busyo_name.Text = get_busyo_name(tb_busyo_cd.Text);
        }
    }
}
