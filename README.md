![Game Screenshot](https://img.itch.zone/aW1hZ2UvNTMxNjI2OS8zMTk0MDk5Ny5wbmc=/794x1000/aYtQdE.png)
# Unity Slot Machine Game

### [Play on Itch.io](https://samicode-games.itch.io/slot-machine-game)

A 2D slot machine game developed using **Unity and C#** as part of Underpin  game development internship assessment.

The game features animated reels, randomized outcomes, configurable betting options, a wallet system, and a combination-based payout system.

## Game Overview

The objective is to spin the slot machine and match predefined symbol combinations to win rewards.

### Features

- **Animated Reels:** Three independently animated reels with smooth spinning and staggered stopping times.
- **Randomized Outcomes:** Reel results are generated using Unity's random number generation.
- **Interactive Lever:** Pull the lever to initiate a spin.
- **Bet Selection:** Choose between three configurable betting amounts.
- **Wallet System:** Starting balance, bet deductions, and winnings are managed through a wallet system.
- **Combination-Based Payouts:** Different symbol combinations offer different payout multipliers.
- **Insufficient Balance Handling:** Players cannot start a spin if they cannot afford the selected bet.
- **Game Over Detection:** The game detects when the remaining balance is insufficient for every available bet.

## How to Play

1. Launch the game.
2. Select one of the available betting amounts.
3. Click the lever to start spinning.
4. Wait for all three reels to stop.
5. Match a configured winning combination to receive a payout.
6. Continue playing while you have sufficient balance.

The payout is calculated using:

**Payout = Bet Amount × Combination Multiplier**

For example, a 20G bet with a 10x multiplier awards 200G.

## Instructions to Run the WebGL Build Locally

1. Download or clone this repository.
2. Navigate to the WebGL build folder.
3. Locate the `index.html` file.
4. Open a terminal in that folder.
5. Start a local HTTP server:

**Windows:**

```bash
py -m http.server 8000
```

**Linux / macOS:**

```bash
python3 -m http.server 8000
```

6. Open your browser and visit:

```text
http://localhost:8000
```

7. Wait for the Unity WebGL game to load.

**Note:** Unity WebGL builds should be served through a local web server rather than opened directly using `file://`. Use a modern browser with WebGL support.

## ⭐ Bonus Feature: Combination-Based Payout System

Instead of rewarding players only for matching three identical symbols, the game supports multiple configurable winning combinations.

Each combination contains:

- A combination name.
- An ordered sequence of three symbols.
- A payout multiplier.

Example payout configuration:

| Symbol Combination       | Multiplier |
| ------------------------ | ---------: |
| Seven – Seven – Seven    |        10x |
| Cherry – Cherry – Cherry |         7x |
| Cherry – Seven – Seven   |         6x |
| Seven – Cherry – Bell    |         5x |
| Bell – Bell – Bell       |         5x |
| Bell – Seven – Bell      |         4x |
| BAR – BAR – BAR          |         3x |
| Cherry – BAR – Seven     |         2x |

The combinations and their multipliers can be configured through the Unity Inspector without modifying the payout calculation code.

This makes it easier to introduce new winning combinations and adjust the game's reward balance.

## Thought Process & Development Approach

The main goal was to build a functional slot machine with a clean separation between game logic, visual presentation, and UI interaction.

### 1. Modular Architecture

The implementation separates responsibilities into different components:

- **SlotMachineController:** Controls the spin lifecycle, generates reel results, tracks reel completion, and determines when a spin is complete.
- **ReelController:** Handles individual reel animations and visual symbol transitions.
- **WalletManager:** Manages the player's balance, deductions, and rewards.
- **BetManager:** Stores available betting amounts and the currently selected bet.
- **PayoutManager:** Matches reel results against configured winning combinations and calculates rewards.
- **GameUI:** Handles UI updates, button interactions, and presentation.

### 2. Predetermined Reel Results

The winning outcome is calculated before the reel animations begin.

The `SlotMachineController` generates the results and passes the corresponding final symbols to each `ReelController`.

This ensures that reel animations are purely visual and cannot change the predetermined outcome.

### 3. Event-Driven Communication

C# events are used to communicate important game events, such as:

- Spin started.
- Spin completed.
- Balance changed.
- Bet changed.
- Insufficient funds.

This reduces unnecessary dependencies between different game components.

### 4. Configurable Payouts

The payout system uses serializable combination data rather than hardcoding individual winning patterns.

This allows winning combinations and multipliers to be modified through the Unity Inspector, making the system easier to maintain and extend.

### 5. Development Priorities

Development was approached incrementally:

1. Build the slot machine layout and UI.
2. Implement reel movement and spinning animations.
3. Add lever interaction.
4. Implement betting and wallet management.
5. Connect randomized reel results.
6. Implement configurable winning combinations and payouts.
7. Integrate UI feedback and game-over handling.
8. Add sound effects.
9. Test the complete game flow.
10. Add main menu.

## Technologies Used

- **Game Engine:** Unity
- **Programming Language:** C#