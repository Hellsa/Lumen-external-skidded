using System.Windows.Media;

namespace SkiddingApp.Features.Modules
{
    public static class NameESP
    {
        public static void Draw(DrawingContext dc, double x, double y, double w, string name, Color color, Typeface typeface, double fontSize = 11)
        {
            var dim = TextESP.GetDimensions(name, typeface, fontSize);
            double textX = x + (w / 2) - (dim.width / 2);
            double textY = y - dim.height - 2;

            TextESP.DrawTextWithOutline(dc, name, typeface, fontSize, color, textX, textY);
        }
    }
}
