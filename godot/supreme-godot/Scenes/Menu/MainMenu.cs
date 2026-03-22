using Godot;

public partial class MainMenu : Control
{
	private SceneManager _sceneManager;
	private Button _playButton;

	public override void _Ready()
	{
		_sceneManager = GetNode<SceneManager>(AutoloadPath.SceneManager);
		_playButton = GetNode<Button>("VBoxContainer/PlayButton");
		_playButton.Pressed += OnPlayPressed;
	}

	private void OnPlayPressed()
	{
		_sceneManager.GoToSlotSelection();
	}
}
