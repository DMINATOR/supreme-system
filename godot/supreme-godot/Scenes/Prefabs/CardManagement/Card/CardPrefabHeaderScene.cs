using Godot;
using SupremeEngine;

public partial class CardPrefabHeaderScene : TextureRect
{
	[Export]
	public Texture2D CommonHeader { get; set; }

	[Export]
	public Texture2D UncommonHeader { get; set; }

	[Export]
	public Texture2D RareHeader { get; set; }

	[Export]
	public Texture2D LegendaryHeader { get; set; }

	[Export]
	public Texture2D UniqueHeader { get; set; }

	public void Setup(CardRarity rarity)
	{
		Texture = rarity switch
		{
			CardRarity.Common => CommonHeader,
			CardRarity.Uncommon => UncommonHeader,
			CardRarity.Rare => RareHeader,
			CardRarity.Legendary => LegendaryHeader,
			CardRarity.Unique => UniqueHeader,
			_ => CommonHeader
		};
	}
}
