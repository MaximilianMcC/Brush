using System.Numerics;
using Raylib_cs;

class Toolbar
{
	private const float width = 65f;
	private const float padding = 10f;
	private const float padding2 = padding * 2;

	public static List<Tool> Tools = new List<Tool>();
	public static int SelectedToolIndex = 0;

	public static void Update()
	{
		// Check for if they wanna change tool
		for (int i = 0; i < Tools.Count; i++)
		{
			// Check for if they press the shortcut
			if (Raylib.IsKeyPressed(Tools[i].ShortcutKey)) SelectedToolIndex = i;

			// Check for if they click the button on the toolbar
			if (Raylib.IsMouseButtonDown(MouseButton.Left))
			{
				if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), GetToolRectangles()[i])) SelectedToolIndex = i;
			}
		}
	}

	public static void Render()
	{
		// Draw the actual bar
		Raylib.DrawRectangleRec(new Rectangle(Vector2.Zero, width, Raylib.GetScreenHeight()), ColorTheme.BackgroundPrimary);

		// Draw all the tools
		List<Rectangle> tools = GetToolRectangles();
		for (int i = 0; i < tools.Count; i++)
		{
			// Get the color depending on if the
			// tool is selected or not then draw it
			Color color = (i == SelectedToolIndex) ? ColorTheme.Content : ColorTheme.BackgroundSecondary;
			Raylib.DrawRectangleRec(tools[i], color);
		}
	}

	private static List<Rectangle> GetToolRectangles()
	{
		// TODO: Use array
		List<Rectangle> tools = new List<Rectangle>();

		// Loop through all the tools
		float y = padding;
		for (int i = 0; i < Tools.Count; i++)
		{
			// Get the size of each tool and
			// the rectangle from that
			float toolSize = width - padding2;
			tools.Add(new Rectangle(padding, y, new Vector2(toolSize)));

			// Move onto the next one
			y += toolSize + padding;
		}

		// Ka Pai
		return tools;
	}
}