using Godot;
using SupremeEngine;
using System;

public partial class CardActivityEvents : Node
{
	public event Action<CardSlotPrefabScene> CardDragStarted;
	public event Action<CardSlotPrefabScene> CardDragCancelled;
	public event Action<CardSlotPrefabScene, CardSlotPrefabScene, Card> CardDragEnded;

	public void RaiseCardDragStarted(CardSlotPrefabScene source) => CardDragStarted?.Invoke(source);
	public void RaiseCardDragCancelled(CardSlotPrefabScene source) => CardDragCancelled?.Invoke(source);
	public void RaiseCardDragEnded(CardSlotPrefabScene source, CardSlotPrefabScene target, Card card) => CardDragEnded?.Invoke(source, target, card);
}
