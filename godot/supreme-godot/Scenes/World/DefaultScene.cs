using Godot;

public partial class DefaultScene : Node2D
{
	private SaveManager _saveManager;

	public override void _Ready()
	{
		LoadNodes();
		PrepareNodes();
	}

	public override void _Process(double delta)
	{
	}

	private void LoadNodes()
	{
		_saveManager = GetNode<SaveManager>(AutoloadPath.SaveManager);
	}

	private void PrepareNodes()
	{
		GD.Print($"Info: DefaultScene ready — slot {_saveManager.ActiveSlotIndex}");
	}
}
