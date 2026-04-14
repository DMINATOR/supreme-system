using Godot;
using SupremeEngine;

public static class PrefabFactory
{
    public static CardDragPrefabScene CreateCardDragPreviewScene(Card card)
    {
        var prefab = GD.Load<PackedScene>(SceneManager.CardDragPrefabScene);
        var scene = prefab.Instantiate<CardDragPrefabScene>();
        scene.Setup(card);
        return scene;
    }

    public static CardCollectionPrefabScene CreateBagScene(Node parent)
    {
        var prefab = GD.Load<PackedScene>(SceneManager.CardCollectionPrefabScene);
        var scene = prefab.Instantiate<CardCollectionPrefabScene>();
        scene.Source = CollectionSource.Bag;
        parent.AddChild(scene);
        return scene;
    }

    public static CardCollectionPrefabScene CreateCompanionDeckScene(Node parent, string companionId)
    {
        var prefab = GD.Load<PackedScene>(SceneManager.CardCollectionPrefabScene);
        var scene = prefab.Instantiate<CardCollectionPrefabScene>();
        scene.Source = CollectionSource.CompanionDeck;
        scene.CompanionId = companionId;
        parent.AddChild(scene);
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

    public static CardSlotPrefabScene CreateCardSlotScene(Node parent, int slotIndex, Card? card)
        => CreateCardSlotScene(parent, (slotIndex + 1).ToString(), card);

    public static CardSlotPrefabScene CreateCardSlotScene(Node parent, string slotName, Card? card)
    {
        var prefab = GD.Load<PackedScene>(SceneManager.CardSlotPrefabScene);
        var scene = prefab.Instantiate<CardSlotPrefabScene>();
        parent.AddChild(scene);
        scene.Setup(slotName);
        if (card is not null)
            scene.SetCard(card);
        return scene;
    }

}
