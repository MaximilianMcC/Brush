using Raylib_cs;

class Cursor : Tool
{
	public override string Name => "Cursor";
	public override KeyboardKey ShortcutKey => KeyboardKey.V;

	public override void OnSelect() => Raylib.ShowCursor();
}