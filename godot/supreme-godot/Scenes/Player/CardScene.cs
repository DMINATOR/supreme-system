using Godot;
using SupremeEngine;

public partial class CardScene : PanelContainer
{
	private Card _card;
	private Label _nameLabel;
	private Label _rarityLabel;
	private Label _typeLabel;

	public void Setup(Card card)
	{
		_card = card;
	}

	public override void _Ready()
	{
		LoadNodes();
		PrepareNodes();
	}

	private void LoadNodes()
	{
		_nameLabel = GetNode<Label>("VBoxContainer/NameLabel");
		_rarityLabel = GetNode<Label>("VBoxContainer/RarityLabel");
		_typeLabel = GetNode<Label>("VBoxContainer/TypeLabel");
	}

	private void PrepareNodes()
	{
		if (_card == null)
		{
			GD.PushError("CardScene.Setup() must be called before adding to the scene tree.");
			return;
		}

		_nameLabel.Text = _card.Name;
		_rarityLabel.Text = _card.Rarity.ToString();
		_typeLabel.Text = _card.Type.ToString();
	}
}
