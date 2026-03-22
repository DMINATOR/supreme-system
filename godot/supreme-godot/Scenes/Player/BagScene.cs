using Godot;

public partial class BagScene : Control
{
	private SceneManager _sceneManager;
	private WorldManager _worldManager;
	private Button _backButton;
	private HFlowContainer _cardsContainer;

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
		_cardsContainer = GetNode<HFlowContainer>("VBoxContainer/ScrollContainer/CardsContainer");
	}

	private void PrepareNodes()
	{
		_backButton.Pressed += _sceneManager.GoToWorld;
		RefreshCards();
	}

	private void RefreshCards()
	{
		foreach (Node child in _cardsContainer.GetChildren())
			child.QueueFree();

		var cards = _worldManager.State.BagManager.Cards;
		if (cards.Count == 0)
		{
			var empty = new Label { Text = "Bag is empty." };
			_cardsContainer.AddChild(empty);
			return;
		}

		foreach (var card in cards)
			CardSceneHelper.CreateCardScene(_cardsContainer, card);
	}
}
