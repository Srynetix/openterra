namespace Tiles {
    public class FlowstoneTile: ControlledTile {
        public override void Step() {
            if (World.GameTicks > 0 && World.GameTicks % 16 == 0) {
                Grow();
            }
        }

        private void Grow() {
            var d = GetRandomDirection();
            var pos = World.GetNeighborPosition(this, d);
            var n = GetNeighborAtDirection(d);
            if (n is FlowstoneTile || n is WallTile || n is PlayerTile) {
                return;
            }

            if (n?.Indestructible == true) {
                return;
            }

            n?.Pick();
            World.CreateTile("Flowstone", pos);
        }
    }
}
