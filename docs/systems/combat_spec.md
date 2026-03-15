# Combat — Spec

> Structured reference extracted from [combat.md](combat.md). Open questions: [combat_tbd.md](combat_tbd.md).

## Facts

- Combat type: turn-based
- Turn order: individual initiative — each participant acts in descending initiative order, regardless of side
- Party side: all active party members (player + companions)
- Enemy group size: 1 to 5
- Initiative source: [initiative](initiative.md) is a property of cards, not a fixed character stat
- Initiative modifiers: applied by encounter type and starting conditions ([TBD](combat_tbd.md) specifics)
- Each turn: the active participant draws cards from their deck into their hand and plays them

## Rules

- The entire party participates together against the enemy group
- Turn order is determined by initiative — party members and enemies are interleaved
- Encounter type and conditions can modify initiative values at the start of combat
- Each participant draws cards and plays on their individual turn
- Combat continues until one side has no active participants remaining
- If all party members are no longer active, the run ends (party wipe)
- If the party wins, loot is awarded and the party returns to the world map

## Behaviors

- Start combat (calculate initiative for all participants, apply encounter modifiers)
- Take a turn (draw cards into hand, play cards)
- End turn (next participant by initiative acts)
- End combat (one side has no active participants remaining)
- Award loot on party victory

## Relationships

- Combat involves one party and one enemy group
- Turn order is determined by individual initiative across both sides — see [initiative_spec.md](initiative_spec.md)
- Encounter type applies initiative modifiers at combat start
- Party wipe ends the run
- Victory feeds loot into the bag
