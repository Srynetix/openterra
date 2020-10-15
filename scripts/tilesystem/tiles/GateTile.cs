namespace Tiles
{
    public class GateTile : StaticTile
    {
        public KeyColorEnum KeyColor;

        public GateTile()
        {
            TileLayer = TileLayerEnum.Background;
            IsGate = true;
        }

        public override bool CanBePassedThrough(Tile source, Direction direction)
        {
            var neighbor = World.GetNeighborTile(this, direction);
            if (neighbor == null)
            {
                return source.Player && World.PInventory.HasKeyColor(KeyColor);
            }
            else
            {
                return false;
            }
        }
    }
}
