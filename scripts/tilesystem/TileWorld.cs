using Godot;
using System.Collections.Generic;
using System.Linq;

namespace Tiles
{
    public class TileWorld : Node2D
    {
        [Signal] public delegate void TimeUpdated(int value);

        public Level TileMap;
        public Node2D TileContainer;
        public bool StartPaused;

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

        public PlayerInventory PInventory
        {
            get => _playerInventory;
        }

        public int ElapsedTime
        {
            get => _elapsedTime;
        }

        public VirtualKeyboard VKeyboard;

        private int _physicsTicks;
        private int _gameTicks;
        private bool _running;
        private TileWorldDebugDraw _debugDraw;
        private TileCamera _camera;
        private PlayerInputHandler _playerInput;
        private PlayerInventory _playerInventory;
        private bool _normalExitsOpened;
        private bool _hardExitsOpened;

        private Vector2 _gridSize;
        private Dictionary<TileLayerEnum, Tile[,]> _tiles;
        private Dictionary<Tile, Vector2> _tilesIndex;

        private int _elapsedTime;

        public static List<TileLayerEnum> AllLayers = new List<TileLayerEnum> {
            TileLayerEnum.Background,
            TileLayerEnum.Middle,
            TileLayerEnum.Foreground,
        };

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
            var bgColor = new ColorRect
            {
                Name = "BGColor",
                Color = Colors.Black,
                MouseFilter = Control.MouseFilterEnum.Ignore
            };
            AddChild(bgColor);

            // Check for TileContainer
            if (TileContainer == null)
            {
                TileContainer = new Node2D
                {
                    Name = "TileContainer"
                };
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
            var canvasLayer = new CanvasLayer
            {
                Name = "DebugDrawCanvasLayer"
            };
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

            // Connect player inventory
            _playerInventory = (PlayerInventory)GD.Load<PackedScene>("res://ui/PlayerInventory.tscn").Instance();
            _playerInventory.Connect(nameof(PlayerInventory.GemsUpdated), this, nameof(OnGemsUpdated));
            _playerInventory.World = this;
            AddChild(_playerInventory);

            // Manual gems update
            OnGemsUpdated(0);

            if (StartPaused) {
                Running = false;
            }
        }

        private void UnsetTileAtPosition(Tile tile, Vector2 pos)
        {
            int x = (int)pos.x;
            int y = (int)pos.y;

            var layer = tile.TileLayer;
            var tTile = _tiles[layer][y, x];
            if (tTile != tile)
            {
                GD.PrintErr("Unsetting mismatching tile ", pos, " : Expecting ", tile.Name, ", found ", tTile?.Name);
            }
            else
            {
                _tiles[layer][y, x] = null;
            }
        }

        private void SetTileAtPosition(Tile tile, Vector2 pos)
        {
            int x = (int)pos.x;
            int y = (int)pos.y;

            var layer = tile.TileLayer;
            var tTile = _tiles[layer][y, x];
            if (tTile != null && tTile != tile)
            {
                GD.PrintErr("Setting tile in a non-empty space ", pos, " : Setting ", tile.Name, ", found ", tTile.Name);
            }
            _tiles[layer][y, x] = tile;
        }

        public void SpawnExplosionAtTile(Tile tile)
        {
            var cellPosition = GetTileCurrentGridPosition(tile);
            tile.Updated = true;
            foreach (Tile t in ListTilesAtGridPosition(cellPosition))
            {
                RemoveTile(t);
            }

            // Spawn explosion
            var eTile = CreateTile("Explosion", cellPosition);

            // Get neighbor tiles
            foreach (Direction dir in Tile.AllDirections)
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
                    CreateTile("Explosion", tPos);
                }
            }
        }

        public List<Tile> ScanAllTilesOfType(string tileType)
        {
            var tiles = new List<Tile>();
            for (int j = 0; j < _gridSize.y; ++j)
            {
                for (int i = 0; i < _gridSize.x; ++i)
                {
                    foreach (TileLayerEnum tileLayer in AllLayers)
                    {
                        var tile = _tiles[tileLayer][j, i];
                        if (tile?.Type == tileType)
                        {
                            tiles.Add(tile);
                        }
                    }
                }
            }

            return tiles;
        }

        public Tile ScanNextTileOfType(Vector2 tilePosition, string tileType)
        {
            // Scan next tile of type from position
            int tSize = (int)(_gridSize.x * _gridSize.y);
            int sIdx = (int)(tilePosition.x + (tilePosition.y * _gridSize.x));
            for (int i = sIdx + 1; i < tSize; ++i)
            {
                int tX = i % (int)_gridSize.x;
                int tY = i / (int)_gridSize.x;

                foreach (TileLayerEnum tileLayer in AllLayers)
                {
                    var tile = _tiles[tileLayer][tY, tX];
                    if (tile?.Type == tileType)
                    {
                        return tile;
                    }
                }
            }

            // Rewind
            for (int i = 0; i <= sIdx; ++i)
            {
                int tX = i % (int)_gridSize.x;
                int tY = i / (int)_gridSize.x;

                foreach (TileLayerEnum tileLayer in AllLayers)
                {
                    var tile = _tiles[tileLayer][tY, tX];
                    if (tile?.Type == tileType)
                    {
                        return tile;
                    }
                }
            }

            return null;
        }

        public void UpdateTilePosition(Tile tile)
        {
            // Use previous index
            var curPos = GetTileCurrentGridPosition(tile);
            var tgtPos = GetTargetTilePosition(tile);
            var lastPos = _tilesIndex[tile];
            if (lastPos != curPos)
            {
                UnsetTileAtPosition(tile, lastPos);
                SetTileAtPosition(tile, curPos);
                _tilesIndex[tile] = curPos;
            }

            if (tgtPos != curPos) {
                SetTileAtPosition(tile, tgtPos);
            }
        }

        public void ToggleBarriersState(BarrierColorEnum barrierColor)
        {
            foreach (BarrierTile tile in GetTree().GetNodesInGroup(barrierColor.ToString() + "_barrier"))
            {
                tile.Active = !tile.Active;
            }
        }

        public void OpenExits(ExitTypeEnum exitType)
        {
            foreach (ExitTile tile in GetTree().GetNodesInGroup(exitType.ToString() + "_exit"))
            {
                tile.Opened = true;
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

        public Tile CreateTile(string tileName, Vector2 cellPosition)
        {
            int idx = TileMap.TileSet.FindTileByName(tileName);
            Rect2 tileRect = TileMap.TileSet.TileGetRegion(idx);
            Texture tex = TileMap.TileSet.TileGetTexture(idx);

            // Build node
            Tile cellNode = TileFactory.CreateTileFromName(tileName);
            cellNode.World = this;
            cellNode.Name = tileName;
            cellNode.Type = tileName;
            cellNode.Position = (cellPosition * TileMap.CellSize) + (TileMap.CellSize / 2);
            cellNode.StepTicks = GameSpeed;
            cellNode.TargetPosition = cellNode.Position;
            cellNode.TargetRotation = 0;

            var cellSprite = new Sprite
            {
                Name = "Sprite",
                Texture = tex,
                RegionEnabled = true,
                RegionRect = tileRect,
                ShowBehindParent = true
            };
            cellNode.AddChild(cellSprite);
            TileContainer.AddChild(cellNode);

            // Set tiles
            _tiles[cellNode.TileLayer][(int)cellPosition.y, (int)cellPosition.x] = cellNode;

            // Index
            _tilesIndex[cellNode] = cellPosition;
            return cellNode;
        }

        private void MapCellsToObjects()
        {
            _gridSize = TileMap.GetUsedRect().Size;
            _tiles = new Dictionary<TileLayerEnum, Tile[,]>
            {
                [TileLayerEnum.Background] = new Tile[(int)_gridSize.y, (int)_gridSize.x],
                [TileLayerEnum.Middle] = new Tile[(int)_gridSize.y, (int)_gridSize.x],
                [TileLayerEnum.Foreground] = new Tile[(int)_gridSize.y, (int)_gridSize.x]
            };
            _tilesIndex = new Dictionary<Tile, Vector2>();

            foreach (Vector2 cellPosition in TileMap.GetUsedCells())
            {
                int idx = TileMap.GetCellv(cellPosition);
                string name = TileMap.TileSet.TileGetName(idx);
                CreateTile(name, cellPosition);
            }
        }

        private void ResetTickOnTiles(List<Tile> tiles)
        {
            foreach (Tile tile in tiles)
            {
                tile.ResetTick();
            }
        }

        private void OnGemsUpdated(int gems)
        {
            // Exits check
            if (!_normalExitsOpened && TileMap.GemsForNormalExit <= gems)
            {
                OpenExits(ExitTypeEnum.Normal);
                _normalExitsOpened = true;
            }

            if (!_hardExitsOpened && TileMap.GemsForHardExit <= gems)
            {
                OpenExits(ExitTypeEnum.Hard);
                _hardExitsOpened = true;
            }
        }

        private void ApplyStepOnTiles(List<Tile> tiles)
        {
            foreach (Tile tile in tiles)
            {
                if (!tile.Moved && (tile.MoveState == State.Moving || tile.MoveState == State.WillMove) && !tile.Destroyed)
                {
                    tile.Move();
                    tile.Moved = true;
                }

                if (!tile.Updated && !tile.Destroyed)
                {
                    if (tile.PreStep()) tile.Step();
                    tile.Updated = true;
                }
            }
        }

        private List<Tile> OrderTilesByPriority(Tile[,] tiles)
        {
            Dictionary<int, List<Tile>> priorities = new Dictionary<int, List<Tile>>();

            for (int j = (int)_gridSize.y - 1; j >= 0; --j)
            {
                // for (int i = 0; i < _gridSize.x; ++i)
                for (int i = (int)_gridSize.x - 1; i >= 0; --i)
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
            // GD.Print("*** STEP ***");

            var orderedBgTiles = OrderTilesByPriority(_tiles[TileLayerEnum.Background]);
            var orderedMdTiles = OrderTilesByPriority(_tiles[TileLayerEnum.Middle]);
            var orderedFgTiles = OrderTilesByPriority(_tiles[TileLayerEnum.Foreground]);

            ResetTickOnTiles(orderedBgTiles);
            ResetTickOnTiles(orderedMdTiles);
            ResetTickOnTiles(orderedFgTiles);

            ApplyStepOnTiles(orderedBgTiles);
            ApplyStepOnTiles(orderedMdTiles);
            ApplyStepOnTiles(orderedFgTiles);
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
            var pos = current.Position;
            var dir = current.NextDirection;
            var ipos = TileMap.WorldToMap(pos);
            var cpos = TileMap.MapToWorld(ipos);

            var offset = pos - cpos;

            if (offset.x == 32 && dir == Direction.Right) {
                ipos.x += 1;
            }

            if (offset.x == 0 && dir == Direction.Left) {
                ipos.x -= 1;
            }

            if (offset.y == 0 && dir == Direction.Up) {
                ipos.y -= 1;
            }

            if (offset.y == 32 && dir == Direction.Down) {
                ipos.y += 1;
            }

            return ipos;
        }

        public Vector2 GetTargetTilePosition(Tile current) {
            return TileMap.WorldToMap(current.TargetPosition);
        }

        public Tile GetNeighborTile(Tile current, Direction direction, TilePickEnum pickEnum = TilePickEnum.ForegroundFirst)
        {
            return GetTileAtGridPosition(GetNeighborPosition(current, direction), pickEnum);
        }

        public Vector2 GetNeighborPosition(Tile current, Direction direction)
        {
            return GetTileCurrentGridPosition(current) + TileUtils.GetDirectionVector(direction);
        }

        public Tile GetTileAtGridPosition(Vector2 gridPosition, TilePickEnum pickEnum = TilePickEnum.ForegroundFirst)
        {
            if (pickEnum != TilePickEnum.BackgroundOnly && pickEnum != TilePickEnum.MiddleOnly)
            {
                var tile = GetTileAtGridPositionAtLayer(gridPosition, TileLayerEnum.Foreground);
                if (tile != null) return tile;
            }

            if (pickEnum != TilePickEnum.ForegroundOnly && pickEnum != TilePickEnum.BackgroundOnly)
            {
                var tile = GetTileAtGridPositionAtLayer(gridPosition, TileLayerEnum.Middle);
                if (tile != null) return tile;
            }

            if (pickEnum != TilePickEnum.ForegroundOnly && pickEnum != TilePickEnum.MiddleOnly)
            {
                var tile = GetTileAtGridPositionAtLayer(gridPosition, TileLayerEnum.Background);
                if (tile != null) return tile;
            }

            return null;
        }

        public Tile GetTileAtGridPositionAtLayer(Vector2 gridPosition, TileLayerEnum layer)
        {
            if (gridPosition.x < 0 || gridPosition.x >= _gridSize.x || gridPosition.y < 0 || gridPosition.y >= _gridSize.y)
            {
                // Out of bounds
                return null;
            }

            return _tiles[layer][(int)gridPosition.y, (int)gridPosition.x];
        }

        public List<Tile> ListTilesAtGridPosition(Vector2 gridPosition)
        {
            var tiles = new List<Tile>();
            var fgTile = GetTileAtGridPosition(gridPosition, TilePickEnum.ForegroundOnly);
            var mdTile = GetTileAtGridPosition(gridPosition, TilePickEnum.MiddleOnly);
            var bgTile = GetTileAtGridPosition(gridPosition, TilePickEnum.BackgroundOnly);

            if (fgTile != null) tiles.Add(fgTile);
            if (mdTile != null) tiles.Add(mdTile);
            if (bgTile != null) tiles.Add(bgTile);
            return tiles;
        }

        public void DebugStepForward()
        {
            Running = false;
            if (_gameTicks == 0) {
                GameStep();
                _gameTicks++;
            } else {
                for (int i = 0; i < 4; ++i) {
                    GameStep();
                    _gameTicks++;
                }
            }
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
            _debugDraw.SetCurrentDebugTile(null);
        }

        public void RestartLevel()
        {
            GetTree().ReloadCurrentScene();
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

            if (Running) {
                for (int i = 0; i < 2; ++i) {
                    GameStep();
                    _gameTicks++;
                }

                if (_gameTicks % 120 == 0) {
                    _elapsedTime++;
                    EmitSignal(nameof(TimeUpdated), _elapsedTime);
                }
            }

            // if (Running && _physicsTicks % GameSpeed == 0)
            // {
            //     GameStep();
            //     _gameTicks++;

            //     // One second each 4 ticks
            //     if (_gameTicks % 8 == 0)
            //     {
            //         _elapsedTime++;
            //         EmitSignal(nameof(TimeUpdated), _elapsedTime);
            //     }
            // }
        }
    }
}
