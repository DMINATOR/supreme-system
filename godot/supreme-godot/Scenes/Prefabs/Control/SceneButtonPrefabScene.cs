using Godot;

public partial class SceneButtonPrefabScene : Button
{
	[Export]
	public PackedScene TargetScene { get; set; }

	public override void _Ready()
	{
		PrepareNodes();
	}

	private void PrepareNodes()
	{
		Pressed += OnPressed;
	}

	private void OnPressed()
	{
		if (TargetScene == null)
		{
			GD.PushError("SceneButtonPrefabScene: TargetScene export is not set.");
			return;
		}

		GetTree().ChangeSceneToPacked(TargetScene);
	}
}
