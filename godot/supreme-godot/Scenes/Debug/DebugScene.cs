using Godot;

public partial class DebugScene : Control
{
	private SceneManager _sceneManager;
	private SaveManager _saveManager;
	private WorldManager _worldManager;
	private Button _mainMenuButton;
	private Button _defaultSceneButton;
	private Button _bagButton;
	private Button _cardCreatorButton;
	private Label _activeSlotLabel;
	private Button _loadFromSlotButton;
	private Button _saveToActiveSlotButton;
	private Label _slotSummaryLabel;
	private TextEdit _slotJsonTextEdit;

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
		_cardCreatorButton = GetNode<Button>("VBoxContainer/TabContainer/Scenes/CardCreatorButton");
		_activeSlotLabel = GetNode<Label>("VBoxContainer/TabContainer/World/ActiveSlotLabel");
		_loadFromSlotButton = GetNode<Button>("VBoxContainer/TabContainer/World/LoadFromSlotButton");
		_saveToActiveSlotButton = GetNode<Button>("VBoxContainer/TabContainer/World/SaveToActiveSlotButton");
		_slotSummaryLabel = GetNode<Label>("VBoxContainer/TabContainer/World/SlotSummaryLabel");
		_slotJsonTextEdit = GetNode<TextEdit>("VBoxContainer/TabContainer/World/SlotJsonTextEdit");
	}

	private void PrepareNodes()
	{
		_mainMenuButton.Pressed += _sceneManager.GoToMainMenu;
		_defaultSceneButton.Pressed += _sceneManager.GoToDefaultScene;
		_bagButton.Pressed += _sceneManager.GoToBag;
		_cardCreatorButton.Pressed += _sceneManager.GoToCardCreator;
		_loadFromSlotButton.Pressed += OnLoadFromSlotPressed;
		_saveToActiveSlotButton.Pressed += OnSaveToActiveSlotPressed;
		RefreshWorldTab();
	}

	private void RefreshWorldTab()
	{
		_activeSlotLabel.Text = $"Active Slot: {_saveManager.ActiveSlotIndex}";

		var index = _saveManager.ActiveSlotIndex;
		if (index >= 0)
		{
			var summary = _saveManager.GetSlotSummary(index);
			_slotSummaryLabel.Text = summary.ToString();
			_slotJsonTextEdit.Text = _saveManager.GetRawJson(index) ?? string.Empty;
		}
		else
		{
			_slotSummaryLabel.Text = "Slot Summary: —";
			_slotJsonTextEdit.Text = string.Empty;
		}
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
