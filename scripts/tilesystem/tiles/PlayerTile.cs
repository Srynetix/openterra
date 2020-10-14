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

        protected bool CanGoTowards(Direction direction)
        {
            var neighbor = World.GetNeighborTile(this, direction);
            return neighbor?.PassthroughMode != PassthroughModeEnum.Nothing || neighbor?.Pickable != false;
        }

        protected bool CanPushTowards(Direction direction)
        {
            var neighbor = World.GetNeighborTile(this, direction);
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
                if (CanGoTowards(playerDirection))
                {
                    if (!World.PlayerInput.Action.Pressed)
                    {
                        WillMoveTowards(playerDirection);
                    }

                    var neighbor = World.GetNeighborTile(this, playerDirection);
                    if (neighbor?.Pickable == true)
                    {
                        neighbor?.Pick();
                    }
                }
                else if (CanPushTowards(playerDirection))
                {
                    var tile = World.GetNeighborTile(this, playerDirection);
                    tile.WillBePushedTowards(playerDirection);

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
            sb.AppendFormat("* Can go up:    {0,-30} - Can push up:    {1,-30}\n", DebugDrawUtils.ShowBool(CanGoTowards(Direction.Up)), DebugDrawUtils.ShowBool(CanPushTowards(Direction.Up)));
            sb.AppendFormat("* Can go down:  {0,-30} - Can push down:  {1,-30}\n", DebugDrawUtils.ShowBool(CanGoTowards(Direction.Down)), DebugDrawUtils.ShowBool(CanPushTowards(Direction.Down)));
            sb.AppendFormat("* Can go left:  {0,-30} - Can push left:  {1,-30}\n", DebugDrawUtils.ShowBool(CanGoTowards(Direction.Left)), DebugDrawUtils.ShowBool(CanPushTowards(Direction.Left)));
            sb.AppendFormat("* Can go right: {0,-30} - Can push right: {1,-30}\n", DebugDrawUtils.ShowBool(CanGoTowards(Direction.Right)), DebugDrawUtils.ShowBool(CanPushTowards(Direction.Right)));
            return sb.ToString();
        }
    }
}
