using Godot;
using System;

public static class DialogHelper
{
    public static void ShowConfirm(Node parent, string message, Action onConfirmed)
    {
        var dialog = new ConfirmationDialog();
        dialog.DialogText = message;
        dialog.Confirmed += () =>
        {
            onConfirmed();
            dialog.QueueFree();
        };
        dialog.Canceled += () => dialog.QueueFree();
        parent.AddChild(dialog);
        dialog.PopupCentered();
    }

    public static void ShowError(Node parent, string message)
    {
        GD.PushError(message);
        var dialog = new AcceptDialog();
        dialog.DialogText = message;
        dialog.Confirmed += () => dialog.QueueFree();
        parent.AddChild(dialog);
        dialog.PopupCentered();
    }
}
