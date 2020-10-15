namespace Tiles
{
    public class GemTile : FallingTile
    {
        public int Value;

        public GemTile()
        {
            Pickable = true;
        }

        public override void BeforePick()
        {
            World.PInventory.AddGems(Value);
        }
    }

    public class GemsInDirtTile : StaticTile
    {
        public GemsInDirtTile()
        {
            Pickable = true;
        }

        public override void BeforePick()
        {
            World.PInventory.AddGems(2);
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
