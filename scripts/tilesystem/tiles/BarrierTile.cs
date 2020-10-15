namespace Tiles
{
    public class BarrierTile : StaticTile
    {
        public bool Active
        {
            get => _active;
            set
            {
                _active = value;
                UpdateTileSprite();
            }
        }

        public BarrierColorEnum BarrierColor;

        private bool _active;

        public BarrierTile()
        {
            TileLayer = TileLayerEnum.Background;
            Indestructible = true;
            ZIndex = 0;
        }

        public override void _Ready()
        {
            base._Ready();
            AddToGroup(BarrierColor.ToString() + "_barrier");
            UpdateTileSprite();
        }

        public override bool CanBePassedThrough(Tile source, Direction direction)
        {
            return !Active;
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
            string tileName = "";
            if (Active)
            {
                tileName += "Active";
            }
            else
            {
                tileName += "Disabled";
            }
            tileName += BarrierColor.ToString();
            tileName += "Barrier";
            return tileName;
        }
    }
}
