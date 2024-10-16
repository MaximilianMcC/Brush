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
	
		// Check for if they do ctrl+o to open a file
		if (Raylib.IsKeyDown(KeyboardKey.LeftControl) && Raylib.IsKeyPressed(KeyboardKey.O))
		{
			// Get the default pictures path
			string picturesPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

			// Open the save file dialogue thingy
			// to ask them where they wanna save it
			FileFilter filter = new FileFilter("Image files", ["*.jpg", "*.jpeg", "*.png"]);
			var (canceled, path) = TinyDialogs.OpenFileDialog("Saving the picture rn (whereabouts)", picturesPath, false, filter);
			

			// Open the image
			if (canceled == false)
			{
				// Load in the image
				Texture2D image = Raylib.LoadTexture(path.First());

				// Load a new canvas thingy from the texture
				Canvas.Setup(image.Width, image.Height);
				
				// Paste the image on top of the canvas
				// to make it look like we opened it
				Raylib.BeginTextureMode(Canvas.RenderTexture);
				Raylib.DrawTexture(image, 0, 0, Color.White);
				Raylib.EndTextureMode();

				// Unload the image since we're done using it
				// and its been baked into the canvas
				Raylib.UnloadTexture(image);
			}
		}
	}
}