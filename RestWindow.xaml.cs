using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SedentaryCountdown
{
    /// <summary>
    /// RestWindow.xaml 的交互逻辑
    /// </summary>
    public partial class RestWindow : Window
    {
        public RestWindow()
        {
            InitializeComponent();

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler((object sender, EventArgs e) =>
            {
                Close();
            });
            dispatcherTimer.Interval = TimeSpan.FromSeconds(10);
            dispatcherTimer.Start();
        }

        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}
