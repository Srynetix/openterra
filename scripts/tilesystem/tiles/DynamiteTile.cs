using Godot;

namespace Tiles {
    public class DynamiteTile: StaticTile {
        public bool Armed;
        public int ExplodeAtTick = -1;

        public override void Step() {
            if (Armed && ExplodeAtTick == World.GameTicks) {
                // BOOM
                World.SpawnExplosionAtTile(this);
            }
        }
    }
}
