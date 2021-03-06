namespace Tiles
{
    public class ExitTile : StaticTile
    {
        public bool Opened
        {
            get => _opened;
            set
            {
                _opened = value;
                UpdateTileSprite();
            }
        }

        public ExitTypeEnum ExitType;

        private bool _opened;

        public ExitTile()
        {
            TileLayer = TileLayerEnum.Foreground;
        }

        public override void _Ready()
        {
            base._Ready();
            AddToGroup(ExitType.ToString() + "_exit");
            UpdateTileSprite();
        }

        public override bool CanBePassedThrough(Tile source, Direction direction)
        {
            return source.Player && _opened;
        }

        public override void Step()
        {
            var tile = GetOverlappingTile(TileLayerEnum.Middle);
            if (tile?.Player == true)
            {
                tile.Pick();
            }
        }

        private void UpdateTileSprite()
        {
            if (_sprite != null)
            {
                int idx = World.TileMap.TileSet.FindTileByName(GetTileName());
                _sprite.RegionRect = World.TileMap.TileSet.TileGetRegion(idx);
            }
        }

        private string GetTileName()
        {
            string name = "";
            if (ExitType == ExitTypeEnum.Hard)
            {
                name += "Hard";
            }

            name += "Exit";

            if (Opened)
            {
                name += "Open";
            }
            else
            {
                name += "Closed";
            }

            return name;
        }
    }
}
