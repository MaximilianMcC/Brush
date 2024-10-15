using Raylib_cs;

abstract class Tool
{
	public abstract string Name { get; }
	public abstract KeyboardKey ShortcutKey { get; }

	public virtual void OnSelect() { }
	public virtual void Update() { }
	public virtual void Render() { }
	public virtual void OnDeselect() { }
}