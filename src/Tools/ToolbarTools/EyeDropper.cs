using System.Numerics;
using Raylib_cs;

class EyeDropper : Tool
{
	public override string Name => "Eye Dropper";
	public override string Tutorial => "Click on a color to select it";
	public override KeyboardKey ShortcutKey => KeyboardKey.I;

	public override void OnSelect() => Raylib.HideCursor();
	public override void OnDeselect() => Raylib.ShowCursor();

	// TODO: Make a magnifier thingy so you can see what you're doing
	public override void Update()
	{
		// Check for if the brush is "on" the canvas
		// TODO: Make a onClick or onMouseDown method thing
		if (Raylib.IsMouseButtonDown(MouseButton.Left) == false) return;

		// Get the mouse position as separate ints
		Vector2 mousePosition = Canvas.MousePosition;
		int x = (int)mousePosition.X;
		int y = (int)mousePosition.Y;

		// If the mouse isn't on the canvas
		// then don't do anything
		if (Raylib.CheckCollisionPointRec(mousePosition, AssetManager.GetTextureSize(Canvas.CurrentLayer.Texture)) == false) return;

		// Turn the canvas into an image then
		// extract the color at the position. Unload
		// the image afterwards since we don't need it
		Color color = Raylib.GetImageColor(Canvas.GetFlattenedImage(), x, y);

		// Set the color as the active color
		Canvas.Color = color;
	}

	public override void UiRender()
	{
		// Draw the icon on the cursor
		Raylib.DrawTexturePro(Icon, AssetManager.GetTextureSize(Icon), new Rectangle(Raylib.GetMousePosition() - new Vector2(0, 32), new Vector2(32)), Vector2.Zero, 0f, Color.White);
	}
}