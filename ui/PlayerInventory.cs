using Godot;
using System.Collections.Generic;

namespace Tiles
{
    public class PlayerInventory : CanvasLayer
    {
        [Signal]
        public delegate void GemsUpdated(int quantity);

        public TileWorld World;
        public int Gems
        {
            get => _gems;
        }

        private int _gems;
        private readonly Dictionary<KeyColorEnum, bool> _keys;

        public PlayerInventory()
        {
            Name = "PlayerInventory";
            _keys = new Dictionary<KeyColorEnum, bool>
            {
                [KeyColorEnum.Blue] = false,
                [KeyColorEnum.Green] = false,
                [KeyColorEnum.Red] = false,
                [KeyColorEnum.Yellow] = false
            };
        }

        public override void _Ready()
        {
            World.Connect(nameof(TileWorld.TimeUpdated), this, nameof(UpdateTime));

            UpdateKeyColors();
            UpdateGemValue();
            UpdateTime(0);
        }

        public void AddGems(int quantity)
        {
            _gems += quantity;
            UpdateGemValue();

            EmitSignal(nameof(GemsUpdated), _gems);
        }

        public bool HasKeyColor(KeyColorEnum color)
        {
            return _keys[color];
        }

        public void SetKeyColor(KeyColorEnum color)
        {
            _keys[color] = true;
            UpdateKeyColors();
        }

        public void UnsetKeyColor(KeyColorEnum color)
        {
            _keys[color] = false;
            UpdateKeyColors();
        }

        public void UpdateKeyColors()
        {
            foreach (KeyColorEnum color in _keys.Keys)
            {
                var node = GetNode<Control>("Main/TopBar/KeyStats/" + color.ToString() + "Key");
                node.Visible = _keys[color];
            }
        }

        public void UpdateGemValue()
        {
            GetNode<Label>("Main/TopBar/GemStats/Value").Text = _gems.ToString() + " / " + World.TileMap.GemsForNormalExit.ToString();
        }

        private void UpdateTime(int value)
        {
            GetNode<Label>("Main/TopBar/TimeStats/Value").Text = Mathf.Max(World.TileMap.TimeLimit - value, 0).ToString();
        }
    }
}
