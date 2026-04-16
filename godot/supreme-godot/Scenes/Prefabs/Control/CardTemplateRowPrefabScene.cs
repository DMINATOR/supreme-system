using Godot;
using System;

public partial class CardTemplateRowPrefabScene : HBoxContainer
{
	public event Action CreatePressed;

	private Label _nameLabel;
	private Button _createButton;

	public override void _Ready()
	{
		LoadNodes();
		PrepareNodes();
	}

	public void Setup(string displayText)
	{
		_nameLabel.Text = displayText;
	}

	private void LoadNodes()
	{
		_nameLabel = GetNode<Label>("NameLabel");
		_createButton = GetNode<Button>("CreateButton");
	}

	private void PrepareNodes()
	{
		_createButton.Pressed += () => CreatePressed?.Invoke();
	}
}
