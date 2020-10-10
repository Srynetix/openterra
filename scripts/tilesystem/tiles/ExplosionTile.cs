using Godot;

namespace Tiles
{
    public class ExplosionTile : StaticTile
    {
        public override void _Process(float delta)
        {
            base._Process(delta);
            Modulate = Colors.White.LinearInterpolate(Colors.Transparent, Mathf.Min(_currentTick, StepTicks - 2) / (float)StepTicks);
        }

        public override void Step()
        {
            base.Step();
            Pick();
        }
    }
}
