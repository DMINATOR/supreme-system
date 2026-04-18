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

    public static CompanionMemberTabPrefabScene CreateCompanionMemberTabScene(Node parent, string companionId)
    {
        var prefab = GD.Load<PackedScene>(SceneManager.CompanionMemberTabPrefabScene);
        var scene = prefab.Instantiate<CompanionMemberTabPrefabScene>();
        scene.CompanionId = companionId;
        parent.AddChild(scene);
        return scene;
    }

    public static CardTemplateRowPrefabScene CreateCardTemplateRowScene(Node parent, string displayText)
    {
        var prefab = GD.Load<PackedScene>(SceneManager.CardTemplateRowPrefabScene);
        var scene = prefab.Instantiate<CardTemplateRowPrefabScene>();
        parent.AddChild(scene);
        scene.Setup(displayText);
        return scene;
    }

    public static CardTemplateRowPrefabScene CreateCardTemplateRowScene(Node parent, string displayText, Action onCreate)
    {
        var scene = CreateCardTemplateRowScene(parent, displayText);
        scene.CreatePressed += onCreate;
        return scene;
    }

    public static SaveSlotRowPrefabScene CreateSaveSlotRowScene(Node parent, SlotSummary summary)
    {
        var prefab = GD.Load<PackedScene>(SceneManager.SaveSlotRowPrefabScene);
        var scene = prefab.Instantiate<SaveSlotRowPrefabScene>();
        parent.AddChild(scene);
        scene.Setup(summary);
        return scene;
    }

    public static SaveSlotRowPrefabScene CreateSaveSlotRowScene(Node parent, SlotSummary summary, Action onNew, Action onLoad, Action onDelete)
    {
        var scene = CreateSaveSlotRowScene(parent, summary);
        scene.NewPressed += onNew;
        scene.LoadPressed += onLoad;
        scene.DeletePressed += onDelete;
        return scene;
    }

    public static void CreateCardSlotScenes(Node parent, ICardCollection collection, bool enableDragAndDrop, Action<Card> onCardPressed)
    {
        var prefab = GD.Load<PackedScene>(SceneManager.CardSlotPrefabScene);
        for (var i = 0; i < collection.Capacity; i++)
        {
            var slot = prefab.Instantiate<CardSlotPrefabScene>();
            parent.AddChild(slot);
            slot.Setup(collection.Slots[i], (i + 1).ToString());
            if (enableDragAndDrop)
                slot.EnableDragAndDrop();
            slot.CardPressed += onCardPressed;
        }
    }

}
