using Godot;

public partial class DebugScene : Control
{
	private SceneManager _sceneManager;
	private Button _mainMenuButton;
	private Button _defaultSceneButton;
	private Button _bagButton;

	public override void _Ready()
	{
		_sceneManager = GetNode<SceneManager>(AutoloadPath.SceneManager);
		_mainMenuButton = GetNode<Button>("VBoxContainer/TabContainer/Scenes/MainMenuButton");
		_defaultSceneButton = GetNode<Button>("VBoxContainer/TabContainer/Scenes/DefaultSceneButton");
		_bagButton = GetNode<Button>("VBoxContainer/TabContainer/Scenes/BagButton");
		_mainMenuButton.Pressed += _sceneManager.GoToMainMenu;
		_defaultSceneButton.Pressed += _sceneManager.GoToDefaultScene;
		_bagButton.Pressed += _sceneManager.GoToBag;
	}
}
