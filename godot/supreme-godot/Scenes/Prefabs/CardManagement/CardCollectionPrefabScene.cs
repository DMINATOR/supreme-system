using Godot;
using SupremeEngine;
using System.Collections.Generic;
using System.Linq;

public enum CollectionSource { Bag, PlayerDeck, CompanionDeck }

public partial class CardCollectionPrefabScene : Control
{
	[Export] public CollectionSource Source { get; set; }
	[Export] public string CompanionId { get; set; } = "";

	private WorldManager _worldManager;
	private Label _titleLabel;
	private HFlowContainer _cardsContainer;
	private readonly List<CardSlotPrefabScene> _slots = new();
	private ICardCollection _collection;

	public override void _Ready()
	{
		LoadNodes();
		PrepareNodes();
	}

	private void LoadNodes()
	{
		_worldManager = GetNode<WorldManager>(AutoloadPath.WorldManager);
		_titleLabel = GetNode<Label>("TitleLabel");
		_cardsContainer = GetNode<HFlowContainer>("ScrollContainer/CardsContainer");
	}

	private void PrepareNodes()
	{
		var inventory = _worldManager.State.Inventory;

		var (collection, label) = Source switch
		{
			CollectionSource.Bag => (inventory.Bag, "Bag"),
			CollectionSource.PlayerDeck => (inventory.Player.Deck, "Deck"),
			CollectionSource.CompanionDeck => (inventory.Companions.First(c => c.CompanionId == CompanionId).Deck, "Deck"),
			_ => (null, string.Empty)
		};

		if (collection is null)
			return;

		_collection = collection;

		_titleLabel.Text = label;

		for (var i = 0; i < collection.Capacity; i++)
		{
			var card = i < collection.Cards.Count ? collection.Cards[i] : null;
			var slot = PrefabFactory.CreateCardSlotScene(_cardsContainer, i, card);
			slot.EnableDragAndDrop();
			_slots.Add(slot);
		}

		foreach (var slot in _slots)
			slot.CardReceived = (src, card) =>
			{
				if (_slots.Contains(src))
					_collection.RemoveCard(card);
				_collection.AddCard(card);
			};
	}
}
