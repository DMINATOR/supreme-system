using Godot;
using SupremeEngine;

public partial class CardCollectionPrefabScene : Control
{
	private Label _titleLabel;
	private HFlowContainer _cardsContainer;

	public override void _Ready()
	{
		LoadNodes();
	}

	private void LoadNodes()
	{
		_titleLabel = GetNode<Label>("TitleLabel");
		_cardsContainer = GetNode<HFlowContainer>("ScrollContainer/CardsContainer");
	}

	public void Setup(CardCollection collection, string label)
	{
		_titleLabel.Text = label;

		foreach (Node child in _cardsContainer.GetChildren())
			child.QueueFree();

		if (collection.Cards.Count == 0)
		{
			_cardsContainer.AddChild(new Label { Text = "Empty." });
			return;
		}

		foreach (var card in collection.Cards)
			CardSceneHelper.CreateCardScene(_cardsContainer, card);
	}
}
