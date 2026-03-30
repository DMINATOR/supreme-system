using Godot;

public partial class BagScene : Control
{
	private SceneManager _sceneManager;
	private Button _backButton;

	public override void _Ready()
	{
		LoadNodes();
		PrepareNodes();
	}

	private void LoadNodes()
	{
		_sceneManager = GetNode<SceneManager>(AutoloadPath.SceneManager);
		_backButton = GetNode<Button>("VBoxContainer/BackButton");
	}

	private void PrepareNodes()
	{
		_backButton.Pressed += _sceneManager.GoToWorld;
	}
}
