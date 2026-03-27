using Godot;

public partial class {SceneName} : {BaseType}
{
	private SceneManager _sceneManager;
	// TODO: Add other autoload fields as needed (SaveManager, WorldManager)

	public override void _Ready()
	{
		LoadNodes();
		PrepareNodes();
	}

	private void LoadNodes()
	{
		_sceneManager = GetNode<SceneManager>(AutoloadPath.SceneManager);
		// TODO: Add GetNode calls here
	}

	private void PrepareNodes()
	{
		// TODO: Wire signals and initialize state here
	}
}
