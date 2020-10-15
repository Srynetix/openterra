namespace Tiles
{
    public class BarrierTile : StaticTile
    {
        public bool Active;
        public BarrierColorEnum BarrierColor;

        public BarrierTile()
        {
            Background = true;
            Indestructible = true;
            ZIndex = 0;
        }

        public override bool CanBePassedThrough(Tile source, Direction direction)
        {
            return !Active;
        }
    }
}
