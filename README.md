# CS6457 Project: Team Noobs README
## Working Title: The Legend of Helga: Death of the Mild (Canon)
**Updated:** 4.24.22  
**Version:** Final

Trailer Video:: https://drive.google.com/file/d/18-s1FULQe8xhfJYEaxstKMCYA3360THJ/view?usp=sharing

Gameplay Video: https://drive.google.com/file/d/1rmByqjyhqBB4piYdoYNQ_IaPnHca7onC/view?usp=sharing

### 1. Starter Scene
- **MainMenu**

### 2. How to Play: Basic Controls
- **WASD:** Move
- **Mouse Move:** Camera
- **Mouse Click:** Attack
- **Space bar:** Jump (Hold = Higher)
- **Left Shift:** Roll
- **E:** Interact
- **F:** Target Lockon
- **P:** Pause
- **ESC:** Menu
- **1:** Fist
- **2:** Sword (Reward)
- **3:** Bow (Reward)

### 3. Gameplay and Associated Technology Requirements
#### 3.1. Number of Levels
- Our game has four levels in total: BeginningIsland, FireContinent, WindContinent, and BossContinent.

#### 3.2. Gameplay Options
- The player starts out on the beginning island once the game starts. We have three gates/portals to transport the player to other levels. There are also colliders to teleport back to the beginning island from the other continents. The game ends when the player beats the final boss in the BossContinent. In our current plan, the player can visit continents in any order they want. However, when the game starts, the player is not given any weapons to use aside from their fists, which makes it difficult to beat the boss. It is recommended that the player visit the other two continents first to collect weapons before going into the boss fight. The rewards can be picked up from chests at the top of the summit in the WindContinent or in the middle of a maze guarded by strong enemies in the FireContinent.
- We want to mention that the order of visiting different levels can change the gameplay. In the Fire/Wind continents, we put shortcuts as an option to reach the respective destination if the player collects the weapon from the other area. We think this makes exploring the world more fun once players find out the dependency between different levels.

#### 3.3. Details for Each Level
##### 3.3.1. BeginningIsland
- Player starts the game in the BeginningIsland. In this area, we show the player how to interact with the game world, including:
  - **Visual interaction with the 3D world.** The player can move the camera to look around.
  - **Animation controlled, physics enabled character control.**
  - **NPC interaction and Animation.** There is one NPC in the beginning island who provides information on the basic movement control to the player. The interaction between the player and the NPC is triggered by the player's keyboard input.
  - **Clear goals/subgoals communicated to the player.** The NPC provides information about the goal of the game (beat the boss) and the story to the player. There are two signs that the player can read for additional hints about what lies beyond each gate.
  - **Proper interaction with the world through physics simulation.** We put a dummy enemy (a Weeble Wobble) for the player to try the action set for attacks. The interaction between the player and the dummy enemy is controlled by both trigger based collision (hit detection) and physics simulation (rigidbody based onHit movement of the dummy enemy).
  - **Digital Audio.** There is background music and sounds for all levels, menus, and credit scenes. There are also player sound effects for walking, jumping, punching, swinging, bow releasing, sword hit on impact, arrow hit on impact, hurt sound and on death using animation unity events and the audio event manager schema.
  - **Cutscenes.** To provide additional story and guidance, the beginning island loads a cutscene when the game starts, showing an aerial view of the island, the three gates, and the NPC that the player can interact with. Each area also contains their respective cutscenes to give a preview of the area.
  - **In-game polish.** We added symbolic objects onto the gates leading to the other levels, such as a sword on the red gate, arrows on the green gate, and bones under the orange gate. We hope these visual cues can help players know what game contents they are supposed to encounter for other levels.
  - **Terrain.** The ground terrain is synthesized. There are invisible walls to prevent the player from falling off the map and trigger colliders to help the player respawn from the last activated checkpoint.
  - **P.S.** The red gate leads to the FireContinent. The green gate leads to the WindContinent. The orange gate in the forest behind the house at the starting point leads to the boss area. All these can be seen from the cutscene upon starting the game.
  
##### 3.3.2. FireContinent
- **Purpose:** Helps players get familiar with the basic fighting system.
- **Goal:** Reach the destination depicted by the wooden chest in the center of a maze, collect the fire sword, and unlock the teleport (golden crown) back to the beginning island.
- **3.3.2.a. Defined boundary:** A maze defines the level's boundary, surrounded by mountains that restrict player movement.
- **3.3.2.b. Communication of goals:** Two information boards provide game goals; one at the red gate and another at the maze's entrance, detailing the challenges and rewards.
- **3.3.2.c. Decision making during gameplay:** The maze design forces decision-making to navigate towards the wooden chest. Stronger yellow-headed enemies guard the chest, and health potions are placed strategically to aid in survival and encourage exploration.
- **3.3.2.d. Non-boss enemy AI:** Enemies are controlled by a NavMesh agent and a procedural-based finite state machine, adjusting their behavior based on player visibility and proximity.
- **3.3.2.e. AI movement controlled by animation:** Enemy movements are managed by an animation controller integrated with the finite state machine.
- **3.3.2.f. Interaction with objects and gameplay:** The wooden chest has animated poses (Open, Closed, Half Open, Half Closed) to enhance the item collection experience.

##### 3.3.3. WindContinent
- **Purpose:** Designed as a puzzle-style jumping mini-game to create engaging gameplay.
- **Goal:** Reach the summit of the mountain, grab the treasure in the chest, and exit using the golden crown.
- **3.3.3.a. Interaction with objects through physics simulation:** Players navigate a series of moving platforms with rigidbody-based collisions.
- **3.3.3.b. Goals communicated to players:** Similar to 3.3.2.b.
- **3.3.3.c. Defined boundary:** Similar to 3.3.2.a.
- **3.3.3.d. Basic elements:** Includes static platforms, moving platforms, phantom platforms (timed disappearance), and a blue-orange tornado pair for transportation within the puzzle.
- **3.3.3.e. Decision making:** Players choose from three paths to reach the summit: Courage path (platform jumping), Trail of wisdom (puzzle solving), and a shortcut.
- **3.3.3.f. Puzzle:** Consists of moving platforms and a blue-orange tornado pair. [Puzzle design and solution](https://drive.google.com/file/d/1qsl8Ee7bDcVV-sIYLEhMUT7XrC5u-Ilw/view?usp=sharing).
- **3.3.3.g. Hidden shortcut:** Appears near a tip-sign if the player equips the sword from the FireContinent.
- **3.3.3.h. Checkpoint system:** Checkpoints use blue crystals to mark safe spots, enhancing player experience during challenging platform jumps. Players respawn at the last checkpoint or can manually respawn using the menu.

##### 3.3.4. BossContinent
- **Purpose:** Players use all skills learned from previous levels to defeat the final boss.
- **3.3.4.a. Boss AI system:** Utilizes the vector3 API for movement. The boss moves and looks towards the player with `Vector3.MoveTowards` and `Transform.LookAt`. It attacks with cannon balls if players are within range, disappearing as its health reaches zero.
- **3.3.4.b. Environment:** The terrain is synthesized with defined boundaries and obstacles that allow players to strategize and dodge attacks.
- **3.3.4.c. Audio:** Includes sound effects for cannonball launch (Phase 1) and explosion (Phase 2) to indicate the boss's attacks.
- **3.3.4.d. Cutscene:** A cutscene triggers when transitioning to the BossContinent, enhancing the narrative and setting the stage for the final battle.

##### 3.3.5. Menu System/UI
- **3.3.5.a. Main Menu:** Allows the player to start, quit the game, or view credits. Uses a font similar to the parody inspiration (Legend of Zelda, Breath of the Wild).
- **3.3.5.b. Ingame Menu:** Features pause, restart, and quit options. Includes controls and objectives for player reference during gameplay.
- **3.3.5.c. DeathMenu:** Provides options to return to the beginning island or go back to the main menu upon death.
- **3.3.5.d. EndMenu:** Displays a message upon death, with options to restart the game or quit.
- **3.3.5.e. Fade In/Fade Out:** Scene transitions are visually enhanced with fade effects.
- **3.3.5.f. Credits:** Credits are accessible from the main menu and displayed at the end of the gameplay.

##### 3.3.6. Ending Scenes/Credit Scenes
- **Ending Scene:** A cutscene that completes the story with a twist ending. Character animations and subtitles are controlled by a timeline, providing a cinematic conclusion.

### 4. Known Bugs/Issues
- **4.1 Chest Accessibility:** Players may find it hard to open the chest occasionally. Adjusting position and orientation directly in front of the chest can help.
- **4.2 Punching Mechanism:** Sometimes triggers multiple times on enemies.
- **4.3 Sign Color Changes:** Can bug out briefly before displaying correctly due to the typewriter effect.
- **4.4 Save System Inconsistency:** If players return to the main menu after transitioning back from another area, the location is saved, but the items collected are not.

### 5. Division of Work

#### 5.1. Huy Nguyen (hnguyen441@gatech.edu)
- **Assets/GameObjects:**
  - PlayerUI
  - MenuCanvas
  - HeartsSystem
  - InfoPanel
  - All Menu Buttons (Pause, Respawn, Play, Exit, Main Menu)
  - Title Objects
  - Edit sign Texts in wind continent
- **Sprites/Fonts:**
  - hud_heartEmpty
  - hud_heartFull
  - The Wild Breath of Zelda
- **Scenes:**
  - MainMenu
  - DeathMenu
  - EndMenu
  - CreditsMenu
- **Scripts (.cs):**
  - GameMenuScript
  - HeartsConfig
  - HPDisplay
  - PauseScript
  - MenuToggle
  - SceneSwitchBegin
  - SceneSwitchBoss
  - SceneSwitchEnd
  - SceneSwitchFire
  - SceneSwitchWind
  - TransitionToMainFromCredits

#### 5.2. Andy Guo (aguo43@gatech.edu)
- **Assets/GameObjects:**
  - Cannon
  - NPC Talk
  - DialogCanvas
  - Invisible Wall
  - Projectile
  - ExplodingProjectile
- **Scripts:**
  - TypewriterEffect
  - DialogueScript
  - DialogueObject
  - CannonCollision
  - CannonController
  - PlayerCannon
  - LaunchProjectile
  - DialogueUI
  - RotateCannon
  - Explode
  - ProjectileCollision
 
#### 5.3. Yu-Han Sun (ysun662@gatech.edu)
- **Assets/GameObjects:**
  - All environmental assets for Beginning Island (except for Bridge) are from Unity.
  - Environment for Boss Continent (Terrain)
  - Terrain for Beginning Island (Terrain)
  - Invisible walls for Beginning Island
  - SceneChanger (Prefab)
  - Sign (Prefab)
  - Wooden Sword Chests with Textbox (Prefab)
  - Magic Bow Chests with Textbox (Prefab)
  - Global Object (Prefab)
  - Congratulations Text (Prefab)
  - AudioEventManager (Prefab)
  - DialogCoroutineManager (Prefab)
  - Spawn Locations
  - NPC Whale
  - Text for Interaction of Chest, NPC, and Signs
  - StoryText
  - FireGateSign
  - WindGateSign
- **Audio:**
  - BGM for all scenes, menus, and credits
  - All Player Sound Effects (Walk, Jump, Punch, Sword Swing, Bow Release, death, hurt)
  - All Boss Sound Effects (Cannonball launch, Cannonball explode)
  - All Impact Sound Effects (Sword damage dealt, Arrow damage dealt)
- **Scripts:**
  - ChestScript.cs
  - ChestAnimation.cs
  - SwordRotate.cs
  - AudioEventManager.cs
  - CongratulationsTextScript.cs
  - DialogueCoroutineManager.cs
  - GlobalControl.cs
  - Interaction.cs
  - ItemScript.cs
  - LoadOnActivation.cs
  - PickingUp.cs
  - PlayerAudioScript.cs
  - SceneChanger.cs
  - SignScript.cs
  - PickUp.cs
  - Interact.cs
- **Modifications to Existing Scripts:**
  - DialogueScript.cs
  - GameMenuScript.cs
  - InputController.cs
  - PlayerAnimationEvents.cs
- **Animations:**
  - Chest Animator Mecanim
  - Chest Open, Close, Half Open, Half Close (modified from asset)
  - Credits Menu Animation
  - Fade In
  - Fade Out
  - Whale Animator Mecanim

#### 5.4. Chen Xu (cxu369@gatech.edu)
- **Assets/GameObjects:**
  - Everything in the Wind Island
  - Crown (Prefab)
  - Checkpoint Crystals (Prefab)
  - Moving Platforms (Prefab)
  - Ghost Platform (Prefab)
  - Normal Platform (Prefab)
  - Shortcut Platform (Prefab)
  - Transport teleports (Prefab)
  - Cutscene in Wind Continent
  - Cutscene in Beginning Island
  - Checkpoint Sound
  - Respawn Wall
  - Heart Elixirs
  - Entire Puzzle Area in Wind Continent
  - All Sign Texts in Wind Continent
- **Scripts:**
  - WindBlow.cs
  - MovingPlatform.cs
  - HighJumpBounceAvoid.cs
  - cloudScript.cs
  - GhostPlatform.cs
  - Coin_Collect.cs
  - Collectibles_wave.cs
  - Elixir_Collect.cs
  - ActivateShortcut.cs
  - checkpointSoundPlayer.cs
  - shortcutMovingPlatform.cs
  - DisablePan.cs
  - CheckpointUtility.cs (edit)
  - RespawnPlayer.cs
- **Animation:**
  - Checkpoint crystal animation
  - Cutscenes in Beginning Island and Wind Island

#### 5.5. Bing Yang (byang322@gatech.edu)
- **Role:** Focuses on player animation, player control, and physics utilities.
- **Assets/GameObjects:**
  - The maze
  - The enemies
  - The weapons
- **Scripts Independently Written:**
  - All scripts within Bing/Scripts unless specified otherwise.
- **Collaborative Scripts:**
  - PickingUp.cs (Created by Yu-han)
  - Interact.cs (Created by Yu-han)
  - InputController.cs (Yu-han added functions for PickingUp and Interact)
  - PlayerAnimationEvents.cs (Yu-han added input handler for Interacting with objects)
- **Animations:**
  - Animation controller for the main character and enemy AI in the FireContinent
  
### Credits

#### Licenses
- **CC0:** Copyright-Only Dedication (Public Domain)
- **CC-BY 3.0:** Creative Commons Attribution 3.0
- **CC-BY 4.0:** Creative Commons Attribution 4.0
- **OGA-BY 3.0:** OpenGameArt Attribution 3.0
- **CC-BY-SA 3.0:** Creative Commons Attribution-Share Alike 3.0
- **CC-BY-SA 4.0:** Creative Commons Attribution-Share Alike 4.0
- **CC BY-NC-SA 4.0:** Attribution-NonCommercial-ShareAlike 4.0 International
- **GPL2:** GNU General Public License 2
- **Unity EULA:** Unity End User License Agreement
- **Zapsplat Standard License:** Attribution
- **Free for Personal, non-profit use**

#### Music
- **Soliloquy** by Matthew Pablo (CC-BY 3.0) [Link](https://opengameart.org/content/soliloquy)
- **Flute Sonata in E minor** by Dee Yan-Key (CC BY-NC-SA 4.0) [Link](https://freemusicarchive.org/music/Dee_Yan-Key/Flute_Sonatas/04--Dee_Yan-Key-Flute_Sonata_in_E_minor)
- **Death is Just Another Path** by Otto Halmen (CC-BY 3.0 OGA-BY 3.0) [Link](https://opengameart.org/content/death-is-just-another-path)
- **Happy Ending** by Varon Kein (CC-BY 3.0) [Link](https://opengameart.org/content/mc-happy-ending)
- **DreamTeam** by OveMelaa (CC-BY 3.0 CC-BY-SA 3.0) [Link](https://opengameart.org/content/ove-melaa-dream-team)
- **March** by SpringSpring (CC-BY 4.0 CC-BY 3.0 CC-BY-SA 4.0 CC-BY-SA 3.0) [Link](https://opengameart.org/content/march)
- Additional music from [Zapsplat](http://www.zapsplat.com/).

#### Sound Effects
- **3HealSpells** by DoKashiteru (CC-BY 3.0 GPL 2.0, GPL 3.0) [Link](https://opengameart.org/content/3-heal-spells#comment-form)
- **Win Sound Effect** by Listener (CC0) [Link](https://opengameart.org/content/win-sound-effect)
- **Battle Sound Effects** by artisticdude (CC-BY 3.0 CC-BY-SA 3.0 GPL 3.0 GPL 2.0 CC0) [Link](https://opengameart.org/content/battle-sound-effects)
- **PlayerSounds 1** by EmoPreben (CC0) [Link](https://opengameart.org/content/playersounds-1-by-emopreben)
- **Grass Foot Step Sounds** by Blender Foundation (CC-BY 3.0) [Link](https://opengameart.org/content/grass-foot-step-sounds-yo-frankie)
- **Punches, Hits, Swords and Squishes** by tarfmagougou (CC-BY-SA 3.0) [Link](https://opengameart.org/content/punches-hits-swords-and-squishes)
- Additional sound effects from [Zapsplat](http://www.zapsplat.com/).

#### Assets
- **RPG Character Mecanim Animation Pack** by Explosive (Standard Unity Asset Store EULA, Single Entity) [Link](https://assetstore.unity.com/packages/3d/animations/rpg-character-mecanim-animation-pack-63772#publisher)
- **SimpleFX - Cartoon Particles** by Synty Studios (Standard Unity Asset Store EULA, Extension Asset) [Link](https://assetstore.unity.com/packages/vfx/particles/simple-fx-cartoon-particles-67834)
- **Low Poly Cartoon Battle Tank** by Babka (Standard Unity Asset Store EULA, Single Entity) [Link](https://assetstore.unity.com/packages/3d/characters/low-poly-cartoon-battle-tank-assets-pack-207321)
- **PolyWorld: Low Poly Vistas** by Quantum Theory (Standard Unity Asset Store EULA, Single Entity) [Link](https://assetstore.unity.com/packages/3d/environments/landscapes/polyworld-low-poly-vistas-34775)
- **Stones and Buried Treasure** by Comeback (Standard Unity Asset Store EULA, Extension Asset) [Link](https://assetstore.unity.com/packages/3d/environments/fantasy/stones-and-buried-treasure-95557)
- **Bridges Pack** by Maxime Brunoni (Standard Unity Asset Store EULA, Extension Asset) [Link](https://assetstore.unity.com/packages/3d/props/bridges-pack-212950)

- **Simple Low Poly Nature** by Neutroncat (Standard Unity Asset Store EULA, Extension Asset) [Link](https://assetstore.unity.com/packages/3d/environments/landscapes/simple-low-poly-nature-pack-157552)
- **Low Poly Simple House** by Kunniki (Standard Unity Asset Store EULA, Extension Asset) [Link](https://assetstore.unity.com/packages/3d/characters/lowpoly-simple-house-117348)
- **Simple Water Shader URP** by Ignitecoders (Standard Unity Asset Store EULA, Extension Asset) [Link](https://assetstore.unity.com/packages/2d/textures-materials/water/simple-water-shader-urp-191449)
- **Low Poly Chests** by UniquePlayer (Standard Unity Asset Store EULA, Extension Asset) [Link](https://assetstore.unity.com/packages/3d/props/low-poly-chests-95342)
- **Cartoon Whale Low Poly** by Sr Studio Kerala (Standard Unity Asset Store EULA, Extension Asset) [Link](https://assetstore.unity.com/packages/3d/characters/animals/fish/cartoon-whale-low-poly-197919)
- **Lowpoly Medieval Peasant** by Polytope Studio (Standard Unity Asset Store EULA, Extension Asset) [Link](https://assetstore.unity.com/packages/3d/characters/humanoids/humans/lowpoly-medieval-peasants-free-pack-122225)
- **Low Poly Simple Graveyard** by EvilTorn (Standard Unity Asset Store EULA, Extension Asset) [Link](https://assetstore.unity.com/packages/3d/environments/dungeons/low-poly-simple-graveyard-134110)

- **Low Poly Dungeon** by Broken Vector (Standard Unity Asset Store EULA, Extension Asset) [Link](https://assetstore.unity.com/packages/3d/environments/dungeons/ultimate-low-poly-dungeon-143535)
- **Simple Heart Health System** by OArielG (Standard Unity Asset Store EULA, Extension Asset) [Link](https://assetstore.unity.com/packages/tools/gui/simple-heart-health-system-120676)

#### Fonts
- **The Wild Breath of Zelda Font** by Chequered Ink (Free for Personal, non-profit use) [Link](https://fontmeme.com/fonts/the-wild-breath-of-zelda-font/)
