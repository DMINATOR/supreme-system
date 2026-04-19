namespace SupremeEngine;

/// <see href="../../../../docs/systems/map-and-structure_spec.md"/>
public class RegionDto
{
    public int X { get; set; }
    public int Y { get; set; }
    public int AreaLevel { get; set; }
    public List<RegionLocationDto> Locations { get; set; } = new();
}
