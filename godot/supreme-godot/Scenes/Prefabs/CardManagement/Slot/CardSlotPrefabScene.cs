using Godot;
using SupremeEngine;

public partial class CardSlotPrefabScene : DragDropContainer
{
	public CardSlot CardSlot;

	private CommandDispatcher _commandDispatcher;
	private Label _indexLabel;
	private Control _emptyView;
	private CardPrefabScene _cardView;
	private Control _draggingView;

	public void Setup(string label, Card? card)
	{
		_indexLabel.Text = label;
		if (card is not null)
			SetCard(card);
		else
			ShowEmpty();
	}

	public void EnableDragAndDrop()
	{
		IsEnabled = true;
		UpdateCursor();
	}

	public override void _Ready()
	{
		base._Ready();
		LoadNodes();
		PrepareNodes();
	}

	protected override Control GetDragPreviewNode()
		=> PrefabFactory.CreateCardDragPreviewScene(((CardDropContent)DraggedContent).Card);

	protected override void OnDragStarted()
	{
		Content = null;
		ShowDragging();
	}

	protected override void OnDragCancelled()
	{
		SetCard(((CardDropContent)DraggedContent).Card);
	}

	protected override void OnDropReceived(DragDropContainer source)
	{
		SetCard(((CardDropContent)source.DraggedContent).Card);
	}

	protected override void OnDropCompleted(DragDropContainer source)
	{
		var cardSource = (CardSlotPrefabScene)source;
		cardSource.ShowEmpty();
		if (CardSlot is not null)
			_commandDispatcher.Dispatch(new TransferCardCommand(cardSource.CardSlot, CardSlot, _cardView.Card));
	}

	private void SetCard(Card card)
	{
		Content = new CardDropContent(card);
		_cardView.Setup(card);
		ShowState(_cardView);
		UpdateCursor();
	}

	public void ShowEmpty()
	{
		ShowState(_emptyView);
		UpdateCursor();
	}

	private void ShowDragging()
	{
		ShowState(_draggingView);
		UpdateCursor();
	}

	private void ShowState(Control active)
	{
		_emptyView.Visible = _emptyView == active;
		_cardView.Visible = _cardView == active;
		_draggingView.Visible = _draggingView == active;
	}

	private void LoadNodes()
	{
		_commandDispatcher = GetNode<CommandDispatcher>(AutoloadPath.CommandDispatcher);
		_indexLabel = GetNode<Label>("VBoxContainer/IndexLabel");
		_emptyView = GetNode<Control>("VBoxContainer/EmptyView");
		_cardView = GetNode<CardPrefabScene>("VBoxContainer/CardView");
		_draggingView = GetNode<Control>("VBoxContainer/DraggingView");
	}

	private void PrepareNodes()
	{
		DragKey = "card_drag";
	}
}
