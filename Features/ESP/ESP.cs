using SkiddingApp;
using System;
using System.Collections.Generic;

namespace SkiddingApp.Features
{
    public class ESPSettings
    {
        // General
        public bool Enabled { get; set; } = true;
        public bool TeamCheck { get; set; } = true;
        public bool KnockCheck { get; set; } = false;

        // Colors
        public System.Windows.Media.Color FriendColor { get; set; } = System.Windows.Media.Color.FromRgb(33, 150, 243); // Blue
        public System.Windows.Media.Color EnemyColor { get; set; } = System.Windows.Media.Color.FromRgb(244, 67, 54); // Red

        // Bounding Box
        public bool Boxes { get; set; } = true;
        public bool FillBackground { get; set; } = false;
        public string BoxStyle { get; set; } = "Full";

        // Health Bar
        public bool HealthBar { get; set; } = true;
        public System.Windows.Media.Color HighHealthColor { get; set; } = System.Windows.Media.Color.FromRgb(76, 175, 80); // Green
        public System.Windows.Media.Color LowHealthColor { get; set; } = System.Windows.Media.Color.FromRgb(244, 67, 54); // Red

        // Player Names
        public bool Names { get; set; } = true;
        public string NameType { get; set; } = "Username";
        public System.Windows.Media.Color NameColor { get; set; } = System.Windows.Media.Colors.White;

        // Distance
        public bool Distance { get; set; } = true;
        public System.Windows.Media.Color DistanceColor { get; set; } = System.Windows.Media.Color.FromRgb(170, 170, 170); // Gray
    }

    public class ESP
    {
        private MemoryManager memory;

        public ESP(MemoryManager mem)
        {
            this.memory = mem;
        }

        public void ProcessESP(ESPSettings settings, List<PlayerModel> players, double screenWidth, double screenHeight)
        {
            if (!settings.Enabled) return;

            float[] matrix = memory.GetViewMatrix();

            foreach (var p in players)
            {
                if (p.IsLocal) continue;
                if (p.Health <= 0) continue;
                if (settings.TeamCheck && p.IsSameTeam) continue;

                var screenPos = memory.WorldToScreen(p.WorldPosition, matrix, screenWidth, screenHeight);
                if (screenPos.visible)
                {
                    p.ScreenX = screenPos.x;
                    p.ScreenY = screenPos.y;
                    p.IsVisibleOnScreen = true;
                }
                else
                {
                    p.IsVisibleOnScreen = false;
                }
            }
        }
    }
}
