using static Raylib_cs.Raylib;



public class TextRenderer
{
    public static List<TextLabel> textLables = new List<TextLabel> { };

    public static void DrawTextItems()
    {
        for (int i = 0; i < textLables.Count; i++)
        {
            TextLabel text = textLables[i];
            DrawText(text.Label, (int)text.Position.X, (int)text.Position.Y, text.FontSize, ColorAlpha(text.TextColor, text.TextColor.A));
        }
    }
}