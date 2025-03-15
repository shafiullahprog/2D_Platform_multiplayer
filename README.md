![image](https://github.com/user-attachments/assets/676c24a0-8471-4ad2-a5d6-aaa834977124)Game Documentation 

1. Procedural Level Generation  

The game generates levels dynamically using a procedural generation system. Platforms, obstacles, and the goal are placed at runtime to create a unique experience each time.  

Generation Process
1. Randomized Platform Placement 
   - A set number of platforms (numberOfPlatforms) are spawned.
   - The X-position is chosen randomly between minX and maxX.
   - The Y-position is incremented based on a randomly determined gap between startMinGapY and startMaxGapY.

2. Dynamic Difficulty Scaling  
   - As the player progresses upward, the platform gaps (currentMinGapY, currentMaxGapY) and obstacle spawn chance increase.  
   - The total number of platforms is dynamically adjusted to fit within the vertical space allocated for level generation. The total number of platforms is dynamically adjusted to fit within the vertical space allocated for level generation.

3. Obstacle Placement Rules  
   - Obstacles should not appear on every platform (40% chance per platform).    
   - The position of each obstacle is slightly randomized to ensure variety:

4. Goal Placement
   - The last platform is marked as the goal platform.  
   - The goal is instantiated slightly above and to the right of this platform.

2. Real-Time Multiplayer Flow (Photon PUN Integration)

Multiplayer Architecture  
The game uses Photon PUN (Photon Unity Networking) for real-time synchronization. The gameplay is multiplayer-based, where two players compete in the same procedurally generated level.

Matchmaking Process
1. Connecting to Photon Network
   - Players connect to Photon servers.
   - If a room exists, they join; otherwise, a new room is created.

2. Spawning Players
   - Players are instantiated at different spawn points to prevent overlap  

3. Synchronizing the Procedural Level
   - The Master Client generates the level and sends the seed to the other client using an RPC (Remote Procedure Call):
     
4. Player Movement Synchronization  
   - Each player's movement is updated locally, but the position is synchronized over the network for both the players.  
   - To reduce lag, movement is interpolated using Photon’s PhotonTransformViewClassic or manual smoothing.

5. Lag Compensation & Smoothing 
   - Players might experience lag due to network delay.  
   - Instead of directly updating positions, interpolation and extrapolation techniques are used to predict movement.

3. Game Mechanics & Controls

Controls (Touch Input for Mobile)
| Action      | Input                          |  
|-------------|--------------------------------|  
| Move Left   | Swipe left                     |  
| Move Right  | Swipe right                    |  
| Jump        | Swipe up / Tap                 |

Win Conditions
- The first player to reach the goal platform wins the game.
