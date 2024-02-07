using System.Numerics;
using Raylib_cs;

public class ImageExtened
{

    public Texture2D Texture2D { get; set; }
    public ImageType ImageType { get; set; }
    public string ImageFileName { get; set; }
    public Vector2 ImagePosition { get; set; }

    public Color Color { get; set; }
    
    public float ImageSize { get; set; }
    public float ImageRotation { get; set; }

    public ImageExtened(string file_name, Vector2 image_pos, Color img_color, float image_size, float image_rotation)
    {
        ImageFileName = file_name;
        ImageType = ImageType.Free;
        ImagePosition = image_pos;
        Color = img_color;
        ImageSize = image_size;
        ImageRotation = image_rotation;
    }

     public ImageExtened(string file_name, Color img_color, float image_size, float image_rotation)
    {
        ImageFileName = file_name;
        ImageType = ImageType.Centered;
        Color = img_color;
        ImageSize = image_size;
        ImageRotation = image_rotation;
    }
}