using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

public class Button
{


    public Rectangle Rect { get; set; }
    public bool Is_Rounded { get; set; }
    public string FunctionID  { get; set; }
    public ImageExtened? ButtonImage { get; set; }
    public Color Color { get; set; }
    private Color baseColor;
    public int Width { get; set; }
    public int fontSize { get; set; }
    public int Height { get; set; }
    public float Roundness { get; set; }
    public TextLabel? TextLabel;

    public Button(Vector2 pos, int width, int height, Color color, bool rounded, float roundness,string functionID)
    {
        Vector2 sizeVector = new Vector2(width, height);
        Width = width;
        Height = height;
        Rect = new Rectangle(pos, sizeVector);
        Color = color;
        Is_Rounded = rounded;
        Roundness = roundness;
        baseColor = Color;
        FunctionID = functionID;
        //selfAction += 
    }

    public Button(Vector2 pos, int width, int height, Color color, bool rounded, float roundness, ImageExtened image,string functionID)
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
        FunctionID = functionID;

    }

    public Button(Vector2 pos, int width, int height, Color color, bool rounded, float roundness, TextLabel textLabel,string functionID)
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
        FunctionID = functionID;
    }

    public Button(Vector2 pos, int width, int height, Color color, bool rounded, float roundness, TextLabel textLabel, ImageExtened image,string functionID)
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
        FunctionID = functionID;
    }


    public bool DetectInput()
    {
        Vector2 mousePos = GetMousePosition();
        if (CheckCollisionPointRec(mousePos, Rect))
        {

            //Console.WriteLine("Detected: " + TextLabel?.Label + " inside");
            Color = ColorAlpha(Color, 0.01f);

            if (IsMouseButtonPressed(MouseButton.Left))
            {
                //Console.WriteLine("Clicked inside " + TextLabel?.Label);
                return true;
            }
        }
        else
        {
            Color = baseColor;
        }

        return false;
    }

    public object Called(){
         return this;
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