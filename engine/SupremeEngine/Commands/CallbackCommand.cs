namespace SupremeEngine;

using System;

/// <summary>
/// Base class for commands that report their outcome via callbacks rather than throwing.
/// Subclasses must call <see cref="InvokeSuccess"/> or <see cref="InvokeFailure"/> in their <c>Execute</c> implementation.
/// </summary>
public abstract class CallbackCommand : ICommand
{
    private readonly Action? _onSuccess;
    private readonly Action<Exception>? _onFailure;

    protected CallbackCommand(Action? onSuccess, Action<Exception>? onFailure)
    {
        _onSuccess = onSuccess;
        _onFailure = onFailure;
    }

    public abstract void Execute();

    protected void InvokeSuccess() => _onSuccess?.Invoke();

    protected void InvokeFailure(Exception ex) => _onFailure?.Invoke(ex);
}
