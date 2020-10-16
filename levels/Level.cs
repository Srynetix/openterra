using Godot;

public class Level : TileMap
{
    [Export] public int TimeLimit = -1;
    [Export] public int GemsForNormalExit = 5;
    [Export] public int GemsForHardExit = 10;
}
