using Godot;
using System.Linq;

public partial class CompanionMemberTabPrefabScene : VBoxContainer
{
	[Export] public string CompanionId { get; set; } = "";

	private WorldManager _worldManager;
	private VBoxContainer _deckContainer;
	private VBoxContainer _equipmentContainer;

	public override void _Ready()
	{
		LoadNodes();
		PrepareNodes();
	}

	private void LoadNodes()
	{
		_worldManager = GetNode<WorldManager>(AutoloadPath.WorldManager);
		_deckContainer = GetNode<VBoxContainer>("MemberTabs/Deck");
		_equipmentContainer = GetNode<VBoxContainer>("MemberTabs/Equipment");
	}

	private void PrepareNodes()
	{
		if (string.IsNullOrEmpty(CompanionId))
		{
			GD.PushError("CompanionMemberTabPrefabScene: CompanionId not set.");
			return;
		}

		Name = $"Companion: {CompanionId}";

		var companion = _worldManager.State.Inventory.Companions.FirstOrDefault(c => c.CompanionId == CompanionId);
		if (companion is null)
		{
			GD.PushError($"CompanionMemberTabPrefabScene: Companion '{CompanionId}' not found.");
			return;
		}

		PrefabFactory.CreateCompanionDeckScene(_deckContainer, companion.Deck);
		PrefabFactory.CreateCompanionEquipmentScene(_equipmentContainer, CompanionId);
	}
}
