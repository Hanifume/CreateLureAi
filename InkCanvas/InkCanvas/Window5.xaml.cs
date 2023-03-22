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
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using InkCanvas.Common;
using System.Collections.ObjectModel;
using static InkCanvas.Common.Pantone;
using System.Net;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;

namespace InkCanvas
{
    /// <summary>
    /// Window5.xaml の相互作用ロジック
    /// </summary>
    public partial class Window5 : System.Windows.Window
    {
        //作成するモデル名
        public string CreateModelName = string.Empty;
        //作成するカラーコード
        public string CreateColorCode = string.Empty;
        //作成するカラー名
        public string CreateColorName = string.Empty;
        private const string LURE_SIDE = "lure_side";
        private const string LURE_UNDER = "lure_under";
        private const string LURE_REAR = "lure_rear";
        private const string LURE_FRONT = "lure_front";
        private const string LURE_BERRY = "lure_top";

        private List<int> alphaList = new List<int>() { 25, 50, 75, 100, 125, 150, 175, 200, 225, 255 };

        private List<int> rowIndesxes = new List<int>() { 10, 24, 35, 46, 46 };

        private List<int> columnIndesxes = new List<int>() { 5, 5, 5, 3, 18 };

        private ObservableCollection<PantoneColor> PantonColoes = new ObservableCollection<PantoneColor>();

        private ObservableCollection<PantoneColor> SearchWordsPantonColor = new ObservableCollection<PantoneColor>();

        List<System.Windows.Ink.StrokeCollection> _added = new List<StrokeCollection>();
        List<System.Windows.Ink.StrokeCollection> _removed = new List<StrokeCollection>();
        private bool handle = true;

        public Window5()
        {
            InitializeComponent();

            inkCanvas1.Strokes.StrokesChanged += Strokes_StrokesChanged;
            inkCanvas2.Strokes.StrokesChanged += Strokes_StrokesChanged;
            inkCanvas3.Strokes.StrokesChanged += Strokes_StrokesChanged;
            inkCanvas4.Strokes.StrokesChanged += Strokes_StrokesChanged;
            inkCanvas5.Strokes.StrokesChanged += Strokes_StrokesChanged;
        }

        private List<Control> canvases = new List<Control>();
        // 描画属性用変数
        private DrawingAttributes inkDA = new DrawingAttributes();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            radioNormalPen.IsChecked = true;

            // ペン幅の初期化(20にする)
            inkDA.Width = 5;
            inkDA.Height = 5;
            inkDA.FitToCurve = true;
            //ペンの色を黒で初期化
            inkDA.Color = Colors.Black;

            // 上で作成した属性値をキャンバスにセット
            inkCanvas1.DefaultDrawingAttributes = inkDA;
            inkCanvas2.DefaultDrawingAttributes = inkDA;
            inkCanvas3.DefaultDrawingAttributes = inkDA;
            inkCanvas4.DefaultDrawingAttributes = inkDA;
            inkCanvas5.DefaultDrawingAttributes = inkDA;

            ImageBrush brush = new ImageBrush()
            {
                ImageSource = new BitmapImage(new Uri(@"Images\lure_side.jpg", UriKind.Relative))
            };

            inkCanvas1.Background = brush;

            ImageBrush brush_under = new ImageBrush()
            {
                ImageSource = new BitmapImage(new Uri(@"Images\lure_under.jpg", UriKind.Relative))
            };

            inkCanvas3.Background = brush_under;

            ImageBrush brush_top = new ImageBrush()
            {
                ImageSource = new BitmapImage(new Uri(@"Images\lure_top.jpg", UriKind.Relative))
            };

            inkCanvas2.Background = brush_top;

            ImageBrush brush_front = new ImageBrush()
            {
                ImageSource = new BitmapImage(new Uri(@"Images\lure_front.jpg", UriKind.Relative))
            };

            inkCanvas4.Background = brush_front;

            ImageBrush brush_rear = new ImageBrush()
            {
                ImageSource = new BitmapImage(new Uri(@"Images\lure_rear.png", UriKind.Relative))
            };

            inkCanvas5.Background = brush_rear;

            var xmls = new XmlSerializer(typeof(Pantone));

            using (FileStream fileStream = new FileStream(System.IO.Path.Combine(Environment.CurrentDirectory, "Data", "PantoneList.xml"), FileMode.Open))
            {
                XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(Pantone));
                var deserializeData = (Pantone)serializer.Deserialize(fileStream);

                foreach (var item in deserializeData.PantoneColors)
                    PantonColoes.Add(item);
            }

            this.PantonColors.ItemsSource = PantonColoes;
        }

        /// <summary>
        /// 通常ペン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioNormalPen_Checked(object sender, RoutedEventArgs e)
        {
            // 蛍光ペンをオフ（通常のペン）
            inkDA.IsHighlighter = false;
            inkCanvas1.DefaultDrawingAttributes = inkDA;
            inkCanvas1.EditingMode = InkCanvasEditingMode.InkAndGesture;
            inkCanvas2.DefaultDrawingAttributes = inkDA;
            inkCanvas2.EditingMode = InkCanvasEditingMode.InkAndGesture;
            inkCanvas3.DefaultDrawingAttributes = inkDA;
            inkCanvas3.EditingMode = InkCanvasEditingMode.InkAndGesture;
            inkCanvas4.DefaultDrawingAttributes = inkDA;
            inkCanvas4.EditingMode = InkCanvasEditingMode.InkAndGesture;
            inkCanvas5.DefaultDrawingAttributes = inkDA;
            inkCanvas5.EditingMode = InkCanvasEditingMode.InkAndGesture;
        }

        /// <summary>
        /// 蛍光ペン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioHighlightPen_Checked(object sender, RoutedEventArgs e)
        {
            // 蛍光ペンをオン
            inkDA.IsHighlighter = true;
            inkCanvas1.DefaultDrawingAttributes = inkDA;
        }

        /// <summary>
        /// アルファ変更時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //UpdatePen();
        }

        /// <summary>
        /// スライダーの設定を反映させる
        /// </summary>
        private void UpdatePen()
        {
            Color penColor = new Color();

            // ペンの色を作成
            penColor = Color.FromArgb(
                Convert.ToByte(sliderA.Value),
                Convert.ToByte(sliderR.Value),
                Convert.ToByte(sliderG.Value),
                Convert.ToByte(sliderB.Value));

            // 作成した色をペンに割り当てる
            inkDA.Color = penColor;
            inkCanvas1.DefaultDrawingAttributes = inkDA;
            this.inkCanvas2.DefaultDrawingAttributes = inkDA;
            this.inkCanvas3.DefaultDrawingAttributes = inkDA;
            this.inkCanvas4.DefaultDrawingAttributes = inkDA;
            this.inkCanvas5.DefaultDrawingAttributes = inkDA;

            ColorPanel.Background = new SolidColorBrush(penColor);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            List<string> fileNames = new List<string>();
            fileNames.Add(Save(inkCanvas1, $"{LURE_SIDE}.jpg"));
            fileNames.Add(Save(inkCanvas2, $"{LURE_BERRY}.jpg"));
            fileNames.Add(Save(inkCanvas3, $"{LURE_UNDER}.jpg"));
            fileNames.Add(Save(inkCanvas4, $"{LURE_FRONT}.jpg"));
            fileNames.Add(Save(inkCanvas5, $"{LURE_REAR}.jpg"));

            EXCEL作成("test", "test", "test", fileNames, rowIndesxes, columnIndesxes);
        }

        private string Save(System.Windows.Controls.InkCanvas canvas, string fileName)
        {
            // レンダリングオブジェクトの描画先を作成する
            var drawingVisual = new DrawingVisual();
            var drawingContext = drawingVisual.RenderOpen();

            // InkCanvas 原寸大の描画「領域(rectangle)」を定義する
            var rect = new Rect(0, 0, canvas.ActualWidth, canvas.ActualHeight);

            if (rect.Width == 0 || rect.Height == 0)
                return System.IO.Path.Combine(Environment.CurrentDirectory, "Images", fileName);
            // レンダリングオブジェクトに、背景画像を描画する
            drawingContext.DrawRectangle(canvas.Background, null, rect);
            // レンダリングオブジェクトに、追加でストローク情報を描画する
            canvas.Strokes.Draw(drawingContext);
            // レンダリングオブジェクト情報をフラッシュする
            drawingContext.Close();

            // 描画先をビットマップにする(96dpi)
            var renderTargetBitmap = new RenderTargetBitmap(
              (int)rect.Width, (int)rect.Height, 96d, 96d, PixelFormats.Default
            );
            renderTargetBitmap.Render(drawingVisual);

            // 描画先を 幅・高さ共に0.5倍へ変換する
            var scaledBitmap = new TransformedBitmap(renderTargetBitmap,
              new ScaleTransform(1d, 1d));

            // ここまでの描画情報をエンコーダーのフレームに追加する
            var encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(scaledBitmap));

            if (!Directory.Exists(System.IO.Path.Combine(Environment.CurrentDirectory, "Save")))
                Directory.CreateDirectory(System.IO.Path.Combine(Environment.CurrentDirectory, "Save"));

            if (!Directory.Exists(System.IO.Path.Combine(Environment.CurrentDirectory, "Save","test_test_test")))
                Directory.CreateDirectory(System.IO.Path.Combine(Environment.CurrentDirectory, "Save", "test_test_test"));

            // C:\Temp\test.jpg へ JPG として出力
            using (var fileStream = File.Create(System.IO.Path.Combine(Environment.CurrentDirectory, "Save", "test_test_test", fileName)))
            {
                encoder.Save(fileStream);
            }

            return System.IO.Path.Combine(Environment.CurrentDirectory, "Save", "test_test_test", fileName);
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TxtPenWidth_TextChanged(object sender, TextChangedEventArgs e)
        {
            //this.inkDA.Width = 
        }

        private void TxtPenWidth_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !new Regex("[0-9]").IsMatch(e.Text);
        }

        private void RadioRectanglePen_Checked(object sender, RoutedEventArgs e)
        {
            this.inkDA.StylusTip = StylusTip.Rectangle;

            if (inkCanvas1 == null)
                return;

            this.inkCanvas1.DefaultDrawingAttributes = inkDA;
            this.inkCanvas2.DefaultDrawingAttributes = inkDA;
            this.inkCanvas3.DefaultDrawingAttributes = inkDA;
            this.inkCanvas4.DefaultDrawingAttributes = inkDA;
            this.inkCanvas5.DefaultDrawingAttributes = inkDA;
        }

        private void RadioCirclePen_Checked(object sender, RoutedEventArgs e)
        {
            this.inkDA.StylusTip = StylusTip.Ellipse;

            if (inkCanvas1 == null)
                return;

            this.inkCanvas1.DefaultDrawingAttributes = inkDA;
            this.inkCanvas2.DefaultDrawingAttributes = inkDA;
            this.inkCanvas3.DefaultDrawingAttributes = inkDA;
            this.inkCanvas4.DefaultDrawingAttributes = inkDA;
            this.inkCanvas5.DefaultDrawingAttributes = inkDA;
        }

        private void SliderPenWidth_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var val = (int)sliderPenWidth.Value;

            this.inkDA.Width = val;
            this.inkDA.Height = val;

            if (inkCanvas1 != null)
                this.inkCanvas1.DefaultDrawingAttributes = inkDA;
            if (inkCanvas2 != null)
                this.inkCanvas2.DefaultDrawingAttributes = inkDA;
            if (inkCanvas3 != null)
                this.inkCanvas3.DefaultDrawingAttributes = inkDA;
            if (inkCanvas4 != null)
                this.inkCanvas4.DefaultDrawingAttributes = inkDA;
            if (inkCanvas5 != null)
                this.inkCanvas5.DefaultDrawingAttributes = inkDA;
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var selectedLabel = (System.Windows.Controls.Label)sender;

            var selectItem = PantonColoes.Where(r => r.PantoneColorCode.Equals(selectedLabel.Content)).FirstOrDefault();

            var brush = (SolidColorBrush)(new BrushConverter().ConvertFrom(selectItem.ColorHex));

            sliderA.Value = brush.Color.A;
            sliderR.Value = selectItem.ColorR;
            sliderG.Value = selectItem.ColorG;
            sliderB.Value = selectItem.ColorB;

            UpdatePen();
        }

        private void KeywordSearch_Click(object sender, RoutedEventArgs e)
        {
            SearchWordsPantonColor.Clear();

            var searchWords = this.txtSearchWord.Text.Split(',');

            List<string> colorItems = new List<string>();

            foreach (var str in searchWords)
            {
                var dataString = $"search.php?q={str}" + string.Join("+", searchWords);
                //var dataString = $"search.php?q={str}" + string.Join("+", searchWords);

                var byteData = Encoding.UTF8.GetBytes(dataString);

                var request = WebRequest.Create($"https://alexbeals.com/projects/colorize/{dataString}");

                var responce = request.GetResponse();

                string webRes = string.Empty;

                using (var stream = new StreamReader(responce.GetResponseStream(), Encoding.GetEncoding("UTF-8")))
                {
                    webRes = stream.ReadToEnd();
                }

                var text = Regex.Replace(webRes, "<[^>]*?>", "");

                text = text.Replace("\r", "");
                text = text.Replace("\n", "");
                text = text.Replace("\t", "");

                var targetCnt = text.IndexOf('#');
                colorItems.Add(text.Substring(targetCnt, 7));
            }

            //取得したカラーに近いアイテムを取得
            foreach (var item in colorItems)
            {
                var brush = (SolidColorBrush)(new BrushConverter().ConvertFrom(item));

                //var pantonList = PantonColoes.Where(r => (r.ColorR < brush.Color.R - 10 && r.ColorR > brush.Color.R + 10) &&
                //                                                                    (r.ColorG < brush.Color.G - 10 && r.ColorG > brush.Color.G + 10) &&
                //                                                                    (r.ColorB < brush.Color.B - 10 && r.ColorB > brush.Color.B + 10)).ToList();

                var pantonList = GetColor(brush);
                //if (!pantonList.Any())
                //    foreach (var alpha in alphaList)
                //        pantonList.Add();
                foreach (var panton in pantonList)
                    SearchWordsPantonColor.Add(panton);

            }

            this.KeyWordColors.ItemsSource = SearchWordsPantonColor.Distinct();
        }

        private void BtnPantonSearch_Click(object sender, RoutedEventArgs e)
        {
            var searchText = txtPantoneSearch.Text;

            this.PantonColors.ItemsSource = PantonColoes.Where(r => r.PantoneColorCode.Contains(searchText)).ToList();

        }
        private PantoneColor CreateSolidBrushColor(int alpha, string srcColor)
        {
            return new PantoneColor();
        }

        private void KeywordSearchClear_Click(object sender, RoutedEventArgs e)
        {
            this.txtSearchWord.Text = string.Empty;

            this.txtSearchWord.Focus();
        }

        private void PantonSearchClear_Click(object sender, RoutedEventArgs e)
        {
            this.txtPantoneSearch.Text = string.Empty;

            this.txtPantoneSearch.Focus();
        }

        private void RadioPointEracerPen_Checked(object sender, RoutedEventArgs e)
        {
            var control = (RadioButton)sender;

            if(control.Name.Equals("radioPointEracerPen"))
            {
                inkCanvas1.EditingMode = InkCanvasEditingMode.EraseByPoint;
                inkCanvas2.EditingMode = InkCanvasEditingMode.EraseByPoint;
                inkCanvas3.EditingMode = InkCanvasEditingMode.EraseByPoint;
                inkCanvas4.EditingMode = InkCanvasEditingMode.EraseByPoint;
                inkCanvas5.EditingMode = InkCanvasEditingMode.EraseByPoint;
            }
            else
            {
                inkCanvas1.EditingMode = InkCanvasEditingMode.EraseByStroke;
                inkCanvas2.EditingMode = InkCanvasEditingMode.EraseByStroke;
                inkCanvas3.EditingMode = InkCanvasEditingMode.EraseByStroke;
                inkCanvas4.EditingMode = InkCanvasEditingMode.EraseByStroke;
                inkCanvas5.EditingMode = InkCanvasEditingMode.EraseByStroke;
            }
        }

        private bool NearColor(System.Drawing.Color col1, System.Drawing.Color col2)
        {
            // 双方の明度が黒または白に近ければ同じと見なす
            var br1 = col1.GetBrightness();
            var br2 = col2.GetBrightness();
            if (br1 < 0.12f && br2 < 0.12f) return true;
            if (br1 >= 0.9f && br2 >= 0.9f) return true;

            // 明度差と色相差がどちらも範囲内か
            var diffB = Math.Abs(br1 - br2);                      // 明度の差
            var diffS = Math.Abs(col1.GetHue() - col2.GetHue());  // 色相の差
            if (diffS > 180) diffS = 360 - diffS;
            return diffB < 0.15f && diffS < 360 * 0.08f;
        }

        private List<PantoneColor> GetColor(SolidColorBrush brush)
        {

            List<PantoneColor> retList = new List<PantoneColor>();

            var color1 = System.Drawing.Color.FromArgb(brush.Color.A, brush.Color.R, brush.Color.G, brush.Color.B);

            foreach(var pantoneColor in PantonColoes)
            {
                var obj = ColorConverter.ConvertFromString(pantoneColor.ColorHex);

                if (obj == null)
                    continue;

                var solidPantone = new SolidColorBrush((Color)obj);

                if (NearColor(color1, System.Drawing.Color.FromArgb(solidPantone.Color.A, solidPantone.Color.R, solidPantone.Color.G, solidPantone.Color.B)))
                    retList.Add(pantoneColor);
            }

            return retList;
        }
        #region undo/redo
        private void Strokes_StrokesChanged(object sender, System.Windows.Ink.StrokeCollectionChangedEventArgs e)
        {
            if (handle)
            {
                _added.Add(e.Added);
                _removed.Add(e.Removed);
            }
        }


        private void Undo(object sender, RoutedEventArgs e)
        {
            //foreach(var item in this.tabList.)
            if(this.tabSide.IsSelected)
            {
                if (inkCanvas1.Strokes.Count > 0)
                    inkCanvas1.Strokes.RemoveAt(inkCanvas1.Strokes.Count - 1);

                if(inkCanvas1.Children.Count >0)
                    inkCanvas1.Children.RemoveAt(inkCanvas1.Children.Count - 1);

            }
            else if(this.tabUp.IsSelected)
            {
                if (inkCanvas2.Strokes.Count > 0)
                    inkCanvas2.Strokes.RemoveAt(inkCanvas2.Strokes.Count - 1);
            }
            else if (this.tabBottom.IsSelected)
            {
                if (inkCanvas3.Strokes.Count > 0)
                    inkCanvas3.Strokes.RemoveAt(inkCanvas3.Strokes.Count - 1);
            }
            else if (this.tabFront.IsSelected)
            {
                if (inkCanvas4.Strokes.Count > 0)
                    inkCanvas4.Strokes.RemoveAt(inkCanvas4.Strokes.Count - 1);
            }
            else if (this.tabRear.IsSelected)
            {
                if (inkCanvas5.Strokes.Count > 0)
                    inkCanvas5.Strokes.RemoveAt(inkCanvas5.Strokes.Count - 1);
            }

            //foreach(var page in tabList)
            //if (YourInkCanva.Strokes.Count > 0)
            //{
            //    YourInkCanva.Strokes.RemoveAt(YourInkCanva.Strokes.Count - 1);
            //}
            //else
            //{
            //    // Else Do Nothing.
            //}
        }

        private void Redo(object sender, RoutedEventArgs e)
        {
            handle = false;
            inkCanvas1.Strokes.Add(_added.LastOrDefault());
            inkCanvas1.Strokes.Remove(_removed.LastOrDefault());
            handle = true;
        }
        #endregion

        #region Gesture
        private void inkCanvas_Gesture(object sender, InkCanvasGestureEventArgs e)
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
                        ellipse.Stroke = new SolidColorBrush(inkDA.Color);
                        ellipse.Fill = new SolidColorBrush(inkDA.Color);
                        // 作成した円をキャンバスに追加
                        inkCanvas1.Children.Add(ellipse);
                        break;
                    case ApplicationGesture.Square: // 四角形認識時
                        // 認識した外枠サイズに合わせて四角形を作成する
                        System.Windows.Shapes.Rectangle rectangle = new System.Windows.Shapes.Rectangle();
                        rectangle.Width = strokeRect.Width;
                        rectangle.Height = strokeRect.Height;
                        rectangle.SetValue(System.Windows.Controls.InkCanvas.LeftProperty, strokeRect.X);
                        rectangle.SetValue(System.Windows.Controls.InkCanvas.TopProperty, strokeRect.Y);
                        rectangle.Stroke = new SolidColorBrush(inkDA.Color);
                        rectangle.Fill = new SolidColorBrush(inkDA.Color);
                        // 作成した四角形をキャンバスに追加
                        inkCanvas1.Children.Add(rectangle);
                        break;
                }

            }
        }
        #endregion

        private void InkCanvas1_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (inkCanvas1.EditingMode == InkCanvasEditingMode.InkAndGesture)
                inkCanvas1.EditingMode = InkCanvasEditingMode.Select;
            else if(inkCanvas1.EditingMode == InkCanvasEditingMode.Select)
                inkCanvas1.EditingMode = InkCanvasEditingMode.InkAndGesture;
        }

        #region excel
        public bool EXCEL作成(string modelName, string modelColorCode, string modelColorName, List<string> imagePathList = null, List<int> RowIndexs = null, List<int> ColumnIndexs = null)
        {
            bool isExcelSave = false;
            string saveExcelName = string.Empty;
            var ファイル名 = $"{modelName}_{modelColorCode}_{modelColorName}.xlsx";
            var テンプレートファイルパス = Environment.CurrentDirectory + @"\Data\template.xlsx";
            try
            {
                //var createExcelFilePath = 入力フォームLogic.CreateEXCEL(entity, ファイル名, テンプレートファイルパス);
                //アプリケーションのオブジェクト
                Microsoft.Office.Interop.Excel.Application excel;
                //ブックのオブジェクト
                Workbook workbook;
                Workbook newbook;
                //アプリケーションのインスタンス作成
                excel = new Microsoft.Office.Interop.Excel.Application
                {
                    //アプリケーションの表示設定 Visible=true:Excel表示　false:Excel非表示(バックグラウンド処理)
                    Visible = false
                };
                //ファイルを開く
                workbook = excel.Workbooks.Open(System.IO.Path.GetFullPath(テンプレートファイルパス));
                newbook = excel.Workbooks.Add();

                for (int i = 1; i <= workbook.Sheets.Count; i++)
                {
                    Worksheet sheet = workbook.Sheets[i];
                    if (sheet.Name.Equals("sample"))
                    {
                        sheet.Range["F5"].Value = modelName;
                        sheet.Range["W5"].Value = modelColorCode;
                        sheet.Range["W6"].Value = modelColorName;
                        //saide picture
                        //sheet.Range["E10"].Value = imageList[0];
                        //back picture
                        //sheet.Range["E24"].Value = data["性別"];
                        //under?(berry) picture
                        //sheet.Range["E35"].Value = Get和暦生年月日(DateTime.Parse(data["生年月日"].ToString()).ToString("yyyy/MM/dd"));
                        //front picture
                        //sheet.Range["C46"].Value = entity.病名;
                        //rear picture
                        //sheet.Range["R46"].Value = entity.現在の状態;
                        if (RowIndexs.Count == ColumnIndexs.Count)
                        {
                            for (int cnt = 0; cnt < RowIndexs.Count; cnt++)
                            {
                                Range targetCell = sheet.Cells[RowIndexs[cnt], ColumnIndexs[cnt]];
                                double left = targetCell.Left;
                                double top = targetCell.Top;
                                double width = 0f;
                                double height = 0f;
                                var shape = sheet.Shapes.AddPicture(imagePathList[cnt], Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue, (float)left, (float)top, (float)width, (float)height);
                                if (cnt == 4)
                                {
                                    shape.ScaleWidth(0.5f, Microsoft.Office.Core.MsoTriState.msoTrue);
                                    shape.ScaleHeight(0.5f, Microsoft.Office.Core.MsoTriState.msoTrue);
                                }
                                else
                                {
                                    shape.ScaleWidth(1f, Microsoft.Office.Core.MsoTriState.msoTrue);
                                    shape.ScaleHeight(1f, Microsoft.Office.Core.MsoTriState.msoTrue);
                                }
                            }
                        }
                    }

                    sheet.Copy(Type.Missing, newbook.Worksheets[newbook.Worksheets.Count]);

                }

                List<int> deleteSheetList = new List<int>();
                for (int i = 1; i <= newbook.Sheets.Count; i++)
                {
                    Worksheet sheet = newbook.Sheets[i];
                    if (sheet.Name.Contains("Sheet"))
                        deleteSheetList.Add(i);
                }

                int deleteCount = 0;
                foreach (var name in deleteSheetList)
                {
                    var sheet = newbook.Worksheets[name - deleteCount];
                    sheet.Delete();
                    deleteCount += 1;
                }

                newbook.SaveCopyAs("test_test_test.xlsx");

                //ブック閉じる
                newbook.Close(false);
                Marshal.ReleaseComObject(newbook);

                workbook.Close(false);
                Marshal.ReleaseComObject(workbook);

                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();

                // アプリケーションのオブジェクトの解放
                excel.Quit();
                Marshal.ReleaseComObject(excel);
                //logger.Info($"エクセルへの出力が完了しました。");
                return true;
            }
            catch (Exception ex)
            {
                //logger.Info($"システムエラーが発生しました。{ex.ToString()}");
                //MessageCommon.MassageError_OK("システムエラーが発生しました。");
                return false;
            }
            finally
            {
                //ガベージコレクションの実行
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }
        #endregion
    }
}
