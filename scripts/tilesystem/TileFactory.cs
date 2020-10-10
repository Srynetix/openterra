using Godot;

namespace Tiles
{
    public static class TileFactory
    {
        public static Tile CreateTileFromName(string name)
        {
            return name switch
            {
                "Player1" => new PlayerTile(),
                "Player2" => new PlayerTile(),
                "Rock" => new RockTile(),
                "Ruby" => new RubyTile(),
                "Emerald" => new EmeraldTile(),
                "Diamond" => new DiamondTile(),
                "Egg" => new EggTile(),
                "Dirt" => new DirtTile(),
                "Wall" => new WallTile(),
                "Hint" => new HintTile(),
                "MarbleFloor" => new MarbleFloorTile(),
                "RoundedWall" => new RoundedWallTile(),
                "Bomb" => new BombTile(),
                "SteelWall" => new SteelWallTile(),
                "Explosion" => new ExplosionTile(),
                "ExtendingWallLR" => new ExtendingWallTile
                {
                    ExtensionDirection = ExtendingWallTile.ExtensionDirectionEnum.LeftRight
                },
                "ExtendingWallUD" => new ExtendingWallTile
                {
                    ExtensionDirection = ExtendingWallTile.ExtensionDirectionEnum.UpDown
                },
                "ExtendingWall4Way" => new ExtendingWallTile
                {
                    ExtensionDirection = ExtendingWallTile.ExtensionDirectionEnum.FourWay
                },
                "LeftRoundedWall" => new RoundedWallTile
                {
                    RollDirection = Tile.RollDirectionEnum.Left
                },
                "RightRoundedWall" => new RoundedWallTile
                {
                    RollDirection = Tile.RollDirectionEnum.Right
                },
                "Slimey" => new SlimeyTile(),
                "Elevator" => new ElevatorTile(),
                "Warp" => new WarpTile(),
                "Save" => new SaveTile(),
                _ => new StaticTile(),
            };
        }
    }
}
