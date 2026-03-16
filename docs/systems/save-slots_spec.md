# Save Slots — Spec

> Structured reference extracted from [save-slots.md](save-slots.md). Open questions: [save-slots_tbd.md](save-slots_tbd.md).

## Facts

- Number of save slots: 3
- Slot states: empty, in-progress
- Slots are selected from the slot selection screen on game launch and from the main menu
- Meta progression scope: per-slot — each slot has its own independent meta progression
- Slot summary display: time played across sessions

## Rules

- A new run can only be started in an empty slot
- Only an in-progress slot can be loaded
- Deleting a slot permanently removes all data in that slot — it cannot be recovered
- Only one slot can be active during a play session
- Progress is saved automatically at a rest site before the player leaves it
- The player can also manually force a save at a rest site
- Each slot holds world progression independently; slots do not share in-run state

## Behaviors

- Display the slot selection screen on game launch and from the main menu
- Start a new run when the player selects New on an empty slot
- Resume a run when the player selects Load on an in-progress slot
- Prompt for confirmation before deleting a slot
- Delete a slot and return it to empty state when deletion is confirmed

## Relationships

- A save slot contains one world state: map progress, party composition, individual decks, bag contents, and meta progression
- Save slots are accessed from outside gameplay (main menu / slot selection screen)
