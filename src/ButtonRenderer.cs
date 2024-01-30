using System.Numerics;
using static Raylib_cs.Raylib;



public static class ButtonRenderer
{
    public static List<Button> buttons = new List<Button>{};

    public static void DrawButtons()
    {
        for (int i = 0; i < buttons.Count; i++)
            {
                Button but = buttons[i];
                if (but.Is_Rounded)
                {
                    DrawRectangleRounded(but.Rect, but.Roundness, 0, ColorAlpha(but.Color, but.Color.A));
                }
                else
                {
                    DrawRectangle((int)but.Rect.Position.X, (int)but.Rect.Position.Y, but.Width, but.Height, ColorAlpha(but.Color, but.Color.A));
                }

                if (but.ButtonImage != null)
                {
                    if (but.ButtonImage.ImageType == ImageType.Centered)
                    {
                        float rectWidth = but.Width;
                        float rectHeight = but.Height;

                        float textureWidth = but.ButtonImage.Texture2D.Width;
                        float textureHeight = but.ButtonImage.Texture2D.Height;

                        float posX = but.Rect.Position.X + (rectWidth - textureWidth) / 2.0f;
                        float posY = but.Rect.Position.Y + (rectHeight - textureHeight) / 2.0f;
                        //DrawTextureEx(Texture2D texture, Vector2 position, float rotation, float scale, Color tint);
                        Vector2 imagePos = new Vector2(posX, posY);
                        DrawTextureEx(but.ButtonImage.Texture2D, imagePos, but.ButtonImage.ImageRotation, but.ButtonImage.ImageSize, ColorAlpha(but.ButtonImage.Color, but.ButtonImage.Color.A));
                    }

                    if (but.ButtonImage.ImageType == ImageType.Free)
                    {
                        float rectWidth = but.Width;
                        float rectHeight = but.Height;

                        float textureWidth = but.ButtonImage.Texture2D.Width;
                        float textureHeight = but.ButtonImage.Texture2D.Height;

                        float posX = but.Rect.Position.X + (rectWidth - textureWidth) / 2.0f;
                        float posY = but.Rect.Position.Y + (rectHeight - textureHeight) / 2.0f;

                        Vector2 newPos = new Vector2(posX, posY) + but.ButtonImage.ImagePosition;
                        DrawTextureEx(but.ButtonImage.Texture2D, newPos, but.ButtonImage.ImageRotation, but.ButtonImage.ImageSize, ColorAlpha(but.ButtonImage.Color, but.ButtonImage.Color.A));
                        //DrawTexture(but.GetTexture(), (int)newPos.X, (int)newPos.Y, Color.White);
                    }

                }
            }

            for (int i = 0; i < buttons.Count; i++)
            {
                Button but = buttons[i];

                if (but.TextLabel != null)
                {
                    TextLabel text = but.TextLabel;
                    float textWidth = MeasureText(text.Label, text.FontSize);
                    float textHeight = text.FontSize;

                    float textX = but.Rect.X + (but.Rect.Width - textWidth) / 2.0f;
                    float textY = but.Rect.Y + (but.Rect.Height - textHeight) / 2.0f;

                    Vector2 newPos = new Vector2(textX, textY) + text.Position;

                    DrawText(text.Label, (int)newPos.X, (int)newPos.Y, text.FontSize, ColorAlpha(text.TextColor, text.TextColor.A));
                }


            }

    }
}