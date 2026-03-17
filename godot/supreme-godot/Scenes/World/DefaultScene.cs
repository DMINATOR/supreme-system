using Godot;

public partial class DefaultScene : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var saveManager = GetNode<SaveManager>("/root/SaveManager");
		GD.Print($"Info: DefaultScene ready — slot {saveManager.ActiveSlotIndex}");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
