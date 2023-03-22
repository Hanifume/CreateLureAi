using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

// 追加
using System.Windows.Ink;

namespace InkCanvas
{
    /// <summary>
    /// Window4.xaml の相互作用ロジック
    /// </summary>
    public partial class Window4 : Window
    {
        public Window4()
        {
            InitializeComponent();
        }

        // 描画属性用変数
        private DrawingAttributes inkDA = new DrawingAttributes();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // 初期化
            inkDA.Width = 1;    // ペンの幅
            inkDA.Height = 1;   // ペンの高さ
            inkCanvas1.DefaultDrawingAttributes = inkDA;
        }

        /// <summary>
        /// ペンの形状を●にした場合
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton1_Checked(object sender, RoutedEventArgs e)
        {
            if (inkCanvas1 == null)
                return;

            // 楕円形の先端に設定
            inkDA.StylusTip = StylusTip.Ellipse;

            // 属性をセット
            inkCanvas1.DefaultDrawingAttributes = inkDA;
        }

        /// <summary>
        /// ペンの形状を■にした場合
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton2_Checked(object sender, RoutedEventArgs e)
        {
            // 楕円形の先端に設定
            inkDA.StylusTip = StylusTip.Rectangle;

            // 属性をセット
            inkCanvas1.DefaultDrawingAttributes = inkDA;
        }

        /// <summary>
        /// ペンの太さ変更時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (inkCanvas1 == null)
                return;

            // ペン先の幅と高さを設定
            inkDA.Width = slider1.Value;
            inkDA.Height = slider1.Value;

            inkCanvas1.DefaultDrawingAttributes = inkDA;
        }
    }
}
