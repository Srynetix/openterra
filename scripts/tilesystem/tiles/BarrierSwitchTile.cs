using Godot;

namespace Tiles
{
    public class BarrierSwitchTile : StaticTile
    {
        public BarrierColorEnum BarrierColor;

        public int _lastSwitchTick = -1;
        public Tile _lastSwitchTile;

        public BarrierSwitchTile()
        {
            TileLayer = TileLayerEnum.Background;
            ZIndex = 0;
        }

        public override bool CanBePassedThrough(Tile source, Direction direction)
        {
            return true;
        }

        public override void Step()
        {
            var fgTile = GetOverlappingTile(TileLayerEnum.Middle);
            if (fgTile != null)
            {
                // Check toggle loop
                if (_lastSwitchTick == World.GameTicks - 1 && _lastSwitchTile == fgTile)
                {
                    _lastSwitchTick = World.GameTicks;
                    return;
                }

                World.ToggleBarriersState(BarrierColor);

                // Update tick & tile
                _lastSwitchTick = World.GameTicks;
                _lastSwitchTile = fgTile;

                var n = GetNeighborAtDirection(fgTile.NextDirection);
                if (n?.CanBePassedThrough(fgTile, fgTile.NextDirection) != false)
                {
                    fgTile.MoveTowards(fgTile.NextDirection);
                }
                else
                {
                    fgTile.MoveTowards(TileUtils.GetInvertedDirection(fgTile.NextDirection));
                }
            }
        }
    }
}
