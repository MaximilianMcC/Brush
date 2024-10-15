using System.Numerics;
using System.Text.RegularExpressions;
using Raylib_cs;

partial class Toolbar
{
	private const float toolbarWidth = 65f;
	private const float tutorialBarHeight = 45f;
	private const float padding = 10f;
	private const float padding2 = padding * 2;

	public static List<Tool> Tools = new List<Tool>();
	private static List<Texture2D> iconTextures = new List<Texture2D>();
	public static int SelectedToolIndex = 0;

	public static Tool CurrentTool {
		get { return Tools[SelectedToolIndex]; }
	}

	public static void Update()
	{
		// Check for if they wanna change tool
		int previousSelectedToolIndex = SelectedToolIndex;
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
			Raylib.DrawTexturePro(iconTextures[i], AssetManager.GetTextureSize(iconTextures[i]), tools[i], Vector2.Zero, 0f, Color.White);
		}

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
		foreach (Texture2D icon in iconTextures) Raylib.UnloadTexture(icon);
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

	public static void RegisterTool(Tool tool)
	{
		Tools.Add(tool);
		iconTextures.Add(AssetManager.LoadTexture(@$"./assets/icons/{tool.Name.ToLower().Replace(' ', '-')}.png"));
	}
}