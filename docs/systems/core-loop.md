# Core Loop

> Facts, rules, behaviors, and relationships: [core-loop_spec.md](core-loop_spec.md)

## Run Structure

- Single open 2D [world map](../glossary.md#world-map) — player roams freely between [encounters](../glossary.md#encounter), [dungeons](../glossary.md#dungeon), and [cities](../glossary.md#city)
- Enemy placement on the world map (visible patrols vs. random triggers) — TBD
- [World map](../glossary.md#world-map) is divided into [regions](../glossary.md#region) — structural subdivisions used to track movement progress
- Progression is open-ended — enemies and card drops grow stronger as the player moves into higher [area level](../glossary.md#area-level) regions ([monster scaling](../glossary.md#monster-scaling))
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
