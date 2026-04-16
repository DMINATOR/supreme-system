using Godot;
using SupremeEngine;
using System;

public partial class CatalogueCollectionPrefabScene : Control
{
	[Export] public CardTemplateLibrary Library { get; set; }

	public event Action<Card> CardSelected;

	private CardCollectionPrefabScene _collection;

	public override void _Ready()
	{
		LoadNodes();
		PrepareNodes();
	}

	private void LoadNodes()
	{
		_collection = GetNode<CardCollectionPrefabScene>("Collection");
	}

	private void PrepareNodes()
	{
		if (Library is null)
		{
			GD.PushError("CatalogueCollectionPrefabScene: Library export is not assigned.");
			return;
		}

		var factory = new CardFactory(new Random());
		var cards = new CardCollection(Library.Templates.Length);
		for (var i = 0; i < Library.Templates.Length; i++)
			cards.Slots[i].Equip(factory.Create(Library.Templates[i].ToCardTemplate()));

		_collection.Setup(cards, "All Cards", enableDragAndDrop: false);
		_collection.CardSelected += c => CardSelected?.Invoke(c);
	}
}

