using Godot;

namespace Tiles
{
    public class KeyTile : FallingTile
    {
        public enum KeyColorEnum
        {
            Green,
            Yellow,
            Blue,
            Red
        }

        public KeyColorEnum KeyColor;

        public KeyTile()
        {
            IsLightweight = true;
        }
    }
}
