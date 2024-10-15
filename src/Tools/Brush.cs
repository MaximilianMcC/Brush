using Raylib_cs;

class Brush : Tool
{
	public override string Name => "Brush";
	public override KeyboardKey ShortcutKey => KeyboardKey.B;

	public static float Size = 25;
	public static Color Color = Color.Green;

	public override void OnSelect() => Raylib.HideCursor();
	public override void OnDeselect() => Raylib.ShowCursor();

	public override void Update()
	{
		// Check for if they want to change the brush size
		if (Raylib.IsKeyPressed(KeyboardKey.LeftBracket) || Raylib.IsKeyPressedRepeat(KeyboardKey.LeftBracket)) Size--;
		if (Raylib.IsKeyPressed(KeyboardKey.RightBracket) || Raylib.IsKeyPressedRepeat(KeyboardKey.RightBracket)) Size++;

		// Make sure the brush size doesn't go into the negatives
		// TODO: Maybe add a max size but idk
		if (Size < 1) Size = 1;
	}

	public override void CanvasRender()
	{
		// TODO: Do this in update
		if (Raylib.IsMouseButtonDown(MouseButton.Left))
		{
			Raylib.DrawCircleV(Raylib.GetMousePosition(), Size, Color);
		}
	}

	public override void UiRender()
	{
		// Draw a circle around showing where the brush is gonna draw
		// TODO: Make it invert stuff behind with a shader or whatever
		Raylib.DrawCircleLinesV(Raylib.GetMousePosition(), Size, Color.Black);
	}
}