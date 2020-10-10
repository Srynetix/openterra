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
    }
}
