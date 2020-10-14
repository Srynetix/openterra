namespace Tiles
{
    public class DoorTile : StaticTile
    {
        public KeyColorEnum KeyColor;
        private bool _opened;

        public DoorTile()
        {
            Pickable = true;
        }

        public override bool CanBePicked()
        {
            if (_opened) return true;

            if (World.PInventory.HasKeyColor(KeyColor))
            {
                World.PInventory.UnsetKeyColor(KeyColor);
                _opened = true;
                return true;
            }

            return false;
        }
    }
}
