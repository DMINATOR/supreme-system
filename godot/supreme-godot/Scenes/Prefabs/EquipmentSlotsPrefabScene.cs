using Godot;
using SupremeEngine;

public partial class EquipmentSlotsPrefabScene : Control
{
	private Label _titleLabel;
	private VBoxContainer _slotsContainer;

	public override void _Ready()
	{
		LoadNodes();
	}

	private void LoadNodes()
	{
		_titleLabel = GetNode<Label>("TitleLabel");
		_slotsContainer = GetNode<VBoxContainer>("SlotsContainer");
	}

	public void Setup(EquipmentSlots slots, string label)
	{
		_titleLabel.Text = label;

		foreach (Node child in _slotsContainer.GetChildren())
			child.QueueFree();

		AddSlotLabel("Weapon", slots.Weapon);
		AddSlotLabel("Off-Hand", slots.OffHand);
		AddSlotLabel("Head", slots.Head);
		AddSlotLabel("Chest", slots.Chest);
		AddSlotLabel("Hands", slots.Hands);
		AddSlotLabel("Feet", slots.Feet);
		AddSlotLabel("Amulet", slots.Amulet);
		AddSlotLabel("Ring 1", slots.Ring1);
		AddSlotLabel("Ring 2", slots.Ring2);
	}

	private void AddSlotLabel(string slotName, Card card)
	{
		var text = card is not null ? $"{slotName}: {card.Name}" : $"{slotName}: —";
		_slotsContainer.AddChild(new Label { Text = text });
	}
}
