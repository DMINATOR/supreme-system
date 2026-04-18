using Godot;
using SupremeEngine;
using System;

public partial class CardCollectionPrefabScene : Control
{
	public event Action<Card> CardSelected;

	private Label _titleLabel;
	private HFlowContainer _cardsContainer;

	public override void _Ready()
	{
		LoadNodes();
	}

	public void Setup(ICardCollection collection, string title, bool enableDragAndDrop = true)
	{
		_titleLabel.Text = title;
		PrefabFactory.CreateCardSlotScenes(_cardsContainer, collection, enableDragAndDrop, c => CardSelected?.Invoke(c));
	}

	private void LoadNodes()
	{
		_titleLabel = GetNode<Label>("TitleLabel");
		_cardsContainer = GetNode<HFlowContainer>("ScrollContainer/CardsContainer");
	}
}


