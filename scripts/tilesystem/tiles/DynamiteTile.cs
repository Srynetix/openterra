namespace Tiles
{
    public class DynamiteTile : StaticTile
    {
        public bool Active;

        public DynamiteTile()
        {
            CanExplode = true;
        }

        public override bool CanBePicked()
        {
            return !Active;
        }
    }
}
