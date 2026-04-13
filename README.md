# Fountain of Objects

This is a small console adventure game written in C#. It is based on the challange Fountain of Objects from the book The C# Player's Guide by RB Whitaker.  

You explore a dark 4x4 cavern, locate the Fountain of Objects, activate it, and return to the entrance alive.

## Objective

To win the game:

1. Start at the entrance `(Row=0, Col=0)`.
2. Reach the fountain room and run `enable fountain`.
3. Return to the entrance while the fountain is enabled.

You lose immediately if you enter a room containing a pit or an Amarok.

## Gameplay Overview

- The cave is mostly dark; each turn shows your current coordinates and room feedback.
- You get sensory warnings when dangerous rooms are nearby:
	- **Pit nearby**: “You feel a draft…”
	- **Maelstrom nearby**: “You hear the growling and groaning…”
	- **Amarok nearby**: “You can smell the rotten stench…”
- You have a limited number of arrows (starts at `5`) and can shoot into adjacent rooms.

## Commands

### Movement

- `move north`
- `move south`
- `move east`
- `move west`

### Fountain Actions

- `enable fountain`
- `disable fountain`

### Combat

- `shoot north`
- `shoot south`
- `shoot east`
- `shoot west`

### Help

- `help` opens an in-game command guide.

## Hazards and Rooms

- **Starting room**: safe, and the only room with visible light.
- **Fountain room**: activate the fountain here.
- **Pit room**: instant death.
- **Amarok room**: instant death.
- **Maelstrom room**: does not kill directly, but relocates both you and the maelstrom.
- **Empty room**: no direct effect.

## How to run the game

From the project root:

```bash
dotnet run --project FountainOfObjects.csproj
```

Or build first:

```bash
dotnet build
dotnet run
```

## Project Structure

- `src/Program.cs` – entry point.
- `src/Core/Game.cs` – main game loop, win/loss flow, turn rendering.
- `src/Core/World.cs` – map, room placement, adjacency checks, maelstrom resolution.
- `src/Entities/Player.cs` – movement, shooting, actions, player state.
- `src/Rooms/*` – room types and their descriptions.
