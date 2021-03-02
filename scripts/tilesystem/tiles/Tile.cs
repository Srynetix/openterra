using Godot;

namespace Tiles
{
    /// <summary>
    /// Tile.
    /// </summary>
    public class Tile : Node2D
    {
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
        public Direction LastDirection;

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
        public bool Moved;

        /// <summary>Step ticks</summary>
        public int StepTicks = 10;

        public Vector2 SourcePosition;
        public Vector2 TargetPosition;
        public float SourceRotation;
        public float TargetRotation;

        public int StateMachineIdx;

        public RollDirectionEnum RollDirection
        {
            get => _rollDirection;
            set
            {
                _rollDirection = value;
                UpdateRollDirection();
            }
        }

        public Vector2 TilePosition
        {
            get => World.GetTileCurrentGridPosition(this);
        }

        public TileWorld World { get; set; }
        public bool Updated { get; set; }

        protected int _currentTick;
        protected RollDirectionEnum _rollDirection;
        protected Sprite _sprite;

        public Tile()
        {
            ZIndex = 1;
        }

        public void ResetTick()
        {
            // _targetRotation %= Mathf.Pi * 2;
            // Position = _sourcePosition = _targetPosition;
            // Rotation = _sourceRotation = _targetRotation;
            Updated = false;
            Moved = false;
            // _currentTick = 0;
        }

        public virtual bool CanBePicked()
        {
            return Pickable;
        }

        public virtual bool CanBePassedThrough(Tile source, Direction direction)
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
            LastDirection = direction;
            NextDirection = direction;
            SourcePosition = Position;
            TargetPosition = Position + TileUtils.GetDirectionVector(direction) * World.TileMap.CellSize.x;
            SourceRotation = Rotation;
            TargetRotation = Rotation + Mathf.Deg2Rad(180);
            World.UpdateTilePosition(this);
            Updated = true;
            GD.Print("Node ", Name, " will move towards ", direction);
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

        public Direction GetInvertedNextDirection()
        {
            return TileUtils.GetInvertedDirection(NextDirection);
        }

        public Vector2 GetNextDirectionVector()
        {
            return TileUtils.GetDirectionVector(NextDirection);
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

        public override void _Draw() {
            var color = Colors.LightBlue;
            var ballSize = 4;

            if (TileUtils.IsDirectionHorizontal(NextDirection)) {
                var src = new Vector2(-World.TileMap.CellSize.x / 4, 0);
                var dst = new Vector2(World.TileMap.CellSize.x / 4, 0);
                DrawLine(src, dst, color);

                if (NextDirection == Direction.Left) {
                    DrawCircle(src, ballSize, color);
                } else {
                    DrawCircle(dst, ballSize, color);
                }
            } else if (TileUtils.IsDirectionVertical(NextDirection)) {
                var src = new Vector2(0, -World.TileMap.CellSize.y / 4);
                var dst = new Vector2(0, World.TileMap.CellSize.y / 4);
                DrawLine(src, dst, color);

                if (NextDirection == Direction.Up) {
                    DrawCircle(src, ballSize, color);
                } else {
                    DrawCircle(dst, ballSize, color);
                }
            }
        }

        public virtual void Move()
        {
            const int offset = 2;
            var directionVector = GetNextDirectionVector();
            Position += directionVector * offset;

            // Detect if position is round
            if (Position == TargetPosition) {
                MoveState = State.DoneMoving;
            } else {
                MoveState = State.Moving;
            }

            // var newTargetPosition = TargetPosition + (directionVector * offset);

            // Use warp?
            // if (WarpTarget != null)
            // {
            //     var tWTile = World.GetNeighborTile(WarpTarget, NextDirection);
            //     if (tWTile?.CanBePassedThrough(this, NextDirection) == false && tWTile != this)
            //     {
            //         Stop();
            //         WarpTarget = null;
            //         return;
            //     }
            //     else
            //     {
            //         // Teleport
            //         Position = WarpTarget.Position;
            //         newTargetPosition = WarpTarget.Position + (offset * directionVector);
            //         WarpTarget = null;
            //     }
            // }
            // else
            // {
            //     // Detect tile
            //     var tTile = World.GetNeighborTile(this, NextDirection);
            //     if (tTile?.CanBePassedThrough(this, NextDirection) == false)
            //     {
            //         Stop();
            //         return;
            //     }
            // }

            // float l1 = (TargetPosition - SourcePosition).LengthSquared();
            // float l2 = (TargetPosition - Position).LengthSquared();
            // float c = 1 - (l2 / l1);

            // Rotation
            if (CanRotate)
            {
                // Rotation += (TargetRotation - SourceRotation) / World.TileMap.CellSize.x;
                // Rotation = Mathf.Lerp(SourceRotation, TargetRotation, c);
            }

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
            // float weight = _currentTick / (float)StepTicks;
            // Position = _sourcePosition.LinearInterpolate(_targetPosition, weight);
            // Rotation = Mathf.Lerp(_sourceRotation, _targetRotation, weight);
            // _currentTick = Mathf.Clamp(_currentTick + 1, 0, StepTicks);

            Update();

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
}
