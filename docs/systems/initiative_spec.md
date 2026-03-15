# Initiative — Spec

> Structured reference extracted from [initiative.md](initiative.md). Open questions: [initiative_tbd.md](initiative_tbd.md).

## Facts

- Initiative source: cards (equipment cards before combat, combat cards during)
- Default initiative when no card provides it: 0
- Turn order: descending initiative value across all participants from both sides
- Initiative modification: possible during combat by playing cards with initiative effects

## Rules

- Equipment cards set a participant's initiative before combat begins
- Participants act in descending initiative order, interleaved across both sides
- When two or more participants share the same initiative value, a random roll decides which side acts first among the tied group, then turns interleave by member position (A1 → B1 → A2 → B2…)
- If no initiative cards are equipped, all participants are 0 and the tie-breaking rule applies to the whole combat
- Combat cards can modify the initiative of any individual participant or an entire party
- Initiative cards can target a single participant or the entire party
- The duration of an initiative modification is defined by the card that applied it
- Encounter conditions can apply initiative modifiers at combat start (see [combat_spec.md](combat_spec.md))

## Behaviors

- Calculate initiative for each participant at combat start (from equipped cards + encounter modifiers)
- Build turn order in descending initiative
- Apply tie-breaking (random roll + positional interleave) for participants sharing the same value
- Modify a participant's (or party's) initiative via a played card
- Re-order turn sequence after an initiative modification

## Relationships

- Initiative modifications are properties of cards (equipment cards before combat, combat cards during)
- Initiative feeds into the combat turn order
- Encounter type can apply modifiers to initiative at combat start
