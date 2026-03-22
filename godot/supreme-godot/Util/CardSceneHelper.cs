using Godot;
using SupremeEngine;

public static class CardSceneHelper
{
    private const string CardScenePath = "res://Scenes/Player/CardScene.tscn";

    public static CardScene CreateCardScene(Card card)
    {
        var prefab = GD.Load<PackedScene>(CardScenePath);
        var cardScene = prefab.Instantiate<CardScene>();
        cardScene.Setup(card);
        return cardScene;
    }
}
