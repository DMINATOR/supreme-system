using Godot;

public partial class SceneManager : Node
{
    private static readonly string MainMenu = "res://Scenes/Menu/MainMenu.tscn";
    private static readonly string SlotSelection = "res://Scenes/Menu/SlotSelection.tscn";
    private static readonly string World = "res://Scenes/World/DefaultScene.tscn";

    public void GoToMainMenu() => GetTree().ChangeSceneToFile(MainMenu);

    public void GoToSlotSelection() => GetTree().ChangeSceneToFile(SlotSelection);

    public void GoToWorld() => GetTree().ChangeSceneToFile(World);
}
