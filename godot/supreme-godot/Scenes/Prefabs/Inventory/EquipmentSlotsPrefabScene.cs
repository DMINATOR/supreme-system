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

		_headSlot.Setup("Head");
		_weaponSlot.Setup("Weapon");
		_offHandSlot.Setup("Off-Hand");
		_chestSlot.Setup("Chest");
		_handsSlot.Setup("Hands");
		_feetSlot.Setup("Feet");
		_amuletSlot.Setup("Amulet");
		_ring1Slot.Setup("Ring 1");
		_ring2Slot.Setup("Ring 2");

		if (_equipmentSlots.Head.Card is not null) _headSlot.SetCard(_equipmentSlots.Head.Card);
		if (_equipmentSlots.Weapon.Card is not null) _weaponSlot.SetCard(_equipmentSlots.Weapon.Card);
		if (_equipmentSlots.OffHand.Card is not null) _offHandSlot.SetCard(_equipmentSlots.OffHand.Card);
		if (_equipmentSlots.Chest.Card is not null) _chestSlot.SetCard(_equipmentSlots.Chest.Card);
		if (_equipmentSlots.Hands.Card is not null) _handsSlot.SetCard(_equipmentSlots.Hands.Card);
		if (_equipmentSlots.Feet.Card is not null) _feetSlot.SetCard(_equipmentSlots.Feet.Card);
		if (_equipmentSlots.Amulet.Card is not null) _amuletSlot.SetCard(_equipmentSlots.Amulet.Card);
		if (_equipmentSlots.Ring1.Card is not null) _ring1Slot.SetCard(_equipmentSlots.Ring1.Card);
		if (_equipmentSlots.Ring2.Card is not null) _ring2Slot.SetCard(_equipmentSlots.Ring2.Card);

		_headSlot.CardSlot    = _equipmentSlots.Head;
		_weaponSlot.CardSlot  = _equipmentSlots.Weapon;
		_offHandSlot.CardSlot = _equipmentSlots.OffHand;
		_chestSlot.CardSlot   = _equipmentSlots.Chest;
		_handsSlot.CardSlot   = _equipmentSlots.Hands;
		_feetSlot.CardSlot    = _equipmentSlots.Feet;
		_amuletSlot.CardSlot  = _equipmentSlots.Amulet;
		_ring1Slot.CardSlot   = _equipmentSlots.Ring1;
		_ring2Slot.CardSlot   = _equipmentSlots.Ring2;

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
