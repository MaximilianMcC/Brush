using Raylib_cs;

class Theme
{
	// Dark theme
	private static readonly Color DarkForeground = new Color(255, 255, 255, 255);
	private static readonly Color DarkBackgroundPrimary = new Color(24, 24, 27, 255);
	private static readonly Color DarkBackgroundSecondary = new Color(31, 31, 35, 255);
	private static readonly Color DarkContent = new Color(40, 40, 40, 255);

	// Light theme
	private static readonly Color LightForeground = new Color(0, 0, 0, 255);
	private static readonly Color LightBackgroundPrimary = new Color(255, 255, 255, 255);
	private static readonly Color LightBackgroundSecondary = new Color(240, 244, 249, 255);
	private static readonly Color LightContent = new Color(230, 230, 230, 255);

	// Current colors
	public static Color Foreground;
	public static Color BackgroundPrimary;
	public static Color BackgroundSecondary;
	public static Color Content;

	// Fonts
	public static Font Font = AssetManager.LoadFont("./assets/fonts/bahnschrift.ttf");
	public static float FontSize = 16f;
	public static float FontSpacing = FontSize / 10f;

	public static void SetTheme(string colorThemeName)
	{
		// Set the color theme
		if (colorThemeName == "dark")
		{
			Foreground = DarkForeground;
			BackgroundPrimary = DarkBackgroundPrimary;
			BackgroundSecondary = DarkBackgroundSecondary;
			Content = DarkContent;
		}
		else if (colorThemeName == "light")
		{
			Foreground = LightForeground;
			BackgroundPrimary = LightBackgroundPrimary;
			BackgroundSecondary = LightBackgroundSecondary;
			Content = LightContent;
		}
	}

	public static void CleanUp()
	{
		Raylib.UnloadFont(Font);
	}
}