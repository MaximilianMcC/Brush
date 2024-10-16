using Raylib_cs;
using TinyDialogsNet;

class FileManager
{
	public static void Update()
	{
		// Check for if they do ctrl+s to save a file
		if (Raylib.IsKeyDown(KeyboardKey.LeftControl) && Raylib.IsKeyPressed(KeyboardKey.S))
		{
			// Get the default pictures path
			string picturesPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
			string defaultPath = Path.Combine(picturesPath, "mean-as-picture.png");

			// Open the save file dialogue thingy
			// to ask them where they wanna save it
			FileFilter filter = new FileFilter("Image files", ["*.jpg", "*.jpeg", "*.png"]);
			var (canceled, path) = TinyDialogs.SaveFileDialog("Saving the picture rn (whereabouts)", defaultPath, filter);
			
			// Save the image
			if (canceled == false)
			{
				// Get the canvas as an image and
				// rotate it because render textures
				// are drawn upside down
				Image image = Raylib.LoadImageFromTexture(Canvas.RenderTexture.Texture);
				Raylib.ImageFlipVertical(ref image);

				// Save then unload the image
				Raylib.ExportImage(image, path);
				Raylib.UnloadImage(image);
			}
		}
	}
}