using SkiddingApp;
using System;
using System.Collections.Generic;

namespace SkiddingApp.Features
{
    public class PlayerManager
    {
        private MemoryManager memory;
        public long LocalPlayerInstance { get; private set; }

        public PlayerManager(MemoryManager mem)
        {
            this.memory = mem;
        }

        private long GetPlayersService(long dataModel)
        {
            var children = memory.GetChildren(dataModel);
            foreach (var child in children)
            {
                long localPlayer = memory.ReadInt64(child + Offsets.Player.LocalPlayer);
                if (localPlayer != 0)
                {
                    long parent = memory.ReadInt64(localPlayer + Offsets.Instance.Parent);
                    if (parent == child)
                    {
                        return child;
                    }
                }
            }
            return 0;
        }

        public List<PlayerModel> UpdatePlayers()
        {
            var list = new List<PlayerModel>();
            long dm = memory.GetDataModel();
            if (dm == 0) return list;

            long playersSvc = GetPlayersService(dm);
            if (playersSvc == 0) return list;

            LocalPlayerInstance = memory.ReadInt64(playersSvc + Offsets.Player.LocalPlayer);
            (float x, float y, float z) lpPos = (0, 0, 0);
            bool lpValid = false;
            long lpTeam = 0;

            if (LocalPlayerInstance != 0)
            {
                long charInst = memory.ReadInt64(LocalPlayerInstance + Offsets.Player.ModelInstance);
                lpTeam = memory.ReadInt64(LocalPlayerInstance + Offsets.Player.Team);

                if (memory.TryGetRootPrimitive(charInst, out long prim))
                {
                    lpPos = memory.ReadVector3(prim + Offsets.Primitive.Position);
                    lpValid = true;
                }
            }

            foreach (var p in memory.GetChildren(playersSvc))
            {
                string name = memory.ReadString(p + Offsets.Instance.Name);
                if (string.IsNullOrEmpty(name)) continue;

                string disp = memory.ReadString(p + Offsets.Player.DisplayName);
                long charInst = memory.ReadInt64(p + Offsets.Player.ModelInstance);
                var stats = memory.GetHealth(charInst);
                long team = memory.ReadInt64(p + Offsets.Player.Team);

                double dist = -1;
                (float x, float y, float z) pPos = (0, 0, 0);

                if (memory.TryGetRootPrimitive(charInst, out long prim))
                {
                    pPos = memory.ReadVector3(prim + Offsets.Primitive.Position);
                    if (lpValid)
                    {
                        dist = Math.Sqrt(Math.Pow(pPos.x - lpPos.x, 2) + Math.Pow(pPos.y - lpPos.y, 2) + Math.Pow(pPos.z - lpPos.z, 2));
                    }
                }

                list.Add(new PlayerModel
                {
                    Name = name,
                    DisplayName = string.IsNullOrEmpty(disp) ? name : disp,
                    UserId = "@" + memory.ReadInt64(p + Offsets.Player.UserId),
                    Health = stats.health,
                    MaxHealth = stats.maxHealth,
                    Distance = dist >= 0 ? $"{(int)(dist * 0.28)}m" : "---",
                    RawDistance = dist,
                    Address = p,
                    Status = "Active",
                    WorldPosition = pPos,
                    IsLocal = (p == LocalPlayerInstance),
                    Team = team,
                    IsSameTeam = (team != 0 && team == lpTeam)
                });
            }
            return list;
        }

        public bool AreInSameTeam(long p1, long p2)
        {
            long t1 = memory.ReadInt64(p1 + Offsets.Player.Team);
            long t2 = memory.ReadInt64(p2 + Offsets.Player.Team);
            return (t1 != 0 && t1 == t2);
        }
    }
}
