using Godot;
using SupremeEngine;

public partial class CardSlotPrefabScene : DragDropContainer
{
	public CardSlot CardSlot;

	private CommandDispatcher _commandDispatcher;
	private Label _indexLabel;
	private Control _cardContainer;
	private Label _emptyLabel;
	private CardPrefabScene _cardScene;

	private Card _card;

	public void Setup(string label, Card card)
	{
		_indexLabel.Text = label;
		SetCard(card);
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
		SetCard(null);
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
		if (CardSlot is not null)
			_commandDispatcher.Dispatch(new TransferCardCommand(cardSource.CardSlot, CardSlot, _card));
	}

	private void SetCard(Card card)
	{
		_card = card;
		Content = card is not null ? new CardDropContent(card) : null;

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

	private void LoadNodes()
	{
		_commandDispatcher = GetNode<CommandDispatcher>(AutoloadPath.CommandDispatcher);
		_indexLabel = GetNode<Label>("VBoxContainer/IndexLabel");
		_cardContainer = GetNode<Control>("VBoxContainer/CardContainer");
		_emptyLabel = GetNode<Label>("VBoxContainer/EmptyLabel");
	}

	private void PrepareNodes()
	{
		DragKey = "card_drag";
	}
}
