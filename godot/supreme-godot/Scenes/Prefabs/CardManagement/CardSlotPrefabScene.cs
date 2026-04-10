using Godot;
using SupremeEngine;

public partial class CardSlotPrefabScene : PanelContainer
{
	public CardSlot EngineSlot;

	private static CardSlotPrefabScene _activeDragSource;
	private static Card _activeDragCard;

	private CardActivityEvents _cardActivityEvents;
	private CommandDispatcher _commandDispatcher;
	private Label _indexLabel;
	private Control _cardContainer;
	private Label _emptyLabel;
	private CardPrefabScene _cardScene;

	private Card _card;
	private bool _dragAndDropEnabled;

	public void Setup(string label, Card card)
	{
		_indexLabel.Text = label;
		SetCard(card);
	}

	public void EnableDragAndDrop()
	{
		_dragAndDropEnabled = true;
		UpdateCursor();
	}

	public void SetHighlight(bool highlighted)
	{
		if (highlighted)
		{
			var style = new StyleBoxFlat();
			style.BgColor = new Color(0, 1, 0, 0.3f);
			AddThemeStyleboxOverride("panel", style);
		}
		else
		{
			RemoveThemeStyleboxOverride("panel");
		}
	}

	public override void _Ready()
	{
		LoadNodes();
		PrepareNodes();
	}

	public override Variant _GetDragData(Vector2 atPosition)
	{
		if (!_dragAndDropEnabled || _card is null)
			return default;

		_activeDragSource = this;
		_activeDragCard = _card;
		SetCard(null);
		SetDragPreview(CreateDragPreview(_activeDragCard));
		_cardActivityEvents.RaiseCardDragStarted(this);

		return Variant.From("card_drag");
	}

	public override bool _CanDropData(Vector2 atPosition, Variant data)
	{
		return _dragAndDropEnabled
			&& _card is null
			&& _activeDragSource is not null
			&& _activeDragSource != this
			&& data.As<string>() == "card_drag";
	}

	public override void _DropData(Vector2 atPosition, Variant data)
	{
		var source = _activeDragSource;
		var card = _activeDragCard;
		_activeDragCard = null;
		_activeDragSource = null;
		SetCard(card);
		_cardActivityEvents.RaiseCardDragEnded(source, this, card);
	}

	public override void _Notification(int what)
	{
		base._Notification(what);

		if (what == NotificationDragEnd && _activeDragSource == this)
		{
			var card = _activeDragCard;
			_activeDragCard = null;
			_activeDragSource = null;
			SetCard(card);
			_cardActivityEvents.RaiseCardDragCancelled(this);
		}

		if (what == NotificationPredelete)
		{
			// Called when the node is about to be freed
			_cardActivityEvents.CardDragStarted -= OnCardDragStarted;
			_cardActivityEvents.CardDragCancelled -= OnCardDragCancelled;
			_cardActivityEvents.CardDragEnded -= OnCardDragEnded;
		}
	}

	private void SetCard(Card card)
	{
		_card = card;

		if (_cardScene is not null)
		{
			_cardScene.QueueFree();
			_cardScene = null;
		}

		if (card is not null)
		{
			_emptyLabel.Hide();
			_cardScene = PrefabFactory.CreateCardScene(_cardContainer, card);
		}
		else
		{
			_emptyLabel.Show();
		}

		UpdateCursor();
	}

	private void UpdateCursor()
	{
		MouseDefaultCursorShape = _dragAndDropEnabled && _card is not null
			? CursorShape.PointingHand
			: CursorShape.Arrow;
	}

	private Control CreateDragPreview(Card card)
	{
		var panel = new PanelContainer();
		panel.CustomMinimumSize = new Vector2(100, 150);

		var label = new Label();
		label.Text = card.Name;
		label.HorizontalAlignment = HorizontalAlignment.Center;
		panel.AddChild(label);

		return panel;
	}

	private void LoadNodes()
	{
		_cardActivityEvents = GetNode<CardActivityEvents>(AutoloadPath.CardActivityEvents);
		_commandDispatcher = GetNode<CommandDispatcher>(AutoloadPath.CommandDispatcher);
		_indexLabel = GetNode<Label>("VBoxContainer/IndexLabel");
		_cardContainer = GetNode<Control>("VBoxContainer/CardContainer");
		_emptyLabel = GetNode<Label>("VBoxContainer/EmptyLabel");
	}

	private void PrepareNodes()
	{
		_cardActivityEvents.CardDragStarted += OnCardDragStarted;
		_cardActivityEvents.CardDragCancelled += OnCardDragCancelled;
		_cardActivityEvents.CardDragEnded += OnCardDragEnded;
	}

	private void OnCardDragStarted(CardSlotPrefabScene source)
	{
		SetHighlight(true);
	}

	private void OnCardDragCancelled(CardSlotPrefabScene source)
	{
		SetHighlight(false);
	}

	private void OnCardDragEnded(CardSlotPrefabScene source, CardSlotPrefabScene target, Card card)
	{
		SetHighlight(false);

		if (this != target)
			return;

		if (EngineSlot is not null)
			_commandDispatcher.Dispatch(new TransferCardCommand(source.EngineSlot, EngineSlot, card));
	}
}
