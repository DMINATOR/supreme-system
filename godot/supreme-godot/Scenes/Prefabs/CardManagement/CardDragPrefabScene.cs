using Godot;
using SupremeEngine;

public partial class CardDragPrefabScene : PanelContainer
{
	public void Setup(Card card)
	{
		CustomMinimumSize = new Vector2(100, 150);

		var label = new Label();
		label.Text = card.Name;
		label.HorizontalAlignment = HorizontalAlignment.Center;
		label.VerticalAlignment = VerticalAlignment.Center;
		label.SetAnchorsAndOffsetsPreset(Control.LayoutPreset.FullRect);
		AddChild(label);
	}
}
