using System.Numerics;
using Raylib_cs;

class Brush : Tool
{
	public override string Name => "Brush";
	public override string Tutorial => "`[` Decrease Brush \t `]` Increase Brush";
	public override KeyboardKey ShortcutKey => KeyboardKey.B;

	// Brush settings
	public static float Size = 25;

	private static Vector2 previousBrushPosition;
	private static bool brushPreviouslyDown = false;

	public override void Update()
	{
		// Check for if they want to change the brush size
		const float brushIncrease = 3f;
		if (Raylib.IsKeyPressed(KeyboardKey.LeftBracket) || Raylib.IsKeyPressedRepeat(KeyboardKey.LeftBracket)) Size -= brushIncrease;
		if (Raylib.IsKeyPressed(KeyboardKey.RightBracket) || Raylib.IsKeyPressedRepeat(KeyboardKey.RightBracket)) Size += brushIncrease;

		// Make sure the brush size doesn't go into the negatives
		// TODO: Maybe add a max size but idk
		if (Size < 1) Size = 1;
	}

	public override void CanvasRender()
	{
		// Check for if the brush is "on" the canvas
		if (Raylib.IsMouseButtonDown(MouseButton.Left) == false)
		{
			brushPreviouslyDown = false;
			return;
		}
		
		// Get the brush position
		Vector2 brushPosition = Canvas.MousePosition();
		if (brushPreviouslyDown == false) previousBrushPosition = brushPosition;

		// Get the distance that the brush has traveled
		// so we can interpolate between it so the drawing
		// is super smooth and whatnot and no fat circles
		//! Might not need the > 0 check idk
		float distance = Vector2.Distance(previousBrushPosition, brushPosition);
		if (distance > 0)
		{
			// TODO: Only have this part in the render thing
			for (int i = 0; i < distance; i++)
			{
				// Draw the brush thingy lerped
				Vector2 position = Vector2.Lerp(previousBrushPosition, brushPosition, i / distance);
				Draw(position);
			}
		}
		else Draw(brushPosition);

		// Say that we just had the brush down and
		// also update the position now
		brushPreviouslyDown = true;
		previousBrushPosition = brushPosition;
	}

	public override void UiRender()
	{
		// Draw a circle around showing where the brush is gonna draw
		// TODO: Make it invert stuff behind with a shader or whatever
		Raylib.DrawCircleLinesV(Raylib.GetMousePosition(), Size, Color.DarkGray);
	}

	private static void Draw(Vector2 position)
	{
		// TODO: Use an image instead of a circle (different brushes)
		Raylib.DrawCircleV(position, Size / Canvas.Zoom, Canvas.Color);
	}

	public override void OnSelect() => Raylib.HideCursor();
	public override void OnDeselect() => Raylib.ShowCursor();
}