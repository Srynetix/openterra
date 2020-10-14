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
            if (fgTile?.Warpable == true && fgTile.WarpTarget == null && !fgTile.WillExplodeSoon())
            {
                var tWrap = GetTargetWarp(fgTile.NextDirection);
                if (tWrap != null)
                {
                    // Send to target warp
                    fgTile.WillWarpTo(tWrap, fgTile.NextDirection);
                }
                else
                {
                    // Destroy or explode
                    if (fgTile.CanExplode)
                    {
                        fgTile.WillExplode();
                    }
                    else
                    {
                        fgTile.Pick();
                    }
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
            var nextTilePosition = World.GetTileCurrentGridPosition(this);
            while (true)
            {
                WarpTile nextTile = (WarpTile)World.ScanNextTileOfType(nextTilePosition, Type);
                nextTilePosition = World.GetTileCurrentGridPosition(nextTile);
                if (nextTile == this)
                {
                    break;
                }

                var collisionStatus = World.GetTileCollisions(nextTile);
                if (!nextTile.HasNeighbor(collisionStatus, direction))
                {
                    continue;
                }

                return nextTile;
            }

            var thisCollisionStatus = World.GetTileCollisions(this);
            if (HasNeighbor(thisCollisionStatus, direction))
            {
                return this;
            }

            return null;
        }
    }
}
