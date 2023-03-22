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

// 下記を追加
using System.Windows.Ink;
using System.Collections.ObjectModel;

namespace InkCanvas
{
    /// <summary>
    /// Window7.xaml の相互作用ロジック
    /// </summary>
    public partial class Window7 : Window
    {
        public Window7()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // InkAndGesture に設定した場合、認識されたかったストロークはInkCanvas上に残ります
            // GestureOnly にした場合には、どのストロークもInkCanvas上に残りません

            inkCanvas1.EditingMode = InkCanvasEditingMode.InkAndGesture;

            // または下記
            // InkCanvas1.EditingMode = InkCanvasEditingMode.GestureOnly
        }

        /// <summary>
        /// ジェスチャの認識
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void inkCanvas1_Gesture(object sender, InkCanvasGestureEventArgs e)
        {
            ReadOnlyCollection<GestureRecognitionResult> gestureResults;

            // ジェスチャの認識結果を取得
            gestureResults = e.GetGestureRecognitionResults();

            // 認識結果の信頼性が高い順にメッセージを表示する
            if ( gestureResults[0].RecognitionConfidence == RecognitionConfidence.Strong )
                MessageBox.Show(gestureResults[0].ApplicationGesture.ToString());
        }
    }
}
