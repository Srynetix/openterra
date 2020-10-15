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
            Background = true;
            ZIndex = 0;
        }

        public override bool CanBePassedThrough(Tile source, Direction direction)
        {
            return true;
        }

        public override void Step()
        {
            base.Step();
            var thisPosition = World.GetTileCurrentGridPosition(this);
            var fgTile = World.GetTileAtGridPosition(thisPosition, TilePickEnum.ForegroundOnly);
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

                var n = World.GetNeighborTile(this, fgTile.NextDirection);
                if (n?.CanBePassedThrough(fgTile, fgTile.NextDirection) != false)
                {
                    fgTile.MoveTowards(fgTile.NextDirection);
                }
                else
                {
                    fgTile.MoveTowards(World.GetInvertedDirection(fgTile.NextDirection));
                }
            }
        }
    }
}
