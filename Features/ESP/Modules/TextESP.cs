using System.Windows;
using System.Windows.Media;

namespace SkiddingApp.Features.Modules
{
    public static class TextESP
    {
        public static void DrawTextWithOutline(DrawingContext dc, string text, Typeface typeface, double fontSize, Color color, double x, double y)
        {
            FormattedText formattedText = new FormattedText(
                text,
                System.Globalization.CultureInfo.InvariantCulture,
                FlowDirection.LeftToRight,
                typeface,
                fontSize,
                new SolidColorBrush(color),
                1.25);

            FormattedText outlineText = new FormattedText(
                text,
                System.Globalization.CultureInfo.InvariantCulture,
                FlowDirection.LeftToRight,
                typeface,
                fontSize,
                Brushes.Black,
                1.25);

            dc.DrawText(outlineText, new Point(x - 1, y));
            dc.DrawText(outlineText, new Point(x + 1, y));
            dc.DrawText(outlineText, new Point(x, y - 1));
            dc.DrawText(outlineText, new Point(x, y + 1));
            dc.DrawText(formattedText, new Point(x, y));
        }
        
        public static (double width, double height) GetDimensions(string text, Typeface typeface, double fontSize)
        {
            FormattedText formattedText = new FormattedText(
                text,
                System.Globalization.CultureInfo.InvariantCulture,
                FlowDirection.LeftToRight,
                typeface,
                fontSize,
                Brushes.White,
                1.25);
            return (formattedText.Width, formattedText.Height);
        }
    }
}
