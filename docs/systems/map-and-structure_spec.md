# Map & Structure — Spec

## Facts

- The game world is a single open 2D map the player roams freely.
- The world map is a grid of regions identified by integer X/Y coordinates.
- Regions are structural subdivisions with no inherent theme or type — used to track player movement progress.
- The world map grid spans the full signed integer range on both axes (int min/max); the player cannot move beyond those bounds.
- Regions are not pre-created; a region is generated on the player's first entry into that X/Y coordinate.
- Region content is randomly generated on first entry and then persisted for the remainder of that run.
- A region's base area level equals the current world level at the time of generation.
- World level starts at 1.
- World level increases by 1 when the player completes a dungeon whose base area level equals the current world level (frontier dungeon).
- If the player's level exceeds the current world level (e.g. after leveling up at a master), world level immediately snaps up to match player level.
- Enemy density (number of patrols and random encounter triggers) is determined by region properties.
- Dungeon and city frequency per region is determined by region properties.
- The full set of region properties and their formulas are TBD.
- Each region contains three location types: dungeons, cities, and encounters.
- Each dungeon has its own area level in the range [world level, world level + 5].
- Each encounter has its own area level in the range [world level, world level + 5].
- Cities have an area level equal to the current world level.
- Cities function as rest sites — the only place the game can be saved.
- Cities contain masters — NPCs who can increase the player's level and grant a benefit in exchange.
- The player starts at level 1.
- Cities offer: rest (save), companion recruitment, card trading, card upgrades, card repairs (durability), equipment trading/upgrades, and leveling up via masters.
- Previously visited cities can be fast-travelled to.
- Dungeon rooms are arranged in a branching structure; the player chooses their route.
- Dungeon room count is procedurally generated.
- The run has a final goal — a final dungeon or boss — TBD specifics.

## Rules

- The player can only save the game inside a city (rest site).
- On party wipe, the party reverts to the last rest site save; all progress since is lost.
- Enemy and loot scaling inside a dungeon or encounter uses that location's own area level (world level + 0–5).
- Higher area level locations yield better loot rewards, motivating players to attempt harder content.
- Entering a city never triggers combat.
- The player roams the world map freely with no forced path.
- Region borders are seamless — the player crosses them by walking on the open world map.
- A dungeon must be completed in one attempt; the player cannot exit once they have entered.
- Abandoning a dungeon mid-attempt forfeits any uncollected loot from incomplete rooms.

## Behaviors

- Player moves freely across the world map.
- Player crosses into a new region by walking across its seamless border; the region is generated on first entry.
- Player enters a dungeon by interacting with it on the world map.
- Player enters a city by interacting with it on the world map.
- Player triggers an encounter by contacting a visible enemy patrol or an invisible random trigger on the world map.
- Completing a dungeon room awards a small loot reward scaled to that dungeon's area level.
- Completing a dungeon in full awards a larger loot reward scaled to that dungeon's area level.
- Completing an encounter awards loot scaled to that encounter's area level.
- Completing a frontier dungeon (area level == world level) increases world level by 1.
- Player levels up by visiting a master in a city; leveling grants a benefit and raises the player's level.
- When player level exceeds world level, world level snaps up to match player level.
- Resting in a city saves the game and grants access to city services.
- Player can fast-travel to any previously visited city.

## Relationships

- World Map is a grid of Regions identified by integer X/Y coordinates.
- Each Region contains Dungeons, Cities, and Encounters.
- Dungeons and Encounters each have an area level in the range [world level, world level + 5].
- Cities are rest sites and have an area level equal to the current world level.
- Encounters yield Loot on completion.
- Dungeons yield Loot per room (small) and on full completion (large).
