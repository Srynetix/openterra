namespace Tiles
{
    public class CollisionStatus
    {
        public Tile top;
        public Tile bottom;
        public Tile left;
        public Tile right;
        public Tile topLeft;
        public Tile topRight;
        public Tile bottomLeft;
        public Tile bottomRight;

        public CollisionStatus()
        {
            top = null;
            bottom = null;
            left = null;
            right = null;
            topLeft = null;
            topRight = null;
            bottomLeft = null;
            bottomRight = null;
        }

        public Tile GetTileAtDirection(Tile.Direction direction)
        {
            return direction switch
            {
                Tile.Direction.Left => left,
                Tile.Direction.Up => top,
                Tile.Direction.Right => right,
                Tile.Direction.Down => bottom,
                Tile.Direction.UpLeft => topLeft,
                Tile.Direction.UpRight => topRight,
                Tile.Direction.DownLeft => bottomLeft,
                Tile.Direction.DownRight => bottomRight,
                _ => null
            };
        }
    }
}
