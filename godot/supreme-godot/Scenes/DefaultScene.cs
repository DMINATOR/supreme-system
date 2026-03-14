using Godot;
using System;
using SupremeEngine;

public partial class DefaultScene : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var engine = new SupremeEngine.Engine();
		var version = engine.GetVersion();
		GD.Print($"Info: MainScene is ready = {version}");         // Standard log
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
