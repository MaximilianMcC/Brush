using Raylib_cs;

abstract class Tool
{
	public abstract string Name { get; }
	public virtual string Tutorial { get; } = "";
	public abstract KeyboardKey ShortcutKey { get; }
	public Texture2D Icon;

	public Tool()
	{
		// Load in the icon based on the name of the tool
		string name = Name.ToLower().Replace(' ', '-');
		Icon = AssetManager.LoadTexture(@$"./assets/icons/{name}.png");
	}

	public virtual void OnSelect() { }
	public virtual void Update() { }
	public virtual void UiRender() { }
	public virtual void CanvasRender() { }
	public virtual void OnDeselect() { }

	public void CleanUp()
	{
		// Unload the icon
		Raylib.UnloadTexture(Icon);
	}
}