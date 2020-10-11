using Godot;
using System.Collections.Generic;
using System.Linq;

namespace Tiles
{
    public enum TilePickEnum
    {
        ForegroundFirst,
        BackgroundOnly,
        ForegroundOnly
    }

    public class TileWorld : Node2D
    {
        public TileMap TileMap;
        public Node2D TileContainer;

        public int GameSpeed { set; get; } = 20;

        public bool Running
        {
            get => _running;
            set
            {
                _running = value;
                if (_camera != null)
                {
                    _camera.Paused = !value;
                }
            }
        }

        public Vector2 GridSize
        {
            get => _gridSize;
        }

        public int GameTicks
        {
            get => _gameTicks;
        }

        public Camera2D Camera
        {
            get => _camera;
        }

        public PlayerInputHandler PlayerInput
        {
            get => _playerInput;
        }

        public VirtualKeyboard VKeyboard;

        private int _physicsTicks;
        private int _gameTicks;
        private bool _running;
        private TileWorldDebugDraw _debugDraw;
        private TileCamera _camera;
        private PlayerInputHandler _playerInput;

        private Vector2 _gridSize;
        private Tile[,] _backgroundTiles;
        private Tile[,] _foregroundTiles;
        private Dictionary<Tile, Vector2> _tilesIndex;

        public TileWorld()
        {
            Name = "TileWorld";
        }

        public override void _Ready()
        {
            var size = GetViewportRect().Size;
            _running = true;
            _physicsTicks = 0;
            _gameTicks = 0;

            // Black background
            var bgColor = new ColorRect();
            bgColor.Name = "BGColor";
            bgColor.Color = Colors.Black;
            bgColor.MouseFilter = Control.MouseFilterEnum.Ignore;
            AddChild(bgColor);

            // Check for TileContainer
            if (TileContainer == null)
            {
                TileContainer = new Node2D();
                TileContainer.Name = "TileContainer";
                AddChild(TileContainer);
            }

            MapCellsToObjects();
            bgColor.RectMinSize = _gridSize * TileMap.CellSize;
            TileMap.Visible = false;

            // Add camera
            _camera = new TileCamera(this)
            {
                Current = true
            };
            AddChild(_camera);
            _camera.Paused = !_running;
            _camera.ScanPlayers();

            // Debug draw
            var canvasLayer = new CanvasLayer();
            canvasLayer.Name = "DebugDrawCanvasLayer";
            AddChild(canvasLayer);
            _debugDraw = new TileWorldDebugDraw(this);
            canvasLayer.AddChild(_debugDraw);

            // Connect virtual keyboard
            VKeyboard = (VirtualKeyboard)GD.Load<PackedScene>("res://ui/VirtualKeyboard.tscn").Instance();
            VKeyboard.Connect(nameof(VirtualKeyboard.RestartPressed), this, nameof(RestartLevel));
            VKeyboard.Connect(nameof(VirtualKeyboard.PausePressed), this, nameof(DebugToggleRun));
            VKeyboard.Connect(nameof(VirtualKeyboard.StepPressed), this, nameof(DebugStepForward));
            VKeyboard.Connect(nameof(VirtualKeyboard.DebugDrawToggled), this, nameof(DebugToggleDebugDraw));
            AddChild(VKeyboard);

            // Connect player input
            _playerInput = new PlayerInputHandler(this);
            AddChild(_playerInput);
        }

        private void UnsetTileAtPosition(Tile tile, Vector2 pos)
        {
            int x = (int)pos.x;
            int y = (int)pos.y;

            if (tile.Background)
            {
                var tTile = _backgroundTiles[y, x];
                if (tTile != tile)
                {
                    GD.PrintErr("Unsetting mismatching tile ", pos, " : Expecting ", tile.Name, ", found ", tTile?.Name);
                }
                else
                {
                    _backgroundTiles[y, x] = null;
                }
            }
            else
            {
                var tTile = _foregroundTiles[y, x];
                if (tTile != tile)
                {
                    GD.PrintErr("Unsetting mismatching tile ", pos, " : Expecting ", tile.Name, ", found ", tTile?.Name);
                }
                else
                {
                    _foregroundTiles[y, x] = null;
                }
            }
        }

        private void SetTileAtPosition(Tile tile, Vector2 pos)
        {
            int x = (int)pos.x;
            int y = (int)pos.y;

            if (tile.Background)
            {
                var tTile = _backgroundTiles[y, x];
                if (tTile != null)
                {
                    GD.PrintErr("Setting tile in a non-empty space ", pos, " : Setting ", tile.Name, ", found ", tTile.Name);
                }
                _backgroundTiles[y, x] = tile;
            }
            else
            {
                var tTile = _foregroundTiles[y, x];
                if (tTile != null)
                {
                    GD.PrintErr("Setting tile in a non-empty space ", pos, " : Setting ", tile.Name, ", found ", tTile.Name);
                }
                _foregroundTiles[y, x] = tile;
            }
        }

        public void SpawnExplosionAtTile(Tile tile)
        {
            var cellPosition = GetTileCurrentGridPosition(tile);
            tile.Updated = true;
            RemoveTile(tile);

            // Spawn explosion
            var explosionIdx = TileMap.TileSet.FindTileByName("Explosion");
            var eTile = CreateTile(explosionIdx, cellPosition);

            // Get neighbor tiles
            foreach (Tile.Direction dir in Tile.AllDirections)
            {
                var tPos = GetNeighborPosition(eTile, dir);
                var tTiles = ListTilesAtGridPosition(tPos);
                bool shouldCreateTile = true;

                if (tTiles.Count > 0)
                {
                    foreach (Tile tTile in tTiles)
                    {
                        if (tTile.Indestructible || tTile.Type == "Explosion" || tTile.WillExplodeAtTick > 0)
                        {
                            shouldCreateTile = false;
                        }
                        else if (tTile.CanExplode)
                        {
                            tTile.WillExplode();
                            shouldCreateTile = false;
                        }
                        else
                        {
                            tTile.Destroyed = true;
                            tTile.Updated = true;
                            RemoveTile(tTile);
                        }
                    }
                }

                if (shouldCreateTile)
                {
                    CreateTile(explosionIdx, tPos);
                }
            }
        }

        public void UpdateTilePosition(Tile tile)
        {
            // Use previous index
            var curPos = GetTileCurrentGridPosition(tile);
            var lastPos = _tilesIndex[tile];
            if (lastPos != curPos)
            {
                UnsetTileAtPosition(tile, lastPos);
                SetTileAtPosition(tile, curPos);
                _tilesIndex[tile] = curPos;
            }
        }

        public void RemoveTile(Tile tile)
        {
            // Invalidate current debug tile
            if (_debugDraw.CurrentDebugTile == tile)
            {
                _debugDraw.CurrentDebugTile = null;
            }

            var cellPosition = GetTileCurrentGridPosition(tile);
            UnsetTileAtPosition(tile, cellPosition);
            tile.QueueFree();
        }

        public Tile CreateTile(int idx, Vector2 cellPosition)
        {
            string tileName = TileMap.TileSet.TileGetName(idx);
            Rect2 tileRect = TileMap.TileSet.TileGetRegion(idx);
            Texture tex = TileMap.TileSet.TileGetTexture(idx);

            // Build node
            Tile cellNode = TileFactory.CreateTileFromName(tileName);
            cellNode.World = this;
            cellNode.Name = tileName;
            cellNode.Type = tileName;
            cellNode.Position = (cellPosition * TileMap.CellSize) + (TileMap.CellSize / 2);
            cellNode.StepTicks = GameSpeed;
            cellNode.TargetRotation = cellNode.Rotation;
            cellNode.TargetPosition = cellNode.Position;

            var cellSprite = new Sprite();
            cellSprite.Name = "Sprite";
            cellSprite.Texture = tex;
            cellSprite.RegionEnabled = true;
            cellSprite.RegionRect = tileRect;
            cellNode.AddChild(cellSprite);
            TileContainer.AddChild(cellNode);

            // Set tiles
            if (cellNode.Background)
            {
                _backgroundTiles[(int)cellPosition.y, (int)cellPosition.x] = cellNode;
            }
            else
            {
                _foregroundTiles[(int)cellPosition.y, (int)cellPosition.x] = cellNode;
            }

            // Index
            _tilesIndex[cellNode] = cellPosition;
            return cellNode;
        }

        private void MapCellsToObjects()
        {
            _gridSize = TileMap.GetUsedRect().Size;
            _backgroundTiles = new Tile[(int)_gridSize.y, (int)_gridSize.x];
            _foregroundTiles = new Tile[(int)_gridSize.y, (int)_gridSize.x];
            _tilesIndex = new Dictionary<Tile, Vector2>();

            foreach (Vector2 cellPosition in TileMap.GetUsedCells())
            {
                int idx = TileMap.GetCellv(cellPosition);
                CreateTile(idx, cellPosition);
            }
        }

        private void ResetTickOnTiles(List<Tile> tiles)
        {
            foreach (Tile tile in tiles)
            {
                tile.ResetTick();
            }
        }

        private void ApplyMoveOnTiles(List<Tile> tiles)
        {
            foreach (Tile tile in tiles)
            {
                if (tile.MoveState == Tile.State.WillMove && !tile.Destroyed)
                {
                    tile.Move();
                }
            }
        }

        private void ApplyStepOnTiles(List<Tile> tiles)
        {
            foreach (Tile tile in tiles)
            {
                if (!tile.Updated && !tile.Destroyed)
                {
                    tile.Step();
                    tile.Updated = true;
                }
            }
        }

        private List<Tile> OrderTilesByPriority(Tile[,] tiles)
        {
            Dictionary<int, List<Tile>> priorities = new Dictionary<int, List<Tile>>();

            for (int j = (int)_gridSize.y - 1; j >= 0; --j)
            {
                for (int i = 0; i < _gridSize.x; ++i)
                {
                    var tile = tiles[j, i];
                    if (tile != null)
                    {
                        if (!priorities.ContainsKey(tile.Priority))
                        {
                            priorities[tile.Priority] = new List<Tile>();
                        }
                        priorities[tile.Priority].Add(tile);
                    }
                }
            }

            List<Tile> result = new List<Tile>();
            var sortedKeys = priorities.Keys.OrderByDescending(k => k);
            foreach (int priority in sortedKeys)
            {
                result.AddRange(priorities[priority]);
            }
            return result;
        }

        private void GameStep()
        {
            var orderedBgTiles = OrderTilesByPriority(_backgroundTiles);
            var orderedFgTiles = OrderTilesByPriority(_foregroundTiles);

            ResetTickOnTiles(orderedBgTiles);
            ResetTickOnTiles(orderedFgTiles);
            ApplyStepOnTiles(orderedBgTiles);
            ApplyStepOnTiles(orderedFgTiles);
            ApplyMoveOnTiles(orderedBgTiles);
            ApplyMoveOnTiles(orderedFgTiles);
        }

        public CollisionStatus GetTileCollisions(Tile current)
        {
            var thisGridPosition = GetTileCurrentGridPosition(current);
            var thisTopPosition = thisGridPosition + new Vector2(0, -1);
            var thisBottomPosition = thisGridPosition + new Vector2(0, 1);
            var thisLeftPosition = thisGridPosition + new Vector2(-1, 0);
            var thisRightPosition = thisGridPosition + new Vector2(1, 0);
            var thisTopLeftPosition = thisGridPosition + new Vector2(-1, -1);
            var thisTopRightPosition = thisGridPosition + new Vector2(1, -1);
            var thisBottomLeftPosition = thisGridPosition + new Vector2(-1, 1);
            var thisBottomRightPosition = thisGridPosition + new Vector2(1, 1);

            return new CollisionStatus
            {
                top = GetTileAtGridPosition(thisTopPosition),
                bottom = GetTileAtGridPosition(thisBottomPosition),
                left = GetTileAtGridPosition(thisLeftPosition),
                right = GetTileAtGridPosition(thisRightPosition),
                topLeft = GetTileAtGridPosition(thisTopLeftPosition),
                topRight = GetTileAtGridPosition(thisTopRightPosition),
                bottomLeft = GetTileAtGridPosition(thisBottomLeftPosition),
                bottomRight = GetTileAtGridPosition(thisBottomRightPosition),
            };
        }

        public Vector2 GetTileCurrentGridPosition(Tile current)
        {
            var pos = current.TargetPosition;
            return TileMap.WorldToMap(pos);
        }

        public Tile GetNeighborTile(Tile current, Tile.Direction direction)
        {
            return GetTileAtGridPosition(GetNeighborPosition(current, direction));
        }

        public Vector2 GetNeighborPosition(Tile current, Tile.Direction direction)
        {
            var pos = current.TargetPosition;
            return TileMap.WorldToMap(pos) + GetDirectionVector(direction);
        }

        public Tile GetTileAtGridPosition(Vector2 gridPosition, TilePickEnum pickEnum = TilePickEnum.ForegroundFirst)
        {
            if (gridPosition.x < 0 || gridPosition.x >= _gridSize.x || gridPosition.y < 0 || gridPosition.y >= _gridSize.y)
            {
                // Out of bounds
                return null;
            }

            if (pickEnum != TilePickEnum.BackgroundOnly)
            {
                Tile fTile = _foregroundTiles[(int)gridPosition.y, (int)gridPosition.x];
                if (fTile != null)
                {
                    return fTile;
                }
            }

            if (pickEnum != TilePickEnum.ForegroundOnly)
            {
                Tile bTile = _backgroundTiles[(int)gridPosition.y, (int)gridPosition.x];
                if (bTile != null)
                {
                    return bTile;
                }
            }

            return null;
        }

        public List<Tile> ListTilesAtGridPosition(Vector2 gridPosition)
        {
            var tiles = new List<Tile>();
            var fgTile = GetTileAtGridPosition(gridPosition, TilePickEnum.ForegroundOnly);
            var bgTile = GetTileAtGridPosition(gridPosition, TilePickEnum.BackgroundOnly);

            if (fgTile != null) tiles.Add(fgTile);
            if (bgTile != null) tiles.Add(bgTile);
            return tiles;
        }

        public void DebugStepForward()
        {
            Running = false;
            GameStep();
            _gameTicks++;
        }

        public void DebugToggleRun()
        {
            Running = !Running;
        }

        public void DebugToggleDebugDraw(bool pressed)
        {
            _debugDraw.Visible = pressed;
            _debugDraw.SetProcess(pressed);
            _debugDraw.SetProcessUnhandledInput(pressed);
        }

        public void RestartLevel()
        {
            GetTree().ReloadCurrentScene();
        }

        public Vector2 GetDirectionVector(Tile.Direction direction)
        {
            return direction switch
            {
                Tile.Direction.Left => new Vector2(-1, 0),
                Tile.Direction.Right => new Vector2(1, 0),
                Tile.Direction.Up => new Vector2(0, -1),
                Tile.Direction.Down => new Vector2(0, 1),
                Tile.Direction.UpLeft => new Vector2(-1, -1),
                Tile.Direction.UpRight => new Vector2(1, -1),
                Tile.Direction.DownLeft => new Vector2(-1, 1),
                Tile.Direction.DownRight => new Vector2(1, 1),
                _ => new Vector2(0, 0),
            };
        }

        public Tile.Direction GetInvertedDirection(Tile.Direction direction)
        {
            return direction switch
            {
                Tile.Direction.Left => Tile.Direction.Right,
                Tile.Direction.Right => Tile.Direction.Left,
                Tile.Direction.Up => Tile.Direction.Down,
                Tile.Direction.Down => Tile.Direction.Up,
                Tile.Direction.UpLeft => Tile.Direction.DownRight,
                Tile.Direction.UpRight => Tile.Direction.DownLeft,
                Tile.Direction.DownLeft => Tile.Direction.UpRight,
                Tile.Direction.DownRight => Tile.Direction.UpLeft,
                _ => Tile.Direction.None,
            };
        }

        public override void _PhysicsProcess(float delta)
        {
            _physicsTicks++;

            if (Input.IsActionJustPressed("level_reset"))
            {
                RestartLevel();
            }

            if (Input.IsActionJustPressed("debug_step_forward"))
            {
                DebugStepForward();
            }

            if (Input.IsActionJustPressed("debug_toggle_info"))
            {
                DebugToggleDebugDraw(!_debugDraw.Visible);
            }

            if (Input.IsActionJustPressed("debug_toggle_run"))
            {
                DebugToggleRun();
            }

            if (Running && _physicsTicks % GameSpeed == 0)
            {
                GameStep();
                _gameTicks++;
            }
        }
    }
}
