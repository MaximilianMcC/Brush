using System.Numerics;
using Raylib_cs;

class Helper
{
	// Draw text in a quicker way
	public static void DrawText(string text, Vector2 position)
	{
		Raylib.DrawTextEx(Theme.Font, text, position, Theme.FontSize, Theme.FontSpacing, Theme.Foreground);
	}

	// Draw text in a quicker way but also with a custom color
	public static void DrawText(string text, Vector2 position, Color color)
	{
		Raylib.DrawTextEx(Theme.Font, text, position, Theme.FontSize, Theme.FontSpacing, color);
	}
	
	// Measure text in a quicker way
	public static Vector2 MeasureText(string text)
	{
		return Raylib.MeasureTextEx(Theme.Font, text, Theme.FontSize, Theme.FontSpacing);
	}
}