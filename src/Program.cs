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

		// Set the UI theme
		Theme.SetTheme("dark");

		// Add/register all the tools
		Toolbar.Tools.Add(new Cursor());
		Toolbar.Tools.Add(new Brush());
		Toolbar.Tools.Add(new EyeDropper());
		Toolbar.Tools.Add(new Eraser());

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
			FileManager.Update();
			Toolbar.Update();
			Layers.Update();
			Toolbar.CurrentTool.Update();
			Canvas.Update();

			// Canvas drawing stuff
			// TODO: Do this in the canvas class
			Raylib.BeginTextureMode(Canvas.CurrentLayer);
			Toolbar.CurrentTool.CanvasRender();
			Raylib.EndTextureMode();

			Raylib.BeginDrawing();
			Raylib.ClearBackground(Color.Magenta);

			// Draw the actual canvas
			Canvas.Render();

			// Tool related stuff UI stuff
			Toolbar.Render();
			Layers.Render();
			Toolbar.CurrentTool.UiRender();


			Raylib.EndDrawing();
		}

		Toolbar.CleanUp();
		Canvas.CleanUp();
		Theme.CleanUp();
		Raylib.CloseWindow();
	}
}