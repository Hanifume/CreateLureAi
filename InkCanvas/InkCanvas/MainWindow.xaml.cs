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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InkCanvas
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Window5 w5 = new Window5();
            w5.ShowDialog();
            this.Close();

        }

        /// <summary>
        /// 01.インクを使用する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Window1 w1 = new Window1();
            w1.ShowDialog();
        }

        /// <summary>
        /// 02.描画されたストロークを消去する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Window2 w2 = new Window2();
            w2.ShowDialog();
        }

        /// <summary>
        /// 03.描画されたストロークを選択する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Window3 w3 = new Window3();
            w3.ShowDialog();
        }

        /// <summary>
        /// 04.ペンの太さ/形状を設定する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, RoutedEventArgs e)
        {
            Window4 w4 = new Window4();
            w4.ShowDialog();
        }

        /// <summary>
        /// 05.ペンの色を変更する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, RoutedEventArgs e)
        {
            Window5 w5 = new Window5();
            w5.ShowDialog();
            this.Close();
        }

        /// <summary>
        /// 06.滑らかな線を描く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, RoutedEventArgs e)
        {
            Window6 w6 = new Window6();
            w6.ShowDialog();
        }

        /// <summary>
        /// 07.ジェスチャを認識する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, RoutedEventArgs e)
        {
            Window7 w7 = new Window7();
            w7.ShowDialog();
        }

        /// <summary>
        /// 08.ジェスチャで認識した図形を描画する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, RoutedEventArgs e)
        {
            Window8 w8 = new Window8();
            w8.ShowDialog();
        }

        /// <summary>
        /// 09.キャンバスに書いた文字を認識する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button9_Click(object sender, RoutedEventArgs e)
        {
            //Window9 w9 = new Window9();
            //w9.ShowDialog();
        }

        /// <summary>
        /// 10.クリップボードを使用する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button10_Click(object sender, RoutedEventArgs e)
        {
            Window10 w10 = new Window10();
            w10.ShowDialog();
        }

        /// <summary>
        /// 11.キャンバスに描いた絵を画像として保存する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button11_Click(object sender, RoutedEventArgs e)
        {
            Window11 w11 = new Window11();
            w11.ShowDialog();
        }

        private void button12_Click(object sender, RoutedEventArgs e)
        {
            Window12 w12 = new Window12();
            w12.ShowDialog();
        }

        /// <summary>
        /// 13.ストロークをXAML要素として保存する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button13_Click(object sender, RoutedEventArgs e)
        {
            //Window13 w13 = new Window13();
            //w13.ShowDialog();
        }
    }
}
