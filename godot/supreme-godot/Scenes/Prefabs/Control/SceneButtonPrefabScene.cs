using Godot;

public partial class SceneButtonPrefabScene : Button
{
	[Export]
	public SceneManager.GameScene TargetScene { get; set; }

	private SceneManager _sceneManager;

	public override void _Ready()
	{
		LoadNodes();
		PrepareNodes();
	}

	private void LoadNodes()
	{
		_sceneManager = GetNode<SceneManager>(AutoloadPath.SceneManager);
	}

	private void PrepareNodes()
	{
		Pressed += OnPressed;
	}

	private void OnPressed()
	{
		_sceneManager.GoTo(TargetScene);
	}
}
