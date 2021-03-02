using Godot;
using Tiles;

using System.Collections.Generic;
using System.Linq;

public class TestLevel : Node2D
{
    [Export] public bool StartPaused;
    [Export] public int GameSpeed = 10;

    private List<Level> _levels;
    private TileWorld _tileWorld;
    private OptionButton _optionButton;
    private Button _loadButton;

    public override void _Ready()
    {
        _optionButton = GetNode<OptionButton>("CanvasLayer/Main/Row/OptionButton");
        _loadButton = GetNode<Button>("CanvasLayer/Main/Row/Button");
        _loadButton.Connect("pressed", this, nameof(LoadCurrentTilemap));
        _levels = ListLevels();

        foreach (Level level in _levels)
        {
            _optionButton.AddItem(level.Name);
        }
        _optionButton.Select(0);
        LoadCurrentTilemap();
    }

    private void LoadCurrentTilemap()
    {
        var selectedIdx = _optionButton.Selected;
        if (selectedIdx == -1) return;

        var tileMap = _levels[_optionButton.Selected];
        if (_tileWorld != null)
        {
            RemoveChild(_tileWorld);
            _tileWorld.QueueFree();
        }

        _tileWorld = new TileWorld
        {
            TileMap = tileMap,
            GameSpeed = GameSpeed,
            StartPaused = StartPaused
        };
        AddChild(_tileWorld);
    }

    private List<Level> ListLevels()
    {
        var levels = GetNode<Node2D>("Levels");
        return levels.GetChildren().Cast<Level>().ToList();
    }
}
