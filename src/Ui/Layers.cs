using Raylib_cs;

class Layers
{
	private const float layersWidth = 265f;

	public static void Update()
	{
		
	}

	public static void Render()
	{
		// Draw the layers bar thingy
		Raylib.DrawRectangleRec(new Rectangle(Raylib.GetScreenWidth() - layersWidth, 0, layersWidth, Raylib.GetScreenHeight()), Theme.BackgroundPrimary);
	}
}