using Godot;
using SupremeEngine;

public partial class CardSlotPrefabScene : PanelContainer
{
	[Signal] public delegate void CardDragStartedEventHandler();

	private static CardSlotPrefabScene _activeDragSource;
	private static Card _activeDragCard;

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
	}

	public override Variant _GetDragData(Vector2 atPosition)
	{
		if (!_dragAndDropEnabled || _card is null)
			return default;

		_activeDragSource = this;
		_activeDragCard = _card;
		SetCard(null);
		SetDragPreview(CreateDragPreview(_activeDragCard));
		EmitSignal(SignalName.CardDragStarted);

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
		var card = _activeDragCard;
		_activeDragCard = null;
		_activeDragSource = null;
		SetCard(card);
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
		_indexLabel = GetNode<Label>("VBoxContainer/IndexLabel");
		_cardContainer = GetNode<Control>("VBoxContainer/CardContainer");
		_emptyLabel = GetNode<Label>("VBoxContainer/EmptyLabel");
	}
}
