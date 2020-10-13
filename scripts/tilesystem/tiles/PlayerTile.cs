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

        protected bool CanGoTowards(CollisionStatus status, Direction direction)
        {
            var neighbor = status.GetTileAtDirection(direction);
            return neighbor?.PassthroughMode != PassthroughModeEnum.Nothing || neighbor?.Pickable != false;
        }

        protected bool CanPushTowards(CollisionStatus status, Direction direction)
        {
            var neighbor = status.GetTileAtDirection(direction);
            if (neighbor?.Movable == true && neighbor?.MoveState != State.WillMove)
            {
                var tNeighbor = World.GetNeighborTile(neighbor, direction);
                if (tNeighbor != null)
                {
                    return tNeighbor.PassthroughMode == PassthroughModeEnum.All;
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
                if (CanGoTowards(status, playerDirection))
                {
                    if (!World.PlayerInput.Action.Pressed)
                    {
                        WillMoveTowards(playerDirection);
                    }

                    var neighbor = status.GetTileAtDirection(playerDirection);
                    if (neighbor?.Pickable == true)
                    {
                        neighbor?.Pick();
                    }
                }
                else if (CanPushTowards(status, playerDirection))
                {
                    var tile = status.GetTileAtDirection(playerDirection);
                    tile.WillMoveTowards(playerDirection);
                    tile.Updated = true;

                    if (!World.PlayerInput.Action.Pressed)
                    {
                        WillMoveTowards(playerDirection);
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
            sb.AppendFormat("* Can go up:    {0,-30} - Can push up:    {1,-30}\n", DebugDrawUtils.ShowBool(CanGoTowards(status, Direction.Up)), DebugDrawUtils.ShowBool(CanPushTowards(status, Direction.Up)));
            sb.AppendFormat("* Can go down:  {0,-30} - Can push down:  {1,-30}\n", DebugDrawUtils.ShowBool(CanGoTowards(status, Direction.Down)), DebugDrawUtils.ShowBool(CanPushTowards(status, Direction.Down)));
            sb.AppendFormat("* Can go left:  {0,-30} - Can push left:  {1,-30}\n", DebugDrawUtils.ShowBool(CanGoTowards(status, Direction.Left)), DebugDrawUtils.ShowBool(CanPushTowards(status, Direction.Left)));
            sb.AppendFormat("* Can go right: {0,-30} - Can push right: {1,-30}\n", DebugDrawUtils.ShowBool(CanGoTowards(status, Direction.Right)), DebugDrawUtils.ShowBool(CanPushTowards(status, Direction.Right)));
            return sb.ToString();
        }
    }
}
