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
    public partial class frm_kubun_m : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable dt_kubun_meisyou_m = new DataTable();
        DataTable dt_kubun_m = new DataTable();
        string str_kubun_cd;

        public frm_kubun_m()
        {
            InitializeComponent();
        }

        private void frm_kubun_m_Load(object sender, EventArgs e)
        {
            dt_kubun_meisyou_m = tss.OracleSelect("select * from tss_kubun_meisyou_m order by kubun_meisyou_cd asc");
        }

        private void cb_kubun_meisyou_cd_DropDown(object sender, EventArgs e)
        {
            //コンボボックスに区分名称テーブルをバインドする
            cb_kubun_meisyou_cd.DataSource = dt_kubun_meisyou_m;
            cb_kubun_meisyou_cd.DisplayMember = "KUBUN_NAME";
            cb_kubun_meisyou_cd.ValueMember = "KUBUN_MEISYOU_CD";
        }

        private void cb_kubun_meisyou_cd_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cb_kubun_meisyou_cd.SelectedIndex != -1)
            {
                //選択された場合
                str_kubun_cd = cb_kubun_meisyou_cd.SelectedValue.ToString();    //選択された区分名称コードを退避
                tb_bikou.Text = dt_kubun_meisyou_m.Rows[cb_kubun_meisyou_cd.SelectedIndex]["BIKOU"].ToString();
                dt_kubun_m = tss.OracleSelect("select * from tss_kubun_m where kubun_meisyou_cd = '" + cb_kubun_meisyou_cd.SelectedValue.ToString() + "'  order by kubun_cd asc");
            }
            else
            {
                //選択されなかった場合
                str_kubun_cd = "";    //選択された区分名称コードを退避
                dt_kubun_m = null;
            }
            //区分マスタ表示
            dgv_kubun_m.DataSource = null;
            dgv_kubun_m.DataSource = dt_kubun_m;
            //カラム名
            dgv_kubun_m.Columns[0].HeaderText = "区分名称コード";
            dgv_kubun_m.Columns[1].HeaderText = "区分コード";
            dgv_kubun_m.Columns[2].HeaderText = "区分名";
            dgv_kubun_m.Columns[3].HeaderText = "備考";
            dgv_kubun_m.Columns[4].HeaderText = "作成者コード";
            dgv_kubun_m.Columns[5].HeaderText = "作成日時";
            dgv_kubun_m.Columns[6].HeaderText = "更新者コード";
            dgv_kubun_m.Columns[7].HeaderText = "更新日時";
            //列の表示・非表示
            dgv_kubun_m.Columns[0].Visible = false;
            dgv_kubun_m.Columns[1].Visible = true;
            dgv_kubun_m.Columns[2].Visible = true;
            dgv_kubun_m.Columns[3].Visible = true;
            dgv_kubun_m.Columns[4].Visible = false;
            dgv_kubun_m.Columns[5].Visible = false;
            dgv_kubun_m.Columns[6].Visible = false;
            dgv_kubun_m.Columns[7].Visible = false;
            //入力桁数制限


            //削除不可
            dgv_kubun_m.AllowUserToDeleteRows = false;



            //その他の設定
            dgv_kubun_m.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;   //カラム幅の自動調整
            dgv_kubun_m.AllowUserToResizeRows = false;    //セルの高さ変更不可
            dgv_kubun_m.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;    //カラムヘッダーの高さ変更不可
            ////主キーの設定
            //DataColumn[] keyColumn = new DataColumn[2];
            //keyColumn[0] = dt_kubun_m.Columns["KUBUN_MEISYOU_CD"];
            //keyColumn[1] = dt_kubun_m.Columns["KUBUN_CD"];
            //dt_kubun_m.PrimaryKey = keyColumn;



        }

        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_touroku_Click(object sender, EventArgs e)
        {
            tss.GetUser();  //ユーザー情報の取得
            int int_insert = 0; //新規レコード数
            int int_update = 0; //更新レコード数
            //登録前にデータのチェック
            //※削除はできない仕様なので、削除レコード等の考慮はしない
            foreach(DataRow dr in dt_kubun_m.Rows)
            {
                //区分コード文字数チェック
                if (tss.StringByte(dr["KUBUN_CD"].ToString()) == 0 || tss.StringByte(dr["KUBUN_CD"].ToString()) > 2)
                {
                    dgv_kubun_m.Focus();
                    MessageBox.Show("区分コードの文字数に異常があります。 [" + dr["KUBUN_CD"].ToString() + "]" + Environment.NewLine + "区分コードは00から99までです。");
                    break;  //foreachを抜ける
                }
                //区分名文字数チェック
                if (tss.StringByte(dr["KUBUN_NAME"].ToString()) == 0 || tss.StringByte(dr["KUBUN_NAME"].ToString()) > 20)
                {
                    dgv_kubun_m.Focus();
                    MessageBox.Show("区分名の文字数に異常があります。 [" + dr["KUBUN_NAME"].ToString() + "]" + Environment.NewLine + "区分名は半角20文字（全角10文字）までです。");
                    break;  //foreachを抜ける
                }
                //備考文字数チェック
                if (tss.StringByte(dr["BIKOU"].ToString()) > 128)
                {
                    dgv_kubun_m.Focus();
                    MessageBox.Show("備考の文字数に異常があります。 [" + dr["BIKOU"].ToString() + "]" + Environment.NewLine + "備考は半角128文字（全角64文字）までです。");
                    break;  //foreachを抜ける
                }
                //区分コードを00形式にする
                int i;
                if (int.TryParse(dr["KUBUN_CD"].ToString(), out i))
                {
                    //変換出来たら、iにその数値が入る
                     dr["KUBUN_CD"] = i.ToString("00");
                }
                else
                {
                    dgv_kubun_m.Focus();
                    MessageBox.Show("区分コードに数字以外の文字があります。 [" + dr["KUBUN_CD"].ToString() + "]");
                    break;  //foreachを抜ける
                }

                //空白項目に適切な値を入れる
                if(dr["KUBUN_MEISYOU_CD"].ToString() == null || dr["KUBUN_MEISYOU_CD"].ToString() == "")
                {
                    dr["KUBUN_MEISYOU_CD"] = str_kubun_cd;
                    dr["CREATE_USER_CD"] = tss.user_cd;
                }

                //重複したキーが無いかチェック
                if (dt_kubun_m.Select("KUBUN_CD = " + dr["KUBUN_CD"].ToString()).Length >= 2)
                {
                    dgv_kubun_m.Focus();
                    MessageBox.Show("区分コード [" + dr["KUBUN_CD"].ToString() + "] が重複しています。");
                    break;  //foreachを抜ける
                }

                //区分名が空白はNGとする
                if (dr["KUBUN_NAME"].ToString() == "")
                {
                    dgv_kubun_m.Focus();
                    MessageBox.Show("区分名が空白です。");
                    break;  //foreachを抜ける
                }

                //同一キーを読み込み、あったら比較しUpdate又は何もしない、無かったらInsert
                DataTable dt_check = new DataTable();
                dt_check = tss.OracleSelect("select * from tss_kubun_m where kubun_meisyou_cd = '" + dr["KUBUN_MEISYOU_CD"].ToString() + "' and kubun_cd = '" + dr["KUBUN_CD"].ToString() + "'");
                if(dt_check.Rows.Count >= 1)
                {
                    //同一キーがある場合
                    //変更があるかチェック
                    if (dt_check.Rows[0]["KUBUN_NAME"].ToString() != dr["KUBUN_NAME"].ToString() || dt_check.Rows[0]["BIKOU"].ToString() != dr["BIKOU"].ToString())
                    {
                        //違いがある場合はUpdate
                        bool bl = tss.OracleUpdate("UPDATE TSS_KUBUN_M SET KUBUN_NAME = '" + dr["KUBUN_NAME"].ToString() + "',BIKOU = '" + dr["BIKOU"].ToString() + "',UPDATE_USER_CD = '" + tss.user_cd + "',UPDATE_DATETIME = SYSDATE WHERE KUBUN_MEISYOU_CD = '" + dr["KUBUN_MEISYOU_CD"].ToString() + "' and KUBUN_CD = '" + dr["KUBUN_CD"].ToString() + "'");
                        if (bl != true)
                        {
                            tss.ErrorLogWrite(tss.user_cd, "区分マスタ／登録", "登録ボタン押下時のOracleUpdate");
                            MessageBox.Show("書込みでエラーが発生しました。" + Environment.NewLine  + "処理を中止します。");
                            this.Close();
                        }
                        else
                        {
                            int_update++;
                        }
                    }
                }
                else
                {
                    //同一キーが無い場合はInsert
                    bool bl = tss.OracleInsert("INSERT INTO tss_kubun_m (kubun_meisyou_cd,kubun_cd,kubun_name,bikou,create_user_cd,create_datetime) VALUES ('" + dr["kubun_meisyou_cd"].ToString() + "','" + dr["kubun_cd"].ToString() + "','" + dr["kubun_name"].ToString() + "','" + dr["bikou"].ToString() + "','" + tss.user_cd + "',SYSDATE)");
                    if (bl != true)
                    {
                        tss.ErrorLogWrite(tss.user_cd, "区分名称マスタ／登録", "登録ボタン押下時のOracleInsert");
                        MessageBox.Show("書込みでエラーが発生しました。" + Environment.NewLine + "処理を中止します。");
                        this.Close();
                    }
                    else
                    {
                        int_insert++;
                    }
                }
            }
            if (int_insert != 0 || int_update != 0)
            {
                MessageBox.Show("登録しました。" + Environment.NewLine + "追加=" + int_insert.ToString() + Environment.NewLine + "更新=" + int_update.ToString());
            }
            else
            {
                MessageBox.Show("追加・更新するデータはありません。");
            }
        }
    }
}
