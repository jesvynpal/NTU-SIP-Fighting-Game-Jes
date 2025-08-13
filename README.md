# FightStyleJes - 2D Fighting Game

A 2D side-scrolling fighting game built with Unity during my student internship program at Temasek Polytechnic.

## Project Overview

This is a Unity-based 2D fighting game where players control a character through various levels, fighting enemies and collecting coins. The game features:

- **Player Character**: With movement, jumping, attacking, and health mechanics
- **Enemy AI**: State-based AI system with patrol, idle, and melee combat states
- **Combat System**: Melee attacks with collision detection and damage handling
- **Level System**: Multiple scenes with scene transitions
- **Health & Coin System**: Player health management and coin collection
- **Sound Management**: Audio system for game effects and music

## Project Structure

### Core Scripts

- **`Player.cs`**: Main player controller with movement, combat, and health management
- **`Character.cs`**: Base abstract class for all characters (player and enemies)
- **`Enemy.cs`**: Enemy AI controller with state machine implementation
- **`GameManager.cs`**: Game state management, coin system, and scene handling
- **`CameraFollow.cs`**: Camera following system for the player

### Enemy AI System

The enemy AI uses a state pattern with the following states:
- **`IEnemyState.cs`**: Interface defining enemy state behavior
- **`IdleState.cs`**: Enemy idle behavior
- **`PatrolState.cs`**: Enemy patrol movement
- **`MeleeState.cs`**: Enemy melee combat behavior

### Animation System

- **`AttackBehaviour.cs`**: Controls attack animations and timing
- **`DamageBehaviour.cs`**: Handles damage animation states
- **Various animation controllers** for player and enemy movements

### UI & Menus

- **`MainMenu.cs`**: Main menu functionality
- **`PauseMenu.cs`**: In-game pause menu
- **`GameOver.cs`**: Game over screen handling
- **`BarScript.cs`**: Health bar display system

## Features

### Player Mechanics
- **Movement**: Left/right movement with smooth animations
- **Jumping**: Ground detection and jump mechanics
- **Combat**: Melee attacks with sword collision detection
- **Health System**: Health management with damage immunity frames
- **Coin Collection**: Coin pickup and persistent storage

### Enemy System
- **AI States**: Intelligent behavior switching between patrol, idle, and combat
- **Line of Sight**: Enemy detection and targeting system
- **Combat AI**: Melee range detection and attack coordination
- **Health Management**: Enemy health bars and death handling

### Game Systems
- **Scene Management**: Multiple levels with persistent player data
- **Save System**: Player health and coin persistence between levels
- **Sound Management**: Audio system for game events
- **Collision System**: Precise collision detection for combat

## Controls

- **WASD/Arrow Keys**: Movement
- **Space**: Jump
- **Left Shift**: Attack
- **Escape**: Pause menu

## Technical Details

- **Unity Version**: Compatible with Unity 2021.3 LTS and later
- **Platform**: Windows (build included)
- **Architecture**: Component-based design with state pattern for AI
- **Performance**: Optimized for smooth 60fps gameplay

## Build Information

The project includes a Windows build (`FightStyleJes.exe`) that can be run directly without Unity.

## Development Notes

- Uses Unity's built-in 2D physics system
- Implements custom state machine for enemy AI
- Follows Unity best practices for component design
- Includes comprehensive animation system with behavior scripts

## Getting Started

1. Open the project in Unity 2021.3 LTS or later
2. Open the main scene from `Assets/Levels/`
3. Press Play to test the game
4. Use the build settings to create your own executable

## About the Developer

**Built by: Jes**  
**Institution: Temasek Polytechnic**  
**Program: Student Internship Program**  
**Project Type: Unity 2D Game Development**

This project was developed as part of my learning experience at Temasek Polytechnic, focusing on game development principles, Unity engine usage, and software engineering practices.

## Contact

For questions, feedback, or collaboration opportunities, please contact me directly.

---

*This project represents the culmination of my studies and practical application of game development concepts learned during my time at Temasek Polytechnic.*
