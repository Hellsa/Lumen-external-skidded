using SkiddingApp;
using System;

namespace SkiddingApp.Features
{
    public class Teleport
    {
        private MemoryManager memory;

        public Teleport(MemoryManager mem)
        {
            this.memory = mem;
        }

        public void ToPlayer(long targetPlayer)
        {
            long dm = memory.GetDataModel();
            long players = memory.FindService(dm, "Players");
            if (players == 0) return;

            long local = memory.ReadInt64(players + Offsets.Player.LocalPlayer);
            if (local == 0 || targetPlayer == 0) return;

            long lChar = memory.ReadInt64(local + Offsets.Player.ModelInstance);
            long tChar = memory.ReadInt64(targetPlayer + Offsets.Player.ModelInstance);

            if (memory.TryGetRootPrimitive(lChar, out long lPrim) && memory.TryGetRootPrimitive(tChar, out long tPrim))
            {
                var targetPos = memory.ReadVector3(tPrim + Offsets.Primitive.Position);
                // Set local position with a slight offset to avoid getting stuck
                memory.WriteVector3(lPrim + Offsets.Primitive.Position, targetPos.x, targetPos.y + 5.0f, targetPos.z);
            }
        }

        public void ToPosition((float x, float y, float z) pos)
        {
            long dm = memory.GetDataModel();
            long players = memory.FindService(dm, "Players");
            if (players == 0) return;

            long local = memory.ReadInt64(players + Offsets.Player.LocalPlayer);
            if (local == 0) return;

            long lChar = memory.ReadInt64(local + Offsets.Player.ModelInstance);
            if (memory.TryGetRootPrimitive(lChar, out long lPrim))
            {
                memory.WriteVector3(lPrim + Offsets.Primitive.Position, pos.x, pos.y, pos.z);
            }
        }
    }
}
