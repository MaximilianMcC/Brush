using Raylib_cs;

abstract class Tool
{
	public abstract string Name { get; }
	public virtual string Tutorial { get; } = "";
	public abstract KeyboardKey ShortcutKey { get; }

	public virtual void OnSelect() { }
	public virtual void Update() { }
	public virtual void UiRender() { }
	public virtual void CanvasRender() { }
	public virtual void OnDeselect() { }
}