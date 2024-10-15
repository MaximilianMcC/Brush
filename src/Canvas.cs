using System.Numerics;
using Raylib_cs;

class Canvas
{
	public static RenderTexture2D RenderTexture;
	private static Camera2D camera;
	
	public static float Zoom {
		get { return camera.Zoom; }
	}

	public static void Setup(int width, int height)
	{
		// Make the render texture so we can draw on it
		RenderTexture = Raylib.LoadRenderTexture(width, height);
		
		// Make a 2D camera so we can move around the canvas easy
		camera = new Camera2D()
		{
			Target = new Vector2(width, height) / 2,
			Offset = new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight()) / 2,
			Rotation = 0f,
			Zoom = 0.8f
		};

		// Put a quick background color on the thing
		// TODO: Make it transparent by default
		Raylib.BeginTextureMode(RenderTexture);
		Raylib.ClearBackground(Color.White);
		Raylib.EndTextureMode();
	}

	public static void Render()
	{
		// Draw the canvas
		Raylib.BeginMode2D(camera);
		Raylib.DrawTexturePro(RenderTexture.Texture, new Rectangle(0, 0, RenderTexture.Texture.Width, -RenderTexture.Texture.Height), new Rectangle(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight()), Vector2.Zero, 0f, Color.White);
		Raylib.EndMode2D();
	}

	public static void CleanUp()
	{
		Raylib.UnloadRenderTexture(RenderTexture);
	}

	public static Vector2 MousePosition()
	{
		return Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), camera);
	}
}