using Godot;
using SupremeEngine;

public partial class CardCreatorScene : Control
{
	private const string LibraryPath = "res://Data/Cards/CardTemplateLibrary.tres";

	private WorldManager _worldManager;
	private VBoxContainer _templatesContainer;
	private Control _offerContainer;
	private VBoxContainer _bagContainer;

	public override void _Ready()
	{
		LoadNodes();
		PrepareNodes();
	}

	private void LoadNodes()
	{
		_worldManager = GetNode<WorldManager>(AutoloadPath.WorldManager);
		_templatesContainer = GetNode<VBoxContainer>("VBoxContainer/ContentContainer/ScrollContainer/TemplatesContainer");
		_offerContainer = GetNode<Control>("VBoxContainer/OfferContainer");
		_bagContainer = GetNode<VBoxContainer>("VBoxContainer/ContentContainer/BagContainer");
	}

	private void PrepareNodes()
	{
		PopulateTemplates();
		RefreshBag();
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

		var createButton = new Button { Text = "Create" };
		createButton.Pressed += () => OnCreatePressed(resource);

		row.AddChild(label);
		row.AddChild(createButton);
		return row;
	}

	private void OnCreatePressed(CardTemplateResource resource)
	{
		foreach (Node child in _offerContainer.GetChildren())
			child.QueueFree();

		ICardCollection bag = _worldManager.State.Inventory.Bag;
		var slot = bag.FirstEmptySlot();
		if (slot is null)
		{
			DialogHelper.ShowError(this, "Bag is full.");
			return;
		}

		var factory = new CardFactory(_worldManager.State.Random);
		slot.Equip(factory.Create(resource.ToCardTemplate()));
		RefreshBag();
	}

	private void RefreshBag()
	{
		foreach (Node child in _bagContainer.GetChildren())
			child.QueueFree();
		PrefabFactory.CreateBagScene(_bagContainer, _worldManager.State.Inventory.Bag);
	}

	private void ClearOffer()
	{
		foreach (Node child in _offerContainer.GetChildren())
			child.QueueFree();
	}
}
