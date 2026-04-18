# Core Loop — Spec

> Structured reference extracted from [core-loop.md](core-loop.md). Open questions: [core-loop_tbd.md](core-loop_tbd.md).

## Facts

- Map type: open world 2D — seamless or zoned ([TBD](core-loop_tbd.md))
- Progression: infinite, no fixed end goal
- Each zone or region has an area level that determines the level of enemies and card drops within it
- Area level assignment per zone: [TBD](core-loop_tbd.md)
- Combat type: turn-based card combat
- Energy or action constraint system: [TBD](core-loop_tbd.md)
- Hand draw behavior: [TBD](core-loop_tbd.md) (per-turn draw or persistent hand)
- Final boss: [TBD](core-loop_tbd.md)

## Rules

- The player roams freely between encounters — no forced path
- Progress is saved at rest sites only — the game cannot be saved elsewhere
- On party wipe, the party reverts to the last rest site save
- All cards, loot, and map progress acquired since the last rest site are lost on party wipe
- Meta progression specifics (what persists beyond the run): [TBD](core-loop_tbd.md)

## Behaviors

- Trigger an encounter (by approaching or being found by an enemy)
- Save progress at a rest site
- Scale enemy stats to match the area level of the current zone
- Revert party to last rest site save on party wipe
- Award loot after a completed encounter

## Relationships

- Encounters are the primary source of loot (cards go into the bag)
- Rest sites are the save and checkpoint boundary
- Meta progression persists across runs; everything else resets
- Area level drives card level and enemy difficulty — see [progression-and-meta_spec.md](progression-and-meta_spec.md)
