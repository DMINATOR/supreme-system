# Cards — Spec

> Structured reference extracted from [cards.md](cards.md). Open questions: [cards_tbd.md](cards_tbd.md).

## Facts

- Card rarity tiers: Common, Uncommon, Rare, Legendary, Unique
- Unique cards do not drop — acquired through quest lines only
- Drop rates: Common 63%, Uncommon 25%, Rare 10%, Legendary 2%
- Card types: Equipment (passive), Attack, Spell, Consumable
- Consumable cards are discarded on use and return to the deck after combat ends
- Every card has a `Durability` value (float, range 0.0–1.0) representing how much use it has left
- All cards start with `Durability = 1.0` when created from a template
- `DurabilityOnUse` is defined on the card template; it is the fraction of durability lost each time the card is played or damaged (e.g. `0.1` → 10 uses at full durability)
- Broken threshold: 0.2 — a card with `Durability` below this value is broken
- A broken card has all its raw stat values halved but remains playable
- A card with `Durability` ≤ 0 is destroyed — permanently removed from wherever it is held (bag, deck, or equipment slot) and cannot be recovered
- Every card has a `Level` (positive integer, minimum 1)
- A card's level is set at drop time and equals the area level of the zone where it was found
- Card level scales the card's raw stat values (e.g. damage, healing, shield) — exact scaling formula is TBD
- There is no maximum card level

## Rules

- Passive (equipment) cards are assigned before combat and cannot normally be changed once combat starts
- Attack cards require a weapon equipped in the relevant slot to be played
- Spell cards require no equipped weapon
- Consumable cards are single-use
- Unique companion cards can only appear in that companion's deck
- A broken card (Durability < 0.2) has all its raw stat values halved but can still be played
- A destroyed card (Durability ≤ 0) is immediately and permanently removed from wherever it is held — bag, deck, or equipment slot
- A destroyed card cannot be recovered by any means
- A card's level cannot change after it drops
- A card always drops at the current area level of the zone it was found in
- Card level affects raw stat values only — not rarity, card type, or durability

## Behaviors

- Slot an equipment card into an equipment slot (before combat)
- Play an attack card (requires matching weapon equipped)
- Play a spell card
- Play a consumable card (consumed on use)
- Award unique card as a quest line reward
- Reduce a card's `Durability` by its template's `DurabilityOnUse` fraction when played or damaged
- Apply broken state (halved stats) when a card's `Durability` drops below 0.2
- Destroy a card (permanently remove it from its current location) when its `Durability` drops to or below 0
- Drop a card at the current area level, setting its `Level` at that point
- Scale a card's raw stat values according to its `Level` when it is created

## Relationships

- Equipment cards occupy equipment slots — one card per slot
- Attack cards depend on a weapon being present in the weapon slot
- Unique cards are tied to a specific companion and their quest line
- Card level is derived from area level — see [Progression & Meta](progression-and-meta.md)

## Rationale

- **Durability as risk-reward** — the broken threshold at 0.2 lets players deliberately push a card past the safe zone in high-stakes moments, accepting halved stats in exchange for continued use. Permanent destruction at 0.0 ensures there is a real cost to ignoring durability entirely, distinguishing this from degradation systems where nothing ultimately breaks. Float values (0.0–1.0) leave room for fractional repair mechanics in future without changing the model.
