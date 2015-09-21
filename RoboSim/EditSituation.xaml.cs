using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RoboSim.converter;
using RoboSim.viewModels;

namespace RoboSim
{
    /// <summary>
    /// Interaktionslogik für EditSituation.xaml
    /// </summary>
    public partial class EditSituation : UserControl
    {
        private XPositionConverter xPlayerPosition;
        private YPositionConverter yPlayerPosition;

        public EditSituation()
        {
            InitializeComponent();
            xPlayerPosition = new XPositionConverter { Width = 350 };
            yPlayerPosition = new YPositionConverter { Height = 350 };
        }

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Thumb thumb = e.Source as Thumb;

            var fieldObject = thumb.DataContext as FieldObjectViewModel;

            if (fieldObject != null)
            {
                fieldObject.X = Math.Round((double)xPlayerPosition.ConvertBack(Canvas.GetLeft(thumb) + e.HorizontalChange, null, null, null));
                fieldObject.Y = Math.Round((double)yPlayerPosition.ConvertBack(Canvas.GetTop(thumb) + e.VerticalChange, null, null, null));
            }
        }
    }
}
