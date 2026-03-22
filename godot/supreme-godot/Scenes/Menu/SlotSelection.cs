using Godot;
using SupremeEngine;

public partial class SlotSelection : Control
{
	private SaveManager _saveManager;
	private SceneManager _sceneManager;
	private WorldManager _worldManager;

	private Button _backButton;
	private VBoxContainer _slotsContainer;

	public override void _Ready()
	{
		LoadNodes();
		PrepareNodes();
	}

	private void LoadNodes()
	{
		_saveManager = GetNode<SaveManager>(AutoloadPath.SaveManager);
		_sceneManager = GetNode<SceneManager>(AutoloadPath.SceneManager);
		_worldManager = GetNode<WorldManager>(AutoloadPath.WorldManager);
		_backButton = GetNode<Button>("VBoxContainer/BackButton");
		_slotsContainer = GetNode<VBoxContainer>("VBoxContainer/SlotsContainer");
	}

	private void PrepareNodes()
	{
		_backButton.Pressed += OnBackPressed;
		RefreshSlots();
	}

	private void RefreshSlots()
	{
		foreach (Node child in _slotsContainer.GetChildren())
			child.QueueFree();

		var summaries = _saveManager.GetAllSummaries();
		for (int i = 0; i < SaveManager.SlotCount; i++)
		{
			var row = BuildSlotRow(summaries[i]);
			_slotsContainer.CallDeferred(Node.MethodName.AddChild, row);
		}
	}

	private HBoxContainer BuildSlotRow(SlotSummary summary)
	{
		var row = new HBoxContainer();

		var label = new Label();
		label.SizeFlagsHorizontal = Control.SizeFlags.ExpandFill;

		if (summary.State == SlotState.Empty)
		{
			label.Text = summary.ToString();

			var newBtn = new Button { Text = "New" };
			newBtn.Pressed += () => OnNewPressed(summary.Index);
			row.AddChild(label);
			row.AddChild(newBtn);
		}
		else
		{
			label.Text = summary.ToString();

			var loadBtn = new Button { Text = "Load" };
			loadBtn.Pressed += () => OnLoadPressed(summary.Index);

			var deleteBtn = new Button { Text = "Delete" };
			deleteBtn.Pressed += () => OnDeletePressed(summary.Index);

			row.AddChild(label);
			row.AddChild(loadBtn);
			row.AddChild(deleteBtn);
		}

		return row;
	}

	private void OnNewPressed(int index)
	{
		_worldManager.StartNewGame(_saveManager, index);
		_worldManager.SaveToActiveSlot(_saveManager);
		_sceneManager.GoToWorld();
	}

	private void OnLoadPressed(int index)
	{
		if (_worldManager.LoadFromSlot(this, _saveManager, index))
			_sceneManager.GoToWorld();
	}

	private void OnDeletePressed(int index)
	{
		DialogHelper.ShowConfirm(this, $"Delete Slot {index + 1}? This cannot be undone.", () =>
		{
			_saveManager.DeleteSlot(index);
			RefreshSlots();
		});
	}

	private void OnBackPressed()
	{
		_sceneManager.GoToMainMenu();
	}
}
