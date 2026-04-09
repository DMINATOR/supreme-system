using Godot;
using SupremeEngine;
using System.Linq;

public enum EquipmentSource { Player, Companion }

public partial class EquipmentSlotsPrefabScene : Control
{
	[Export] public EquipmentSource Source { get; set; }
	[Export] public string CompanionId { get; set; } = "";

	private WorldManager _worldManager;
	private Label _titleLabel;
	private CardSlotPrefabScene _headSlot;
	private CardSlotPrefabScene _weaponSlot;
	private CardSlotPrefabScene _offHandSlot;
	private CardSlotPrefabScene _chestSlot;
	private CardSlotPrefabScene _handsSlot;
	private CardSlotPrefabScene _feetSlot;
	private CardSlotPrefabScene _amuletSlot;
	private CardSlotPrefabScene _ring1Slot;
	private CardSlotPrefabScene _ring2Slot;

	public override void _Ready()
	{
		LoadNodes();
		PrepareNodes();
	}

	private void LoadNodes()
	{
		_worldManager = GetNode<WorldManager>(AutoloadPath.WorldManager);
		_titleLabel = GetNode<Label>("TitleLabel");
		_weaponSlot = GetNode<CardSlotPrefabScene>("CenterContainer/SlotsContainer/Row1/WeaponSlot");
		_headSlot = GetNode<CardSlotPrefabScene>("CenterContainer/SlotsContainer/Row1/HeadSlot");
		_offHandSlot = GetNode<CardSlotPrefabScene>("CenterContainer/SlotsContainer/Row1/OffHandSlot");
		_handsSlot = GetNode<CardSlotPrefabScene>("CenterContainer/SlotsContainer/Row2/HandsSlot");
		_chestSlot = GetNode<CardSlotPrefabScene>("CenterContainer/SlotsContainer/Row2/ChestSlot");
		_amuletSlot = GetNode<CardSlotPrefabScene>("CenterContainer/SlotsContainer/Row2/AmuletSlot");
		_feetSlot = GetNode<CardSlotPrefabScene>("CenterContainer/SlotsContainer/Row3/FeetSlot");
		_ring1Slot = GetNode<CardSlotPrefabScene>("CenterContainer/SlotsContainer/Row3/Ring1Slot");
		_ring2Slot = GetNode<CardSlotPrefabScene>("CenterContainer/SlotsContainer/Row3/Ring2Slot");
	}

	private void PrepareNodes()
	{
		var inventory = _worldManager.State.Inventory;

		var slots = Source switch
		{
			EquipmentSource.Player => inventory.Player.Equipment,
			EquipmentSource.Companion => inventory.Companions.First(c => c.CompanionId == CompanionId).Equipment,
			_ => null
		};

		if (slots is null)
			return;

		_titleLabel.Text = "Equipment";

		_headSlot.Setup("Head", slots.Head);
		_weaponSlot.Setup("Weapon", slots.Weapon);
		_offHandSlot.Setup("Off-Hand", slots.OffHand);
		_chestSlot.Setup("Chest", slots.Chest);
		_handsSlot.Setup("Hands", slots.Hands);
		_feetSlot.Setup("Feet", slots.Feet);
		_amuletSlot.Setup("Amulet", slots.Amulet);
		_ring1Slot.Setup("Ring 1", slots.Ring1);
		_ring2Slot.Setup("Ring 2", slots.Ring2);
	}
}
