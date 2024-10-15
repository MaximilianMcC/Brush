using System.Numerics;
using Raylib_cs;

class Program
{
	public static void Main(string[] args)
	{
		Raylib.SetTraceLogLevel(TraceLogLevel.Warning);
		Raylib.SetConfigFlags(ConfigFlags.ResizableWindow);
		Raylib.InitWindow(800, 600, "John canvas, inventor of art");
		Raylib.SetTargetFPS(60);

		float brushSize = 25f;
		Color color = Color.Blue;
		List<BrushStroke> paint = new List<BrushStroke>();

		Raylib.HideCursor();

		while (!Raylib.WindowShouldClose())
		{
			if (Raylib.IsMouseButtonDown(MouseButton.Left))
			{
				paint.Add(new BrushStroke()
				{
					Position = Raylib.GetMousePosition(),
					Size = brushSize,
					Color = color
				});
			}

			if (Raylib.IsKeyPressed(KeyboardKey.LeftBracket)) brushSize--;
			if (Raylib.IsKeyPressed(KeyboardKey.RightBracket)) brushSize++;

			if (Raylib.IsKeyPressed(KeyboardKey.One)) color = Color.Blue;
			if (Raylib.IsKeyPressed(KeyboardKey.Two)) color = Color.Green;
			if (Raylib.IsKeyPressed(KeyboardKey.Three)) color = Color.White;
			if (Raylib.IsKeyPressed(KeyboardKey.Four)) color = Color.Red;

			Raylib.BeginDrawing();
			Raylib.ClearBackground(Color.White);

			foreach (BrushStroke stroke in paint)
			{
				Raylib.DrawCircleV(stroke.Position, stroke.Size, stroke.Color);
			}

			Raylib.DrawCircleLinesV(Raylib.GetMousePosition(), brushSize, Color.Black);

			Raylib.EndDrawing();
		}

		Raylib.CloseWindow();
	}
}