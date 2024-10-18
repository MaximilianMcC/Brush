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

		// Check for if they click on a layer
		List<Rectangle> layerPreviews = GetRectangles();
		for (int i = 0; i < layerPreviews.Count; i++)
		{
			// Check if they're clicking on the layer
			if (Raylib.IsMouseButtonPressed(MouseButton.Left))
			{
				if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), layerPreviews[i])) Canvas.SelectedLayerIndex = i;
			}

		}
	}

	public static void Render()
	{
		// Draw the layers bar thingy
		Raylib.DrawRectangleRec(new Rectangle(Raylib.GetScreenWidth() - layersWidth, 0, layersWidth, Raylib.GetScreenHeight()), Theme.BackgroundPrimary);

		// Draw layer previews on the side
		List<Rectangle> layerPreviews = GetRectangles();
		for (int i = 0; i < Canvas.Layers.Count; i++)
		{
			// Draw the transparent background
			Raylib.DrawTexturePro(
				Canvas.TransparentBackground,
				new Rectangle(0, 0, Canvas.Width, -Canvas.Height),
				layerPreviews[i],
				Vector2.Zero,
				0f,
				Color.White	
			);

			// Draw the actual layer
			Raylib.DrawTexturePro(
				Canvas.Layers[i].Texture,
				new Rectangle(0, 0, Canvas.Width, -Canvas.Height),
				layerPreviews[i],
				Vector2.Zero,
				0f,
				Color.White	
			);
		}
	}

	private static List<Rectangle> GetRectangles()
	{
		// Store all of the rectangles
		List<Rectangle> rectangles = new List<Rectangle>();

		// Calculate the height of the layer, and also make
		// a default rectangle thingy to copy off of (lazy)
		float layerPreviewHeight = ((layersWidth - padding2) / Canvas.Width) * Canvas.Height;
		Rectangle layerPreview = new Rectangle(Raylib.GetScreenWidth() - layersWidth + padding, padding, layersWidth - padding2, layerPreviewHeight);

		// Loop over all layers and get the rectangle
		for (int i = Canvas.Layers.Count - 1; i >= 0 ; i--)
		{
			// Save the current layer
			rectangles.Add(layerPreview);

			// Move downwards for the next layer
			layerPreview.Y += layerPreview.Height + padding;
		}

		// Ka Pai
		return rectangles;
	}
}