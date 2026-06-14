# Dungeon RPG

A console-based dungeon crawler written in C#, developed for the **Object-Oriented Design** course at the Faculty of Mathematics and Information Science, Warsaw University of Technology. The project serves as a practical study of classical design patterns applied within a working game architecture.

---

## Table of Contents

- [Overview](#overview)
- [Design Patterns](#design-patterns)
- [Requirements](#requirements)
- [Getting Started](#getting-started)
- [Controls](#controls)
- [Gameplay](#gameplay)
- [Project Structure](#project-structure)
- [Architecture](#architecture)

---

## Overview

The player navigates a procedurally generated ASCII dungeon, engages enemies in combat, and manages a dual-wield inventory of weapons and spells. Each run is shaped by a theme-driven factory, producing a distinct layout, enemy composition, and item pool.

Key features:

- **Procedural dungeon generation** via Builder and Strategy patterns
- **Three distinct themes** — Castle, Jungle, and Crimson — each supplying unique enemies and weapons through an Abstract Factory
- **Multiple enemy types** — Such as: Goblin, Evil Knight, Armored Skeleton, and Possessed Armor — each with individual flocking behaviour
- **Full character sheet** — Health, Strength, Dexterity, Luck, Aggression, and Wisdom stats, plus Coins and Gold currency
- **Dual-wield equipment** — separate left-hand and right-hand weapon slots, each with its own damage value
- **Three attack modes** — Normal Attack, Stealth Attack, and Magic Attack, selectable each combat round
- **20-slot inventory** — carry weapons and spells and swap them mid-dungeon
- **Acoustic system** — dropping a weapon generates noise that attracts nearby enemies within range
- **Species network** — when an enemy is killed, its kin in the dungeon are alerted via an Observer hub
- **Timestamped event log** — combat events written to an in-memory log visible at the bottom of the screen, and optionally to a file on disk
- **JSON configuration** — player name, log file path, and other settings loaded from `game_settings.json`

---

## Design Patterns

| Pattern | Implementation |
|---|---|
| Builder | `DefaultDungeonBuilder` assembles terrain, enemies, and items into a complete room |
| Strategy | Terrain generation strategies (e.g. `DefaultTerrainStrategy`) are swapped at construction time |
| Abstract Factory | `IThemeFactory` and its three concrete factories supply theme-consistent enemies and weapons |
| Chain of Responsibility | Input handlers (`MovementInputHandler`, `CombatInputHandler`, `InventoryInputHandler`) form an ordered chain |
| Visitor | `NormalAttackVisitor` computes damage without coupling the `Player` and `Enemy` classes directly |
| Observer | `SpeciesNetwork` propagates death events across a species; `DungeonAcoustics` propagates sound events |
| Composite | `CompositeLogger` fans a single log call out to any number of attached loggers |
| Pattern Matching | `Weapon.GetNoiseRange()` resolves noise level using a `switch` expression over marker interfaces |

---

## Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/download) or later
- A terminal with UTF-8 support (Windows Terminal or iTerm2 recommended)

---

## Getting Started

```bash
git clone https://github.com/m00fi/dungeon-rpg-project.git
cd dungeon-rpg-project/rpg-project
dotnet run
```

Before running, you can customise `game_settings.json`:

```json
{
  "PlayerName": "Hero",
  "LogFilePath": "Logs"
}
```

---

## Controls

| Key | Action |
|---|---|
| W / A / S / D | Move up / left / down / right |
| E | Pick up item on the current tile |
| Q | Drop the currently held item |
| 1 / 2 / 3 | Select attack type during combat (Normal / Stealth / Magic) |
| J | Display the in-session event log |

Moving onto a tile occupied by an enemy initiates combat and prompts for an attack choice.

---

## Gameplay

![Gameplay screenshot](./screenshots/battle1.png)

---

## Project Structure

```
rpg-project/
├── Program.cs                       # Entry point; initialises config, logger, factories, and game loop
├── Game.cs                          # Main game loop: input reading, NPC movement, rendering
├── GameConfig.cs                    # Loads settings from game_settings.json
├── game_settings.json               # Runtime configuration file
│
├── Dungeon/                         # Map module
│   ├── Room.cs                      # 2D grid of Cell objects; drives enemy movement each tick
│   ├── Cell.cs                      # Base cell and subtypes: WallCell, EmptyCell, etc.
│   └── Generation/
│       ├── DefaultDungeonBuilder.cs # Builder that composes terrain, enemies, and items
│       └── Strategies/              # Pluggable terrain generation strategies
│
├── Entities/                        # Live objects on the map
│   ├── Entity.cs                    # Base class: HP, stats, position (X/Y), ASCII glyph
│   ├── Player.cs                    # Player character: dual-wield slots, inventory, currency
│   └── Enemies/
│       ├── Enemy.cs                 # Enemy base class; exposes flocking behaviour hooks
│       ├── Goblin.cs
│       ├── EvilKnight.cs
│       ├── ArmoredSkeleton.cs
│       └── PossessedArmor.cs
│
├── Items/                           # Collectible objects
│   ├── IItem.cs                     # Base item interface, including GetNoiseRange()
│   └── Weapons/
│       ├── Weapon.cs                # Weapon base class; noise level resolved via pattern matching
│       ├── IHeavyWeapon.cs          # Marker interface
│       ├── IMagicWeapon.cs          # Marker interface
│       ├── ILightWeapon.cs          # Marker interface
│       ├── Sword.cs
│       ├── Spear.cs
│       └── Greatsword.cs
│
├── Input/                           # Chain of Responsibility
│   ├── IInputHandler.cs
│   ├── MovementInputHandler.cs      # Handles W/A/S/D and wall collision
│   ├── CombatInputHandler.cs        # Detects enemy collision and dispatches a Visitor
│   └── InventoryInputHandler.cs     # Handles E (pick up) and Q (drop)
│
├── Combat/                          # Visitor pattern
│   ├── IAttackVisitor.cs
│   └── NormalAttackVisitor.cs       # Standard damage calculation
│
├── Themes/                          # Abstract Factory pattern
│   ├── IThemeFactory.cs
│   ├── CastleThemeFactory.cs
│   ├── JungleThemeFactory.cs
│   └── CrimsonThemeFactory.cs
│
├── Logging/                         # Composite + Strategy
│   ├── GameLogger.cs                # Static global wrapper
│   ├── ILogger.cs
│   ├── MemoryLogger.cs              # Retains timestamped entries for in-game display
│   ├── FileLogger.cs                # Writes entries to a .txt or .log file
│   └── CompositeLogger.cs           # Fans one log call out to multiple loggers
│
└── Systems/                         # Observer-based global systems
    ├── Factions/
    │   ├── ISpeciesSubject.cs
    │   ├── ISpeciesObserver.cs
    │   └── SpeciesNetwork.cs        # Hub: notifies all members of a species on a death event
    ├── Acoustics/
    │   ├── DungeonAcoustics.cs      # Hub: broadcasts noise events when items are dropped
    │   └── IAcousticObserver.cs
    └── Pathfinding/
        └── Pathfinder.cs            # BFS pathfinding that routes around walls
```

---

## Architecture

The project is developed across six iterative stages. The current state (after Stage 5) contains the complete game logic with creational, behavioural, and structural patterns in place. **Stage 6** refactors the codebase toward an **MVC** separation of concerns:

- `Game.cs` will be split into a pure model and a `ConsoleView` responsible for all rendering
- Physical key reading will move into a dedicated `PlayerController`
- **Data Transfer Objects (DTOs)** will be introduced to resolve the circular reference between `Player` and `Room` during JSON serialisation

```
+------------------+     input     +------------------+
|  PlayerController| ------------> |   Game (Model)   |
+------------------+               +--------+---------+
                                            |
                                            | game state
                                            v
                                   +------------------+
                                   |   ConsoleView    |
                                   +------------------+
```

---

*Academic project — MiNI WUT, Object-Oriented Design course.*
