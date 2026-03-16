using Godot;
using System;
using SupremeEngine;

public partial class SlotSelection : Control
{
    private SaveManager _saveManager;

    public override void _Ready()
    {
        _saveManager = GetNode<SaveManager>("/root/SaveManager");
        GetNode<Button>("VBoxContainer/BackButton").Pressed += OnBackPressed;
        RefreshSlots();
    }

    private void RefreshSlots()
    {
        var container = GetNode<VBoxContainer>("VBoxContainer/SlotsContainer");

        foreach (Node child in container.GetChildren())
            child.QueueFree();

        var summaries = _saveManager.GetAllSummaries();
        for (int i = 0; i < SaveManager.SlotCount; i++)
        {
            var row = BuildSlotRow(summaries[i]);
            container.CallDeferred(Node.MethodName.AddChild, row);
        }
    }

    private HBoxContainer BuildSlotRow(SlotSummary summary)
    {
        var row = new HBoxContainer();

        var label = new Label();
        label.SizeFlagsHorizontal = Control.SizeFlags.ExpandFill;

        if (summary.State == SlotState.Empty)
        {
            label.Text = $"Slot {summary.Index + 1} — Empty";

            var newBtn = new Button { Text = "New" };
            newBtn.Pressed += () => OnNewPressed(summary.Index);
            row.AddChild(label);
            row.AddChild(newBtn);
        }
        else
        {
            var time = TimeSpan.FromSeconds(summary.TotalSecondsPlayed);
            label.Text = $"Slot {summary.Index + 1} — {(int)time.TotalHours:D2}:{time.Minutes:D2}:{time.Seconds:D2}";

            var loadBtn = new Button { Text = "Load" };
            loadBtn.Pressed += () => OnLoadPressed(summary.Index);

            var deleteBtn = new Button { Text = "Delete" };
            deleteBtn.Pressed += () => OnDeletePressed(summary.Index);

            row.AddChild(label);
            row.AddChild(loadBtn);
            row.AddChild(deleteBtn);
        }

        return row;
    }

    private void OnNewPressed(int index)
    {
        var world = new WorldState();
        _saveManager.SaveWorld(index, world);
        _saveManager.SetActiveSlot(index);
        GetTree().ChangeSceneToFile("res://Scenes/DefaultScene.tscn");
    }

    private void OnLoadPressed(int index)
    {
        _saveManager.SetActiveSlot(index);
        GetTree().ChangeSceneToFile("res://Scenes/DefaultScene.tscn");
    }

    private void OnDeletePressed(int index)
    {
        var dialog = new ConfirmationDialog();
        dialog.DialogText = $"Delete Slot {index + 1}? This cannot be undone.";
        dialog.Confirmed += () =>
        {
            _saveManager.DeleteSlot(index);
            dialog.QueueFree();
            RefreshSlots();
        };
        dialog.Canceled += () => dialog.QueueFree();
        AddChild(dialog);
        dialog.PopupCentered();
    }

    private void OnBackPressed()
    {
        GetTree().ChangeSceneToFile("res://Scenes/MainMenu.tscn");
    }
}
