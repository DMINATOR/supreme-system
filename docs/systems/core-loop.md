# Core Loop

> Facts, rules, behaviors, and relationships: [core-loop_spec.md](core-loop_spec.md)

## Run Structure

- Open world 2D map — player roams freely between [encounters](../glossary.md#encounter)
- Enemies are present in the world (visible or random [encounters](../glossary.md#encounter) — TBD)
- World is split into [zones/regions](../glossary.md#zone--region) (TBD) or a single seamless map (TBD)
- Progression is infinite — enemies and card drops grow stronger as the player moves into higher [area level](../glossary.md#area-level) zones ([monster scaling](../glossary.md#monster-scaling))
- No fixed end goal; the [run](../glossary.md#run) is a "how far can you go" challenge (final boss option — TBD)

## Combat

- Turn-based card combat
- [Energy](../glossary.md#energy) system or other constraint — TBD
- [Hand](../glossary.md#hand) draw behavior (per-turn draw vs persistent hand) — see [hand_tbd.md](hand_tbd.md)

## Between Fights

- Player returns to the open world map and roams freely
- No forced path — player chooses where to go next

## Save & Death

- The game can only be saved at [rest sites](../glossary.md#rest-site) — fixed locations or anywhere (TBD)
- On [party wipe](../glossary.md#party-wipe), the party reverts to the last rest site save — all progress since that save is lost
- Cards, loot, and map progress acquired since the last rest site do not persist through death
