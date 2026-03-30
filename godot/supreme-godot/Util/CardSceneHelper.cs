using Godot;
using SupremeEngine;
using System;

public static class CardSceneHelper
{
    public static EquipmentSlotsPrefabScene CreateEquipmentSlotsScene(Node parent, EquipmentSlots slots, string label)
    {
        var prefab = GD.Load<PackedScene>(SceneManager.EquipmentSlotsPrefabScene);
        var scene = prefab.Instantiate<EquipmentSlotsPrefabScene>();
        parent.AddChild(scene);
        scene.Setup(slots, label);
        return scene;
    }

    public static CardCollectionPrefabScene CreateCardCollectionScene(Node parent, CardCollection collection, string label)
    {
        var prefab = GD.Load<PackedScene>(SceneManager.CardCollectionPrefabScene);
        var scene = prefab.Instantiate<CardCollectionPrefabScene>();
        parent.AddChild(scene);
        scene.Setup(collection, label);
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
