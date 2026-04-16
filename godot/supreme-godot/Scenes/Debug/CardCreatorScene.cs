using Godot;
using SupremeEngine;

public partial class CardCreatorScene : Control
{
	[Export] public CardTemplateLibrary TemplateLibrary { get; set; }

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
		if (TemplateLibrary is null)
		{
			GD.PushError("CardCreatorScene: TemplateLibrary not set.");
			return;
		}

		foreach (var resource in TemplateLibrary.Templates)
		{
			var row = PrefabFactory.CreateCardTemplateRowScene(
				_templatesContainer,
				$"{resource.Name} [{resource.Rarity} / {resource.Type}]");
			row.CreatePressed += () => OnCreatePressed(resource);
		}
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
