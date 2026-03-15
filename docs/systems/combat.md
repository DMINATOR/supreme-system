# Combat

> Facts, rules, behaviors, and relationships: [combat_spec.md](combat_spec.md)

Combat is turn-based. The entire [party](../glossary.md#party) fights together against a group of enemies. A fight continues until there are no active participants left on one side.

## Turn Structure

Each participant — party member or enemy — acts individually based on their [initiative](../glossary.md#initiative). The participant with the highest initiative acts first, regardless of which side they belong to. Turns then proceed in descending initiative order.

## Starting a Fight

Who acts first is determined by initiative. The encounter type and starting conditions can modify initiative values — for example, an ambush may reduce the party's initiative, letting enemies act first.

## Sides

- The party side is the player and all active [companions](../glossary.md#companion)
- The enemy side is a group of 1 to 5 enemies

## Ending a Fight

Combat ends when one side has no active participants remaining. If the party wins, [loot](../glossary.md#loot) is awarded and the player returns to the world map. If the party loses, this results in a [party wipe](../glossary.md#party-wipe) and ends the [run](../glossary.md#run).
