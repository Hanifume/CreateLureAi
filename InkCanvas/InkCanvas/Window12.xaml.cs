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
    /// Window12.xaml の相互作用ロジック
    /// </summary>
    public partial class Window12 : Window
    {
        public Window12()
        {
            InitializeComponent();
        }

        /// <summary>
        /// [ISF形式で保存する]ボタンクリック時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveAsISF_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlgSave = new Microsoft.Win32.SaveFileDialog();

            dlgSave.Filter = "ISFファイル(*.isf)|*.isf";

            if ((bool)dlgSave.ShowDialog())
            {
                using ( System.IO.FileStream fs = 
                    new System.IO.FileStream(dlgSave.FileName, System.IO.FileMode.Create))
                {
                    inkCanvas1.Strokes.Save(fs);
                }

                MessageBox.Show("保存しました");
            }
        }

        /// <summary>
        /// [ISF形式ファイルを開く]ボタンクリック時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenISF_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlgOpen = new Microsoft.Win32.OpenFileDialog();

            dlgOpen.Filter = "ISFファイル(*.isf)|*.isf";

            if ((bool)dlgOpen.ShowDialog())
            {
                // 現在のストロークをクリア
                inkCanvas1.Strokes.Clear();

                using (System.IO.FileStream fs =
                    new System.IO.FileStream(dlgOpen.FileName, System.IO.FileMode.Open))
                {
                    inkCanvas1.Strokes = new System.Windows.Ink.StrokeCollection(fs);
                }
            }
        }
    }
}
