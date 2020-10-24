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
                "Bomb" => new BombTile(),
                "SteelWall" => new SteelWallTile(),
                "Explosion" => new ExplosionTile(),
                // Barriers
                "ActiveBlueBarrier" => new BarrierTile() { Active = true, BarrierColor = BarrierColorEnum.Blue },
                "DisabledBlueBarrier" => new BarrierTile() { Active = false, BarrierColor = BarrierColorEnum.Blue },
                "ActivePinkBarrier" => new BarrierTile() { Active = true, BarrierColor = BarrierColorEnum.Pink },
                "DisabledPinkBarrier" => new BarrierTile() { Active = false, BarrierColor = BarrierColorEnum.Pink },
                "BlueBarrierSwitch" => new BarrierSwitchTile() { BarrierColor = BarrierColorEnum.Blue },
                "PinkBarrierSwitch" => new BarrierSwitchTile() { BarrierColor = BarrierColorEnum.Pink },
                // Keys
                "GreenKey" => new KeyTile() { KeyColor = KeyColorEnum.Green },
                "YellowKey" => new KeyTile() { KeyColor = KeyColorEnum.Yellow },
                "RedKey" => new KeyTile() { KeyColor = KeyColorEnum.Red },
                "BlueKey" => new KeyTile() { KeyColor = KeyColorEnum.Blue },
                // Doors
                "GreenDoor" => new DoorTile() { KeyColor = KeyColorEnum.Green },
                "YellowDoor" => new DoorTile() { KeyColor = KeyColorEnum.Yellow },
                "RedDoor" => new DoorTile() { KeyColor = KeyColorEnum.Red },
                "BlueDoor" => new DoorTile() { KeyColor = KeyColorEnum.Blue },
                // Gates
                "GreenGate" => new GateTile() { KeyColor = KeyColorEnum.Green },
                "YellowGate" => new GateTile() { KeyColor = KeyColorEnum.Yellow },
                "RedGate" => new GateTile() { KeyColor = KeyColorEnum.Red },
                "BlueGate" => new GateTile() { KeyColor = KeyColorEnum.Blue },
                // Crates
                "Crate" => new CrateTile(),
                "CrateLR" => new CrateTile() { PushDirectionLock = PushDirectionLockEnum.Horizontal },
                "CrateUD" => new CrateTile() { PushDirectionLock = PushDirectionLockEnum.Vertical },
                // Ports
                "PortUp" => new PortTile() { PortDirection = Direction.Up },
                "PortDown" => new PortTile() { PortDirection = Direction.Down },
                "PortLeft" => new PortTile() { PortDirection = Direction.Left },
                "PortRight" => new PortTile() { PortDirection = Direction.Right },
                "PortHorizontal" => new TwoWayPortTile() { Horizontal = true },
                "PortVertical" => new TwoWayPortTile() { Horizontal = false },
                "Port4Way" => new FourWayPortTile(),
                // Extending wall
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
                // Walls
                "OneTimePassage" => new OneTimePassageTile(),
                "FragileWall" => new FragileWallTile(),
                "StarBlock" => new StarBlockTile(),
                "TopRoundedWall" => new RoundedWallTile(),
                "TopLeftRoundedWall" => new RoundedWallTile
                {
                    RollDirection = RollDirectionEnum.Left
                },
                "TopRightRoundedWall" => new RoundedWallTile
                {
                    RollDirection = RollDirectionEnum.Right
                },
                "Quicksand" => new QuicksandTile(),
                "Flowstone" => new FlowstoneTile(),
                // Exits
                "ExitClosed" => new ExitTile { ExitType = ExitTypeEnum.Normal },
                "HardExitClosed" => new ExitTile { ExitType = ExitTypeEnum.Hard },
                "Slimey" => new SlimeyTile(),
                "Elevator" => new ElevatorTile(),
                "Warp" => new WarpTile(),
                "Save" => new SaveTile(),
                _ => new StaticTile(),
            };
        }
    }
}
