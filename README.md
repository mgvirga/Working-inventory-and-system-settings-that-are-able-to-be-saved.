# Working-inventory-and-system-settings-that-are-able-to-be-saved.
Worked on with Adam Pellegrini and made with Unity. We do not own, nor did we help create the assets and music used in this game. 
To navigate scripts open project 4.
Our game is a first person shooter controlled
by the keyboard and mouse. It translates and rotates. We have a health text in the lower left corner. 
That changes according to player health. We have different projectiles and use scripts to manage shooting 
with projectiles. The enemy health bar is the 3d GUI menu that always faces the player and changes when taking damage. 
Our storage holds a minimum of 6 items and a max of 12 items. Our items show the name and quantity. When the items are 
used pressing the keys 1,2,3,4 depending on what item slot they are in. If they are used up they are deleted and the inventory 
shifts over. The inventory is controlled by the user by pressing the i key. We have 3 different health items that heal their player. 
They do not work if the player would have exceeded 100 health points. They heal the player by 10, 20, and 30 points. The 
bullets all fire and are named according to their color. These are Red_bullet, Green_Bullet, and Blue_Bullet.
We have two different ai enemies. One patrols 5 waypoints and the other is idle. The one patrolling punches the
player with an attack animation and the idle one shoots the player. 

We have one background noise and one sound effect noise. The 
background noise is always playing and the sound effect noise plays when the menu is opened up. These are managed
by our scripts. We have a game settings menu that has more than 5 settings. This includes background sound,
sound effects, shadows, resolution, video quality, and more. This can be opened up with the escape key.
We save inventory information into a xml file and make the game read it before running. We save game settings to a 
binary formatter and are able to read it before running. We are able to load the inventory information and game settings from 
the previous time quit when starting the game. 
