# Core Loop

## Run Structure

- Open world 2D map — player roams freely between [encounters](../glossary.md#encounter)
- Enemies are present in the world (visible or random [encounters](../glossary.md#encounter) — TBD)
- World is split into [zones/regions](../glossary.md#zone--region) (TBD) or a single seamless map (TBD)
- Progression is infinite — [monster scaling](../glossary.md#monster-scaling) over time (scaling trigger: distance, time, or other — TBD)
- No fixed end goal; the [run](../glossary.md#run) is a "how far can you go" challenge (final boss option — TBD)

## Combat

- Turn-based card combat
- [Energy](../glossary.md#energy) system or other constraint — TBD
- [Hand](../glossary.md#hand) draw behavior (per-turn draw vs persistent hand) — TBD

## Between Fights

- Player returns to the open world map and roams freely
- No forced path — player chooses where to go next

## Save & Death

- Progress is saved at [rest sites](../glossary.md#rest-site) (fixed locations or anywhere — TBD)
- [Rest sites](../glossary.md#rest-site) are the checkpoint boundary
- The [run](../glossary.md#run) ends on a [party wipe](../glossary.md#party-wipe) — all [party](../glossary.md#party) members must die for the run to end
- On [party wipe](../glossary.md#party-wipe):
  - Some progress is **lost** (current [deck](../glossary.md#deck) builds, items, map progress — TBD)
  - Some progress is **kept** ([meta progression](../glossary.md#meta-progression) currency, unlocks, or other — TBD)
  - Player does not continue from last [rest site](../glossary.md#rest-site) — [run](../glossary.md#run) ends (or resets to last rest — TBD)
