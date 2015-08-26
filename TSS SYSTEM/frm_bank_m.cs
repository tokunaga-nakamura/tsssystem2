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
    public partial class frm_bank_m : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
    
        public frm_bank_m()
        {
            InitializeComponent();
        }

        private void frm_bank_m_Load(object sender, EventArgs e)
        {
            tb_bank_cd.Focus();
            dgv_disp();
        }

        private void btn_touroku_Click(object sender, EventArgs e)
        {
            //取引先コードのチェック
            if (chk_torihikisaki_cd() == false)
            {
                MessageBox.Show("取引先コードは6文字で入力してください。");
                tb_torihikisaki_cd.Focus();
                return;
            }
            //金融機関コードのチェック
            if (chk_bank_cd() == false)
            {
                MessageBox.Show("金融機関コードは3文字で入力してください。");
                tb_bank_cd.Focus();
                return;
            }
            //支店コードのチェック
            if (chk_siten_cd() == false)
            {
                MessageBox.Show("支店コードは3文字で入力してください。");
                tb_siten_cd.Focus();
                return;
            }
            //金融機関名のチェック
            if (chk_bank_name() == false)
            {
                MessageBox.Show("金融機関名は1文字以上、128バイト以内で入力してください");
                tb_bank_name.Focus();
                return;
            }
            //支店名のチェック
            if (chk_siten_name() == false)
            {
                MessageBox.Show("支店名は1文字以上、128バイト以内で入力してください");
                tb_siten_name.Focus();
                return;
            }
            //口座種別のチェック
            if (chk_kouza_syubetu() == false)
            {
                MessageBox.Show("口座種別は1か2で入力してください。");
                tb_kouza_syubetu.Focus();
                return;
            }
            //口座番号のチェック
            if (chk_kouza_no() == false)
            {
                MessageBox.Show("口座番号は10バイト以内で入力してください。");
                tb_kouza_no.Focus();
                return;
            }
            //口座名義のチェック
            if (chk_kouza_meigi() == false)
            {
                MessageBox.Show("口座名義は128バイト以内で入力してください。");
                tb_kouza_meigi.Focus();
                return;
            }
            else
            //書込み
            {
                tss.GetUser();
                bool bl_tss;
                //既存の区分があるかチェック
                DataTable dt_work = new DataTable();
                dt_work = tss.OracleSelect("select * from TSS_BANK_M where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "'and bank_cd = '" + tb_bank_cd.Text + "'and siten_cd = '" + tb_siten_cd.Text + "' and kouza_syubetu = '" + tb_kouza_syubetu.Text + "'and kouza_no = '" + tb_kouza_no.Text + "'");

                if (dt_work.Rows.Count != 0)
                {
                    DialogResult result = MessageBox.Show("この口座は既に登録されています。上書きしますか？",
                    "口座削除",
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
                        bl_tss = tss.OracleUpdate("UPDATE TSS_bank_m SET BANK_NAME = '" + tb_bank_name.Text
                            + "',SITEN_NAME = '" + tb_siten_name.Text + "',KOUZA_MEIGI = '" + tb_kouza_meigi.Text
                            + "',UPDATE_USER_CD = '" + tss.user_cd + "',UPDATE_DATETIME = SYSDATE WHERE torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "'and bank_cd = '" + tb_bank_cd.Text + "'and siten_cd = '" + tb_siten_cd.Text + "' and kouza_syubetu = '" + tb_kouza_syubetu.Text + "'and kouza_no = '" + tb_kouza_no.Text + "'");
                        if (bl_tss != true)
                        {
                            tss.ErrorLogWrite(tss.user_cd, "銀行マスタ／登録", "登録ボタン押下時のOracleUpdate");
                            MessageBox.Show("登録でエラーが発生しました。処理を中止します。");
                            //this.Close();
                        }
                        else
                        {
                            MessageBox.Show("銀行口座情報を更新しました。");
                            dgv_disp();
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
                    bl_tss = tss.OracleInsert("INSERT INTO TSS_BANK_M (torihikisaki_cd,bank_cd,siten_cd,bank_name,siten_name,kouza_syubetu,kouza_no,kouza_meigi,create_user_cd) "
                                              + "VALUES ('" + tb_torihikisaki_cd.Text + "','" + tb_bank_cd.Text + "','" + tb_siten_cd.Text + "','" + tb_bank_name.Text + "','" + tb_siten_name.Text + "','" + tb_kouza_syubetu.Text + "','" + tb_kouza_no.Text + "','" + tb_kouza_meigi.Text + "','" + tss.user_cd + "')");
                    if (bl_tss != true)
                    {
                        tss.ErrorLogWrite(tss.UserID, "技b校マスタ／登録", "登録ボタン押下時のOracleInsert");
                        MessageBox.Show("登録でエラーが発生しました。処理を中止します。");
                        //this.Close();
                    }
                    else
                    {
                        MessageBox.Show("銀行口座情報を登録しました。");
                        //this.Close();

                        dgv_disp();
                    }
                }
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
        private bool chk_bank_cd()
        {
            bool bl = true; //戻り値用

            if (tb_bank_cd.Text == null || tb_bank_cd.Text.Length > 3 || tb_bank_cd.Text.Length < 3)
            {
                bl = false;
            }
            return bl;
        }
        private bool chk_siten_cd()
        {
            bool bl = true; //戻り値用

            if (tb_siten_cd.Text == null || tb_siten_cd.Text.Length > 3 || tb_siten_cd.Text.Length < 3)
            {
                bl = false;
            }
            return bl;
        }
        private bool chk_bank_name()
        {
            bool bl = true; //戻り値用

            if (tb_bank_name.Text == null || tb_bank_name.Text.Length == 0 || tss.StringByte(tb_bank_name.Text) > 128)
            {
                bl = false;
            }
            return bl;
        }
        private bool chk_siten_name()
        {
            bool bl = true; //戻り値用

            if (tb_siten_name.Text == null || tb_siten_name.Text.Length == 0 || tss.StringByte(tb_siten_name.Text) > 128)
            {
                bl = false;
            }
            return bl;
        }
        private bool chk_kouza_syubetu()
        {
            bool bl = true; //戻り値用

            if (tb_kouza_syubetu.Text == null || int.Parse(tb_kouza_syubetu.Text) > 2)
            {
                bl = false;
            }
            return bl;
        }
        private bool chk_kouza_no()
        {
            bool bl = true; //戻り値用

            if (tb_kouza_no.Text == null || tb_kouza_no.Text.Length == 0 || tss.StringByte(tb_kouza_no.Text) > 10)
            {
                bl = false;
            }
            return bl;
        }
        private bool chk_kouza_meigi()
        {
            bool bl = true; //戻り値用

            if (tb_kouza_meigi.Text == null || tb_kouza_meigi.Text.Length == 0 || tss.StringByte(tb_kouza_meigi.Text) > 128)
            {
                bl = false;
            }
            return bl;
        }

        private void tb_torihikisaki_cd_Leave(object sender, EventArgs e)
        {
            
        }

        private void tb_kouza_syubetu_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void tb_kouza_syubetu_DoubleClick(object sender, EventArgs e)
        {
            //選択用のdatatableの作成
            DataTable dt_work = new DataTable();
            //列の定義
            dt_work.Columns.Add("区分コード");
            dt_work.Columns.Add("区分名");
            //行追加
            DataRow dr_work = dt_work.NewRow();
            dr_work["区分コード"] = "1";
            dr_work["区分名"] = "普通";
            dt_work.Rows.Add(dr_work);
            dr_work = dt_work.NewRow();
            dr_work["区分コード"] = "2";
            dr_work["区分名"] = "当座";
            dt_work.Rows.Add(dr_work);
            //選択画面へ
            this.tb_kouza_syubetu.Text = tss.kubun_cd_select_dt("口座種別", dt_work);
            chk_kouza_syubetu();   //口座種別の表示
        }

        private void bt_hensyu_Click(object sender, EventArgs e)
        {
                int i = dgv_bank_m.CurrentCell.RowIndex;

                tb_torihikisaki_cd.Text = dgv_bank_m[0, i].Value.ToString();
                tb_torihikisaki_name.Text = dgv_bank_m[2, i].Value.ToString();
                tb_bank_cd.Text = dgv_bank_m[3, i].Value.ToString();
                tb_bank_name.Text = dgv_bank_m[4, i].Value.ToString();
                tb_siten_cd.Text = dgv_bank_m[5, i].Value.ToString();
                tb_siten_name.Text = dgv_bank_m[6, i].Value.ToString();
                tb_kouza_syubetu.Text = dgv_bank_m[7, i].Value.ToString();
                tb_kouza_no.Text = dgv_bank_m[8, i].Value.ToString();
                tb_kouza_meigi.Text = dgv_bank_m[9, i].Value.ToString();
        }

        private void bt_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_sakujyo_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("口座情報を削除しますか？",
           "口座削除",
           MessageBoxButtons.OKCancel,
           MessageBoxIcon.Exclamation,
           MessageBoxDefaultButton.Button2);

            //何が選択されたか調べる
            if (result == DialogResult.OK)
            {
                //「はい」が選択された時
                int i = dgv_bank_m.CurrentCell.RowIndex;

                tb_torihikisaki_cd.Text = dgv_bank_m[0, i].Value.ToString();
                tb_torihikisaki_name.Text = dgv_bank_m[2, i].Value.ToString();
                tb_bank_cd.Text = dgv_bank_m[3, i].Value.ToString();
                tb_bank_name.Text = dgv_bank_m[4, i].Value.ToString();
                tb_siten_cd.Text = dgv_bank_m[5, i].Value.ToString();
                tb_siten_name.Text = dgv_bank_m[6, i].Value.ToString();
                tb_kouza_syubetu.Text = dgv_bank_m[7, i].Value.ToString();
                tb_kouza_no.Text = dgv_bank_m[8, i].Value.ToString();
                tb_kouza_meigi.Text = dgv_bank_m[9, i].Value.ToString();

                tss.GetUser();
                bool bl_tss;
                bl_tss = tss.OracleDelete("delete from TSS_BANK_M where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "'and bank_cd = '" + tb_bank_cd.Text + "'and siten_cd = '" + tb_siten_cd.Text + "' and kouza_syubetu = '" + tb_kouza_syubetu.Text + "'and kouza_no = '" + tb_kouza_no.Text + "'");
                if (bl_tss != true)
                {
                    tss.ErrorLogWrite(tss.user_cd, "銀行マスタ／登録", "登録ボタン押下時のOracleUpdate");
                    MessageBox.Show("エラーが発生しました。処理を中止します。");
                    //this.Close();
                }
                else
                {
                    MessageBox.Show("口座情報から削除しました。");
                    dgv_disp();
                    tb_clear();
                    //this.Close();
                }
            }
            else if (result == DialogResult.Cancel)
            {
                //「キャンセル」が選択された時
                Console.WriteLine("「キャンセル」が選択されました");
            }
        }

        private void dgv_disp()
        {
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select TSS_BANK_M.torihikisaki_cd as torihikisaki_cd_A,TSS_TORIHIKISAKI_M.torihikisaki_cd as torihikisaki_cd_B,torihikisaki_name,bank_cd,bank_name,siten_cd,siten_name,kouza_syubetu,kouza_no,kouza_meigi from TSS_BANK_M LEFT OUTER JOIN TSS_TORIHIKISAKI_M ON TSS_BANK_M.TORIHIKISAKI_CD = TSS_TORIHIKISAKI_M.TORIHIKISAKI_CD ORDER BY TORIHIKISAKI_CD_A");

            dt_work.AcceptChanges();
            dgv_bank_m.DataSource = null;
            dgv_bank_m.DataSource = dt_work;

            //リードオンリーにする（編集できなくなる）
            dgv_bank_m.ReadOnly = true;
            //行ヘッダーを非表示にする
            dgv_bank_m.RowHeadersVisible = false;
            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            dgv_bank_m.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //セルの高さ変更不可
            dgv_bank_m.AllowUserToResizeRows = false;
            //カラムヘッダーの高さ変更不可
            dgv_bank_m.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //削除不可にする（コードからは削除可）
            dgv_bank_m.AllowUserToDeleteRows = false;
            //１行のみ選択可能（複数行の選択不可）
            dgv_bank_m.MultiSelect = false;
            //セルを選択すると行全体が選択されるようにする
            dgv_bank_m.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //DataGridView1にユーザーが新しい行を追加できないようにする
            dgv_bank_m.AllowUserToAddRows = false;
            
            this.dgv_bank_m.Columns["TORIHIKISAKI_CD_B"].Visible = false;

            dgv_bank_m.Columns[0].HeaderText = "取引先コード";
            dgv_bank_m.Columns[2].HeaderText = "取引先名";
            dgv_bank_m.Columns[3].HeaderText = "銀行コード";
            dgv_bank_m.Columns[4].HeaderText = "銀行名";
            dgv_bank_m.Columns[5].HeaderText = "支店コード";
            dgv_bank_m.Columns[6].HeaderText = "支店名";
            dgv_bank_m.Columns[7].HeaderText = "口座種別";
            dgv_bank_m.Columns[8].HeaderText = "口座番号";
            dgv_bank_m.Columns[9].HeaderText = "口座名義";

            dgv_bank_m.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
        
        private void tb_clear()
        {
              tb_torihikisaki_cd.Text = "";
              tb_torihikisaki_name.Text = "";
              tb_bank_cd.Text = "";
              tb_bank_name.Text = "";
              tb_siten_cd.Text = "";
              tb_siten_name.Text = "";
              tb_kouza_syubetu.Text = "";
              tb_kouza_no.Text = "";
              tb_kouza_meigi.Text = "";
        }

        private void tb_torihikisaki_cd_Validating(object sender, CancelEventArgs e)
        {
            DataTable dt_work2 = new DataTable();
            dt_work2 = tss.OracleSelect("select torihikisaki_name from TSS_TORIHIKISAKI_M where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "'");

            if (dt_work2.Rows.Count != 0)
            {
                tb_torihikisaki_name.Text = dt_work2.Rows[0][0].ToString();
            }
            else
            {
                //MessageBox.Show("取引先マスタに登録がありません。取引先マスタの登録をしてください。");
                return;
            }
        }
    }
}
