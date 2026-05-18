using System.Windows;
using System.Windows.Media;

namespace SkiddingApp.Features.Modules
{
    public static class BoxESP
    {
        public static void Draw(DrawingContext dc, double x, double y, double w, double h, Pen pen, Brush? fill)
        {
            if (fill != null)
            {
                var transFill = fill.Clone();
                transFill.Opacity = 0.2;
                dc.DrawRectangle(transFill, pen, new Rect(x, y, w, h));
            }
            else
            {
                dc.DrawRectangle(null, pen, new Rect(x, y, w, h));
            }
        }
    }
}
