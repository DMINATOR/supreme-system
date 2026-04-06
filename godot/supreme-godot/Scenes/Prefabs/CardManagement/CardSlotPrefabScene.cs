using Godot;
using SupremeEngine;

public partial class CardSlotPrefabScene : PanelContainer
{
	private Label _indexLabel;
	private Control _cardContainer;
	private Label _emptyLabel;

	public void Setup(string label, Card card)
	{
		_indexLabel.Text = label;

		if (card is not null)
		{
			_emptyLabel.Hide();
			PrefabFactory.CreateCardScene(_cardContainer, card);
		}
	}

	public override void _Ready()
	{
		LoadNodes();
	}

	private void LoadNodes()
	{
		_indexLabel = GetNode<Label>("VBoxContainer/IndexLabel");
		_cardContainer = GetNode<Control>("VBoxContainer/CardContainer");
		_emptyLabel = GetNode<Label>("VBoxContainer/EmptyLabel");
	}
}
