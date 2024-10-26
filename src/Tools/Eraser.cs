using System.Numerics;
using Raylib_cs;

class Eraser : Tool
{
	public override string Name => "Eraser";
	public override string Tutorial => "`[` Decrease Eraser \t `]` Increase Eraser";
	public override KeyboardKey ShortcutKey => KeyboardKey.E;

	// Eraser settings
	public static float Size = 25;

	private static Vector2 previousEraserPosition;
	private static bool EraserPreviouslyDown = false;

	public override void Update()
	{
		// Check for if they want to change the Eraser size
		const float EraserIncrease = 3f;
		if (Raylib.IsKeyPressed(KeyboardKey.LeftBracket) || Raylib.IsKeyPressedRepeat(KeyboardKey.LeftBracket)) Size -= EraserIncrease;
		if (Raylib.IsKeyPressed(KeyboardKey.RightBracket) || Raylib.IsKeyPressedRepeat(KeyboardKey.RightBracket)) Size += EraserIncrease;

		// Make sure the Eraser size doesn't go into the negatives
		// TODO: Maybe add a max size but idk
		if (Size < 1) Size = 1;
	}

	public override void CanvasRender()
	{
		// Check for if the Eraser is "on" the canvas
		if (Raylib.IsMouseButtonDown(MouseButton.Left) == false)
		{
			EraserPreviouslyDown = false;
			return;
		}
		
		// Get the Eraser position
		Vector2 EraserPosition = Canvas.MousePosition();
		if (EraserPreviouslyDown == false) previousEraserPosition = EraserPosition;

		// Get the distance that the Eraser has traveled
		// so we can interpolate between it so the drawing
		// is super smooth and whatnot and no fat circles
		//! Might not need the > 0 check idk
		float distance = Vector2.Distance(previousEraserPosition, EraserPosition);
		if (distance > 0)
		{
			// TODO: Only have this part in the render thing
			for (int i = 0; i < distance; i++)
			{
				// Draw the Eraser thingy lerped
				Vector2 position = Vector2.Lerp(previousEraserPosition, EraserPosition, i / distance);
				Erase(position);
			}
		}
		else Erase(EraserPosition);

		// Say that we just had the Eraser down and
		// also update the position now
		EraserPreviouslyDown = true;
		previousEraserPosition = EraserPosition;
	}

	public override void UiRender()
	{
		// Draw a circle around showing where the Eraser is gonna draw
		// TODO: Make it invert stuff behind with a shader or whatever
		Raylib.DrawCircleLinesV(Raylib.GetMousePosition(), Size, Color.DarkGray);
	}

	private static void Erase(Vector2 position)
	{
		// Set the blend mode to remove everything thats white or something idk
		//? idk wtf this is doing (shout out guy on discord)
		// TODO: Set blend factors on select
		Rlgl.SetBlendFactors(0x0302, 0x0302, 0x8007);
		Raylib.BeginBlendMode(BlendMode.CustomSeparate);

		// Do the actual rubbing out/erasing
		Raylib.DrawCircleV(position, Size / Canvas.Zoom, Color.White);

		// Return to the normal blend mode thing
		Raylib.EndBlendMode();
	}

	public override void OnSelect() => Raylib.HideCursor();
	public override void OnDeselect() => Raylib.ShowCursor();
}