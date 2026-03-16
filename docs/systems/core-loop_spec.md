# Core Loop — Spec

> Structured reference extracted from [core-loop.md](core-loop.md). Open questions: [core-loop_tbd.md](core-loop_tbd.md).

## Facts

- Map type: open world 2D — seamless or zoned ([TBD](core-loop_tbd.md))
- Progression: infinite, no fixed end goal
- Monster scaling trigger: [TBD](core-loop_tbd.md) (distance, time, or other)
- Combat type: turn-based card combat
- Energy or action constraint system: [TBD](core-loop_tbd.md)
- Hand draw behavior: [TBD](core-loop_tbd.md) (per-turn draw or persistent hand)
- Final boss: [TBD](core-loop_tbd.md)

## Rules

- The player roams freely between encounters — no forced path
- A run ends only on a party wipe; individual member deaths do not end it
- Progress is saved at rest sites only
- On party wipe: current decks, items, and map progress are lost
- On party wipe: meta progression (currency, unlocks) for that slot is kept (specifics [TBD](core-loop_tbd.md))
- After a party wipe the player does not resume from the last rest site — the run ends

## Behaviors

- Trigger an encounter (by approaching or being found by an enemy)
- Save progress at a rest site
- Apply monster scaling over time
- End run on party wipe
- Award loot after a completed encounter

## Relationships

- Encounters are the primary source of loot (cards go into the bag)
- Rest sites are the save and checkpoint boundary
- Meta progression persists across runs; everything else resets
