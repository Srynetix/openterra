using Godot;
using System.Collections.Generic;

namespace Tiles
{
    public class PlayerInventory : CanvasLayer
    {
        [Signal]
        public delegate void GemsUpdated(int quantity);

        public int Gems
        {
            get => _gems;
        }

        private int _gems;
        private readonly Dictionary<Tile.KeyColorEnum, bool> _keys;

        public PlayerInventory()
        {
            Name = "PlayerInventory";
            _keys = new Dictionary<Tile.KeyColorEnum, bool>
            {
                [Tile.KeyColorEnum.Blue] = false,
                [Tile.KeyColorEnum.Green] = false,
                [Tile.KeyColorEnum.Red] = false,
                [Tile.KeyColorEnum.Yellow] = false
            };
        }

        public override void _Ready()
        {
            UpdateKeyColors();
        }

        public void AddGems(int quantity)
        {
            _gems += quantity;
            UpdateGemValue();

            EmitSignal(nameof(GemsUpdated), _gems);
        }

        public bool HasKeyColor(Tile.KeyColorEnum color)
        {
            return _keys[color];
        }

        public void SetKeyColor(Tile.KeyColorEnum color)
        {
            _keys[color] = true;
            UpdateKeyColors();
        }

        public void UnsetKeyColor(Tile.KeyColorEnum color)
        {
            _keys[color] = false;
            UpdateKeyColors();
        }

        public void UpdateKeyColors()
        {
            foreach (Tile.KeyColorEnum color in _keys.Keys)
            {
                var node = GetNode<Control>("Main/TopBar/KeyStats/" + color.ToString() + "Key");
                node.Visible = _keys[color];
            }
        }

        public void UpdateGemValue()
        {
            GetNode<Label>("Main/TopBar/GemStats/Value").Text = _gems.ToString();
        }
    }
}
