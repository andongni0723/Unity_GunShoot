# GunShoot

![gunshoot-github-main-img](https://hackmd.io/_uploads/rJLeOXdTR.jpg)


## Game Description
This project is a top-down shooting game made by me using **Unity2022.3.0**. In the game, you can choose one of three characters, each with unique skills and different base stats.

After starting the game, you need to find a console to purchase your main weapon. The goal is to kill enemies or reach the exit while using as little health and money as possible. Your final score is based on the health used, money spent, and the number of enemies defeated. Have fun!

# Tech Stack 
- **Unity**: Game development platform.
- **[DoTween](https://dotween.demigiant.com/)**: A plugin about animation engine for Unity
- **[Odin inspector](https://odininspector.com/)**: A plugin that uses Attributes to easily draw editor UI
- **[NavMeshPlus](https://github.com/h8man/NavMeshPlus)**: A plugin that use of NavMesh in Unity 2D. In this project, it is used for enemy navigation towards the player.


# How to Play
> There is a bug in the game where the enemy may not spawn properly in the scene.
> Please press the gray gun icon button at the top left to quit the game.
> I apologize for the inconvenience, and I will fix the bug after I find the reason.

1. Open the game.
2. Press the "角色" button at the bottom left to choose your character.
3. Press the "開始遊戲" button at the bottom right to start the game.
4. Use the following controls:
   - `WASD`: Move
   - `C`: Switch weapon
   - `R`: Reload
   - `Q`: Perform actions like opening doors, accessing the console (store), or entering the exit
   - `Left Mouse Button`: Shoot
   - `F`: Use skill
   - `V`: Heal HP
   - `Tab`: Open the panel
5. Follow the mission points on the screen to find the console, where you can buy a main weapon, skills, and healing items.
6. Defeat enemies or find the exit to complete the mission.

# Installation

### Download the game 
Go to the **Release** Page download the game.

### Clone Repository
1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/Unity_GunShoot.git
   ```
2. Open the project in **Unity (version 2022.3.0 or later)**.
3. Ensure the Hierarchy includes the following three scenes: **Persistent**, **GameScene**, and **StartScene**.
4. Make sure the **Persistent Scene** is open.
