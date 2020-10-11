using Godot;

public class VirtualKeyboard : CanvasLayer
{
    [Signal] public delegate void RestartPressed();
    [Signal] public delegate void PausePressed();
    [Signal] public delegate void StepPressed();
    [Signal] public delegate void DebugDrawToggled(bool pressed);

    private IconTouchButton _upButton;
    private IconTouchButton _downButton;
    private IconTouchButton _leftButton;
    private IconTouchButton _rightButton;
    private IconTouchButton _bombButton;
    private IconTouchButton _actionButton;

    private Button _explodeButton;

    public override void _Ready()
    {
        _upButton = GetNode<IconTouchButton>("Main/ArrowButtons/Col/TopRow/Up");
        _downButton = GetNode<IconTouchButton>("Main/ArrowButtons/Col/BottomRow/Down");
        _leftButton = GetNode<IconTouchButton>("Main/ArrowButtons/Col/MiddleRow/Left");
        _rightButton = GetNode<IconTouchButton>("Main/ArrowButtons/Col/MiddleRow/Right");
        _bombButton = GetNode<IconTouchButton>("Main/ActionButtons/Col/Row/Bomb");
        _actionButton = GetNode<IconTouchButton>("Main/ActionButtons/Col/Row/Action");

        _explodeButton = GetNode<Button>("Main/MenuButtons/Col/TopRow/Explode");

        GetNode<Button>("Main/MenuButtons/Col/TopRow/Restart").Connect("pressed", this, nameof(OnRestartPressed));
        GetNode<Button>("Main/MenuButtons/Col/TopRow/Pause").Connect("pressed", this, nameof(OnPausePressed));
        GetNode<Button>("Main/MenuButtons/Col/TopRow/Step").Connect("pressed", this, nameof(OnStepPressed));
        GetNode<CheckButton>("Main/MenuButtons/Col/BottomRow/DebugDrawButton").Connect("toggled", this, nameof(OnDebugDrawToggled));

        // Hide arrow and action keys on non-mobile platforms
        if (!OS.GetName().Match("Android|iOS"))
        {
            GetNode<Control>("Main/ArrowButtons").Visible = false;
            GetNode<Control>("Main/ActionButtons").Visible = false;
        }
    }

    public bool UpButtonPressed => _upButton.Pressed;
    public bool LeftButtonPressed => _leftButton.Pressed;
    public bool RightButtonPressed => _rightButton.Pressed;
    public bool DownButtonPressed => _downButton.Pressed;
    public bool ExplodeButtonPressed => _explodeButton.Pressed;
    public bool BombButtonPressed => _bombButton.Pressed;
    public bool ActionButtonPressed => _actionButton.Pressed;

    private void OnRestartPressed() => EmitSignal(nameof(RestartPressed));
    private void OnPausePressed() => EmitSignal(nameof(PausePressed));
    private void OnStepPressed() => EmitSignal(nameof(StepPressed));
    private void OnDebugDrawToggled(bool pressed) => EmitSignal(nameof(DebugDrawToggled), pressed);
}
