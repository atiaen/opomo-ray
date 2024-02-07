using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

public class Button
{


    public Rectangle Rect { get; set; }
    public bool Is_Rounded { get; set; }

    public ImageExtened? ButtonImage { get; set; }
    public Color Color { get; set; }
    private Color baseColor;
    public int Width { get; set; }
    public int fontSize { get; set; }
    public int Height { get; set; }
    public float Roundness { get; set; }
    public TextLabel? TextLabel;

    public Button(Vector2 pos, int width, int height, Color color, bool rounded, float roundness)
    {
        Vector2 sizeVector = new Vector2(width, height);
        Width = width;
        Height = height;
        Rect = new Rectangle(pos, sizeVector);
        Color = color;
        Is_Rounded = rounded;
        Roundness = roundness;
        baseColor = Color;
    }

    public Button(Vector2 pos, int width, int height, Color color, bool rounded, float roundness, ImageExtened image)
    {
        Vector2 sizeVector = new Vector2(width, height);
        Width = width;
        Height = height;
        Rect = new Rectangle(pos, sizeVector);
        Color = color;
        Is_Rounded = rounded;
        Roundness = roundness;
        ButtonImage = image;
        baseColor = Color;

    }

    public Button(Vector2 pos, int width, int height, Color color, bool rounded, float roundness, TextLabel textLabel)
    {
        Vector2 sizeVector = new Vector2(width, height);
        Width = width;
        Height = height;
        Rect = new Rectangle(pos, sizeVector);
        Color = color;
        Is_Rounded = rounded;
        Roundness = roundness;
        TextLabel = textLabel;
        baseColor = Color;
    }

    public Button(Vector2 pos, int width, int height, Color color, bool rounded, float roundness, TextLabel textLabel, ImageExtened image)
    {
        Vector2 sizeVector = new Vector2(width, height);
        Width = width;
        Height = height;
        Rect = new Rectangle(pos, sizeVector);
        Color = color;
        Is_Rounded = rounded;
        Roundness = roundness;
        TextLabel = textLabel;
        ButtonImage = image;
        baseColor = Color;
    }


    public void DetectInput()
    {
        Vector2 mousePos = GetMousePosition();
        if (CheckCollisionPointRec(mousePos, Rect))
        {

            Console.WriteLine("Detected: " + TextLabel?.Label + " inside");
            Color = ColorAlpha(Color, 0.01f);

            if (IsMouseButtonPressed(MouseButton.Left))
            {
                Console.WriteLine("Clicked inside " + TextLabel?.Label);
            }
        }
        else
        {
            Color = baseColor;
        }
        //     mp = GetMousePosition();
        // pos = { GetMousePosition().x,GetMousePosition().y };


        // 	if (IsMouseButtonPressed(MOUSE_LEFT_BUTTON) && CheckCollisionPointCircle(pos,RedBall.Pos,RedBall.Radius))
        // 	{
        // 		Player.Current_Game_Score++;
        // 	}
    }

    // public void SetTexture(Texture2D texture)
    // {Vector2 textPosition,int font_size,Color text_color
    //     Texture2D = texture;
    // }

    // public Texture2D GetTexture()
    // {
    //     return Texture2D;
    // }




}