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
		Raylib.SetExitKey(KeyboardKey.Null);

		// Set the UI color theme
		ColorTheme.SetColorTheme("dark");

		// Add/register all the tools
		Toolbar.RegisterTool(new Cursor());
		Toolbar.RegisterTool(new Brush());

		while (!Raylib.WindowShouldClose())
		{
			// Tool related stuff
			Toolbar.Update();
			Toolbar.Tools[Toolbar.SelectedToolIndex].Update();

			Raylib.BeginDrawing();
			Raylib.ClearBackground(Color.Magenta);

			// Tool related stuff
			Toolbar.Render();
			Toolbar.Tools[Toolbar.SelectedToolIndex].Render();

			Raylib.EndDrawing();
		}

		Toolbar.CleanUp();
		Raylib.CloseWindow();
	}
}