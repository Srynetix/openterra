namespace Tiles
{
    public class QuicksandTile : StaticTile
    {
        public QuicksandTile()
        {
            TileLayer = TileLayerEnum.Foreground;
            ZIndex = 2;
        }

        private Tile GetTrappedTile()
        {
            return GetOverlappingTile(TileLayerEnum.Middle);
        }

        public override bool CanBePassedThrough(Tile source, Direction direction)
        {
            // Only accept keys and rocks
            return (source is KeyTile || source is RockTile) && GetTrappedTile() == null;
        }

        public override void Step()
        {
            var tile = GetTrappedTile();
            if (tile?.TrappedInSand == false)
            {
                tile.TrappedInSand = true;
                tile.Stop();
            }
            else if (tile?.TrappedInSand == true)
            {
                // Check neighbor
                var n = tile.GetNeighborAtDirection(Direction.Down);
                if (n?.CanBePassedThrough(tile, Direction.Down) != false)
                {
                    tile.TrappedInSand = false;
                    tile.WillMoveTowards(Direction.Down);
                }
            }
        }
    }
}
