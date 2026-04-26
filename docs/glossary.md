# Glossary

Reference for terms used across all design documents.

---

## Run
A single playthrough. The player builds their deck and progresses through the world. A run ends when the player reaches a final goal or victory condition — TBD.

## Rest Site
A safe location in the world where progress is saved. The only place the game can be saved. On [party wipe](glossary.md#party-wipe), the party reverts to the last rest site save — all progress since then is lost.

## Encounter
A single combat event triggered when the player contacts an enemy on the [world map](glossary.md#world-map). Each encounter has its own [area level](glossary.md#area-level) within its parent [region's](glossary.md#region) range. Completing an encounter yields [loot](glossary.md#loot). Enemy placement on the world map — TBD. See [Map & Structure](systems/map-and-structure.md).

## Bag
The shared card pool belonging to the whole [party](glossary.md#party). All acquired cards land here first. Accessible only outside of combat (at rest). Party members can transfer cards between the bag and their individual decks at rest.

## Deck
The individual collection of cards assigned to a single [party](glossary.md#party) member. Assembled from cards drawn out of the shared [bag](glossary.md#bag) during rest. Cards in the deck are drawn into the [hand](glossary.md#hand) during combat. Subject to a [deck limit](glossary.md#deck-limit).

## Deck Limit
The maximum number of cards a party member's deck can hold. The player has a fixed deck limit of 20; each companion has their own varying limit.

## Hand
The subset of cards drawn from a party member's [deck](glossary.md#deck) that are available to play on a given turn during combat. Hand size and draw behavior are TBD.

## Loot
Cards rewarded to the player after completing an [encounter](glossary.md#encounter). The primary way to grow a deck during a run. Loot goes into the shared [bag](glossary.md#bag).

## Initiative
A value carried by cards that determines turn order in combat. Participants act in descending initiative order across both sides. Tied values are broken by a random roll to pick which side goes first, then turns interleave by member position. Default initiative is 0 when no card provides one. See [Initiative](systems/initiative.md).

## Energy
(TBD) The resource spent to play cards each turn.

## World Level
The current difficulty baseline of the world. Starts at 1. Increases by 1 when the player completes a frontier dungeon (a dungeon whose base [area level](glossary.md#area-level) equals the current world level). Also snaps up to match [player level](glossary.md#player-level) if a master raises player level above it. Newly generated [regions](glossary.md#region) use world level as their base area level. See [Map & Structure](systems/map-and-structure.md).

## Player Level
The player character's current level. Raised exclusively by visiting a [master](glossary.md#master) in a city. Grants a benefit on each increase. If player level exceeds [world level](glossary.md#world-level) after a level-up, world level snaps up to match. The player starts every run at level 1.

## Master
An NPC found in [cities](glossary.md#city) who raises the [player's level](glossary.md#player-level) and grants a benefit in exchange. When player level after leveling up exceeds [world level](glossary.md#world-level), world level immediately snaps up to match. Master count per city, available benefits, and any cost or requirement are — TBD.

## Card Level
The level of a card, fixed at the moment it drops, equal to the [area level](glossary.md#area-level) of the [dungeon](glossary.md#dungeon) or [encounter](glossary.md#encounter) where it was found. Determines the card's raw stat values — higher level means more powerful stats. A card's level cannot change after it drops.

## Area Level
The difficulty level of a specific location ([dungeon](glossary.md#dungeon), [encounter](glossary.md#encounter), or [city](glossary.md#city)). Dungeons and encounters are generated at [world level](glossary.md#world-level) + 0–5; cities match world level exactly. Determines the level of enemies faced and cards that can drop. Attempting higher area level content yields better [loot](glossary.md#loot).

## Monster Scaling
The mechanic by which enemy stats scale with [area level](glossary.md#area-level). Since area level is anchored to [world level](glossary.md#world-level), enemies always present a relevant challenge. The specific stat formula is TBD.

## Meta Progression
Persistent unlocks or rewards that carry over between runs, surviving death.

## World Map
The single open 2D map the player roams freely during a [run](glossary.md#run). Contains all [regions](glossary.md#region), [dungeons](glossary.md#dungeon), [cities](glossary.md#city), and [encounters](glossary.md#encounter). See [Map & Structure](systems/map-and-structure.md).

## Region
A subdivision of the [world map](glossary.md#world-map) used to track the player's movement progress. Has no inherent theme or type — regions are purely structural. Generated on first entry at the current [world level](glossary.md#world-level). Contains [dungeons](glossary.md#dungeon), [cities](glossary.md#city), and [encounters](glossary.md#encounter). See [Map & Structure](systems/map-and-structure.md).

## Dungeon
A multi-room location on the [world map](glossary.md#world-map) containing exploration and combat. The main source of [loot](glossary.md#loot) within a [region](glossary.md#region). Has its own [area level](glossary.md#area-level) within its region's range. Room structure and exit rules — TBD. See [Map & Structure](systems/map-and-structure.md).

## City
A safe location on the [world map](glossary.md#world-map) that functions as a [rest site](glossary.md#rest-site). No combat occurs inside a city. The player can save, manage their [deck](glossary.md#deck), and recruit [companions](glossary.md#companion) here. Services beyond rest and recruitment — TBD. See [Map & Structure](systems/map-and-structure.md).

## Party
The group consisting of the player and up to 2 [companions](glossary.md#companion) — 3 members total. They travel and fight together.

## Companion
An NPC recruited during a run who follows the player on the map, has their own deck and HP, and takes their own turn in combat. Companions can be acquired or dismissed over the course of a run.

## Companion Deck Limit
A [companion's](glossary.md#companion) specific [deck limit](glossary.md#deck-limit). Varies per companion.

## Companion Quest Line
A series of events or objectives unique to a specific companion. Completing stages of a quest line rewards the companion with unique cards.

## Equipment Card
A passive card slotted into an [equipment slot](glossary.md#equipment-slot) before combat. Each party member has their own equipment slots. Provides stats or enables the use of [attack cards](glossary.md#attack-card).

## Equipment Slot
A dedicated slot on a party member where an [equipment card](glossary.md#equipment-card) can be placed before combat. Modelled similarly to Diablo-style item slots. See [Equipment](systems/equipment.md) for the full slot list.

## Attack Card
A combat card that performs an offensive action using the weapon equipped in the relevant [equipment slot](glossary.md#equipment-slot). Requires a compatible weapon to be equipped.

## Spell Card
A combat card that performs an offensive or utility action without requiring an equipped weapon.

## Consumable Card
A single-use combat card (e.g. potions). Behaviour after use — TBD.

## Party Wipe
The condition where all party members have been killed. The party reverts to the last [rest site](glossary.md#rest-site) save, losing all progress since then.

## The Ruin
The catastrophic event that collapsed civilization roughly 100 years before the game takes place. The exact cause is — TBD. See [The Ruin](world/the-ruin.md) for full detail.

## Wasteland
The post-collapse world the game takes place in — a landscape of ruins, toxic zones, and struggling survivor settlements left in the wake of [The Ruin](glossary.md#the-ruin).

## Save Slot
One of three independent save files available to the player. Each slot holds the complete world state of a single run, tracked separately from all other slots. Players can start a new run, load an existing run, or delete a slot from the slot selection screen. See [Save Slots](systems/save-slots.md).

## Slot Selection Screen
The screen shown on game launch and accessible from the main menu where the player chooses which [save slot](glossary.md#save-slot) to play.

## Faction
One of several organized groups competing for resources and territory across the [wasteland](glossary.md#wasteland). Factions may influence available [encounters](glossary.md#encounter), companions, and [loot](glossary.md#loot). See [Factions](world/factions.md).

## Durability
A float value (0.0–1.0) carried by every card representing how much use it has left. All cards start at 1.0. Each time the card is played or damaged, durability decreases by the card template's **durability on use** fraction. For example, a durability on use of 0.1 means the card lasts 10 uses before breaking. When durability reaches 0 the card is broken and cannot be used.
