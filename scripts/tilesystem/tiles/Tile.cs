using Godot;

namespace Tiles
{
    /// <summary>
    /// Tile.
    /// </summary>
    public class Tile : Node2D
    {
        /// <summary>
        /// Tile state.
        /// </summary>
        public enum State
        {
            Stopped,
            WillMove,
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

        public static Direction[] AllDirections = {
            Direction.Left,
            Direction.Right,
            Direction.Up,
            Direction.Down,
            Direction.UpLeft,
            Direction.UpRight,
            Direction.DownLeft,
            Direction.DownRight,
        };

        public static Direction[] FourDirections = {
            Direction.Left,
            Direction.Right,
            Direction.Up,
            Direction.Down,
        };

        /// <summary>Tile type.</summary>
        public string Type;

        /// <summary>Is pickable.</summary>
        public bool Pickable;

        /// <summary>Can fall.</summary>
        public bool CanFall;
        public bool CanExplode;
        public bool CanRotate;
        public bool Indestructible;
        public bool IsHeavy;
        public bool IsLightweight;
        public bool IsFragile;
        public PushDirectionLockEnum PushDirectionLock;

        /// <summary>Is controlled</summary>
        public bool Controlled;

        /// <summary>Make roll left.</summary>
        public bool MakeRollLeft;

        /// <summary>Make roll right.</summary>
        public bool MakeRollRight;

        /// <summary>Movable.</summary>
        public bool Movable;

        public bool IsWarp;

        /// <summary>Tile layer.</summary>
        public TileLayerEnum TileLayer = TileLayerEnum.Middle;

        /// <summary>Is player.</summary>
        public bool Player;

        public PassthroughModeEnum PassthroughMode;

        /// <summary>Move state.</summary>
        public State MoveState;

        /// <summary>Next direction.</summary>
        public Direction NextDirection;

        public Tile WarpTarget;
        public int WillExplodeAtTick = -1;

        public bool Picked;
        public bool Destroyed;
        public int Priority;
        public bool Warpable;
        public bool IsGate;
        public bool IsSwitch;
        public bool IsActionable;
        public bool TrappedInSand;

        /// <summary>Step ticks</summary>
        public int StepTicks = 10;

        public RollDirectionEnum RollDirection
        {
            get => _rollDirection;
            set
            {
                _rollDirection = value;
                UpdateRollDirection();
            }
        }

        public Vector2 SourcePosition
        {
            get => _sourcePosition;
        }

        public Vector2 TargetPosition
        {
            get => _targetPosition;
            set
            {
                _sourcePosition = Position;
                _targetPosition = value;
            }
        }

        public float TargetRotation
        {
            get => _targetRotation;
            set
            {
                _sourceRotation = Rotation;
                _targetRotation = value;
            }
        }

        public Vector2 TilePosition
        {
            get => World.GetTileCurrentGridPosition(this);
        }

        public TileWorld World { get; set; }
        public bool Updated { get; set; }

        protected Vector2 _sourcePosition;
        protected Vector2 _targetPosition;
        protected float _sourceRotation;
        protected float _targetRotation;
        protected int _currentTick;
        protected RollDirectionEnum _rollDirection;
        protected Sprite _sprite;

        public Tile()
        {
            ZIndex = 1;
        }

        public void ResetTick()
        {
            _targetRotation %= Mathf.Pi * 2;
            Position = _sourcePosition = _targetPosition;
            Rotation = _sourceRotation = _targetRotation;
            Updated = false;
            _currentTick = 0;
        }

        public virtual bool CanBePicked()
        {
            return Pickable;
        }

        public virtual bool CanBePassedThrough(Tile source, Tile.Direction direction)
        {
            if (source.Player && CanBePicked()) return true;

            if (!source.Player)
            {
                return PassthroughMode == PassthroughModeEnum.All;
            }

            return PassthroughMode != PassthroughModeEnum.Nothing;
        }

        public virtual void DoAction() { }

        public bool CanFallCheck()
        {
            if (!CanFall) return false;

            var neighbor = World.GetNeighborTile(this, Direction.Down);
            if (neighbor == null)
            {
                return true;
            }

            return neighbor.CanBePassedThrough(this, Direction.Down);
        }

        public virtual bool CanBePushedTowards(Tile pusher, Direction direction)
        {
            if (!Movable)
            {
                return false;
            }

            // Elevator special case
            var neighbor = World.GetNeighborTile(this, direction);
            if (pusher.Type == "Elevator" && direction == Direction.Up)
            {
                if (Movable && CanFall)
                {
                    if (neighbor != null)
                    {
                        return neighbor.CanBePushedTowards(this, direction);
                    }
                    else
                    {
                        return true;
                    }
                }
            }

            // Can not push up a tile that can fall, unless its lightweight
            if (direction == Direction.Up && CanFall && !IsLightweight)
            {
                return false;
            }

            // Can not push sides an object that can fall down
            if ((direction == Direction.Left || direction == Direction.Right) && CanFallCheck())
            {
                return false;
            }

            // Normal case
            if (Movable && MoveState != State.WillMove)
            {
                // Check lock
                if (PushDirectionLock == PushDirectionLockEnum.Vertical && (direction == Direction.Left || direction == Direction.Right))
                {
                    return false;
                }
                else if (PushDirectionLock == PushDirectionLockEnum.Horizontal && (direction == Direction.Up || direction == Direction.Down))
                {
                    return false;
                }

                // Check neighbor
                if (neighbor == null)
                {
                    return true;
                }
                else
                {
                    if (IsLightweight && neighbor.IsLightweight)
                    {
                        return neighbor.CanBePushedTowards(this, direction);
                    }
                    else
                    {
                        return neighbor.CanBePassedThrough(this, direction);
                    }
                }
            }

            return false;
        }

        public Direction GetRandomDirection() {
            return (int)GD.RandRange(0, 4) switch {
                0 => Direction.Left,
                1 => Direction.Right,
                2 => Direction.Up,
                3 => Direction.Down,
                _ => Direction.None
            };
        }

        public void PushTowards(Direction direction)
        {
            var neighbor = World.GetNeighborTile(this, direction);
            if (neighbor?.CanBePushedTowards(this, direction) == true)
            {
                neighbor.PushTowards(direction);
            }

            MoveTowards(direction);
            Updated = true;
        }

        public Sprite GetSprite()
        {
            return GetNode<Sprite>("Sprite");
        }

        public void MoveTowards(Direction direction)
        {
            WillMoveTowards(direction);
            Move();
            Updated = true;
        }

        public void WillMoveTowards(Direction direction)
        {
            MoveState = State.WillMove;
            NextDirection = direction;
            Updated = true;
        }

        public void WillWarpTo(Tile target, Direction direction)
        {
            MoveState = State.WillMove;
            NextDirection = direction;
            WarpTarget = target;
            Updated = true;
        }

        public bool WillExplodeSoon()
        {
            return WillExplodeAtTick != -1;
        }

        public void WillExplode(int when = -1)
        {
            if (when == -1)
            {
                when = World.GameTicks + 1;
            }

            WillExplodeAtTick = when;
            Updated = true;
        }

        public void Explode()
        {
            World.SpawnExplosionAtTile(this);
            Updated = true;
        }

        public Direction GetInvertedDirection()
        {
            return World.GetInvertedDirection(NextDirection);
        }

        public Vector2 GetDirectionVector()
        {
            return World.GetDirectionVector(NextDirection);
        }

        public override void _Ready()
        {
            _sprite = GetNode<Sprite>("Sprite");
        }

        public virtual void Stop()
        {
            MoveState = State.Stopped;
            NextDirection = Direction.None;
            Updated = true;
        }

        public virtual bool PreStep()
        {
            // Explosion check
            if (WillExplodeAtTick == World.GameTicks)
            {
                Explode();
                WillExplodeAtTick = -1;
                return false;
            }

            return true;
        }

        public virtual void Step()
        {
        }

        public virtual void BeforePick() { }

        public virtual void Pick()
        {
            Stop();
            BeforePick();
            Updated = true;
            World.RemoveTile(this);
        }

        public virtual void Move()
        {
            var offset = World.TileMap.CellSize.x;
            var directionVector = GetDirectionVector();
            var newTargetPosition = TargetPosition + (directionVector * offset);

            // Use warp?
            if (WarpTarget != null)
            {
                var tWTile = World.GetNeighborTile(WarpTarget, NextDirection);
                if (tWTile?.CanBePassedThrough(this, NextDirection) == false && tWTile != this)
                {
                    Stop();
                    WarpTarget = null;
                    return;
                }
                else
                {
                    // Teleport
                    Position = WarpTarget.Position;
                    newTargetPosition = WarpTarget.Position + (offset * directionVector);
                    WarpTarget = null;
                }
            }
            else
            {
                // Detect tile
                var tTile = World.GetNeighborTile(this, NextDirection);
                if (tTile?.CanBePassedThrough(this, NextDirection) == false)
                {
                    Stop();
                    return;
                }
            }

            // Rotation
            if (CanRotate && directionVector.y >= 0)
            {
                var rotationOffset = Mathf.Deg2Rad(180);
                if (directionVector.x == 0)
                {
                    TargetRotation += rotationOffset;
                }
                else
                {
                    TargetRotation += directionVector.x * rotationOffset;
                }
            }

            TargetPosition = newTargetPosition;
            MoveState = State.Stopped;
            World.UpdateTilePosition(this);
            EndMoveCallback();
        }

        public virtual void EndMoveCallback() { }

        public virtual string GenerateTileDebugInfo(CollisionStatus status)
        {
            return "";
        }

        public override void _Process(float delta)
        {
            float weight = _currentTick / (float)StepTicks;
            Position = _sourcePosition.LinearInterpolate(_targetPosition, weight);
            Rotation = Mathf.Lerp(_sourceRotation, _targetRotation, weight);
            _currentTick = Mathf.Clamp(_currentTick + 1, 0, StepTicks);

            if (WillExplodeAtTick == World.GameTicks)
            {
                Modulate = Colors.Red;
            }
        }

        public Tile GetOverlappingTile(TileLayerEnum layer)
        {
            return World.GetTileAtGridPositionAtLayer(TilePosition, layer);
        }

        public Tile GetNeighborAtDirection(Direction direction, TilePickEnum pickEnum = TilePickEnum.ForegroundFirst)
        {
            return World.GetNeighborTile(this, direction, pickEnum);
        }

        private void UpdateRollDirection()
        {
            switch (_rollDirection)
            {
                case RollDirectionEnum.None:
                    MakeRollLeft = MakeRollRight = false;
                    break;
                case RollDirectionEnum.Left:
                    MakeRollLeft = true;
                    MakeRollRight = false;
                    break;
                case RollDirectionEnum.Right:
                    MakeRollLeft = false;
                    MakeRollRight = true;
                    break;
                case RollDirectionEnum.Both:
                    MakeRollLeft = MakeRollRight = true;
                    break;
            }
        }
    }

    public class StaticTile : Tile
    {
        public StaticTile()
        {
            Movable = false;
            Warpable = false;
        }
    }

    public class ControlledTile : Tile
    {
        public ControlledTile()
        {
            Controlled = true;
        }
    }

    public class BackgroundTile : StaticTile
    {
        public BackgroundTile()
        {
            TileLayer = TileLayerEnum.Background;
            ZIndex = 0;
            PassthroughMode = PassthroughModeEnum.All;
        }
    }

    public class SaveTile : ControlledTile
    {
        public SaveTile()
        {
            Pickable = true;
        }
    }

    public class HintTile : BackgroundTile { }

    public class RockTile : FallingTile
    {
        public RockTile()
        {
            IsHeavy = true;
        }
    }

    public class BombTile : FallingTile
    {
        public BombTile()
        {
            CanExplode = true;
        }
    }

    public class EggTile : FallingTile { }

    public class DirtTile : StaticTile
    {
        public DirtTile()
        {
            Pickable = true;
        }
    }

    public class MarbleFloorTile : BackgroundTile
    {
        public MarbleFloorTile()
        {
            Indestructible = true;
            PassthroughMode = PassthroughModeEnum.PlayerOnly;
        }
    }

    public class SlimeyTile : ControlledTile { }
}
