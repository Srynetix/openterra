using Godot;
using Tiles;

public class TestLevel : Node2D
{
    [Export]
    public NodePath LevelTileMap;

    private TileWorld tileWorld;

    public override void _Ready()
    {
        var tileMap = GetNode<TileMap>(LevelTileMap);
        if (tileMap == null)
        {
            return;
        }

        tileWorld = new TileWorld
        {
            TileMap = tileMap,
            GameSpeed = 10
        };

        AddChild(tileWorld);
    }
}
