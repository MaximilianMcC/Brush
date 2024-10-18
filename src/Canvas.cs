using System.Numerics;
using Raylib_cs;

class Canvas
{
	public static int Width;
	public static int Height;
	public static List<RenderTexture2D> Layers;

	public static Texture2D TransparentBackground;
	private static Image tempFlattenedImage;

	public static int SelectedLayerIndex = 0;
	public static RenderTexture2D CurrentLayer {
		get { return Layers[SelectedLayerIndex]; }
	}

	private static Camera2D camera;
	public static float Zoom {
		get { return camera.Zoom; }
	}

	public static Color Color = Color.Magenta;

	public static void Setup(int width, int height)
	{	
		// Set the size of the canvas
		Width = width;
		Height = height;

		// Make a 2D camera so we can move around the canvas easy
		camera = new Camera2D()
		{
			Target = new Vector2(width, height) / 2,
			Offset = new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight()) / 2,
			Rotation = 0f,
			Zoom = 0.9f
		};

		// Generate a texture thing that has the
		// transparent checked squares
		tempFlattenedImage = Raylib.GenImageChecked(Width, Height, 32, 32, Color.White, Color.LightGray);
		TransparentBackground = Raylib.LoadTextureFromImage(tempFlattenedImage);

		// Make a first default layer thing 
		Layers = new List<RenderTexture2D>();
		AddLayer();
	}

	public static void Update()
	{
		// Check for if they wanna zoom in (ctrl + scroll)
		const float zoomZoomeyZoomer = 0.01f;
		if (Raylib.IsKeyDown(KeyboardKey.LeftControl))
		{
			// Zoom in according to the delta mouse scroll
			camera.Zoom += Raylib.GetMouseWheelMove() * zoomZoomeyZoomer;
			Console.WriteLine(Zoom);
		}
	}

	public static void Render()
	{
		// Draw the canvas
		Raylib.BeginMode2D(camera);

		// Draw a transparent background thingy in the very back
		Raylib.DrawTexturePro(TransparentBackground, new Rectangle(0, 0, Width, -Height), new Rectangle(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight()), Vector2.Zero, 0f, Color.White);

		// Draw all the layers
		DrawAllLayers();

		Raylib.EndMode2D();
	}

	public static void CleanUp()
	{
		// Unload all of the layers
		foreach (RenderTexture2D layer in Layers) Raylib.UnloadRenderTexture(layer);

		// Unload the temporary flattened image
		Raylib.UnloadImage(tempFlattenedImage);
	}

	public static Vector2 MousePosition()
	{
		// TODO: Make this a property
		return Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), camera);
	}

	public static void AddLayer(Color? color = null)
	{
		// Make the actual layer
		RenderTexture2D currentLayer = Raylib.LoadRenderTexture(Width, Height);

		// If a color was supplied then fill it
		// as the background thingy
		if (color != null)
		{
			Raylib.BeginTextureMode(currentLayer);
			Raylib.DrawRectangle(0, 0, Width, Height, (Color)color);
			Raylib.EndTextureMode();
		}

		// Add it to the list of layers and also
		// set it to be the selected layer rn
		Layers.Insert(0, currentLayer);
		SelectedLayerIndex = 0;
	}

	public static Image GetFlattenedImage()
	{
		// Make a new render texture so that we can draw
		// all the layers onto it before making it an image
		RenderTexture2D renderTexture = Raylib.LoadRenderTexture(Width, Height);

		// Loop over every layer and draw it onto the image
		Raylib.BeginTextureMode(renderTexture);
		DrawAllLayers();
		Raylib.EndTextureMode();

		// Put the render texture onto the image then
		// flip it because openGl draws upside down
		tempFlattenedImage = Raylib.LoadImageFromTexture(renderTexture.Texture);
		Raylib.ImageFlipVertical(ref tempFlattenedImage);

		// Get rid of the render texture then give them
		// back the temp image. The image is unloaded in
		// cleanup() at the very end
		Raylib.UnloadRenderTexture(renderTexture);
		return tempFlattenedImage;
	}

	private static void DrawAllLayers()
	{
		// Loop over every layer and draw it
		// TODO: Might need to use reverse for loop
		foreach (RenderTexture2D layer in Layers)
		{
			Raylib.DrawTexturePro(layer.Texture, new Rectangle(0, 0, Width, -Height), new Rectangle(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight()), Vector2.Zero, 0f, Color.White);
		}
	}
}