using Godot;
using SupremeEngine;

public abstract partial class EquipmentSlotsPrefabScene : Control
{
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

	private EquipmentSlots _equipmentSlots;

	public override void _Ready()
	{
		LoadNodes();
		PrepareNodes();
	}

	protected abstract EquipmentSlots ResolveSlots(WorldManager worldManager);

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
		_equipmentSlots = ResolveSlots(_worldManager);

		if (_equipmentSlots is null)
		{
			GD.PushError($"{GetType().Name}: ResolveSlots returned null.");
			return;
		}

		_titleLabel.Text = "Equipment";

		_headSlot.Setup(_equipmentSlots.Head, "Head");
		_weaponSlot.Setup(_equipmentSlots.Weapon, "Weapon");
		_offHandSlot.Setup(_equipmentSlots.OffHand, "Off-Hand");
		_chestSlot.Setup(_equipmentSlots.Chest, "Chest");
		_handsSlot.Setup(_equipmentSlots.Hands, "Hands");
		_feetSlot.Setup(_equipmentSlots.Feet, "Feet");
		_amuletSlot.Setup(_equipmentSlots.Amulet, "Amulet");
		_ring1Slot.Setup(_equipmentSlots.Ring1, "Ring 1");
		_ring2Slot.Setup(_equipmentSlots.Ring2, "Ring 2");

		foreach (var slot in GetAllSlots())
			slot.EnableDragAndDrop();
	}

	private CardSlotPrefabScene[] GetAllSlots()
	{
		return new[]
		{
			_headSlot, _weaponSlot, _offHandSlot, _chestSlot,
			_handsSlot, _feetSlot, _amuletSlot, _ring1Slot, _ring2Slot
		};
	}
}
