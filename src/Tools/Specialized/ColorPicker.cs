using System.Numerics;
using Raylib_cs;
using TinyDialogsNet;

class ColorPicker
{
	public static KeyboardKey ShortcutKey = KeyboardKey.K;

	public static void Open()
	{
		// Open the color dialogue thingy
		// TODO: Don't use var
		var (canceled, colorString) = TinyDialogs.ColorChooser("Choosing the color rn", "#FF00FF");
		
		// Set the color
		if (canceled == false)
		{
			// Extract all bytes
			colorString = colorString.Remove(0, 1);
			byte red = Convert.ToByte(colorString.Substring(1, 2), 16);
			byte green = Convert.ToByte(colorString.Substring(2, 2), 16);
			byte blue = Convert.ToByte(colorString.Substring(4, 2), 16);

			// Make then set the color
			// TODO: Don't set the color just for brush. Have one thing that store the color yk
			Color color = new Color(red, green, blue, byte.MaxValue);
			Canvas.Color = color;
		}
	}

	public static Rectangle GetRectangle(Vector2 position, Vector2 size)
	{
		return new Rectangle(position, size);
	}
}