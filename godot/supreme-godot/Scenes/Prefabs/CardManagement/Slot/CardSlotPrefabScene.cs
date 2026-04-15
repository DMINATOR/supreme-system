using Godot;
using SupremeEngine;

public partial class CardSlotPrefabScene : DragDropContainerBase
{
	public CardSlot CardSlot { get; private set; }

	private CommandDispatcher _commandDispatcher;
	private Label _indexLabel;
	private Control _emptyView;
	private CardPrefabScene _cardView;
	private Control _draggingView;

	public void Setup(CardSlot cardSlot, string label)
	{
		CardSlot = cardSlot;
		_indexLabel.Text = label;
		if (cardSlot.Card is not null)
			SetCard(cardSlot.Card);
		else
			ShowEmpty();
	}

	public void EnableDragAndDrop()
	{
		IsEnabled = true;
		UpdateCursor();
	}

	protected override bool CanDrag() => _cardView.Visible;
	protected override bool CanAcceptDrop() => _emptyView.Visible;

	public override void _Ready()
	{
		base._Ready();
		LoadNodes();
		PrepareNodes();
	}

	protected override Control GetDragPreviewNode()
		=> PrefabFactory.CreateCardDragPreviewScene(((CardDropContent)DraggedContent).CardSlot.Card);

	protected override DropContent CreateDragContent()
	{
		ShowDragging();
		return new CardDropContent(CardSlot);
	}

	protected override void HandleDragCancelled(DropContent content)
	{
		SetCard(((CardDropContent)content).CardSlot.Card);
	}

	protected override void HandleDropReceived(IDragContainer source, DropContent content)
	{
		SetCard(((CardDropContent)content).CardSlot.Card);
	}

	protected override void HandleDropCompleted(IDragContainer source, DropContent content)
	{
		((CardSlotPrefabScene)source).ShowEmpty();
		_commandDispatcher.Dispatch(new TransferCardCommand(((CardDropContent)content).CardSlot, CardSlot));
	}

	public void SetCard(Card card)
	{
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
