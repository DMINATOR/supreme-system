# Map & Structure

> Facts, rules, behaviors, and relationships: [map-and-structure_spec.md](map-and-structure_spec.md)

## The World Map

The game takes place on a single open 2D [world map](../glossary.md#world-map) the player roams freely. It represents the [wasteland](../glossary.md#wasteland) roughly 100 years after [The Ruin](../glossary.md#the-ruin) — vast, dangerous, and only partially charted by any survivor.

The map is divided into [regions](../glossary.md#region) — structural subdivisions used to track the player's movement progress. Regions carry no inherent theme or type; their purpose is to segment the world into areas of increasing difficulty. The player has no fixed destination — the goal is to push as far and as deep into the world as possible, surviving longer and accumulating stronger cards with each [run](../glossary.md#run).

## Regions

The [world map](../glossary.md#world-map) is structured as a grid of [regions](../glossary.md#region) identified by integer X/Y coordinates. Regions are not pre-built — each one is generated the first time the player steps into it, then persisted for the rest of that [run](../glossary.md#run). The player crosses between regions seamlessly by walking across their borders on the open world map.

A region's base [area level](../glossary.md#area-level) equals the current [world level](../glossary.md#world-level) at the time of generation. This means the world scales with how far the player has proven they can go — not just how many times they have visited a city.

## Locations

Within each [region](../glossary.md#region) the player will find three types of location on the [world map](../glossary.md#world-map):

### Dungeon

A [dungeon](../glossary.md#dungeon) is a multi-room location with exploration and combat. Rooms are arranged in a branching structure — the player chooses their route through the dungeon. Room count is procedurally generated. Each dungeon has its own [area level](../glossary.md#area-level) in the range of the current [world level](../glossary.md#world-level) up to 5 levels higher. Higher-level dungeons yield better [loot](../glossary.md#loot), motivating players to take on harder content. A small reward drops per room cleared; a larger reward drops on full completion. A dungeon must be finished in one attempt — the player cannot exit once they have entered.

Completing a **frontier dungeon** — one whose base area level matches the current [world level](../glossary.md#world-level) — increases world level by 1, making newly generated regions more dangerous.

### City

A [city](../glossary.md#city) is a safe hub and the only type of location that acts as a [rest site](../glossary.md#rest-site). Entering a city never triggers combat. A city's [area level](../glossary.md#area-level) equals the current [world level](../glossary.md#world-level). Beyond rest and [companion](../glossary.md#companion) recruitment, cities offer card trading, card upgrades, card repairs ([durability](../glossary.md#durability)), and equipment trading and upgrades. Previously visited cities can be fast-travelled to from anywhere on the world map.

Cities contain [masters](../glossary.md#master) — NPCs who raise the [player's level](../glossary.md#player-level) and grant a benefit in exchange. If the player's level after leveling up exceeds the current [world level](../glossary.md#world-level), world level immediately snaps up to match — allowing masters to fast-track world scaling when the player outpaces their dungeon progress.

### Encounter

An [encounter](../glossary.md#encounter) is a single combat event triggered when the player contacts an enemy on the [world map](../glossary.md#world-map). Enemies appear as both visible patrols (which the player can spot and potentially avoid) and invisible random triggers. Each encounter has its own [area level](../glossary.md#area-level) in the range of the current [world level](../glossary.md#world-level) up to 5 levels higher.

## Progression

[World level](../glossary.md#world-level) is the engine of difficulty scaling. It starts at 1 and increases by 1 each time the player completes a frontier dungeon (a dungeon whose base area level matches the current world level). It also snaps up to match [player level](../glossary.md#player-level) if masters in cities push the player's level ahead of the world. Newly generated [regions](../glossary.md#region) always reflect the current world level, ensuring the challenge keeps pace with the player's demonstrated ability. The [run](../glossary.md#run) ends when the [party](../glossary.md#party) is wiped or a final dungeon or boss is reached and defeated — specifics TBD.
