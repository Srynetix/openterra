using Godot;
using System.Text;

namespace Tiles
{
    public class FallingTile : Tile
    {
        private int _fallTicks;

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
            sb.AppendFormat("* Fall ticks:     {0}\n", DebugDrawUtils.ShowWithColor(_fallTicks, Colors.Yellow));
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
                _fallTicks++;
                WillMoveTowards(Direction.Down);
            }
            else if (!canGoDown && _fallTicks > 0)
            {
                // Hit
                if (!IsLightweight)
                {
                    var bottomTile = status.GetTileAtDirection(Direction.Down);
                    if (bottomTile.CanExplode)
                    {
                        bottomTile.Explode();
                    }
                    else if (IsHeavy && bottomTile.IsFragile)
                    {
                        // Crush
                        bottomTile.Pick();
                        WillMoveTowards(Direction.Down);
                    }
                }

                _fallTicks = 0;
            }
            else if (CanRollLeft(status))
            {
                _fallTicks = 0;
                WillMoveTowards(Direction.Left);
            }
            else if (CanRollRight(status))
            {
                _fallTicks = 0;
                WillMoveTowards(Direction.Right);
            }
            else
            {
                _fallTicks = 0;
                Stop();
            }
        }
    }
}
