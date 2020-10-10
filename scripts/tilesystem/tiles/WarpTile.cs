using Godot;

namespace Tiles
{
    public class WarpTile : BackgroundTile
    {
        private float _t;

        public WarpTile()
        {
            Background = true;
            IsWarp = true;
        }

        public override void _Ready()
        {
            base._Ready();
            AddToGroup("warps");
        }

        public override void Step()
        {
            base.Step();

            // Get fg tile
            var thisPosition = World.GetTileCurrentGridPosition(this);
            var fgTile = World.GetTileAtGridPosition(thisPosition, TilePickEnum.ForegroundOnly);
            if (fgTile != null)
            {
                var tWrap = GetTargetWarp(fgTile.NextDirection);
                if (tWrap != null)
                {
                    // Send to target warp
                    fgTile.WillWarpTo(tWrap);
                    fgTile.Updated = true;
                }
                else
                {
                    // Go back
                    var invertedDirection = fgTile.GetInvertedDirection();
                    fgTile.WillMoveTowards(invertedDirection);
                    fgTile.Updated = true;
                }
            }
        }

        public override void _Process(float delta)
        {
            _t = Mathf.Wrap(_t + delta, 0, 1);
            Modulate = Colors.White.LinearInterpolate(Colors.LightBlue, _t);
        }

        public WarpTile GetTargetWarp(Direction direction)
        {
            var tiles = GetTree().GetNodesInGroup("warps");
            var minPosition = float.MaxValue;
            WarpTile minTile = null;

            foreach (WarpTile tile in tiles)
            {
                if (tile == this)
                {
                    // Ignore self
                    continue;
                }

                var tileStatus = World.GetTileCollisions(tile);
                if (!tile.CanGoTowards(tileStatus, direction))
                {
                    continue;
                }

                var dist = Position.DistanceSquaredTo(tile.Position);
                if (dist < minPosition)
                {
                    minPosition = dist;
                    minTile = tile;
                }
            }

            return minTile;
        }
    }
}
