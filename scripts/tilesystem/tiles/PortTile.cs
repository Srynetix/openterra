namespace Tiles
{
    public class PortTile : StaticTile
    {
        public Direction PortDirection;

        public PortTile()
        {
            TileLayer = TileLayerEnum.Background;
            ZIndex = 2;
            IsGate = true;
        }

        public virtual bool CheckDirection(Direction direction)
        {
            return direction == PortDirection;
        }

        public override bool CanBePassedThrough(Tile source, Direction direction)
        {
            if (!CheckDirection(direction)) return false;

            var neighbor = World.GetNeighborTile(this, direction);
            if (neighbor?.CanBePassedThrough(source, direction) != false)
            {
                return source.Player;
            }
            else
            {
                return false;
            }
        }
    }

    public class TwoWayPortTile : PortTile
    {
        public bool Horizontal;

        public override bool CheckDirection(Direction direction)
        {
            return Horizontal
                ? direction == Direction.Left || direction == Direction.Right
                : direction == Direction.Up || direction == Direction.Down;
        }
    }

    public class FourWayPortTile : PortTile
    {
        public override bool CheckDirection(Direction direction)
        {
            return true;
        }
    }
}
