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

		// Run the select thingy for the fist
		// tool because theres nothing to call it
		Toolbar.Tools[Toolbar.SelectedToolIndex].OnSelect();

		// Create the actual canvas so we can
		// draw on it and whatnot
		// TODO: Don't do this like this
		Canvas.Setup(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());

		while (!Raylib.WindowShouldClose())
		{
			// Tool related stuff
			Toolbar.Update();
			Toolbar.Tool.Update();

			// Canvas drawing stuff
			// TODO: Do this in the canvas class
			Raylib.BeginTextureMode(Canvas.RenderTexture);
			Toolbar.Tool.CanvasRender();
			Raylib.EndTextureMode();

			Raylib.BeginDrawing();
			Raylib.ClearBackground(Color.Magenta);

			// Draw the actual canvas
			Canvas.Render();

			// Tool related stuff UI stuff
			Toolbar.Render();
			Toolbar.Tool.UiRender();


			Raylib.EndDrawing();
		}

		Toolbar.CleanUp();
		Canvas.CleanUp();
		Raylib.CloseWindow();
	}
}