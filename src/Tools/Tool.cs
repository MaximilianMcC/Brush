using Raylib_cs;

abstract class Tool
{
	public abstract string Name { get; }
	public abstract KeyboardKey ShortcutKey { get; }

	public abstract void Update();
	public abstract void Render();
}