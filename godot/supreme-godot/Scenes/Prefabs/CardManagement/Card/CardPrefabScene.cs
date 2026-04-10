using Godot;
using SupremeEngine;

public partial class CardPrefabScene : PanelContainer
{
	private CardPrefabHeaderScene _headerImage;
	private Label _nameLabel;
	private Label _rarityLabel;
	private Label _typeLabel;
	private Label _durabilityLabel;

	public void Setup(Card card)
	{
		_headerImage.Setup(card.Rarity);
		_nameLabel.Text = card.Name;
		_rarityLabel.Text = card.Rarity.ToString();
		_typeLabel.Text = card.Type.ToString();
		_durabilityLabel.Text = $"Durability: {card.Durability}";
	}

	public override void _Ready()
	{
		LoadNodes();
	}

	private void LoadNodes()
	{
		_headerImage = GetNode<CardPrefabHeaderScene>("VBoxContainer/HeaderImage");
		_nameLabel = GetNode<Label>("VBoxContainer/NameLabel");
		_rarityLabel = GetNode<Label>("VBoxContainer/RarityLabel");
		_typeLabel = GetNode<Label>("VBoxContainer/TypeLabel");
		_durabilityLabel = GetNode<Label>("VBoxContainer/DurabilityLabel");
	}
}
