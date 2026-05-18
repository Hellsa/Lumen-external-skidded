using System.Windows;
using System.Windows.Media;

namespace SkiddingApp.Features.Modules
{
    public static class HealthESP
    {
        public static void Draw(DrawingContext dc, double x, double y, double h, double health, double maxHealth, Color highColor, Color lowColor)
        {
            double healthPercent = maxHealth > 0 ? health / maxHealth : 0;
            if (healthPercent > 1) healthPercent = 1;
            if (healthPercent < 0) healthPercent = 0;

            double healthBarHeight = h * healthPercent;
            double barX = x - 6;

            Color healthColor = Color.FromRgb(
                (byte)(lowColor.R + (highColor.R - lowColor.R) * healthPercent),
                (byte)(lowColor.G + (highColor.G - lowColor.G) * healthPercent),
                (byte)(lowColor.B + (highColor.B - lowColor.B) * healthPercent)
            );

            // Background bar
            dc.DrawRectangle(Brushes.Black, null, new Rect(barX, y, 4, h));
            // Foreground bar
            dc.DrawRectangle(new SolidColorBrush(healthColor), null, new Rect(barX + 1, y + (h - healthBarHeight) + 1, 2, healthBarHeight - 2));
        }
    }
}
