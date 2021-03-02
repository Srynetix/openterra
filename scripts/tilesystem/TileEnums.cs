namespace Tiles {
    /// <summary>
    /// Tile state.
    /// </summary>
    public enum State
    {
        Stopped,
        WillMove,
        DoneMoving,
        Moving
    }

    /// <summary>
    /// Tile direction.
    /// </summary>
    public enum Direction
    {
        None,
        Left,
        Right,
        Up,
        Down,
        UpLeft,
        UpRight,
        DownLeft,
        DownRight
    }

    public enum RollDirectionEnum
    {
        None,
        Left,
        Right,
        Both
    }

    public enum PassthroughModeEnum
    {
        Nothing,
        PlayerOnly,
        All
    }

    public enum PushDirectionLockEnum
    {
        None,
        Horizontal,
        Vertical
    }

    public enum KeyColorEnum
    {
        Green,
        Yellow,
        Blue,
        Red
    }

    public enum BarrierColorEnum
    {
        Blue,
        Pink
    }

    public enum ExitTypeEnum
    {
        Normal,
        Hard
    }

    public enum TilePickEnum
    {
        ForegroundFirst,
        BackgroundOnly,
        MiddleOnly,
        ForegroundOnly
    }

    public enum TileLayerEnum
    {
        Background,
        Middle,
        Foreground
    }

}
