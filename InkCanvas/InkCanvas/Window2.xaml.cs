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

namespace InkCanvas
{
    /// <summary>
    /// Window2.xaml の相互作用ロジック
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
        }

        /// <summary>
        /// [インク]ボタンクリック時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInk_Click(object sender, RoutedEventArgs e)
        {
            // インクを使用できる状態にする
            inkCanvas1.EditingMode = InkCanvasEditingMode.Ink;
        }

        /// <summary>
        /// [ポイント消しゴム]ボタンクリック時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEraseByPoint_Click(object sender, RoutedEventArgs e)
        {
            // ペンがストロークと交差した部分のみを消去する
            inkCanvas1.EditingMode = InkCanvasEditingMode.EraseByPoint;
        }

        /// <summary>
        /// [ストローク消しゴム]ボタンクリック時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEraseByStroke_Click(object sender, RoutedEventArgs e)
        {
            // ペンが交差したストロークの全体を消去する
            inkCanvas1.EditingMode = InkCanvasEditingMode.EraseByStroke;
        }

        /// <summary>
        /// [ストローク全消去]ボタンクリック時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAllClear_Click(object sender, RoutedEventArgs e)
        {
            // 全ストロークを消去する
            inkCanvas1.Strokes.Clear();
        }
    }
}
