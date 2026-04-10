using Godot;
using System.Reflection;

public partial class SceneManager : Node
{
	public enum GameScene
	{
		[ScenePath("res://Scenes/Menu/MainMenu.tscn")]
		MainMenu,
		[ScenePath("res://Scenes/Menu/SlotSelection.tscn")]
		SlotSelection,
		[ScenePath("res://Scenes/Debug/DebugScene.tscn")]
		DebugScene,
		[ScenePath("res://Scenes/World/DefaultScene.tscn")]
		DefaultScene,
		[ScenePath("res://Scenes/Player/BagScene.tscn")]
		BagScene,
		[ScenePath("res://Scenes/Debug/CardCreatorScene.tscn")]
		CardCreatorScene,
		[ScenePath("res://Scenes/Demo/DragAndDropScene.tscn")]
		DragAndDropScene,
	}

	public const string CardCollectionPrefabScene = "res://Scenes/Prefabs/CardManagement/Collection/CardCollectionPrefabScene.tscn";
	public const string CardOfferPrefabScene = "res://Scenes/Prefabs/CardManagement/Collection/CardOfferPrefabScene.tscn";
	public const string CardPrefabScene = "res://Scenes/Prefabs/CardManagement/Card/CardPrefabScene.tscn";
	public const string CardDragPrefabScene = "res://Scenes/Prefabs/CardManagement/Card/CardDragPrefabScene.tscn";
	public const string CardSlotPrefabScene = "res://Scenes/Prefabs/CardManagement/Slot/CardSlotPrefabScene.tscn";
	public const string CompanionEquipmentSlotsPrefabScene = "res://Scenes/Prefabs/Inventory/CompanionEquipmentSlotsPrefabScene.tscn";
	public const string InventoryPrefabScene = "res://Scenes/Prefabs/Inventory/InventoryPrefabScene.tscn";
	public const string PlayerEquipmentSlotsPrefabScene = "res://Scenes/Prefabs/Inventory/PlayerEquipmentSlotsPrefabScene.tscn";
	public const string SceneButtonPrefabScene = "res://Scenes/Prefabs/Control/SceneButtonPrefabScene.tscn";

	public void GoToMainMenu() => GoTo(GameScene.MainMenu);

	public void GoToSlotSelection() => GoTo(GameScene.SlotSelection);

	public void GoToWorld() => GoTo(GameScene.DebugScene);

	public void GoToDefaultScene() => GoTo(GameScene.DefaultScene);

	public void GoToBag() => GoTo(GameScene.BagScene);

	public void GoToCardCreator() => GoTo(GameScene.CardCreatorScene);

	public void GoToDragAndDropScene() => GoTo(GameScene.DragAndDropScene);

	public void GoTo(GameScene scene)
	{
		var path = typeof(GameScene)
			.GetField(scene.ToString())
			?.GetCustomAttribute<ScenePathAttribute>()
			?.Path;

		if (path == null)
		{
			GD.PushError($"SceneManager.GoTo: no ScenePath attribute for '{scene}'.");
			return;
		}

		GetTree().ChangeSceneToFile(path);
	}
}
