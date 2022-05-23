CS6457 Project: Team Noobs README
Working Title: The Legend of Helga: Death of the Mild (Canon)
Updated: 4.24.22
Version: Final
 
1. Starter Scene
MainMenu
 
2. How to Play: Basic Controls
WASD: Move
Mouse Move: Camera
Mouse Click: Attack
Space bar: Jump (Hold = Higher)
Left Shift: Roll
E: Interact
F: Target Lockon
P: Pause
ESC: Menu
1: Fist
2: Sword (Reward)
3: Bow (Reward)
 
3. Gameplay and Associated Technology Requirements
3.1. Number of Levels
Our game has four levels in total: BeginningIsland, FireContinent, WindContinent and BossContinent
3.2. Gameplay Options
The player starts out on the beginning island once the game starts. We have three gates/portals to transport the player to other levels. There are also colliders to teleport back to the beginning island from the other continents. The game ends when the player beats the final boss in the BossContinent. In our current plan, the player can visit continents in any order he/she wants. However, when the game starts, the player is not given any weapons to use aside from their fists, which makes it difficult to beat the boss. It is recommended that the player visit the other two continents first to collect weapons before going into the boss fight. The rewards can be picked up from chests at the top of the summit in the WindContinent or in the middle of a maze guarded by strong enemies in the FireContinent.
We want to mention that the order of visiting different levels can change the gameplay.  In the Fire/Wind continents, we put shortcuts as an option to reach the respective destination if the player collects the weapon from the other area.  We think this makes exploring the world more fun once players find out the dependency between different levels.
3.3. Details for Each Level
3.3.1. BeginningIsland
Player starts the game in the BeginningIsland. In this area, we show the player how to interact with the game world, including:
3.3.1.a. Visual interaction with the 3D world. The player can move the camera to look around.
3.3.1.b. Animation controlled, physics enabled character control. 
3.3.1.c NPC interaction and Animation. There is one NPC in the beginning island who provides information on the basic movement control to the player. The interaction between the player and the NPC is triggered by the player's keyboard input.
3.3.1.d. Clear goals/subgoals communicated to the player.  The NPC provides information about the goal of the game (beat the boss) and the story to the player. There are two signs that the player can read for additional hints about what lies beyond each gate. 
3.3.1.e. Proper interaction with the world through physics simulation.  We put a dummy enemy (a Weeble Wobble) for the player to try the action set for attacks.  The interaction between the player and the dummy enemy is controlled by both trigger based collision (hit detection) and physics simulation (rigidbody based onHit movement of the dummy enemy).
3.3.1.f. Digital Audio. There is background music and sounds for all levels, menus and credit scenes. There are also player sound effects for walking, jumping, punching, swinging, bow releasing, sword hit on impact, arrow hit on impact, hurt sound and on death using animation unity events and the audio event manager schema.
3.3.1.g. Cutscenes. To provide additional story and guidance, the beginning island loads a cutscene when the game starts, showing an aerial view of the island, the three gates, and the NPC that the player can interact with. Each area also contains their respective cutscenes to give a preview of the area.
3.3.1.h. In-game polish. We added symbolic objects onto the gates leading to the other levels, such as a sword on the red gate, arrows on the green gate and bones under the orange gate. We hope these visual cues can help players know what game contents they are supposed to encounter for other levels.
3.3.1.i. Terrain. The ground terrain is synthesized. There are invisible walls to prevent the player from falling off the map and trigger colliders to help the player respawn from the last activated checkpoint.
P.S. The red gate leads to the FireContinent.  The green gate leads to the WindContinent.  The orange gate in the forest behind the house at the starting point leads to the boss area.  All these can be seen from the cutscene upon starting the game.
 
3.3.2. FireContinent
In our design, we use the FireContinent to help the player to get familiar with the basic fighting system.  The goal is to reach the destination depicted by the wooden chest in the center of a maze and collect the fire sword and unlock the teleport (golden crown) back to the beginning island.
3.3.2.a. Defined boundary for the level.  We created a maze for the player to explore.  The outer wall of the maze defines the boundary of this level.  Also, the surrounding mountains restrict the area players can explore (players can not climb across the mountains).
3.3.2.b. Communication of goals. We added two information boards outside/inside the level to provide information on the game goal to the player.  The board at the foot of the red gate implicitly informs the player of the reward they can collect in the Fire continent (the red sword). The board at the entry of the maze provides information on the design of the maze, namely yellow headed enemies guarding the wooden chest are much stronger than regular green-body enemies, and players can collect health potions as sub-reward which are useful for future gameplay.
3.3.2.c. Decision making during game play.  One goal of having a maze is to force the player to make decisions on how to reach the destination, namely the wooden chest.  Upon entering the area, the player can see the chest straight ahead.  The yellow-headed enemies are difficult to beat with bare-hand and starting maximum health.  To reward the players for exploring the maze, we put health potions that can both revive the player as well as boost their maximum health in different parts of the maze, which are very useful for the boss fight.  We hope both the strong enemies and the useful rewards can “force” players to explore the maze.
3.3.2.d. Non-boss enemy AI.  All enemies in the FireContinent are controlled by a NavMesh agent together with a simple procedural-based finite state machine.  The enemy AIs wander around predefined waypoints when the player is not in view.  When they “see” the player, they chase and strafe around to attack the player when in a certain distance.
3.3.2.e. AI movement controlled by animation.  All enemy movements are controlled by an animation controller based on the FSM in 3.3.2.d. 
3.3.2.f. Interaction with objects and fun gameplay. The wooden chest, which contains the final reward in this level, is animated using Mecanim and has several different poses (Open, Closed, Half Open, Half Closed). Interacting with the chest triggers those animations and some sound effects, making the item collection more fun.
 
3.3.3. WindContinent
In this level, we designed a puzzle-style jumping mini-game to create fun gameplay for the players.  The goal is to reach the destination at the summit of the mountain, grab the treasure in the chest, and exit using the golden crown.
3.3.3.a. Interaction with objects through physics simulation.  Players can jump onto a series of moving platforms to reach the destination (showed in the demo). Collisions between the character and the platforms are implemented based on rigidbody. 
3.3.3.b. Goals communicated to players.  Same as 3.3.2.b.
3.3.3.c. Defined boundary.  Same as 3.3.2.a.
3.3.3.d. Basic elements. (1)Static platform, (2) Moving platform, (3) Phantom platforms, which can disappear in a timely manner, so that players need to wait and catch the timing. (4) Blue-orange tornado pair: player can be transported in between the blue-orange tornado pair; this mechanism is used in the puzzle.
3.3.3.e. Decision making.  We designed three paths to reach the destination. Courage path(Platform jumping challenge), Trail of wisdom(Puzzle), and a shortcut. At the midpoint of the level, the player needs to make a decision and choose one of three paths to reach the destination.
3.3.3.f. Puzzle. The puzzle consists of moving platforms and blue-orange tornado pair, The puzzle design and solution can be found via this link: https://drive.google.com/file/d/1qsl8Ee7bDcVV-sIYLEhMUT7XrC5u-Ilw/view?usp=sharing
3.3.3.g. Hidden shortcut. Near the location where a sign gives the tip, if the player equips the sword gathered from the Fire Continent, the hidden shortcut will appear.
3.3.3.h. Checkpoint system. In the game, we've built a checkpoint mechanism that runs the entire time. This especially makes it more player-friendly when it comes to tackling difficult platform jumps. If players die, they will be respawn at the previous checkpoint, and players are also allowed to respawn manually using the Respawn button in the menu. On Wind Continent, checkpoints are some blue crystals, players can hear a sound and see the waking up animation once they trigger the checkpoint.
 
3.3.4. BossContinent
In this level, players can use all the skills they learn from the previous two levels to beat the final boss.
3.3.4.a. Boss AI system. The Boss AI system functions using predominantly the vector3 API to move the boss around. When the player first lands on the boss continent, the boss will move and look towards the player using the Vector3 MoveTowards function and the transform lookAt function. The boss will launch cannon balls at the player while moving towards the player if they are outside of the boss range. As the bosses’ health lowers to 0, the cannon platform will disappear and show that it has been defeated. 
3.3.4.b. Environment
The environment is synthesized with the terrain object. A wall is drawn to create an appropriate boundary to confide the player in the space. A few obstacles in the terrain allow the player to choose their play style. They can hide behind the obstacles to dodge from the Boss. 
3.3.4.c. Audio
To indicate the boss's attacks, we have added two sound effects for the two types of attacks. One for cannonball launching in Phase 1, and another for cannonball explosion in Phase 2.
3.3.4.c. Cutscene.
Transitioning to the boss continent will trigger a cutscene.
 
 
3.3.5. Menu System/UI
3.3.5.a. The main menu allows the player to start the game and quit it. They can also view the credits directly from the main menu and go back. The title graphic uses a similar font to the inspiration for the parody (Legend of Zelda, Breath of the Wild).
3.3.5.b. For the ingame menu, basic functionalities are pause, restart, and quit to the main menu. We also put controls + objective information to the in-game menu so that players have access to that information during the gameplay in case it is forgotten or for reference.
3.3.5.c. DeathMenu allows the player to return to the beginning island when they die or go back to the main menu.
3.3.5.d. EndMenu shows the message of death, along with the option to restart the game (and try a different path) or quit.
3.3.6.e. Fade In/Fade Out. Transitions between scenes are done aesthetically with fade in, fade out.
3.3.6.f. Credits. We added credits both at the main menu and at the end of the gameplay.
 
3.3.4. Ending Scenes/Credit Scenes
The Ending Scene is a cutscene that completes the story behind the game (with a twist ending). The character animations and subtitles in the cutscene are controlled by a timeline.
 
4. Known bugs/issues
4.1 The player may find it’s hard to open the chest occasionally. Adjusting position and orientation directly in front of the chest can help.
4.2 Punching sometimes triggers multiple times on enemies.
4.3 The code for color changes on signs can bug out briefly before displaying correctly due to the typewriter effect.
4.4 If players go back to the main menu after transitioning back from another area, the location is saved but the items collected are not.
 
5. Division of Work
5.1. Huy Nguyen (hnguyen441@gatech.edu)
Assets/Gameobjects
    PlayerUI
    MenuCanvas
    HeartsSystem
    InfoPanel
    All Menu Buttons (Pause, Respawn, Play, Exit, Main Menu)
    Title Objects
    Edit sign Texts in wind continent 
Sprites/Fonts
    hud_heartEmpty
    hud_heartFull
    The Wild Breath of Zelda
Scenes
    MainMenu
    DeathMenu
    EndMenu
    CreditsMenu
Scripts(.cs)
    GameMenuScript
    HeartsConfig
    HPDisplay
    PauseScript
    MenuToggle
    SceneSwitchBegin
    SceneSwitchBoss
    SceneSwitchEnd
    SceneSwitchFire
    SceneSwitchWind
    TransitionToMainFromCredits
 
5.2. Andy Guo (aguo43@gatech.edu)
Assets/GameObjects
    Cannon
    NPC Talk
    DialogCanvas
    Invisible Wall
    Projectile
    ExplodingProjectile
Scripts
    TypewriterEffect
    DialogueScript
    DialogueObject
    CannonCollision
    CannonController
    PlayerCannon
    LaunchProjectile
    DialogueUI
    RotateCannon
    Explode
    ProjectileCollision
 
5.3. Yu-Han Sun (ysun662@gatech.edu)
Assets/GameObjects
    All environmental assets for Beginning Island (except for Bridge) are from Unity.
    Environment for Boss Continent (Terrain)
    Terrain for Beginning Island (Terrain)
    Invisible walls for Beginning Island
    SceneChanger (Prefab)
    Sign (Prefab)
    Wooden Sword Chests with Textbox (Prefab)
    Magic Bow Chests with Textbox (Prefab)
    Global Object (Prefab)
    Congratulations Text (Prefab)
    AudioEventManager (Prefab)
    DialogCoroutineManager(Prefab)
    Spawn Locations
    NPC Whale
    Text for Interaction of Chest, NPC, and Signs
    StoryText
    FireGateSign
    WindGateSign
Audio:
    BGM for all scenes, menus and credits
    All Player Sound Effects
        Walk, Jump, Punch, Sword Swing, Bow Release, death, hurt (ouch)
    All Boss Sound Effects
        Cannonball launch, Cannonball explode
    All Impact Sound Effects
        Sword (damage dealt), Arrow (damage dealt)
Scripts:
    ChestScript.cs
    ChestAnimation.cs
    SwordRotate.cs
    AudioEventManager.cs
    CongratulationsTextScript.cs
    DialogueCoroutineManager.cs
    GlobalControl.cs
    Interaction.cs
    ItemScript.cs
    LoadOnActivation.cs
    PickingUp.cs
    PlayerAudioScript.cs
    SceneChanger.cs
    SignScript.cs
    PickUp.cs
    Interact.cs
Also made modifications to the following Scripts:
    DialogueScript.cs
    GameMenuScript.cs
    InputController.cs
    PlayerAnimationEvents.cs
Animations
    Chest Animator Mecanim
    Chest Open, Close, Half Open, Half Close (modified from asset)
    Credits Menu Animation
    Fade In
    Fade Out
    Whale Animator Mecanim
 
5.4. Chen Xu (cxu369@gatech.edu)
Assets/GameObjects
    Everything in the wind island
    Crown (Prefab)
    Checkpoint Crystals (Prefab)
    Moving Platforms (Prefab)
    Ghost Platform (Prefab)
    Normal Platform (Prefab)
    Shortcut Platform (Prefab)
    Transport teleports (Prefab)
    Cutscene in Wind Continent
    Cutscene in Beginning Island
    Checkpoint Sound
    Respawn Wall
    Heart Elixirs
    Entire Puzzle Area in wind continent
    All Sign Texts in wind continent    
Scripts
    WindBlow.cs, 
    MovingPlatform.cs, 
    HighJumpBounceAvoid.cs, 
    cloudScript.cs,   
    GhostPlatform.cs, 
    Coin_Collect.cs, 
    Collectibles_wave.cs,
    Elixir_Collect.cs,
    ActivateShortcut.cs,
    checkpointSoundPlayer.cs,
    shortcutMovingPlatfrom.cs,
    DisablePan.cs,
    CheckpointUtility.cs(edit),
    RespawnPlayer.cs
Animation
    checkpoint crystal animation
    cutscenes in beginning island and wind island
 
5.5. Bing Yang (byang322@gatech.edu)
Bing Yang works on the player animation, player control and physics utilities associated with this part.  The character model and animation clips are imported from ExplosiveLLC libraries (in the project).  Bing Yang writes the animation controller for the main character and for the enemy AI in the FireContinent.  Bing Yang also writes all the code necessary for controlling the movement of the player and the enemy AI in the FireContinent.  Bing Yang creates the maze in the FireContinent.  All the scripts written by Bing Yang can be found in Bing/Scripts.  Unless specified in the following list, all scripts are written by Bing independently.  
In the Bing/ folder, you can also check all the prefabs created by Bing Yang, including:
The maze
The enemies
The weapons
Scripts collaborated with other people or written by other people in Bing/Scripts folder:
PickingUp.cs (Created by Yu-han)
Interact.cs  (Created by Yu-han)
InputController.cs (Yu-han added functions for PickingUp and Interact)
PlayerAnimationEvents.cs (Yu-han added input handler for Interacting with objects)
 
 
Credits
Licenses
CC0				Copyright-Only Dedication (Public Domain)
CC-BY 3.0			Creative Commons Attribution 3.0
CC-BY 4.0			Creative Commons Attribution 4.0
OGA-BY 3.0 			OpenGameArt Attribution 3.0
CC-BY-SA 3.0			Creative Commons Attribution-Share Alike 3.0
CC-BY-SA 4.0			Creative Commons Attribution-Share Alike 4.0
CC BY-NC-SA 4.0		Attribution-NonCommercial-ShareAlike 4.0 International
GPL2				GNU General Public License 2
Unity EULA			Unity End User License Agreement
Zapsplat Standard License	Attribution
Free for Personal, non-profit use
 
Music
Soliloquy - Matthew Pablo (CC-BY 3.0)
https://opengameart.org/content/soliloquy 
Flute Sonata in E minor - Dee Yan-Key (CC BY-NC-SA 4.0)
https://freemusicarchive.org/music/Dee_Yan-Key/Flute_Sonatas/04--Dee_Yan-Key-Flute_Sonata_in_E_minor
Death is Just Another Path - Otto Halmen (CC-BY 3.0 OGA-BY 3.0)
https://opengameart.org/content/death-is-just-another-path
Happy Ending - Varon Kein (CC-BY 3.0)
https://opengameart.org/content/mc-happy-ending
DreamTeam - OveMelaa (CC-BY 3.0 CC-BY-SA 3.0)
https://opengameart.org/content/ove-melaa-dream-team
March - SpringSpring (CC-BY 4.0 CC-BY 3.0 CC-BY-SA 4.0 CC-BY-SA 3.0)
https://opengameart.org/content/march
Additional Music from http://www.zapsplat.com/
 
 
Sound Effects
3HealSpells - DoKashiteru (CC-BY 3.0 GPL 2.0, GPL 3.0)
https://opengameart.org/content/3-heal-spells#comment-form
Win Sound Effect - Listener (CC0)
https://opengameart.org/content/win-sound-effect
Battle Sound Effects - artisticdude (CC-BY 3.0 CC-BY-SA 3.0 GPL 3.0 GPL 2.0 CC0)
https://opengameart.org/content/battle-sound-effects
PlayerSounds 1 - EmoPreben (CC0)
https://opengameart.org/content/playersounds-1-by-emopreben
Grass Foot Step Sounds - Blender Foundation (CC-BY 3.0)
https://opengameart.org/content/grass-foot-step-sounds-yo-frankie
Punches, Hits, Swords and Squishes - tarfmagougou (CC-BY-SA 3.0)
https://opengameart.org/content/punches-hits-swords-and-squishes
 
Additional sound effects from http://www.zapsplat.com/
 
Assets
RPG Character Mecanim Animation Pack - Explosive (Standard Unity Asset Store EULA, Single Entity)
https://assetstore.unity.com/packages/3d/animations/rpg-character-mecanim-animation-pack-63772#publisher
SimpleFX - Cartoon Particles - Synty Studios (Standard Unity Asset Store EULA, Extension Asset)
https://assetstore.unity.com/packages/vfx/particles/simple-fx-cartoon-particles-67834
Low Poly Cartoon Battle Tank - Babka (Standard Unity Asset Store EULA, Single Entity)
https://assetstore.unity.com/packages/3d/characters/low-poly-cartoon-battle-tank-assets-pack-207321
PolyWorld: Low Poly Vistas - Quantum Theory (Standard Unity Asset Store EULA, Single Entity)
https://assetstore.unity.com/packages/3d/environments/landscapes/polyworld-low-poly-vistas-34775
Stones and buried treasure - Comeback (Standard Unity Asset Store EULA, Extension Asset)
https://assetstore.unity.com/packages/3d/environments/fantasy/stones-and-buried-treasure-95557
Bridges Pack - Maxime Brunoni (Standard Unity Asset Store EULA, Extension Asset)
https://assetstore.unity.com/packages/3d/props/bridges-pack-212950
 
Simple Low Poly Nature - Neutroncat (Standard Unity Asset Store EULA, Extension Asset)
https://assetstore.unity.com/packages/3d/environments/landscapes/simple-low-poly-nature-pack-157552
Low Poly Simple House - Kunniki (Standard Unity Asset Store EULA, Extension Asset)
https://assetstore.unity.com/packages/3d/characters/lowpoly-simple-house-117348
Simple Water Shader URP - Ignitecoders (Standard Unity Asset Store EULA, Extension Asset)
https://assetstore.unity.com/packages/2d/textures-materials/water/simple-water-shader-urp-191449
Low poly Chests - UniquePlayer (Standard Unity Asset Store EULA, Extension Asset)
https://assetstore.unity.com/packages/3d/props/low-poly-chests-95342
Cartoon Whale Low Poly - Sr Studio Kerala (Standard Unity Asset Store EULA, Extension Asset)
https://assetstore.unity.com/packages/3d/characters/animals/fish/cartoon-whale-low-poly-197919
Lowpoly Medieval Peasant - Polytope Studio (Standard Unity Asset Store EULA, Extension Asset)
https://assetstore.unity.com/packages/3d/characters/humanoids/humans/lowpoly-medieval-peasants-free-pack-122225
Low Poly Simple Graveyard - EvilTorn (Standard Unity Asset Store EULA, Extension Asset)
https://assetstore.unity.com/packages/3d/environments/dungeons/low-poly-simple-graveyard-134110
 
Low Poly Dungeon - Broken Vector (Standard Unity Asset Store EULA, Extension Asset)
https://assetstore.unity.com/packages/3d/environments/dungeons/ultimate-low-poly-dungeon-143535
Simple Heart Health System - OArielG (Standard Unity Asset Store EULA, Extension Asset)
https://assetstore.unity.com/packages/tools/gui/simple-heart-health-system-120676
 
Fonts
The Wild Breath of Zelda Font - Chequered Ink (Free for Personal, non-profit use)
https://fontmeme.com/fonts/the-wild-breath-of-zelda-font/
