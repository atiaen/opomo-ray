using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;


class Program
{



    public static void Main()
    {
        // Initialization
        //--------------------------------------------------------------------------------------
        const int screenWidth = 950;
        const int screenHeight = 600;
        float innerRadius = 95f;
        float outerRadius = 80.0f;

        float startAngle = -90f;
        float endAngle = 150f;
        float segments = 0.0f;

        var dbHandler = new DBHandler();

        InitWindow(screenWidth, screenHeight, "OPOMO Raylib");

        SetTargetFPS(60);

        var backgroundcolor = new Color(58, 58, 58, 1);
        var rectangleColor = new Color(104, 112, 153, 1);
        var rectangleColor2 = new Color(233, 233, 233, 1);
        var ringColor = new Color(160, 52, 47, 1);
        //rgba(160, 52, 47, 1)
        var rectangleColor3 = new Color(Convert.ToByte(104), Convert.ToByte(112), Convert.ToByte(153), Convert.ToByte(0.1));
        Vector2 center = new Vector2(GetScreenWidth() / 2.0f, (GetScreenHeight() - 250f) / 2.0f);

        List<Button> buttons = new List<Button>{
            new Button(new Vector2(10, 10),30,30,rectangleColor,true,0.3f,new ImageExtened("cog_icon.png",Color.White,1.0f,0f),""),
            new Button(new Vector2(55, 10),30,30,rectangleColor,true,0.3f,new ImageExtened("chart_icon.png",Color.White,1.0f,0f),""),
            new Button(new Vector2(100, 10),30,30,rectangleColor,true,0.3f,new ImageExtened("palette_icon.png",Color.White,1.0f,0f),""),

            new Button(new Vector2(740, 10),150,30,rectangleColor,true,0.3f,new TextLabel("Pomodoro",new Vector2(10,0),Color.White,15),new ImageExtened("pomo_icon.png",new Vector2(-45,0),Color.White,0.75f,0f),""),

            new Button(new Vector2(10, 85),150,30,rectangleColor,true,0.3f,new TextLabel("New Task",new Vector2(-30,0),Color.White,10),new ImageExtened("plus_icon.png",new Vector2(35,2),Color.White,0.8f,0f),"New Task"),
            new Button(new Vector2(170, 85),150,30,rectangleColor2,true,0.3f,new TextLabel("New Project",new Vector2(-30,0),rectangleColor,10),new ImageExtened("plus_icon.png",new Vector2(35,2),rectangleColor,0.8F,0f),"New Project"),

            new Button(new Vector2(700, 85),30,30,rectangleColor,true,0.3f,new TextLabel("All",new Vector2(0,0),Color.White,10),""),
            new Button(new Vector2(750, 85),50,30,rectangleColor3,true,0.3f,new TextLabel("Tasks",new Vector2(0,0),Color.White,10),"Tasks"),
            new Button(new Vector2(800, 85),70,30,rectangleColor3,true,0.3f,new TextLabel("Projects",new Vector2(0,0),Color.White,10),"Projects"),
            new Button(new Vector2(80, 144),30,30,rectangleColor3,true,0.3f,new ImageExtened("history_eye_icon.png",Color.White,1.0f,0f),""),
        };

        List<TextLabel> textLabels = new List<TextLabel>
        {
            new TextLabel("History",new Vector2(10,150),Color.White,15)
        };
        //Image Tracker here
        List<Image> images = new List<Image> { };
        TextRenderer.textLables = textLabels;
        ButtonRenderer.buttons = buttons;

        ActionHandlers.addActionToDic("New Project", new Action(() =>
                {
                    endAngle += 10f;
                    Console.WriteLine("End Angle Value: " + endAngle);

                })
                );


        ActionHandlers.addActionToDic("Projects", new Action(() =>
                {
                    endAngle -= 10f;
                    Console.WriteLine("End Angle Value: " + endAngle);

                })
                );

        //Texture Loader Here
        for (int i = 0; i < ButtonRenderer.buttons.Count; i++)
        {
            Button but = ButtonRenderer.buttons[i];
            if (but.ButtonImage != null)
            {
                Image img = LoadImage("resources/" + but.ButtonImage.ImageFileName);
                Texture2D texture = LoadTextureFromImage(img);
                but.ButtonImage.Texture2D = texture;
                images.Add(img);
            }
        }

        //Image Unloader here
        for (int i = 0; i < images.Count; i++)
        {
            UnloadImage(images[i]);
        }

        // Main game loop
        while (!WindowShouldClose())
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                var button = buttons[i];
                bool clicked = button.DetectInput();
                if (clicked)
                {
                    if (ActionHandlers.hasAction(button.FunctionID))
                    {
                        var act = ActionHandlers.getAction(buttons[i].FunctionID);
                        act.DynamicInvoke();
                    }

                }
            }


            BeginDrawing();
            ClearBackground(backgroundcolor);
            DrawRing(center, innerRadius, outerRadius, startAngle, endAngle, (int)segments, ColorAlpha(ringColor, 1.0f));
            ButtonRenderer.DrawButtons();
            TextRenderer.DrawTextItems();
            EndDrawing();
            //----------------------------------------------------------------------------------
        }

        //Texture Unloader Here
        for (int i = 0; i < ButtonRenderer.buttons.Count; i++)
        {
            Button but = ButtonRenderer.buttons[i];
            if (but.ButtonImage != null)
            {
                UnloadTexture(but.ButtonImage.Texture2D);
            }
        }

        // De-Initialization
        //--------------------------------------------------------------------------------------
        CloseWindow();
        //--------------------------------------------------------------------------------------

    }
}


