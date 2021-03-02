using Godot;

namespace Tiles {
    public static class TileUtils {
        public static bool IsDirectionHorizontal(Direction d) {
            return d switch {
                Direction.Left => true,
                Direction.Right => true,
                _ => false
            };
        }

        public static bool IsDirectionVertical(Direction d) {
            return d switch {
                Direction.Up => true,
                Direction.Down => true,
                _ => false
            };
        }

        public static Vector2 GetDirectionVector(Direction d) {
            return d switch
            {
                Direction.Left => new Vector2(-1, 0),
                Direction.Right => new Vector2(1, 0),
                Direction.Up => new Vector2(0, -1),
                Direction.Down => new Vector2(0, 1),
                Direction.UpLeft => new Vector2(-1, -1),
                Direction.UpRight => new Vector2(1, -1),
                Direction.DownLeft => new Vector2(-1, 1),
                Direction.DownRight => new Vector2(1, 1),
                _ => new Vector2(0, 0),
            };
        }

        public static Direction GetInvertedDirection(Direction d) {
            return d switch
            {
                Direction.Left => Direction.Right,
                Direction.Right => Direction.Left,
                Direction.Up => Direction.Down,
                Direction.Down => Direction.Up,
                Direction.UpLeft => Direction.DownRight,
                Direction.UpRight => Direction.DownLeft,
                Direction.DownLeft => Direction.UpRight,
                Direction.DownRight => Direction.UpLeft,
                _ => Direction.None,
            };
        }
    }
}
