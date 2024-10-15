using System.Numerics;
using Raylib_cs;

class Program
{
	public static void Main(string[] args)
	{
		// Shout out the bro Ray
		Raylib.SetTraceLogLevel(TraceLogLevel.Warning);
		Raylib.SetConfigFlags(ConfigFlags.ResizableWindow | ConfigFlags.AlwaysRunWindow);
		Raylib.InitWindow(800, 600, "John canvas, inventor of art");
		Raylib.MaximizeWindow();

		// Set the UI color theme
		ColorTheme.SetColorTheme("dark");

		// Add/register all the tools
		Toolbar.Tools.Add(new Cursor());
		Toolbar.Tools.Add(new Brush());

		while (!Raylib.WindowShouldClose())
		{
			Toolbar.Update();

			Raylib.BeginDrawing();
			Raylib.ClearBackground(Color.Magenta);
			Toolbar.Render();
			Raylib.EndDrawing();
		}

		Raylib.CloseWindow();
	}
}