# Toronto Adventure RPG

## Overview

"Toronto Adventure RPG" is a C# WPF-based game where players embark on an exciting journey through various locations in Toronto. Players can navigate through the city, encounter monsters, complete quests, trade items, and level up their characters. The game combines elements of adventure, strategy, and RPG mechanics to create an engaging experience.

## Features

- **Navigation**: Move between different locations using directional buttons.
- **Combat System**: Encounter monsters and engage in battles using weapons. Each attack inflicts a random amount of damage.
- **Health Management**: Players can die from monster attacks, resulting in a return to the starting location, "Home," with full health restored.
- **Experience and Leveling**: Defeating monsters grants experience points (XP) and gold. Accumulating enough XP allows players to level up, increasing their hit points.
- **Quests**: Complete quests by bringing specific items to earn rewards such as XP, gold, and weapons.
- **Trading System**: Trade items for gold or purchase items using gold at designated locations.
- **User Interface**: A user-friendly UI displays player stats, inventory, quests, and current location details.

## Project Structure

- **Engine Project**: Contains factory classes for populating game objects (Items, Monsters, Quests, Traders, and World). It also includes model classes and a ViewModel class that handles game logic and user interactions.
- **UI Project**: Features the main game window with sections for user stats, navigation, logs, inventory, quests, and current location details. Includes additional screens for quest details and trading.
- **EngineTester Project**: Contains unit tests to ensure the functionality of the game engine, including player movement and session data validation.

## Getting Started

### Prerequisites

- .NET SDK 8.0.4
- Visual Studio 17.11.1

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/savocc/TorontoAdventureRPG.git
