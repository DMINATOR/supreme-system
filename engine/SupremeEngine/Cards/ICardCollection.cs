using System.Linq;

namespace SupremeEngine;

public interface ICardCollection
{
    int Capacity { get; }
    IReadOnlyList<CardSlot> Slots { get; }

    bool IsFull => Slots.All(s => s.Card is not null);

    CardSlot? FirstEmptySlot() => Slots.FirstOrDefault(s => s.Card is null);
}
