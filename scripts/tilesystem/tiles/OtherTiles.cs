namespace Tiles {
        public class StaticTile : Tile
    {
        public StaticTile()
        {
            Movable = false;
            Warpable = false;
        }
    }

    public class ControlledTile : Tile
    {
        public ControlledTile()
        {
            Controlled = true;
        }
    }

    public class BackgroundTile : StaticTile
    {
        public BackgroundTile()
        {
            TileLayer = TileLayerEnum.Background;
            ZIndex = 0;
            PassthroughMode = PassthroughModeEnum.All;
        }
    }

    public class SaveTile : ControlledTile
    {
        public SaveTile()
        {
            Pickable = true;
        }
    }

    public class HintTile : BackgroundTile { }

    public class RockTile : FallingTile
    {
        public RockTile()
        {
            IsHeavy = true;
        }
    }

    public class BombTile : FallingTile
    {
        public BombTile()
        {
            CanExplode = true;
        }
    }

    public class EggTile : FallingTile { }

    public class DirtTile : StaticTile
    {
        public DirtTile()
        {
            Pickable = true;
        }
    }

    public class MarbleFloorTile : BackgroundTile
    {
        public MarbleFloorTile()
        {
            Indestructible = true;
            PassthroughMode = PassthroughModeEnum.PlayerOnly;
        }
    }

    public class SlimeyTile : ControlledTile { }
}
