using Godot;

public partial class MainMenu : Control
{
	private SceneManager _sceneManager;

	public override void _Ready()
	{
		_sceneManager = GetNode<SceneManager>("/root/SceneManager");
		GetNode<Button>("VBoxContainer/PlayButton").Pressed += OnPlayPressed;
	}

	private void OnPlayPressed()
	{
		_sceneManager.GoToSlotSelection();
	}
}
