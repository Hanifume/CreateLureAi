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
    /// Window6.xaml の相互作用ロジック
    /// </summary>
    public partial class Window6 : Window
    {
        public Window6()
        {
            InitializeComponent();
        }


        // 描画属性用変数
        private DrawingAttributes inkDA = new DrawingAttributes();

        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // ペンの幅を5に設定する
            inkDA.Width = 5;
            inkDA.Height = 5;
            inkCanvas1.DefaultDrawingAttributes = inkDA;
        }

        /// <summary>
        /// 「通常の線」選択時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdoFitToCurveOff_Checked(object sender, RoutedEventArgs e)
        {
            // FitToCurve を Off にする
            inkDA.FitToCurve = false;
            inkCanvas1.DefaultDrawingAttributes = inkDA;
        }

        /// <summary>
        /// 「滑らかな線」選択時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdoFitToCurveOn_Checked(object sender, RoutedEventArgs e)
        {
            // FitToCurve を On にする
            inkDA.FitToCurve = true;
            inkCanvas1.DefaultDrawingAttributes = inkDA;
        }
    }
}
