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
            Priority = 2;
        }

        protected bool CanGoUp()
        {
            var top = GetNeighborAtDirection(Direction.Up);
            return top?.CanBePassedThrough(this, Direction.Up) != false;
        }

        protected bool CanGoDown()
        {
            var bottom = GetNeighborAtDirection(Direction.Down);
            return bottom?.CanBePassedThrough(this, Direction.Down) != false;
        }

        protected bool CanRollLeft()
        {
            var bottom = GetNeighborAtDirection(Direction.Down);
            var left = GetNeighborAtDirection(Direction.Left);
            var bottomLeft = GetNeighborAtDirection(Direction.DownLeft);
            return bottom?.MakeRollLeft == true && bottom.MoveState == State.Stopped && left == null && bottomLeft == null;
        }

        protected bool CanRollRight()
        {
            var bottom = GetNeighborAtDirection(Direction.Down);
            var right = GetNeighborAtDirection(Direction.Right);
            var bottomRight = GetNeighborAtDirection(Direction.DownRight);
            return bottom?.MakeRollRight == true && bottom.MoveState == State.Stopped && right == null && bottomRight == null;
        }

        public override string GenerateTileDebugInfo(CollisionStatus status)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("* Can go up:      {0}\n", DebugDrawUtils.ShowBool(CanGoUp()));
            sb.AppendFormat("* Can go down:    {0}\n", DebugDrawUtils.ShowBool(CanGoUp()));
            sb.AppendFormat("* Can roll left:  {0}\n", DebugDrawUtils.ShowBool(CanRollLeft()));
            sb.AppendFormat("* Can roll right: {0}\n", DebugDrawUtils.ShowBool(CanRollRight()));
            sb.AppendFormat("* Fall ticks:     {0}\n", DebugDrawUtils.ShowWithColor(_fallTicks, Colors.Yellow));
            return sb.ToString();
        }

        public override void Step()
        {
            base.Step();

            if (MoveState == State.Moving) {
                // Moving
                Updated = true;
                return;
            }

            // if (TrappedInSand)
            // {
            //     _fallTicks = 0;
            //     Stop();
            //     return;
            // }

            var canGoDown = CanGoDown();

            if (canGoDown)
            {
                _fallTicks++;
                WillMoveTowards(Direction.Down);
            }
            // else if (!canGoDown && _fallTicks > 0)
            // {
            //     // Hit
            //     // if (!IsLightweight)
            //     // {
            //     //     var bottomTile = GetNeighborAtDirection(Direction.Down);
            //     //     if (bottomTile.CanExplode)
            //     //     {
            //     //         bottomTile.Explode();
            //     //     }
            //     //     else if (IsHeavy && bottomTile.IsFragile)
            //     //     {
            //     //         // Crush
            //     //         bottomTile.Pick();
            //     //         WillMoveTowards(Direction.Down);
            //     //     }
            //     // }

            //     _fallTicks = 0;
            // }
            else if (CanRollLeft())
            {
                _fallTicks = 0;
                WillMoveTowards(Direction.Left);
            }
            else if (CanRollRight())
            {
                _fallTicks = 0;
                WillMoveTowards(Direction.Right);
            }
            else
            {
                _fallTicks = 0;
                Stop();
                // StateMachineIdx = (StateMachineIdx + 1) % 3;
            }

            // Check collisions
            // var canGoDown = CanGoDown();

            // if (StateMachineIdx == 0 && CanRollLeft())
            // {
            //     _fallTicks = 0;
            //     WillMoveTowards(Direction.Left);
            //     StateMachineIdx = 0;
            // }
            // else if (StateMachineIdx == 1 && CanRollRight())
            // {
            //     _fallTicks = 0;
            //     WillMoveTowards(Direction.Right);
            //     StateMachineIdx = 0;
            // }
            // else if (StateMachineIdx == 2 && canGoDown)
            // {
            //     _fallTicks++;
            //     WillMoveTowards(Direction.Down);
            // }
            // else if (StateMachineIdx == 2 && !canGoDown && _fallTicks > 0)
            // {
            //     // Hit
            //     if (!IsLightweight)
            //     {
            //         var bottomTile = GetNeighborAtDirection(Direction.Down);
            //         if (bottomTile.CanExplode)
            //         {
            //             bottomTile.Explode();
            //         }
            //         else if (IsHeavy && bottomTile.IsFragile)
            //         {
            //             // Crush
            //             bottomTile.Pick();
            //             WillMoveTowards(Direction.Down);
            //         }
            //     }

            //     _fallTicks = 0;
            // }
            // else
            // {
            //     _fallTicks = 0;
            //     Stop();
            //     StateMachineIdx = (StateMachineIdx + 1) % 3;
            // }
        }
    }
}
