using Godot;
using SupremeEngine;

public partial class CardCatalogueScene : Control
{
	private SceneManager _sceneManager;
	private Control _catalogueContainer;
	private Label _nameLabel;
	private Label _rarityLabel;
	private Label _typeLabel;
	private Label _durabilityLabel;

	public override void _Ready()
	{
		LoadNodes();
		PrepareNodes();
	}

	private void LoadNodes()
	{
		_sceneManager = GetNode<SceneManager>(AutoloadPath.SceneManager);
		_catalogueContainer = GetNode<Control>("VBoxContainer/ContentContainer/CatalogueContainer");
		_nameLabel = GetNode<Label>("VBoxContainer/ContentContainer/DetailPanel/NameLabel");
		_rarityLabel = GetNode<Label>("VBoxContainer/ContentContainer/DetailPanel/RarityLabel");
		_typeLabel = GetNode<Label>("VBoxContainer/ContentContainer/DetailPanel/TypeLabel");
		_durabilityLabel = GetNode<Label>("VBoxContainer/ContentContainer/DetailPanel/DurabilityLabel");
	}

	private void PrepareNodes()
	{
		var catalogue = PrefabFactory.CreateCatalogueScene(_catalogueContainer);
		catalogue.CardSelected += ShowDetails;
	}

	private void ShowDetails(Card card)
	{
		_nameLabel.Text = card.Name;
		_rarityLabel.Text = $"Rarity: {card.Rarity}";
		_typeLabel.Text = $"Type: {card.Type}";
		_durabilityLabel.Text = $"Durability on Use: {card.Durability}";
	}
}

