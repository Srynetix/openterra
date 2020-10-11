using System.Text;

namespace Tiles
{
    public class PlayerTile : Tile
    {
        public PlayerTile()
        {
            Player = true;
            Warpable = true;
            CanExplode = true;
        }

        public override void _Ready()
        {
            base._Ready();
            AddToGroup("players");
        }

        protected bool CanGoUp(CollisionStatus status)
        {
            return status.top?.PassthroughMode != PassthroughModeEnum.Nothing || status.top?.Pickable != false;
        }

        protected bool CanGoDown(CollisionStatus status)
        {
            return status.bottom?.PassthroughMode != PassthroughModeEnum.Nothing || status.bottom?.Pickable != false;
        }

        protected bool CanGoRight(CollisionStatus status)
        {
            return status.right?.PassthroughMode != PassthroughModeEnum.Nothing || status.right?.Pickable != false;
        }

        protected bool CanGoLeft(CollisionStatus status)
        {
            return status.left?.PassthroughMode != PassthroughModeEnum.Nothing || status.left?.Pickable != false;
        }

        protected bool CanPushLeft(CollisionStatus status)
        {
            if (status.left?.Movable == true && status.left?.MoveState != State.WillMove)
            {
                var statusLeft = World.GetNeighborTile(status.left, Direction.Left);
                if (statusLeft != null)
                {
                    return statusLeft.PassthroughMode == PassthroughModeEnum.All;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        protected bool CanPushRight(CollisionStatus status)
        {
            if (status.right?.Movable == true && status.right?.MoveState != State.WillMove)
            {
                var statusRight = World.GetNeighborTile(status.right, Direction.Right);
                if (statusRight != null)
                {
                    return statusRight.PassthroughMode == PassthroughModeEnum.All;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        protected bool CanPushUp(CollisionStatus status)
        {
            if (status.top?.Movable == true && !status.top.CanFall)
            {
                var statusTop = World.GetNeighborTile(status.top, Direction.Up);
                if (statusTop != null)
                {
                    return statusTop.PassthroughMode == PassthroughModeEnum.All;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        protected bool CanPushDown(CollisionStatus status)
        {
            if (status.bottom?.Movable == true && !status.bottom.CanFall)
            {
                var statusBottom = World.GetNeighborTile(status.bottom, Direction.Down);
                if (statusBottom != null)
                {
                    return statusBottom.PassthroughMode == PassthroughModeEnum.All;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        public override void EndMoveCallback()
        {
            if (World.PlayerInput.Bomb.Pressed)
            {
                var invDir = GetInvertedDirection();
                if (invDir != Direction.None)
                {
                    var tPos = World.GetNeighborPosition(this, invDir);
                    var tTile = World.GetTileAtGridPosition(tPos);
                    if (tTile == null)
                    {
                        // Spawn bomb
                        var idx = World.TileMap.TileSet.FindTileByName("Dynamite");
                        var bomb = (DynamiteTile)World.CreateTile(idx, tPos);
                        bomb.WillExplode(World.GameTicks + 8);
                    }
                }
            }
        }

        public override void Step()
        {
            base.Step();

            Direction playerDirection = Direction.None;
            if (World.PlayerInput.Right.Pressed)
            {
                playerDirection = Direction.Right;
            }
            else if (World.PlayerInput.Left.Pressed)
            {
                playerDirection = Direction.Left;
            }
            else if (World.PlayerInput.Up.Pressed)
            {
                playerDirection = Direction.Up;
            }
            else if (World.PlayerInput.Down.Pressed)
            {
                playerDirection = Direction.Down;
            }

            if (World.PlayerInput.Explode.Pressed)
            {
                Explode();
                return;
            }

            if (playerDirection != Direction.None)
            {
                var status = World.GetTileCollisions(this);

                if (playerDirection == Direction.Left)
                {
                    if (CanGoLeft(status))
                    {
                        if (!World.PlayerInput.Action.Pressed)
                        {
                            WillMoveTowards(Direction.Left);
                        }

                        if (status.left?.Pickable == true)
                        {
                            status.left.Pick();
                        }
                    }
                    else if (CanPushLeft(status))
                    {
                        var leftTile = status.left;
                        leftTile.WillMoveTowards(Direction.Left);
                        leftTile.Updated = true;

                        if (!World.PlayerInput.Action.Pressed)
                        {
                            WillMoveTowards(Direction.Left);
                        }
                    }
                }
                else if (playerDirection == Direction.Right)
                {
                    if (CanGoRight(status))
                    {
                        if (!World.PlayerInput.Action.Pressed)
                        {
                            WillMoveTowards(Direction.Right);
                        }

                        if (status.right?.Pickable == true)
                        {
                            status.right.Pick();
                        }
                    }
                    else if (CanPushRight(status))
                    {
                        var rightTile = status.right;
                        rightTile.WillMoveTowards(Direction.Right);
                        rightTile.Updated = true;

                        if (!World.PlayerInput.Action.Pressed)
                        {
                            WillMoveTowards(Direction.Right);
                        }
                    }
                }
                else if (playerDirection == Direction.Up)
                {
                    if (CanGoUp(status))
                    {
                        if (!World.PlayerInput.Action.Pressed)
                        {
                            WillMoveTowards(Direction.Up);
                        }

                        if (status.top?.Pickable == true)
                        {
                            status.top.Pick();
                        }
                    }
                    else if (CanPushUp(status))
                    {
                        var topTile = status.top;
                        topTile.WillMoveTowards(Direction.Up);
                        topTile.Updated = true;

                        if (!World.PlayerInput.Action.Pressed)
                        {
                            WillMoveTowards(Direction.Up);
                        }
                    }
                }
                else if (playerDirection == Direction.Down)
                {
                    if (CanGoDown(status))
                    {
                        if (!World.PlayerInput.Action.Pressed)
                        {
                            WillMoveTowards(Direction.Down);
                        }

                        if (status.bottom?.Pickable == true)
                        {
                            status.bottom.Pick();
                        }
                    }
                    else if (CanPushDown(status))
                    {
                        var bottomTile = status.bottom;
                        bottomTile.WillMoveTowards(Direction.Down);
                        bottomTile.Updated = true;

                        if (!World.PlayerInput.Action.Pressed)
                        {
                            WillMoveTowards(Direction.Down);
                        }
                    }
                }
            }
            else
            {
                Stop();
            }
        }

        public override void _Process(float delta)
        {
            base._Process(delta);

            // Orient sprite depending on direction
            if (NextDirection == Direction.Left)
            {
                GetSprite().FlipH = true;
            }
            else if (NextDirection == Direction.Right)
            {
                GetSprite().FlipH = false;
            }
        }

        public override string GenerateTileDebugInfo(CollisionStatus status)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("* Can go up:    {0,-30} - Can push up:    {1,-30}\n", DebugDrawUtils.ShowBool(CanGoUp(status)), DebugDrawUtils.ShowBool(CanPushUp(status)));
            sb.AppendFormat("* Can go down:  {0,-30} - Can push down:  {1,-30}\n", DebugDrawUtils.ShowBool(CanGoDown(status)), DebugDrawUtils.ShowBool(CanPushDown(status)));
            sb.AppendFormat("* Can go left:  {0,-30} - Can push left:  {1,-30}\n", DebugDrawUtils.ShowBool(CanGoLeft(status)), DebugDrawUtils.ShowBool(CanPushLeft(status)));
            sb.AppendFormat("* Can go right: {0,-30} - Can push right: {1,-30}\n", DebugDrawUtils.ShowBool(CanGoRight(status)), DebugDrawUtils.ShowBool(CanPushRight(status)));
            return sb.ToString();
        }
    }
}
