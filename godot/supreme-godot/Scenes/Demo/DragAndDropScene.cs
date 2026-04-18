using Godot;
using SupremeEngine;

public partial class DragAndDropScene : Control
{
	private CardSlotPrefabScene _slot1;
	private CardSlotPrefabScene _slot2;
	private CardSlotPrefabScene _slot3;
	private CardSlotPrefabScene _slot4;

	public override void _Ready()
	{
		LoadNodes();
		PrepareNodes();
	}

	private void LoadNodes()
	{
		_slot1 = GetNode<CardSlotPrefabScene>("VBoxContainer/SlotsContainer/Slot1");
		_slot2 = GetNode<CardSlotPrefabScene>("VBoxContainer/SlotsContainer/Slot2");
		_slot3 = GetNode<CardSlotPrefabScene>("VBoxContainer/SlotsContainer/Slot3");
		_slot4 = GetNode<CardSlotPrefabScene>("VBoxContainer/SlotsContainer/Slot4");
	}

	private void PrepareNodes()
	{
		var card1 = new Card("demo-001", "Iron Sword", CardRarity.Common, CardType.Equipment, 100f, level: 1);
		var card2 = new Card("demo-002", "Fire Spell", CardRarity.Rare, CardType.Spell, 75f, level: 1);

		var cardSlot1 = new CardSlot(); cardSlot1.Equip(card1);
		var cardSlot2 = new CardSlot(); cardSlot2.Equip(card2);

		_slot1.Setup(cardSlot1, "Slot 1");
		_slot2.Setup(cardSlot2, "Slot 2");
		_slot3.Setup(new CardSlot(), "Slot 3");
		_slot4.Setup(new CardSlot(), "Slot 4");

		foreach (var slot in GetAllSlots())
			slot.EnableDragAndDrop();
	}

	private CardSlotPrefabScene[] GetAllSlots() => new[] { _slot1, _slot2, _slot3, _slot4 };
}

