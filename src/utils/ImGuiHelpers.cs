using System.Numerics;
using ImGuiNET;
using Raylib_cs;

public class Helpers
{
    public static Color rectangleColor = new Color(104, 112, 153, 255);
    public static Color cardColor = new Color(102, 102, 102, (int)63.75);
    public static Color otherColor = new Color(51, 76, 203, (int)71.4);
    public static Color blueColor = new Color(218, 224, 255, 205);
    public static Color ringColor = new Color(160, 52, 47, 1);

    public static uint convertToUnit(Color color)
    {
        Vector4 vec = new Vector4(color.R / 255f, color.G / 255f, color.B / 255f, color.A / 255f);
        uint bgColor = ImGui.ColorConvertFloat4ToU32(vec);

        return bgColor;
    }

    public static Vector4 convertToVector4(Color color)
    {
        return new Vector4(Convert.ToInt64(color.R) / 255f, Convert.ToInt64(color.G) / 255f, Convert.ToInt64(color.B) / 255f, Convert.ToInt64(color.A) / 255f); ;
    }

    public static void DrawCard(string cardName, float cardWidth, float cardHeight, Vector4 bgColor, bool hasBorder, Action content)
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

    public static void CompletedTask(int cardSize, string cardId, string taskName, string taskDesc, string completedTime)
    {
        DrawCard("Completed Card: " + cardId, cardSize, 120, convertToVector4(cardColor), false, () =>
                {
                    ImGui.SetCursorPosX(ImGui.GetCursorPosX() + 10f);
                    ImGui.SetCursorPosY(ImGui.GetCursorPosY() + 10f);
                    ImGui.Text(taskName);

                    ImGui.SetCursorPosX(ImGui.GetCursorPosX() + 10f);
                    ImGui.SetCursorPosY(ImGui.GetCursorPosY() + 10f);
                    ImGui.Text(taskDesc);

                    ImGui.SetCursorPosX(ImGui.GetCursorPosX() + 10f);
                    ImGui.SetCursorPosY(ImGui.GetCursorPosY() + 10f);
                    ImGui.Text($"Completed on {completedTime}");

                    // ImGui.SetCursorPosX(ImGui.GetCursorPosX() + 10f);
                    // ImGui.SetCursorPosY(ImGui.GetCursorPosY() + 10f);
                    // ImGui.Text("1 set 2 Pomodoro");
                }
        );
    }

}

