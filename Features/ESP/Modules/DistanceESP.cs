using System.Windows.Media;

namespace SkiddingApp.Features.Modules
{
    public static class DistanceESP
    {
        public static void Draw(DrawingContext dc, double x, double y, double w, double h, string distance, Color color, Typeface typeface, double fontSize = 10)
        {
            var dim = TextESP.GetDimensions(distance, typeface, fontSize);
            double textX = x + (w / 2) - (dim.width / 2);
            double textY = y + h + 2;

            TextESP.DrawTextWithOutline(dc, distance, typeface, fontSize, color, textX, textY);
        }
    }
}
