using System.Numerics;
using Raylib_cs;
using rlImGui_cs;
using ImGuiNET;
using static Raylib_cs.Raylib;

class Program
{




    static void Main(string[] args)
    {



        InitWindow(800, 650, "Opomo");
        SetTargetFPS(60);

        Image cog = LoadImage("resources/" + "cog_icon.png");
        Texture2D cogTexture = LoadTextureFromImage(cog);

        Image chart = LoadImage("resources/" + "chart_icon.png");
        Texture2D chartTexture = LoadTextureFromImage(chart);

        Image palette = LoadImage("resources/" + "palette_icon.png");
        Texture2D paletteTexture = LoadTextureFromImage(palette);

        Image logo = LoadImage("resources/" + "pomo_icon.png");
        Texture2D logoTexture = LoadTextureFromImage(logo);

        Image eye = LoadImage("resources/" + "history_eye_icon.png");
        Texture2D eyeTexture = LoadTextureFromImage(eye);

        Image pause = LoadImage("resources/" + "Play.png");
        Texture2D pauseTexture = LoadTextureFromImage(pause);

        Image stop = LoadImage("resources/" + "Union.png");
        Texture2D stopTexture = LoadTextureFromImage(stop);

        var rectangleColor = new Color(104, 112, 153, 255);
        var cardColor = new Color(102, 102, 102, (int)63.75);
        var otherColor = new Color(51, 76, 203, (int)71.4);
        var blueColor = new Color(218, 224, 255, 205);
        var ringColor = new Color(160, 52, 47, 1);

        Vector2 center = new Vector2(GetScreenWidth() / 2.0f - 20, (GetScreenHeight() - 250f) / 2.0f);

        float innerRadius = 95f;
        float outerRadius = 80.0f;

        float startAngle = -90f;
        float endAngle = 150f;
        float segments = 0.0f;

        var screenWidthIn3 = GetScreenWidth() / 3;

        UnloadImage(logo);
        UnloadImage(chart);
        UnloadImage(palette);
        UnloadImage(cog);

        rlImGui.Setup(true);

        while (!WindowShouldClose())
        {
            BeginDrawing();
            ClearBackground(Color.DarkGray);
            rlImGui.Begin();

            if (ImGui.Begin("Something", ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoBackground))
            {
                ImGui.SetWindowSize(new Vector2(GetScreenWidth(), GetScreenHeight()));

                ImGui.Columns(2, "TopLayout", false);
                ImGui.SetColumnWidth(0, 400); // Set width of first column


                ImGui.BeginGroup();
                rlImGui.ImageButton("Settings", cogTexture);
                ImGui.SameLine();
                rlImGui.ImageButton("Stats", chartTexture);
                ImGui.SameLine();
                rlImGui.ImageButton("Palette", paletteTexture);

                ImGui.EndGroup();

                // Anchor to top-right section
                ImGui.NextColumn();
                ImGui.SetCursorPosX(ImGui.GetWindowWidth() - 100); // Adjust position for top-right section

                ImGui.BeginGroup();
                DrawCard("Logo", 100f, 20f, convertToVector4(rectangleColor), false, () =>
                {
                    ImGui.SetCursorPosX(ImGui.GetCursorPosX() + 10f);
                    rlImGui.Image(logoTexture);
                    ImGui.SameLine(0, 10);
                    ImGui.Text("OPOMO");
                });
                // DrawImageButtonWithTextAndBackground(logoTexture, "Opomo", rectangleColor);
                ImGui.EndGroup();
                ImGui.Columns(1);

                ImGui.Dummy(new Vector2(0, 50));

                ImGui.Columns(3, "SecondLayout", false);
                //Column 1
                {
                    ImGui.SetColumnWidth(0, screenWidthIn3); // Set width of first column

                    if (ImGui.Button("New Task " + IconFonts.FontAwesome6.Plus))
                    {
                        endAngle += 10;
                    }
                    ImGui.SameLine(100);
                    if (ImGui.Button("New Project " + IconFonts.FontAwesome6.Plus))
                    {
                        endAngle -= 10;
                    }

                    ImGui.Dummy(new Vector2(0, 20));

                    ImGui.Text("History " + IconFonts.FontAwesome6.Eye);

                    ImGui.Dummy(new Vector2(0, 15));

                    DrawCard("History Card", screenWidthIn3 - 100, 18f, convertToVector4(otherColor), false, () =>
                   {
                       ImGui.SameLine(10);
                       ImGui.Text(DateTime.Now.ToLongDateString());
                   });


                    ImGuiWindowFlags window_flags = ImGuiWindowFlags.NoScrollbar;
                    ImGui.BeginChild("LeftScroll", new Vector2(ImGui.GetContentRegionAvail().X, ImGui.GetContentRegionAvail().Y), ImGuiChildFlags.None, window_flags);
                    for (int i = 0; i < 30; i++)
                    {
                        DrawCard("Completed Card: " + i, screenWidthIn3 - 50, 120, convertToVector4(cardColor), false, () =>
                        {
                            ImGui.SetCursorPosX(ImGui.GetCursorPosX() + 10f);
                            ImGui.SetCursorPosY(ImGui.GetCursorPosY() + 10f);
                            ImGui.Text("Title");

                            ImGui.SetCursorPosX(ImGui.GetCursorPosX() + 10f);
                            ImGui.SetCursorPosY(ImGui.GetCursorPosY() + 10f);
                            ImGui.Text("Goal and Objective");

                            ImGui.SetCursorPosX(ImGui.GetCursorPosX() + 10f);
                            ImGui.SetCursorPosY(ImGui.GetCursorPosY() + 10f);
                            ImGui.Text("Completed at 12:00:00PM");

                            ImGui.SetCursorPosX(ImGui.GetCursorPosX() + 10f);
                            ImGui.SetCursorPosY(ImGui.GetCursorPosY() + 10f);
                            ImGui.Text("1 set 2 Pomodoro");
                        });
                    }

                    ImGui.EndChild();

                }


                ImGui.NextColumn();

                //Column 2
                {
                    ImGui.SetColumnWidth(1, screenWidthIn3); // Set width of first column
                    DrawRing(center, innerRadius, outerRadius, startAngle, endAngle, (int)segments, ColorAlpha(ringColor, 1.0f));
                    DrawText("22:10", (int)center.X - 35, (int)center.Y - 27, 30, Color.White);


                    // ImGui.SetCursorPosX(ImGui.GetCursorPosX() + 30f);
                    ImGui.SetCursorPosY(ImGui.GetCursorPosY() + 250f);
                    rlImGui.ImageButton("pause",pauseTexture);
                    ImGui.SameLine();
                    ImGui.Text("Good luck with the task \n 1 set done");
                    ImGui.SameLine();
                    rlImGui.ImageButton("stop",stopTexture);

                }


                // ImGui.NextColumn();
                // //Column 3
                // {
                //     ImGui.SetColumnWidth(2, screenWidthIn3); // Set width of first column

                //     ImGui.Button("All");
                //     ImGui.SameLine(50);
                //     ImGui.Button("Tasks");
                //     ImGui.SameLine(105);
                //     ImGui.Button("Projects");

                //     ImGui.Dummy(new Vector2(0, 15));


                //     ImGuiWindowFlags window_flags = ImGuiWindowFlags.NoScrollbar;
                //     ImGui.BeginChild("RightScroll", new Vector2(ImGui.GetContentRegionAvail().X, ImGui.GetContentRegionAvail().Y), ImGuiChildFlags.None, window_flags);
                //     for (int i = 0; i < 10; i++)
                //     {
                //         DrawCard("Card " + i, screenWidthIn3, 80f, convertToVector4(cardColor), false, () =>
                //                           {

                //                               ImGui.SetCursorPosX(ImGui.GetCursorPosX() + 10f);
                //                               ImGui.SetCursorPosY(ImGui.GetCursorPosY() + 10f);
                //                               ImGui.Columns(2, "Inside Card", false);
                //                               ImGui.SetColumnWidth(0, screenWidthIn3 * 0.5f);
                //                               ImGui.Text("Title");

                //                               ImGui.NextColumn();
                //                               ImGui.SetColumnWidth(1, screenWidthIn3 * 0.5f);
                //                               ImGui.SetCursorPosX(screenWidthIn3 - 50f); // Adjust position for top-right section
                //                               ImGui.Button(IconFonts.FontAwesome6.Ellipsis);
                //                               ImGui.Columns(1);



                //                               ImGui.SetCursorPosX(ImGui.GetCursorPosX() + 10f);
                //                               ImGui.Text("Goal and objective");


                //                               ImGui.SetCursorPosX(ImGui.GetCursorPosX() + 10f);
                //                               if (i % 3 == 0)
                //                               {
                //                                   DrawCard("Project name", screenWidthIn3 * 0.35f, 18f, convertToVector4(blueColor), false, () =>
                //                                                                                {
                //                                                                                    ImGui.SetCursorPosX(ImGui.GetCursorPosX() + 3f);
                //                                                                                    ImGui.Text("Project name");
                //                                                                                });
                //                               }
                //                           });
                //     }
                //     ImGui.EndChild();


                // }


                ImGui.Columns(3);
            }

            ImGui.End();
            rlImGui.End();

            EndDrawing();
        }

        UnloadTexture(cogTexture);
        UnloadTexture(paletteTexture);
        UnloadTexture(chartTexture);
        UnloadTexture(logoTexture);
        UnloadTexture(eyeTexture);
        UnloadTexture(pauseTexture);
        UnloadTexture(stopTexture);
        rlImGui.Shutdown();
        CloseWindow();
    }


    static void DrawCard(string cardName, float cardWidth, float cardHeight, Vector4 bgColor, bool hasBorder, Action content)
    {
        // Draw card background
        ImGui.PushStyleColor(ImGuiCol.ChildBg, bgColor);
        ImGui.BeginChild(cardName, new Vector2(cardWidth, cardHeight), hasBorder ? ImGuiChildFlags.Border : ImGuiChildFlags.None, ImGuiWindowFlags.NoScrollbar);

        content?.Invoke();
        // ImGui.PopTextWrapPos();

        // End card
        ImGui.EndChild();
        ImGui.PopStyleColor();
    }

    static uint convertToUnit(Color color)
    {
        Vector4 vec = new Vector4(color.R / 255f, color.G / 255f, color.B / 255f, color.A / 255f);
        uint bgColor = ImGui.ColorConvertFloat4ToU32(vec);

        return bgColor;
    }

    static Vector4 convertToVector4(Color color)
    {
        return new Vector4(Convert.ToInt64(color.R) / 255f, Convert.ToInt64(color.G) / 255f, Convert.ToInt64(color.B) / 255f, Convert.ToInt64(color.A) / 255f); ;
    }


}


