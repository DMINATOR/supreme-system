# Save Slots

> Facts, rules, behaviors, and relationships: [save-slots_spec.md](save-slots_spec.md)

The save slot system gives the player three independent save files. Each slot holds the full state of one run — the world, the party, and all progress made. The player picks a slot at startup and can delete any slot to start fresh, without affecting the others.

Three slots was chosen as the standard minimum: enough for a player to keep a main run, try something different, or let a second player have their own file — without creating complexity around managing an arbitrary number of saves.

## Slot States

A slot is either **empty** or **in progress**. An empty slot shows a prompt to start a new run. An in-progress slot shows a brief summary of the current run and can be loaded to resume.

## Actions Available Per Slot

From the slot selection screen the player can:

- **New** — start a fresh run in an empty slot
- **Load** — resume an existing run from the slot's saved state
- **Delete** — permanently remove all data from the slot, returning it to empty

Deleting cannot be undone. A confirmation prompt is shown before the slot is cleared.

## World Progression Isolation

Each slot maintains its own world progression. Events, encounters, and map state from one slot have no effect on another. This applies to all in-run state: the map, the [party](../glossary.md#party), individual [decks](../glossary.md#deck), and the shared [bag](../glossary.md#bag).

[Meta progression](../glossary.md#meta-progression) is fully isolated per slot — unlocks and persistent rewards in one slot have no effect on any other slot.

## Saving

Progress is saved automatically at a rest site before the player leaves it — no manual step is required. The player can also force a save at any point while at a rest site. There is no save on demand outside of rest sites.

## Slot Selection Screen

The slot selection screen is the entry point to the game after the title screen. It is always shown on launch and can be reached from the main menu. The player must select a slot before entering gameplay. An in-progress slot displays the total time played across all sessions.
