using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SedentaryCountdown
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        DateTime dateTime = DateTime.Now;

        Config Config;

        public MainWindow()
        {
            InitializeComponent();

            Console.WriteLine();
            Console.WriteLine("== SedentaryCountdown ==");
            ////开启启动
            //try {
            //    File.Copy(AppDomain.CurrentDomain.BaseDirectory+"SedentaryCountdownStarter.exe", @"C:\Users\chuliang.xu\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup\SedentaryCountdownStarter.exe");
            //} catch (Exception e){ Console.WriteLine(e); }

            //读取配置
            try
            {
                using (StreamReader sr = new StreamReader("Config.json"))
                {
                    string str = sr.ReadToEnd();
                    if (!string.IsNullOrEmpty(str))
                    {
                        Config = JsonSerializer.JsonDeserialize<Config>(str);
                    }
                }
            }
            catch (Exception) { }

            CheckConfig();

            RestWindow restWindow = null;
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler((object sender, EventArgs e) =>
            {
                if (restWindow != null && restWindow.IsLoaded)
                {
                    dateTime = DateTime.Now;
                    return;
                }
                TimeSpan timeSpan = TimeSpan.FromMinutes(Config.CountdownMinute) - (DateTime.Now - dateTime);
                if (timeSpan.TotalSeconds <= 0)
                {
                    restWindow = new RestWindow(Config.RestMinute);
                    restWindow.Show();
                    dateTime = DateTime.Now;
                }
                else
                {
                    TxtCountdown.Text = $@"{timeSpan:hh\:mm\:ss}";
                }
            });
            dispatcherTimer.Interval = TimeSpan.FromSeconds(0.3);
            dispatcherTimer.Start();

            Left = SystemParameters.PrimaryScreenWidth - Width - 50;
            Top = 50;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.DragMove();
        }

        private void MenuItemClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuItemReset_Click(object sender, RoutedEventArgs e)
        {
            dateTime = DateTime.Now;
        }

        private void MenuItemFaster_Click(object sender, RoutedEventArgs e)
        {
            dateTime -= TimeSpan.FromMinutes(5);
        }

        private void MenuItemSlower_Click(object sender, RoutedEventArgs e)
        {
            dateTime += TimeSpan.FromMinutes(5);
        }

        private void MenuItemConfig_Click(object sender, RoutedEventArgs e)
        {
            new ConfigWindow(Config).ShowDialog();

            CheckConfig();

            try
            {
                string str = JsonSerializer.JsonSerialize<Config>(Config);
                if (!string.IsNullOrEmpty(str))
                {
                    using (StreamWriter sw = new StreamWriter("Config.json"))
                    {
                        sw.Write(str);
                    }
                }
            }
            catch (Exception) { }
        }

        void CheckConfig()
        {
            if (Config == null) Config = new Config();
            if (Config.CountdownMinute < 1) Config.CountdownMinute = 50;//最少1分钟，默认50分钟
            if (Config.RestMinute < 1) Config.RestMinute = 10;//最少1分钟,默认10分钟
        }
    }
}
