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
            Priority = 3;
        }

        public override void _Ready()
        {
            base._Ready();
            AddToGroup("players");
        }

        protected bool CanActionTowards(Direction direction)
        {
            var neighbor = GetNeighborAtDirection(direction);
            return neighbor?.IsActionable == true;
        }

        protected bool CanGoTowards(Direction direction)
        {
            var neighbor = GetNeighborAtDirection(direction);
            return neighbor == null || (neighbor?.CanBePassedThrough(this, direction) == true);
        }

        protected bool CanPushTowards(Direction direction)
        {
            var neighbor = GetNeighborAtDirection(direction);
            if (neighbor != null)
            {
                return neighbor.CanBePushedTowards(this, direction);
            }
            else
            {
                return false;
            }
        }

        public override void EndMoveCallback()
        {
            if (World.PlayerInput.Bomb.Pressed)
            {
                var invDir = GetInvertedNextDirection();
                if (invDir != Direction.None)
                {
                    var tPos = World.GetNeighborPosition(this, invDir);
                    var tTile = World.GetTileAtGridPosition(tPos);
                    if (tTile == null)
                    {
                        // Spawn bomb
                        var bomb = (DynamiteTile)World.CreateTile("Dynamite", tPos);
                        bomb.Active = true;
                        bomb.WillExplode(World.GameTicks + 8);
                    }
                }
            }
        }

        public override void Step()
        {
            if (MoveState == State.Moving) {
                return;
            }

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
                if (CanGoTowards(playerDirection))
                {
                    var neighbor = GetNeighborAtDirection(playerDirection);
                    if (neighbor?.CanBePicked() == true)
                    {
                        neighbor?.Pick();
                    }

                    if (!World.PlayerInput.Action.Pressed)
                    {
                        if (neighbor?.IsGate == true)
                        {
                            // Handle warp
                            WillWarpTo(neighbor, playerDirection);
                        }
                        else if (neighbor?.IsSwitch == true)
                        {
                            // Handle back
                            var n = neighbor.GetNeighborAtDirection(playerDirection);
                            if (n?.CanBePassedThrough(this, playerDirection) != false)
                            {
                                WillWarpTo(neighbor, playerDirection);
                            }
                            else
                            {
                                WillWarpTo(neighbor, TileUtils.GetInvertedDirection(playerDirection));
                            }
                        }
                        else
                        {
                            WillMoveTowards(playerDirection);
                        }
                    }
                }
                else if (CanPushTowards(playerDirection))
                {
                    var tile = GetNeighborAtDirection(playerDirection);
                    tile.PushTowards(playerDirection);

                    if (!World.PlayerInput.Action.Pressed)
                    {
                        WillMoveTowards(playerDirection);
                    }
                }
                else if (CanActionTowards(playerDirection))
                {
                    var tile = GetNeighborAtDirection(playerDirection);
                    tile.DoAction();
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
            sb.AppendFormat("* Can go up:    {0,-30} - Can push up:    {1,-30}\n", DebugDrawUtils.ShowBool(CanGoTowards(Direction.Up)), DebugDrawUtils.ShowBool(CanPushTowards(Direction.Up)));
            sb.AppendFormat("* Can go down:  {0,-30} - Can push down:  {1,-30}\n", DebugDrawUtils.ShowBool(CanGoTowards(Direction.Down)), DebugDrawUtils.ShowBool(CanPushTowards(Direction.Down)));
            sb.AppendFormat("* Can go left:  {0,-30} - Can push left:  {1,-30}\n", DebugDrawUtils.ShowBool(CanGoTowards(Direction.Left)), DebugDrawUtils.ShowBool(CanPushTowards(Direction.Left)));
            sb.AppendFormat("* Can go right: {0,-30} - Can push right: {1,-30}\n", DebugDrawUtils.ShowBool(CanGoTowards(Direction.Right)), DebugDrawUtils.ShowBool(CanPushTowards(Direction.Right)));
            return sb.ToString();
        }
    }
}
