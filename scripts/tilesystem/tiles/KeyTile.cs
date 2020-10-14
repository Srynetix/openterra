namespace Tiles
{
    public class KeyTile : FallingTile
    {
        public KeyColorEnum KeyColor;

        public KeyTile()
        {
            IsLightweight = true;
            Pickable = true;
        }

        public override void BeforePick()
        {
            World.PInventory.SetKeyColor(KeyColor);
        }

        public override bool CanBePicked()
        {
            return !World.PInventory.HasKeyColor(KeyColor);
        }
    }
}
