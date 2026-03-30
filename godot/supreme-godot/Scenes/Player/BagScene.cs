using Godot;

public partial class BagScene : Control
{
	private SceneManager _sceneManager;
	private WorldManager _worldManager;
	private Button _backButton;
	private TabContainer _tabContainer;
	private VBoxContainer _bagContainer;
	private VBoxContainer _playerContainer;

	public override void _Ready()
	{
		LoadNodes();
		PrepareNodes();
	}

	private void LoadNodes()
	{
		_sceneManager = GetNode<SceneManager>(AutoloadPath.SceneManager);
		_worldManager = GetNode<WorldManager>(AutoloadPath.WorldManager);
		_backButton = GetNode<Button>("VBoxContainer/BackButton");
		_tabContainer = GetNode<TabContainer>("VBoxContainer/TabContainer");
		_bagContainer = GetNode<VBoxContainer>("VBoxContainer/TabContainer/Bag");
		_playerContainer = GetNode<VBoxContainer>("VBoxContainer/TabContainer/Player");
	}

	private void PrepareNodes()
	{
		_backButton.Pressed += _sceneManager.GoToWorld;
		RefreshScene();
	}

	private void RefreshScene()
	{
		ClearContainer(_bagContainer);
		ClearContainer(_playerContainer);
		RemoveCompanionTabs();

		var inventory = _worldManager.State.Inventory;

		CardSceneHelper.CreateCardCollectionScene(_bagContainer, inventory.Bag, "Bag");

		CardSceneHelper.CreateCardCollectionScene(_playerContainer, inventory.Player.Deck, "Deck");
		CardSceneHelper.CreateEquipmentSlotsScene(_playerContainer, inventory.Player.Equipment, "Equipment");

		foreach (var companion in inventory.Companions)
		{
			var container = new VBoxContainer { Name = $"Companion: {companion.CompanionId}" };
			_tabContainer.AddChild(container);
			CardSceneHelper.CreateCardCollectionScene(container, companion.Deck, "Deck");
			CardSceneHelper.CreateEquipmentSlotsScene(container, companion.Equipment, "Equipment");
		}
	}

	private static void ClearContainer(VBoxContainer container)
	{
		foreach (Node child in container.GetChildren())
			child.QueueFree();
	}

	private void RemoveCompanionTabs()
	{
		foreach (Node child in _tabContainer.GetChildren())
		{
			if (child != _bagContainer && child != _playerContainer)
				child.QueueFree();
		}
	}
}
