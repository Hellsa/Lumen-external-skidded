using SkiddingApp;
using System;

namespace SkiddingApp.Features
{
    public class ServerInfo
    {
        public long PlaceId { get; set; }
        public long GameId { get; set; }
        public string JobId { get; set; } = "";
        public string ServerIP { get; set; } = "";
        public bool IsLoaded { get; set; }
    }

    public class ServerMonitor
    {
        private MemoryManager memory;

        public ServerMonitor(MemoryManager mem)
        {
            this.memory = mem;
        }

        public ServerInfo GetInfo()
        {
            long dm = memory.GetDataModel();
            if (dm == 0) return new ServerInfo();

            var gameLoadedBytes = memory.ReadBytes(dm + Offsets.DataModel.GameLoaded, 1);
            bool isLoaded = gameLoadedBytes.Length > 0 && gameLoadedBytes[0] != 0;

            return new ServerInfo
            {
                PlaceId = memory.ReadInt64(dm + Offsets.DataModel.PlaceId),
                GameId = memory.ReadInt64(dm + Offsets.DataModel.GameId),
                IsLoaded = isLoaded,
                JobId = memory.ReadString(dm + Offsets.DataModel.JobId),
                ServerIP = memory.ReadString(dm + Offsets.DataModel.ServerIP)
            };
        }
    }
}
