using Godot;

public partial class DebugScene : Control
{
	private SceneManager _sceneManager;
	private SaveManager _saveManager;
	private WorldManager _worldManager;
	private Button _mainMenuButton;
	private Button _defaultSceneButton;
	private Button _bagButton;
	private Label _activeSlotLabel;
	private Button _loadFromSlotButton;
	private Button _saveToActiveSlotButton;

	public override void _Ready()
	{
		LoadNodes();
		PrepareNodes();
	}

	private void LoadNodes()
	{
		_sceneManager = GetNode<SceneManager>(AutoloadPath.SceneManager);
		_saveManager = GetNode<SaveManager>(AutoloadPath.SaveManager);
		_worldManager = GetNode<WorldManager>(AutoloadPath.WorldManager);
		_mainMenuButton = GetNode<Button>("VBoxContainer/TabContainer/Scenes/MainMenuButton");
		_defaultSceneButton = GetNode<Button>("VBoxContainer/TabContainer/Scenes/DefaultSceneButton");
		_bagButton = GetNode<Button>("VBoxContainer/TabContainer/Scenes/BagButton");
		_activeSlotLabel = GetNode<Label>("VBoxContainer/TabContainer/World/ActiveSlotLabel");
		_loadFromSlotButton = GetNode<Button>("VBoxContainer/TabContainer/World/LoadFromSlotButton");
		_saveToActiveSlotButton = GetNode<Button>("VBoxContainer/TabContainer/World/SaveToActiveSlotButton");
	}

	private void PrepareNodes()
	{
		_mainMenuButton.Pressed += _sceneManager.GoToMainMenu;
		_defaultSceneButton.Pressed += _sceneManager.GoToDefaultScene;
		_bagButton.Pressed += _sceneManager.GoToBag;
		_loadFromSlotButton.Pressed += OnLoadFromSlotPressed;
		_saveToActiveSlotButton.Pressed += OnSaveToActiveSlotPressed;
		RefreshWorldTab();
	}

	private void RefreshWorldTab()
	{
		_activeSlotLabel.Text = $"Active Slot: {_saveManager.ActiveSlotIndex}";
	}

	private void OnLoadFromSlotPressed()
	{
		if (_saveManager.ActiveSlotIndex >= 0)
			_worldManager.LoadFromSlot(this, _saveManager, _saveManager.ActiveSlotIndex);
		RefreshWorldTab();
	}

	private void OnSaveToActiveSlotPressed()
	{
		if (_saveManager.ActiveSlotIndex >= 0)
			_worldManager.SaveToActiveSlot(_saveManager);
		RefreshWorldTab();
	}
}
