using Godot;

public partial class RegionMapScene : Control
{
	private SceneManager _sceneManager;

	public override void _Ready()
	{
		LoadNodes();
	}

	private void LoadNodes()
	{
		_sceneManager = GetNode<SceneManager>(AutoloadPath.SceneManager);
	}
}
