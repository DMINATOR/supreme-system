using Godot;
using SupremeEngine;
using System.Linq;

public enum CollectionSource { Bag, PlayerDeck, CompanionDeck }

public partial class CardCollectionPrefabScene : Control
{
	[Export] public CollectionSource Source { get; set; }
	[Export] public string CompanionId { get; set; } = "";

	private WorldManager _worldManager;
	private Label _titleLabel;
	private HFlowContainer _cardsContainer;

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

		_titleLabel.Text = label;

		if (collection.Cards.Count == 0)
		{
			_cardsContainer.AddChild(new Label { Text = "Empty." });
			return;
		}

		foreach (var card in collection.Cards)
			CardSceneHelper.CreateCardScene(_cardsContainer, card);
	}
}
