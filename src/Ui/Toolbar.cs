using System.Numerics;
using Raylib_cs;

partial class Toolbar
{
	private const float toolbarWidth = 65f;
	private const float tutorialBarHeight = 45f;
	private const float padding = 10f;
	private const float padding2 = padding * 2;

	public static List<Tool> Tools = new List<Tool>();
	public static int SelectedToolIndex = 0;

	public static Tool CurrentTool {
		get { return Tools[SelectedToolIndex]; }
	}

	// Manually make the rectangle for the color picker
	// TODO: Put in some sorta class or something or at the very least a method
	private static Rectangle colorPickerRectangle;

	public static void Update()
	{
		// Calculate the color picker icon position
		float iconSize = toolbarWidth - padding2;
		colorPickerRectangle = new Rectangle(padding, Raylib.GetScreenHeight() - tutorialBarHeight - iconSize, new Vector2(iconSize));

		// Get all the rectangles for collision detection
		List<Rectangle> rectangles = GetToolRectangles();
		rectangles.Add(colorPickerRectangle);

		// Check for if they wanna change tool
		int previousSelectedToolIndex = SelectedToolIndex;
		for (int i = 0; i < rectangles.Count; i++)
		{
			// Check for if they click the button on the toolbar
			if (Raylib.IsMouseButtonDown(MouseButton.Left))
			{
				if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), rectangles[i]))
				{
					// Set the index or launch the specialist
					if (i >= Tools.Count)
					{
						if (i == (Tools.Count + 0)) ColorPicker.Open();
					}
					else SelectedToolIndex = i;
				}
			}

			// If the current rectangle isn't a tool then
			// don't look for its shortcut because it hasn't got one
			if (i >= Tools.Count) continue;

			// Check for if they press the shortcut
			if (Raylib.IsKeyPressed(Tools[i].ShortcutKey)) SelectedToolIndex = i;
		}

		// Manually check for if they do the color picker shortcut
		// TODO: Don't do manually
		if (Raylib.IsKeyPressed(ColorPicker.ShortcutKey)) ColorPicker.Open();

		// Check for if we selected a new tool. If we did
		// then run the select/deselect methods for the
		// tools and whatnot
		if (SelectedToolIndex != previousSelectedToolIndex)
		{
			// Deselect the previous tool, and select the current one
			Tools[previousSelectedToolIndex].OnDeselect();
			Tools[SelectedToolIndex].OnSelect();
		}
	}

	public static void Render()
	{
		// Draw the actual bar
		Raylib.DrawRectangleRec(new Rectangle(Vector2.Zero, toolbarWidth, Raylib.GetScreenHeight()), Theme.BackgroundPrimary);

		// Draw all the tools
		List<Rectangle> tools = GetToolRectangles();
		for (int i = 0; i < tools.Count; i++)
		{
			// Get the color depending on if the
			// tool is selected or not then draw it
			Color color = (i == SelectedToolIndex) ? Theme.Content : Theme.BackgroundSecondary;
			Raylib.DrawRectangleRec(tools[i], color);

			// Draw the tools icon on the icon thing
			Raylib.DrawTexturePro(Tools[i].Icon, AssetManager.GetTextureSize(Tools[i].Icon), tools[i], Vector2.Zero, 0f, Color.White);
		}

		// Calculate the smaller inner rectangle thingy
		// thingy that shows the actual color
		float scale = 0.8f;
		float margin = (1 - scale) / 2 * colorPickerRectangle.Width;
		Rectangle innerRectangle = new Rectangle(
			colorPickerRectangle.X + margin, 
			colorPickerRectangle.Y + margin, 
			colorPickerRectangle.Width * scale, 
			colorPickerRectangle.Height * scale
		);

		// manually draw the color picker
		// TODO: Don't do manually
		Raylib.DrawRectangleRec(colorPickerRectangle, Theme.Content);
		Raylib.DrawRectangleRec(innerRectangle, Canvas.Color);

		// Draw a tutorial bar thingy at the bottom
		// that has shortcuts and whatnot
		// TODO: Put this in another class called 'tutorial' or something
		float textY = Raylib.GetScreenHeight() - tutorialBarHeight;
		Raylib.DrawRectangleRec(new Rectangle(toolbarWidth, textY, Raylib.GetScreenWidth(), tutorialBarHeight), Theme.BackgroundPrimary);
		textY += padding;

		// Draw the tutorial text
		// TODO: Render keys for shortcuts
		Helper.DrawText(CurrentTool.Tutorial, new Vector2(toolbarWidth + padding, textY));
	}

	public static void CleanUp()
	{
		// Unload all the tools
		foreach (Tool tool in Tools) tool.CleanUp();
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
			float toolSize = toolbarWidth - padding2;
			tools.Add(new Rectangle(padding, y, new Vector2(toolSize)));

			// Move onto the next one
			y += toolSize + padding;
		}

		// Ka Pai
		return tools;
	}
}