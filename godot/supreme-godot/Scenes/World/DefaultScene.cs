using Godot;

public partial class DefaultScene : Node2D
{
	private SaveManager _saveManager;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_saveManager = GetNode<SaveManager>(AutoloadPath.SaveManager);
		GD.Print($"Info: DefaultScene ready — slot {_saveManager.ActiveSlotIndex}");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
