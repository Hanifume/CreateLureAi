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
    /// Window3.xaml の相互作用ロジック
    /// </summary>
    public partial class Window3 : Window
    {
        public Window3()
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
        /// [選択]ボタンクリック時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            // 描画されたストロークを選択できる状態にする
            inkCanvas1.EditingMode = InkCanvasEditingMode.Select;
        }
    }
}
