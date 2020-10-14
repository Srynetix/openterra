using Godot;
using System.Text;

namespace Tiles
{
    public class FallingTile : Tile
    {
        private bool _wasFalling = false;

        public FallingTile()
        {
            Warpable = true;
            CanFall = true;
            Movable = true;
            CanRotate = true;
            RollDirection = RollDirectionEnum.Both;
            Priority = 1;
        }

        protected bool CanGoUp(CollisionStatus status)
        {
            return status.top == null || status.top.PassthroughMode == PassthroughModeEnum.All;
        }

        protected bool CanGoDown(CollisionStatus status)
        {
            return status.bottom == null || status.bottom.PassthroughMode == PassthroughModeEnum.All;
        }

        protected bool CanRollLeft(CollisionStatus status)
        {
            return status.bottom?.MakeRollLeft == true && status.bottom.MoveState == State.Stopped && status.left == null && status.bottomLeft == null;
        }

        protected bool CanRollRight(CollisionStatus status)
        {
            return status.bottom?.MakeRollRight == true && status.bottom.MoveState == State.Stopped && status.right == null && status.bottomRight == null;
        }

        public override string GenerateTileDebugInfo(CollisionStatus status)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("* Can go up:      {0}\n", DebugDrawUtils.ShowBool(CanGoUp(status)));
            sb.AppendFormat("* Can go down:    {0}\n", DebugDrawUtils.ShowBool(CanGoUp(status)));
            sb.AppendFormat("* Can roll left:  {0}\n", DebugDrawUtils.ShowBool(CanRollLeft(status)));
            sb.AppendFormat("* Can roll right: {0}\n", DebugDrawUtils.ShowBool(CanRollRight(status)));
            return sb.ToString();
        }

        public override void Step()
        {
            base.Step();

            // Check collisions
            var status = World.GetTileCollisions(this);
            var canGoDown = CanGoDown(status);

            if (canGoDown)
            {
                _wasFalling = true;
                WillMoveTowards(Direction.Down);
            }
            else if (!canGoDown && _wasFalling)
            {
                // Hit
                if (!IsLightweight)
                {
                    var bottomTile = status.GetTileAtDirection(Direction.Down);
                    if (bottomTile.CanExplode)
                    {
                        bottomTile.WillExplode();
                        bottomTile.Updated = true;
                    }
                    else if (IsHeavy && bottomTile.IsFragile)
                    {
                        // Crush
                        bottomTile.Pick();
                        bottomTile.Updated = true;
                    }
                }

                _wasFalling = false;
            }
            else if (CanRollLeft(status))
            {
                _wasFalling = false;
                WillMoveTowards(Direction.Left);
            }
            else if (CanRollRight(status))
            {
                _wasFalling = false;
                WillMoveTowards(Direction.Right);
            }
            else
            {
                _wasFalling = false;
                Stop();
            }
        }
    }
}
