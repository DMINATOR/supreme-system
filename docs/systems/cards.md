# Cards

> Facts, rules, behaviors, and relationships: [cards_spec.md](cards_spec.md)

Definition of individual cards and their types used in [decks](../glossary.md#deck).

Cards have four types: **Equipment**, **Attack**, **Spell**, and **Consumable**.

### Equipment

Assigned before combat begins. Cannot normally be changed once combat starts. Slotted into [equipment slots](../glossary.md#equipment-slot) (one per slot, per member). A weapon must be equipped to play attack cards.

### Attack

Offensive cards that require a weapon to be equipped in the relevant [equipment slot](../glossary.md#equipment-slot). The attack is made using that weapon.

### Spell

Offensive or utility cards that require no equipped weapon.

### Consumable

Single-use cards such as potions. When played, the card is discarded and unavailable for the rest of combat. It returns to the deck after combat ends.

## Card Rarity

Cards have five rarity tiers: **Common**, **Uncommon**, **Rare**, **Legendary**, and **Unique**.

- Common through Legendary cards can be acquired through drops: Common 63%, Uncommon 25%, Rare 10%, Legendary 2%.
- **Unique** cards never drop. They are acquired exclusively through [companion quest lines](../glossary.md#companion-quest-line).

## Unique Companion Cards

- Some cards are exclusive to specific [companions](../glossary.md#companion) and can only appear in their [deck](../glossary.md#deck)
- These are acquired through [companion quest lines](../glossary.md#companion-quest-line)

## Durability

Every card has a [durability](../glossary.md#durability) value. When a card is played or damaged, its durability decreases by the card's **durability on use** amount (defined in the card template). A card whose durability reaches 0 is **broken** — it loses all its properties and cannot be played.

## Card Level

Every card has a [card level](../glossary.md#card-level) that is fixed the moment it drops. The level is inherited from the [area level](../glossary.md#area-level) of the zone where the card was found — a card dropped in a level 5 area is a level 5 card.

Card level scales the card's raw stat values (damage, healing, shield, etc.). There is no maximum level. Card levels are not persistent — they reset when the run ends.

This is the game's primary in-run progression mechanism: the player must push into higher-level areas to find higher-level cards and grow the deck's power. There is no experience gain, grinding, or crafting — the world itself is the upgrade system.
