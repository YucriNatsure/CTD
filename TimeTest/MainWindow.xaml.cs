using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.IO;


using Microsoft.Win32;

namespace TimeTest
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public string ImageFileName = null;

        string showTime;

        Dictionary<string, string> timeAndMsgs;

        private DispatcherTimer timer;


        private System.Windows.Threading.DispatcherTimer Timer;

        public MainWindow()
        {

            InitializeComponent();

            timer = CreateTimer();

            InitProc();

            
        }
      

        private DispatcherTimer CreateTimer()
        {
            var t = new DispatcherTimer(DispatcherPriority.SystemIdle);
            t.Interval = TimeSpan.FromMilliseconds(200);

            // タイマーイベントの定義
            t.Tick += (sender, e) =>
            {
                // タイマーイベント発生時の処理をここに書く

                // 現在の時分秒をテキストに設定
                textBlock.Text = DateTime.Now.ToString("HH:mm:ss");
            };

            // 生成したタイマーを返す
            return t;
        }




        private void textBlock_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            timer.Start();

        }

        private void button1_click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();

            dialog.Filter = "画像ファイル (*.jpg)|*.png*.*";

            if (dialog.ShowDialog() == true)
            {
                ImageFileName = dialog.FileName;
                BitmapImage bitmapImage = new BitmapImage(new Uri(ImageFileName));
                IMG.Opacity = 0.50;
                IMG.Source = bitmapImage;
            }

            else
            {
                MessageBox.Show("画像が開けませんでした");
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // 一覧設定あり？
                if (IsExistsSaveConfigPath())
                {
                    using (var sr = new System.IO.StreamReader(
                        Properties.Settings.Default.SaveConfigPath, Encoding.UTF8))
                    {
                        // ファイルの終わりまで繰り返し
                        while (sr.EndOfStream == false)
                        {
                            // 一行読み込み
                            string line = sr.ReadLine();
                            // 分解
                            string[] lineAry = line.Split('\t');

                            if (lineAry.Length == 2)
                            {
                                // 追加
                                listBox.Items.Add(line);
                                timeAndMsgs.Add(lineAry[0], lineAry[1]);
                            }
                        }

                    }
                }
                // アラームON/OFF設定値読み込み
                alarmON.IsChecked = Properties.Settings.Default.AlarmEnabled;

                if (alarmON.IsChecked == true)
                {
                    // アラームONならタイマー開始
                    Timer.Start();
                }
            }
            catch (Exception ex)
            {
                ErrMsg(ex.Message);
            }
        }

        /// <param name="msg"></param>
        private void InfoMsg(string msg)
        {
            MessageBox.Show(msg, Properties.Resources.AppTitle,
                MessageBoxButton.OK, MessageBoxImage.Information);
        }


        /// <summary>
        /// エラーメッセージ表示
        /// </summary>
        /// <param name="msg"></param>
        private void ErrMsg(string msg)
        {
            MessageBox.Show(msg, Properties.Resources.AppTitle,
                MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (listBox.Items.Count > 0)
                {
                    // 一覧の保存先パスチェック
                    if (IsExistsSaveConfigPath())
                    {
                        // ファイル保存
                        SaveConfigFile();
                    }
                    else
                    {
                        // 一覧ファイル保存先パスが未設定か、指定した場所にファイルが無い場合

                        // ファイル保存ダイアログを表示してファイル保存
                        ShowSaveDialogToConfigFile();
                    }
                }

                // 画面のアラームOn/OFF設定値をSettingsに反映
                Properties.Settings.Default.AlarmEnabled = alarmON.IsChecked == true;
                // 設定ファイル保存
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                ErrMsg(ex.Message);
            }
        }

        private void SaveConfigFile()
        {
            using (var sw = new System.IO.StreamWriter(
                Properties.Settings.Default.SaveConfigPath, append: false, encoding: Encoding.UTF8))
            {
                // リストボックスの内容をファイル出力
                foreach (string item in listBox.Items)
                {
                    // 一行書き込み
                    sw.WriteLine(item);
                }
            }
        }

        private void ShowSaveDialogToConfigFile()
        {
            // ダイアログ生成
            var dlg = new Microsoft.Win32.SaveFileDialog();
            // ダイアログタイトル設定
            dlg.Title = "設定を保存";
            // 保存する一覧ファイルの既定ファイル名を設定
            dlg.FileName = Properties.Resources.AppTitle + "_設定.dat";
            // フィルタ設定
            dlg.Filter = "設定ファイル|*.dat|全てのファイル|*.*";

            // 保存ダイアログ表示
            if (dlg.ShowDialog() == true)
            {
                // ダイアログでOKされたらファイルパス設定
                Properties.Settings.Default.SaveConfigPath = dlg.FileName;
                // ファイル保存
                SaveConfigFile();
            }
        }

        private bool IsExistsSaveConfigPath()
        {
            if (string.IsNullOrWhiteSpace(Properties.Settings.Default.SaveConfigPath) ||
                System.IO.File.Exists(Properties.Settings.Default.SaveConfigPath) == false)
            {
                // パスの設定が無い、または指定した場所にファイルが存在しない
                return false;
            }
            else
            {
                // パスが設定されていてファイルが実在する
                return true;
            }
        }
       
        private void Timer_Tick(object sender, EventArgs e)
        {
            // 現在時刻取得
            string time = DateTime.Now.ToString("HH:mm");

            // 表示済みの時間ではないか？登録されている時刻か？
            if (time != showTime &&
                timeAndMsgs.ContainsKey(time))
            { 
                // メッセージ表示時間を保存
                showTime = time;
                // 対応するメッセージ表示
                InfoMsg(timeAndMsgs[time]);
            }
        }


        private void InitProc()
        {
            // 登録時間一覧初期化
            timeAndMsgs = new Dictionary<string, string>();

            showTime = "";

            // タイマー初期化
            Timer = new DispatcherTimer();
            // イベント発生間隔を200ミリ秒に設定
            Timer.Interval = TimeSpan.FromMilliseconds(200);
            // タイマーイベント設定
            Timer.Tick += new EventHandler(Timer_Tick);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime d;

            // 時刻の形式が正しいか？
            //（数字2桁:数字2桁か簡易チェックしてOKならDateTime型に変換できるかチェックする）
            if (System.Text.RegularExpressions.Regex.IsMatch(
                timeText.Text, "^[0-9]{2}:[0-9]{2}$") == false ||
                DateTime.TryParse("2000/01/01 " + timeText.Text, out d) == false)
            {
                // 時刻として正しくない
                ErrMsg("時刻を正しく入力してください。例）12:34");
                timeText.Focus();
                return;
            }

            // メッセージが入力されているか？
            if (string.IsNullOrWhiteSpace(msgText.Text))
            {
                // メッセージ未入力（または空白しか入力されていない）
                ErrMsg("メッセージを入力してください。");
                msgText.Focus();
                return;
            }

            // 時刻もメッセージも入力されている

            // まだ追加されていない時刻かチェック
            if (timeAndMsgs.ContainsKey(timeText.Text))
            {
                ErrMsg("この時刻は追加済みです。");
            }
            else
            {
                // 一覧に追加
                listBox.Items.Add(timeText.Text + "\t" + msgText.Text);
                timeAndMsgs.Add(timeText.Text, msgText.Text);

                timeText.Clear();
                msgText.Clear();
            }

            timeText.Focus();
        }

        /// <summary>
        /// アラームON/OFFチェック時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void alarmOnOff_Checked(object sender, RoutedEventArgs e)
        {
            if (Timer == null)
            {
                return;
            }

            if (alarmON.IsChecked == true)
            {
                Timer.Start();
            }
            else
            {
                Timer.Stop();
            }
        }

    }

}
    

