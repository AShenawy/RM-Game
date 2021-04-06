The Plan.jslib file is used to connect Methodyca to the Research Plan database setup.

Currently, Methodyca does not implement the plugin, as the player is not asked to provide any details about their research plan (RP) during the course of the game.
If Methodyca is updated to include such thing, and the developer would like the player to input information about their RP in the game, and have this information saved and used by the database to fill in the information automatically for the player, then the methods can be called as listed in the Plan.jslib file.

A sample script is included in this folder to show how to call the methods from within a Unity C# script file.

N.B.: This Plan.jslib methods is intended for use in WebGL builds and my not work correctly for other types of builds. Testing and verification will be needed for other formats.