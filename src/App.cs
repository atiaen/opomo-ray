using System.Numerics;
using Raylib_cs;
using rlImGui_cs;
using ImGuiNET;
using static Raylib_cs.Raylib;
using IconFonts;
using System.Security.Cryptography;

public class App
{
    Image cog = LoadImage("resources/" + "cog_icon.png");
    Texture2D cogTexture;

    Image chart = LoadImage("resources/" + "chart_icon.png");
    Texture2D chartTexture;

    Image palette = LoadImage("resources/" + "palette_icon.png");
    Texture2D paletteTexture;

    Image logo = LoadImage("resources/" + "pomo_icon.png");
    Texture2D logoTexture;

    Image eye = LoadImage("resources/" + "history_eye_icon.png");
    Texture2D eyeTexture;

    Image pause = LoadImage("resources/" + "Play.png");
    Texture2D pauseTexture;

    Image stop = LoadImage("resources/" + "Union.png");
    Texture2D stopTexture;

    Image top = LoadImage("resources/" + "Vector.png");
    Texture2D topTexture;

    int screenWidthIn3;

    Vector2 center;

    List<Tasks> newTasks = new List<Tasks>();
    List<Tasks> taskHistory = new List<Tasks>();
    List<Tasks> completedTasks = new List<Tasks>();

    Tasks currentTask = null;
    float innerRadius = 95f;
    float outerRadius = 80.0f;

    float startAngle = -90f;
    float endAngle = 270f;
    float segments = 100.0f;


    public static string title = "Enter a task title";
    public static string desc = "Describe the task";
    public static string project_title = "Enter a Project Title";

    static int selectedIndex = 0;

    public DBHandler handler = new DBHandler();

    public void LoadTextures()
    {
        cogTexture = LoadTextureFromImage(cog);
        chartTexture = LoadTextureFromImage(chart);
        paletteTexture = LoadTextureFromImage(palette);
        logoTexture = LoadTextureFromImage(logo);
        eyeTexture = LoadTextureFromImage(eye);
        pauseTexture = LoadTextureFromImage(pause);
        stopTexture = LoadTextureFromImage(stop);
        topTexture = LoadTextureFromImage(top);
    }

    public void UnloadTextures()
    {
        UnloadTexture(cogTexture);
        UnloadTexture(paletteTexture);
        UnloadTexture(chartTexture);
        UnloadTexture(logoTexture);
        UnloadTexture(eyeTexture);
        UnloadTexture(pauseTexture);
        UnloadTexture(stopTexture);
        UnloadTexture(topTexture);
    }

    public void UnloadImages()
    {
        UnloadImage(logo);
        UnloadImage(chart);
        UnloadImage(palette);
        UnloadImage(cog);
        UnloadImage(eye);
        UnloadImage(pause);
        UnloadImage(stop);
        UnloadImage(top);

    }

    public void Run()
    {

        InitWindow(800, 650, "Opomo");
        SetTargetFPS(60);
        LoadTextures();

        UnloadImages();

        rlImGui.Setup(true);

        while (!WindowShouldClose())
        {
            screenWidthIn3 = GetScreenWidth() / 3;
            center = new Vector2(GetScreenWidth() / 2.0f - 20, (GetScreenHeight() - 250f) / 2.0f);
            taskHistory = handler.ReadFromTasks();
            completedTasks = handler.GetCompletedTasksByVal(true);

            BeginDrawing();
            ClearBackground(Color.DarkGray);
            rlImGui.Begin();


            if (ImGui.Begin("Opomo", ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoBackground))
            {
                ImGui.SetWindowSize(new Vector2(GetScreenWidth(), GetScreenHeight()));
                ImGui.PushStyleVar(ImGuiStyleVar.FrameRounding, 5.0f); // Adjust the rounding value as needed
                ImGui.PushStyleVar(ImGuiStyleVar.FramePadding, new Vector2(5, 5));

                RenderTop();

                ImGui.Columns(3, "SecondLayout", false);

                RenderLeftColumn();
                ImGui.NextColumn();
                RenderCenterColumn();
                ImGui.NextColumn();
                RenderRightColumn();
                ImGui.Columns(3);



                RenderProjectModal();
                RenderTaskModal();
            }



            ImGui.End();
            rlImGui.End();

            EndDrawing();
        }

        handler.CloseConnection();
        UnloadTextures();
        rlImGui.Shutdown();
        CloseWindow();
    }

    public void RenderTop()
    {
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
        Helpers.DrawCard("Logo", 100f, 20f, Helpers.convertToVector4(Helpers.rectangleColor), false, () =>
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
    }

    public void RenderLeftColumn()
    {
        //Column 1
        ImGui.SetColumnWidth(0, screenWidthIn3); // Set width of first column

        if (ImGui.Button("New Task " + FontAwesome6.Plus))
        {
            ImGui.OpenPopup("New Task");
            // endAngle += 10;
        }
        ImGui.SameLine(100);
        if (ImGui.Button("New Project " + FontAwesome6.Plus))
        {
            ImGui.OpenPopup("New Project");
            // endAngle -= 10;
        }

        ImGui.Dummy(new Vector2(0, 20));

        ImGui.Text("History " + FontAwesome6.Eye);

        ImGui.Dummy(new Vector2(0, 15));

        Helpers.DrawCard("History Card", screenWidthIn3 - 75, 18f, Helpers.convertToVector4(Helpers.otherColor), false, () =>
       {
           ImGui.SameLine(10);
           ImGui.Text(DateTime.Now.ToLongDateString());
       });


        ImGuiWindowFlags window_flags = ImGuiWindowFlags.NoScrollbar;
        ImGui.BeginChild("LeftScroll", new Vector2(ImGui.GetContentRegionAvail().X, ImGui.GetContentRegionAvail().Y), ImGuiChildFlags.None, window_flags);
        if (completedTasks.Count() > 0)
        {
            for (int i = 0; i < completedTasks.Count(); i++)
            {
                var task = completedTasks[i];
                Helpers.CompletedTask(screenWidthIn3, task.TaskId.ToString(), task.TaskName, task.TaskDesc, task.EndDate.ToShortDateString());
            }
        }
        else
        {
            ImGui.Text($"You haven't completed any tasks yet 🙁");
        }


        ImGui.EndChild();

    }

    public void RenderCenterColumn()
    {
        ImGui.SetColumnWidth(1, screenWidthIn3); // Set width of first column
        DrawRing(center, innerRadius, outerRadius, startAngle, endAngle, (int)segments, ColorAlpha(Helpers.ringColor, 1.0f));
        DrawText("22:10", (int)center.X - 35, (int)center.Y - 27, 30, Color.White);

        // Calculate the position of the image based on the end angle of the ring
        float imageAngle = endAngle; // Adjust this value as needed
        float imageRadius = outerRadius; // Adjust this value as needed
        Vector2 imagePosition = new Vector2(center.X + imageRadius * MathF.Cos(MathHelper.DegreesToRadians(imageAngle)),
                                            center.Y + imageRadius * MathF.Sin(MathHelper.DegreesToRadians(imageAngle)));

        imagePosition.X -= topTexture.Width / 2; // Adjust horizontally to center the texture
        imagePosition.Y -= topTexture.Height / 2; // Adjust vertically to center the texture

        // Draw the image at the calculated position
        // Replace "image" with your image texture and "imagePosition" with the calculated position
        DrawTexture(topTexture, (int)imagePosition.X, (int)imagePosition.Y - 7, Color.White);

        // ImGui.SetCursorPosX(ImGui.GetCursorPosX() + 30f);

        ImGui.Dummy(new Vector2(0, 250));
        rlImGui.ImageButton("pause", pauseTexture);
        ImGui.SameLine(0, 15);
        ImGui.Text("Good luck with the task \n        1 set done");
        ImGui.SameLine(0, 15);
        rlImGui.ImageButton("stop", stopTexture);

        ImGui.Dummy(new Vector2(0, 40));
        ImGui.SetCursorPosX(ImGui.GetCursorPosX() + 70f);
        ImGui.Button("Mark as completed");

        ImGui.Dummy(new Vector2(0, 30));

        if (currentTask != null)
        {
            Helpers.DrawCard("Input Card", screenWidthIn3, 130, Helpers.convertToVector4(Helpers.cardColor), false, () =>
                    {
                        ImGui.SetCursorPosX(ImGui.GetCursorPosX() + 10f);
                        ImGui.SetCursorPosY(ImGui.GetCursorPosY() + 10f);
                        ImGui.Columns(2, "Inside Card 2", false);
                        ImGui.SetColumnWidth(0, screenWidthIn3 * 0.5f);
                        ImGui.Text(currentTask.TaskName);

                        ImGui.NextColumn();
                        ImGui.SetColumnWidth(1, screenWidthIn3 * 0.5f);
                        ImGui.SetCursorPosX(screenWidthIn3 - 50f); // Adjust position for top-right section
                        ImGui.Button(FontAwesome6.Ellipsis);
                        ImGui.Columns(1);


                        ImGui.SetCursorPosX(ImGui.GetCursorPosX() + 10f);
                        ImGui.Text(currentTask.TaskDesc);


                        ImGui.SetCursorPosX(ImGui.GetCursorPosX() + 10f);
                        if (currentTask.ProjectId != null)
                        {
                            var proj = handler.GetProject(currentTask.ProjectId.ToString());
                            Helpers.DrawCard(proj.Title, screenWidthIn3 * 0.35f, 18f, Helpers.convertToVector4(Helpers.blueColor), false, () =>
                                                     {
                                                                         ImGui.Text(proj.Title);
                                                                     });

                        }


                    });
        }


    }

    public void RenderRightColumn()
    {
        ImGui.SetColumnWidth(2, screenWidthIn3); // Set width of first column

        ImGui.Button("All");
        ImGui.SameLine(50);
        ImGui.Button("Tasks");
        ImGui.SameLine(105);
        ImGui.Button("Projects");

        ImGui.Dummy(new Vector2(0, 15));


        ImGuiWindowFlags window_flags = ImGuiWindowFlags.NoScrollbar;
        ImGui.BeginChild("RightScroll", new Vector2(ImGui.GetContentRegionAvail().X, ImGui.GetContentRegionAvail().Y), ImGuiChildFlags.None, window_flags);
        if (taskHistory.Count() > 0)
        {
            for (int i = 0; i < taskHistory.Count(); i++)
            {
                var task = taskHistory[i];

                Helpers.DrawCard("Card " + i, screenWidthIn3, 80f, Helpers.convertToVector4(Helpers.cardColor), false, () =>
                                  {

                                      ImGui.SetCursorPosX(ImGui.GetCursorPosX() + 10f);
                                      ImGui.SetCursorPosY(ImGui.GetCursorPosY() + 10f);
                                      ImGui.Columns(2, "Inside Card", false);
                                      ImGui.SetColumnWidth(0, screenWidthIn3 * 0.5f);
                                      ImGui.Text(task.TaskName);

                                      ImGui.NextColumn();
                                      ImGui.SetColumnWidth(1, screenWidthIn3 * 0.5f);
                                      ImGui.SetCursorPosX(screenWidthIn3 - 50f); // Adjust position for top-right section
                                      ImGui.Button(FontAwesome6.Ellipsis);
                                      ImGui.Columns(1);



                                      ImGui.SetCursorPosX(ImGui.GetCursorPosX() + 10f);
                                      ImGui.Text(task.TaskDesc);


                                      ImGui.SetCursorPosX(ImGui.GetCursorPosX() + 10f);
                                      if (task.ProjectId != null)
                                      {
                                          var proj = handler.GetProject(task.ProjectId.ToString());

                                          Helpers.DrawCard(proj.Title, screenWidthIn3 * 0.35f, 18f, Helpers.convertToVector4(Helpers.blueColor), false, () =>
                                                                                       {
                                                                                           ImGui.SetCursorPosX(ImGui.GetCursorPosX() + 3f);
                                                                                           ImGui.Text(proj.Title);
                                                                                       });
                                      }
                                  });
            }

        }
        else
        {
            ImGui.Text($"You haven't created any tasks🙁");
        }

        ImGui.EndChild();


    }

    public void RenderTaskModal()
    {
        //New Task Modal
        ImGuiWindowFlags popup_flags = ImGuiWindowFlags.AlwaysAutoResize | ImGuiWindowFlags.NoMove | ImGuiWindowFlags.Modal | ImGuiWindowFlags.NoTitleBar;
        bool o = true;
        Vector2 viewCenter = ImGui.GetMainViewport().GetCenter();
        ImGui.SetNextWindowPos(viewCenter, ImGuiCond.Appearing, new Vector2(0.5f, 0.5f));

        if (ImGui.BeginPopupModal("New Task", ref o, popup_flags))
        {


            ImGui.InputText("##title", ref title, 1000);
            ImGui.Separator();

            ImGui.PushStyleVar(ImGuiStyleVar.FramePadding, new Vector2(0, 0));

            ImGuiInputTextFlags newFlags = ImGuiInputTextFlags.AllowTabInput;

            Vector2 modalSize = ImGui.GetWindowSize();

            ImGui.InputTextMultiline("##desc", ref desc, 5000000, new
            Vector2(modalSize.X * 0.92f, ImGui.GetTextLineHeight() * 8), newFlags);

            ImGui.PopStyleVar();

            string[] items = {
                "Short Break",
                "Long Break"
            };

            ImGui.Combo("##break_type", ref selectedIndex, items, items.Length);


            if (ImGui.Button("OK", new Vector2(modalSize.X * 0.9f, 0)))
            {
                Tasks task = new Tasks
                {
                    TaskId = Guid.NewGuid(),
                    TaskDesc = desc,
                    TaskName = title,
                    CreatedDate = DateTime.Now,
                    BreakType = items[selectedIndex]
                };
                handler.CreateTasks(task);

                Console.WriteLine($"{task.BreakType} | {task.TaskId} | {task.TaskDesc} | {task.TaskName}");
                ImGui.CloseCurrentPopup();
            }
            ImGui.SetItemDefaultFocus();

            ImGui.EndPopup();
        }

    }

    public void RenderProjectModal()
    {
        ImGuiWindowFlags popup_flags = ImGuiWindowFlags.AlwaysAutoResize | ImGuiWindowFlags.Modal | ImGuiWindowFlags.NoTitleBar;
        bool o = true;
        Vector2 viewCenter = ImGui.GetMainViewport().GetCenter();
        ImGui.SetNextWindowPos(viewCenter, ImGuiCond.Appearing, new Vector2(0.5f, 0.5f));
        Vector2 modalSize = ImGui.GetWindowSize();

        if (ImGui.BeginPopupModal("New Project", ref o, popup_flags))
        {

            ImGui.InputText("##project_title", ref project_title, 1000);
            ImGui.Dummy(new Vector2(0, 10));
            ImGui.Separator();

            ImGui.PushStyleVar(ImGuiStyleVar.FramePadding, new Vector2(0, 0));

            for (int i = newTasks.Count - 1; i >= 0; i--)
            {
                ImGui.InputText("##title " + i, ref title, 1000);

                // ImGui.PushStyleVar(ImGuiStyleVar.FramePadding, new Vector2(0, 0));

                ImGuiInputTextFlags newFlags = ImGuiInputTextFlags.AllowTabInput;

                ImGui.InputTextMultiline("desc " + i, ref desc, 5000000, new
                Vector2(modalSize.X * 0.3f, ImGui.GetTextLineHeight() * 3), newFlags);

                // ImGui.PopStyleVar();

                ImGui.Combo("##break_type " + i, ref selectedIndex, "Short Break\0Long Break\0");
                ImGui.SameLine();
                if (ImGui.Button(" " + FontAwesome6.Minus + " "))
                {
                    Console.WriteLine("Should remove: " + i);
                    if (newTasks.Count > 1)
                    {
                        // int s = createdTasks[i];
                        // tasks.RemoveAt(i);
                        // Console.WriteLine("Should remove: " + s);
                    }
                    ImGui.SetItemDefaultFocus();
                }
                ImGui.Separator();
            }

            ImGui.PopStyleVar();

            if (ImGui.Button(FontAwesome6.Plus + " Add Task"))
            {
                // createdTasks.Add(RandomNumberGenerator.GetInt32(10000));
                // ImGui.CloseCurrentPopup();
            }

            if (ImGui.Button("OK"))
            {
                ImGui.CloseCurrentPopup();
            }

            ImGui.EndPopup();
        }

    }

}