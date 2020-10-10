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

        protected bool CanGoUp(CollisionStatus status)
        {
            return status.top == null;
        }

        protected bool CanGoDown(CollisionStatus status)
        {
            return status.bottom == null;
        }

        protected bool CanPushUp(CollisionStatus status)
        {
            if (status.top != null)
            {
                if (status.top.Movable && status.top.CanFall)
                {
                    var topStatus = World.GetTileCollisions(status.top);
                    if (topStatus.top == null)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool TryUp(CollisionStatus status)
        {
            if (CanGoUp(status))
            {
                WillMoveTowards(Direction.Up);
                _lastDirection = NextDirection;
                return true;
            }
            else if (CanPushUp(status))
            {
                var topTile = status.top;
                topTile.WillMoveTowards(Direction.Up);
                topTile.Updated = true;

                WillMoveTowards(Direction.Up);
                return true;
            }

            return false;
        }

        private bool TryDown(CollisionStatus status)
        {
            if (CanGoDown(status))
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
            var status = World.GetTileCollisions(this);
            if (_lastDirection == Direction.Up)
            {
                if (!TryUp(status) && !TryDown(status)) Stop();
            }
            else if (_lastDirection == Direction.Down)
            {
                if (!TryDown(status) && !TryUp(status)) Stop();
            }
        }

        public override string GenerateTileDebugInfo(CollisionStatus status)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("* Last direction: {0}\n", DebugDrawUtils.ShowTileDirection(_lastDirection));
            sb.AppendFormat("* Can go up:      {0}\n", DebugDrawUtils.ShowBool(CanGoUp(status)));
            sb.AppendFormat("* Can push up:    {0}\n", DebugDrawUtils.ShowBool(CanPushUp(status)));
            sb.AppendFormat("* Can go down:    {0}\n", DebugDrawUtils.ShowBool(CanGoUp(status)));
            return sb.ToString();
        }
    }
}
