using SupremeEngine;

public partial class PlayerEquipmentSlotsPrefabScene : EquipmentSlotsPrefabScene
{
	protected override EquipmentSlots ResolveSlots(WorldManager worldManager)
	{
		return worldManager.State.Player.Equipment;
	}
}
