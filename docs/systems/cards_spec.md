# Cards — Spec

> Structured reference extracted from [cards.md](cards.md). Open questions: [cards_tbd.md](cards_tbd.md).

## Facts

- Card rarity tiers: Common, Uncommon, Rare, Legendary, Unique
- Unique cards do not drop — acquired through quest lines only
- Drop rates: Common 63%, Uncommon 25%, Rare 10%, Legendary 2%
- Card types: Equipment (passive), Attack, Spell, Consumable
- Consumable cards are discarded on use and return to the deck after combat ends
- Every card has a `Durability` value (float) representing its current durability
- `DurabilityOnUse` is defined on the card template; it sets both the per-use durability cost and the card's initial `Durability` when created from the template
- A card with `Durability` ≤ 0 is broken and cannot be used (`IsUsable` is false)

## Rules

- Passive (equipment) cards are assigned before combat and cannot normally be changed once combat starts
- Attack cards require a weapon equipped in the relevant slot to be played
- Spell cards require no equipped weapon
- Consumable cards are single-use
- Unique companion cards can only appear in that companion's deck
- A broken card (Durability ≤ 0) loses all properties and cannot be played

## Behaviors

- Slot an equipment card into an equipment slot (before combat)
- Play an attack card (requires matching weapon equipped)
- Play a spell card
- Play a consumable card (consumed on use)
- Award unique card as a quest line reward
- Reduce a card's `Durability` by `DurabilityOnUse` when played or damaged
- Check `IsUsable` before allowing a card to be played

## Relationships

- Equipment cards occupy equipment slots — one card per slot
- Attack cards depend on a weapon being present in the weapon slot
- Unique cards are tied to a specific companion and their quest line
