using System.Linq;

namespace SupremeEngine;

/// <see href="../../../../docs/systems/bag_spec.md"/>
public class CardCollection : ICardCollection
{
    private readonly CardSlot[] _slots;

    public CardCollection(int capacity)
    {
        _slots = new CardSlot[capacity];
        for (var i = 0; i < capacity; i++)
            _slots[i] = new CardSlot();
    }

    public int Capacity => _slots.Length;

    public IReadOnlyList<CardSlot> Slots => _slots;

    public CardCollectionDto ToDto() => new()
    {
        Slots = _slots.Select(s => s.Card?.ToDto()).ToArray()
    };

    public static CardCollection FromDto(CardCollectionDto dto, int capacity)
    {
        var collection = new CardCollection(capacity);
        for (var i = 0; i < dto.Slots.Length && i < capacity; i++)
            if (dto.Slots[i] is not null)
                collection._slots[i].Equip(Card.FromDto(dto.Slots[i]!));
        return collection;
    }
}
