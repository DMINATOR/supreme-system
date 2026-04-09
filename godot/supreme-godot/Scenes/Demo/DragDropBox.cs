using Godot;

public partial class DragDropBox : PanelContainer
{
	private Label _label;
	private Color _color;

	public void Setup(string text, Color color)
	{
		_color = color;
		ApplyColor(color);

		_label = new Label();
		_label.Text = text;
		_label.HorizontalAlignment = HorizontalAlignment.Center;
		_label.VerticalAlignment = VerticalAlignment.Center;
		_label.SizeFlagsHorizontal = SizeFlags.ExpandFill;
		_label.SizeFlagsVertical = SizeFlags.ExpandFill;
		AddChild(_label);
	}

	public override Variant _GetDragData(Vector2 atPosition)
	{
		var previewBox = new PanelContainer();
		previewBox.CustomMinimumSize = new Vector2(80, 80);
		var previewStyle = new StyleBoxFlat();
		previewStyle.BgColor = _color;
		previewBox.AddThemeStyleboxOverride("panel", previewStyle);
		SetDragPreview(previewBox);

		return Variant.From(_color);
	}

	public override bool _CanDropData(Vector2 atPosition, Variant data)
	{
		return data.VariantType == Variant.Type.Color;
	}

	public override void _DropData(Vector2 atPosition, Variant data)
	{
		var dropped = data.As<Color>();
		_color = dropped;
		ApplyColor(dropped);
	}

	private void ApplyColor(Color color)
	{
		var style = new StyleBoxFlat();
		style.BgColor = color;
		AddThemeStyleboxOverride("panel", style);
	}
}
