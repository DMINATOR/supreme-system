using Godot;
using SupremeEngine;
using System.Linq;

public partial class CompanionEquipmentSlotsPrefabScene : EquipmentSlotsPrefabScene
{
	[Export] public string CompanionId { get; set; } = "";

	protected override EquipmentSlots ResolveSlots(WorldManager worldManager)
	{
		return worldManager.State.Inventory.Companions.First(c => c.CompanionId == CompanionId).Equipment;
	}
}
