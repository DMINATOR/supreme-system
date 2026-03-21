using Godot;

public partial class DebugScene : Control
{
	private SceneManager _sceneManager;
	private OptionButton _sceneOption;

	public override void _Ready()
	{
		_sceneManager = GetNode<SceneManager>("/root/SceneManager");
		_sceneOption = GetNode<OptionButton>("VBoxContainer/SceneOption");
		_sceneOption.AddItem("Main Menu");
		_sceneOption.AddItem("Default Scene");
		GetNode<Button>("VBoxContainer/LoadButton").Pressed += OnLoadPressed;
	}

	private void OnLoadPressed()
	{
		switch (_sceneOption.Selected)
		{
			case 0:
				_sceneManager.GoToMainMenu();
				break;
			case 1:
				_sceneManager.GoToDefaultScene();
				break;
		}
	}
}
