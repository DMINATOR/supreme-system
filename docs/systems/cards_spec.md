# Cards — Spec

> Structured reference extracted from [cards.md](cards.md). Open questions: [cards_tbd.md](cards_tbd.md).

## Facts

- Card rarity tiers: Common, Uncommon, Rare, Legendary, Unique
- Unique cards do not drop — acquired through quest lines only
- Drop rates: Common 63%, Uncommon 25%, Rare 10%, Legendary 2%
- Two top-level categories: passive and combat
- Passive types: equipment
- Combat types: attack, spell, consumable
- Consumable cards are discarded on use and return to the deck after combat ends

## Rules

- Passive (equipment) cards are assigned before combat and cannot normally be changed once combat starts
- Attack cards require a weapon equipped in the relevant slot to be played
- Spell cards require no equipped weapon
- Consumable cards are single-use
- Unique companion cards can only appear in that companion's deck

## Behaviors

- Slot an equipment card into an equipment slot (before combat)
- Play an attack card (requires matching weapon equipped)
- Play a spell card
- Play a consumable card (consumed on use)
- Award unique card as a quest line reward

## Relationships

- Equipment cards occupy equipment slots — one card per slot
- Attack cards depend on a weapon being present in the weapon slot
- Unique cards are tied to a specific companion and their quest line
