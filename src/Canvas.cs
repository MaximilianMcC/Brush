using System.Numerics;
using Raylib_cs;

class Canvas
{
	private const float renderPadding = 50f;

	public static RenderTexture2D RenderTexture;

	public static void Setup(int width, int height)
	{
		RenderTexture = Raylib.LoadRenderTexture(width, height);
		
		Raylib.BeginTextureMode(RenderTexture);
		Raylib.ClearBackground(Color.White);
		Raylib.EndTextureMode();
	}

	public static void Render()
	{
		// TODO: Draw the canvas so that its slightly scaled down to fit inside window with padding
		// Raylib.DrawTexture(RenderTexture.Texture, 0, 0, Color.White);
		Raylib.DrawTexturePro(RenderTexture.Texture, new Rectangle(0, 0, RenderTexture.Texture.Width, -RenderTexture.Texture.Height), new Rectangle(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight()), Vector2.Zero, 0f, Color.White);
	}

	public static void CleanUp()
	{
		Raylib.UnloadRenderTexture(RenderTexture);
	}
}