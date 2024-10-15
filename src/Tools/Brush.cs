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

	}

	public override void Render()
	{
		
	}
}