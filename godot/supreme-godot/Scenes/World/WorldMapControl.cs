using Godot;
using SupremeEngine;
using System;
using System.Collections.Generic;

public partial class WorldMapControl : Control
{
    public event Action<Region> RegionSelected;

    public void Setup(Dictionary<(int X, int Y), Region> regions)
    {
    }

    public override void _Draw()
    {
    }
}
