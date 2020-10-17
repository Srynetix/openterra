using Godot;

namespace Tiles
{
    public class WarpTile : BackgroundTile
    {
        private float _t;

        public WarpTile()
        {
            TileLayer = TileLayerEnum.Background;
            IsWarp = true;
        }

        public override void _Ready()
        {
            base._Ready();
            AddToGroup("warps");
        }

        public override void Step()
        {
            // Get fg tile
            var fgTile = GetOverlappingTile(TileLayerEnum.Middle);
            if (fgTile?.Warpable == true && fgTile.WarpTarget == null && !fgTile.WillExplodeSoon())
            {
                var tWrap = GetTargetWarp(fgTile, fgTile.NextDirection);
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
                        fgTile.Explode();
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

        public WarpTile GetTargetWarp(Tile source, Direction direction)
        {
            var nextTilePosition = TilePosition;
            while (true)
            {
                WarpTile nextTile = (WarpTile)World.ScanNextTileOfType(nextTilePosition, Type);
                nextTilePosition = nextTile.TilePosition;
                if (nextTile == this)
                {
                    break;
                }

                var neighborTile = nextTile.GetNeighborAtDirection(direction);
                if (neighborTile?.CanBePassedThrough(source, direction) == false)
                {
                    // If neighbor can not be passed through, continue
                    continue;
                }

                return nextTile;
            }

            var thisNeighborTile = GetNeighborAtDirection(direction);
            if (thisNeighborTile?.CanBePassedThrough(source, direction) != false)
            {
                // Select this warp if accessible
                return this;
            }

            return null;
        }
    }
}
