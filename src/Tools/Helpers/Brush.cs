using System.Numerics;
using Raylib_cs;

class Brush : Tool
{
	// TODO: Make it so these don't have to be here
	public override string Name => "";
	public override string Tutorial => "";
	public override KeyboardKey ShortcutKey => KeyboardKey.Null;

	// Settings
	public float Size = 25;
	public float ResizeScaler = 10f;
	
	private Vector2 previousBrushPosition;
	private bool brushPreviouslyDown = false;

	public override void OnSelect() => Raylib.HideCursor();
	public override void OnDeselect() => Raylib.ShowCursor();

	public override void Update()
	{
		// Check for if they want to resize the brush
		if (Raylib.IsKeyPressed(KeyboardKey.LeftBracket) || Raylib.IsKeyPressedRepeat(KeyboardKey.LeftBracket)) Size -= ResizeScaler;
		if (Raylib.IsKeyPressed(KeyboardKey.RightBracket) || Raylib.IsKeyPressedRepeat(KeyboardKey.RightBracket)) Size += ResizeScaler;

		// Make sure the brush size doesn't go into the negatives
		// TODO: Maybe add a max size but idk
		if (Size < 1) Size = 1;
	}

	// Check for if the brush is "on" the canvas (clicking)
	public override void CanvasRender()
	{
		// Check for if the brush is on the canvas
		if (Raylib.IsMouseButtonDown(MouseButton.Left) == false)
		{
			brushPreviouslyDown = false;
			return;
		}
		
		// Get the brush position
		Vector2 brushPosition = Canvas.MousePosition;
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
				Vector2 smoothedPosition = Vector2.Lerp(previousBrushPosition, brushPosition, i / distance);

				// Use the brush
				Draw(smoothedPosition);
			}
		}
		else
		{
			// Use the brush a single time without lerping
			Draw(brushPosition);
		}

		// Say that we just had the brush down and
		// also update the position now
		brushPreviouslyDown = true;
		previousBrushPosition = brushPosition;
	}

	public override void UiRender() => DrawUi(Canvas.MousePosition);

	public virtual void Draw(Vector2 brushPosition)
	{

	}

	public virtual void DrawUi(Vector2 brushPosition)
	{
		
	}
}