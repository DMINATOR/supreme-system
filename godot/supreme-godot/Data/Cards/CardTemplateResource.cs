using Godot;
using SupremeEngine;

public partial class CardTemplateResource : Resource
{
	[Export] public string TemplateId { get; set; } = string.Empty;
	[Export] public string Name { get; set; } = string.Empty;
	[Export] public CardRarity Rarity { get; set; }
	[Export] public CardType Type { get; set; }
	[Export] public float DurabilityOnUse { get; set; }

	public CardTemplate ToCardTemplate() => new(TemplateId, Name, Rarity, Type, DurabilityOnUse);
}
