# Pack-My-Game

Download folder: https://github.com/daerlnaxe/Pack-My-Game/tree/master/Releases  

## What it does:
 * It copies then compress everything about a game contained in the db of LaunchBox app.
 * It generates a short xml file with the main information about the game
 * It takes images, pdf manual, video, music, rom file.
 * It takes also the cheatcode files if you fill the path ('GameName-*.*')
 * It generates a tree view file.
 * Support Cue files (1.4+ versions)
 * 7z and zip compression
 * Backup datas about a game from LaunchBox xml
 * When PackMe run,  creates an enhanced xml file, adding missing paths, additionally to the original backup
 * Permits to choose manually video, music, manual if db don't mention paths.
 * Contextual menu permits now to make some operations to construct the workfolder, to compress it later.
	
## Why ?
 * Because as a french gamer i wanted to save everything i fill about my games and keep it
for later, just in case there was a problem. 

## Note
 * Use it only with roms and cue files since 23/08/2020
 * Don't move or delete files from the source (never)
 * It asks before to overwrite roms, manuals, music, video that are in the working directory meanwhile
 the copy (the target directory)
 * Currently it asks a global permission to overwrite for the image/pictures files, even if there is
no image file in the the destination folder.
 * The clones are added only if they are grouped with a main in LaunchBox
 * Compression 7z and zip
 * It logs everything during the game treatment in a window, and a file.
		
## Versions
### Beta 1.6.0.0 22/20/2020 Work In Progress
 * BugFix: Path Creation (images)
 * First implementation of new system to compare files.

### Beta 1.5.0.4 10/10/2020
 * Bug Correction of unwanted space in "Cheats" Windows
 * Bug Correction of unwanted lines in "Cheats" Windows
 * Bug Correction when you erase system name
 * Bug Correction: Temporary solution. Currently sometimes there is a problem during compression, it's due to the
 access of the UI by another task. There is a call to avoid it in theory, but it seems sometimes there is a problem
 dispite of it. Then for the while this access are balised by a try/catch. It's not important because it's just very
 rare visual problem. I will try to resolve it because it's caused by another of my libraries, but i must use a bench
 protocol to reproduce the same condition.
 * Feature: open temp folder in explorer
 
 
### Beta 1.5.0.3 09/10/2020
 * Bug: correction on cancel button in the "Cheat" box. Error when you enlarged window + uneffective.
 * Bug: correction name suggested for a new cheat file comported a ":" characters as for the game name.
 * Bug: Detected, sometimes an unwanted space when you format text on "Cheat" box
 * Bug: Detected, if you erase system name then you pack a game => error. Must change in all case the way it's managed
 * New features on "files" box:
 	- removed button for copy files, now you have several options by contextual menus on each list box
	- You can open with external program musics/manuals/videos.
	- You can edit cheats codes.
	- You can delete by one/batch files for each section. 
 * New feature on "Cheat" box:
 	- A menu for search on two websites for cheats (see todo).
	- New contextual menu on textbox with select all/copy part/copy all/paste
	- On copy 4 spaces are converted to tabulation
	- Settings for tabulation have been modified.
 
### Beta 1.5.0.2 08/10/2020
 * Added an automatic limit length of text for the cheats codes.
 
### Beta 1.5.0.1 05/10/2020
 * Check for files is now repositioned just before compression.
 * New window indicating wich files have been copied, instead of old that only shows a checkbox.
 * This window can permit to add video/roms/cheats/musics/manuals files.
 * This window can permit to add a new file of cheats by a richtextbox, tabulation is allowed.
 * Based on Framework 4.8

### Beta 1.4 23/08/2020
 * Cue file supporting added, read the file and copy files mentionned to the rom directory.
 
### Beta 1.3.0.7 01/08/2020
 * Correction of many bugs.
 * Can pack game with a name containing some special chars (can't add some files before)
 * Added the ability to let you choose file name for the compression.
 * Tested on more than 50 games.

### Beta 1.3.0.4: 08/09/2018
 * Visual studio made a big bug on the main window, i hope i repared all the problems
 * Packme: Filter clones * 
 * Contextual menu
 * Several manual operations from contextual menu (as zip, 7zip, make info, backup etc...)
 * PackMe: 'Bug' fixed: original xml can don't contain path for video, music, because it probably use
 an algorithm additionnaly to the db, now PackMyGame will search in dedicated folders and purpose you some
 occurences. If they are checked, it means they match to the habitual name system. Check or Uncheck according to your needs.
 * Game become GameInfo, Game is now (i hope) the copy of what you find in LaunchBox. GameInfo don't contain paths anymore.
 * PackMe: In config menu, a checkbox system to controle the PackMe process according to your needs.
 * PackMe: Create a file containing original datas (OBGame)
 * PackMe: Create a file containing enhanced datas, in case of problems (EBGame). 
 * ...

### Alpha10: 27/08/2018
 * Set up of a system updating parameters to follow builds upgrade.
 * French Translation achieved
 * Translation files for variables
 * Some modifications about configuration (path verification, possibility to copy/paste...)
 * Bug fixes in configuration section
 * Integration of the help and credits sections 

### Alpha02: 26/08/2018
 * Box to ask for images 
 * Externalization of method: transfers verification (possibility to evolve to "decision for all" system
silent debug...)
 * Loop to process on  many games (implemented but not really tested - one game works with the same method)
 * Remove from listview the game packed

### Alpha01: 08/2018
 * 1st functionnal release
 * Trad Module Implantation: Ok
 * DotNetZip implanted instead of FileZip
 * Write Log	
 * Window log mode autokill, keep: ok			
 * Custom Box to handle collisions
 * Method to copy cheatcodes dir
 * 7z choice
 * File Schéma
 * Add Clones	


## TODO
- [ ] Work in progress: Eliminate duplicates images files function in contextual menu (md5 calcul)
- [ ] Filter supports
- [ ] Find a better way to handle images files
- [ ] Correct the english version		
- [ ] Carroussel to see image files to overwrite etc... (if necessary)		
- [x] Add Debug Options in panel config
- [ ] Mode silent without box prompt ? (All overwrite)
- [ ] Mode silent without log window
- [x] Contextual menu with 7zip only, ziponly ?
- [ ] Edit info in short list ? => it means to load total information of the game.
- [ ] Splashscreen on loading
- [ ] Ameliorate config with own browser system  and box path editable
- [x] md5 Compareason ?
- [ ] Move VFolder, HFolder, copyfile, reconstruct path (set a security basic path option)
- [ ] md5/sha Calculation of the package
- [x] bug for system to correct
- [x] bug of unwanted space in "cheat" box
- [ ] Make a system to have possibility to add new site for research (perhaps plugin, or config files)
- [ ] Make a system (not for now) to have the possibility to scrap to txt files, from a website (by plugin)
- [ ] See to count images in subfolder fo the master folder images and have something like x in the folder / total y for subfolders
- [ ] Find somebody that wants hire me... in CDI :/
