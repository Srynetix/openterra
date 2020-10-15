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
                "SmallGems" => new GemsInDirtTile(),
                "Egg" => new EggTile(),
                "Dirt" => new DirtTile(),
                "Wall" => new WallTile(),
                "Dynamite" => new DynamiteTile(),
                "Hint" => new HintTile(),
                "MarbleFloor" => new MarbleFloorTile(),
                "RoundedWall" => new RoundedWallTile(),
                "Bomb" => new BombTile(),
                "SteelWall" => new SteelWallTile(),
                "Explosion" => new ExplosionTile(),
                // Barriers
                "ActiveBlueBarrier" => new BarrierTile() { Active = true, BarrierColor = Tile.BarrierColorEnum.Blue },
                "DisabledBlueBarrier" => new BarrierTile() { Active = false, BarrierColor = Tile.BarrierColorEnum.Blue },
                "ActivePinkBarrier" => new BarrierTile() { Active = true, BarrierColor = Tile.BarrierColorEnum.Pink },
                "DisabledPinkBarrier" => new BarrierTile() { Active = false, BarrierColor = Tile.BarrierColorEnum.Pink },
                // Keys
                "GreenKey" => new KeyTile() { KeyColor = KeyTile.KeyColorEnum.Green },
                "YellowKey" => new KeyTile() { KeyColor = KeyTile.KeyColorEnum.Yellow },
                "RedKey" => new KeyTile() { KeyColor = KeyTile.KeyColorEnum.Red },
                "BlueKey" => new KeyTile() { KeyColor = KeyTile.KeyColorEnum.Blue },
                // Doors
                "GreenDoor" => new DoorTile() { KeyColor = KeyTile.KeyColorEnum.Green },
                "YellowDoor" => new DoorTile() { KeyColor = KeyTile.KeyColorEnum.Yellow },
                "RedDoor" => new DoorTile() { KeyColor = KeyTile.KeyColorEnum.Red },
                "BlueDoor" => new DoorTile() { KeyColor = KeyTile.KeyColorEnum.Blue },
                // Gates
                "GreenGate" => new GateTile() { KeyColor = KeyTile.KeyColorEnum.Green },
                "YellowGate" => new GateTile() { KeyColor = KeyTile.KeyColorEnum.Yellow },
                "RedGate" => new GateTile() { KeyColor = KeyTile.KeyColorEnum.Red },
                "BlueGate" => new GateTile() { KeyColor = KeyTile.KeyColorEnum.Blue },
                // Crates
                "Crate" => new CrateTile(),
                "CrateLR" => new CrateTile() { PushDirectionLock = Tile.PushDirectionLockEnum.Horizontal },
                "CrateUD" => new CrateTile() { PushDirectionLock = Tile.PushDirectionLockEnum.Vertical },
                // Ports
                "PortUp" => new PortTile() { PortDirection = Tile.Direction.Up },
                "PortDown" => new PortTile() { PortDirection = Tile.Direction.Down },
                "PortLeft" => new PortTile() { PortDirection = Tile.Direction.Left },
                "PortRight" => new PortTile() { PortDirection = Tile.Direction.Right },
                "PortHorizontal" => new TwoWayPortTile() { Horizontal = true },
                "PortVertical" => new TwoWayPortTile() { Horizontal = false },
                "Port4Way" => new FourWayPortTile(),
                // Walls
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
