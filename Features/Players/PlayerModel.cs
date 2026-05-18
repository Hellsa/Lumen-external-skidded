namespace SkiddingApp.Features
{
    public class PlayerModel
    {
        public string Name { get; set; } = "";
        public string DisplayName { get; set; } = "";
        public string UserId { get; set; } = "";
        public string Status { get; set; } = "";
        public double Health { get; set; }
        public double MaxHealth { get; set; }
        public string HealthText => $"{(int)Health}/{(int)MaxHealth}";
        public string Distance { get; set; } = "";
        public double RawDistance { get; set; }
        public long Address { get; set; }
        public string AddressHex => $"0x{Address:X}";
        public (float x, float y, float z) WorldPosition { get; set; }
        
        public bool IsLocal { get; set; }
        public long Team { get; set; }
        public bool IsSameTeam { get; set; }

        public float ScreenX { get; set; }
        public float ScreenY { get; set; }
        public bool IsVisibleOnScreen { get; set; }
    }
}
