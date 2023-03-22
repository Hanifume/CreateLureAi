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
    /// Window8.xaml の相互作用ロジック
    /// </summary>
    public partial class Window8 : Window
    {
        public Window8()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // InkAndGestureモードに設定する
            inkCanvas1.EditingMode = InkCanvasEditingMode.InkAndGesture;
            this.imageControl.Source = new BitmapImage(new Uri(@"Images\lure_side.jpg",UriKind.Relative));
        }

        /// <summary>
        /// ジェスチャを認識
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void inkCanvas1_Gesture(object sender, InkCanvasGestureEventArgs e)
        {
            ReadOnlyCollection<GestureRecognitionResult> gestureResults;
            gestureResults = e.GetGestureRecognitionResults();

            if (gestureResults[0].RecognitionConfidence ==
                RecognitionConfidence.Strong)
            {
                // 認識時の領域（外枠）サイズを取得
                Rect strokeRect = e.Strokes.GetBounds();

                switch (gestureResults[0].ApplicationGesture)
                {
                    case ApplicationGesture.Circle: // 楕円（円）認識時
                        // 認識した外枠サイズに合わせて円を作成する
                        Ellipse ellipse = new Ellipse();
                        ellipse.Width = strokeRect.Width;
                        ellipse.Height = strokeRect.Height;
                        ellipse.SetValue(System.Windows.Controls.InkCanvas.LeftProperty, strokeRect.X);
                        ellipse.SetValue(System.Windows.Controls.InkCanvas.TopProperty, strokeRect.Y);
                        ellipse.Stroke = Brushes.Black;

                        // 作成した円をキャンバスに追加
                        inkCanvas1.Children.Add(ellipse);
                        break;
                    case ApplicationGesture.Square: // 四角形認識時
                        // 認識した外枠サイズに合わせて四角形を作成する
                        Rectangle rectangle = new Rectangle();
                        rectangle.Width = strokeRect.Width;
                        rectangle.Height = strokeRect.Height;
                        rectangle.SetValue(System.Windows.Controls.InkCanvas.LeftProperty, strokeRect.X);
                        rectangle.SetValue(System.Windows.Controls.InkCanvas.TopProperty, strokeRect.Y);
                        rectangle.Stroke = Brushes.Black;

                        // 作成した四角形をキャンバスに追加
                        inkCanvas1.Children.Add(rectangle);
                        break;
                }

            }
        }

        private void Select_Checked(object sender, RoutedEventArgs e)
        {
            var button = (RadioButton)sender;

            if(button.Name.Equals("select"))
            {
                // インクを使用できる状態にする
                inkCanvas1.EditingMode = InkCanvasEditingMode.Select;
            }
            else
            {
                // インクを使用できる状態にする
                inkCanvas1.EditingMode = InkCanvasEditingMode.InkAndGesture;
            }
        }
    }
}
