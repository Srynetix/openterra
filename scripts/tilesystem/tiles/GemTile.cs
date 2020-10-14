using Godot;

namespace Tiles
{
    public class GemTile : FallingTile
    {
        public int Value;

        public GemTile()
        {
            Pickable = true;
        }
    }

    public class RubyTile : GemTile
    {
        public RubyTile()
        {
            Value = 5;
        }
    }

    public class EmeraldTile : GemTile
    {
        public EmeraldTile()
        {
            IsFragile = true;
            Value = 3;
        }
    }

    public class DiamondTile : GemTile
    {
        public DiamondTile()
        {
            Value = 1;
        }
    }
}
