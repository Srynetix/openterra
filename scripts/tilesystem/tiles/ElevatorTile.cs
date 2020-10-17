using System.Text;

namespace Tiles
{
    public class ElevatorTile : ControlledTile
    {
        private Direction _lastDirection = Direction.Up;

        public ElevatorTile()
        {
            RollDirection = RollDirectionEnum.Both;
        }

        protected bool CanGoUp()
        {
            return GetNeighborAtDirection(Direction.Up) == null;
        }

        protected bool CanGoDown()
        {
            return GetNeighborAtDirection(Direction.Down) == null;
        }

        protected bool CanPushUp()
        {
            var neighbor = World.GetNeighborTile(this, Direction.Up);
            if (neighbor != null)
            {
                return neighbor.CanBePushedTowards(this, Direction.Up);
            }
            else
            {
                return false;
            }
        }

        private bool TryUp()
        {
            if (CanGoUp())
            {
                WillMoveTowards(Direction.Up);
                _lastDirection = NextDirection;
                return true;
            }
            else if (CanPushUp())
            {
                var topTile = World.GetNeighborTile(this, Direction.Up);
                topTile.PushTowards(Direction.Up);

                WillMoveTowards(Direction.Up);
                return true;
            }

            return false;
        }

        private bool TryDown()
        {
            if (CanGoDown())
            {
                WillMoveTowards(Direction.Down);
                _lastDirection = NextDirection;
                return true;
            }

            return false;
        }

        public override void Step()
        {
            base.Step();

            // Check collisions
            if (_lastDirection == Direction.Up)
            {
                if (!TryUp() && !TryDown()) Stop();
            }
            else if (_lastDirection == Direction.Down)
            {
                if (!TryDown() && !TryUp()) Stop();
            }
        }

        public override string GenerateTileDebugInfo(CollisionStatus status)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("* Last direction: {0}\n", DebugDrawUtils.ShowTileDirection(_lastDirection));
            sb.AppendFormat("* Can go up:      {0}\n", DebugDrawUtils.ShowBool(CanGoUp()));
            sb.AppendFormat("* Can push up:    {0}\n", DebugDrawUtils.ShowBool(CanPushUp()));
            sb.AppendFormat("* Can go down:    {0}\n", DebugDrawUtils.ShowBool(CanGoUp()));
            return sb.ToString();
        }
    }
}
