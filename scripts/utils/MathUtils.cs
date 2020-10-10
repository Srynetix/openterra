using Godot;

public static class MathUtils
{
    public static float LerpAngle(float from, float to, float weight)
    {
        return from + (ShortAngleDist(from, to) * weight);
    }

    public static float ShortAngleDist(float from, float to)
    {
        const float maxAngle = Mathf.Pi * 2;
        float diff = (to - from) % maxAngle;
        return (2 * diff % maxAngle) - diff;
    }
}
