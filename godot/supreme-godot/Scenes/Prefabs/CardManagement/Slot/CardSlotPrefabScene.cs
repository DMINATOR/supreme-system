using Godot;
using SupremeEngine;

public partial class CardSlotPrefabScene : DragDropContainer<CardDropContent>
{
	public CardSlot CardSlot;

	private CommandDispatcher _commandDispatcher;
	private Label _indexLabel;
	private Control _emptyView;
	private CardPrefabScene _cardView;
	private Control _draggingView;

	public void Setup(string label)
	{
		_indexLabel.Text = label;
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
		=> PrefabFactory.CreateCardDragPreviewScene(DraggedContent.Card);

	protected override CardDropContent OnDragStarted()
	{
		var content = new CardDropContent(Content.Card, CardSlot);
		Content = null;
		ShowDragging();
		return content;
	}

	protected override void OnDragCancelled(CardDropContent content)
	{
		SetCard(content.Card);
	}

	protected override void OnDropReceived(IDragContainer source, CardDropContent content)
	{
		SetCard(content.Card);
	}

	protected override void OnDropCompleted(IDragContainer source, CardDropContent content)
	{
		((CardSlotPrefabScene)source).ShowEmpty();
		if (CardSlot is not null)
			_commandDispatcher.Dispatch(new TransferCardCommand(content.CardSlot, CardSlot, content.Card));
	}

	public void SetCard(Card card)
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
