using Godot;
using System;
using SupremeEngine;

public partial class SaveSlotRowPrefabScene : HBoxContainer
{
	public event Action NewPressed;
	public event Action LoadPressed;
	public event Action DeletePressed;

	private Label _slotLabel;
	private Button _newButton;
	private Button _loadButton;
	private Button _deleteButton;

	public override void _Ready()
	{
		LoadNodes();
		PrepareNodes();
	}

	public void Setup(SlotSummary summary)
	{
		_slotLabel.Text = summary.ToString();

		if (summary.State == SlotState.Empty)
		{
			_newButton.Visible = true;
		}
		else
		{
			_loadButton.Visible = true;
			_deleteButton.Visible = true;
		}
	}

	private void LoadNodes()
	{
		_slotLabel = GetNode<Label>("SlotLabel");
		_newButton = GetNode<Button>("NewButton");
		_loadButton = GetNode<Button>("LoadButton");
		_deleteButton = GetNode<Button>("DeleteButton");
	}

	private void PrepareNodes()
	{
		_newButton.Pressed += () => NewPressed?.Invoke();
		_loadButton.Pressed += () => LoadPressed?.Invoke();
		_deleteButton.Pressed += () => DeletePressed?.Invoke();
	}
}
