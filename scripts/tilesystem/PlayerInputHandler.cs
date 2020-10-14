using Godot;

namespace Tiles
{
    public class PlayerInputHandler : Node
    {
        public class PlayerInputKey
        {
            public string Name { get; }
            public bool Pressed;
            private int _lastUpdateTick = -1;

            public PlayerInputKey(string name)
            {
                Name = name;
            }

            public void Update(bool keyStatus, int gameTick)
            {
                if (keyStatus && !Pressed)
                {
                    Pressed = true;
                    _lastUpdateTick = gameTick;
                }

                if (Pressed && !keyStatus && _lastUpdateTick != gameTick)
                {
                    Pressed = false;
                    _lastUpdateTick = gameTick;
                }
            }
        }

        public PlayerInputKey Left;
        public PlayerInputKey Right;
        public PlayerInputKey Up;
        public PlayerInputKey Down;
        public PlayerInputKey Explode;
        public PlayerInputKey Bomb;
        public PlayerInputKey Action;

        public TileWorld World { get; }

        public PlayerInputHandler(TileWorld world)
        {
            Name = "PlayerInputHandler";
            World = world;

            // Create keys
            Left = new PlayerInputKey("Left");
            Right = new PlayerInputKey("Right");
            Up = new PlayerInputKey("Up");
            Down = new PlayerInputKey("Down");
            Explode = new PlayerInputKey("Explode");
            Bomb = new PlayerInputKey("Bomb");
            Action = new PlayerInputKey("Action");
        }

        private bool LeftPressed => Input.IsActionPressed("player_left") || World.VKeyboard.LeftButtonPressed;
        private bool RightPressed => Input.IsActionPressed("player_right") || World.VKeyboard.RightButtonPressed;
        private bool UpPressed => Input.IsActionPressed("player_up") || World.VKeyboard.UpButtonPressed;
        private bool DownPressed => Input.IsActionPressed("player_down") || World.VKeyboard.DownButtonPressed;
        private bool ExplodePressed => Input.IsActionPressed("player_die") || World.VKeyboard.ExplodeButtonPressed;
        private bool BombPressed => Input.IsKeyPressed((int)KeyList.Control) || World.VKeyboard.BombButtonPressed;
        private bool ActionPressed => Input.IsKeyPressed((int)KeyList.Shift) || World.VKeyboard.ActionButtonPressed;

        public override void _PhysicsProcess(float delta)
        {
            Left.Update(LeftPressed, World.GameTicks);
            Right.Update(RightPressed, World.GameTicks);
            Up.Update(UpPressed, World.GameTicks);
            Down.Update(DownPressed, World.GameTicks);
            Explode.Update(ExplodePressed, World.GameTicks);
            Bomb.Update(BombPressed, World.GameTicks);
            Action.Update(ActionPressed, World.GameTicks);
        }
    }
}
