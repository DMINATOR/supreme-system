using Godot;
using SupremeEngine;

public partial class CardPrefabScene : PanelContainer
{
	private Label _nameLabel;
	private Label _rarityLabel;
	private Label _typeLabel;

	public void Setup(Card card)
	{
		_nameLabel.Text = card.Name;
		_rarityLabel.Text = card.Rarity.ToString();
		_typeLabel.Text = card.Type.ToString();
	}

	public override void _Ready()
	{
		LoadNodes();
	}

	private void LoadNodes()
	{
		_nameLabel = GetNode<Label>("VBoxContainer/NameLabel");
		_rarityLabel = GetNode<Label>("VBoxContainer/RarityLabel");
		_typeLabel = GetNode<Label>("VBoxContainer/TypeLabel");
	}
}
