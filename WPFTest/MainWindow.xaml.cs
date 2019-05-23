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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ImageSource imageSource = new BitmapImage(new Uri(@"Images\test.png", UriKind.Relative));

            this.btn_img.Background = new ImageBrush
            {
                ImageSource = imageSource
            };

            // 这样设置的背景图是平铺的，需要设置，并不适应需求（这个功能效果更强大）
            //this.bor_img.Background = new ImageBrush
            //{
            //    ImageSource = imageSource,
            //    Viewport = new Rect(0,0,0.5,0.5)
            //};

            // 这个可以直接设置图片大小，默认居中，比较符合需求
            this.bor_img.Children.Add(new Image
            {
                Source = imageSource,
                Width = 125,
                Height = 161
            });
        }
    }
}
