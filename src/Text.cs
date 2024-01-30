using System.Numerics;
using Raylib_cs;

public class TextLabel
{
    public string Label { get; set; }
    public Vector2 Position { get; set; }
    public Color TextColor { get; set; }
    public int FontSize { get; set; }

    public TextLabel(string label_text, Vector2 text_pos, Color text_color, int text_size)
    {
        Label = label_text;
        Position = text_pos;
        TextColor = text_color;
        FontSize = text_size;
    }
}