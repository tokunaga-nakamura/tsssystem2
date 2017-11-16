//  SYSTEM NAME     TSS SYSTEM
//  PROGRAM NAME    TSS SYSTEMライブラリ
//  CREATE          T.NAKAMURA,J.OKUDA
//  UPDATE LOG
//  xxxx/xx/xx  NAMExxxx    NAIYOU

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;     //app.config用（参照設定にも追加）
using System.Windows.Forms;     //messagebox用（参照設定にも追加）
using Oracle.DataAccess.Client; //（参照設定にも追加）
using System.Data;              //OracleのUPDATE文の実行の際にIsolationLevelを指定するのに必要らしい
using System.IO;                //StreamWriter

//プログラムのバージョン管理
//----------------------------------------------------------------------------------------------------------------------------------
//system_versionでシステム全体のバージョン管理をする
//code_versionでプログラムのバージョン管理をする
//主なバージョンのカウント方法は、
//・テーブルの変更、仕様の変更等、新と旧のDBまたはプログラムの混載が許されない場合はシステムのバージョンを上げる
//・プログラムの機能の追加やバグ修正など、現状バージョンのまま使用できる場合は、プログラムバージョンのみ上げる
//※尚、system_versionが上がると、get_newしないと起動できなくなるので、新規プログラムの確実な配信に役立てることも可能
//----------------------------------------------------------------------------------------------------------------------------------
//プログラムの配布手順（システムのバージョンを上げる場合）
//①program_versionが上がった場合、tss system libraryのコンストラクタ（このライブラリのもう少し下）に宣言してある変数 program_version の値を変更する
//②program_Code_versionが上がった場合、tss system libraryのコンストラクタ（このライブラリのもう少し下）に宣言してある変数 program_code_version の値を変更する
//③コンパイルしたexeを\\TSSSVR\tss_share\tsssystem\tss\tsssystem\binの中にコピーする
//④program_versionが上がった場合、SQL Deveroperで直接tss_system_mのsystem_cd = '0101'のレコードのsystem_versionの値を変更する（上記のprogram_versionと同じ値にする）
//⑤システム管理者メニューにあるコントロールマスタでプログラムのバージョン変更の案内を登録する
//----------------------------------------------------------------------------------------------------------------------------------
//更新履歴
//ver   code    update      note
//1.01                      正式リリース
//1.02          2016/04/26  受注残参照画面の検索条件に、製品マスタの集計区分を追加
//1.03   1      2016/06/22  1.03は自挿生産工程対応版
//                          -生産スケジュール関連のテーブル、項目の追加
//                          -工程、部署、ライン、生産工程、生産ライン、生産スケジュール等
//                          -納品スケジュール区分の追加とそれに伴う検索画面や受注入力、納品スケジュールの表示、納品スケジュールの順番変更機能等を追加・変更
//                          -社員マスタと勤怠登録機能、及びメニューに勤怠表示を追加
//                          -受注マスタの登録時に自動で生産スケジュールを作成する機能を追加
//                          -生産スケジュールの調整・印刷機能の実装
//                          -コントロールマスタ（標準工数、メニューメッセージ）、ログインファイル（ログイン履歴）の追加
//                          -ユーザーマスタに権限7（生産）の追加
//                          -各検索画面、入力画面の検索機能・区分等の選択機能の充実
//                          などなど
//      2       2016/07/xx  -スケジュール調整画面大幅変更
//                          -メニューのステータスにコードバージョンの表示を追加
//      3       2016/07/22  -生産工程マスタメンテの登録権限バグ修正
//      4       2016/08/17  納品スケジュールの参照モードにおいて、受注マスタの納品区分が１以外及び削除フラグの立っている受注を表示しないように修正
//      5       2016/08/18  上記v4を変更モードにも対応
//1.04  1       2016/08/23  メニューのメッセージを2行→3行へ変更
//                          上記に伴いコントロールマスタにmsg3を追加
//      2       2016/08/26  伊藤さんから報告：製品別在庫照会、製品構成が登録されていない製品を続けて表示させるとこけるbugfix
//      3       2016/10/24  既存売上データを更新すると、締日が空白になるバグを修正
//                          重要！！上記デバッグ中に既存データの在庫のマイナス処理（在庫を一旦戻す）がされていないらしいというバグを発見→このバージョンでは未対応
//      4       2016/10/25  上記CODE:4にて見つかったマイナス処理のバグ対応
//                          ユーザーマスタにログイン時のバージョンを追加し、ログイン時にユーザー毎のバージョンを記録するように機能追加
//                          コントロールマスタのメッセージの色を指定できるように機能追加
//                          ×ボタンによる終了時にもログアウト処理が実行されるように修正
//      5       2016/11/02  部品入出庫履歴の取引先コードを指定した場合、部品入出庫履歴マスタの取引先コードを参照していたが、それだとフリー在庫（999999）がうまく抽出できないので
//                          joinしている部品ましたの取引先コードを参照するように修正
//1.05  0       2016/11/28  -生産工数一覧、表示単位（時・分・秒）の対応とcsv出力も同様の対応
//                          -生産スケジュール編集 v2リリース
//                              ・前日、翌日のスケジュール（日付の変更可）を画面下に表示
//                              ・別の日からのD&D（メンバーのD&Dはまだ）
//                              ・空白状態からの新規行、及び空白行からの新規行の追加対応
//                              ・自動時刻計算
//                              ・部署、工程、ラインでのグループ化表示
//                              ・納品スケジュールと生産スケジュールとの生産数チェック
//                          -生産スケジュール一覧印刷
//                          -製品検索子画面モード時に検索結果と見出しがずれているbugfix
//                          -生産工程マスタ、登録時のエラー表示を「内容＋工程順」から「工程順\n内容」に修正
//                          -生産工程マスタのメニューを製品・部品タブから生産タブへ移動
//                          -作業指示書の印刷
//                          -生産実績入力の追加
//                          -生産実績検索の追加
//                          -生産実績入力のバーコード入力
//                          -生産工程マスタに完成品をカウントする工程を判断するためのフラグを追加
//                          -複数行ある売上の訂正時に、1行目の売上数を引いていたバグを修正（累計が狂ってくる＆売上数＝受注数にならなくなる）
//      1       2016/11/29  生産スケジュール調整、新規行にて編集中に落ちる現象を修正
//      2       2016/11/30  生産スケジュール作成メソッド（ライブラリ）、同一日に複数の納品スケジュールが有る場合、まとめて１つの生産スケジュールを作成するように修正
//                          生産スケジュール調整、行毎の実績・予定・受注数チェックを実装
//                          生産スケジュール一覧、時刻がPM表記だったものを24時間表記に修正
//      3       2016/12/02  月初日を求めるライブラリ追加
//                          請求締めにて、末締め（取引先マスタの締日が99）の場合かつ今月が31日まであり先月が30日までしかないような場合に、先月末の日付が間違って求められていた不具合修正
//                          取引先マスタ、データ更新時に支払月が更新されていないバグ修正
//      4       2016/12/05  生産実績入力の正式公開（タブオーダーの不具合あり・・・原因不明）
//                          生産スケジュール調整画面の右下に現在編集中の行の受注情報の納品、生産、実績情報を表示
//                          仮受注to本受注に生産実績Fを追加
//      5       2016/12/06  生産スケジュール調整画面の右下の表示を、編集行の「工程」のみから編集行の「受注の工程全て」の表示に変更
//      6       2016/12/16  各画面（奥田氏作成）、splitcontainer設定ミス見直し
//                          システム管理者用のSQL実行画面を追加
//                          無条件に生産工程の最終工程の生産カウントフラグを１に、それ以外は０にするゴミプロの実装
//                          納品スケジュール、生産工数画面の並び順を変えられないように修正
//                          生産工程には、同一工程を登録しないようルール化（実際は登録できてしまう）
//                          ↑上記に伴い、実績の集計は部署に関係なく工程毎に集計できる（今までは部署＋工程だった）
//                          部署コードのコンバートゴミプロ実装（実行済みなのでコメントアウトしてある）
//      7       2016/12/19  システム管理者向けのメッセージ送信機能の実装
//                          上記に伴い tss_message_log_f の主キー（発生日時）の桁数を20から36に増やしミリ秒まで記録するように変更
//                          同様にtss_error_log_fの主キーも36桁に修正し、ミリ秒まで記録するように変更
//                          RDBは本番環境、開発環境ともに変更済み
//                          入金画面において、入金区分・入金額が未入力でも登録できてしまうバグ修正（プログラム見てみたら明らかな未検証状態でした（検証してれば確実に発見できる））
//      8       2017/01/20  請求締め処理、１カ月前の売掛レコードが無い場合に、前回売掛残高が０になるバグを修正（１カ月前が無かったら直近のレコードを抽出するように修正）
//      9       2017/02/01  支払締め処理において、新規の締め（同月の再締めではない）の場合に、新規レコード作成する処理で支払残高に無条件で０を入れていたバグを修正
//                          その際に、同プログラム及び支払一覧のプログラムで、select * で抽出した項目（カラム）を使っているコードを発見したが、未修正。
//1.06  0       2017/03/01  売上において、訂正した場合のマイナス＆プラス処理で、部品入出庫履歴の書き込み数値がおかしい件、修正
//      1       2017/03/15  生産工程マスタ画面、一部レイアウト変更、及び検索画面にてダブルクリックで選択できるコードが無かったので追加
//                          生産実績入力、生産指示日報レイアウト等、稼働前のデバッグ修正
//      2       2017/03/29  生産スケジュール調整画面、新規行等で受注番号を入力すると、タクト等の情報は表示されるが、生産機種が表示されないバグ修正
//                          生産スケジュール調整画面、受注や製品コードのValidating等のほとんどに、生産機種を表示するコードが記述されていないバグ修正orz
//                          生産実績入力及び生産指示書、入力しやすく、見やすい並びに調整
//                          生産実績入力、生産スケジュールとの連動をやめ、同一日の同一受注があったら参考表示するように仕様変更
//                          dgv_today内のダブルクリックイベント全般にて、入力中の値を取得できない、また戻ってきた値が反映されないバグ修正
//                          生産指示書の受注数の小数点以下の表示フォーマットを00から##に変更（小数点以下が無ければ表示させないように変更）
//                          生産工程マスタ画面、製品コードを入力せずに直接マウスで備考などにフォーカスを移しvalidatingイベント等を発生させるとエラーが発生する
//                          →製品コードを入力するまでsplitcontainerのenableをfalse、製品コード入力後はtrueにするようにして対応
//      3       2017/06/07  ・生産スケジュール一覧で行の順番が画面と異なるバグ修正
//                          ・（starttime,endtimeがdatetime型なので日付を持っていて、その日付がコピーなどすると更新されておらず、一覧側はstarttime順に印刷していた）
//                          →登録時にlbl_seisan_todayの値を入れるようにし、8:30より小さい値の場合は翌日の日付にするようにした
//                          ・仮受注コードの変更を、受注コード２のみだった仕様を受注コード１と受注コード２を変更できるように修正
//      4       2017/06/13  生産スケジュール調整画面において、タクトタイムや生産数など、リアルタイムに計算で使用する項目に、数値として認識できるか入力チェックを追加
//      5       2017/06/30  ユーザーマスタ画面、権限のmaxlength、未設定→1桁に修正
//                          単価別売上明細、代入される値が不確定な変数をdecimal.Parseを使用しているためこける。使用前にTryParseで確認しfalseの場合"0"を入れるようにして対応
//      6       2017/09/04  ・生産工程マスタ、工程を空（未登録の状態）にできない（登録時にエラーが発生）症状を修正
//                          ・生産スケジュール調整、入力チェック処理においてずれたカラムをチェックしていたバグを修正
//      7       2017/09/27  空白の請求書が印刷できるよう機能追加
//      8       2017/10/10  全てのコードの先頭にコメント（システム名、プログラム名、作成者、更新履歴）を追加
//                          生産スケジュールチェックにカーソル行の納品・生産スケジュール・生産実績表示を追加（生産スケジュール調整と同じもの）
//              2017/10/18  受注マスタ、納品スケジュール参照、一覧、生産スケジュール調整、一覧、生産指示日報に備考2を追加
//                          受注コメント登録画面を追加、及び納品スケジュール参照からリンク
//              2017/10/20  入出庫移動画面で出庫登録時にこける不具合（まだ未解決）、Oracleエラー時にログ出力がされていない箇所が多数あったので追加 
//      9       2017/10/24  コントロールマスタにメッセージ4と色4を追加し、メニューを4行表示に変更
//              2017/10/25  部品マスタの移動履歴にラジオボタンを設け、表示数を変更可能にして速度アップ
//                          （今後も更に移動履歴が増えることを考えると、範囲指定などが必要になってくるが現時点では未対応）
//1.07  0       2017/11/09  tssライブラリzaiko_procにおいて、入出庫マスタの入出庫移動番号が入庫と出庫が混ざった状態で書き込んでいる不具合の修正                   
//      x       2017/11/13  ・login時にlogin_fにてサイレントエラーが発生している件について想定対応（たぶんログイン画面にて終了を選んだ際に２回login_fへの書き込みが行われていて2回目はusercdが空白）
//                          ・メニューの警告用ラベル、2行分の表示エリアを確保
//                          ・クイックメニューに勤怠入力を追加
//                          ・勤怠入力、キー違反防止のチェックを追加
//                          ・納品スケジュール、売上完了フラグを表示に追加し、売上完了の受注の納品数をグレー表示に変更
//
//
//
//
//
//
//
//
//
//

namespace TSS_SYSTEM
{
    #region TssSystemLibrary
    /// <summary>
    /// tssシステム用のライブラリ詰め合わせ
    /// tssシステム以外の事は考えていないので他で使用する場合は注意！
    /// </summary>
    class TssSystemLibrary
    {
        #region TssSystemLibrary クラス
        //フィールドの定義
        string program_version;
        string program_code_version;

        string fld_DataSource;
        string fld_UserID;
        string fld_Password;
        string fld_ConnectionString;
        string fld_BinPath;
        string fld_SystemPath;
        string fld_CsvPath;
        string fld_dainichi_cd;

        string fld_system_cd;
        string fld_system_name;
        string fld_system_version;
        int fld_chat_update_interval;
        string fld_share_directory;

        string fld_user_cd;
        string fld_user_name;
        string fld_user_name2;
        string fld_busyo_cd;
        string fld_kengen1;
        string fld_kengen2;
        string fld_kengen3;
        string fld_kengen4;
        string fld_kengen5;
        string fld_kengen6;
        string fld_kengen7;
        string fld_kengen8;
        string fld_kengen9;

        public TssSystemLibrary()
        {
            //コンストラクタ
            program_version = "1.07";
            program_code_version = "0";

            fld_DataSource = null;
            fld_UserID = null;
            fld_Password = null;
            fld_ConnectionString = null;
            fld_BinPath = null;
            fld_SystemPath = null;
            fld_CsvPath = null;
            fld_dainichi_cd = null;

            fld_system_cd = null;
            fld_system_name = null;
            fld_system_version = null;
            fld_chat_update_interval = 0;
            fld_share_directory = null;

            fld_user_cd = null;
            fld_user_name = null;
            fld_user_name2 = null;
            fld_busyo_cd = null;
            fld_kengen1 = null;
            fld_kengen2 = null;
            fld_kengen3 = null;
            fld_kengen4 = null;
            fld_kengen5 = null;
            fld_kengen6 = null;
            fld_kengen7 = null;
            fld_kengen8 = null;
            fld_kengen9 = null;
        }

        public string DataSource { get { return fld_DataSource; } }
        public string UserID { get { return fld_UserID; } }
        public string Password { get { return fld_Password; } }
        public string ConnectionString { get { return fld_ConnectionString; } }
        public string BinPath { get { return fld_BinPath; } }
        public string SystemPath { get { return fld_SystemPath; } }
        public string CsvPath { get { return fld_CsvPath; } }
        public string Dainichi_cd { get { return fld_dainichi_cd; } }

        public string system_cd { get { return fld_system_cd; } }
        public string system_name { get { return fld_system_name; } }
        public string system_version { get { return fld_system_version; } }
        public string code_version { get { return program_code_version; } }
        public int chat_update_interval { get { return fld_chat_update_interval; } }
        public string share_directory { get { return fld_share_directory; } }

        public string user_cd { get { return fld_user_cd; } }
        public string user_name { get { return fld_user_name; } }
        public string user_name2 { get { return fld_user_name2; } }
        public string busyo_cd { get { return fld_busyo_cd; } }
        public string kengen1 { get { return fld_kengen1; } }
        public string kengen2 { get { return fld_kengen2; } }
        public string kengen3 { get { return fld_kengen3; } }
        public string kengen4 { get { return fld_kengen4; } }
        public string kengen5 { get { return fld_kengen5; } }
        public string kengen6 { get { return fld_kengen6; } }
        public string kengen7 { get { return fld_kengen7; } }
        public string kengen8 { get { return fld_kengen8; } }
        public string kengen9 { get { return fld_kengen9; } }
        #endregion

        #region GetConnectionString
        /// <summary>
        /// App.Configから情報を取得し、暗号・マスク処理を行い、DB接続に必要なConnectionStringを生成し文字列を返す
        /// 戻り値 エラー:null 正常:ConnectionString文字列を作成して返す
        /// </summary>
        public string GetConnectionString()
        {
            /// <summary>
            /// 2014.08.11現在
            /// ID、Password、接続文字列は以下のように暗号化している
            /// ***Strに実際に必要な文字列＋ダミーの文字列を入れる（ダミーの文字列は何文字でも良い）
            /// ***Mskに実際に必要な文字列と同じ長さの適当な文字を入れる
            /// ***Mskの文字数分を***Strの先頭から切り出し、実際の文字列とする
            /// </summary>

            //app.configから必要な情報を取得
            string DataSourceStr = ConfigurationManager.AppSettings["DataSourceStr"];   //データソース名文字列の取得
            string DataSourceMsk = ConfigurationManager.AppSettings["DataSourceMsk"];   //データソース名用のマスク（隠蔽文字列）の取得
            string UserIDStr = ConfigurationManager.AppSettings["UserIDStr"];   //ユーザー名文字列を取得
            string UserIDMsk = ConfigurationManager.AppSettings["UserIDMsk"];   //ユーザー名用のマスク（隠蔽文字列）の取得
            string PasswordStr = ConfigurationManager.AppSettings["PasswordStr"];   //パスワード文字列の取得
            string PasswordMsk = ConfigurationManager.AppSettings["PasswordMsk"];   //パスワード文字列用のマスク（隠蔽文字列）の取得

            if (DataSourceStr.Length < DataSourceMsk.Length)
            {
                MessageBox.Show("設定ファイル:DataSourceを処理できません。");
                return null;
            }
            else
            {
                fld_DataSource = DataSourceStr.Substring(0, DataSourceMsk.Length);
            }
            if (UserIDStr.Length < UserIDMsk.Length)
            {
                MessageBox.Show("設定ファイル:UserIDを処理できません。");
                return null;
            }
            else
            {
                fld_UserID = UserIDStr.Substring(0, UserIDMsk.Length);
            }
            if (PasswordStr.Length < PasswordMsk.Length)
            {
                MessageBox.Show("設定ファイル:Passwordを処理できません。");
                return null;
            }
            else
            {
                fld_Password = PasswordStr.Substring(0, PasswordMsk.Length);
            }
            //戻り値
            fld_ConnectionString = "Data Source=" + DataSource + ";User Id=" + UserID + ";Password=" + Password;
            return ConnectionString;
        }
        #endregion

        #region GetSystemSetting
        /// <summary>
        /// テーブル TSS_SYSTEM のシステム情報を読み込み、フィールドに格納し、プロパティとして参照可能にする
        /// 引数：無し　戻り値：無し
        /// </summary>
        public void GetSystemSetting()
        {
            DataTable dt = new DataTable();
            dt = OracleSelect("SELECT * FROM TSS_SYSTEM_M where system_cd = '0101'");
            fld_system_cd = dt.Rows[0]["system_cd"].ToString();
            fld_system_name = dt.Rows[0]["system_name"].ToString();
            fld_system_version = dt.Rows[0]["system_version"].ToString();
        }
        #endregion

        #region GetUser
        /// <summary>
        /// テーブル TSS_USER_M を読み込み、フィールドに格納し、プロパティとして参照可能にする
        /// 引数：無し　戻り値：無し
        /// </summary>
        public void GetUser()
        {
            string TempPath = ConfigurationManager.AppSettings["TempPath"];   //テンポラリフォルダのパスの取得
            string usercd;
            using (StreamReader sr = new StreamReader(TempPath + "user.txt"))
            {
                usercd = sr.ReadToEnd();
            }
            if (usercd != "notlogin") //ユーザー名にnotloginという文字列が入っていたら終了する
            {
                DataTable dt = new DataTable();
                dt = OracleSelect("SELECT * FROM TSS_USER_M where user_cd = '" + usercd + "'");

                fld_user_cd = dt.Rows[0]["user_cd"].ToString();
                fld_user_name = dt.Rows[0]["user_name"].ToString();
                fld_user_name2 = dt.Rows[0]["user_name2"].ToString();
                fld_busyo_cd = dt.Rows[0]["busyo_cd"].ToString();
                fld_kengen1 = dt.Rows[0]["kengen1"].ToString();
                fld_kengen2 = dt.Rows[0]["kengen2"].ToString();
                fld_kengen3 = dt.Rows[0]["kengen3"].ToString();
                fld_kengen4 = dt.Rows[0]["kengen4"].ToString();
                fld_kengen5 = dt.Rows[0]["kengen5"].ToString();
                fld_kengen6 = dt.Rows[0]["kengen6"].ToString();
                fld_kengen7 = dt.Rows[0]["kengen7"].ToString();
                fld_kengen8 = dt.Rows[0]["kengen8"].ToString();
                fld_kengen9 = dt.Rows[0]["kengen9"].ToString();
            }
        }
        #endregion

        #region Login_Rireki
        /// <summary>
        /// ログイン、ログアウトの記録を更新する
        /// 引数：区分 1:ログイン 2:ログアウト
        /// </summary>
        public void Login_Rireki(string in_kbn)
        {
            if(in_kbn != "1" && in_kbn != "2")
            {
                return;
            }
            string w_host_name;
            GetUser();
            w_host_name = System.Net.Dns.GetHostName();
            string w_sql = "insert into tss_login_f (log_date,user_cd,login_kbn,host_name,create_user_cd,create_datetime) values (sysdate,'" + user_cd + "','" + in_kbn + "','" + w_host_name + "','" + user_cd + "',sysdate)";
            bool w_bl_login_rireki;
            w_bl_login_rireki = OracleInsert(w_sql);
            //if(w_bl_login_rireki == false)
            //{
            //    MessageBox.Show("エラーが発生しました。下記のメッセージををシステム管理者へ見せてください。\n tss.Login_Rireki(" + in_kbn + ")\n w_sql = " + w_sql)
            //}
        }
        #endregion

        #region GetCOM
        /// <summary>
        /// BCRのCOMポートを取得する
        /// 引数：無し　戻り値：string out_com
        /// </summary>
        public string GetCOM()
        {
            string out_com = null;  //戻り値
            string SystemPath = ConfigurationManager.AppSettings["SystemPath"];   //システムフォルダのパスの取得
            using (StreamReader sr = new StreamReader(SystemPath + "COM.txt"))
            {
                out_com = sr.ReadToEnd();
            }
            return out_com;
        }
        #endregion

        #region OracleUpdate
        /// <summary>
        /// OracleへUPDATE文を実行します。
        /// </summary>
        /// <param name="sql">実行するSQL文です</param>
        /// <returns>bool</returns>
        public Boolean OracleUpdate(string sql)
        {
            Boolean rtncode = false;    //戻り値用（初期値false）
            string connStr = GetConnectionString();
            using (OracleCommand cmd = new OracleCommand())
            {
                OracleConnection conn = new OracleConnection();
                conn.ConnectionString = connStr;
                //トランザクション開始
                conn.Open();
                OracleTransaction transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    cmd.CommandText = sql;
                    cmd.Connection = conn;
                    cmd.Transaction = transaction;
                    cmd.ExecuteNonQuery();
                    //トランザクションをコミットします。
                    transaction.Commit();
                    rtncode = true;
                }
                catch (Exception)
                {
                    //トランザクションをロールバック
                    transaction.Rollback();
                    rtncode = false;
                    GetUser();
                    ErrorLogWrite(user_cd, "OracleUpdate", sql.Replace("'", "###"));
                }
                finally
                {
                    conn.Close();
                }
            }
            return rtncode;
        }
        #endregion

        #region OracleDelete
        /// <summary>
        /// OracleへDELETE文を実行します。
        /// </summary>
        /// <param name="sql">実行するSQL文です</param>
        /// <returns>bool</returns>
        public Boolean OracleDelete(string sql)
        {
            Boolean rtncode = false;    //戻り値用（初期値false）
            string connStr = GetConnectionString();
            using (OracleCommand cmd = new OracleCommand())
            {
                OracleConnection conn = new OracleConnection();
                conn.ConnectionString = connStr;
                //トランザクション開始
                conn.Open();
                OracleTransaction transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    cmd.CommandText = sql;
                    cmd.Connection = conn;
                    cmd.Transaction = transaction;
                    cmd.ExecuteNonQuery();
                    //トランザクションをコミットします。
                    transaction.Commit();
                    rtncode = true;
                }
                catch (Exception)
                {
                    //トランザクションをロールバック
                    transaction.Rollback();
                    rtncode = false;
                    GetUser();
                    ErrorLogWrite(user_cd, "OracleDelete", sql.Replace("'", "#"));
                }
                finally
                {
                    conn.Close();
                }
            }
            return rtncode;
        }
        #endregion

        #region OracleInsert
        /// <summary>
        /// OracleへINSERT文を実行します。
        /// </summary>
        /// <param name="sql">実行するSQL文です</param>
        /// <returns>bool</returns>
        public Boolean OracleInsert(string sql)
        {
            Boolean rtncode = false;    //戻り値用（初期値false）
            string connStr = GetConnectionString();
            using (OracleCommand cmd = new OracleCommand())
            {
                OracleConnection conn = new OracleConnection();
                conn.ConnectionString = connStr;
                //トランザクション開始
                conn.Open();
                OracleTransaction transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    cmd.CommandText = sql;
                    cmd.Connection = conn;
                    cmd.Transaction = transaction;
                    cmd.ExecuteNonQuery();
                    //トランザクションをコミットします。
                    transaction.Commit();
                    rtncode = true;
                }
                catch (Exception)
                {
                    //トランザクションをロールバック
                    transaction.Rollback();
                    rtncode = false;
                    GetUser();
                    ErrorLogWrite(user_cd, "OracleInsert", sql.Replace("'", "#"));
                }
                finally
                {
                    conn.Close();
                }
            }
            return rtncode;
        }
        #endregion

        #region OracleSelect
        /// <summary>
        /// OracleへSELECT文を実行します。
        /// </summary>
        /// <param name="sql">実行するSQL文です</param>
        /// <returns>DataTable エラー時:null</returns>
        public DataTable OracleSelect(string sql)
        {
            DataTable dt = new DataTable(); //戻り値用
            string connStr = GetConnectionString();
            using (OracleCommand cmd = new OracleCommand())
            {
                try
                {
                    OracleConnection conn = new OracleConnection();
                    conn.ConnectionString = connStr;
                    //問い合わせ開始
                    //conn.Open();
                    OracleDataAdapter da = new OracleDataAdapter();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    conn.ConnectionString = connStr;
                    da.SelectCommand = cmd;
                    cmd.CommandText = sql;
                    da.Fill(dt);
                    conn.Close();
                }
                catch (Exception)
                {
                    dt = null;
                    GetUser();
                    ErrorLogWrite(user_cd, "OracleSelect", sql.Replace("'", "#"));
                    MessageBox.Show("データベースの処理中にエラーが発生しました。\n" + sql);
                }
            }
            return dt;
        }
        #endregion

        #region MessageLogWrite
        /// <summary>
        /// 受け取った文字列をテーブル TSS_MESSAGE_LOG_F に書き込む
        /// </summary>
        /// <param name="in_user_cd_from">string 送信元ユーザーコード</param>
        /// <param name="in_user_cd_to">string 送信先ユーザーコード</param>
        /// <param name="in_syori_name">string 処理名（タイトル）</param>
        /// <param name="in_naiyou">string メッセージ文字列</param>
        /// <returns>bool</returns>
        public bool MessageLogWrite(string in_user_cd_from, string in_user_cd_to, string in_syori_name, string in_naiyou)
        {
            bool out_bl;
            out_bl = true;
            out_bl = OracleInsert("insert into tss_message_log_f(message_datetime,user_cd_from,user_cd_to,message_syori_name,message_log_naiyou,create_user_cd,create_datetime) values (to_char(systimestamp,'yyyy/mm/dd hh24:mi:ss.FF'),'" + in_user_cd_from + "','" + in_user_cd_to + "','" + in_syori_name + "','" + in_naiyou + "','" + in_user_cd_from + "',sysdate)");
            return out_bl;
        }
        #endregion

        #region ErrorLogWrite
        /// <summary>
        /// 受け取った文字列をテーブル TSS_ERROR_LOG_F に書き込む
        /// 引数：送信先ユーザーコード、発生処理名、メッセージ内容　戻り値：bool型
        /// </summary>
        public bool ErrorLogWrite(string user_cd, string syori_name, string naiyou)
        {
            bool bl = new bool();
            bl = OracleInsert("insert into tss_error_log_f(error_datetime,user_cd,error_syori_name,error_naiyou,create_user_cd,create_datetime) values (to_char(systimestamp,'yyyy/mm/dd hh24:mi:ss.FF'),'" + user_cd + "','" + syori_name + "','" + naiyou + "','" + user_cd + "',sysdate)");
            return bl;
        }
        #endregion

        #region DataTableCSV
        /// <summary>
        /// DataTableをCSVファイルに出力します。
        /// </summary>
        /// <param name="in_dt">出力するDataTable名</param>
        /// <param name="in_SaveFileDialog">ファイルダイアログを使用する時はtrue</param>
        /// <param name="in_csvPath">CSVファイルのフルパス（ファイル名まで含める）※SaveFileDialogがtrueの場合はパスを除いたファイル名</param>
        /// <param name="in_interstring">各データを囲む文字</param>
        /// <param name="in_writeHeader">ヘッダを書き込む時はtrue</param>
        /// <returns>正常:true 失敗:false</returns>
        public Boolean DataTableCSV(DataTable in_dt, bool in_SaveFileDialog, string in_csvPath, string in_interstring, Boolean in_writeHeader)
        {
            //保存するファイルパスとファイル名を決める
            string w_str_filename;
            if (in_SaveFileDialog)
            {
                //SaveFileDialogクラスのインスタンスを作成
                SaveFileDialog sfd = new SaveFileDialog();
                //はじめのファイル名を指定する
                sfd.FileName = in_csvPath;
                //はじめに表示されるフォルダを指定する
                sfd.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                //[ファイルの種類]に表示される選択肢を指定する
                sfd.Filter = "CSVファイル(*.csv)|*.csv|すべてのファイル(*.*)|*.*";
                //タイトルを設定する
                sfd.Title = "保存先のファイルを選択してください";
                //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
                sfd.RestoreDirectory = true;
                //既に存在するファイル名を指定したとき警告する（デフォルトでTrueなので指定する必要はない）
                sfd.OverwritePrompt = true;
                //存在しないパスが指定されたとき警告を表示する（デフォルトでTrueなので指定する必要はない）
                sfd.CheckPathExists = true;
                //ダイアログを表示する
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    //OKボタンがクリックされたとき選択されたファイル名を表示する
                    w_str_filename = sfd.FileName;
                }
                else
                {
                    //OKボタンでない場合はキャンセルとみなし終了する
                    return false;
                }
            }
            else
            {
                w_str_filename = in_csvPath;
            }
            //CSVファイルに書き込むときに使うEncoding
            System.Text.Encoding enc = System.Text.Encoding.GetEncoding("Shift_JIS");
            //書き込むファイルを開く
            StreamWriter sr = new StreamWriter(w_str_filename, false, enc);

            int columncnt = in_dt.Columns.Count;
            int lastcolumncnt = columncnt - 1;
            try
            {
                //ヘッダを書き込む
                if (in_writeHeader)
                {
                    for (int i = 0; i < columncnt; i++)
                    {
                        //ヘッダの取得
                        string field = in_dt.Columns[i].Caption;
                        //データを囲む処理
                        field = in_interstring + field + in_interstring;
                        //フィールドを書き込む
                        sr.Write(field);
                        //カンマを書き込む
                        if (lastcolumncnt > i)
                        {
                            sr.Write(',');
                        }
                    }
                    //改行する
                    sr.Write("\r\n");
                }
                //レコードを書き込む
                foreach (DataRow row in in_dt.Rows)
                {
                    for (int i = 0; i < columncnt; i++)
                    {
                        //フィールドの取得
                        string field = row[i].ToString();
                        //データを囲む処理
                        field = in_interstring + field + in_interstring;
                        //フィールドを書き込む
                        sr.Write(field);
                        //カンマを書き込む
                        if (lastcolumncnt > i)
                        {
                            sr.Write(',');
                        }
                    }
                    //改行する
                    sr.Write("\r\n");
                }
            }
            catch (System.Exception)
            {
                sr.Close();
                return false;
            }
            //閉じる
            sr.Close();
            return true;
        }
        #endregion

        #region StringByte
        /// <summary>半角 1 バイト、全角 2 バイトとして、指定された文字列のバイト数を返します。</summary>
        /// <param name="str">バイト数取得の対象となる文字列。</param>
        /// <returns>半角 1 バイト、全角 2 バイトでカウントされたバイト数。</returns>
        public int StringByte(string str)
        {
            return System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(str);
        }
        #endregion

        #region StringRight
        /// <summary>
        /// 末尾から指定された文字列を取得します。</summary>
        /// <param name="str">
        /// 対象となる文字列。</param>
        /// <param name="len">
        /// 取得する文字数。</param>
        /// <returns>
        /// 文字列</returns>
        public string StringRight(string str, int len)
        {
            if (len < 0)
            {
                return "";
            }
            if (str == null)
            {
                return "";
            }
            string w_space = "";
            for (int i = 0; i < len;i++)
            {
                w_space = w_space + " ";
            }
            w_space = w_space + str;
            return w_space.Substring(w_space.Length - len, len);
        }
        #endregion

        #region HardCopy
        /// <summary>
        /// 呼ばれた時点でのアクティブなウィンドゥのハードコピーをクリップボードに送ります。
        /// 正確にはAlt+PrtScが押された事をOSに送信します。
        /// </summary>
        /// <returns>
        /// 戻り値はありません。
        /// </returns>
        public void HardCopy()
        {
            //Altキー＋Print Screenキーの送信
            SendKeys.SendWait("%{PRTSC}");
            MessageBox.Show("画面をクリップボードにコピーしました。");
        }
        #endregion

        #region kubun_cd_select
        //区分コード選択画面の呼び出し
        public string kubun_cd_select(string in_kubun_cd)
        {
            //マウスのX座標を取得する
            int x = System.Windows.Forms.Cursor.Position.X;
            //マウスのY座標を取得する
            int y = System.Windows.Forms.Cursor.Position.Y;

            string out_kubun_cd = "";   //戻り値用
            frm_kubun_select frm_ks = new frm_kubun_select();

            //フォームをマウスの位置に表示する
            frm_ks.Left = x;
            frm_ks.Top = y;
            frm_ks.StartPosition = FormStartPosition.Manual;

            //子画面のプロパティに値をセットする
            frm_ks.str_kubun_meisyou_cd = in_kubun_cd;
            frm_ks.ShowDialog();
            //子画面から値を取得する
            out_kubun_cd = frm_ks.str_kubun_cd;
            frm_ks.Dispose();
            return out_kubun_cd;
        }
        #endregion

        #region kubun_cd_select（初期選択機能付き）
        //区分コード選択画面の呼び出し 初期値あり版
        public string kubun_cd_select(string in_kubun_cd, string in_initial_cd)
        {
            //マウスのX座標を取得する
            int x = System.Windows.Forms.Cursor.Position.X;
            //マウスのY座標を取得する
            int y = System.Windows.Forms.Cursor.Position.Y;

            string out_kubun_cd = "";   //戻り値用
            frm_kubun_select frm_ks = new frm_kubun_select();

            //フォームをマウスの位置に表示する
            frm_ks.Left = x;
            frm_ks.Top = y;
            frm_ks.StartPosition = FormStartPosition.Manual;

            //子画面のプロパティに値をセットする
            frm_ks.str_kubun_meisyou_cd = in_kubun_cd;
            frm_ks.str_initial_cd = in_initial_cd;
            frm_ks.ShowDialog();
            //子画面から値を取得する
            out_kubun_cd = frm_ks.str_kubun_cd;
            frm_ks.Dispose();
            return out_kubun_cd;
        }
        #endregion

        #region kubun_name_select
        //区分コードから区分名を取得
        public string kubun_name_select(string in_kubun_meisyou_cd, string in_kubun_cd)
        {
            string out_kubun_name = "";   //戻り値用
            //区分名を取得する
            DataTable dt_work = OracleSelect("select kubun_name from tss_kubun_m where kubun_meisyou_cd = '" + in_kubun_meisyou_cd + "' and kubun_cd = '" + in_kubun_cd + "'");
            if (dt_work.Rows.Count <= 0)
            {
                out_kubun_name = "";
            }
            else
            {
                out_kubun_name = dt_work.Rows[0]["kubun_name"].ToString();
            }
            return out_kubun_name;
        }
        #endregion

        #region kubun_cd_select_dt（DataTable版）
        //区分コード選択画面（DataTable版）の呼び出し
        public string kubun_cd_select_dt(string in_kubun_name, DataTable in_dt_kubun)
        {
            //マウスのX座標を取得する
            int x = System.Windows.Forms.Cursor.Position.X;
            //マウスのY座標を取得する
            int y = System.Windows.Forms.Cursor.Position.Y;

            string out_kubun_cd = "";   //戻り値用
            frm_kubun_select_dt frm_ks_dt = new frm_kubun_select_dt();

            //フォームをマウスの位置に表示する
            frm_ks_dt.Left = x;
            frm_ks_dt.Top = y;
            frm_ks_dt.StartPosition = FormStartPosition.Manual;

            //子画面のプロパティに値をセットする
            frm_ks_dt.ppt_str_kubun_name = in_kubun_name;
            frm_ks_dt.ppt_dt_kubun = in_dt_kubun;
            frm_ks_dt.ShowDialog();
            //子画面から値を取得する
            out_kubun_cd = frm_ks_dt.ppt_str_kubun_cd;
            frm_ks_dt.Dispose();
            return out_kubun_cd;
        }
        #endregion

        #region kubun_cd_select_dt（DataTable版＋初期選択機能付き）
        //区分コード選択画面（DataTable版）の呼び出し　初期値あり版
        public string kubun_cd_select_dt(string in_kubun_name, DataTable in_dt_kubun, string in_initial_cd)
        {
            //マウスのX座標を取得する
            int x = System.Windows.Forms.Cursor.Position.X;
            //マウスのY座標を取得する
            int y = System.Windows.Forms.Cursor.Position.Y;

            string out_kubun_cd = "";   //戻り値用
            frm_kubun_select_dt frm_ks_dt = new frm_kubun_select_dt();

            //フォームをマウスの位置に表示する
            frm_ks_dt.Left = x;
            frm_ks_dt.Top = y;
            frm_ks_dt.StartPosition = FormStartPosition.Manual;

            //子画面のプロパティに値をセットする
            frm_ks_dt.ppt_str_kubun_name = in_kubun_name;
            frm_ks_dt.ppt_dt_kubun = in_dt_kubun;
            frm_ks_dt.ppt_str_initial_cd = in_initial_cd;
            frm_ks_dt.ShowDialog();
            //子画面から値を取得する
            out_kubun_cd = frm_ks_dt.ppt_str_kubun_cd;
            frm_ks_dt.Dispose();
            return out_kubun_cd;
        }
        #endregion

        #region seihin_kousei_select_dt
        //製品構成番号選択画面（DataTable版）の呼び出し
        public string seihin_kousei_select_dt(string in_seihin_cd, DataTable in_dt_seihin_kousei_name)
        {
            //マウスのX座標を取得する
            int x = System.Windows.Forms.Cursor.Position.X;
            //マウスのY座標を取得する
            int y = System.Windows.Forms.Cursor.Position.Y;

            string out_seihin_cd = "";   //戻り値用
            string out_seihin_kousei_no = "";   //戻り値用
            frm_seihin_kousei_select frm_sks = new frm_seihin_kousei_select();

            //フォームをマウスの位置に表示する
            frm_sks.Left = x;
            frm_sks.Top = y;
            frm_sks.StartPosition = FormStartPosition.Manual;

            //子画面のプロパティに値をセットする
            frm_sks.ppt_str_seihin_cd = in_seihin_cd;
            frm_sks.ppt_dt_seihin_kousei_name = in_dt_seihin_kousei_name;
            frm_sks.ShowDialog();
            //子画面から値を取得する
            out_seihin_cd = frm_sks.ppt_str_seihin_cd + "                ";
            out_seihin_cd = out_seihin_cd.Substring(0, 16);
            out_seihin_kousei_no = frm_sks.ppt_str_seihin_kousei_no + "  ";
            out_seihin_kousei_no = out_seihin_kousei_no.Substring(0, 2);
            frm_sks.Dispose();
            return out_seihin_cd + out_seihin_kousei_no;
        }
        #endregion

        #region seihin_kousei_select_dt2（製品コードの受け渡しなし）
        //製品構成番号選択画面（製品コードの受け渡しなし）の呼び出し
        public string seihin_kousei_select_dt2(string in_seihin_cd, DataTable in_dt_seihin_kousei_name)
        {
            //マウスのX座標を取得する
            int x = System.Windows.Forms.Cursor.Position.X;
            //マウスのY座標を取得する
            int y = System.Windows.Forms.Cursor.Position.Y;

            string out_seihin_cd = "";   //戻り値用
            string out_seihin_kousei_no = "";   //戻り値用
            frm_seihin_kousei_select frm_sks = new frm_seihin_kousei_select();

            //フォームをマウスの位置に表示する
            frm_sks.Left = x;
            frm_sks.Top = y;
            frm_sks.StartPosition = FormStartPosition.Manual;

            //子画面のプロパティに値をセットする
            frm_sks.ppt_str_seihin_cd = "";
            //frm_sks.ppt_dt_seihin_kousei_name = "";
            frm_sks.ShowDialog();
            //子画面から値を取得する
            out_seihin_cd = frm_sks.ppt_str_seihin_cd + "                ";
            out_seihin_cd = out_seihin_cd.Substring(0, 16);
            out_seihin_kousei_no = frm_sks.ppt_str_seihin_kousei_no + "  ";
            out_seihin_kousei_no = out_seihin_kousei_no.Substring(0, 2);
            frm_sks.Dispose();
            return out_seihin_cd + out_seihin_kousei_no;
        }
        #endregion

        #region search_buhin
        //部品検索画面の呼び出し
        public string search_buhin(string in_mode, string in_cd)
        {
            //マウスのX座標を取得する
            //int x = System.Windows.Forms.Cursor.Position.X;
            //マウスのY座標を取得する
            //int y = System.Windows.Forms.Cursor.Position.Y;

            string out_cd = "";   //戻り値用
            frm_search_buhin frm_sb = new frm_search_buhin();

            //フォームをマウスの位置に表示する
            //frm_sb.Left = x;
            //frm_sb.Top = y;
            //frm_sb.StartPosition = FormStartPosition.Manual;

            //子画面のプロパティに値をセットする
            frm_sb.str_mode = in_mode;
            frm_sb.str_name = in_cd;
            frm_sb.ShowDialog();
            //子画面から値を取得する
            out_cd = frm_sb.str_cd;
            frm_sb.Dispose();
            return out_cd;
        }
        #endregion

        #region search_torihikisaki
        //取引先検索画面の呼び出し
        public string search_torihikisaki(string in_mode, string in_cd)
        {
            //マウスのX座標を取得する
            //int x = System.Windows.Forms.Cursor.Position.X;
            //マウスのY座標を取得する
            //int y = System.Windows.Forms.Cursor.Position.Y;

            string out_cd = "";   //戻り値用
            frm_search_torihikisaki frm_sb = new frm_search_torihikisaki();

            //フォームをマウスの位置に表示する
            //frm_sb.Left = x;
            //frm_sb.Top = y;
            //frm_sb.StartPosition = FormStartPosition.Manual;

            //子画面のプロパティに値をセットする
            frm_sb.str_mode = in_mode;
            frm_sb.str_name = in_cd;
            frm_sb.ShowDialog();
            //子画面から値を取得する
            out_cd = frm_sb.str_cd;
            frm_sb.Dispose();
            return out_cd;
        }
        #endregion

        #region search_juchu
        //受注検索画面の呼び出し
        public string search_juchu(string in_mode, string in_torihikisaki_cd, string in_juchu_cd1, string in_juchu_cd2, string in_seihin_cd)
        {
            //マウスのX座標を取得する
            //int x = System.Windows.Forms.Cursor.Position.X;
            //マウスのY座標を取得する
            //int y = System.Windows.Forms.Cursor.Position.Y;

            string out_cd = "";   //戻り値用
            frm_search_juchu frm_sb = new frm_search_juchu();

            //フォームをマウスの位置に表示する
            //frm_sb.Left = x;
            //frm_sb.Top = y;
            //frm_sb.StartPosition = FormStartPosition.Manual;

            //子画面のプロパティに値をセットする
            frm_sb.str_mode = in_mode;
            frm_sb.str_name = in_torihikisaki_cd;

            frm_sb.in_torihikisaki_cd = in_torihikisaki_cd;
            frm_sb.in_juchu_cd1 = in_juchu_cd1;
            frm_sb.in_juchu_cd2 = in_juchu_cd2;
            frm_sb.in_seihin_cd = in_seihin_cd;

            frm_sb.ShowDialog();
            //子画面から値を取得する
            //※受注検索画面の戻り値は、受注Noの３つの項目を１つの文字列にして返す
            out_cd = frm_sb.str_torihikisaki_cd + frm_sb.str_juchu_cd1 + frm_sb.str_juchu_cd2;
            frm_sb.Dispose();
            return out_cd;
        }
        #endregion

        #region search_seihin
        public string search_seihin(string in_mode, string in_cd)
        {
            //マウスのX座標を取得する
            //int x = System.Windows.Forms.Cursor.Position.X;
            //マウスのY座標を取得する
            //int y = System.Windows.Forms.Cursor.Position.Y;

            string out_cd = "";   //戻り値用
            frm_search_seihin frm_sb = new frm_search_seihin();

            //フォームをマウスの位置に表示する
            //frm_sb.Left = x;
            //frm_sb.Top = y;
            //frm_sb.StartPosition = FormStartPosition.Manual;

            //子画面のプロパティに値をセットする
            frm_sb.str_mode = in_mode;
            frm_sb.str_name = in_cd;
            frm_sb.ShowDialog();
            //子画面から値を取得する
            out_cd = frm_sb.str_cd;
            frm_sb.Dispose();
            return out_cd;
        }
        #endregion

        #region search_syain
        public string search_syain(string in_mode, string in_cd)
        {
            //マウスのX座標を取得する
            //int x = System.Windows.Forms.Cursor.Position.X;
            //マウスのY座標を取得する
            //int y = System.Windows.Forms.Cursor.Position.Y;

            string out_cd = "";   //戻り値用
            frm_search_syain frm_sb = new frm_search_syain();

            //フォームをマウスの位置に表示する
            //frm_sb.Left = x;
            //frm_sb.Top = y;
            //frm_sb.StartPosition = FormStartPosition.Manual;

            //子画面のプロパティに値をセットする
            frm_sb.str_mode = in_mode;
            frm_sb.str_name = in_cd;
            frm_sb.ShowDialog();
            //子画面から値を取得する
            out_cd = frm_sb.str_cd;
            frm_sb.Dispose();
            return out_cd;
        }
        #endregion

        #region select_juchu_cd
        public string select_juchu_cd(DataTable in_dt)
        {
            //マウスのX座標を取得する
            //int x = System.Windows.Forms.Cursor.Position.X;
            //マウスのY座標を取得する
            //int y = System.Windows.Forms.Cursor.Position.Y;

            string out_cd = "";   //戻り値用
            frm_select_juchu frm_sb = new frm_select_juchu();

            //フォームをマウスの位置に表示する
            //frm_sb.Left = x;
            //frm_sb.Top = y;
            //frm_sb.StartPosition = FormStartPosition.Manual;

            //子画面のプロパティに値をセットする
            frm_sb.ppt_str_name = "下記リストから受注を選択してください。";
            frm_sb.ppt_dt_m = in_dt;
            frm_sb.ShowDialog();
            //子画面から値を取得する
            out_cd = frm_sb.ppt_str_juchu_cd2;
            frm_sb.Dispose();
            return out_cd;
        }
        #endregion

        #region GetBinPath
        /// <summary>
        /// TSSシステムのBinのパスを取得する。</summary>
        /// <returns>
        /// string パスを返します。</returns>
        public string GetBinPath()
        {
            //app.configから必要な情報を取得
            string BinPath = ConfigurationManager.AppSettings["BinPath"];
            if (BinPath.Length == 0)
            {
                MessageBox.Show("設定ファイル:BinPathを処理できません。");
                return null;
            }
            else
            {
                fld_BinPath = BinPath;
            }
            //戻り値
            return BinPath;
        }
        #endregion
        
        #region GetSystemPath
        /// <summary>
        /// TSSシステムのデフォルトのパスを取得する。</summary>
        /// <returns>
        /// string パスを返します。</returns>
        public string GetSystemPath()
        {
            //app.configから必要な情報を取得
            string SystemPath = ConfigurationManager.AppSettings["SystemPath"];
            if (SystemPath.Length == 0)
            {
                MessageBox.Show("設定ファイル:SystemPathを処理できません。");
                return null;
            }
            else
            {
                fld_SystemPath = SystemPath;
            }
            //戻り値
            return SystemPath;
        }
        #endregion

        #region GetCsvPath
        /// <summary>
        /// TSSシステムのデフォルトのCSV出力先パスを取得する。</summary>
        /// <returns>
        /// string scv出力先のパスを返します。</returns>
        public string GetCsvPath()
        {
            //app.configから必要な情報を取得
            string CsvPath = ConfigurationManager.AppSettings["CsvPath"];   //データソース名文字列の取得
            if (CsvPath.Length == 0)
            {
                MessageBox.Show("設定ファイル:CsvPathを処理できません。");
                return null;
            }
            else
            {
                fld_CsvPath = CsvPath;
            }
            //戻り値
            return CsvPath;
        }
        #endregion

        #region GetDainichi_cd
        /// <summary>
        /// TSSシステムのデフォルトのダイニチ工業の取引先コードを取得する。</summary>
        /// <returns>
        /// string ダイニチ工業の取引先コードを返します。</returns>
        public string GetDainichi_cd()
        {
            //app.configから必要な情報を取得
            string dainichi_cd = ConfigurationManager.AppSettings["Dainichi_cd"];   //データソース名文字列の取得

            if (dainichi_cd.Length == 0)
            {
                MessageBox.Show("設定ファイル:Dainichi_cdを処理できません。");
                return null;
            }
            else
            {
                fld_dainichi_cd = dainichi_cd;
            }
            //戻り値
            return dainichi_cd;
        }
        #endregion

        #region GetSeq
        /// <summary>
        /// 連番マスタから必要な連番を取得し、取得後連番を＋１する。</summary>
        /// <param name="string in_cd">
        /// 取得する連番の連番コード</param>
        /// <returns>
        /// decimal out_seq 取得した連番。エラー時は0を返します。</returns>
        public decimal GetSeq(string in_cd)
        {
            GetUser();
            decimal out_seq = 0;
            DataTable w_dt = new DataTable();
            w_dt = OracleSelect("select * from tss_seq_m where seq_m_cd = '" + in_cd + "'");
            if (w_dt.Rows.Count == 0)
            {
                out_seq = 0;
            }
            else
            {
                //seqは保存されている値が使用される値
                out_seq = Convert.ToDecimal(w_dt.Rows[0]["seq"]);
                string sql = "update tss_seq_m set seq = '" + (out_seq + 1).ToString() + "',UPDATE_USER_CD = '" + user_cd + "',UPDATE_DATETIME = SYSDATE WHERE seq_m_cd = '" + in_cd + "'";
                //取得後、＋１して書き込む
                if (OracleUpdate(sql))
                {
                    //書込み成功
                }
                else
                {
                    ErrorLogWrite(user_cd, "GetSeq内のOracleUpdate", sql.Replace("'", "#"));
                    MessageBox.Show("データベースの処理中にエラーが発生しました。");
                    out_seq = 0;
                }
            }
            return out_seq;
        }
        #endregion

        #region buhin_nyusyukkoidou
        //部品入出庫画面の呼び出し
        public string buhin_nyusyukkoidou(string in_mode)
        {
            string out_cd = "";   //戻り値用
            frm_buhin_nyusyukkoidou frm_bnsi = new frm_buhin_nyusyukkoidou();
            //子画面のプロパティに値をセットする
            frm_bnsi.str_mode = in_mode;
            frm_bnsi.ShowDialog();
            frm_bnsi.Dispose();
            //子画面から値を取得する
            out_cd = frm_bnsi.str_cd;
            return out_cd;
        }
        #endregion

        #region try_string_to_date
        /// <summary>
        /// 文字列を受け取りdate型に変換できるか（適切な日付か）を調べ、bool型を返す</summary>
        /// <param name="in_str">
        /// 変換前の日付文字列</param>
        /// <returns>
        /// bool true:変換可能 false:変換不能
        /// 変換可能時にプロパティ out_datetime に変換後の値を格納</returns>
        public DateTime out_datetime;
        public bool try_string_to_date(string in_str)
        {
            bool bl;    //戻り値用
            bl = true;
            string w_str = in_str;
            //7文字以下はNG
            if (StringByte(in_str) < 8)
            {
                bl = false;
                return bl;
            }
            //スラッシュがある場合はそのまま使用し、なければスラッシュを加える
            if (in_str.IndexOf("/") == -1)
            {
                w_str = in_str.Substring(0, 4) + "/" + in_str.Substring(4, 2) + "/" + in_str.Substring(6);
            }
            if (DateTime.TryParse(w_str, out out_datetime))
            {
                bl = true;
            }
            else
            {
                bl = false;
            }
            return bl;
        }
        #endregion

        #region try_string_to_time
        /// <summary>
        /// 文字列を受け取りtime型に変換できるか（適切な時刻か）を調べ、bool型を返す</summary>
        /// <param name="in_str">
        /// 変換前の時刻文字列</param>
        /// <returns>
        /// bool true:変換可能 false:変換不能
        /// 変換可能時にプロパティ out_time に変換後の値を格納</returns>
        public DateTime out_time;
        public bool try_string_to_time(string in_str)
        {
            bool bl;    //戻り値用
            bl = true;
            string w_str = in_str;
            //4文字以下はNG
            if (StringByte(in_str) < 4)
            {
                bl = false;
                return bl;
            }
            //コロンがある場合はそのまま使用し、なければコロンを加える
            if (in_str.IndexOf(":") == -1)
            {
                w_str = in_str.Substring(0, 2) + ":" + in_str.Substring(2) + ":00";
            }
            if (DateTime.TryParse(w_str, out out_time))
            {
                bl = true;
            }
            else
            {
                bl = false;
            }
            return bl;
        }
        #endregion

        #region try_string_to_yyyymm
        /// <summary>
        /// 文字列を受け取りdate型に変換できるか（適切な日付か）を調べ、bool型を返す</summary>
        /// <param name="in_str">
        /// 変換前の日付文字列</param>
        /// <returns>
        /// bool true:変換可能 false:変換不能
        /// 変換可能時にプロパティ out_datetime に変換後の値を格納</returns>
        public DateTime out_yyyymm;
        public bool try_string_to_yyyymm(string in_str)
        {
            bool bl;    //戻り値用
            bl = true;
            string w_str = in_str;
            //5文字以下はNG
            if (StringByte(in_str) < 6)
            {
                bl = false;
                return bl;
            }
            //スラッシュがある場合はそのまま使用し、なければスラッシュを加える
            if (in_str.IndexOf("/") == -1)
            {
                w_str = in_str.Substring(0, 4) + "/" + in_str.Substring(4, 2);
            }
            if (DateTime.TryParse(w_str, out out_yyyymm))
            {
                bl = true;
            }
            else
            {
                bl = false;
            }
            return bl;
        }
        #endregion

        #region FirstMonth
        /// <summary>
        /// datetime型を受け取り月初を求めて返す</summary>
        /// <param name="in_datetime">
        /// 月初を求めるdatetime</param>
        /// <returns>
        /// datetime</returns>
        public DateTime FirstMonth(DateTime in_datetime)
        {
            return new DateTime(in_datetime.Year,in_datetime.Month, 1);
        }
        #endregion

        #region string_to_yyyymm
        /// <summary>
        /// 文字列を受け取り、yyyy/mm型にした文字列を返す</summary>
        /// <param name="in_str">
        /// 変換前の日付文字列（yyyymmまたはyyyy/mm）</param>
        /// <returns>string 変換後の文字列 変換不能はnull</returns>
        public string string_to_yyyymm(string in_str)
        {
            string out_str = null;    //戻り値用
            //6文字以下、8文字以上はNG
            if (StringByte(in_str) < 6 || StringByte(in_str) > 8)
            {
                out_str = null;
                return out_str;
            }
            //年月を分割する
            string w_yyyy;
            string w_mm;
            if (StringByte(in_str) == 6)
            {
                w_yyyy = in_str.Substring(0, 4);
                w_mm = in_str.Substring(4, 2);
            }
            else
            {
                w_yyyy = in_str.Substring(0, 4);
                w_mm = in_str.Substring(5, 2);
            }
            //try_string_to_dateメソッドで日付として使えるかチェック
            if (try_string_to_date(w_yyyy + "/" + w_mm + "/01") == false)
            {
                out_str = null;
            }
            else
            {
                out_str = w_yyyy + "/" + w_mm;
            }
            return out_str;
        }
        #endregion

        #region try_string_to_decimal
        /// <summary>
        /// 文字列を受け取りdecimal型に変換し返す</summary>
        /// <param name="in_str">変換前の文字列</param>
        /// <returns>decimal 変換後の値 変換不能時は-999999999</returns>
        public decimal try_string_to_decimal(string in_str)
        {
            decimal out_dou;    //戻り値用
            if (decimal.TryParse(in_str, out out_dou) == false)
            {
                out_dou = -999999999;
            }
            return out_dou;
        }
        #endregion

        #region get_user_name
        /// <summary>
        /// ユーザーコードを受け取りユーザー名を返す</summary>
        /// <param name="in_cd">
        /// ユーザー名を取得するユーザーコード</param>
        /// <returns>
        /// string ユーザー名
        /// エラー等、取得できない場合はnull</returns>
        public string get_user_name(string in_cd)
        {
            string out_str = null;  //戻り値用
            DataTable w_dt = new DataTable();
            w_dt = OracleSelect("select * from tss_user_m where user_cd = '" + in_cd + "'");
            if (w_dt.Rows.Count == 0)
            {
                out_str = null;
            }
            else
            {
                out_str = w_dt.Rows[0]["user_name"].ToString();
            }
            return out_str;
        }
        #endregion
        
        #region get_torihikisaki_name
        /// <summary>
        /// 取引先コードを受け取り取引先名を返す</summary>
        /// <param name="in_cd">
        /// 取引先名を取得する取引先コード</param>
        /// <returns>
        /// string 取引先名
        /// エラー等、取得できない場合はnull</returns>
        public string get_torihikisaki_name(string in_cd)
        {
            string out_str = null;  //戻り値用
            DataTable w_dt = new DataTable();
            w_dt = OracleSelect("select * from tss_torihikisaki_m where torihikisaki_cd = '" + in_cd + "'");
            if (w_dt.Rows.Count == 0)
            {
                out_str = null;
            }
            else
            {
                out_str = w_dt.Rows[0]["torihikisaki_name"].ToString();
            }
            return out_str;
        }
        #endregion

        #region get_busyo_name
        /// <summary>
        /// 部署コードを受け取り部署名を返す</summary>
        /// <param name="in_cd">
        /// 部署名を取得する部署コード</param>
        /// <returns>
        /// string 部署名
        /// エラー等、取得できない場合はnull</returns>
        public string get_busyo_name(string in_cd)
        {
            string out_str = null;  //戻り値用
            DataTable w_dt = new DataTable();
            w_dt = OracleSelect("select * from tss_busyo_m where busyo_cd = '" + in_cd + "'");
            if (w_dt.Rows.Count == 0)
            {
                out_str = null;
            }
            else
            {
                out_str = w_dt.Rows[0]["busyo_name"].ToString();
            }
            return out_str;
        }
        #endregion

        #region get_koutei_name
        /// <summary>
        /// 工程コードを受け取り工程名を返す</summary>
        /// <param name="in_cd">
        /// 工程名を取得する工程コード</param>
        /// <returns>
        /// string 工程名
        /// エラー等、取得できない場合はnull</returns>
        public string get_koutei_name(string in_cd)
        {
            string out_str = null;  //戻り値用
            DataTable w_dt = new DataTable();
            w_dt = OracleSelect("select * from tss_koutei_m where koutei_cd = '" + in_cd + "'");
            if (w_dt.Rows.Count == 0)
            {
                out_str = null;
            }
            else
            {
                out_str = w_dt.Rows[0]["koutei_name"].ToString();
            }
            return out_str;
        }
        #endregion

        #region get_line_name
        /// <summary>
        /// ラインコードを受け取りライン名を返す</summary>
        /// <param name="in_cd">
        /// ライン名を取得するラインコード</param>
        /// <returns>
        /// string ライン名
        /// エラー等、取得できない場合はnull</returns>
        public string get_line_name(string in_cd)
        {
            string out_str = null;  //戻り値用
            DataTable w_dt = new DataTable();
            w_dt = OracleSelect("select * from tss_line_m where line_cd = '" + in_cd + "'");
            if (w_dt.Rows.Count == 0)
            {
                out_str = null;
            }
            else
            {
                out_str = w_dt.Rows[0]["line_name"].ToString();
            }
            return out_str;
        }
        #endregion

        #region get_seihin_name
        /// <summary>
        /// 製品コードを受け取り製品名を返す</summary>
        /// <param name="in_cd">
        /// 製品名を取得する製品コード</param>
        /// <returns>
        /// string 製品名
        /// エラー等、取得できない場合はnull</returns>
        public string get_seihin_name(string in_cd)
        {
            string out_str = null;  //戻り値用
            DataTable w_dt = new DataTable();
            w_dt = OracleSelect("select * from tss_seihin_m where seihin_cd = '" + in_cd + "'");
            if (w_dt.Rows.Count == 0)
            {
                out_str = null;
            }
            else
            {
                out_str = w_dt.Rows[0]["seihin_name"].ToString();
            }
            return out_str;
        }
        #endregion

        #region get_seihin_kousei_no
        /// <summary>
        /// 製品コードを受け取り製品構成番号を返す</summary>
        /// <param name="in_cd">
        /// 製品コード</param>
        /// <returns>
        /// string 製品構成番号
        /// エラー等、取得できない場合はnull</returns>
        public string get_seihin_kousei_no(string in_cd)
        {
            string out_str = null;  //戻り値用
            DataTable w_dt = new DataTable();
            w_dt = OracleSelect("select * from tss_seihin_m where seihin_cd = '" + in_cd + "'");
            if (w_dt.Rows.Count == 0)
            {
                out_str = null;
            }
            else
            {
                out_str = w_dt.Rows[0]["seihin_kousei_no"].ToString();
            }
            return out_str;
        }
        #endregion

        #region get_seihin_kousei_name
        /// <summary>
        /// 製品コードを受け取り製品構成名称を返す</summary>
        /// <param name="in_cd">
        /// 製品コード</param>
        /// <returns>
        /// string 製品構成名称
        /// エラー等、取得できない場合はnull</returns>
        public string get_seihin_kousei_name(string in_cd)
        {
            string out_str = null;  //戻り値用
            DataTable w_dt = new DataTable();
            w_dt = OracleSelect("select * from tss_seihin_m where seihin_cd = '" + in_cd + "'");
            if (w_dt.Rows.Count == 0)
            {
                out_str = null;
            }
            else
            {
                DataTable w_dt2 = new DataTable();
                w_dt2 = OracleSelect("select * from tss_seihin_kousei_name_m where seihin_cd = '" + in_cd + "' and seihin_kousei_no = '" + w_dt.Rows[0]["seihin_kousei_no"].ToString() + "'");
                if (w_dt2.Rows.Count == 0)
                {
                    out_str = null;
                }
                else
                {
                    out_str = w_dt2.Rows[0]["seihin_kousei_name"].ToString();
                }
            }
            return out_str;
        }
        #endregion

        #region get_buhin_name
        /// <summary>
        /// 部品コードを受け取り部品名を返す</summary>
        /// <param name="in_cd">
        /// 部品コード</param>
        /// <returns>
        /// string 部品名
        /// エラー等、取得できない場合はnull</returns>
        public string get_buhin_name(string in_cd)
        {
            string out_str = null;  //戻り値用
            DataTable w_dt = new DataTable();
            w_dt = OracleSelect("select * from tss_buhin_m where buhin_cd = '" + in_cd + "'");
            if (w_dt.Rows.Count == 0)
            {
                out_str = null;
            }
            else
            {
                out_str = w_dt.Rows[0]["buhin_name"].ToString();
            }
            return out_str;
        }
        #endregion

        #region get_zaiko
        /// <summary>
        /// 部品コードと在庫区分を受け取り在庫数を返す</summary>
        /// <param name="string in_cd">
        /// 部品コード</param>
        /// <param name="string in_kbn">
        /// 在庫区分 "01":フリー "02":ロット "03":その他 "**":全て</param>
        /// <returns>
        /// string 在庫数（各区分の合計値を返す）
        /// エラー等、取得できない場合はnull</returns>
        public string get_zaiko(string in_cd, string in_kbn)
        {
            string out_str = null;  //戻り値用
            DataTable w_dt = new DataTable();
            if (in_kbn == "01" || in_kbn == "02")
            {
                w_dt = OracleSelect("select sum(zaiko_su) from tss_buhin_zaiko_m where buhin_cd = '" + in_cd + "' and zaiko_kbn = '" + in_kbn + "'");
            }
            else
            {
                if (in_kbn == "03")
                {
                    w_dt = OracleSelect("select sum(zaiko_su) from tss_buhin_zaiko_m where buhin_cd = '" + in_cd + "' and zaiko_kbn <> '01' and zaiko_kbn <> '02'");
                }
                else
                {
                    if (in_kbn == "**")
                    {
                        w_dt = OracleSelect("select sum(zaiko_su) from tss_buhin_zaiko_m where buhin_cd = '" + in_cd + "'");
                    }
                }
            }
            if (w_dt.Rows.Count == 0)
            {
                out_str = null;
            }
            else
            {
                out_str = w_dt.Rows[0][0].ToString();
            }
            return out_str;
        }
        #endregion

        #region get_zaiko
        /// <summary>
        /// 部品コードと在庫区分"02"と受注Noを受け取り在庫数を返す</summary>
        /// <param name="string in_cd">
        /// 部品コード</param>
        /// <param name="string in_kbn">
        /// 在庫区分 "02":ロット</param>
        /// <param name="string in_torihikisaki_cd">
        /// 取引先コード</param>
        /// <param name="string in_juchu_cd1">
        /// 受注コード1</param>
        /// <param name="string in_juchu_cd2">
        /// 受注コード2</param>
        /// <returns>
        /// string 在庫数（各区分の合計値を返す）
        /// エラー等、取得できない場合はnull</returns>
        public string get_zaiko(string in_cd, string in_kbn, string in_torihikisaki_cd, string in_juchu_cd1, string in_juchu_cd2)
        {
            string out_str = null;  //戻り値用
            DataTable w_dt = new DataTable();
            w_dt = OracleSelect("select sum(zaiko_su) from tss_buhin_zaiko_m where buhin_cd = '" + in_cd + "' and zaiko_kbn = '" + in_kbn + "' and torihikisaki_cd = '" + in_torihikisaki_cd + "' and juchu_cd1 = '" + in_juchu_cd1 + "' and juchu_cd2 = '" + in_juchu_cd2 + "'");
            if (w_dt.Rows.Count == 0)
            {
                out_str = null;
            }
            else
            {
                out_str = w_dt.Rows[0][0].ToString();
            }
            return out_str;
        }
        #endregion

        #region get_seihin_tanka
        /// <summary>
        /// 製品コードを受け取り販売単価を返す</summary>
        /// <param name="in_cd">
        /// 販売単価を取得する製品コード</param>
        /// <returns>
        /// decimal 販売単価
        /// エラー等、取得できない場合は-1</returns>
        public decimal get_seihin_tanka(string in_cd)
        {
            decimal out_dou = -1;  //戻り値用
            DataTable w_dt = new DataTable();
            decimal w_dou;
            w_dt = OracleSelect("select * from tss_seihin_m where seihin_cd = '" + in_cd + "'");
            if (w_dt.Rows.Count == 0)
            {
                out_dou = -1;
            }
            else
            {
                if (decimal.TryParse(w_dt.Rows[0]["hanbai_tanka"].ToString(), out w_dou))
                {
                    out_dou = w_dou;
                }
                else
                {
                    out_dou = -1;
                }
            }
            return out_dou;
        }
        #endregion

        #region get_juchu_to_seihin_cd
        /// <summary>
        /// 受注番号を受け取り製品コードを返す</summary>
        /// <param name="in_torihikisaki_cd">
        /// 製品コードを取得する受注番号の取引先コード</param>
        /// <param name="in_juchu_cd1">
        /// 製品コードを取得する受注番号の受注コード1</param>
        /// <param name="in_juchu_cd2">
        /// 製品コードを取得する受注番号の受注コード2</param>
        /// <returns>
        /// string 製品コード
        /// エラー等、取得できない場合はnull</returns>
        public string get_juchu_to_seihin_cd(string in_torihikisaki_cd, string in_juchu_cd1, string in_juchu_cd2)
        {
            string out_str = null;  //戻り値用
            DataTable w_dt = new DataTable();
            w_dt = OracleSelect("select * from tss_juchu_m where torihikisaki_cd = '" + in_torihikisaki_cd + "' and juchu_cd1 = '" + in_juchu_cd1 + "' and juchu_cd2 = '" + in_juchu_cd2 + "'");
            if (w_dt.Rows.Count == 0)
            {
                out_str = null;
            }
            else
            {
                out_str = w_dt.Rows[0]["seihin_cd"].ToString();
            }
            return out_str;
        }
        #endregion

        #region get_seisan_su
        /// <summary>
        /// 受注番号を受け取り生産数を返す</summary>
        /// <param name="in_torihikisaki_cd">
        /// 取引先コード</param>
        /// <param name="in_juchu_cd1">
        /// 受注コード1</param>
        /// <param name="in_juchu_cd2">
        /// 受注コード2</param>
        /// <returns>
        /// string 生産数
        /// エラー等、取得できない場合はnull</returns>
        public string get_seisan_su(string in_torihikisaki_cd, string in_juchu_cd1, string in_juchu_cd2)
        {
            string out_str = null;  //戻り値用
            DataTable w_dt = new DataTable();
            w_dt = OracleSelect("select * from tss_juchu_m where torihikisaki_cd = '" + in_torihikisaki_cd + "' and juchu_cd1 = '" + in_juchu_cd1 + "' and juchu_cd2 = '" + in_juchu_cd2 + "'");
            if (w_dt.Rows.Count == 0)
            {
                out_str = null;
            }
            else
            {
                out_str = w_dt.Rows[0]["seisan_su"].ToString();
            }
            return out_str;
        }
        #endregion

        #region get_seisankisyu
        /// <summary>
        /// 製品コードと工程コードを受け取り製品工程マスタの生産機種を返す</summary>
        /// <param name="in_seihin_cd">
        /// 製品コード</param>
        /// <param name="in_koutei_cd">
        /// 工程コード</param>
        /// <returns>
        /// string 生産機種
        /// エラー等、取得できない場合はnull</returns>
        public string get_seisankisyu(string in_seihin_cd, string in_koutei_cd)
        {
            string out_str = null;  //戻り値用
            DataTable w_dt = new DataTable();
            w_dt = OracleSelect("select * from tss_seisan_koutei_m where seihin_cd = '" + in_seihin_cd + "' and koutei_cd = '" + in_koutei_cd + "'");
            if (w_dt.Rows.Count == 0)
            {
                out_str = null;
            }
            else
            {
                out_str = w_dt.Rows[0]["seisankisyu"].ToString();
            }
            return out_str;
        }
        #endregion

        #region check_juchu
        /// <summary>
        /// 受注番号を受け取り受注マスタにあるか確認し、boolを返す</summary>
        /// <param name="in_torihikisaki_cd">
        /// 受注番号の取引先コード</param>
        /// <param name="in_juchu_cd1">
        /// 受注番号の受注コード1</param>
        /// <param name="in_juchu_cd2">
        /// 受注番号の受注コード2</param>
        /// <returns>
        /// bool
        /// エラー等、取得できない場合はfalse</returns>
        public bool check_juchu(string in_torihikisaki_cd, string in_juchu_cd1, string in_juchu_cd2)
        {
            bool out_bl = true;  //戻り値用
            DataTable w_dt = new DataTable();
            w_dt = OracleSelect("select * from tss_juchu_m where torihikisaki_cd = '" + in_torihikisaki_cd + "' and juchu_cd1 = '" + in_juchu_cd1 + "' and juchu_cd2 = '" + in_juchu_cd2 + "'");
            if (w_dt.Rows.Count == 0)
            {
                out_bl = false;
            }
            else
            {
                out_bl = true;
            }
            return out_bl;
        }
        #endregion

        #region zaiko_proc
        /// <summary>
        /// 部品コード、在庫区分、受注番号の3項目、在庫の加減数を受け取り、在庫の加減算を行い書き込む
        /// ロット在庫の消し込みの場合、ロット在庫が足りない場合、不足分はフリー在庫で処理する
        /// ロット在庫の消し込みの場合、受注マスタの売上完了フラグが立っている場合、ロット在庫の残りはフリー在庫に移動する
        /// レコードがない場合は作成する</summary>
        /// <param name="string in_buhin_cd">在庫の加減算を行う部品コード</param>
        /// <param name="string in_zaiko_kbn">加減算を行う在庫区分</param>
        /// <param name="string in_torihikisaki_cd">ロット在庫の取引先コード</param>
        /// <param name="string in_juchu_cd1">ロット在庫の受注コード1</param>
        /// <param name="string in_juchu_cd2">ロット在庫の受注コード2</param>
        /// <param name="decimal in_su">加減算数</param>
        /// <param name="decimal in_gyou">履歴行</param>
        /// <param name="string in_bikou">備考（伝票番号等）</param>
        /// <param name="string in_syori_kbn">処理した画面（01:入出庫 02:売上 03:製品構成一括</param>
        /// <param name="string in_seisiki_torihikisaki_cd">受注マスタ参照用の正式な取引先コード</param>
        /// <param name="string in_seisiki_juchu_cd1">受注マスタ参照用の正式な受注コード１</param>
        /// <param name="string in_seisiki_juchu_cd2">受注マスタ参照用の正式な受注コード２</param>
        /// <returns>
        /// bool true:正常終了 false:異常終了
        /// エラー等、取得できない場合はnull</returns>
        public int ppt_gyou;   //履歴に書き込んだ行番号
        public bool zaiko_proc(string in_buhin_cd, string in_zaiko_kbn, string in_torihikisaki_cd, string in_juchu_cd1, string in_juchu_cd2, decimal in_su, decimal in_rireki_no, int in_gyou, string in_bikou, string in_syori_kbn,string in_seisiki_torihikisaki_cd,string in_seisiki_juchu_cd1, string in_seisiki_juchu_cd2)
        {
            bool bl = true;  //戻り値用
            string w_sql;

            DataTable w_dt = new DataTable();
            DataTable w_dt2 = new DataTable();  //フリー在庫以外の在庫がマイナスになる場合用
            decimal w_zaiko_su;
            //decimal w_zaiko_su_2;
            decimal w_zaiko_su_3;
            bool w_rireki_bl;   //履歴書込み用
            string w_rireki_kbn;
            if (in_su >= 0)
            {
                w_rireki_kbn = "02";    //出庫（通常消込）
            }
            else
            {
                w_rireki_kbn = "01";    //入庫（売上の取消）
            }
            ppt_gyou = in_gyou;
            decimal w_dou_su = in_su;    //履歴書込み用の数量
            decimal nokori_su;//余剰数
            //在庫履歴に書き込む数量は、
            //マイナス売上時はマイナス数量でいいが、それ以外は正数で書き込む
            if(in_syori_kbn == "03" && in_su < 0)
            {
                w_dou_su = w_dou_su * -1;
            }
            //在庫の消しこみ
      ////////////////////////////////////////////////////////////////////////////////////////////////////////////////      
            //ロット在庫のレコードがない場合
            if (in_zaiko_kbn == "01")
            {
                //ロット在庫がなかった場合は、受注ＣＤ1、受注ＣＤ2（9999999999999999）の在庫を使用する
                w_dt = OracleSelect("select * from tss_buhin_zaiko_m where buhin_cd = '" + in_buhin_cd + "' and zaiko_kbn = '" + in_zaiko_kbn + "' and torihikisaki_cd = '" + in_torihikisaki_cd + "' and juchu_cd1 = '" + in_juchu_cd1 + "' and juchu_cd2 = '9999999999999999'");
                //受注コード1+　受注ＣＤ2（9999999999999999）レコードがある場合
                if (w_dt.Rows.Count > 0)
                {
                    decimal.TryParse(w_dt.Rows[0]["zaiko_su"].ToString(), out w_zaiko_su);
                    decimal lot2_zaiko_su = w_zaiko_su;
                    w_zaiko_su = w_zaiko_su - in_su;
                    //在庫が残る場合、受注マスタの売上完了フラグを見に行く
                    if (w_zaiko_su >= 0)
                    {
                        DataTable w_dt_flg = new DataTable();
                        w_dt_flg = OracleSelect("select * from tss_juchu_m where torihikisaki_cd = '" + in_seisiki_torihikisaki_cd + "' and juchu_cd1 = '" + in_seisiki_juchu_cd1 + "'");
                        int rc = w_dt_flg.Rows.Count;
                        //未完のフラグがあった場合、フリーには移動しない。
                        int[] a = new int[rc]; // フラグ確認データテーブルの行数分の整数型配列を用意。
                        // 値の入力
                        for (int i = 0; i < a.Length; ++i) // a.Length は配列 a の長さ。
                        {
                            a[i] = int.Parse(w_dt_flg.Rows[i]["uriage_kanryou_flg"].ToString());
                        }
                        // 配列値の計算
                        int kakezan = 1;
                        //行数が２以上の場合
                        if (rc >= 2)
                        {
                            for (int i = 0; i < a.Length; ++i)
                            {
                                kakezan = kakezan * a[i];
                            }
                        }
                        //行数が1以下
                        else
                        {
                            for (int i = 0; i < a.Length; ++i)
                            {
                                kakezan = a[i];
                            }
                        }
                        if (kakezan == 0) //売上完了フラグが立っていないものがある場合
                        {
                            //通常に在庫の更新
                            GetUser();
                            w_sql = "UPDATE tss_buhin_zaiko_m SET zaiko_su = '" + w_zaiko_su.ToString("0.00") + "',update_user_cd = '" + user_cd + "',update_datetime = sysdate where buhin_cd = '" + in_buhin_cd + "' and zaiko_kbn = '" + w_dt.Rows[0]["zaiko_kbn"].ToString() + "' and torihikisaki_cd = '" + w_dt.Rows[0]["torihikisaki_cd"].ToString() + "' and juchu_cd1 = '" + w_dt.Rows[0]["juchu_cd1"].ToString() + "' and juchu_cd2 = '9999999999999999' ";
                            if (OracleUpdate(w_sql) == false)
                            {
                                GetUser();
                                ErrorLogWrite(user_cd, "OracleUpdate", w_sql.Replace("'", "#"));
                                MessageBox.Show("データベースの処理中にエラーが発生しました。zaiko_proc 04");
                                bl = false;
                            }
                            //在庫履歴へ書き込み
                            if (in_su >= 0)
                            {
                                decimal w_rireki_seq;
                                w_rireki_seq = GetSeq("02");
                                w_rireki_bl = OracleInsert("insert into tss_buhin_nyusyukko_m (buhin_syori_kbn,buhin_syori_no,seq,buhin_syori_date,buhin_cd,zaiko_kbn,torihikisaki_cd,juchu_cd1,juchu_cd2,suryou,syori_kbn,bikou,create_user_cd,create_datetime) values ('02','" + w_rireki_seq.ToString("0000000000") + "','" + ppt_gyou.ToString() + "',sysdate,'" + in_buhin_cd + "','" + w_dt.Rows[0]["zaiko_kbn"].ToString() + "','" + w_dt.Rows[0]["torihikisaki_cd"].ToString() + "','" + w_dt.Rows[0]["juchu_cd1"].ToString() + "','" + w_dt.Rows[0]["juchu_cd2"].ToString() + "','" + w_dou_su.ToString("0.00") + "','" + in_syori_kbn + "','" + in_bikou + "の消し込み','" + user_cd + "',sysdate)");
                            }
                            else
                            {
                                decimal w_zettaichi;
                                w_zettaichi = w_dou_su * -1;
                                decimal w_rireki_seq;
                                w_rireki_seq = GetSeq("01");
                                w_rireki_bl = OracleInsert("insert into tss_buhin_nyusyukko_m (buhin_syori_kbn,buhin_syori_no,seq,buhin_syori_date,buhin_cd,zaiko_kbn,torihikisaki_cd,juchu_cd1,juchu_cd2,suryou,syori_kbn,bikou,create_user_cd,create_datetime) values ('01','" + w_rireki_seq.ToString("0000000000") + "','" + ppt_gyou.ToString() + "',sysdate,'" + in_buhin_cd + "','" + w_dt.Rows[0]["zaiko_kbn"].ToString() + "','" + w_dt.Rows[0]["torihikisaki_cd"].ToString() + "','" + w_dt.Rows[0]["juchu_cd1"].ToString() + "','" + w_dt.Rows[0]["juchu_cd2"].ToString() + "','" + w_zettaichi.ToString("0.00") + "','" + in_syori_kbn + "','" + in_bikou + "の消し込み','" + user_cd + "',sysdate)");
                            }
                            ppt_gyou++;
                        }
                        if (kakezan == 1) //すべて売上完了フラグが立っている場合
                        {
                            //全て完了フラグが立っていたら、フリーに振替する。
                            //まず、フリー在庫でない在庫の書き込み（０にする）
                            GetUser();
                            w_sql = "UPDATE tss_buhin_zaiko_m SET zaiko_su = '0',update_user_cd = '" + user_cd + "',update_datetime = sysdate where buhin_cd = '" + in_buhin_cd + "' and zaiko_kbn = '" + w_dt.Rows[0]["zaiko_kbn"].ToString() + "' and torihikisaki_cd = '" + in_torihikisaki_cd + "' and juchu_cd1 = '" + w_dt.Rows[0]["juchu_cd1"].ToString() + "' and juchu_cd2 = '9999999999999999' ";
                            if (OracleUpdate(w_sql) == false)
                            {
                                GetUser();
                                ErrorLogWrite(user_cd, "OracleUpdate", w_sql.Replace("'", "#"));
                                MessageBox.Show("データベースの処理中にエラーが発生しました。zaiko_proc 01");
                                bl = false;
                            }
                            //99ロット入出庫履歴へ書き込み
                            decimal w_rireki_seq;
                            w_rireki_seq = GetSeq("02");
                            w_rireki_bl = OracleInsert("insert into tss_buhin_nyusyukko_m (buhin_syori_kbn,buhin_syori_no,seq,buhin_syori_date,buhin_cd,zaiko_kbn,torihikisaki_cd,juchu_cd1,juchu_cd2,suryou,syori_kbn,bikou,create_user_cd,create_datetime) values ('02','" + w_rireki_seq.ToString("0000000000") + "','" + ppt_gyou.ToString() + "',sysdate,'" + in_buhin_cd + "','" + w_dt.Rows[0]["zaiko_kbn"].ToString() + "','" + w_dt.Rows[0]["torihikisaki_cd"].ToString() + "','" + w_dt.Rows[0]["juchu_cd1"].ToString() + "','" + w_dt.Rows[0]["juchu_cd2"].ToString() + "','" + w_dou_su.ToString("0.00") + "','" + in_syori_kbn + "','" + in_bikou + "の消し込み','" + user_cd + "',sysdate)");
                            ppt_gyou++;
                            //次に残りの在庫数をフリー在庫で処理する
                            w_dt2 = OracleSelect("select * from tss_buhin_zaiko_m where buhin_cd = '" + in_buhin_cd + "' and zaiko_kbn = '" + "01" + "' and torihikisaki_cd = '999999' and juchu_cd1 = '9999999999999999' and juchu_cd2 = '9999999999999999'");
                            if (w_dt2.Rows.Count == 0)
                            {
                                GetUser();
                                ErrorLogWrite(user_cd, "tss.zeiko_procでフリー在庫レコードが無い(CODE:03)", "zaiko_proc(部品コード" + in_buhin_cd + ",在庫区分" + in_zaiko_kbn + ",取引先コード" + in_torihikisaki_cd + ",受注コード1" + in_juchu_cd1 + ",受注コード2" + in_juchu_cd2 + ",数" + in_su + ")");
                                bl = false;
                                return bl;
                            }
                            //99ロット在庫数　＝　売上数　なら、フリー在庫の操作は不要
                            if (w_zaiko_su != 0)
                            {
                                //99ロット入出庫履歴へ書き込み（余剰分）
                                decimal w_rireki_seq2;
                                w_rireki_seq2 = GetSeq("02");
                                w_rireki_bl = OracleInsert("insert into tss_buhin_nyusyukko_m (buhin_syori_kbn,buhin_syori_no,seq,buhin_syori_date,buhin_cd,zaiko_kbn,torihikisaki_cd,juchu_cd1,juchu_cd2,suryou,syori_kbn,bikou,create_user_cd,create_datetime) values ('02','" + w_rireki_seq2.ToString("0000000000") + "','" + ppt_gyou.ToString() + "',sysdate,'" + in_buhin_cd + "','" + w_dt.Rows[0]["zaiko_kbn"].ToString() + "','" + w_dt.Rows[0]["torihikisaki_cd"].ToString() + "','" + w_dt.Rows[0]["juchu_cd1"].ToString() + "','" + w_dt.Rows[0]["juchu_cd2"].ToString() + "','" + w_zaiko_su.ToString("0.00") + "','" + in_syori_kbn + "','" + in_bikou + "分のロット在庫の余剰分をフリー在庫に振替6','" + user_cd + "',sysdate)");
                                ppt_gyou++;
                                //フリー在庫に残りの在庫数を更新
                                decimal syorimae_f_zaiko_su;//処理前フリー在庫数
                                decimal f_zaiko_su;//フリー在庫数
                                decimal.TryParse(w_dt2.Rows[0]["zaiko_su"].ToString(), out syorimae_f_zaiko_su);
                                f_zaiko_su = syorimae_f_zaiko_su + w_zaiko_su;
                                w_sql = "UPDATE tss_buhin_zaiko_m SET zaiko_su = '" + f_zaiko_su.ToString("0.00") + "',update_user_cd = '" + user_cd + "',update_datetime = sysdate WHERE buhin_cd = '" + in_buhin_cd + "' and zaiko_kbn = '01' and torihikisaki_cd = '999999' and juchu_cd1 = '9999999999999999' and juchu_cd2 = '9999999999999999'";
                                if (OracleUpdate(w_sql) == false)
                                {
                                    GetUser();
                                    ErrorLogWrite(user_cd, "OracleUpdate", w_sql.Replace("'", "#"));
                                    MessageBox.Show("データベースの処理中にエラーが発生しました。zaiko_proc 02");
                                    bl = false;
                                }
                                //フリー在庫の入出庫履歴へ書き込み
                                decimal w_rireki_seq3;
                                w_rireki_seq3 = GetSeq("01");
                                w_rireki_bl = OracleInsert("insert into tss_buhin_nyusyukko_m (buhin_syori_kbn,buhin_syori_no,seq,buhin_syori_date,buhin_cd,zaiko_kbn,torihikisaki_cd,juchu_cd1,juchu_cd2,suryou,syori_kbn,bikou,create_user_cd,create_datetime) values ('01','" + w_rireki_seq3.ToString("0000000000") + "','" + ppt_gyou.ToString() + "',sysdate,'" + in_buhin_cd + "','" + "01" + "','999999','" + "9999999999999999" + "','" + "9999999999999999" + "','" + w_zaiko_su.ToString("0.00") + "','" + in_syori_kbn + "','" + in_bikou + "分のロット在庫の余剰分をフリー在庫に振替7','" + user_cd + "',sysdate)");
                                ppt_gyou++;
                            }
                        }
                    }
                    //99ロット在庫がマイナスになる場合、フリー在庫で処理する
                    else
                    {
                        //全て完了フラグが立っていたら、フリーに振替する。
                        //まず、99ロット在庫の書き込み（０にする）
                        GetUser();
                        w_sql = "UPDATE tss_buhin_zaiko_m SET zaiko_su = '0',update_user_cd = '" + user_cd + "',update_datetime = sysdate where buhin_cd = '" + in_buhin_cd + "' and zaiko_kbn = '" + w_dt.Rows[0]["zaiko_kbn"].ToString() + "' and torihikisaki_cd = '" + in_torihikisaki_cd + "' and juchu_cd1 = '" + w_dt.Rows[0]["juchu_cd1"].ToString() + "' and juchu_cd2 = '9999999999999999' ";
                        if (OracleUpdate(w_sql) == false)
                        {
                            GetUser();
                            ErrorLogWrite(user_cd, "OracleUpdate", w_sql.Replace("'", "#"));
                            MessageBox.Show("データベースの処理中にエラーが発生しました。zaiko_proc 01");
                            bl = false;
                        }
                        //99ロット在庫の入出庫履歴へ書き込み
                        decimal w_rireki_seq;
                        if(w_rireki_kbn == "01")
                        {
                            w_rireki_seq = GetSeq("01");
                        }
                        else
                        {
                            w_rireki_seq = GetSeq("02");
                        }
                        w_rireki_bl = OracleInsert("insert into tss_buhin_nyusyukko_m (buhin_syori_kbn,buhin_syori_no,seq,buhin_syori_date,buhin_cd,zaiko_kbn,torihikisaki_cd,juchu_cd1,juchu_cd2,suryou,syori_kbn,bikou,create_user_cd,create_datetime) values ('" + w_rireki_kbn + "','" + w_rireki_seq.ToString("0000000000") + "','" + ppt_gyou.ToString() + "',sysdate,'" + in_buhin_cd + "','" + w_dt.Rows[0]["zaiko_kbn"].ToString() + "','" + w_dt.Rows[0]["torihikisaki_cd"].ToString() + "','" + w_dt.Rows[0]["juchu_cd1"].ToString() + "','" + w_dt.Rows[0]["juchu_cd2"].ToString() + "','" + lot2_zaiko_su.ToString("0.00") + "','" + in_syori_kbn + "','" + in_bikou + "の消し込み','" + user_cd + "',sysdate)");
                        ppt_gyou++;
                        //次に残りの在庫数をフリー在庫で処理する
                        w_dt2 = OracleSelect("select * from tss_buhin_zaiko_m where buhin_cd = '" + in_buhin_cd + "' and zaiko_kbn = '" + "01" + "' and torihikisaki_cd = '999999' and juchu_cd1 = '9999999999999999' and juchu_cd2 = '9999999999999999'");
                        if (w_dt2.Rows.Count == 0)
                        {
                            GetUser();
                            ErrorLogWrite(user_cd, "tss.zeiko_procでフリー在庫レコードが無い(CODE:03)", "zaiko_proc(部品コード" + in_buhin_cd + ",在庫区分" + in_zaiko_kbn + ",取引先コード" + in_torihikisaki_cd + ",受注コード1" + in_juchu_cd1 + ",受注コード2" + in_juchu_cd2 + ",数" + in_su + ")");
                            bl = false;
                            return bl;
                        }
                        //フリー在庫に残りの在庫数を更新
                        decimal syorimae_f_zaiko_su;//処理前フリー在庫数
                        decimal f_zaiko_su;//フリー在庫数
                        decimal.TryParse(w_dt2.Rows[0]["zaiko_su"].ToString(), out syorimae_f_zaiko_su);
                        f_zaiko_su = syorimae_f_zaiko_su + w_zaiko_su;
                        w_sql = "UPDATE tss_buhin_zaiko_m SET zaiko_su = '" + f_zaiko_su.ToString("0.00") + "',update_user_cd = '" + user_cd + "',update_datetime = sysdate WHERE buhin_cd = '" + in_buhin_cd + "' and zaiko_kbn = '01' and torihikisaki_cd = '999999' and juchu_cd1 = '9999999999999999' and juchu_cd2 = '9999999999999999'";
                        if (OracleUpdate(w_sql) == false)
                        {
                            GetUser();
                            ErrorLogWrite(user_cd, "OracleUpdate", w_sql.Replace("'", "#"));
                            MessageBox.Show("データベースの処理中にエラーが発生しました。zaiko_proc 02");
                            bl = false;
                        }
                        w_zaiko_su = w_zaiko_su * -1;
                        //在庫履歴へ書き込み
                        decimal w_rireki_seq4;
                        if (w_rireki_kbn == "01")
                        {
                            w_rireki_seq4 = GetSeq("01");
                        }
                        else
                        {
                            w_rireki_seq4 = GetSeq("02");
                        }
                        w_rireki_bl = OracleInsert("insert into tss_buhin_nyusyukko_m (buhin_syori_kbn,buhin_syori_no,seq,buhin_syori_date,buhin_cd,zaiko_kbn,torihikisaki_cd,juchu_cd1,juchu_cd2,suryou,syori_kbn,bikou,create_user_cd,create_datetime) values ('" + w_rireki_kbn + "','" + w_rireki_seq4.ToString("0000000000") + "','" + ppt_gyou.ToString() + "',sysdate,'" + in_buhin_cd + "','" + "01" + "','999999','" + "9999999999999999" + "','" + "9999999999999999" + "','" + w_zaiko_su.ToString() + "','" + in_syori_kbn + "','" + in_bikou + "分のロット在庫不足分の消し込み','" + user_cd + "',sysdate)");
                        ppt_gyou++;
                    }
                }
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //受注コード1+　受注ＣＤ2（9999999999999999）レコードがない場合
                if (w_dt.Rows.Count == 0)
                {
                    //受注ＣＤ1+受注ＣＤ2（9999999999999999）在庫もなかった場合は、フリー在庫を使用する
                    w_dt = OracleSelect("select * from tss_buhin_zaiko_m where buhin_cd = '" + in_buhin_cd + "' and zaiko_kbn = '" + "01" + "' and torihikisaki_cd = '999999' and juchu_cd1 = '9999999999999999' and juchu_cd2 = '9999999999999999'");
                    if (w_dt.Rows.Count == 0)
                    {
                        GetUser();
                        ErrorLogWrite(user_cd, "tss.zeiko_procでフリー在庫レコードが無い(CODE:01)", "zaiko_proc(部品コード" + in_buhin_cd + ",在庫区分" + in_zaiko_kbn + ",取引先コード" + in_torihikisaki_cd + ",受注コード1" + in_juchu_cd1 + ",受注コード2" + in_juchu_cd2 + ",数" + in_su + ")");
                        bl = false;
                        return bl;
                    }
                    decimal.TryParse(w_dt.Rows[0]["zaiko_su"].ToString(), out w_zaiko_su);
                    w_zaiko_su = w_zaiko_su - in_su;
                    w_sql = "UPDATE tss_buhin_zaiko_m SET zaiko_su = '" + w_zaiko_su.ToString("0.00") + "',update_user_cd = '" + user_cd + "',update_datetime = sysdate WHERE buhin_cd = '" + in_buhin_cd + "' and zaiko_kbn = '01' and torihikisaki_cd = '999999' and juchu_cd1 = '9999999999999999' and juchu_cd2 = '9999999999999999'";
                    if (OracleUpdate(w_sql) == false)
                    {
                        GetUser();
                        ErrorLogWrite(user_cd, "OracleUpdate", w_sql.Replace("'", "#"));
                        MessageBox.Show("データベースの処理中にエラーが発生しました。zaiko_proc 02");
                        bl = false;
                    }
                    //入出庫履歴へ書き込み
                    decimal w_rireki_seq;
                    w_rireki_seq = GetSeq("02");
                    w_rireki_bl = OracleInsert("insert into tss_buhin_nyusyukko_m (buhin_syori_kbn,buhin_syori_no,seq,buhin_syori_date,buhin_cd,zaiko_kbn,torihikisaki_cd,juchu_cd1,juchu_cd2,suryou,syori_kbn,bikou,create_user_cd,create_datetime) values ('02','" + w_rireki_seq.ToString("0000000000") + "','" + ppt_gyou.ToString() + "',sysdate,'" + in_buhin_cd + "','" + "01" + "','999999','" + "9999999999999999" + "','" + "9999999999999999" + "','" + w_dou_su.ToString() + "','" + in_syori_kbn + "','" + in_bikou + "分のロット在庫不足分の消し込み','" + user_cd + "',sysdate)");
                    ppt_gyou++;
                }
            }
            //ロット在庫のレコードがある場合
            //①ロット在庫（受注ＣＤ2　!= '9999999999999999'）
            //if (in_juchu_cd2 != "9999999999999999")
            if (in_zaiko_kbn == "02" && in_juchu_cd2 != "9999999999999999")
            {
                //消し込む在庫レコードをw_dtに入れる
                w_dt = OracleSelect("select * from tss_buhin_zaiko_m where buhin_cd = '" + in_buhin_cd + "' and zaiko_kbn = '" + in_zaiko_kbn + "' and torihikisaki_cd = '" + in_torihikisaki_cd + "' and juchu_cd1 = '" + in_juchu_cd1 + "' and juchu_cd2 = '" + in_juchu_cd2 + "'");
                //ロット在庫ありの場合
                if (w_dt.Rows.Count != 0)
                {
                    //在庫数量の計算
                    decimal.TryParse(w_dt.Rows[0]["zaiko_su"].ToString(), out w_zaiko_su);
                    w_zaiko_su = w_zaiko_su - in_su;
                    //在庫が残る場合、受注マスタの売上完了フラグを見に行く
                    if (w_zaiko_su >= 0)
                    {
                        string w_uriage_kanryou_flg;
                        DataTable w_dt_flg = new DataTable();
                        w_dt_flg = OracleSelect("select * from tss_juchu_m where torihikisaki_cd = '" + in_seisiki_torihikisaki_cd + "' and juchu_cd1 = '" + in_seisiki_juchu_cd1 + "' and juchu_cd2 = '" + in_seisiki_juchu_cd2 + "'");
                        w_uriage_kanryou_flg = w_dt_flg.Rows[0]["uriage_kanryou_flg"].ToString();
                        //売上完了フラグが立っていれば、残りの在庫をフリーに移動する
                        if (w_uriage_kanryou_flg == "1")
                        {
                            //まず、ロット在庫の残りを０にする
                            GetUser();
                            w_sql = "UPDATE tss_buhin_zaiko_m SET zaiko_su = '0',update_user_cd = '" + user_cd + "',update_datetime = sysdate where buhin_cd = '" + in_buhin_cd + "' and zaiko_kbn = '" + w_dt.Rows[0]["zaiko_kbn"].ToString() + "' and torihikisaki_cd = '" + in_torihikisaki_cd + "' and juchu_cd1 = '" + w_dt.Rows[0]["juchu_cd1"].ToString() + "' and juchu_cd2 = '" + w_dt.Rows[0]["juchu_cd2"].ToString() + "'";
                            if (OracleUpdate(w_sql) == false)
                            {
                                GetUser();
                                ErrorLogWrite(user_cd, "OracleUpdate", w_sql.Replace("'", "#"));
                                MessageBox.Show("データベースの処理中にエラーが発生しました。zaiko_proc 01");
                                bl = false;
                            }
                            //入出庫履歴へ書き込み
                            decimal w_rireki_seq;
                            if (w_rireki_kbn == "01")
                            {
                                w_rireki_seq = GetSeq("01");
                            }
                            else
                            {
                                w_rireki_seq = GetSeq("02");
                            }
                            w_rireki_bl = OracleInsert("insert into tss_buhin_nyusyukko_m (buhin_syori_kbn,buhin_syori_no,seq,buhin_syori_date,buhin_cd,zaiko_kbn,torihikisaki_cd,juchu_cd1,juchu_cd2,suryou,syori_kbn,bikou,create_user_cd,create_datetime) values ('" + w_rireki_kbn + "','" + w_rireki_seq.ToString("0000000000") + "','" + ppt_gyou.ToString() + "',sysdate,'" + in_buhin_cd + "','" + w_dt.Rows[0]["zaiko_kbn"].ToString() + "','" + w_dt.Rows[0]["torihikisaki_cd"].ToString() + "','" + w_dt.Rows[0]["juchu_cd1"].ToString() + "','" + w_dt.Rows[0]["juchu_cd2"].ToString() + "','" + w_dou_su.ToString("0.00") + "','" + in_syori_kbn + "','" + in_bikou + "の消し込み','" + user_cd + "',sysdate)");
                            ppt_gyou++;
                            //次に残りの在庫数をフリー在庫で処理する
                            w_dt2 = OracleSelect("select * from tss_buhin_zaiko_m where buhin_cd = '" + in_buhin_cd + "' and zaiko_kbn = '" + "01" + "' and torihikisaki_cd = '999999' and juchu_cd1 = '9999999999999999' and juchu_cd2 = '9999999999999999'");
                            if (w_dt2.Rows.Count == 0)
                            {
                                GetUser();
                                ErrorLogWrite(user_cd, "tss.zeiko_procでフリー在庫レコードが無い(CODE:03)", "zaiko_proc(部品コード" + in_buhin_cd + ",在庫区分" + in_zaiko_kbn + ",取引先コード" + in_torihikisaki_cd + ",受注コード1" + in_juchu_cd1 + ",受注コード2" + in_juchu_cd2 + ",数" + in_su + ")");
                                bl = false;
                                return bl;
                            }
                            //フリー在庫数
                            decimal syorimae_f_zaiko_su;//処理前フリー在庫数
                            decimal.TryParse(w_dt2.Rows[0]["zaiko_su"].ToString(), out syorimae_f_zaiko_su);
                            //フリー在庫に余剰ロット在庫数を追加
                            nokori_su = w_zaiko_su;
                            w_zaiko_su = syorimae_f_zaiko_su + w_zaiko_su;
                            //フリー在庫の在庫マスタをw_zaiko_suで更新
                            w_sql = "UPDATE tss_buhin_zaiko_m SET zaiko_su = '" + w_zaiko_su.ToString("0.00") + "',update_user_cd = '" + user_cd + "',update_datetime = sysdate WHERE buhin_cd = '" + in_buhin_cd + "' and zaiko_kbn = '01' and torihikisaki_cd = '999999' and juchu_cd1 = '9999999999999999' and juchu_cd2 = '9999999999999999'";
                            if (OracleUpdate(w_sql) == false)
                            {
                                GetUser();
                                ErrorLogWrite(user_cd, "OracleUpdate", w_sql.Replace("'", "#"));
                                MessageBox.Show("データベースの処理中にエラーが発生しました。zaiko_proc 02");
                                bl = false;
                            }
                            if (nokori_su != 0)
                            {
                                //ロット在庫の入出庫履歴へ書き込み（出庫）
                                decimal w_rireki_seq5;
                                w_rireki_seq5 = GetSeq("02");
                                w_rireki_bl = OracleInsert("insert into tss_buhin_nyusyukko_m (buhin_syori_kbn,buhin_syori_no,seq,buhin_syori_date,buhin_cd,zaiko_kbn,torihikisaki_cd,juchu_cd1,juchu_cd2,suryou,syori_kbn,bikou,create_user_cd,create_datetime) values ('02','" + w_rireki_seq5.ToString("0000000000") + "','" + ppt_gyou.ToString() + "',sysdate,'" + in_buhin_cd + "','" + w_dt.Rows[0]["zaiko_kbn"].ToString() + "','" + w_dt.Rows[0]["torihikisaki_cd"].ToString() + "','" + w_dt.Rows[0]["juchu_cd1"].ToString() + "','" + w_dt.Rows[0]["juchu_cd2"].ToString() + "','" + nokori_su.ToString("0.00") + "','" + in_syori_kbn + "','" + in_bikou + "分のロット在庫の余剰分をフリー在庫に振替0','" + user_cd + "',sysdate)");
                                ppt_gyou++;
                                //フリー在庫の入出庫履歴へ書き込み（入庫）
                                decimal w_rireki_seq6;
                                w_rireki_seq6 = GetSeq("01");
                                w_rireki_bl = OracleInsert("insert into tss_buhin_nyusyukko_m (buhin_syori_kbn,buhin_syori_no,seq,buhin_syori_date,buhin_cd,zaiko_kbn,torihikisaki_cd,juchu_cd1,juchu_cd2,suryou,syori_kbn,bikou,create_user_cd,create_datetime) values ('01','" + w_rireki_seq6.ToString("0000000000") + "','" + ppt_gyou.ToString() + "',sysdate,'" + in_buhin_cd + "','" + "01" + "','999999','" + "9999999999999999" + "','" + "9999999999999999" + "','" + nokori_su.ToString("0.00") + "','" + in_syori_kbn + "','" + in_bikou + "分のロット在庫の余剰分をフリー在庫に振替1','" + user_cd + "',sysdate)");
                                ppt_gyou++;
                            }
                            //受注コード1+999999の在庫をフリーに振替え
                            //受注コード1　+　999999999　の在庫を検索
                            DataTable w_dt_2 = new DataTable();
                            w_dt_2 = OracleSelect("select * from tss_buhin_zaiko_m where buhin_cd = '" + in_buhin_cd + "' and zaiko_kbn = '" + in_zaiko_kbn + "' and torihikisaki_cd = '" + in_torihikisaki_cd + "' and juchu_cd1 = '" + in_juchu_cd1 + "' and juchu_cd2 = '9999999999999999'");
                            //レコードありの場合、マイナスになる分を加減計算
                            if (w_dt_2.Rows.Count > 0)
                            {
                                decimal syorimae_99lot_zaiko_su;//処理前99ロット在庫数
                                decimal.TryParse(w_dt_2.Rows[0]["zaiko_su"].ToString(), out syorimae_99lot_zaiko_su);
                                //在庫が残る場合、売上完了フラグを見に行くが、得意先コード+受注コード1　で受注マスタを検索し、すべてのレコードでフラグが立っていないと消しこまないようにする
                                if (w_zaiko_su >= 0)
                                {
                                    w_dt_flg = OracleSelect("select torihikisaki_cd,juchu_cd1,juchu_cd2,uriage_kanryou_flg from tss_juchu_m where torihikisaki_cd = '" + in_seisiki_torihikisaki_cd + "' and juchu_cd1 = '" + in_seisiki_juchu_cd1 + "'");
                                    int rc = w_dt_flg.Rows.Count;
                                    //未完のフラグがあった場合、フリーには移動しない。
                                    int[] a = new int[rc]; // フラグ確認データテーブルの行数分の整数型配列を用意。
                                    // 値の入力
                                    for (int i = 0; i < a.Length; ++i) // a.Length は配列 a の長さ。
                                    {
                                        a[i] = int.Parse(w_dt_flg.Rows[i]["uriage_kanryou_flg"].ToString());
                                    }
                                    // 配列値の計算
                                    int kakezan = 1;
                                    //行数が２以上の場合
                                    if (rc >= 2)
                                    {
                                        for (int i = 0; i < a.Length; ++i)
                                        {
                                            kakezan = kakezan * a[i];
                                        }
                                    }
                                    //行数が1以下
                                    else
                                    {
                                        for (int i = 0; i < a.Length; ++i)
                                        {
                                            kakezan = a[i];
                                        }
                                    }
                                    if (kakezan == 0) //売上完了フラグが立っていないものがある場合
                                    {
                                        //何もしない
                                    }
                                    if (kakezan == 1) //すべて売上完了フラグが立っている場合
                                    {
                                        //全て完了フラグが立っていたら、フリーに振替する
                                        //まず、99ロットの在庫の書き込み（０にする）
                                        GetUser();
                                        w_sql = "UPDATE tss_buhin_zaiko_m SET zaiko_su = '0',update_user_cd = '" + user_cd + "',update_datetime = sysdate where buhin_cd = '" + in_buhin_cd + "' and zaiko_kbn = '" + w_dt.Rows[0]["zaiko_kbn"].ToString() + "' and torihikisaki_cd = '" + in_torihikisaki_cd + "' and juchu_cd1 = '" + w_dt.Rows[0]["juchu_cd1"].ToString() + "' and juchu_cd2 = '9999999999999999' ";
                                        if (OracleUpdate(w_sql) == false)
                                        {
                                            GetUser();
                                            ErrorLogWrite(user_cd, "OracleUpdate", w_sql.Replace("'", "#"));
                                            MessageBox.Show("データベースの処理中にエラーが発生しました。zaiko_proc 01");
                                            bl = false;
                                        }
                                        //入出庫履歴へ書き込み
                                        if (syorimae_99lot_zaiko_su != 0)
                                        {
                                            decimal w_rireki_seq7;
                                            w_rireki_seq7 = GetSeq("02");
                                            w_rireki_bl = OracleInsert("insert into tss_buhin_nyusyukko_m (buhin_syori_kbn,buhin_syori_no,seq,buhin_syori_date,buhin_cd,zaiko_kbn,torihikisaki_cd,juchu_cd1,juchu_cd2,suryou,syori_kbn,bikou,create_user_cd,create_datetime) values ('02','" + w_rireki_seq7.ToString("0000000000") + "','" + ppt_gyou.ToString() + "',sysdate,'" + in_buhin_cd + "','" + w_dt.Rows[0]["zaiko_kbn"].ToString() + "','" + w_dt.Rows[0]["torihikisaki_cd"].ToString() + "','" + w_dt.Rows[0]["juchu_cd1"].ToString() + "','9999999999999999','" + syorimae_99lot_zaiko_su.ToString("0.00") + "','" + in_syori_kbn + "','" + in_bikou + "分のロット在庫の余剰分をフリー在庫に振替2','" + user_cd + "',sysdate)");
                                           ppt_gyou++;
                                        }
                                        //次に残りの在庫数をフリー在庫で処理する
                                        w_dt2 = OracleSelect("select * from tss_buhin_zaiko_m where buhin_cd = '" + in_buhin_cd + "' and zaiko_kbn = '" + "01" + "' and torihikisaki_cd = '999999' and juchu_cd1 = '9999999999999999' and juchu_cd2 = '9999999999999999'");
                                        if (w_dt2.Rows.Count == 0)
                                        {
                                            GetUser();
                                            ErrorLogWrite(user_cd, "tss.zeiko_procでフリー在庫レコードが無い(CODE:03)", "zaiko_proc(部品コード" + in_buhin_cd + ",在庫区分" + in_zaiko_kbn + ",取引先コード" + in_torihikisaki_cd + ",受注コード1" + in_juchu_cd1 + ",受注コード2" + in_juchu_cd2 + ",数" + in_su + ")");
                                            bl = false;
                                            return bl;
                                        }
                                        //フリー在庫に残りの在庫数を更新
                                        decimal f_zaiko_su;//フリー在庫数
                                        decimal.TryParse(w_dt2.Rows[0]["zaiko_su"].ToString(), out f_zaiko_su);
                                        w_zaiko_su = f_zaiko_su + syorimae_99lot_zaiko_su;
                                        w_sql = "UPDATE tss_buhin_zaiko_m SET zaiko_su = '" + w_zaiko_su.ToString("0.00") + "',update_user_cd = '" + user_cd + "',update_datetime = sysdate WHERE buhin_cd = '" + in_buhin_cd + "' and zaiko_kbn = '01' and torihikisaki_cd = '999999' and juchu_cd1 = '9999999999999999' and juchu_cd2 = '9999999999999999'";
                                        if (OracleUpdate(w_sql) == false)
                                        {
                                            GetUser();
                                            ErrorLogWrite(user_cd, "OracleUpdate", w_sql.Replace("'", "#"));
                                            MessageBox.Show("データベースの処理中にエラーが発生しました。zaiko_proc 02");
                                            bl = false;
                                        }
                                        if (syorimae_99lot_zaiko_su != 0 )
                                        {
                                            //フリー在庫の入出庫履歴へ書き込み(フリー在庫の入庫)
                                            decimal w_rireki_seq8;
                                            w_rireki_seq8 = GetSeq("01");
                                            w_rireki_bl = OracleInsert("insert into tss_buhin_nyusyukko_m (buhin_syori_kbn,buhin_syori_no,seq,buhin_syori_date,buhin_cd,zaiko_kbn,torihikisaki_cd,juchu_cd1,juchu_cd2,suryou,syori_kbn,bikou,create_user_cd,create_datetime) values ('01','" + w_rireki_seq8.ToString("0000000000") + "','" + ppt_gyou.ToString() + "',sysdate,'" + in_buhin_cd + "','" + "01" + "','999999','" + "9999999999999999" + "','" + "9999999999999999" + "','" + syorimae_99lot_zaiko_su.ToString("0.00") + "','" + in_syori_kbn + "','" + in_bikou + "分のロット在庫の余剰分をフリー在庫に振替3','" + user_cd + "',sysdate)");
                                            ppt_gyou++;
                                        }
                                    }
                                }
                            }
                        }
                        //ロット在庫が余り、売上完了フラグが0の場合（売上途中）
                        else
                        {
                            //通常に在庫の更新
                            GetUser();
                            w_sql = "UPDATE tss_buhin_zaiko_m SET zaiko_su = '" + w_zaiko_su.ToString("0.00") + "',update_user_cd = '" + user_cd + "',update_datetime = sysdate where buhin_cd = '" + in_buhin_cd + "' and zaiko_kbn = '" + w_dt.Rows[0]["zaiko_kbn"].ToString() + "' and torihikisaki_cd = '" + w_dt.Rows[0]["torihikisaki_cd"].ToString() + "' and juchu_cd1 = '" + w_dt.Rows[0]["juchu_cd1"].ToString() + "' and juchu_cd2 = '" + w_dt.Rows[0]["juchu_cd2"].ToString() + "'";
                            if (OracleUpdate(w_sql) == false)
                            {
                                GetUser();
                                ErrorLogWrite(user_cd, "OracleUpdate", w_sql.Replace("'", "#"));
                                MessageBox.Show("データベースの処理中にエラーが発生しました。zaiko_proc 03");
                                bl = false;
                            }
                            //入出庫履歴へ書き込み
                            decimal w_rireki_seq9;
                            w_rireki_seq9 = GetSeq("02");
                            w_rireki_bl = OracleInsert("insert into tss_buhin_nyusyukko_m (buhin_syori_kbn,buhin_syori_no,seq,buhin_syori_date,buhin_cd,zaiko_kbn,torihikisaki_cd,juchu_cd1,juchu_cd2,suryou,syori_kbn,bikou,create_user_cd,create_datetime) values ('02','" + w_rireki_seq9.ToString("0000000000") + "','" + ppt_gyou.ToString() + "',sysdate,'" + in_buhin_cd + "','" + w_dt.Rows[0]["zaiko_kbn"].ToString() + "','" + w_dt.Rows[0]["torihikisaki_cd"].ToString() + "','" + w_dt.Rows[0]["juchu_cd1"].ToString() + "','" + w_dt.Rows[0]["juchu_cd2"].ToString() + "','" + w_dou_su.ToString("0.00") + "','" + in_syori_kbn + "','" + in_bikou + "の消し込み','" + user_cd + "',sysdate)");
                            ppt_gyou++;
                        }
                    }
                    //在庫がマイナスになる場合（ロット在庫＜売上）
                    if (w_zaiko_su < 0)
                    {
                        //まず、ロット在庫の残りを０にする
                        GetUser();
                        w_sql = "UPDATE tss_buhin_zaiko_m SET zaiko_su = '0',update_user_cd = '" + user_cd + "',update_datetime = sysdate where buhin_cd = '" + in_buhin_cd + "' and zaiko_kbn = '" + w_dt.Rows[0]["zaiko_kbn"].ToString() + "' and torihikisaki_cd = '" + in_torihikisaki_cd + "' and juchu_cd1 = '" + w_dt.Rows[0]["juchu_cd1"].ToString() + "' and juchu_cd2 = '" + w_dt.Rows[0]["juchu_cd2"].ToString() + "'";
                        if (OracleUpdate(w_sql) == false)
                        {
                            GetUser();
                            ErrorLogWrite(user_cd, "OracleUpdate", w_sql.Replace("'", "#"));
                            MessageBox.Show("データベースの処理中にエラーが発生しました。zaiko_proc 01");
                            bl = false;
                        }
                        //不足数量の計算
                        decimal.TryParse(w_dt.Rows[0]["zaiko_su"].ToString(), out w_zaiko_su);
                        decimal husoku_su;//不足在庫数
                        husoku_su = in_su - w_zaiko_su;
                        //入出庫履歴へ書き込み
                        decimal w_rireki_seq10;
                        w_rireki_seq10 = GetSeq("02");
                        w_rireki_bl = OracleInsert("insert into tss_buhin_nyusyukko_m (buhin_syori_kbn,buhin_syori_no,seq,buhin_syori_date,buhin_cd,zaiko_kbn,torihikisaki_cd,juchu_cd1,juchu_cd2,suryou,syori_kbn,bikou,create_user_cd,create_datetime) values ('02','" + w_rireki_seq10.ToString("0000000000") + "','" + ppt_gyou.ToString() + "',sysdate,'" + in_buhin_cd + "','" + w_dt.Rows[0]["zaiko_kbn"].ToString() + "','" + w_dt.Rows[0]["torihikisaki_cd"].ToString() + "','" + w_dt.Rows[0]["juchu_cd1"].ToString() + "','" + w_dt.Rows[0]["juchu_cd2"].ToString() + "','" + w_zaiko_su.ToString("0.00") + "','" + in_syori_kbn + "','" + in_bikou + "の消し込み','" + user_cd + "',sysdate)");
                        ppt_gyou++;
                        //受注コード1　+　999999999　の在庫を検索
                        DataTable w_dt_2 = new DataTable();
                        w_dt_2 = OracleSelect("select * from tss_buhin_zaiko_m where buhin_cd = '" + in_buhin_cd + "' and zaiko_kbn = '" + in_zaiko_kbn + "' and torihikisaki_cd = '" + in_torihikisaki_cd + "' and juchu_cd1 = '" + in_juchu_cd1 + "' and juchu_cd2 = '9999999999999999'");
                        //レコードありの場合、マイナスになる分を加減計算
                        if (w_dt_2.Rows.Count > 0)
                        {
                            decimal.TryParse(w_dt_2.Rows[0]["zaiko_su"].ToString(), out w_zaiko_su_3);
                            w_zaiko_su = w_zaiko_su_3 - husoku_su;
                            //99ロットの在庫が残る場合、売上完了フラグを見に行くが、得意先コード+受注コード1　で受注マスタを検索し、すべてのレコードでフラグが立っていないと消しこまないようにする
                            if (w_zaiko_su >= 0)
                            {
                                DataTable w_dt_flg = new DataTable();
                                w_dt_flg = OracleSelect("select torihikisaki_cd,juchu_cd1,juchu_cd2,uriage_kanryou_flg from tss_juchu_m where torihikisaki_cd = '" + in_seisiki_torihikisaki_cd + "' and juchu_cd1 = '" + in_seisiki_juchu_cd1 + "'");
                                int rc = w_dt_flg.Rows.Count;
                                //未完のフラグがあった場合、フリーには移動しない。
                                int[] a = new int[rc]; // フラグ確認データテーブルの行数分の整数型配列を用意。
                                // 値の入力
                                for (int i = 0; i < a.Length; ++i) // a.Length は配列 a の長さ。
                                {
                                    a[i] = int.Parse(w_dt_flg.Rows[i]["uriage_kanryou_flg"].ToString());
                                }
                                // 配列値の計算
                                int kakezan = 1;
                                //行数が２以上の場合
                                if (rc >= 2)
                                {
                                    for (int i = 0; i < a.Length; ++i)
                                    {
                                        kakezan = kakezan * a[i];
                                    }
                                }
                                //行数が1以下
                                else
                                {
                                    for (int i = 0; i < a.Length; ++i)
                                    {
                                        kakezan = a[i];
                                    }
                                }
                                if (kakezan == 0) //売上完了フラグが立っていないものがある場合
                                {
                                    //通常に在庫の更新
                                    GetUser();
                                    w_sql = "UPDATE tss_buhin_zaiko_m SET zaiko_su = '" + w_zaiko_su.ToString("0.00") + "',update_user_cd = '" + user_cd + "',update_datetime = sysdate where buhin_cd = '" + in_buhin_cd + "' and zaiko_kbn = '" + w_dt.Rows[0]["zaiko_kbn"].ToString() + "' and torihikisaki_cd = '" + w_dt.Rows[0]["torihikisaki_cd"].ToString() + "' and juchu_cd1 = '" + w_dt.Rows[0]["juchu_cd1"].ToString() + "' and juchu_cd2 = '9999999999999999' ";
                                    if (OracleUpdate(w_sql) == false)
                                    {
                                        GetUser();
                                        ErrorLogWrite(user_cd, "OracleUpdate", w_sql.Replace("'", "#"));
                                        MessageBox.Show("データベースの処理中にエラーが発生しました。zaiko_proc 04");
                                        bl = false;
                                    }
                                    //入出庫履歴へ書き込み
                                    decimal w_rireki_seq11;
                                    w_rireki_seq11 = GetSeq("02");
                                    w_rireki_bl = OracleInsert("insert into tss_buhin_nyusyukko_m (buhin_syori_kbn,buhin_syori_no,seq,buhin_syori_date,buhin_cd,zaiko_kbn,torihikisaki_cd,juchu_cd1,juchu_cd2,suryou,syori_kbn,bikou,create_user_cd,create_datetime) values ('02','" + w_rireki_seq11.ToString("0000000000") + "','" + ppt_gyou.ToString() + "',sysdate,'" + in_buhin_cd + "','" + w_dt.Rows[0]["zaiko_kbn"].ToString() + "','" + w_dt.Rows[0]["torihikisaki_cd"].ToString() + "','" + w_dt.Rows[0]["juchu_cd1"].ToString() + "','9999999999999999','" + husoku_su.ToString("0.00") + "','" + in_syori_kbn + "','" + in_bikou + "の消し込み','" + user_cd + "',sysdate)");
                                    ppt_gyou++;
                                }
                                if (kakezan == 1) //すべて売上完了フラグが立っている場合
                                {
                                    //全て完了フラグが立っていたら、フリーに振替する
                                    //まず、フリー在庫でない在庫の書き込み（０にする）
                                    GetUser();
                                    w_sql = "UPDATE tss_buhin_zaiko_m SET zaiko_su = '0',update_user_cd = '" + user_cd + "',update_datetime = sysdate where buhin_cd = '" + in_buhin_cd + "' and zaiko_kbn = '" + w_dt.Rows[0]["zaiko_kbn"].ToString() + "' and torihikisaki_cd = '" + in_torihikisaki_cd + "' and juchu_cd1 = '" + w_dt.Rows[0]["juchu_cd1"].ToString() + "' and juchu_cd2 = '9999999999999999' ";
                                    if (OracleUpdate(w_sql) == false)
                                    {
                                        GetUser();
                                        ErrorLogWrite(user_cd, "OracleUpdate", w_sql.Replace("'", "#"));
                                        MessageBox.Show("データベースの処理中にエラーが発生しました。zaiko_proc 01");
                                        bl = false;
                                    }
                                    //入出庫履歴へ書き込み
                                    decimal w_rireki_seq12;
                                    w_rireki_seq12 = GetSeq("02");
                                    w_rireki_bl = OracleInsert("insert into tss_buhin_nyusyukko_m (buhin_syori_kbn,buhin_syori_no,seq,buhin_syori_date,buhin_cd,zaiko_kbn,torihikisaki_cd,juchu_cd1,juchu_cd2,suryou,syori_kbn,bikou,create_user_cd,create_datetime) values ('02','" + w_rireki_seq12.ToString("0000000000") + "','" + ppt_gyou.ToString() + "',sysdate,'" + in_buhin_cd + "','" + w_dt.Rows[0]["zaiko_kbn"].ToString() + "','" + w_dt.Rows[0]["torihikisaki_cd"].ToString() + "','" + w_dt.Rows[0]["juchu_cd1"].ToString() + "','" + w_dt.Rows[0]["juchu_cd2"].ToString() + "','" + w_zaiko_su.ToString("0.00") + "','" + in_syori_kbn + "','" + in_bikou + "余剰在庫をフリー在庫に振替4','" + user_cd + "',sysdate)");
                                    ppt_gyou++;
                                    //次に残りの在庫数をフリー在庫で処理する
                                    w_dt2 = OracleSelect("select * from tss_buhin_zaiko_m where buhin_cd = '" + in_buhin_cd + "' and zaiko_kbn = '" + "01" + "' and torihikisaki_cd = '999999' and juchu_cd1 = '9999999999999999' and juchu_cd2 = '9999999999999999'");
                                    if (w_dt2.Rows.Count == 0)
                                    {
                                        GetUser();
                                        ErrorLogWrite(user_cd, "tss.zeiko_procでフリー在庫レコードが無い(CODE:03)", "zaiko_proc(部品コード" + in_buhin_cd + ",在庫区分" + in_zaiko_kbn + ",取引先コード" + in_torihikisaki_cd + ",受注コード1" + in_juchu_cd1 + ",受注コード2" + in_juchu_cd2 + ",数" + in_su + ")");
                                        bl = false;
                                        return bl;
                                    }
                                    //フリー在庫に残りの在庫数を更新
                                    decimal syorimae_f_zaiko_su;//処理前フリー在庫数
                                    decimal f_zaiko_su;//フリー在庫数
                                    decimal.TryParse(w_dt2.Rows[0]["zaiko_su"].ToString(), out syorimae_f_zaiko_su);
                                    f_zaiko_su = syorimae_f_zaiko_su + w_zaiko_su;
                                    w_sql = "UPDATE tss_buhin_zaiko_m SET zaiko_su = '" + f_zaiko_su.ToString("0.00") + "',update_user_cd = '" + user_cd + "',update_datetime = sysdate WHERE buhin_cd = '" + in_buhin_cd + "' and zaiko_kbn = '01' and torihikisaki_cd = '999999' and juchu_cd1 = '9999999999999999' and juchu_cd2 = '9999999999999999'";
                                    if (OracleUpdate(w_sql) == false)
                                    {
                                        GetUser();
                                        ErrorLogWrite(user_cd, "OracleUpdate", w_sql.Replace("'", "#"));
                                        MessageBox.Show("データベースの処理中にエラーが発生しました。zaiko_proc 02");
                                        bl = false;
                                    }
                                    //フリー在庫の入出庫履歴へ書き込み
                                    decimal w_rireki_seq13;
                                    w_rireki_seq13 = GetSeq("01");
                                    w_rireki_bl = OracleInsert("insert into tss_buhin_nyusyukko_m (buhin_syori_kbn,buhin_syori_no,seq,buhin_syori_date,buhin_cd,zaiko_kbn,torihikisaki_cd,juchu_cd1,juchu_cd2,suryou,syori_kbn,bikou,create_user_cd,create_datetime) values ('01','" + w_rireki_seq13.ToString("0000000000") + "','" + ppt_gyou.ToString() + "',sysdate,'" + in_buhin_cd + "','" + "01" + "','999999','" + "9999999999999999" + "','" + "9999999999999999" + "','" + husoku_su.ToString("0.00") + "','" + in_syori_kbn + "','" + in_bikou + "分のロット在庫の余剰分をフリー在庫に振替5','" + user_cd + "',sysdate)");
                                    ppt_gyou++;
                                }
                             }
                            //99ロットの在庫がマイナスになる場合 
                            //if (w_zaiko_su < 0)
                            else
                            {
                                //フリー在庫で処理する
                                //まず、フリー在庫でない在庫の書き込み（０にする）
                                GetUser();
                                w_sql = "UPDATE tss_buhin_zaiko_m SET zaiko_su = '0',update_user_cd = '" + user_cd + "',update_datetime = sysdate where buhin_cd = '" + in_buhin_cd + "' and zaiko_kbn = '" + w_dt_2.Rows[0]["zaiko_kbn"].ToString() + "' and torihikisaki_cd = '" + in_torihikisaki_cd + "' and juchu_cd1 = '" + w_dt_2.Rows[0]["juchu_cd1"].ToString() + "' and juchu_cd2 = '9999999999999999'";
                                if (OracleUpdate(w_sql) == false)
                                {
                                    GetUser();
                                    ErrorLogWrite(user_cd, "OracleUpdate", w_sql.Replace("'", "#"));
                                    MessageBox.Show("データベースの処理中にエラーが発生しました。zaiko_proc 01");
                                    bl = false;
                                }
                                //入出庫履歴へ書き込み
                                decimal w_rireki_seq14;
                                w_rireki_seq14 = GetSeq("02");
                                w_rireki_bl = OracleInsert("insert into tss_buhin_nyusyukko_m (buhin_syori_kbn,buhin_syori_no,seq,buhin_syori_date,buhin_cd,zaiko_kbn,torihikisaki_cd,juchu_cd1,juchu_cd2,suryou,syori_kbn,bikou,create_user_cd,create_datetime) values ('02','" + w_rireki_seq14.ToString("0000000000") + "','" + ppt_gyou.ToString() + "',sysdate,'" + in_buhin_cd + "','" + w_dt_2.Rows[0]["zaiko_kbn"].ToString() + "','" + w_dt_2.Rows[0]["torihikisaki_cd"].ToString() + "','" + w_dt_2.Rows[0]["juchu_cd1"].ToString() + "','" + w_dt_2.Rows[0]["juchu_cd2"].ToString() + "','" + w_zaiko_su_3.ToString("0.00") + "','" + in_syori_kbn + "','" + in_bikou + "の消し込み','" + user_cd + "',sysdate)");
                                ppt_gyou++;
                                //次に残りの在庫数をフリー在庫で処理する
                                w_dt2 = OracleSelect("select * from tss_buhin_zaiko_m where buhin_cd = '" + in_buhin_cd + "' and zaiko_kbn = '" + "01" + "' and torihikisaki_cd = '999999' and juchu_cd1 = '9999999999999999' and juchu_cd2 = '9999999999999999'");
                                if (w_dt2.Rows.Count == 0)
                                {
                                    GetUser();
                                    ErrorLogWrite(user_cd, "tss.zeiko_procでフリー在庫レコードが無い(CODE:03)", "zaiko_proc(部品コード" + in_buhin_cd + ",在庫区分" + in_zaiko_kbn + ",取引先コード" + in_torihikisaki_cd + ",受注コード1" + in_juchu_cd1 + ",受注コード2" + in_juchu_cd2 + ",数" + in_su + ")");
                                    bl = false;
                                    return bl;
                                }
                                //フリー在庫に残りの在庫数を更新
                                decimal f_zaiko_su;//フリー在庫数
                                decimal.TryParse(w_dt2.Rows[0]["zaiko_su"].ToString(), out f_zaiko_su);
                                w_zaiko_su = f_zaiko_su + w_zaiko_su;
                                w_sql = "UPDATE tss_buhin_zaiko_m SET zaiko_su = '" + w_zaiko_su.ToString("0.00") + "',update_user_cd = '" + user_cd + "',update_datetime = sysdate WHERE buhin_cd = '" + in_buhin_cd + "' and zaiko_kbn = '01' and torihikisaki_cd = '999999' and juchu_cd1 = '9999999999999999' and juchu_cd2 = '9999999999999999'";
                                if (OracleUpdate(w_sql) == false)
                                {
                                    GetUser();
                                    ErrorLogWrite(user_cd, "OracleUpdate", w_sql.Replace("'", "#"));
                                    MessageBox.Show("データベースの処理中にエラーが発生しました。zaiko_proc 02");
                                    bl = false;
                                }
                                //入出庫履歴へ書き込み
                                decimal f_kesikomi_su;//フリー在庫から消しこむ数
                                f_kesikomi_su = f_zaiko_su - w_zaiko_su;
                                decimal w_rireki_seq15;
                                w_rireki_seq15 = GetSeq("02");
                                w_rireki_bl = OracleInsert("insert into tss_buhin_nyusyukko_m (buhin_syori_kbn,buhin_syori_no,seq,buhin_syori_date,buhin_cd,zaiko_kbn,torihikisaki_cd,juchu_cd1,juchu_cd2,suryou,syori_kbn,bikou,create_user_cd,create_datetime) values ('02','" + w_rireki_seq15.ToString("0000000000") + "','" + ppt_gyou.ToString() + "',sysdate,'" + in_buhin_cd + "','" + "01" + "','999999','" + "9999999999999999" + "','" + "9999999999999999" + "','" + f_kesikomi_su.ToString("0.00") + "','" + in_syori_kbn + "','" + in_bikou + "分のロット在庫不足分の消し込み5','" + user_cd + "',sysdate)");
                                ppt_gyou++;
                            }
                        }
                    }
                }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////  
                //ロット在庫のレコードがない場合
                if (w_dt.Rows.Count == 0)
                {
                    //ロット在庫がなかった場合は、受注ＣＤ1、受注ＣＤ2（9999999999999999）の在庫を使用する
                    if (in_zaiko_kbn == "02")
                    {
                        w_dt = OracleSelect("select * from tss_buhin_zaiko_m where buhin_cd = '" + in_buhin_cd + "' and zaiko_kbn = '" + in_zaiko_kbn + "' and torihikisaki_cd = '" + in_torihikisaki_cd + "' and juchu_cd1 = '" + in_juchu_cd1 + "' and juchu_cd2 = '9999999999999999'");
                        //受注コード1+　受注ＣＤ2（9999999999999999）レコードがある場合
                        if (w_dt.Rows.Count > 0)
                        {
                           decimal.TryParse(w_dt.Rows[0]["zaiko_su"].ToString(), out w_zaiko_su);
                        　 decimal lot2_zaiko_su = w_zaiko_su;
                           w_zaiko_su = w_zaiko_su - in_su;
                          //在庫が残る場合、受注マスタの売上完了フラグを見に行く
                           if (w_zaiko_su >= 0)
                           {
                               DataTable w_dt_flg = new DataTable();
                               w_dt_flg = OracleSelect("select * from tss_juchu_m where torihikisaki_cd = '" + in_seisiki_torihikisaki_cd + "' and juchu_cd1 = '" + in_seisiki_juchu_cd1 + "'");
                               int rc = w_dt_flg.Rows.Count;
                               //未完のフラグがあった場合、フリーには移動しない。
                               int[] a = new int[rc]; // フラグ確認データテーブルの行数分の整数型配列を用意。
                               // 値の入力
                               for (int i = 0; i < a.Length; ++i) // a.Length は配列 a の長さ。
                               {
                                   a[i] = int.Parse(w_dt_flg.Rows[i]["uriage_kanryou_flg"].ToString());
                               }
                               // 配列値の計算
                               int kakezan = 1;
                               //行数が２以上の場合
                               if(rc >= 2)
                               {
                                   for (int i = 0; i < a.Length; ++i)
                                   {
                                       kakezan = kakezan * a[i];
                                   }
                               }
                               //行数が1以下
                               else
                               {
                                   for (int i = 0; i < a.Length; ++i)
                                   {
                                       kakezan = a[i];
                                   }
                               }
                               if(kakezan == 0) //売上完了フラグが立っていないものがある場合
                               {
                                   //通常に在庫の更新
                                   GetUser();
                                   w_sql = "UPDATE tss_buhin_zaiko_m SET zaiko_su = '" + w_zaiko_su.ToString("0.00") + "',update_user_cd = '" + user_cd + "',update_datetime = sysdate where buhin_cd = '" + in_buhin_cd + "' and zaiko_kbn = '" + w_dt.Rows[0]["zaiko_kbn"].ToString() + "' and torihikisaki_cd = '" + w_dt.Rows[0]["torihikisaki_cd"].ToString() + "' and juchu_cd1 = '" + w_dt.Rows[0]["juchu_cd1"].ToString() + "' and juchu_cd2 = '9999999999999999' ";
                                   if (OracleUpdate(w_sql) == false)
                                   {
                                       GetUser();
                                       ErrorLogWrite(user_cd, "OracleUpdate", w_sql.Replace("'", "#"));
                                       MessageBox.Show("データベースの処理中にエラーが発生しました。zaiko_proc 04");
                                       bl = false;
                                   }
                                   //在庫履歴へ書き込み
                                   decimal w_rireki_seq16;
                                   w_rireki_seq16 = GetSeq("02");
                                   w_rireki_bl = OracleInsert("insert into tss_buhin_nyusyukko_m (buhin_syori_kbn,buhin_syori_no,seq,buhin_syori_date,buhin_cd,zaiko_kbn,torihikisaki_cd,juchu_cd1,juchu_cd2,suryou,syori_kbn,bikou,create_user_cd,create_datetime) values ('02','" + w_rireki_seq16.ToString("0000000000") + "','" + ppt_gyou.ToString() + "',sysdate,'" + in_buhin_cd + "','" + w_dt.Rows[0]["zaiko_kbn"].ToString() + "','" + w_dt.Rows[0]["torihikisaki_cd"].ToString() + "','" + w_dt.Rows[0]["juchu_cd1"].ToString() + "','" + w_dt.Rows[0]["juchu_cd2"].ToString() + "','" + w_dou_su.ToString("0.00") + "','" + in_syori_kbn + "','" + in_bikou + "の消し込み','" + user_cd + "',sysdate)");
                                   ppt_gyou++;
                               }
                               if (kakezan == 1) //すべて売上完了フラグが立っている場合
                               {
                                   //全て完了フラグが立っていたら、フリーに振替する。
                                   //まず、フリー在庫でない在庫の書き込み（０にする）
                                   GetUser();
                                   w_sql = "UPDATE tss_buhin_zaiko_m SET zaiko_su = '0',update_user_cd = '" + user_cd + "',update_datetime = sysdate where buhin_cd = '" + in_buhin_cd + "' and zaiko_kbn = '" + w_dt.Rows[0]["zaiko_kbn"].ToString() + "' and torihikisaki_cd = '" + in_torihikisaki_cd + "' and juchu_cd1 = '" + w_dt.Rows[0]["juchu_cd1"].ToString() + "' and juchu_cd2 = '9999999999999999' ";
                                   if (OracleUpdate(w_sql) == false)
                                   {
                                       GetUser();
                                       ErrorLogWrite(user_cd, "OracleUpdate", w_sql.Replace("'", "#"));
                                       MessageBox.Show("データベースの処理中にエラーが発生しました。zaiko_proc 01");
                                       bl = false;
                                   }
                                   //99ロット入出庫履歴へ書き込み
                                   decimal w_rireki_seq17;
                                   w_rireki_seq17 = GetSeq("02");
                                   w_rireki_bl = OracleInsert("insert into tss_buhin_nyusyukko_m (buhin_syori_kbn,buhin_syori_no,seq,buhin_syori_date,buhin_cd,zaiko_kbn,torihikisaki_cd,juchu_cd1,juchu_cd2,suryou,syori_kbn,bikou,create_user_cd,create_datetime) values ('02','" + w_rireki_seq17.ToString("0000000000") + "','" + ppt_gyou.ToString() + "',sysdate,'" + in_buhin_cd + "','" + w_dt.Rows[0]["zaiko_kbn"].ToString() + "','" + w_dt.Rows[0]["torihikisaki_cd"].ToString() + "','" + w_dt.Rows[0]["juchu_cd1"].ToString() + "','" + w_dt.Rows[0]["juchu_cd2"].ToString() + "','" + w_dou_su.ToString("0.00") + "','" + in_syori_kbn + "','" + in_bikou + "の消し込み','" + user_cd + "',sysdate)");
                                   ppt_gyou++;
                                   //次に残りの在庫数をフリー在庫で処理する
                                   w_dt2 = OracleSelect("select * from tss_buhin_zaiko_m where buhin_cd = '" + in_buhin_cd + "' and zaiko_kbn = '" + "01" + "' and torihikisaki_cd = '999999' and juchu_cd1 = '9999999999999999' and juchu_cd2 = '9999999999999999'");
                                   if (w_dt2.Rows.Count == 0)
                                   {
                                       GetUser();
                                       ErrorLogWrite(user_cd, "tss.zeiko_procでフリー在庫レコードが無い(CODE:03)", "zaiko_proc(部品コード" + in_buhin_cd + ",在庫区分" + in_zaiko_kbn + ",取引先コード" + in_torihikisaki_cd + ",受注コード1" + in_juchu_cd1 + ",受注コード2" + in_juchu_cd2 + ",数" + in_su + ")");
                                       bl = false;
                                       return bl;
                                   }
                                   //99ロット在庫数　＝　売上数　なら、フリー在庫の操作は不要
                                   if(w_zaiko_su != 0)
                                   {
                                       //99ロット入出庫履歴へ書き込み（余剰分）
                                       decimal w_rireki_seq18;
                                       w_rireki_seq18 = GetSeq("02");
                                       w_rireki_bl = OracleInsert("insert into tss_buhin_nyusyukko_m (buhin_syori_kbn,buhin_syori_no,seq,buhin_syori_date,buhin_cd,zaiko_kbn,torihikisaki_cd,juchu_cd1,juchu_cd2,suryou,syori_kbn,bikou,create_user_cd,create_datetime) values ('02','" + w_rireki_seq18.ToString("0000000000") + "','" + ppt_gyou.ToString() + "',sysdate,'" + in_buhin_cd + "','" + w_dt.Rows[0]["zaiko_kbn"].ToString() + "','" + w_dt.Rows[0]["torihikisaki_cd"].ToString() + "','" + w_dt.Rows[0]["juchu_cd1"].ToString() + "','" + w_dt.Rows[0]["juchu_cd2"].ToString() + "','" + w_zaiko_su.ToString("0.00") + "','" + in_syori_kbn + "','" + in_bikou + "分のロット在庫の余剰分をフリー在庫に振替6','" + user_cd + "',sysdate)");
                                       ppt_gyou++;
                                       //フリー在庫に残りの在庫数を更新
                                       decimal syorimae_f_zaiko_su;//処理前フリー在庫数
                                       decimal f_zaiko_su;//フリー在庫数
                                       decimal.TryParse(w_dt2.Rows[0]["zaiko_su"].ToString(), out syorimae_f_zaiko_su);
                                       f_zaiko_su = syorimae_f_zaiko_su + w_zaiko_su;
                                       w_sql = "UPDATE tss_buhin_zaiko_m SET zaiko_su = '" + f_zaiko_su.ToString("0.00") + "',update_user_cd = '" + user_cd + "',update_datetime = sysdate WHERE buhin_cd = '" + in_buhin_cd + "' and zaiko_kbn = '01' and torihikisaki_cd = '999999' and juchu_cd1 = '9999999999999999' and juchu_cd2 = '9999999999999999'";
                                       if (OracleUpdate(w_sql) == false)
                                       {
                                           GetUser();
                                           ErrorLogWrite(user_cd, "OracleUpdate", w_sql.Replace("'", "#"));
                                           MessageBox.Show("データベースの処理中にエラーが発生しました。zaiko_proc 02");
                                           bl = false;
                                       }
                                       //フリー在庫の入出庫履歴へ書き込み
                                       decimal w_rireki_seq19;
                                       w_rireki_seq19 = GetSeq("01");
                                       w_rireki_bl = OracleInsert("insert into tss_buhin_nyusyukko_m (buhin_syori_kbn,buhin_syori_no,seq,buhin_syori_date,buhin_cd,zaiko_kbn,torihikisaki_cd,juchu_cd1,juchu_cd2,suryou,syori_kbn,bikou,create_user_cd,create_datetime) values ('01','" + w_rireki_seq19.ToString("0000000000") + "','" + ppt_gyou.ToString() + "',sysdate,'" + in_buhin_cd + "','" + "01" + "','999999','" + "9999999999999999" + "','" + "9999999999999999" + "','" + w_zaiko_su.ToString("0.00") + "','" + in_syori_kbn + "','" + in_bikou + "分のロット在庫の余剰分をフリー在庫に振替8','" + user_cd + "',sysdate)");
                                       ppt_gyou++;
                                   }
                               }
                           }
                           //99ロット在庫がマイナスになる場合、フリー在庫で処理する
                           else 
                           {
                               //全て完了フラグが立っていたら、フリーに振替する。
                               //まず、99ロット在庫の書き込み（０にする）
                               GetUser();
                               w_sql = "UPDATE tss_buhin_zaiko_m SET zaiko_su = '0',update_user_cd = '" + user_cd + "',update_datetime = sysdate where buhin_cd = '" + in_buhin_cd + "' and zaiko_kbn = '" + w_dt.Rows[0]["zaiko_kbn"].ToString() + "' and torihikisaki_cd = '" + in_torihikisaki_cd + "' and juchu_cd1 = '" + w_dt.Rows[0]["juchu_cd1"].ToString() + "' and juchu_cd2 = '9999999999999999' ";
                               if (OracleUpdate(w_sql) == false)
                               {
                                   GetUser();
                                   ErrorLogWrite(user_cd, "OracleUpdate", w_sql.Replace("'", "#"));
                                   MessageBox.Show("データベースの処理中にエラーが発生しました。zaiko_proc 01");
                                   bl = false;
                               }
                               //99ロット在庫の入出庫履歴へ書き込み
                               decimal w_rireki_seq20;
                               if(w_rireki_kbn == "01")
                               {
                                   w_rireki_seq20 = GetSeq("01");
                               }
                               else
                               {
                                   w_rireki_seq20 = GetSeq("02");
                               }
                               w_rireki_bl = OracleInsert("insert into tss_buhin_nyusyukko_m (buhin_syori_kbn,buhin_syori_no,seq,buhin_syori_date,buhin_cd,zaiko_kbn,torihikisaki_cd,juchu_cd1,juchu_cd2,suryou,syori_kbn,bikou,create_user_cd,create_datetime) values ('" + w_rireki_kbn + "','" + w_rireki_seq20.ToString("0000000000") + "','" + ppt_gyou.ToString() + "',sysdate,'" + in_buhin_cd + "','" + w_dt.Rows[0]["zaiko_kbn"].ToString() + "','" + w_dt.Rows[0]["torihikisaki_cd"].ToString() + "','" + w_dt.Rows[0]["juchu_cd1"].ToString() + "','" + w_dt.Rows[0]["juchu_cd2"].ToString() + "','" + lot2_zaiko_su.ToString("0.00") + "','" + in_syori_kbn + "','" + in_bikou + "の消し込み','" + user_cd + "',sysdate)");
                               ppt_gyou++;
                               //次に残りの在庫数をフリー在庫で処理する
                               w_dt2 = OracleSelect("select * from tss_buhin_zaiko_m where buhin_cd = '" + in_buhin_cd + "' and zaiko_kbn = '" + "01" + "' and torihikisaki_cd = '999999' and juchu_cd1 = '9999999999999999' and juchu_cd2 = '9999999999999999'");
                               if (w_dt2.Rows.Count == 0)
                               {
                                   GetUser();
                                   ErrorLogWrite(user_cd, "tss.zeiko_procでフリー在庫レコードが無い(CODE:03)", "zaiko_proc(部品コード" + in_buhin_cd + ",在庫区分" + in_zaiko_kbn + ",取引先コード" + in_torihikisaki_cd + ",受注コード1" + in_juchu_cd1 + ",受注コード2" + in_juchu_cd2 + ",数" + in_su + ")");
                                   bl = false;
                                   return bl;
                               }
                               //フリー在庫に残りの在庫数を更新
                               decimal syorimae_f_zaiko_su;//処理前フリー在庫数
                               decimal f_zaiko_su;//フリー在庫数
                               decimal.TryParse(w_dt2.Rows[0]["zaiko_su"].ToString(), out syorimae_f_zaiko_su);
                               f_zaiko_su = syorimae_f_zaiko_su + w_zaiko_su;
                               w_sql = "UPDATE tss_buhin_zaiko_m SET zaiko_su = '" + f_zaiko_su.ToString("0.00") + "',update_user_cd = '" + user_cd + "',update_datetime = sysdate WHERE buhin_cd = '" + in_buhin_cd + "' and zaiko_kbn = '01' and torihikisaki_cd = '999999' and juchu_cd1 = '9999999999999999' and juchu_cd2 = '9999999999999999'";
                               if (OracleUpdate(w_sql) == false)
                               {
                                   GetUser();
                                   ErrorLogWrite(user_cd, "OracleUpdate", w_sql.Replace("'", "#"));
                                   MessageBox.Show("データベースの処理中にエラーが発生しました。zaiko_proc 02");
                                   bl = false;
                               }
                               w_zaiko_su = w_zaiko_su * -1;
                               //在庫履歴へ書き込み
                               decimal w_rireki_seq21;
                               if (w_rireki_kbn == "01")
                               {
                                   w_rireki_seq21 = GetSeq("01");
                               }
                               else
                               {
                                   w_rireki_seq21 = GetSeq("02");
                               }
                               w_rireki_bl = OracleInsert("insert into tss_buhin_nyusyukko_m (buhin_syori_kbn,buhin_syori_no,seq,buhin_syori_date,buhin_cd,zaiko_kbn,torihikisaki_cd,juchu_cd1,juchu_cd2,suryou,syori_kbn,bikou,create_user_cd,create_datetime) values ('" + w_rireki_kbn + "','" + w_rireki_seq21.ToString("0000000000") + "','" + ppt_gyou.ToString() + "',sysdate,'" + in_buhin_cd + "','" + "01" + "','999999','" + "9999999999999999" + "','" + "9999999999999999" + "','" + w_zaiko_su.ToString() + "','" + in_syori_kbn + "','" + in_bikou + "分のロット在庫不足分の消し込み','" + user_cd + "',sysdate)");
                               ppt_gyou++;
                           }
                        }
       　///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        //受注コード1+　受注ＣＤ2（9999999999999999）レコードがない場合
                        if (w_dt.Rows.Count == 0)
                        {
                            //受注ＣＤ1+受注ＣＤ2（9999999999999999）在庫もなかった場合は、フリー在庫を使用する
                            w_dt = OracleSelect("select * from tss_buhin_zaiko_m where buhin_cd = '" + in_buhin_cd + "' and zaiko_kbn = '" + "01" + "' and torihikisaki_cd = '999999' and juchu_cd1 = '9999999999999999' and juchu_cd2 = '9999999999999999'");
                            if (w_dt.Rows.Count == 0)
                            {
                                GetUser();
                                ErrorLogWrite(user_cd, "tss.zeiko_procでフリー在庫レコードが無い(CODE:01)", "zaiko_proc(部品コード" + in_buhin_cd + ",在庫区分" + in_zaiko_kbn + ",取引先コード" + in_torihikisaki_cd + ",受注コード1" + in_juchu_cd1 + ",受注コード2" + in_juchu_cd2 + ",数" + in_su + ")");
                                bl = false;
                                return bl;
                            }
                            decimal.TryParse(w_dt.Rows[0]["zaiko_su"].ToString(), out w_zaiko_su);
                            w_zaiko_su = w_zaiko_su - in_su;
                            w_sql = "UPDATE tss_buhin_zaiko_m SET zaiko_su = '" + w_zaiko_su.ToString("0.00") + "',update_user_cd = '" + user_cd + "',update_datetime = sysdate WHERE buhin_cd = '" + in_buhin_cd + "' and zaiko_kbn = '01' and torihikisaki_cd = '999999' and juchu_cd1 = '9999999999999999' and juchu_cd2 = '9999999999999999'";
                            if (OracleUpdate(w_sql) == false)
                            {
                                GetUser();
                                ErrorLogWrite(user_cd, "OracleUpdate", w_sql.Replace("'", "#"));
                                MessageBox.Show("データベースの処理中にエラーが発生しました。zaiko_proc 02");
                                bl = false;
                            }
                            //入出庫履歴へ書き込み
                            decimal w_rireki_seq22;
                            w_rireki_seq22 = GetSeq("02");
                            w_rireki_bl = OracleInsert("insert into tss_buhin_nyusyukko_m (buhin_syori_kbn,buhin_syori_no,seq,buhin_syori_date,buhin_cd,zaiko_kbn,torihikisaki_cd,juchu_cd1,juchu_cd2,suryou,syori_kbn,bikou,create_user_cd,create_datetime) values ('02','" + w_rireki_seq22.ToString("0000000000") + "','" + ppt_gyou.ToString() + "',sysdate,'" + in_buhin_cd + "','" + "01" + "','999999','" + "9999999999999999" + "','" + "9999999999999999" + "','" + w_dou_su.ToString() + "','" + in_syori_kbn + "','" + in_bikou + "分のロット在庫不足分の消し込み','" + user_cd + "',sysdate)");
                            ppt_gyou++;
                        }
                    }      
                }
                return bl;
            } 
            return bl;
        }
        #endregion

        #region hasu_keisan
        /// <summary>
        /// 取引先コードと数値を受け取り端数処理して返す</summary>
        /// <param name="in_cd">
        /// string 取得する取引先コード</param>
        /// <param name="in_decimal">
        /// decimal 端数処理する数値</param>
        /// <returns>
        /// decimal 端数処理後の数値
        /// エラー等は-9999999999</returns>
        public decimal hasu_keisan(string in_cd, decimal in_decimal)
        {
            decimal out_decimal = -9999999999;  //戻り値用
            DataTable w_dt = new DataTable();
            w_dt = OracleSelect("select * from tss_torihikisaki_m where torihikisaki_cd = '" + in_cd + "'");
            if (w_dt.Rows.Count == 0)
            {
                out_decimal = -9999999999;
            }
            else
            {
                //端数処理単位
                int w_hasu_syori_tani;
                switch (w_dt.Rows[0]["hasu_syori_tani"].ToString())
                {
                    case "0":
                        //円未満
                        w_hasu_syori_tani = 1;
                        break;
                    case "1":
                        //10円未満
                        w_hasu_syori_tani = 10;
                        break;
                    case "2":
                        //100円未満
                        w_hasu_syori_tani = 100;
                        break;
                    default:
                        //存在しない区分
                        w_hasu_syori_tani = -1;
                        break;
                }
                //端数処理単位に異常があったら抜ける
                if (w_hasu_syori_tani == -1)
                {
                    out_decimal = -9999999999;
                    return out_decimal;
                }
                //端数区分
                switch (w_dt.Rows[0]["hasu_kbn"].ToString())
                {
                    case "0":
                        //切り捨て
                        out_decimal = Math.Truncate(in_decimal / w_hasu_syori_tani) * w_hasu_syori_tani;
                        break;
                    case "1":
                        //四捨五入
                        out_decimal = Math.Round(in_decimal / w_hasu_syori_tani, MidpointRounding.AwayFromZero) * w_hasu_syori_tani;
                        break;
                    case "2":
                        //切り上げ
                        out_decimal = Math.Ceiling(in_decimal / w_hasu_syori_tani) * w_hasu_syori_tani;
                        break;
                    default:
                        //存在しない区分
                        out_decimal = -9999999999;
                        break;
                }
            }
            return out_decimal;
        }
        #endregion

        #region siharai_no_select_dt
        //祖払い番号選択画面（DataTable版）の呼び出し
        public string siharai_no_select_dt(string in_torihikisaki_cd, DataTable in_dt_siharai_no)
        {
            //マウスのX座標を取得する
            int x = System.Windows.Forms.Cursor.Position.X;
            //マウスのY座標を取得する
            int y = System.Windows.Forms.Cursor.Position.Y;

            //string out_torihikisaki_cd = "";   //戻り値用
            string out_siharai_no = "";   //戻り値用
            frm_siharai_no_select sns = new frm_siharai_no_select();

            //フォームをマウスの位置に表示する
            sns.Left = x;
            sns.Top = y;
            sns.StartPosition = FormStartPosition.Manual;

            //子画面のプロパティに値をセットする
            sns.ppt_str_torihikisaki_cd = in_torihikisaki_cd;
            sns.ppt_dt_siharai_no = in_dt_siharai_no;
            sns.ShowDialog();
            //子画面から値を取得する
            out_siharai_no = sns.ppt_str_siharai_no;
            sns.Dispose();
            return out_siharai_no;
        }
        #endregion

        #region get_seihin_kousei_mattan
        /// <summary>
        /// 製品コードと製品構成番号を受け取り製品構成の末端部品を返す</summary>
        /// <param name="string in_cd">
        /// 製品コード</param>
        /// <param name="string in_no">
        /// 製品構成番号</param>
        /// <returns>
        /// DataTable 末端部品（buhin_cd,siyou_su）
        /// エラー等、取得できない場合はnull</returns>
        public DataTable get_seihin_kousei_mattan(string in_cd, string in_no)
        {
            DataTable out_dt = new DataTable();  //戻り値用
            out_dt.Columns.Add("buhin_cd");
            out_dt.Columns.Add("siyou_su");
            DataRow out_dr;

            DataTable w_dt2 = new DataTable();  //互換部品と親部品を除いたデータ用
            w_dt2.Columns.Add("buhin_cd");
            w_dt2.Columns.Add("siyou_su");
            DataRow dr2;

            decimal w_dou_siyou_su;  //使用数計算用
            decimal w_dou_siyou_su2; //使用数計算用

            //第一段階
            //対象となる製品構成（製品コード＋製品構成番号）を抽出
            DataTable w_dt1 = new DataTable();
            w_dt1 = OracleSelect("select * from tss_seihin_kousei_m where seihin_cd = '" + in_cd + "' and seihin_kousei_no = '" + in_no + "' order by buhin_cd asc");
            if (w_dt1.Rows.Count >= 1)
            {
                //第二段階
                //親レコードと互換部品レコードを除き、末端部品のみを抽出し、w_dt2を作成する
                int w_oyako_flg = 0;    //0:末端部品（加減算対象） 1:親部品（加減算しない）

                foreach (DataRow dr1 in w_dt1.Rows)
                {
                    w_oyako_flg = 0;
                    //互換部品コードに値が入っているレコードは除外
                    if (dr1["gokan_buhin_cd"].ToString() != "" && dr1["gokan_buhin_cd"].ToString() != null)
                    {
                        w_oyako_flg = 1;
                    }
                    else
                    {
                        //自分を親部品として登録されているレコードは除外する
                        for (int i = 0; i < w_dt1.Rows.Count; i++)
                        {
                            if (dr1["buhin_cd"].ToString() == w_dt1.Rows[i]["oya_buhin_cd"].ToString())
                            {
                                w_oyako_flg = 1;
                                break;
                            }
                        }
                    }
                    //末端部品をw_dt2に追加
                    if (w_oyako_flg == 0)
                    {
                        dr2 = w_dt2.NewRow();
                        dr2["buhin_cd"] = dr1["buhin_cd"].ToString();
                        dr2["siyou_su"] = dr1["siyou_su"].ToString();
                        w_dt2.Rows.Add(dr2);
                    }
                }
                //末端部品(w_dt2)を部品コードで集約し、部品毎の使用数の集計をしたデータをout_dtに作成する
                int w_juufuku_flg;  //部品コードの重複フラグ 0:重複無し 1:重複あり
                int w_out_row;      //重複した部品コードのrowインデックス
                foreach (DataRow dr3 in w_dt2.Rows)
                {
                    //自分を親部品として登録されているレコードは除外する
                    w_juufuku_flg = 0;
                    w_out_row = -1;
                    for (int i = 0; i < out_dt.Rows.Count; i++)
                    {
                        if (dr3["buhin_cd"].ToString() == out_dt.Rows[i]["buhin_cd"].ToString())
                        {
                            w_juufuku_flg = 1;
                            w_out_row = i;
                            break;
                        }
                    }
                    //レコード新規・追加判断
                    if (decimal.TryParse(dr3["siyou_su"].ToString(), out w_dou_siyou_su2) == false)
                    {
                        w_dou_siyou_su2 = 0;
                    }
                    if (w_juufuku_flg == 0)
                    {
                        //重複無しの場合、新規に作成
                        out_dr = out_dt.NewRow();
                        out_dr["buhin_cd"] = dr3["buhin_cd"].ToString();
                        out_dr["siyou_su"] = w_dou_siyou_su2.ToString("0.00");
                        out_dt.Rows.Add(out_dr);
                    }
                    else
                    {
                        //重複ありの場合は、既存レコードに使用数を足す
                        decimal.TryParse(out_dt.Rows[w_out_row]["siyou_su"].ToString(), out w_dou_siyou_su);
                        out_dt.Rows[w_out_row]["siyou_su"] = (w_dou_siyou_su + w_dou_siyou_su2).ToString("0.00");
                    }
                }
            }
            return out_dt;
        }
        #endregion

        #region get_juchu_uriage_su
        /// <summary>
        /// 受注番号を受け取り売上数を返す</summary>
        /// <param name="string in_torihikisaki_cd">
        /// 取引先コード</param>
        /// <param name="string in_juchu_cd1">
        /// 受注コード1</param>
        /// <param name="string in_juchu_cd2">
        /// 受注コード2</param>
        /// <returns>
        /// string 売上数
        /// エラー等、取得できない場合は""</returns>
        public string get_juchu_uriage_su(string in_torihikisaki_cd, string in_juchu_cd1, string in_juchu_cd2, string in_uriage_su)
        {
            string out_str = null; //戻り値用
            decimal w_dou1;
            decimal w_dou2;

            DataTable w_dt_m = new DataTable();
            w_dt_m = OracleSelect("select * from tss_juchu_m where torihikisaki_cd = '" + in_torihikisaki_cd + "' and juchu_cd1 = '" + in_juchu_cd1 + "' and juchu_cd2 = '" + in_juchu_cd2 + "'");
            if (w_dt_m.Rows.Count == 0)
            {
                out_str = "";
            }
            else
            {
                decimal.TryParse(in_uriage_su, out w_dou1);  //今回の売上数
                decimal.TryParse(w_dt_m.Rows[0]["uriage_su"].ToString(), out w_dou2);  //受注マスタの売上合計数

                out_str = (w_dou1 + w_dou2).ToString();
            }
            return out_str;
        }
        #endregion

        #region get_juchu_juchu_su
        /// <summary>
        /// 受注番号を受け取り受注数を返す</summary>
        /// <param name="string in_torihikisaki_cd">
        /// 取引先コード</param>
        /// <param name="string in_juchu_cd1">
        /// 受注コード1</param>
        /// <param name="string in_juchu_cd2">
        /// 受注コード2</param>
        /// <returns>
        /// string 受注数
        /// エラー等、取得できない場合は""</returns>
        public string get_juchu_juchu_su(string in_torihikisaki_cd, string in_juchu_cd1, string in_juchu_cd2)
        {
            string out_str = null; //戻り値用

            DataTable w_dt_m = new DataTable();
            w_dt_m = OracleSelect("select * from tss_juchu_m where torihikisaki_cd = '" + in_torihikisaki_cd + "' and juchu_cd1 = '" + in_juchu_cd1 + "' and juchu_cd2 = '" + in_juchu_cd2 + "'");
            if (w_dt_m.Rows.Count == 0)
            {
                out_str = "";
            }
            else
            {
                out_str = w_dt_m.Rows[0]["juchu_su"].ToString();
            }
            return out_str;
        }
        #endregion

        #region Check_String_Escape
        /// <summary>
        /// 文字列に使用不可の文字（エスケープシーケンス）等が無いかチェックする</summary>
        /// <param name="string in_str">チェックする文字列</param>
        /// <returns>bool 正常:true 使用不可あり:false</returns>
        public bool Check_String_Escape(string in_str)
        {
            bool out_bl = true; //戻り値用

            if (in_str.IndexOf("'") >= 0 || in_str.IndexOf("%") >= 0 || in_str.IndexOf("\\") >= 0 || in_str.IndexOf("\"") >= 0)
            {
                MessageBox.Show("文字列に「 ' % \\ \" 」は使用できません。");
                out_bl = false;
            }
            else
            {
                out_bl = true;
            }
            return out_bl;
        }
        #endregion

        #region get_syouhizeiritu
        /// <summary>
        /// 日付を受け取り、消費税率を返す</summary>
        /// <param name="datetime in_datetime">消費税率算出日</param>
        /// <returns>decimal 消費税率 エラー時は-1</returns>
        public decimal get_syouhizeiritu(DateTime in_datetime)
        {
            decimal out_decimal; //戻り値用

            DataTable w_dt = new DataTable();
            w_dt = OracleSelect("select * from tss_syouhizei_m where TO_DATE('" + in_datetime.ToShortDateString() + "','YYYY/MM/DD') >= kaitei_date and TO_DATE('" + in_datetime.ToShortDateString() + "','YYYY/MM/DD') <= syuryou_date");
            if (w_dt.Rows.Count == 0)
            {
                out_decimal = -1;
            }
            else
            {
                out_decimal = try_string_to_decimal(w_dt.Rows[0]["zeiritu"].ToString());
                if (out_decimal == -999999999)
                {
                    out_decimal = -1;
                }
            }
            return out_decimal;
        }
        #endregion

        #region urikake_kesikomi
        /// <summary>取引先マスタの未処理入金額を売掛マスタに消し込みます</summary>
        /// <param name="string in_cd">取引先コード</param>
        public bool urikake_kesikomi(string in_cd)
        {
            bool out_bl = true; //戻り値用

            DataTable w_dt = new DataTable();           //取引先マスタの読み込み用
            DataTable w_dt_urikake = new DataTable();   //売掛マスタ用

            decimal w_dou_misyori_nyukingaku;    //処理する未処理金額
            decimal w_dou_uriage_kingaku;        //計算用売上金額
            decimal w_dou_syouhizeigaku;         //計算用消費税額
            decimal w_dou_nyukingaku;            //計算用入金額
            decimal w_dou_nyukin_kanou_gaku;     //入金可能額（売上金額＋消費税－入金額）
            string w_str_nyukin_kanryou_flg;    //入金完了フラグ
            int w_int_sign;                     //未処理入金額のプラスマイナスサイン 1:プラス値 -1:マイナス値

            //取引先マスタの未処理金額を取得
            w_dt = OracleSelect("select * from tss_torihikisaki_m where torihikisaki_cd = '" + in_cd + "'");
            if (w_dt.Rows.Count == 0)
            {
                ErrorLogWrite(user_cd, "tss.urikake_kesikomi", "in_cd=" + in_cd + "のレコードが無い");
                out_bl = false;
            }
            else
            {
                w_dou_misyori_nyukingaku = try_string_to_decimal(w_dt.Rows[0]["misyori_nyukingaku"].ToString());
                if (w_dou_misyori_nyukingaku == 0 || w_dou_misyori_nyukingaku == -999999999)
                {
                    //未処理入金額が０（処理する金額が無い）
                    out_bl = true;
                }
                else
                {
                    //未処理入金額のプラス値・マイナス値の判定と消し込む売掛マスタの抽出
                    if (w_dou_misyori_nyukingaku > 0)
                    {
                        w_int_sign = 1;
                        //プラスの場合は売掛マスタの古い方から消していく
                        w_dt_urikake = OracleSelect("select * from tss_urikake_m where torihikisaki_cd = '" + in_cd + "'and nyukin_kanryou_flg <> '1' ORDER BY uriage_simebi asc");
                    }
                    else
                    {
                        w_int_sign = -1;
                        //マイナスの場合は売掛マスタの新しい方から消していく
                        w_dt_urikake = OracleSelect("select * from tss_urikake_m where torihikisaki_cd = '" + in_cd + "' ORDER BY uriage_simebi desc");
                    }
                    //消込処理（未処理金額が0以下（0以上）になる、または消し込む売掛マスタが無くなるまで繰り返す
                    foreach (DataRow dr in w_dt_urikake.Rows)
                    {
                        //売上額の取得
                        w_dou_uriage_kingaku = try_string_to_decimal(dr["uriage_kingaku"].ToString());
                        if (w_dou_uriage_kingaku == -999999999)
                        {
                            w_dou_uriage_kingaku = 0;
                        }
                        //消費税の取得
                        w_dou_syouhizeigaku = try_string_to_decimal(dr["syouhizeigaku"].ToString());
                        if (w_dou_syouhizeigaku == -999999999)
                        {
                            w_dou_syouhizeigaku = 0;
                        }
                        //入金額の取得
                        w_dou_nyukingaku = try_string_to_decimal(dr["nyukingaku"].ToString());
                        if (w_dou_nyukingaku == -999999999)
                        {
                            w_dou_nyukingaku = 0;
                        }
                        //処理可能金額の算出
                        if (w_int_sign == 1)
                        {
                            //未処理入金額がプラス値の場合は、売上と入金額の差まで処理可能
                            w_dou_nyukin_kanou_gaku = w_dou_uriage_kingaku + w_dou_syouhizeigaku - w_dou_nyukingaku;
                        }
                        else
                        {
                            //未処理入金額がマイナス値の場合は入金額の値まで処理可能
                            w_dou_nyukin_kanou_gaku = w_dou_nyukingaku;
                        }
                        //入金額の計算
                        //処理可能金額が０の場合は、っこのレコードを飛ばす
                        if (w_dou_nyukin_kanou_gaku != 0)
                        {
                            if (w_dou_nyukin_kanou_gaku <= w_dou_misyori_nyukingaku * w_int_sign)
                            {
                                //未処理入金額に余裕がある場合
                                w_dou_nyukingaku = w_dou_nyukingaku + w_dou_nyukin_kanou_gaku * w_int_sign;                 //入金額を売上額までにして
                                w_dou_misyori_nyukingaku = w_dou_misyori_nyukingaku - w_dou_nyukin_kanou_gaku * w_int_sign; //入金した金額を未処理額から減らす
                                if (w_int_sign == 1)
                                {
                                    w_str_nyukin_kanryou_flg = "1"; //入金完了フラグを立てる
                                }
                                else
                                {
                                    w_str_nyukin_kanryou_flg = "0"; //入金完了フラグを立てる
                                }
                            }
                            else
                            {
                                //未処理額を使い切る場合
                                w_dou_nyukingaku = w_dou_nyukingaku + w_dou_misyori_nyukingaku;                 //入金額に残りの未処理額を加えて
                                w_dou_misyori_nyukingaku = 0;                                                   //未処理入金額を０にする
                                w_str_nyukin_kanryou_flg = "0";                                                 //入金完了フラグは０
                            }
                            //処理したレコードの書き込み
                            OracleUpdate("UPDATE tss_urikake_m SET nyukingaku ='" + w_dou_nyukingaku.ToString() + "',nyukin_kanryou_flg = '" + w_str_nyukin_kanryou_flg + "',UPDATE_USER_CD = '" + user_cd + "',UPDATE_DATETIME = SYSDATE WHERE torihikisaki_cd = '" + dr["torihikisaki_cd"].ToString() + "'and uriage_simebi = to_date('" + dr["uriage_simebi"].ToString() + "','YYYY/MM/DD HH24:MI:SS')");

                        }
                        //未処理入金額が０になったら、ループを抜ける
                        if (w_dou_misyori_nyukingaku == 0)
                        {
                            break;
                        }
                    }
                    //取引先マスタに未処理入金額を書き込む
                    OracleUpdate("UPDATE TSS_torihikisaki_m SET misyori_nyukingaku = '" + w_dou_misyori_nyukingaku.ToString() + "',UPDATE_USER_CD = '" + user_cd + "',UPDATE_DATETIME = SYSDATE WHERE torihikisaki_cd = '" + in_cd + "'");
                }
            }
            return out_bl;
        }
        #endregion

        #region StringMidByte
        /// -----------------------------------------------------------------------------------------
        /// <summary>
        ///     文字列の指定されたバイト位置から、指定されたバイト数分の文字列を返します。</summary>
        /// <param name="in_str">
        ///     取り出す元になる文字列</param>
        /// <param name="in_start">
        ///     取り出しを開始する位置（スタートは０）</param>
        /// <param name="in_size">
        ///     取り出すバイト数</param>
        /// <returns>
        ///     指定されたバイト位置から指定されたバイト数分の文字列。エラー時はnull</returns>
        /// -----------------------------------------------------------------------------------------
        public string StringMidByte(string in_str, int in_start, int in_size)
        {
            string out_str = "";
            try
            {
                System.Text.Encoding enc_str = System.Text.Encoding.GetEncoding("Shift_JIS");
                byte[] btBytes = enc_str.GetBytes(in_str);
                out_str = enc_str.GetString(btBytes, in_start, in_size);
            }
            catch
            {
                out_str = null;
            }
            return out_str;
        }
        #endregion

        #region User_Kengen_Check
        /// -----------------------------------------------------------------------------------------
        /// <summary>
        ///     ユーザー権限をチェックし、条件を満たしているかどうか判定します。</summary>
        /// <param name="in_kengen_kbn">
        ///     必要な権限の区分（1:受注、2:売上、3:仕入、4:部品・製品、5:社内情報、6:マスタ、7:生産、8:未使用、9:未使用）</param>
        /// <param name="in_kengen_level">
        ///     必要な権限のレベル（0:権限無し～9:権限無制限）</param>
        /// <returns>
        ///     true:条件を満たしている、false:条件を満たしていない</returns>
        /// -----------------------------------------------------------------------------------------
        public bool User_Kengen_Check(int in_kengen_kbn, int in_kengen_level)
        {
            bool out_bl = false;
            //ユーザーの権限レベルを取得
            GetUser();
            int w_kengen_level;
            switch(in_kengen_kbn)
            {
                case 1:
                    int.TryParse(fld_kengen1, out w_kengen_level);
                    break;
                case 2:
                    int.TryParse(fld_kengen2, out w_kengen_level);
                    break;
                case 3:
                    int.TryParse(fld_kengen3, out w_kengen_level);
                    break;
                case 4:
                    int.TryParse(fld_kengen4, out w_kengen_level);
                    break;
                case 5:
                    int.TryParse(fld_kengen5, out w_kengen_level);
                    break;
                case 6:
                    int.TryParse(fld_kengen6, out w_kengen_level);
                    break;
                case 7:
                    int.TryParse(fld_kengen7, out w_kengen_level);
                    break;
                case 8:
                    int.TryParse(fld_kengen8, out w_kengen_level);
                    break;
                case 9:
                    int.TryParse(fld_kengen9, out w_kengen_level);
                    break;
                default:
                    w_kengen_level = 0;
                    out_bl = false;
                    break;
            }
            //ユーザーの権限が０ではなく、且つ指定レベル以上であればtrueを返す
            if(in_kengen_level <= w_kengen_level && w_kengen_level != 0)
            {
                out_bl = true;
            }
            else
            {
                out_bl = false;
            }
            return out_bl;
        }
        #endregion

        #region System_Version_Check
        /// -----------------------------------------------------------------------------------------
        /// <summary>
        /// tss_system_mの'0101'レコードのシステムバージョンと、tss_system_libraryのシステムバージョンをチェックし、
        /// 違ったらfalseを返す。</summary>
        /// -----------------------------------------------------------------------------------------
        public bool System_Version_Check()
        {
            bool bl;    //戻り値用
            bl = true;
            //システムのバージョン確認
            DataTable dt_system = new DataTable();
            dt_system = OracleSelect("select * from tss_system_m where system_cd = '0101'");
            //システムレコードのチェック
            if (dt_system == null || dt_system.Rows.Count != 1)
            {
                bl = false;
                return bl;
            }
            //バージョンチェック
            if (dt_system.Rows[0]["system_version"].ToString() != program_version)
            {
                bl = false;
                return bl;
            }
            return bl;
        }
        #endregion

        #region seisan_schedule_make
        /// -----------------------------------------------------------------------------------------
        /// <summary>
        /// 生産スケジュール作成
        /// 受注コードを受け取り、受け取った受注コードの生産スケジュールを作成する
        /// 既に作成済みの場合は、編集済みフラグが立っていなければ再作成し、立っていた場合はメッセージを表示し、ユーザーに判断を委ねる。
        /// </summary>
        /// -----------------------------------------------------------------------------------------
        public bool Seisan_Schedule_Make(string in_torihikisaki_cd,string in_juchu_cd1,string in_juchu_cd2)
        {
            DataTable w_dt_juchu = new DataTable();
            DataTable w_dt_seihin = new DataTable();
            DataTable w_dt_seisan_koutei = new DataTable();
            DataTable w_dt_nouhin_schedule = new DataTable();
            DataTable w_dt_seisan_schedule = new DataTable();
            string w_msg;

            //受注マスタ取得
            w_dt_juchu = OracleSelect("select * from tss_juchu_m where torihikisaki_cd = '" + in_torihikisaki_cd + "' and juchu_cd1 = '" + in_juchu_cd1 + "' and juchu_cd2 = '" + in_juchu_cd2 + "'");
            //製品マスタ取得
            w_dt_seihin = OracleSelect("select * from tss_seihin_m where seihin_cd = '" + w_dt_juchu.Rows[0]["seihin_cd"].ToString() + "'");
            //生産工程マスタが登録されているかチェック（されていなければユーザーにメッセージ送信して終了する）
            w_dt_seisan_koutei = OracleSelect("select * from tss_seisan_koutei_m where seihin_cd = '" + w_dt_juchu.Rows[0]["seihin_cd"].ToString() + "' order by seq_no");
            if (w_dt_seisan_koutei.Rows.Count <= 0)
            {
                MessageLogWrite(user_cd, user_cd, "生産工程未登録の受注を登録", "受注コード " + in_torihikisaki_cd + "-" + in_juchu_cd1 + "-" + in_juchu_cd2 + " 製品コード " + w_dt_juchu.Rows[0]["seihin_cd"].ToString() + " の生産工程が未登録のため生産スケジュールを自動作成できませんでした。\n生産工程を登録後、生産スケジュールを作成してください。");
                return false;
            }
            //編集済みの生産スケジュールがあるかチェック
            w_dt_seisan_schedule = OracleSelect("select * from tss_seisan_schedule_f where torihikisaki_cd = '" + in_torihikisaki_cd + "' and juchu_cd1 = '" + in_juchu_cd1 + "' and juchu_cd2 = '" + in_juchu_cd2 + "' and hensyu_flg = '1'");
            if(w_dt_seisan_schedule.Rows.Count >=1)
            {
                //編集済みの生産スケジュール有り
                DialogResult result = MessageBox.Show("既に編集されている生産スケジュールがあります。\n再作成してもよろしいですか？\n「はい」=再作成＋メッセージ送信\n「いいえ」=作成しない＋メッセージの送信\n「キャンセル」=生産スケジュールは何もしない", "確認", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.Yes)
                {
                    //「はい」
                    //■既にあるデータ削除
                    if(ssm_delete(in_torihikisaki_cd, in_juchu_cd1, in_juchu_cd2) == false)
                    {
                        return false;
                    }
                    //■生産スケジュール作成
                    if (ssm_sakusei(in_torihikisaki_cd, in_juchu_cd1, in_juchu_cd2) == false)
                    {
                        return false;
                    }
                    //■メッセージ送信
                    w_msg = "受注コード" + in_torihikisaki_cd + "-" + in_juchu_cd1 + "-" + in_juchu_cd2 + " が変更され、編集済みの生産スケジュールが更新されました。確認し、再編集してください。";
                    if (ssm_message(in_torihikisaki_cd, in_juchu_cd1, in_juchu_cd2,w_msg) == false)
                    {
                        return false;
                    }
                }
                else if (result == DialogResult.No)
                {
                    //「いいえ」
                    //生産スケジュールは何も行わない
                    //■メッセージ送信
                    w_msg = "受注コード" + in_torihikisaki_cd + "-" + in_juchu_cd1 + "-" + in_juchu_cd2 + " が変更されましたので、生産スケジュールを確認・再編集してください。";
                    if (ssm_message(in_torihikisaki_cd, in_juchu_cd1, in_juchu_cd2, w_msg) == false)
                    {
                        return false;
                    }
                }
                else if (result == DialogResult.Cancel)
                {
                    //「キャンセル」
                    //■生産スケジュールは何も行わない
                    //メッセージは送信しないが、処理者には送信する
                    w_msg = "受注コード" + in_torihikisaki_cd + "-" + in_juchu_cd1 + "-" + in_juchu_cd2 + " 編集済みの生産スケジュールが有る受注の変更を行いましたがユーザーにメッセージを送信しませんでした。";
                    MessageLogWrite(user_cd, user_cd, "生産スケジュール編集後の受注情報の変更", w_msg);
                }
            }
            else
            {
                //編集済みの生産スケジュール無し
                //■未編集のレコードがあるかもしれないので削除
                if (ssm_delete(in_torihikisaki_cd, in_juchu_cd1, in_juchu_cd2) == false)
                {
                    return false;
                }
                //■生産スケジュール作成
                if (ssm_sakusei(in_torihikisaki_cd, in_juchu_cd1, in_juchu_cd2) == false)
                {
                    return false;
                }
            }
            return true;
        }

        private bool ssm_sakusei(string in_torihikisaki_cd, string in_juchu_cd1, string in_juchu_cd2)
        {
            //生産スケジュール作成メソッド
            DataTable w_dt_juchu = new DataTable();
            DataTable w_dt_seihin = new DataTable();
            DataTable w_dt_seisan_koutei = new DataTable();
            DataTable w_dt_nouhin_schedule = new DataTable();
            DataTable w_dt_seisan_schedule = new DataTable();
            DataTable w_dt_seisan_koutei_line = new DataTable();

            //受注マスタ取得
            w_dt_juchu = OracleSelect("select * from tss_juchu_m where torihikisaki_cd = '" + in_torihikisaki_cd + "' and juchu_cd1 = '" + in_juchu_cd1 + "' and juchu_cd2 = '" + in_juchu_cd2 + "'");
            //製品マスタ取得
            w_dt_seihin = OracleSelect("select * from tss_seihin_m where seihin_cd = '" + w_dt_juchu.Rows[0]["seihin_cd"].ToString() + "'");
            //生産工程マスタ取得
            w_dt_seisan_koutei = OracleSelect("select * from tss_seisan_koutei_m where seihin_cd = '" + w_dt_juchu.Rows[0]["seihin_cd"].ToString() + "' order by seq_no");
            if(w_dt_seisan_koutei.Rows.Count <=0)
            {
                MessageBox.Show("生産工程マスタが未登録、または異常があります。\n確認し、再度生産スケジュールを作成してください。");
                return false;
            }
            //納品スケジュールマスタの取得
            //w_dt_nouhin_schedule = OracleSelect("select * from tss_nouhin_schedule_m where torihikisaki_cd = '" + in_torihikisaki_cd + "' and juchu_cd1 = '" + in_juchu_cd1 + "' and juchu_cd2 = '" + in_juchu_cd2 + "' order by nouhin_yotei_date,seq asc");
            //納品スケジュールは同一日に複数行のレコードが存在する可能性があるので、それらを同じ日なら１つにまとめて処理する
            w_dt_nouhin_schedule = OracleSelect("select nouhin_yotei_date,torihikisaki_cd,juchu_cd1,juchu_cd2,sum(nouhin_yotei_su) nouhin_yotei_su from tss_nouhin_schedule_m where torihikisaki_cd = '" + in_torihikisaki_cd + "' and juchu_cd1 = '" + in_juchu_cd1 + "' and juchu_cd2 = '" + in_juchu_cd2 + "' group by nouhin_yotei_date,torihikisaki_cd,juchu_cd1,juchu_cd2 order by nouhin_yotei_date asc");

            DateTime w_date;            //納品日
            TimeSpan w_timespan;        //○日前からの生産
            int w_kaisi_day;            //○日前からの生産
            DateTime w_date_start;      //生産開始日
            DataTable w_dt_calendar;    //営業カレンダー読み込み用

            double w_seisan_su;     //生産数（納品スケジュールの1レコードの）
            double w_seisan_siji_su;//生産指示数
            double w_mst_tact;      //タクト
            double w_mst_dandori;   //段取り
            double w_mst_tuika;     //追加
            double w_mst_hoju;      //補充
            double w_kousu;         //必要工数
            double w_cnt;           //ライン数のカウント
            TimeSpan w_spn_kousu;   //計算用
            DateTime w_start_time;  //開始時刻
            DateTime w_end_time;    //終了時刻
            string w_sql;           //書込み用のsql文字列
            DataTable w_dt_seq;     //最大seq算出用
            int w_seq_max;          //最大seq

            int w_loop_flg = 0;     //ループ用のフラグ
            //納品スケジュールマスタのレコード分繰り返す
            foreach(DataRow dr_nouhin_schedule in w_dt_nouhin_schedule.Rows)
            {
                //生産工程マスタのレコード分繰り返す
                foreach(DataRow dr_seisan_koutei in w_dt_seisan_koutei.Rows)
                {
                    //親子に関係なく、登録されている工程レコード分作成する
                    //2016.05.31現在、「親でも子でも、作業が発生するのであれば、生産スケジュールは必要だろう」という考え。
                    //よって、現状では生産スケジュールを作成しなくてもよい工程は無いと考えている。

                    //納品日と生産開始日から生産スケジュールの日を算出
                    //w_date = DateTime.Parse(dr_nouhin_schedule["nouhin_yotei_date"].ToString());
                    //w_timespan = TimeSpan.Parse(dr_seisan_koutei["seisan_start_day"].ToString());
                    //w_date_start = w_date - w_timespan;

                    //納品日と生産開始日から営業日を考慮した生産日を算出
                    //納品日
                    w_date = DateTime.Parse(dr_nouhin_schedule["nouhin_yotei_date"].ToString());
                    //○日前を1日ずつさかのぼりながら営業日での算出をする
                    if(int.TryParse(dr_seisan_koutei["seisan_start_day"].ToString(),out w_kaisi_day))
                    {

                    }
                    else
                    {
                        w_kaisi_day = 0;
                    }
                    w_timespan = TimeSpan.Parse("1");
                    w_date_start = w_date;
                    for (int i = 0; i < w_kaisi_day; i++)
                    {
                        //営業日を見つけるまで－1日づつして繰り返す
                        w_loop_flg = 0;
                        while(w_loop_flg ==0)
                        {
                            w_date_start = w_date_start - w_timespan;
                            w_dt_calendar = OracleSelect("select * from tss_calendar_f where calendar_year = '" + w_date_start.Year.ToString("0000") + "' and calendar_month = '" + w_date_start.Month.ToString("00") + "' and calendar_day = '" + w_date_start.Day.ToString("00") + "'");
                            if(w_dt_calendar.Rows.Count <= 0)
                            {
                                MessageBox.Show("営業カレンダーに異常があります。\n" + w_date_start.ToShortDateString());
                                return false;
                            }
                            else
                            {
                                if(w_dt_calendar.Rows[0]["eigyou_kbn"].ToString() != "1" && w_dt_calendar.Rows[0]["eigyou_kbn"].ToString() != "2")
                                {
                                    //営業日の場合
                                    w_loop_flg = 1;
                                }
                                else
                                {
                                    //休日の場合、更に一日前に進む
                                }
                            }
                        }
                    }
                    //ループを抜けるとw_date_startの中に生産日が入っている

                    //生産工程ラインマスタの選択区分が立っているレコードを取得する（選択区分は必要なもののみ立っているとみなす）
                    w_dt_seisan_koutei_line = OracleSelect("select * from tss_seisan_koutei_line_m where seihin_cd = '" + dr_seisan_koutei["seihin_cd"].ToString() + "' and seq_no = '" + dr_seisan_koutei["seq_no"].ToString() + "' and select_kbn = '1' order by seq_no asc");
                    if (w_dt_seisan_koutei_line.Rows.Count <= 0)
                    {
                        MessageBox.Show("生産工程ラインマスタの整合性が取れていません。\n確認し、再度生産スケジュールを作成してください。");
                        return false;
                    }
                    //生産工程ラインマスタのレコード分、生産スケジュールのレコードを作成する
                    //ライン選択区分と選択区分の整合性がとれているものとし、複数レコードが存在するのは「分割」の場合のみ。
                    //レコード数で生産数を割ってレコードを作成する（端数は最終レコードにまとめる）
                    w_cnt = 0;
                    foreach(DataRow dr_seisan_koutei_line in w_dt_seisan_koutei_line.Rows)
                    {
                        //計算に必要な項目を求める
                        //納品数
                        if (double.TryParse(dr_nouhin_schedule["nouhin_yotei_su"].ToString(), out w_seisan_su) == false)
                        {
                            w_seisan_su = 0;
                        }
                        //タクト
                        if (double.TryParse(dr_seisan_koutei_line["tact_time"].ToString(), out w_mst_tact) == false)
                        {
                            w_mst_tact = 0;
                        }
                        //段取時間
                        if (double.TryParse(dr_seisan_koutei_line["dandori_time"].ToString(), out w_mst_dandori) == false)
                        {
                            w_mst_dandori = 0;
                        }
                        //追加時間
                        if (double.TryParse(dr_seisan_koutei_line["tuika_time"].ToString(), out w_mst_tuika) == false)
                        {
                            w_mst_tuika = 0;
                        }
                        //補充時間
                        if (double.TryParse(dr_seisan_koutei_line["hoju_time"].ToString(), out w_mst_hoju) == false)
                        {
                            w_mst_hoju = 0;
                        }
                        //生産指示数を求める
                        w_seisan_siji_su = Math.Floor(w_seisan_su / w_dt_seisan_koutei_line.Rows.Count) - (Math.Floor(w_seisan_su / w_dt_seisan_koutei_line.Rows.Count) * w_cnt);
                        //必要工数を求める（小数点以下は切り上げる）
                        w_kousu = Math.Floor(w_seisan_siji_su * w_mst_tact + w_mst_dandori + w_mst_tuika + w_mst_hoju + 0.99);
                        //開始時刻を08:35とし、工数から終了時刻を求める
                        w_start_time = DateTime.Parse(w_date_start.ToShortDateString() + " 08:35:00");  //開始時刻
                        w_spn_kousu = TimeSpan.FromSeconds(w_kousu);                                    //必要工数（秒）
                        w_end_time = w_start_time + w_spn_kousu;                                        //終了時刻
                        //書き込むレコードseqを求める（現在登録されている生産スケジュールの最大seq+1を求める）
                        w_dt_seq = OracleSelect("Select max(seq) max_seq from tss_seisan_schedule_f where seisan_yotei_date = '" + w_date_start.ToShortDateString() + "'　and busyo_cd = '" + dr_seisan_koutei["busyo_cd"].ToString() + "' and koutei_cd = '" + dr_seisan_koutei["koutei_cd"].ToString() + "' and line_cd = '" + dr_seisan_koutei_line["line_cd"].ToString() + "'");
                        if (w_dt_seq.Rows.Count <= 0)
                        {
                            w_seq_max = 0;
                        }
                        else
                        {
                            if (int.TryParse(w_dt_seq.Rows[0]["max_seq"].ToString(), out w_seq_max) == false)
                            {
                                w_seq_max = 0;
                            }
                        }
                        w_seq_max = w_seq_max + 1;
                        //全ての項目をセットし書き込む
                        w_sql = "insert into tss_seisan_schedule_f (seisan_yotei_date,busyo_cd,koutei_cd,line_cd,seq,torihikisaki_cd,juchu_cd1,juchu_cd2,seihin_cd,seihin_name,seisankisyu,juchu_su,seisan_su,tact_time,dandori_kousu,tuika_kousu,hoju_kousu,seisan_time,start_time,end_time,seisan_zumi_su,ninzu,members,hensyu_flg,bikou,create_user_cd,create_datetime) values ("
                                + "'" + w_date_start.ToShortDateString() + "'"                              //生産予定日
                                + ",'" + dr_seisan_koutei["busyo_cd"].ToString() + "'"                      //部署コード
                                + ",'" + dr_seisan_koutei["koutei_cd"].ToString() + "'"                     //工程コード
                                + ",'" + dr_seisan_koutei_line["line_cd"].ToString() + "'"                  //ラインコード
                                + ",'" + w_seq_max.ToString("000") + "'"                                    //seq
                                + ",'" + dr_nouhin_schedule["torihikisaki_cd"].ToString() + "'"             //取引先コード
                                + ",'" + dr_nouhin_schedule["juchu_cd1"].ToString() + "'"                   //受注コード１
                                + ",'" + dr_nouhin_schedule["juchu_cd2"].ToString() + "'"                   //受注コード２
                                + ",'" + dr_seisan_koutei["seihin_cd"].ToString() + "'"                     //製品コード
                                + ",'" + w_dt_seihin.Rows[0]["seihin_name"].ToString() + "'"                //製品名
                                + ",'" + dr_seisan_koutei["seisankisyu"].ToString() + "'"                   //生産機種
                                + ",'" + w_dt_juchu.Rows[0]["juchu_su"].ToString() + "'"                    //受注数
                                + ",'" + w_seisan_siji_su.ToString() + "'"                                  //生産指示数
                                + ",'" + dr_seisan_koutei_line["tact_time"].ToString() + "'"                //タクト
                                + ",'" + dr_seisan_koutei_line["dandori_time"].ToString() + "'"             //段取り時間
                                + ",'" + dr_seisan_koutei_line["tuika_time"].ToString() + "'"               //追加時間
                                + ",'" + dr_seisan_koutei_line["hoju_time"].ToString() + "'"                //補充時間
                                + ",'" + w_kousu.ToString() + "'"                                           //必要工数
                                + ",to_date('" + w_start_time.ToString() + "','YYYY/MM/DD HH24:MI:SS')"     //開始時刻
                                + ",to_date('" + w_end_time.ToString() + "','YYYY/MM/DD HH24:MI:SS')"       //終了時刻
                                + ",'0'"                                                                    //生産済み数
                                + ",'0'"                                                                    //人数
                                + ",''"                                                                     //メンバー
                                + ",'0'"                                                                    //編集済みフラグ
                                + ",'" + w_dt_juchu.Rows[0]["bikou"].ToString() + " " + w_dt_juchu.Rows[0]["bikou2"].ToString() + "'"   //備考
                                + ",'" + user_cd + "'"                                                      //作成者コード
                                + ",SYSDATE)";                                                              //作成日時
                                ;
                        if(OracleInsert(w_sql) == false)
                        {
                            ErrorLogWrite(user_cd, "TSS System Libraly", "aam_sakuseiのOracleInsert");
                            MessageBox.Show("生産スケジュールの書き込みでエラーが発生しました。\n処理を中止します。");
                            return false;
                        }
                        w_cnt = w_cnt + 1;
                    }
                }
            }
            return true;
        }

        private bool ssm_delete(string in_torihikisaki_cd, string in_juchu_cd1, string in_juchu_cd2)
        {
            //生産スケジュール削除
            DataTable w_dt = new DataTable();
            if (OracleDelete("delete from tss_seisan_schedule_f where torihikisaki_cd = '" + in_torihikisaki_cd + "' and juchu_cd1 = '" + in_juchu_cd1 + "' and juchu_cd2 = '" + in_juchu_cd2 + "'") == false)
            {
                ErrorLogWrite(user_cd, "TSS System Libraly", "aam_deleteのOracleDelete");
                MessageBox.Show("生産スケジュールの削除でエラーが発生しました。\n処理を中止します。");
                return false;
            }
            return true;
        }

        private bool ssm_message(string in_torihikisaki_cd, string in_juchu_cd1, string in_juchu_cd2,string in_msg)
        {
            GetUser();
            //受注マスタから製品マスタ→生産工程マスタとリンクし部署コードを抽出、その部署に所属するユーザー且つ生産の権限が3以上のユーザーにメッセージを送信する
            DataTable w_dt_juchu = new DataTable();
            DataTable w_dt_seihin = new DataTable();
            DataTable w_dt_seisan_koutei = new DataTable();
            DataTable w_dt_user = new DataTable();
            //受注マスタ取得
            w_dt_juchu = OracleSelect("select * from tss_juchu_m where torihikisaki_cd = '" + in_torihikisaki_cd + "' and juchu_cd1 = '" + in_juchu_cd1 + "' and juchu_cd2 = '" + in_juchu_cd2 + "'");
            //製品マスタ取得
            w_dt_seihin = OracleSelect("select * from tss_seihin_m where seihin_cd = '" + w_dt_juchu.Rows[0]["seihin_cd"].ToString() + "'");
            //生産工程マスタ取得
            w_dt_seisan_koutei = OracleSelect("select busyo_cd from tss_seisan_koutei_m where seihin_cd = '" + w_dt_juchu.Rows[0]["seihin_cd"].ToString() + "' group by busyo_cd");
            if (w_dt_seisan_koutei.Rows.Count <= 0)
            {
                MessageBox.Show("生産工程マスタが未登録、または異常があります。\n確認し、再度生産スケジュールを作成してください。");
                return false;
            }
            foreach(DataRow dr_seisan_koutei in w_dt_seisan_koutei.Rows)
            {
                //生産工程に存在する部署に所属しているユーザー且つ生産の権限が3以上のユーザーマスタ取得
                w_dt_user = OracleSelect("select * from tss_user_m where busyo_cd = '" + dr_seisan_koutei["busyo_cd"].ToString() + "' and login_kyoka_kbn = '1' and kengen7 >= '3'");
                //ユーザーにメッセージを送信
                foreach(DataRow dr_user in w_dt_user.Rows)
                {
                    MessageLogWrite(user_cd, dr_user["user_cd"].ToString(), "生産スケジュール編集後の受注情報の変更", in_msg);
                }
            }
            return true;
        }
        #endregion

        #region date_eigyou_calc
        /// <summary>
        /// 開始日と日数を受け取り、開始日から日数分をさかのぼった営業日を返す</summary>
        /// <param name="DateTime in_datetime">
        /// 開始日コード</param>
        /// <param name="int in_day">
        /// 日数</param>
        /// <returns>
        /// DateTime w_date_start
        /// エラー等、取得できない場合は2000/01/01 00:00:00のDateTime型を返します。</returns>
        public DateTime date_eigyou_calc(DateTime in_datetime,int in_day)
        {
            TimeSpan w_timespan;
            DateTime w_date_start;
            int w_kaisi_day;
            w_timespan = TimeSpan.Parse("1");
            w_date_start = in_datetime;
            w_kaisi_day = in_day;

            int w_loop_flg = 0;
            DataTable w_dt_calendar;    //営業カレンダー読み込み用

            for (int i = 0; i < w_kaisi_day; i++)
            {
                //営業日を見つけるまで－1日づつして繰り返す
                w_loop_flg = 0;
                while (w_loop_flg == 0)
                {
                    w_date_start = w_date_start - w_timespan;
                    w_dt_calendar = OracleSelect("select * from tss_calendar_f where calendar_year = '" + w_date_start.Year.ToString("0000") + "' and calendar_month = '" + w_date_start.Month.ToString("00") + "' and calendar_day = '" + w_date_start.Day.ToString("00") + "'");
                    if (w_dt_calendar.Rows.Count <= 0)
                    {
                        MessageBox.Show("営業カレンダーに異常があります。\n" + w_date_start.ToShortDateString());
                        w_date_start = DateTime.Parse("2000/01/01 00:00:00");
                        return w_date_start;
                    }
                    else
                    {
                        if (w_dt_calendar.Rows[0]["eigyou_kbn"].ToString() != "1" && w_dt_calendar.Rows[0]["eigyou_kbn"].ToString() != "2")
                        {
                            //営業日の場合
                            w_loop_flg = 1;
                        }
                        else
                        {
                            //休日の場合、更に一日前に進む
                        }
                    }
                }
            }
            //ループを抜けるとw_date_startの中に生産日が入っている
            return w_date_start;
        }
        #endregion

        #region check_HHMM
        /// <summary>
        /// 時刻文字列を受け取り、HH:MM形式の文字列を返す</summary>
        /// <param name="String in_hhmm">
        /// HH:MM形式に変換する文字列</param>
        /// <returns>
        /// DataTable out_str
        /// エラー等、変換できない場合は null を返します。</returns>
        public string check_HHMM(string in_hhmm)
        {
            //3文字以下はNG
            if (in_hhmm.Length < 3)
            {
                return null;
            }
            //コロン（:）が先頭または末尾にあるとNG
            if (in_hhmm.Substring(0, 1) == ":" || in_hhmm.Substring(in_hhmm.Length - 1, 1) == ":")
            {
                return null;
            }
            //コロン（:）が無ければNG
            int idx;
            idx = in_hhmm.IndexOf(":");
            if (idx <= 0)
            {
                return null;
            }
            //00～23以外の時間はNG
            double dHH;
            if (double.TryParse(in_hhmm.Substring(0, idx), out dHH) == false)
            {
                //変換出来なかったら（false）NG
                return null;
            }
            if (dHH < 00 || dHH > 23)
            {
                return null;
            }
            //00～59以外の分はNG
            double dMM;
            if (double.TryParse(in_hhmm.Substring(idx + 1), out dMM) == false)
            {
                //変換出来なかったら（false）NG
                return null;
            }
            if (dMM < 00 || dMM > 59)
            {
                return null;
            }
            //正常時にはHH:MMの書式にした文字列を返す
            return dHH.ToString("00") + ":" + dMM.ToString("00");
        }
        #endregion






    }
    #endregion
}

