using Godot;

public partial class InventoryPrefabScene : VBoxContainer
{
	private WorldManager _worldManager;
	private TabContainer _memberTabContainer;

	public override void _Ready()
	{
		LoadNodes();
		PrepareNodes();
	}

	private void LoadNodes()
	{
		_worldManager = GetNode<WorldManager>(AutoloadPath.WorldManager);
		_memberTabContainer = GetNode<TabContainer>("MemberTabContainer");
	}

	private void PrepareNodes()
	{
		foreach (var companion in _worldManager.State.Inventory.Companions)
		{
			var memberContainer = new VBoxContainer
			{
				Name = $"Companion: {companion.CompanionId}",
				SizeFlagsVertical = SizeFlags.ExpandFill
			};
			_memberTabContainer.AddChild(memberContainer);

			var memberTabs = new TabContainer
			{
				Name = "MemberTabs",
				SizeFlagsVertical = SizeFlags.ExpandFill
			};
			memberContainer.AddChild(memberTabs);

			var deckTab = new VBoxContainer { Name = "Deck" };
			memberTabs.AddChild(deckTab);
			PrefabFactory.CreateCompanionDeckScene(deckTab, companion.CompanionId);

			var equipmentTab = new VBoxContainer { Name = "Equipment" };
			memberTabs.AddChild(equipmentTab);
			PrefabFactory.CreateCompanionEquipmentScene(equipmentTab, companion.CompanionId);
		}
	}
}
