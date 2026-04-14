namespace SupremeEngine;

/// <summary>
/// Moves a card from one slot to another.
/// <list type="bullet">
/// <item>Source slot must contain a card — throws if empty.</item>
/// <item>Target slot must be empty — throws if occupied.</item>
/// <item>Source slot is unequipped before the target is equipped.</item>
/// </list>
/// </summary>
/// <see href="../../../../docs/systems/equipment_spec.md"/>
public class TransferCardCommand : ICommand
{
    private readonly CardSlot _source;
    private readonly CardSlot _target;

    public TransferCardCommand(CardSlot source, CardSlot target)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(target);

        if (source.Card is null)
            throw new InvalidOperationException("Source slot has no card to transfer.");

        _source = source;
        _target = target;
    }

    public void Execute()
    {
        var card = _source.Card!;
        _source.Unequip();
        _target.Equip(card);
    }
}
