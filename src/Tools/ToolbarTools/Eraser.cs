using System.Numerics;
using Raylib_cs;

class Eraser : Brush
{
	public override string Name => "Eraser";
	public override string Tutorial => "`[` Decrease Eraser \t `]` Increase Eraser";
	public override KeyboardKey ShortcutKey => KeyboardKey.E;

	public override void OnSelect()
	{
		base.OnSelect();

		// Set the blend mode to remove everything thats white or something idk
		//? idk wtf this is doing (shout out guy on discord)
		// TODO: Figure out what this is doing
		// TODO: name all the hex numbers
		Rlgl.SetBlendFactors(0x0302, 0x0302, 0x8007);
	}

	public override void Draw(Vector2 position)
	{
		// Draw a circle with the blending mode thing to rub out the area
		// TODO: Draw a brush texture so there can be different brushes
		Raylib.BeginBlendMode(BlendMode.CustomSeparate);
		Raylib.DrawCircleV(position, Size / Canvas.Zoom, Canvas.Color);
		Raylib.EndBlendMode();
	}

	public override void DrawUi(Vector2 position)
	{
		// Draw a circle around showing where the brush is gonna draw
		// TODO: Make it invert stuff behind with a shader or whatever
		Raylib.DrawCircleLinesV(Raylib.GetMousePosition(), Size, Color.DarkGray);
	}
}