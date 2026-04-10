using Godot;
using SupremeEngine;
using System.Collections.Generic;
using System.Linq;

public enum EquipmentSource { Player, Companion }

public partial class EquipmentSlotsPrefabScene : Control
{
	[Export] public EquipmentSource Source { get; set; }
	[Export] public string CompanionId { get; set; } = "";

	private WorldManager _worldManager;
	private CommandDispatcher _commandDispatcher;
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
	private Dictionary<CardSlotPrefabScene, CardSlot> _slotMap;

	public override void _Ready()
	{
		LoadNodes();
		PrepareNodes();
	}

	private void LoadNodes()
	{
		_worldManager = GetNode<WorldManager>(AutoloadPath.WorldManager);
		_commandDispatcher = GetNode<CommandDispatcher>(AutoloadPath.CommandDispatcher);
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

		_equipmentSlots = Source switch
		{
			EquipmentSource.Player => inventory.Player.Equipment,
			EquipmentSource.Companion => inventory.Companions.First(c => c.CompanionId == CompanionId).Equipment,
			_ => null
		};

		if (_equipmentSlots is null)
			return;

		_titleLabel.Text = "Equipment";

		_headSlot.Setup("Head", _equipmentSlots.Head.Card);
		_weaponSlot.Setup("Weapon", _equipmentSlots.Weapon.Card);
		_offHandSlot.Setup("Off-Hand", _equipmentSlots.OffHand.Card);
		_chestSlot.Setup("Chest", _equipmentSlots.Chest.Card);
		_handsSlot.Setup("Hands", _equipmentSlots.Hands.Card);
		_feetSlot.Setup("Feet", _equipmentSlots.Feet.Card);
		_amuletSlot.Setup("Amulet", _equipmentSlots.Amulet.Card);
		_ring1Slot.Setup("Ring 1", _equipmentSlots.Ring1.Card);
		_ring2Slot.Setup("Ring 2", _equipmentSlots.Ring2.Card);

		_slotMap = new Dictionary<CardSlotPrefabScene, CardSlot>
		{
			{ _headSlot,    _equipmentSlots.Head },
			{ _weaponSlot,  _equipmentSlots.Weapon },
			{ _offHandSlot, _equipmentSlots.OffHand },
			{ _chestSlot,   _equipmentSlots.Chest },
			{ _handsSlot,   _equipmentSlots.Hands },
			{ _feetSlot,    _equipmentSlots.Feet },
			{ _amuletSlot,  _equipmentSlots.Amulet },
			{ _ring1Slot,   _equipmentSlots.Ring1 },
			{ _ring2Slot,   _equipmentSlots.Ring2 },
		};

		foreach (var (uiSlot, engineSlot) in _slotMap)
		{
			var capturedEngineSlot = engineSlot;
			uiSlot.EnableDragAndDrop();
			uiSlot.CardReceived = (src, card) =>
			{
				_slotMap.TryGetValue(src, out var sourceEngineSlot);
				_commandDispatcher.Dispatch(new TransferCardCommand(sourceEngineSlot, capturedEngineSlot, card));
			};
		}
	}
}
