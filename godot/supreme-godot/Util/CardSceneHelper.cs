using Godot;
using SupremeEngine;
using System;

public static class CardSceneHelper
{
    public static CardScene CreateCardScene(Node parent, Card card)
    {
        var prefab = GD.Load<PackedScene>(SceneManager.CardScene);
        var cardScene = prefab.Instantiate<CardScene>();
        parent.AddChild(cardScene);
        cardScene.Setup(card);
        return cardScene;
    }

    public static CardOfferScene CreateCardOfferScene(Card card, Action<Card> onAccepted, Action onDeclined)
    {
        var prefab = GD.Load<PackedScene>(SceneManager.CardOfferScene);
        var offerScene = prefab.Instantiate<CardOfferScene>();
        offerScene.Setup(card);
        offerScene.Accepted += onAccepted;
        offerScene.Declined += onDeclined;
        return offerScene;
    }
}
