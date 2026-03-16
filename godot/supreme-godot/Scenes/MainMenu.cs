using Godot;

public partial class MainMenu : Control
{
    public override void _Ready()
    {
        GetNode<Button>("VBoxContainer/PlayButton").Pressed += OnPlayPressed;
    }

    private void OnPlayPressed()
    {
        GetTree().ChangeSceneToFile("res://Scenes/SlotSelection.tscn");
    }
}
