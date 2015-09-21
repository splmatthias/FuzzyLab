using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RoboSim.controls
{
    /// <summary>
    /// Führen Sie die Schritte 1a oder 1b und anschließend Schritt 2 aus, um dieses benutzerdefinierte Steuerelement in einer XAML-Datei zu verwenden.
    ///
    /// Schritt 1a) Verwenden des benutzerdefinierten Steuerelements in einer XAML-Datei, die im aktuellen Projekt vorhanden ist.
    /// Fügen Sie dieses XmlNamespace-Attribut dem Stammelement der Markupdatei 
    /// an der Stelle hinzu, an der es verwendet werden soll:
    ///
    ///     xmlns:MyNamespace="clr-namespace:RoboSim.controls"
    ///
    ///
    /// Schritt 1b) Verwenden des benutzerdefinierten Steuerelements in einer XAML-Datei, die in einem anderen Projekt vorhanden ist.
    /// Fügen Sie dieses XmlNamespace-Attribut dem Stammelement der Markupdatei 
    /// an der Stelle hinzu, an der es verwendet werden soll:
    ///
    ///     xmlns:MyNamespace="clr-namespace:RoboSim.controls;assembly=RoboSim.controls"
    ///
    /// Darüber hinaus müssen Sie von dem Projekt, das die XAML-Datei enthält, einen Projektverweis
    /// zu diesem Projekt hinzufügen und das Projekt neu erstellen, um Kompilierungsfehler zu vermeiden:
    ///
    ///     Klicken Sie im Projektmappen-Explorer mit der rechten Maustaste auf das Zielprojekt und anschließend auf
    ///     "Verweis hinzufügen"->"Projekte"->[Navigieren Sie zu diesem Projekt, und wählen Sie es aus.]
    ///
    ///
    /// Schritt 2)
    /// Fahren Sie fort, und verwenden Sie das Steuerelement in der XAML-Datei.
    ///
    ///     <MyNamespace:SoccerField/>
    ///
    /// </summary>
    public class SoccerField : Canvas
    {
        //public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(SoccerField), (PropertyMetadata)new FrameworkPropertyMetadata((object)null, new PropertyChangedCallback(SoccerField.OnItemsSourceChanged)));
    
        static SoccerField()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SoccerField), new FrameworkPropertyMetadata(typeof(SoccerField)));
            
        }

        public SoccerField()
        {
            Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0x96, 0x00));
            Width = 10400;
            Height = 7400;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            base.MeasureOverride(constraint);
            return new Size(1040, 740);
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            
            double scaleX = ActualWidth / 10400;
            double scaleY = ActualHeight / 7400;

            var scale = Math.Min(scaleX, scaleY);

            drawField(dc, scale);

        }

        private void drawText(DrawingContext dc , double x, double y, string text,

                      Color color)
        {

            FormattedText ft = new FormattedText(text,

              new CultureInfo("en-us"),

              FlowDirection.LeftToRight,

              new Typeface(new FontFamily("Arial"), FontStyles.Normal,

              FontWeights.Normal, new FontStretch()),

              16D,

              Brushes.White);

            dc.DrawText(ft, new Point(x, y));

            // The following does't work because "dc" is not a UIElement

            // canvasObj.Children.Add(dc);

        }

        private void drawField(DrawingContext dc, double scale)
        {
            // field borders and senter line
            dc.DrawRectangle(new SolidColorBrush(), new Pen(new SolidColorBrush(Colors.White), Math.Max(1, 50 * scale)), new Rect(700 * scale, 700 * scale, 9000 * scale, 6000 * scale));
            dc.DrawLine(new Pen(new SolidColorBrush(Colors.White), Math.Max(1, 50 * scale)), new Point(5200 * scale, 700 * scale), new Point(5200 * scale, 6700 * scale));
            //dc.DrawLine(new Pen(new SolidColorBrush(Colors.Red), 1), new Point(5200 * scale, 700 * scale),new Point(5200 * scale, 6700 * scale) );

            // center circle
            dc.DrawEllipse(new SolidColorBrush(), new Pen(new SolidColorBrush(Colors.White), Math.Max(1, 50 * scale)), new Point(5200 * scale, 3700 * scale), 725 * scale, 725 * scale);
            //dc.DrawLine(new Pen(new SolidColorBrush(Colors.Red), 1), new Point(4450 * scale, 700 * scale), new Point(4450 * scale, 6700 * scale));
            //dc.DrawLine(new Pen(new SolidColorBrush(Colors.Red), 1), new Point(5950 * scale, 700 * scale), new Point(5950 * scale, 6700 * scale));

            // center mark
            dc.DrawRectangle(new SolidColorBrush(Colors.White), new Pen(), new Rect(5150*scale, 3675*scale, 100*scale, 50*scale));

            // left penalty area
            dc.DrawRectangle(new SolidColorBrush(), new Pen(new SolidColorBrush(Colors.White), Math.Max(1, 50 * scale)), new Rect(700 * scale, 2600 * scale, 600 * scale, 2200 * scale));
            //dc.DrawLine(new Pen(new SolidColorBrush(Colors.Red), 1), new Point(700 * scale, 2600 * scale), new Point(1500 * scale, 2600 * scale));
            //dc.DrawLine(new Pen(new SolidColorBrush(Colors.Red), 1), new Point(700 * scale, 4800 * scale), new Point(1500 * scale, 4800 * scale));
            //dc.DrawLine(new Pen(new SolidColorBrush(Colors.Red), 1), new Point(1300 * scale, 700 * scale), new Point(1300 * scale, 6700 * scale));

            // left penalty mark
            dc.DrawEllipse(new SolidColorBrush(Colors.White), new Pen(), new Point(2000 * scale, 3700 * scale), Math.Max(1, 50 * scale), Math.Max(1, 50 * scale));

            // right penalty area
            dc.DrawRectangle(new SolidColorBrush(), new Pen(new SolidColorBrush(Colors.White), Math.Max(1, Math.Max(1, 50 * scale))), new Rect(9100 * scale, 2600 * scale, 600 * scale, 2200 * scale));
            //dc.DrawLine(new Pen(new SolidColorBrush(Colors.Red), 1), new Point(9000 * scale, 2600 * scale), new Point(10000 * scale, 2600 * scale));
            //dc.DrawLine(new Pen(new SolidColorBrush(Colors.Red), 1), new Point(9000 * scale, 4800 * scale), new Point(10000 * scale, 4800 * scale));
            //dc.DrawLine(new Pen(new SolidColorBrush(Colors.Red), 1), new Point(9100 * scale, 700 * scale), new Point(9100 * scale, 6700 * scale));

            // right penalty mark
            dc.DrawEllipse(new SolidColorBrush(Colors.White), new Pen(), new Point(8400 * scale, 3700 * scale), Math.Max(1, 50 * scale), Math.Max(1, 50 * scale));


            //// left penalty mark
            //dc.DrawEllipse(new SolidColorBrush(Colors.Orange), new Pen(new SolidColorBrush(Colors.White), 1), new Point(3200 * scale, 2400 * scale), Math.Max(1, 65 * scale), Math.Max(1, 65 * scale));
            //// left penalty mark
            //dc.DrawEllipse(new SolidColorBrush(Colors.DodgerBlue), new Pen(new SolidColorBrush(Colors.White), 1), new Point(2500 * scale, 2400 * scale), Math.Max(1, 150 * scale), Math.Max(1, 150 * scale));
            //drawText(dc, (2500-55) * scale, (2400-65) * scale, "3", Colors.White);
            //dc.DrawEllipse(new SolidColorBrush(Colors.Tomato), new Pen(new SolidColorBrush(Colors.White), 1), new Point(3600 * scale, 2000 * scale), Math.Max(1, 150 * scale), Math.Max(1, 150 * scale));
            //drawText(dc, (3600 -55)* scale, (2000-65) * scale, "2", Colors.White);


            // left goal
            dc.DrawRectangle(new SolidColorBrush(Color.FromArgb(180, 128, 128, 128)), new Pen(new SolidColorBrush(Colors.White), 20 * scale ), new Rect(200 * scale, 2900 * scale, 500 * scale, 1600 * scale));
            dc.DrawRoundedRectangle(new SolidColorBrush(Colors.Gold), new Pen(), new Rect(650 * scale, 2850 * scale, 100 * scale, 1700 * scale), 50 * scale, 50 * scale);
            // right goal
            dc.DrawRectangle(new SolidColorBrush(Color.FromArgb(180, 128, 128, 128)), new Pen(new SolidColorBrush(Colors.White), 20 * scale), new Rect(9700 * scale, 2900 * scale, 500 * scale, 1600 * scale));
            dc.DrawRoundedRectangle(new SolidColorBrush(Colors.Gold), new Pen(), new Rect(9650 * scale, 2850 * scale, 100 * scale, 1700 * scale), 50 * scale, 50 * scale);
        }
    }
}
