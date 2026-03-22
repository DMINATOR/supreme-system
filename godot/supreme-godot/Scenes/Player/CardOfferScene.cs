using Godot;
using SupremeEngine;
using System;

public partial class CardOfferScene : Control
{
	public event Action<Card> Accepted;
	public event Action Declined;

	private Card _card;
	private CardScene _cardScene;
	private Button _keepButton;
	private Button _discardButton;

	public void Setup(Card card)
	{
		_card = card;
	}

	public override void _Ready()
	{
		LoadNodes();
		PrepareNodes();
	}

	private void LoadNodes()
	{
		_cardScene = GetNode<CardScene>("VBoxContainer/CardScene");
		_keepButton = GetNode<Button>("VBoxContainer/ButtonRow/KeepButton");
		_discardButton = GetNode<Button>("VBoxContainer/ButtonRow/DiscardButton");
	}

	private void PrepareNodes()
	{
		if (_card == null)
		{
			GD.PushError("CardOfferScene.Setup() must be called before adding to the scene tree.");
			return;
		}

		_cardScene.Setup(_card);
		_keepButton.Pressed += OnKeepPressed;
		_discardButton.Pressed += OnDiscardPressed;
	}

	private void OnKeepPressed()
	{
		Accepted?.Invoke(_card);
	}

	private void OnDiscardPressed()
	{
		Declined?.Invoke();
	}
}
