using System.Numerics;
using Raylib_cs;

class PaintBrush : Brush
{
	public override string Name => "Brush";
	public override string Tutorial => "`[` Decrease Brush \t `]` Increase Brush";
	public override KeyboardKey ShortcutKey => KeyboardKey.B;

	public override void Draw(Vector2 position)
	{
		// Draw a circle
		// TODO: Draw a brush texture so there can be different brushes
		Raylib.DrawCircleV(position, Size / Canvas.Zoom, Canvas.Color);
	}

	public override void DrawUi(Vector2 position)
	{
		// Draw a circle around showing where the brush is gonna draw
		// TODO: Make it invert stuff behind with a shader or whatever
		Raylib.DrawCircleLinesV(Raylib.GetMousePosition(), Size, Color.DarkGray);
	}
}