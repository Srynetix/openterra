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

        public enum ExternalAction
        {
            None,
            Pushed,
            Picked
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

        /// <summary>Tile type.</summary>
        public string Type;

        /// <summary>Is pickable.</summary>
        public bool Pickable;

        /// <summary>Can fall.</summary>
        public bool CanFall;
        public bool CanExplode;
        public bool CanRotate;
        public bool Indestructible;

        /// <summary>Is controlled</summary>
        public bool Controlled;

        /// <summary>Make roll left.</summary>
        public bool MakeRollLeft;

        /// <summary>Make roll right.</summary>
        public bool MakeRollRight;

        /// <summary>Movable.</summary>
        public bool Movable;

        public bool IsWarp;

        /// <summary>Background.</summary>
        public bool Background;

        /// <summary>Is player.</summary>
        public bool Player;

        public PassthroughModeEnum PassthroughMode;

        /// <summary>Move state.</summary>
        public State MoveState;

        /// <summary>Next direction.</summary>
        public Direction NextDirection;

        public WarpTile WarpTarget;
        public int WillExplodeAtTick = -1;

        public bool Picked;
        public bool Destroyed;
        public int Priority;
        public bool Warpable;

        /// <summary>Step ticks</summary>
        public int StepTicks = 10;

        public RollDirectionEnum RollDirection
        {
            get => _rollDirection;
            set
            {
                _rollDirection = value;
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

        public ExternalAction NextExternalAction;

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

        public TileWorld World { get; set; }
        public bool Updated { get; set; }
        public Direction PushDirection;

        protected Vector2 _sourcePosition;
        protected Vector2 _targetPosition;
        protected float _sourceRotation;
        protected float _targetRotation;
        protected int _currentTick;
        protected RollDirectionEnum _rollDirection;

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


        public bool CanGoTowards(CollisionStatus status, Direction direction)
        {
            return direction switch
            {
                Direction.Left => status.left == null,
                Direction.Right => status.right == null,
                Direction.Up => status.top == null,
                Direction.Down => status.bottom == null,
                _ => false,
            };
        }

        public Sprite GetSprite()
        {
            return GetNode<Sprite>("Sprite");
        }

        public void WillMoveTowards(Direction direction)
        {
            MoveState = State.WillMove;
            NextDirection = direction;
        }

        public void WillWarpTo(WarpTile target)
        {
            MoveState = State.WillMove;
            WarpTarget = target;
        }

        public void WillExplode()
        {
            WillExplodeAtTick = World.GameTicks + 1;
        }

        public void Explode()
        {
            World.SpawnExplosionAtTile(this);
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
        }

        public virtual void Stop()
        {
            MoveState = State.Stopped;
            NextDirection = Direction.None;
        }

        public virtual void Step()
        {
            if (WillExplodeAtTick == World.GameTicks)
            {
                Explode();
                WillExplodeAtTick = -1;
            }
        }

        public virtual void Pick()
        {
            Stop();
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
                if (tWTile?.PassthroughMode == PassthroughModeEnum.Nothing)
                {
                    Stop();
                    WarpTarget = null;
                    return;
                }
                else
                {
                    // Teleport
                    newTargetPosition = WarpTarget.Position + (offset * directionVector);
                    WarpTarget = null;
                }
            }
            else
            {
                // Detect tile
                var tTile = World.GetNeighborTile(this, NextDirection);
                if (tTile?.PassthroughMode == PassthroughModeEnum.Nothing)
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

        public virtual void EndMoveCallback() {}

        public virtual string GenerateTileDebugInfo(CollisionStatus status)
        {
            return "";
        }

        public override void _Process(float delta)
        {
            float weight = (float)_currentTick / (float)StepTicks;
            Position = _sourcePosition.LinearInterpolate(_targetPosition, weight);
            Rotation = Mathf.Lerp(_sourceRotation, _targetRotation, weight);
            _currentTick = Mathf.Clamp(_currentTick + 1, 0, StepTicks);

            if (WillExplodeAtTick == World.GameTicks)
            {
                Modulate = Colors.Red;
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
            Background = true;
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

    public class RockTile : FallingTile { }

    public class GemTile : FallingTile
    {
        public GemTile()
        {
            Pickable = true;
        }
    }

    public class BombTile : FallingTile
    {
        public BombTile()
        {
            CanExplode = true;
        }
    }

    public class RubyTile : GemTile
    {
    }
    public class DiamondTile : GemTile
    {
    }
    public class EmeraldTile : GemTile
    {
    }
    public class EggTile : FallingTile { }

    public class SteelWallTile : WallTile
    {
        public SteelWallTile()
        {
            Indestructible = true;
        }
    }

    public class DirtTile : StaticTile
    {
        public DirtTile()
        {
            Pickable = true;
        }
    }
    public class WallTile : StaticTile { }
    public class RoundedWallTile : WallTile
    {
        public RoundedWallTile()
        {
            RollDirection = RollDirectionEnum.Both;
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