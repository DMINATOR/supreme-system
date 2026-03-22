namespace SupremeEngine;

/// <see href="../../../../docs/systems/bag_spec.md"/>
public class BagManager
{
    private readonly Bag _bag;

    public BagManager() : this(new Bag()) { }

    private BagManager(Bag bag)
    {
        _bag = bag;
    }

    public IReadOnlyList<Card> Cards => _bag.Cards;

    public void AddCard(Card card) => _bag.AddCard(card);

    public void RemoveCard(Card card) => _bag.RemoveCard(card);

    public BagDto ToDto() => _bag.ToDto();

    public static BagManager FromDto(BagDto dto) => new(Bag.FromDto(dto));
}
