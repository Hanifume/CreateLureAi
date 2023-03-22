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
    /// Window10.xaml の相互作用ロジック
    /// </summary>
    public partial class Window10 : Window
    {
        public Window10()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //インクを使用できる状態にする
            inkCanvas1.EditingMode = InkCanvasEditingMode.Ink;
        }

        /// <summary>
        /// [インク]ボタンクリック時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInk_Click(object sender, RoutedEventArgs e)
        {
            //インクを使用できる状態にする
            inkCanvas1.EditingMode = InkCanvasEditingMode.Ink;
        }

        /// <summary>
        /// [選択]ボタンクリック時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            // 描画されたストロークを選択できる状態にする
            inkCanvas1.EditingMode = InkCanvasEditingMode.Select;
        }

        /// <summary>
        /// [コピー]ボタンクリック時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            // 選択されたストロークをクリップボードにコピー
            inkCanvas1.CopySelection();
        }

        /// <summary>
        /// [カット]ボタンクリック時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCut_Click(object sender, RoutedEventArgs e)
        {
            // 選択されたストロークをカットしてクリップボードにコピー
            inkCanvas1.CutSelection();
        }

        /// <summary>
        /// マウスダウン時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void inkCanvas1_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // マウスの右ボタンが押されたか？
            if (e.RightButton == MouseButtonState.Pressed)
            {
                // 右ボタンが押された位置を取得
                Point position;
                position = e.GetPosition(inkCanvas1);

                // 貼り付け可能か？
                if (inkCanvas1.CanPaste())
                    // 右ボタンが押された位置に貼り付け
                    inkCanvas1.Paste(position);
            }
        }
    }
}
