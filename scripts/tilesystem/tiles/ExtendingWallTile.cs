using Godot;

namespace Tiles
{
    public class ExtendingWallTile : ControlledTile
    {
        public enum ExtensionDirectionEnum
        {
            UpDown,
            LeftRight,
            FourWay
        }

        public ExtensionDirectionEnum ExtensionDirection;
        private bool _hasExpanded;
        private int _tickCount;

        public override void Step()
        {
            base.Step();

            if (_tickCount < 1)
            {
                _tickCount++;
                return;
            }

            _tickCount = 0;
            var status = World.GetTileCollisions(this);
            if (ExtensionDirection == ExtensionDirectionEnum.UpDown || ExtensionDirection == ExtensionDirectionEnum.FourWay)
            {
                // Expand up
                if (status.top == null)
                {
                    var tPos = World.GetNeighborPosition(this, Direction.Up);
                    var idx = World.TileMap.TileSet.FindTileByName("ExtendingWallUD");
                    var tile = World.CreateTile(idx, tPos);
                    tile.NextDirection = Direction.Up;
                }

                // Expand down
                if (status.bottom == null)
                {
                    var tPos = World.GetNeighborPosition(this, Direction.Down);
                    var idx = World.TileMap.TileSet.FindTileByName("ExtendingWallUD");
                    var tile = World.CreateTile(idx, tPos);
                    tile.NextDirection = Direction.Down;
                }
            }

            if (ExtensionDirection == ExtensionDirectionEnum.LeftRight || ExtensionDirection == ExtensionDirectionEnum.FourWay)
            {
                // Expand left
                if (status.left == null)
                {
                    var tPos = World.GetNeighborPosition(this, Direction.Left);
                    var idx = World.TileMap.TileSet.FindTileByName("ExtendingWallLR");
                    var tile = World.CreateTile(idx, tPos);
                    tile.NextDirection = Direction.Left;
                }

                // Expand right
                if (status.right == null)
                {
                    var tPos = World.GetNeighborPosition(this, Direction.Right);
                    var idx = World.TileMap.TileSet.FindTileByName("ExtendingWallLR");
                    var tile = World.CreateTile(idx, tPos);
                    tile.NextDirection = Direction.Right;
                }
            }
        }

        public override void _Process(float delta)
        {
            if (_hasExpanded)
            {
                return;
            }

            var source = _targetPosition - World.GetDirectionVector(NextDirection) * World.TileMap.CellSize.x;
            float weight = (float)_currentTick / (float)StepTicks;
            Position = source.LinearInterpolate(_targetPosition, weight);
            _currentTick = Mathf.Clamp(_currentTick + 1, 0, StepTicks);

            // Ok
            if (_currentTick == StepTicks)
            {
                _hasExpanded = true;
            }
        }
    }
}