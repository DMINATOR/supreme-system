using Godot;
using SupremeEngine;
using System;
using System.Collections.Generic;

public partial class CommandDispatcher : Node
{
	private readonly Queue<ICommand> _queue = new();

	public void Dispatch(ICommand command)
	{
		_queue.Enqueue(command);
	}

	public override void _Process(double delta)
	{
		while (_queue.Count > 0)
		{
			var command = _queue.Dequeue();
			try
			{
				command.Execute();
			}
			catch (Exception ex)
			{
				GD.PushError($"CommandDispatcher: {command.GetType().Name} threw {ex.GetType().Name}: {ex.Message}");
			}
		}
	}
}
