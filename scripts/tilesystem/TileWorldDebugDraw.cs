using Godot;
using System.Text;

namespace Tiles
{
    public static class DebugDrawUtils
    {
        public static string ShowBool(bool value)
        {
            return value ? ShowWithColor(true, Colors.Green) : ShowWithColor(false, Colors.Red);
        }

        public static string ShowTileState(State state)
        {
            if (state == State.Stopped)
            {
                return ShowWithColor(state, Colors.Red);
            }
            else if (state == State.Moving)
            {
                return ShowWithColor(state, Colors.Green);
            }
            else
            {
                return ShowWithColor(state, Colors.Yellow);
            }
        }

        public static string ShowTileDirection(Direction direction)
        {
            if (direction == Direction.None)
            {
                return ShowWithColor(direction, Colors.Red);
            }
            else
            {
                return ShowWithColor(direction, Colors.Yellow);
            }
        }

        public static string ShowWithColor(object obj, Color color)
        {
            return "[color=#" + color.ToHtml() + "]" + (obj?.ToString() ?? "None") + "[/color]";
        }

        public static string ShowCollisionTile(string type)
        {
            if (type != null)
            {
                return ShowWithColor(type.Substr(0, 8), Colors.Yellow);
            }
            else
            {
                return ShowWithColor(null, Colors.Red);
            }
        }

        public static string ShowFPS(float value)
        {
            if (value < 50)
            {
                return ShowWithColor(value, Colors.Red);
            }
            else if (value < 59)
            {
                return ShowWithColor(value, Colors.Yellow);
            }
            else
            {
                return ShowWithColor(value, Colors.Green);
            }
        }
    }

    public class TileWorldDebugDraw : Control
    {
        public Tile CurrentDebugTile;

        private readonly TileWorld _world;
        private RichTextLabel _infoLabel;
        private RichTextLabel _cellLabel;

        public TileWorldDebugDraw(TileWorld world)
        {
            Name = "TileWorldDebugDraw";
            _world = world;
            MouseFilter = MouseFilterEnum.Ignore;
        }

        public override void _Ready()
        {
            var size = GetViewportRect().Size;

            _infoLabel = new RichTextLabel
            {
                Name = "InfoLabel",
                RectMinSize = size * new Vector2(1, 0.5f),
                RectPosition = new Vector2(12, 12),
                BbcodeEnabled = true,
                BbcodeText = GenerateInfoText(),
                MouseFilter = MouseFilterEnum.Ignore
            };
            _infoLabel.AddFontOverride("normal_font", Assets.SimpleDefaultFont.Monospace);
            _infoLabel.AddFontOverride("bold_font", Assets.SimpleDefaultFont.Monospace.CloneWithSize(20));
            AddChild(_infoLabel);

            _cellLabel = new RichTextLabel
            {
                Name = "CellLabel",
                RectMinSize = size * new Vector2(1, 0.75f),
                RectPosition = (size * new Vector2(0, 0.25f)) + new Vector2(12, 12),
                BbcodeEnabled = true,
                BbcodeText = GenerateCellText(),
                MouseFilter = MouseFilterEnum.Ignore
            };
            _cellLabel.AddFontOverride("normal_font", Assets.SimpleDefaultFont.Monospace);
            AddChild(_cellLabel);
        }

        public override void _Process(float delta)
        {
            _infoLabel.BbcodeText = GenerateInfoText();
            _cellLabel.BbcodeText = GenerateCellText();
        }

        private string GenerateInfoText()
        {
            var sb = new StringBuilder();
            sb.Append("[b]OpenTerra Debug Draw[/b]\n");
            sb.AppendFormat("* FPS: {0}\n", DebugDrawUtils.ShowFPS(Engine.GetFramesPerSecond()));
            sb.AppendFormat("* Simulation status: {0}\n", DebugDrawUtils.ShowBool(_world.Running));
            sb.AppendFormat("* Ticks elapsed: {0}\n", _world.GameTicks);
            sb.AppendFormat("* Seconds elapsed: {0}\n", _world.ElapsedTime);
            return sb.ToString();
        }

        private string GenerateCellText()
        {
            if (CurrentDebugTile == null)
            {
                return "Click on a cell to get info";
            }

            return GenerateCellDebugInfo(CurrentDebugTile);
        }

        public string GenerateCellDebugInfo(Tile tile)
        {
            var sb = new StringBuilder();
            var status = _world.GetTileCollisions(tile);
            sb.AppendFormat("* Tile:            {0} ({1})\n", tile.Type, tile.GetType());
            sb.AppendFormat("* Name:            {0} (#{1})\n", tile.Name, tile.GetInstanceId());
            sb.AppendFormat("* Position:        {0}\n", tile.Position);
            sb.AppendFormat("* Rotation:        {0}\n", Mathf.Rad2Deg(tile.Rotation));
            sb.AppendFormat("* State:           {0}\n", tile.MoveState);
            sb.AppendFormat("* Last direction:  {0}\n", tile.LastDirection);
            sb.AppendFormat("* Priority:        {0}\n", tile.Priority);
            sb.AppendFormat("* Target position: {0}\n", tile.TargetPosition);
            sb.AppendFormat("* Target rotation: {0}\n", Mathf.Rad2Deg(tile.TargetRotation));
            sb.AppendFormat("* Cell Position:   {0}\n", _world.GetTileCurrentGridPosition(tile));
            sb.AppendFormat("* Tile layer:      {0}\n", DebugDrawUtils.ShowWithColor(tile.TileLayer, Colors.Yellow));
            sb.AppendFormat("* Direction:       {0}\n", DebugDrawUtils.ShowTileDirection(tile.NextDirection));
            sb.Append(GenerateCollisionDebugInfo(status));

            // Explosion info
            if (tile.CanExplode && tile.WillExplodeAtTick != -1)
            {
                sb.AppendFormat(
                    "{0} {1}\n",
                    DebugDrawUtils.ShowWithColor("* Will explode at tick: ", Colors.Red),
                    DebugDrawUtils.ShowWithColor(tile.WillExplodeAtTick + 1, Colors.Red)
                );
            }

            sb.Append(tile.GenerateTileDebugInfo(status));
            return sb.ToString();
        }

        public string GenerateCollisionDebugInfo(CollisionStatus status)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("* Collisions:\n");
            sb.AppendFormat("  {0,-33} {1,-33} {2,-33}\n", DebugDrawUtils.ShowCollisionTile(status.topLeft?.Type), DebugDrawUtils.ShowCollisionTile(status.top?.Type), DebugDrawUtils.ShowCollisionTile(status.topRight?.Type));
            sb.AppendFormat("  {0,-33} {1,-42}\n", DebugDrawUtils.ShowCollisionTile(status.left?.Type), DebugDrawUtils.ShowCollisionTile(status.right?.Type));
            sb.AppendFormat("  {0,-33} {1,-33} {2,-33}\n", DebugDrawUtils.ShowCollisionTile(status.bottomLeft?.Type), DebugDrawUtils.ShowCollisionTile(status.bottom?.Type), DebugDrawUtils.ShowCollisionTile(status.bottomRight?.Type));
            return sb.ToString();
        }

        public override void _UnhandledInput(InputEvent @event)
        {
            if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed && (mouseEvent.ButtonIndex == (int)ButtonList.Left))
            {
                // Offset camera coordinates
                var camera = _world.Camera;
                var topLeftPosition = camera.GetCameraScreenCenter() - (GetViewportRect().Size / 2);
                var tilePosition = _world.TileMap.WorldToMap(mouseEvent.Position + topLeftPosition);

                var tFgTile = _world.GetTileAtGridPosition(tilePosition, TilePickEnum.ForegroundOnly);
                var tMdTile = _world.GetTileAtGridPosition(tilePosition, TilePickEnum.MiddleOnly);
                var tBgTile = _world.GetTileAtGridPosition(tilePosition, TilePickEnum.BackgroundOnly);

                if (CurrentDebugTile == tFgTile && tMdTile != null)
                {
                    SetCurrentDebugTile(tMdTile);
                }
                else if (CurrentDebugTile == tFgTile && tBgTile != null)
                {
                    SetCurrentDebugTile(tBgTile);
                }
                else if (CurrentDebugTile == tMdTile && tBgTile != null)
                {
                    SetCurrentDebugTile(tBgTile);
                }
                else if (CurrentDebugTile == tMdTile && tFgTile != null)
                {
                    SetCurrentDebugTile(tFgTile);
                }
                else if (CurrentDebugTile == tBgTile && tFgTile != null)
                {
                    SetCurrentDebugTile(tFgTile);
                }
                else if (CurrentDebugTile == tBgTile && tMdTile != null)
                {
                    SetCurrentDebugTile(tMdTile);
                }
                else if (tFgTile != null)
                {
                    SetCurrentDebugTile(tFgTile);
                }
                else if (tMdTile != null)
                {
                    SetCurrentDebugTile(tMdTile);
                }
                else if (tBgTile != null)
                {
                    SetCurrentDebugTile(tBgTile);
                }
                else
                {
                    SetCurrentDebugTile(null);
                }
            }
        }

        public void SetCurrentDebugTile(Tile tile)
        {
            if (CurrentDebugTile != null)
            {
                // Remove modulate
                CurrentDebugTile.Modulate = Colors.White;
            }

            CurrentDebugTile = tile;
            if (CurrentDebugTile != null)
            {
                // Set modulate
                CurrentDebugTile.Modulate = Colors.WebPurple;
            }
        }
    }
}
