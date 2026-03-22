using Godot;
using SupremeEngine;
using System;

public static class CardOfferSceneHelper
{
    private const string CardOfferScenePath = "res://Scenes/Player/CardOfferScene.tscn";

    public static CardOfferScene CreateCardOfferScene(Card card, Action<Card> onAccepted, Action onDeclined)
    {
        var prefab = GD.Load<PackedScene>(CardOfferScenePath);
        var offerScene = prefab.Instantiate<CardOfferScene>();
        offerScene.Setup(card);
        offerScene.Accepted += onAccepted;
        offerScene.Declined += onDeclined;
        return offerScene;
    }
}
