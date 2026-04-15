using System;
using Godot;
using SupremeEngine;

public static class PrefabFactory
{
    private const string CardTemplateLibraryPath = "res://Data/Cards/CardTemplateLibrary.tres";

    public static CardDragPrefabScene CreateCardDragPreviewScene(Card card)
    {
        var prefab = GD.Load<PackedScene>(SceneManager.CardDragPrefabScene);
        var scene = prefab.Instantiate<CardDragPrefabScene>();
        scene.Setup(card);
        return scene;
    }

    public static CardCollectionPrefabScene CreateBagScene(Node parent, ICardCollection bag)
    {
        var prefab = GD.Load<PackedScene>(SceneManager.CardCollectionPrefabScene);
        var scene = prefab.Instantiate<CardCollectionPrefabScene>();
        parent.AddChild(scene);
        scene.Setup(bag, "Bag");
        return scene;
    }

    public static CardCollectionPrefabScene CreateCatalogueScene(Node parent)
    {
        var library = GD.Load<CardTemplateLibrary>(CardTemplateLibraryPath);
        var prefab = GD.Load<PackedScene>(SceneManager.CardCollectionPrefabScene);
        var scene = prefab.Instantiate<CardCollectionPrefabScene>();
        parent.AddChild(scene);

        if (library is null)
        {
            GD.PushError("PrefabFactory.CreateCatalogueScene: Failed to load CardTemplateLibrary.");
            return scene;
        }

        var factory = new CardFactory(new Random());
        var collection = new CardCollection(library.Templates.Length);
        for (var i = 0; i < library.Templates.Length; i++)
            collection.Slots[i].Equip(factory.Create(library.Templates[i].ToCardTemplate()));

        scene.Setup(collection, "All Cards", enableDragAndDrop: false);
        return scene;
    }

    public static CardCollectionPrefabScene CreateCompanionDeckScene(Node parent, ICardCollection deck)
    {
        var prefab = GD.Load<PackedScene>(SceneManager.CardCollectionPrefabScene);
        var scene = prefab.Instantiate<CardCollectionPrefabScene>();
        parent.AddChild(scene);
        scene.Setup(deck, "Deck");
        return scene;
    }

    public static CompanionEquipmentSlotsPrefabScene CreateCompanionEquipmentScene(Node parent, string companionId)
    {
        var prefab = GD.Load<PackedScene>(SceneManager.CompanionEquipmentSlotsPrefabScene);
        var scene = prefab.Instantiate<CompanionEquipmentSlotsPrefabScene>();
        scene.CompanionId = companionId;
        parent.AddChild(scene);
        return scene;
    }

    public static CardSlotPrefabScene CreateCardSlotScene(Node parent, int slotIndex, CardSlot cardSlot)
        => CreateCardSlotScene(parent, (slotIndex + 1).ToString(), cardSlot);

    public static CardSlotPrefabScene CreateCardSlotScene(Node parent, string slotName, CardSlot cardSlot)
    {
        var prefab = GD.Load<PackedScene>(SceneManager.CardSlotPrefabScene);
        var scene = prefab.Instantiate<CardSlotPrefabScene>();
        parent.AddChild(scene);
        scene.Setup(cardSlot, slotName);
        return scene;
    }

}
