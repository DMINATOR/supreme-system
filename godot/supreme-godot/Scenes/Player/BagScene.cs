using Godot;

public partial class BagScene : Control
{
	private WorldManager _worldManager;
	private CardCollectionPrefabScene _bagCards;

	public override void _Ready()
	{
		LoadNodes();
		PrepareNodes();
	}

	private void LoadNodes()
	{
		_worldManager = GetNode<WorldManager>(AutoloadPath.WorldManager);
		_bagCards = GetNode<CardCollectionPrefabScene>("VBoxContainer/HBoxContainer/BagContainer/BagCards");
	}

	private void PrepareNodes()
	{
		_bagCards.Setup(_worldManager.State.Inventory.Bag, "Bag");
	}
}
