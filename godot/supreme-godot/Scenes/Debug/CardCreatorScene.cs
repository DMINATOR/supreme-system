using Godot;
using SupremeEngine;

public partial class CardCreatorScene : Control
{
	private const string LibraryPath = "res://Data/Cards/CardTemplateLibrary.tres";

	private SceneManager _sceneManager;
	private WorldManager _worldManager;
	private Button _backButton;
	private VBoxContainer _templatesContainer;

	public override void _Ready()
	{
		LoadNodes();
		PrepareNodes();
	}

	private void LoadNodes()
	{
		_sceneManager = GetNode<SceneManager>(AutoloadPath.SceneManager);
		_worldManager = GetNode<WorldManager>(AutoloadPath.WorldManager);
		_backButton = GetNode<Button>("VBoxContainer/BackButton");
		_templatesContainer = GetNode<VBoxContainer>("VBoxContainer/ScrollContainer/TemplatesContainer");
	}

	private void PrepareNodes()
	{
		_backButton.Pressed += _sceneManager.GoToWorld;
		PopulateTemplates();
	}

	private void PopulateTemplates()
	{
		var library = GD.Load<CardTemplateLibrary>(LibraryPath);
		if (library == null)
		{
			DialogHelper.ShowError(this, "Failed to load card template library.");
			return;
		}

		foreach (var resource in library.Templates)
		{
			var row = BuildTemplateRow(resource);
			_templatesContainer.AddChild(row);
		}
	}

	private HBoxContainer BuildTemplateRow(CardTemplateResource resource)
	{
		var row = new HBoxContainer();

		var label = new Label();
		label.SizeFlagsHorizontal = Control.SizeFlags.ExpandFill;
		label.Text = $"{resource.Name} [{resource.Rarity} / {resource.Type}]";

		var addButton = new Button { Text = "Add to Bag" };
		addButton.Pressed += () => OnAddToBagPressed(resource);

		row.AddChild(label);
		row.AddChild(addButton);
		return row;
	}

	private void OnAddToBagPressed(CardTemplateResource resource)
	{
		var factory = new CardFactory(_worldManager.State.Random);
		var card = factory.Create(resource.ToCardTemplate());
		_worldManager.State.Bag.AddCard(card);
	}
}
