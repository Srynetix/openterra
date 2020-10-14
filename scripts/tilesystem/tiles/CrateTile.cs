using Godot;

namespace Tiles
{
    public class CrateTile : ControlledTile
    {
        public CrateTile()
        {
            Movable = true;
            Warpable = true;
            IsLightweight = true;
        }
    }
}
