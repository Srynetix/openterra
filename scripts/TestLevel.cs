using Godot;
using Tiles;

using System.Collections.Generic;
using System.Linq;

public class TestLevel : Node2D
{
    [Export] public int GameSpeed = 10;

    private List<TileMap> _tileMaps;
    private TileWorld _tileWorld;
    private OptionButton _optionButton;
    private Button _loadButton;

    public override void _Ready()
    {
        _optionButton = GetNode<OptionButton>("CanvasLayer/Main/Row/OptionButton");
        _loadButton = GetNode<Button>("CanvasLayer/Main/Row/Button");
        _loadButton.Connect("pressed", this, nameof(LoadCurrentTilemap));
        _tileMaps = ListTileMaps();

        foreach (TileMap tMap in _tileMaps)
        {
            _optionButton.AddItem(tMap.Name);
        }
        _optionButton.Select(0);
        LoadCurrentTilemap();
    }

    private void LoadCurrentTilemap()
    {
        var selectedIdx = _optionButton.Selected;
        if (selectedIdx == -1) return;

        var tileMap = _tileMaps[_optionButton.Selected];
        if (_tileWorld != null)
        {
            RemoveChild(_tileWorld);
            _tileWorld.QueueFree();
        }

        _tileWorld = new TileWorld
        {
            TileMap = tileMap,
            GameSpeed = GameSpeed
        };
        AddChild(_tileWorld);
    }

    private List<TileMap> ListTileMaps()
    {
        var tilemaps = GetNode<Node2D>("TileMaps");
        return tilemaps.GetChildren().Cast<TileMap>().ToList();
    }
}
