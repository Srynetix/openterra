using Godot;
using System.Text;

namespace Tiles
{
    public class WallTile : StaticTile { }

    public class RoundedWallTile : WallTile
    {
        public RoundedWallTile()
        {
            RollDirection = RollDirectionEnum.Both;
        }

        public override string GenerateTileDebugInfo(CollisionStatus status)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("* Roll direction: {0}\n", DebugDrawUtils.ShowWithColor(RollDirection, Colors.Yellow));
            return sb.ToString();
        }
    }

    public class SteelWallTile : WallTile
    {
        public SteelWallTile()
        {
            Indestructible = true;
        }
    }

    public class OneTimePassageTile : WallTile
    {
        public bool Closed
        {
            get => _closed;
            set
            {
                _closed = value;
                UpdateTileSprite();
            }
        }

        private bool _closed;

        public OneTimePassageTile()
        {
            Background = true;
            ZIndex = 0;
        }

        public override bool CanBePassedThrough(Tile source, Direction direction)
        {
            return !Closed && source.Player;
        }

        public override void Step()
        {
            base.Step();
            if (Closed) return;

            var thisPosition = World.GetTileCurrentGridPosition(this);
            var fgTile = World.GetTileAtGridPosition(thisPosition, TilePickEnum.ForegroundOnly);
            if (fgTile != null)
            {
                Closed = true;
            }
        }

        private void UpdateTileSprite()
        {
            if (Closed && _sprite != null)
            {
                var idx = World.TileMap.TileSet.FindTileByName("Wall");
                _sprite.RegionRect = World.TileMap.TileSet.TileGetRegion(idx);
            }
        }
    }

    public class FragileWallTile : WallTile
    {
        public bool _willBreak;
        public int _breakAtTick = -1;

        public FragileWallTile()
        {
            IsFragile = true;
        }

        public void WillBreakWall()
        {
            if (_breakAtTick != -1) return;
            _breakAtTick = World.GameTicks + 1;
        }

        public void BreakWall()
        {
            _willBreak = true;
            foreach (Direction direction in FourDirections)
            {
                var tile = World.GetNeighborTile(this, direction);
                if (tile is FragileWallTile fTile && fTile._breakAtTick == -1)
                {
                    fTile.WillBreakWall();
                }
            }

            var pos = World.GetTileCurrentGridPosition(this);
            World.RemoveTile(this);
            World.CreateTile("Explosion", pos);
        }

        public override void Step()
        {
            base.Step();

            if (_breakAtTick == World.GameTicks)
            {
                BreakWall();
            }
        }
    }

    public class StarBlockTile : FragileWallTile
    {
        public StarBlockTile()
        {
            IsActionable = true;
        }

        public override void DoAction()
        {
            if (_willBreak) return;
            BreakWall();
        }
    }
}