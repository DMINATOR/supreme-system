# Glossary

Reference for terms used across all design documents.

---

## Run
A single playthrough from start to death or victory. The player builds their deck and progresses through the world during a run.

## Rest Site
A safe location in the world where progress is saved. Acts as a checkpoint boundary between death consequences.

## Encounter
A combat event triggered by interacting with or being found by an enemy on the world map.

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

## Energy
(TBD) The resource spent to play cards each turn.

## Monster Scaling
The mechanic by which enemy stats and difficulty increase over the course of a run. Scaling trigger (distance, time, etc.) is TBD.

## Meta Progression
Persistent unlocks or rewards that carry over between runs, surviving death.

## Zone / Region
(TBD) A distinct area of the open world map, potentially themed or difficulty-tiered.

## Party
The group consisting of the player and up to 2 [companions](glossary.md#companion) — 3 members total. They travel and fight together. The run ends when all party members are dead.

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
The condition where all party members have been killed, ending the current run.

## The Ruin
The catastrophic event that collapsed civilization roughly 100 years before the game takes place. The exact cause is — TBD. See [The Ruin](world/the-ruin.md) for full detail.

## Wasteland
The post-collapse world the game takes place in — a landscape of ruins, toxic zones, and struggling survivor settlements left in the wake of [The Ruin](glossary.md#the-ruin).

## Faction
One of several organized groups competing for resources and territory across the [wasteland](glossary.md#wasteland). Factions may influence available [encounters](glossary.md#encounter), companions, and [loot](glossary.md#loot). See [Factions](world/factions.md).
