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
    public partial class frm_kubun_meisyou_m : Form
    {
        DataTable dt = new DataTable();
        TssSystemLibrary tss = new TssSystemLibrary();
        string sv_kubun_meisyou_cd = "";

        public frm_kubun_meisyou_m()
        {
            InitializeComponent();
        }

        private void btn_hardcopy_Click(object sender, EventArgs e)
        {
            //Altキー＋Print Screenキーの送信
            SendKeys.SendWait("%{PRTSC}");
        }

        private void frm_kubun_meisyou_m_Load(object sender, EventArgs e)
        {
            status_disp();
            kubun_meisyou_m_disp();
        }

        private void btn_touroku_Click(object sender, EventArgs e)
        {
            //区分名称コードのチェック
            if(kubun_meisyou_cd_check() != true)
            {
                tb_kubun_meisyou_cd.Focus();
            }
            //区分名称のチェック
            else if (tb_kubun_meisyou.Text == null || tb_kubun_meisyou.Text.Length == 0 || System.Text.Encoding.GetEncoding(932).GetByteCount(tb_kubun_meisyou.Text) > 20)
            {
                MessageBox.Show("名称を20バイト以内で入力してください。");
                tb_kubun_meisyou.Focus();
            }
            //備考のチェック
            else if (System.Text.Encoding.GetEncoding(932).GetByteCount(tb_bikou.Text) > 256)
            {
                MessageBox.Show("備考が256バイトを超えています。");
                tb_bikou.Focus();
            }
            //書込み
            else
            {
                tss.GetUser();
                bool bl_tss;
                //既存の区分があるかチェック
                DataTable dt_work = new DataTable();
                dt_work = tss.OracleSelect("select * from TSS_KUBUN_MEISYOU_M where kubun_meisyou_cd = '" + tb_kubun_meisyou_cd.Text.ToString() + "'");
                if (dt_work.Rows.Count != 0)
                {
                    //更新
                    bl_tss = tss.OracleUpdate("UPDATE TSS_KUBUN_MEISYOU_M SET KUBUN_NAME = '" + tb_kubun_meisyou.Text.ToString() + "',BIKOU = '" + tb_bikou.Text.ToString() + "',UPDATE_USER_CD = '" + tss.user_cd + "',UPDATE_DATETIME = SYSDATE WHERE KUBUN_MEISYOU_CD = '" + tb_kubun_meisyou_cd.Text.ToString() + "'");
                    if (bl_tss != true)
                    {
                        tss.ErrorLogWrite(tss.user_cd, "区分名称マスタ／登録", "登録ボタン押下時のOracleUpdate");
                        MessageBox.Show("書込みでエラーが発生しました。処理を中止します。");
                        this.Close();
                    }
                }
                else
                {
                    //新規
                    bl_tss = tss.OracleInsert("INSERT INTO tss_kubun_meisyou_m (kubun_meisyou_cd,kubun_name,bikou,create_user_cd) VALUES ('" + tb_kubun_meisyou_cd.Text.ToString() + "','" + tb_kubun_meisyou.Text.ToString() + "','" + tb_bikou.Text.ToString() + "','" + tss.user_cd + "')");
                    if (bl_tss != true)
                    {
                        tss.ErrorLogWrite(tss.user_cd, "区分名称マスタ／登録", "登録ボタン押下時のOracleInsert");
                        MessageBox.Show("書込みでエラーが発生しました。処理を中止します。");
                        this.Close();
                    }
                }
                kubun_meisyou_m_disp();
                gamen_clear();
                tb_kubun_meisyou_cd.Focus();
            }
        }

        private void status_disp()
        {
            TssSystemLibrary tss = new TssSystemLibrary();
            tss.GetSystemSetting();
            tss.GetUser();
            ss_status.Items.Add(tss.system_name);
            ss_status.Items.Add(tss.system_version);
            ss_status.Items.Add(tss.user_name);
            ss_status.Items.Add(tss.kengen1 + tss.kengen2 + tss.kengen3 + tss.kengen4 + tss.kengen5 + tss.kengen6);
        }

        private void kubun_meisyou_m_disp()
        {
            dt = tss.OracleSelect("select * from TSS_KUBUN_MEISYOU_M order by kubun_meisyou_cd asc");
            dgv_kubun.DataSource = null;
            dgv_kubun.DataSource = dt;
            dgv_kubun.Columns[0].HeaderText = "区分名称コード";
            dgv_kubun.Columns[1].HeaderText = "区分名称";
            dgv_kubun.Columns[2].HeaderText = "備考";
            dgv_kubun.Columns[3].HeaderText = "作成者コード";
            dgv_kubun.Columns[4].HeaderText = "作成日時";
            dgv_kubun.Columns[5].HeaderText = "更新者コード";
            dgv_kubun.Columns[6].HeaderText = "更新者日時";
            dgv_kubun.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;   //カラム幅の自動調整
            dgv_kubun.AllowUserToResizeRows = false;    //セルの高さ変更不可
            dgv_kubun.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;    //カラムヘッダーの高さ変更不可
        }

        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gamen_clear()
        {
            tb_kubun_meisyou_cd.Text = "";
            tb_kubun_meisyou.Text = "";
            tb_bikou.Text = "";
            sv_kubun_meisyou_cd = "";
        }

        //区分コードの検証イベント
        private void tb_kubun_meisyou_cd_Validating(object sender, CancelEventArgs e)
        {
            //区分名称区分が空白の場合はOKとする
            if (tb_kubun_meisyou_cd.Text != "")
            {
                if (kubun_meisyou_cd_check() != true)
                {
                    MessageBox.Show("区分コードに異常があります。");
                    e.Cancel = true;
                }
            }
        }

        //区分コードの検証後のイベント（検証時に異常（e.Cancel = true）と判断された場合はこのイベントは発生しない）
        private void tb_kubun_meisyou_cd_Validated(object sender, EventArgs e)
        {
            if (sv_kubun_meisyou_cd != tb_kubun_meisyou_cd.Text)
            {
                //入力された区分コードでデータ読み込み＆表示（なければ新規）
                DataTable dt_work = new DataTable();
                dt_work = tss.OracleSelect("select * from TSS_KUBUN_MEISYOU_M where kubun_meisyou_cd = '" + tb_kubun_meisyou_cd.Text.ToString() + "'");
                if (dt_work.Rows.Count != 0)
                {
                    tb_kubun_meisyou.Text = dt_work.Rows[0]["KUBUN_NAME"].ToString();
                    tb_bikou.Text = dt_work.Rows[0]["BIKOU"].ToString();
                }
                else
                {
                    tb_kubun_meisyou.Text = "";
                    tb_bikou.Text = "";
                }
                sv_kubun_meisyou_cd = tb_kubun_meisyou_cd.Text;
            }
        }


        //区分コードのチェック
        private bool kubun_meisyou_cd_check()
        {
            bool bl = true; //戻り値

            //入力された文字列を00形式にする
            int i;
            if (int.TryParse(tb_kubun_meisyou_cd.Text, out i))
            {
                //変換出来たら、iにその数値が入る
                tb_kubun_meisyou_cd.Text = i.ToString("00");
            }
            else
            {
                //MessageBox.Show("区分名称コードは数字のみです。");
                bl = false;
            }
            return bl;
        }
    }
}
