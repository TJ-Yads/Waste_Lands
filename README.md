# Waste_Lands
Pipe Flow: This script is attached to each pipe and lagoon, it allows a pipe to recieve waste from other pipes and fill up over time, once a pipe fills it will check in a clockwise rotation on where it can send waste next based on it adjacency and nearby pipes. If a pipe is found then that pipe begins to fill if no pipe is found then its game over.


Pipe Spawner: This script managed the spawn system of pipes, when the player removes a pipe it will spawn a new one that is shown on screen, after that the new pipe enters a 3 pipe cooldown and cannot be seen until 3 more pipes are used.


Tile Spawn System: This script runs at the start of the game and will spawn the grid system of grass, houses, water and lagoons. The system allows for maps to be made larger or smaller and can have the amount of "special tiles" go up or down depending on map settings.


Tile Adjacent List: This script would run for both pipes and tiles to allow them to keep track of what tiles/pipes are in each direction. The script allows Pipe Flow to manage what direction a pipe can send waste to or if its a game over.


Pipe Placement Script: This script allows the player to pickup and place pipes on the spawn system or on tiles that do not have waste in them. When the player grabs a spawn system pipe it updates the Pipe Spawner so a new one will spawn, if the player goes to place a pipe it will snap to a location that can recieve a new pipe.
