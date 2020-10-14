using Godot;
using System.Linq;

namespace Tiles
{
    public class TileCamera : Camera2D
    {
        public bool Paused { get; set; }

        private readonly TileWorld _world;
        private Godot.Collections.Array _players;

        public TileCamera(TileWorld world)
        {
            Name = "TileCamera";
            _world = world;
            LimitLeft = 0;
            LimitTop = 0;
            SmoothingEnabled = true;
        }

        public override void _Ready()
        {
            InitializeCameraSettings();
        }

        public void InitializeCameraSettings()
        {
            // Use world size to delimit camera
            var size = _world.GridSize;
            var vpSize = GetViewportRect().Size;
            LimitRight = (int)Mathf.Max(size.x * _world.TileMap.CellSize.x, vpSize.x);
            LimitBottom = (int)Mathf.Max(size.y * _world.TileMap.CellSize.y, vpSize.y);
        }

        public void ScanPlayers()
        {
            _players = GetTree().GetNodesInGroup("players");
        }

        public override void _Process(float delta)
        {
            if (!Paused)
            {
                // List players, and compute center
                ScanPlayers();
                if (_players.Count > 0)
                {
                    GlobalPosition = _players.Cast<PlayerTile>().Select(p => p.Position).Aggregate((a, b) => a + b) / _players.Count;
                }
                return;
            }

            var movingOffset = Vector2.Zero;
            if (Input.IsKeyPressed((int)KeyList.Left))
            {
                movingOffset.x--;
            }
            if (Input.IsKeyPressed((int)KeyList.Right))
            {
                movingOffset.x++;
            }
            if (Input.IsKeyPressed((int)KeyList.Up))
            {
                movingOffset.y--;
            }
            if (Input.IsKeyPressed((int)KeyList.Down))
            {
                movingOffset.y++;
            }

            GlobalPosition += movingOffset * 1000 * delta;
        }
    }
}
