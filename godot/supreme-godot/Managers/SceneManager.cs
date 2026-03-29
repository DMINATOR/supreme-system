using Godot;

public partial class SceneManager : Node
{
	public const string MainMenu = "res://Scenes/Menu/MainMenu.tscn";
	public const string SlotSelection = "res://Scenes/Menu/SlotSelection.tscn";
	public const string DebugScene = "res://Scenes/Debug/DebugScene.tscn";
	public const string DefaultScene = "res://Scenes/World/DefaultScene.tscn";
	public const string BagScene = "res://Scenes/Player/BagScene.tscn";
	public const string CardCreatorScene = "res://Scenes/Debug/CardCreatorScene.tscn";
	public const string CardPrefabScene = "res://Scenes/Prefabs/CardPrefabScene.tscn";
	public const string CardOfferPrefabScene = "res://Scenes/Prefabs/CardOfferPrefabScene.tscn";

	public void GoToMainMenu() => GetTree().ChangeSceneToFile(MainMenu);

	public void GoToSlotSelection() => GetTree().ChangeSceneToFile(SlotSelection);

	public void GoToWorld() => GetTree().ChangeSceneToFile(DebugScene);

	public void GoToDefaultScene() => GetTree().ChangeSceneToFile(DefaultScene);

	public void GoToBag() => GetTree().ChangeSceneToFile(BagScene);

	public void GoToCardCreator() => GetTree().ChangeSceneToFile(CardCreatorScene);
}
