using Godot;

public static class FontExtensions
{
    public static Font CloneWithSize(this Font font, int size)
    {
        var newFont = (Font)font.Duplicate();
        if (newFont is DynamicFont dynamicFont)
        {
            dynamicFont.Size = size;
        }
        return newFont;
    }
}
