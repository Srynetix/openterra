using Godot;
using System.Text;

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
            if (ExtensionDirection == ExtensionDirectionEnum.UpDown || ExtensionDirection == ExtensionDirectionEnum.FourWay)
            {
                // Expand up
                var top = GetNeighborAtDirection(Direction.Up);
                if (top == null)
                {
                    var tPos = World.GetNeighborPosition(this, Direction.Up);
                    var tile = World.CreateTile("ExtendingWallUD", tPos);
                    tile.NextDirection = Direction.Up;
                }

                // Expand down
                var bottom = GetNeighborAtDirection(Direction.Down);
                if (bottom == null)
                {
                    var tPos = World.GetNeighborPosition(this, Direction.Down);
                    var tile = World.CreateTile("ExtendingWallUD", tPos);
                    tile.NextDirection = Direction.Down;
                }
            }

            if (ExtensionDirection == ExtensionDirectionEnum.LeftRight || ExtensionDirection == ExtensionDirectionEnum.FourWay)
            {
                // Expand left
                var left = GetNeighborAtDirection(Direction.Left);
                if (left == null)
                {
                    var tPos = World.GetNeighborPosition(this, Direction.Left);
                    var tile = World.CreateTile("ExtendingWallLR", tPos);
                    tile.NextDirection = Direction.Left;
                }

                // Expand right
                var right = GetNeighborAtDirection(Direction.Right);
                if (right == null)
                {
                    var tPos = World.GetNeighborPosition(this, Direction.Right);
                    var tile = World.CreateTile("ExtendingWallLR", tPos);
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

            var source = _targetPosition - (World.GetDirectionVector(NextDirection) * World.TileMap.CellSize.x);
            float weight = _currentTick / (float)StepTicks;
            Position = source.LinearInterpolate(_targetPosition, weight);
            _currentTick = Mathf.Clamp(_currentTick + 1, 0, StepTicks);

            // Ok
            if (_currentTick == StepTicks)
            {
                _hasExpanded = true;
            }
        }

        public override string GenerateTileDebugInfo(CollisionStatus status)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("* Extension direction: {0}\n", DebugDrawUtils.ShowWithColor(ExtensionDirection, Colors.Yellow));
            return sb.ToString();
        }
    }
}
