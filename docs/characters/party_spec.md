# Party — Spec

> Structured reference extracted from [party.md](party.md). Open questions: [party_tbd.md](party_tbd.md).

## Facts

- Maximum party size: 3 (player + up to 2 companions)
- Player deck limit: [TBD](party_tbd.md)
- Each party member has their own HP pool

## Rules

- The player is always present and cannot be dismissed
- Companions can be dismissed at any time outside of combat
- A companion can die mid-combat without ending the run
- The run ends when every party member is dead (party wipe)
- Combat is turn-based — each member takes their turn sequentially
- Party roster can change freely over the course of a run

## Behaviors

- Add a companion to the party
- Dismiss a companion from the party
- Evaluate party wipe condition

## Relationships

- Party contains the player and up to 2 companions
- Party shares one bag
- Each party member owns one individual deck
- Each party member has their own HP pool
