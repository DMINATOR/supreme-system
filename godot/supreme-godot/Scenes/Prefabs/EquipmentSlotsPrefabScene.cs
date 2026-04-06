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
	private VBoxContainer _weaponContainer;
	private VBoxContainer _offHandContainer;
	private VBoxContainer _headContainer;
	private VBoxContainer _chestContainer;
	private VBoxContainer _handsContainer;
	private VBoxContainer _feetContainer;
	private VBoxContainer _amuletContainer;
	private VBoxContainer _ring1Container;
	private VBoxContainer _ring2Container;

	public override void _Ready()
	{
		LoadNodes();
		PrepareNodes();
	}

	private void LoadNodes()
	{
		_worldManager = GetNode<WorldManager>(AutoloadPath.WorldManager);
		_titleLabel = GetNode<Label>("TitleLabel");
		_weaponContainer = GetNode<VBoxContainer>("WeaponSlot/CardContainer");
		_offHandContainer = GetNode<VBoxContainer>("OffHandSlot/CardContainer");
		_headContainer = GetNode<VBoxContainer>("HeadSlot/CardContainer");
		_chestContainer = GetNode<VBoxContainer>("ChestSlot/CardContainer");
		_handsContainer = GetNode<VBoxContainer>("HandsSlot/CardContainer");
		_feetContainer = GetNode<VBoxContainer>("FeetSlot/CardContainer");
		_amuletContainer = GetNode<VBoxContainer>("AmuletSlot/CardContainer");
		_ring1Container = GetNode<VBoxContainer>("Ring1Slot/CardContainer");
		_ring2Container = GetNode<VBoxContainer>("Ring2Slot/CardContainer");
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

		PopulateSlot(_weaponContainer, slots.Weapon);
		PopulateSlot(_offHandContainer, slots.OffHand);
		PopulateSlot(_headContainer, slots.Head);
		PopulateSlot(_chestContainer, slots.Chest);
		PopulateSlot(_handsContainer, slots.Hands);
		PopulateSlot(_feetContainer, slots.Feet);
		PopulateSlot(_amuletContainer, slots.Amulet);
		PopulateSlot(_ring1Container, slots.Ring1);
		PopulateSlot(_ring2Container, slots.Ring2);
	}

	private void PopulateSlot(VBoxContainer container, Card card)
	{
		if (card is not null)
			PrefabFactory.CreateCardScene(container, card);
	}
}
