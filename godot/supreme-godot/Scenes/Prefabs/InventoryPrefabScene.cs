using Godot;

public partial class InventoryPrefabScene : VBoxContainer
{
	private WorldManager _worldManager;
	private TabContainer _memberTabContainer;
	private VBoxContainer _playerContainer;

	public override void _Ready()
	{
		LoadNodes();
		PrepareNodes();
	}

	private void LoadNodes()
	{
		_worldManager = GetNode<WorldManager>(AutoloadPath.WorldManager);
		_memberTabContainer = GetNode<TabContainer>("MemberTabContainer");
		_playerContainer = GetNode<VBoxContainer>("MemberTabContainer/Player");
	}

	private void PrepareNodes()
	{
		foreach (var companion in _worldManager.State.Inventory.Companions)
		{
			var container = new VBoxContainer { Name = $"Companion: {companion.CompanionId}" };
			_memberTabContainer.AddChild(container);
			PrefabFactory.CreateCompanionDeckScene(container, companion.CompanionId);
			PrefabFactory.CreateCompanionEquipmentScene(container, companion.CompanionId);
		}
	}
}
