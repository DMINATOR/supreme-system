# Progression & Meta — Spec

> Structured reference extracted from [progression-and-meta.md](progression-and-meta.md). Open questions: [progression-and-meta_tbd.md](progression-and-meta_tbd.md).

## Facts

- In-run progression is driven exclusively by card level
- There is no character leveling, experience gain, or skill learning
- Card level is inherited from the area level of the zone where the card drops
- Meta progression specifics: TBD

## Rules

- The only way to improve the deck's power is to acquire higher-level cards from higher-level areas
- A party cannot increase the level of a card they already own — they must find a replacement
- On party wipe, all cards and progress acquired since the last rest site are lost
- Meta progression (if any) persists across runs

## Behaviors

- Drop a card at the area level of the current zone when an encounter reward is granted
- Replace lower-level cards in the deck with higher-level drops acquired from more dangerous areas

## Relationships

- Card level depends on area level — see [cards_spec.md](cards_spec.md)
- Area level is a property of zone/region — see [core-loop_spec.md](core-loop_spec.md)
- Cards flow from encounter loot → bag → deck — see [core-loop_spec.md](core-loop_spec.md)
