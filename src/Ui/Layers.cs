using System.Numerics;
using Raylib_cs;

class Layers
{
	private const float layersWidth = 256f;
	private const float padding = 10f;
	private const float padding2 = padding * 2;

	public static void Update()
	{
		// Check for if they press ctrl+shift+n to make a new layer
		if (Raylib.IsKeyDown(KeyboardKey.LeftControl) && Raylib.IsKeyDown(KeyboardKey.LeftShift) && Raylib.IsKeyPressed(KeyboardKey.N)) Canvas.AddLayer();
	}

	public static void Render()
	{
		// Draw the layers bar thingy
		Raylib.DrawRectangleRec(new Rectangle(Raylib.GetScreenWidth() - layersWidth, 0, layersWidth, Raylib.GetScreenHeight()), Theme.BackgroundPrimary);

		// Draw a smaller version of all the layers
		// going downwards
		float layerPreviewHeight = ((layersWidth - padding2) / Canvas.Width) * Canvas.Height;
		Rectangle layerPreview = new Rectangle(Raylib.GetScreenWidth() - layersWidth + padding, padding, layersWidth - padding2, layerPreviewHeight);
		for (int i = Canvas.Layers.Count - 1; i >= 0 ; i--)
		{
			// Draw the transparent background
			Raylib.DrawTexturePro(
				Canvas.TransparentBackground,
				new Rectangle(0, 0, Canvas.Width, -Canvas.Height),
				layerPreview,
				Vector2.Zero,
				0f,
				Color.White	
			);

			// Draw the actual layer
			Raylib.DrawTexturePro(
				Canvas.Layers[i].Texture,
				new Rectangle(0, 0, Canvas.Width, -Canvas.Height),
				layerPreview,
				Vector2.Zero,
				0f,
				Color.White	
			);

			// Move downwards for the next layer
			layerPreview.Y += layerPreview.Height + padding;
		}
	}
}