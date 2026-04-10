namespace SupremeEngine;

/// <see href="../../../../docs/systems/equipment_spec.md"/>
public class TransferCardCommand : ICommand
{
    private readonly CardSlot? _source;
    private readonly CardSlot _target;
    private readonly Card _card;

    public TransferCardCommand(CardSlot? source, CardSlot target, Card card)
    {
        _source = source;
        _target = target;
        _card = card;
    }

    public void Execute()
    {
        _source?.Unequip();
        _target.Equip(_card);
    }
}
