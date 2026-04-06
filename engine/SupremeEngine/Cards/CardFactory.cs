namespace SupremeEngine;

/// <see href="../../../../docs/systems/cards_spec.md"/>
public class CardFactory
{
    private readonly Random _random;

    public CardFactory(Random random)
    {
        _random = random;
    }

    public Card Create(CardTemplate template)
    {
        var id = $"{template.TemplateId}-{Guid.NewGuid():N}";
        return new Card(id, template.Name, template.Rarity, template.Type, template.DurabilityOnUse);
    }
}
