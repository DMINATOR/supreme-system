using Godot;

public partial class MainMenu : Control
{
	private SceneManager _sceneManager;
	private Button _playButton;

	public override void _Ready()
	{
		LoadNodes();
		PrepareNodes();
	}

	private void LoadNodes()
	{
		_sceneManager = GetNode<SceneManager>(AutoloadPath.SceneManager);
		_playButton = GetNode<Button>("VBoxContainer/PlayButton");
	}

	private void PrepareNodes()
	{
		_playButton.Pressed += OnPlayPressed;
	}

	private void OnPlayPressed()
	{
		_sceneManager.GoToSlotSelection();
	}
}
