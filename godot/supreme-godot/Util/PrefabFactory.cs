using Godot;
using SupremeEngine;
using System;

public static class PrefabFactory
{
    public static CardCollectionPrefabScene CreateCompanionDeckScene(Node parent, string companionId)
    {
        var prefab = GD.Load<PackedScene>(SceneManager.CardCollectionPrefabScene);
        var scene = prefab.Instantiate<CardCollectionPrefabScene>();
        scene.Source = CollectionSource.CompanionDeck;
        scene.CompanionId = companionId;
        parent.AddChild(scene);
        return scene;
    }

    public static EquipmentSlotsPrefabScene CreateCompanionEquipmentScene(Node parent, string companionId)
    {
        var prefab = GD.Load<PackedScene>(SceneManager.EquipmentSlotsPrefabScene);
        var scene = prefab.Instantiate<EquipmentSlotsPrefabScene>();
        scene.Source = EquipmentSource.Companion;
        scene.CompanionId = companionId;
        parent.AddChild(scene);
        return scene;
    }

    public static CardPrefabScene CreateCardScene(Node parent, Card card)
    {
        var prefab = GD.Load<PackedScene>(SceneManager.CardPrefabScene);
        var cardScene = prefab.Instantiate<CardPrefabScene>();
        parent.AddChild(cardScene);
        cardScene.Setup(card);
        return cardScene;
    }

    public static CardSlotPrefabScene CreateCardSlotScene(Node parent, int slotIndex, Card card)
    {
        var prefab = GD.Load<PackedScene>(SceneManager.CardSlotPrefabScene);
        var scene = prefab.Instantiate<CardSlotPrefabScene>();
        parent.AddChild(scene);
        scene.Setup(slotIndex, card);
        return scene;
    }

    public static CardOfferPrefabScene CreateCardOfferScene(Card card, Action<Card> onAccepted, Action onDeclined)
    {
        var prefab = GD.Load<PackedScene>(SceneManager.CardOfferPrefabScene);
        var offerScene = prefab.Instantiate<CardOfferPrefabScene>();
        offerScene.Setup(card);
        offerScene.Accepted += onAccepted;
        offerScene.Declined += onDeclined;
        return offerScene;
    }
}
